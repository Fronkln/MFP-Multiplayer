// Decompiled with JetBrains decompiler
// Type: SpawnDoorScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: C:\Users\orhan\Desktop\PedroModsAssembly\MPmod\Assembly-UnityScript.dll

using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class SpawnDoorScript : MonoBehaviour //PARTLY WORKS
{

    private BaseNetworkEntity networkHelper;

    [Header("-------------- Optimizations ---------------")]
    public bool mainBodyKinematic;
    public bool alwaysOnFlatGround;
    public bool dontDoLegBendRayCast;
    public bool dontDoArmBendRayCast;
    public bool dontWallCheckLeft;
    public bool dontWallCheckRight;
    [Header("--------------------------------------------")]
    public SwitchScript[] inputSwitch;
    private float switchInput;
    private SwitchScript switchScript;
    public GameObject enemy;
    public AudioClip[] openSound;
    public AudioClip closeSound;
    private AudioSource theAudioSource;
    public int nrOfEnemies;
    public int weapon;
    public bool disableWeaponPickup;
    [Header("-----------------------------------------------")]
    [HideInInspector]
    public bool attackAfterDoorSpawnForced;
    public bool attackAfterDoorSpawn;
    [HideInInspector]
    public bool faceRightForced;
    public bool faceRight;
    [Header("-------------------- Idle ---------------------")]
    public bool standStill;
    public float walkLeftIdleAmount;
    public float walkRightIdleAmount;
    [Header("------------------ Hunt mode ------------------")]
    public bool standStillInHuntMode;
    public float walkLeftAbsoluteAmount;
    public float walkRightAbsoluteAmount;
    [HideInInspector]
    public int forcedSpawns;
    [HideInInspector]
    public int forcedSpawnWeapon;
    [HideInInspector]
    public bool isTriggered;
    private bool allowSpawn;
    private float spawnTimer;
    private Quaternion startRot;
    private Vector3 startPos;
    private bool openDoor;
    private bool doorOpenDoOnce;
    private RootScript root;
    private RootSharedScript rootShared;
    private GameObject tempEnemy;
    private EnemyScript tempEnemyScript;


    public SpawnDoorScript()
    {
        this.nrOfEnemies = 1;
        this.weapon = 1;
        this.faceRight = true;
        this.walkLeftIdleAmount = 5f;
        this.walkRightIdleAmount = 5f;
        this.walkLeftAbsoluteAmount = 7f;
        this.walkRightAbsoluteAmount = 7f;
    }


    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    public virtual void Start()
    {
        this.startRot = this.transform.rotation;
        this.switchScript = (SwitchScript)this.GetComponent(typeof(SwitchScript));
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.rootShared = RootSharedScript.Instance;
        if (this.rootShared.lowEndHardware)
            this.transform.parent.Find("black_plane/Fade").gameObject.SetActive(false);
        this.theAudioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.root.nrOfEnemiesTotal += this.nrOfEnemies;
    }

    public virtual void Update()
    {

        if (!EMFDNS.isNull(networkHelper.interactingPlayer))
            isTriggered = true;

        if (!this.isTriggered && Extensions.get_length((System.Array)this.inputSwitch) > 0)
        {
            this.switchInput = -1f;
            int index = 0;
            SwitchScript[] inputSwitch = this.inputSwitch;
            for (int length = inputSwitch.Length; index < length; ++index)
            {
                if ((double)inputSwitch[index].output > (double)this.switchInput)
                    this.switchInput = inputSwitch[index].output;
            }
            if ((double)this.switchInput >= 1.0 && !isTriggered)
            {
                if (MultiplayerManagerTest.singleplayerMode)
                    this.isTriggered = true;
                else
                    PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });
            }
        }
        if (!this.isTriggered)
            return;
        if (this.openDoor)
        {
            this.transform.rotation = this.root.DampSlerp(this.startRot * Quaternion.Euler(0.0f, 0.0f, -50f), this.transform.rotation, 0.25f);
            if (!this.doorOpenDoOnce)
            {
                this.theAudioSource.clip = this.openSound[UnityEngine.Random.Range(0, Extensions.get_length((System.Array)this.openSound))];
                this.theAudioSource.volume = UnityEngine.Random.Range(0.8f, 0.9f);
                this.theAudioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
                this.theAudioSource.Play();
                this.doorOpenDoOnce = true;
            }
        }
        else
        {
            this.transform.rotation = this.root.DampSlerp(this.startRot, this.transform.rotation, 0.15f);
            if (this.doorOpenDoOnce)
            {
                this.theAudioSource.clip = this.closeSound;
                this.theAudioSource.volume = UnityEngine.Random.Range(0.9f, 1f);
                this.theAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                this.theAudioSource.Play();
                this.doorOpenDoOnce = false;
            }
        }
        int num = (double)this.spawnTimer <= 0.0 ? 1 : 0;
        if (num != 0)
            num = !this.openDoor ? 1 : 0;
        if (num != 0)
            num = this.nrOfEnemies > 0 ? 1 : 0;
        if (num == 0)
            num = this.forcedSpawns > 0 ? 1 : 0;
        if (num != 0)
            num = (double)Mathf.Round(this.transform.rotation.eulerAngles.y) == 180.0 ? 1 : 0;
        this.allowSpawn = num != 0;
        if (this.allowSpawn)
        {
            this.switchScript.output = 1f;
            this.tempEnemy = UnityEngine.Object.Instantiate<GameObject>(this.enemy, this.transform.position + new Vector3(2f, 1.5f, 1f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            ((Rigidbody)this.tempEnemy.GetComponent(typeof(Rigidbody))).constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
            ((Collider)this.tempEnemy.GetComponent(typeof(Collider))).enabled = false;
            this.tempEnemyScript = (EnemyScript)this.tempEnemy.GetComponent(typeof(EnemyScript));
            this.tempEnemyScript.doorSpawn = true;
            this.tempEnemyScript.hasBeenOnScreen = true;
            this.tempEnemyScript.mainBodyKinematic = this.mainBodyKinematic;
            this.tempEnemyScript.alwaysOnFlatGround = this.alwaysOnFlatGround;
            this.tempEnemyScript.dontDoLegBendRayCast = this.dontDoLegBendRayCast;
            this.tempEnemyScript.dontDoArmBendRayCast = this.dontDoArmBendRayCast;
            this.tempEnemyScript.dontWallCheckLeft = this.dontWallCheckLeft;
            this.tempEnemyScript.dontWallCheckRight = this.dontWallCheckRight;
            if (this.forcedSpawns > 0)
            {
                this.tempEnemyScript.weapon = (float)this.forcedSpawnWeapon;
                this.tempEnemyScript.disableWeaponPickup = this.disableWeaponPickup;
                this.tempEnemyScript.faceRight = this.faceRightForced;
                this.tempEnemyScript.standStill = this.standStill;
                this.tempEnemyScript.standStillInHuntMode = this.standStillInHuntMode;
                this.tempEnemyScript.walkLeftIdleAmount = this.walkLeftIdleAmount;
                this.tempEnemyScript.walkRightIdleAmount = this.walkRightIdleAmount;
                this.tempEnemyScript.walkLeftAbsoluteAmount = this.walkLeftAbsoluteAmount;
                this.tempEnemyScript.walkRightAbsoluteAmount = this.walkRightAbsoluteAmount;
                this.tempEnemyScript.attackAfterDoorSpawn = this.attackAfterDoorSpawnForced;
                --this.forcedSpawns;
                this.spawnTimer = 20f;
            }
            else
            {
                this.tempEnemyScript.weapon = (float)this.weapon;
                this.tempEnemyScript.disableWeaponPickup = this.disableWeaponPickup;
                this.tempEnemyScript.faceRight = this.faceRight;
                this.tempEnemyScript.standStill = this.standStill;
                this.tempEnemyScript.standStillInHuntMode = this.standStillInHuntMode;
                this.tempEnemyScript.walkLeftIdleAmount = this.walkLeftIdleAmount;
                this.tempEnemyScript.walkRightIdleAmount = this.walkRightIdleAmount;
                this.tempEnemyScript.walkLeftAbsoluteAmount = this.walkLeftAbsoluteAmount;
                this.tempEnemyScript.walkRightAbsoluteAmount = this.walkRightAbsoluteAmount;
                this.tempEnemyScript.attackAfterDoorSpawn = this.attackAfterDoorSpawn;
                --this.nrOfEnemies;
                this.spawnTimer = 60f;
                this.allowSpawn = false;
            }
            this.openDoor = true;
        }
        if (!((UnityEngine.Object)this.tempEnemyScript != (UnityEngine.Object)null))
            return;
        if (!this.tempEnemyScript.doorSpawn || !this.tempEnemyScript.enabled)
            this.openDoor = false;
        if (!this.tempEnemyScript.enabled && (double)this.spawnTimer > 0.0)
            this.spawnTimer -= this.root.timescale;
        if (this.nrOfEnemies > 0 || this.tempEnemyScript.enabled)
            return;
        this.switchScript.output = -1f;
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        if (!(col.tag == "Player") || Extensions.get_length((System.Array)this.inputSwitch) > 0)
            return;
        if (MultiplayerManagerTest.singleplayerMode)
            this.isTriggered = true;
        else
            PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });
    }

    public virtual void OnDrawGizmosSelected()
    {
        if ((double)Time.time == 0.0)
        {
            this.startPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1f);
            this.walkLeftIdleAmount = Mathf.Clamp(this.walkLeftIdleAmount, 0.0f, this.walkLeftAbsoluteAmount);
            this.walkRightIdleAmount = Mathf.Clamp(this.walkRightIdleAmount, 0.0f, this.walkRightAbsoluteAmount);
        }
        if (!this.standStill)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z - 1f), new Vector3(this.startPos.x - this.walkLeftIdleAmount, this.startPos.y + 0.5f, this.startPos.z));
            Gizmos.DrawLine(new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z - 1f), new Vector3(this.startPos.x + this.walkRightIdleAmount, this.startPos.y + 0.5f, this.startPos.z));
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z - 1f), new Vector3(this.transform.position.x + 2f - this.walkLeftAbsoluteAmount, this.transform.position.y + 0.5f, this.transform.position.z - 1f));
        Gizmos.DrawLine(new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z - 1f), new Vector3(this.transform.position.x + 2f + this.walkRightAbsoluteAmount, this.transform.position.y + 0.5f, this.transform.position.z - 1f));
    }
}
