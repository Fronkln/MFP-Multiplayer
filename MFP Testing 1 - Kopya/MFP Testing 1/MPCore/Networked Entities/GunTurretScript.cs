// Decompiled with JetBrains decompiler
// Type: GunTurretScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class GunTurretScript : MonoBehaviour
{

    private bool overrideSwitchInput = false;
    private BaseNetworkEntity networkHelper;

    private float health;
    public GameObject bullet;
    public SwitchScript[] inputSwitch;
    private float switchInput;
    public bool noShields;
    public bool raycastCheckForPlayer;
    public bool tutorialTurret;
    private bool tutorialTurretInPosition;
    public bool fireEvenIfPlayerIsNotWithinAimAngles;
    private SwitchScript localSwitchScript;
    private RootScript root;
    private Transform mainPlayer;
    private float animTimer;
    private AutoAimTargetScript autoAimScript;
    private Transform door;
    private Transform gunHolder;
    private Transform turret;
    private Transform shield1;
    private Transform shield2;
    private Transform pipe;
    private GameObject electric;
    private BoxCollider shield1Collider;
    private BoxCollider shield2Collider;
    private Transform aimFromPoint;
    private ParticleSystem smokeParticle;
    private ParticleSystem shellParticle;
    private float gunHolderStartZPos;
    private bool fireWeapon;
    private float fireDelay;
    private bool readyFire;
    [HideInInspector]
    public float fireTimer;
    private float aimSpeed;
    private float raycastCheckTimer;
    private bool canSeePlayer;
    private LayerMask layerMask;
    private float playerAngle;
    private Renderer gunHolderRend;
    private Vector2 hitColorOffset;
    private Vector2 hitColorOffsetTarget;
    private float hitTimer;
    private float electricAnimTimer;
    private Vector3 electricStartPos;
    private Transform healthBar;
    private AudioSource shootAudioSource;
    private AudioSource windUpAudioSource;
    private AudioSource appearAudioSource;
    private bool doorMovedDoOnce;

    public GunTurretScript()
    {
        this.health = 4.5f;
        this.raycastCheckForPlayer = true;
    }

    public virtual void OnTurretDetectPlayer()
    {
        fireEvenIfPlayerIsNotWithinAimAngles = true;
        overrideSwitchInput = true;
        canSeePlayer = true;
        switchInput = 1;
    }

    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.mainPlayer = GameObject.Find("Player/PlayerGraphics/Armature/Center/LowerBack/UpperBack/Neck/Head").transform;
        this.autoAimScript = (AutoAimTargetScript)this.GetComponent(typeof(AutoAimTargetScript));
        this.door = this.transform.Find("GunTurretHatchDoor");
        this.gunHolder = this.transform.Find("GunTurretHolder");
        this.turret = this.gunHolder.Find("GunTurretGun");
        this.shield1 = this.turret.Find("GunTurretShield1");
        this.shield2 = this.turret.Find("GunTurretShield2");
        this.pipe = this.turret.Find("GunTurretGunPipe");
        this.electric = this.gunHolder.Find("Electric").gameObject;
        this.aimFromPoint = this.transform.Find("AimFromPoint");
        this.shield1Collider = (BoxCollider)this.shield1.GetComponent(typeof(BoxCollider));
        this.shield2Collider = (BoxCollider)this.shield2.GetComponent(typeof(BoxCollider));
        this.smokeParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeParticle").GetComponent(typeof(ParticleSystem));
        this.shellParticle = (ParticleSystem)GameObject.Find("Main Camera/ShellParticle").GetComponent(typeof(ParticleSystem));
        this.shootAudioSource = (AudioSource)this.pipe.GetComponent(typeof(AudioSource));
        this.windUpAudioSource = (AudioSource)this.pipe.Find("WindUp").GetComponent(typeof(AudioSource));
        this.appearAudioSource = (AudioSource)this.door.GetComponent(typeof(AudioSource));
        this.windUpAudioSource.volume = 0.0f;
        this.windUpAudioSource.Stop();
        this.gunHolderStartZPos = this.gunHolder.localPosition.z;
        this.layerMask = (LayerMask)65792;
        this.gunHolderRend = (Renderer)this.gunHolder.GetComponent(typeof(Renderer));
        this.electric.SetActive(false);
        this.electricStartPos = this.electric.transform.localPosition;
        this.localSwitchScript = (SwitchScript)this.transform.GetComponent(typeof(SwitchScript));
        this.localSwitchScript.output = -1f;
        this.healthBar = this.transform.Find("GunTurretHealthBar");
        if (this.noShields)
        {
            this.shield1.gameObject.SetActive(false);
            this.shield2.gameObject.SetActive(false);
        }
        if (this.fireEvenIfPlayerIsNotWithinAimAngles)
            this.fireTimer = -150f;
        if (!this.tutorialTurret)
            return;
        this.health = 2f;
        float num1 = this.health / 4.5f;
        Vector3 localScale = this.healthBar.localScale;
        double num2 = (double)(localScale.y = num1);
        Vector3 vector3 = this.healthBar.localScale = localScale;
    }

    public virtual void Update()
    {



        if (!overrideSwitchInput)
        {
            this.switchInput = -1f;
            if ((double)this.health > 0.0)
            {
                if (Extensions.get_length((System.Array)this.inputSwitch) == 0)
                {
                    this.switchInput = 1f;
                }
                else
                {
                    int index = 0;
                    SwitchScript[] inputSwitch = this.inputSwitch;
                    for (int length = inputSwitch.Length; index < length; ++index)
                    {
                        if ((double)inputSwitch[index].output > (double)this.switchInput)
                            this.switchInput = inputSwitch[index].output;
                    }
                }
            }
        }
        if (this.raycastCheckForPlayer)
        {
            this.raycastCheckTimer += this.root.timescale;
            if ((double)this.raycastCheckTimer > 3.0)
            {
                RaycastHit hitInfo = new RaycastHit();
                this.canSeePlayer = !Physics.Linecast(this.mainPlayer.position, this.pipe.position, out hitInfo, (int)this.layerMask) || hitInfo.transform.tag == "GunTurret";
                this.raycastCheckTimer = 0.0f;
            }
        }
        else
            // this.canSeePlayer = true;
            PacketSender.BaseNetworkedEntityRPC("OnTurretDetectPlayer", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID });
        this.animTimer = Mathf.Clamp(this.animTimer + this.switchInput * this.root.timescale, 0.0f, 80f);
        this.hitTimer = Mathf.Clamp(this.hitTimer - this.root.timescale, 0.0f, this.hitTimer);
        if ((double)this.hitTimer <= 0.0)
        {
            this.electric.SetActive(false);
        }
        else
        {
            this.electricAnimTimer += this.root.timescale;
            if ((double)this.electricAnimTimer > 2.0)
            {
                this.electricAnimTimer = 0.0f;
                int num1 = UnityEngine.Random.Range(0, 360);
                Quaternion localRotation = this.electric.transform.localRotation;
                Vector3 eulerAngles = localRotation.eulerAngles;
                double num2 = (double)(eulerAngles.z = (float)num1);
                Vector3 vector3 = localRotation.eulerAngles = eulerAngles;
                Quaternion quaternion = this.electric.transform.localRotation = localRotation;
                this.electric.transform.localScale = new Vector3(UnityEngine.Random.Range(0.05f, 0.2f), UnityEngine.Random.Range(0.1f, 0.45f), 1f);
                this.electric.transform.localPosition = this.electricStartPos + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f), 0.0f);
            }
        }
        if ((double)this.switchInput >= 1.0)
        {
            this.localSwitchScript.output = 1f;
            if ((UnityEngine.Object)this.autoAimScript != (UnityEngine.Object)null)
                this.autoAimScript.enabled = true;
            this.tutorialTurretInPosition = (double)Quaternion.Angle(this.turret.localRotation, Quaternion.Euler(0.0f, this.playerAngle, 0.0f)) < 5.0;
            if (this.tutorialTurret)
            {
                this.root.neverDoDodgeHelper = false;
                this.root.alwaysDoDodgeHelper = true;
                this.fireWeapon = true;
                this.fireTimer = -100f;
                this.animTimer = 100f;
            }
            if ((double)this.animTimer > 40.0)
            {
                if (this.canSeePlayer)
                {
                    mainPlayer = EMFDNS.GetNearestPlayer(transform.position);

                    this.playerAngle = (float)(180.0 + (double)Mathf.Atan2(this.aimFromPoint.InverseTransformPoint(this.mainPlayer.position).x, this.aimFromPoint.InverseTransformPoint(this.mainPlayer.position).z) * 57.2957801818848);
                    if (this.fireEvenIfPlayerIsNotWithinAimAngles || (double)Quaternion.Angle(this.turret.localRotation, Quaternion.Euler(0.0f, this.playerAngle, 0.0f)) < 5.0)
                    {
                        this.readyFire = true;
                        if ((double)Vector2.Distance((Vector2)this.transform.position, (Vector2)this.mainPlayer.position) < 40.0)
                            this.root.autoSaveTimer = 0.0f;
                    }
                }
                else
                    this.playerAngle = Mathf.Clamp(Mathf.Sin(Time.time * 0.5f) * 89f, -60f, 60f);
                if ((double)this.playerAngle < 180.0 && (double)this.playerAngle > 89.0)
                    this.playerAngle = 89f;
                else if ((double)this.playerAngle > 180.0 && (double)this.playerAngle < 271.0)
                    this.playerAngle = 271f;
                if (this.tutorialTurret)
                {
                    this.playerAngle = 271f;
                    this.readyFire = true;
                    this.aimSpeed = 0.6f;
                }
                this.turret.localRotation = Quaternion.RotateTowards(this.turret.localRotation, Quaternion.Euler(0.0f, this.playerAngle, 0.0f), this.aimSpeed * this.root.timescale);
            }
            if (this.readyFire && (double)this.fireTimer >= 0.0)
            {
                float num1 = this.pipe.localRotation.eulerAngles.z + 3f * this.root.timescale;
                Quaternion localRotation = this.pipe.localRotation;
                Vector3 eulerAngles = localRotation.eulerAngles;
                double num2 = (double)(eulerAngles.z = num1);
                Vector3 vector3 = localRotation.eulerAngles = eulerAngles;
                Quaternion quaternion = this.pipe.localRotation = localRotation;
                this.fireTimer += this.root.timescale;
                if ((double)this.fireTimer > 50.0)
                {
                    this.aimSpeed = 0.2f;
                    this.fireWeapon = true;
                }
                else
                {
                    this.aimSpeed = 0.0f;
                    if (!this.windUpAudioSource.isPlaying)
                        this.windUpAudioSource.Play();
                    this.windUpAudioSource.volume = Mathf.Clamp01(this.fireTimer / 35f);
                    this.windUpAudioSource.pitch = Mathf.Clamp01(this.fireTimer / 45f) * 1.25f;
                }
                if ((double)this.fireTimer > 220.0)
                {
                    this.fireWeapon = false;
                    this.aimSpeed = 0.0f;
                }
                if ((double)this.fireTimer > 260.0)
                {
                    this.fireTimer = (float)UnityEngine.Random.Range(-180, -140);
                    this.readyFire = false;
                }
            }
            else
            {
                this.fireTimer = Mathf.Clamp(this.fireTimer + this.root.timescale, this.fireTimer, 0.0f);
                this.aimSpeed = this.root.Damp(2f, this.aimSpeed, 0.05f);
                this.windUpAudioSource.volume = Mathf.Clamp01(this.windUpAudioSource.volume - 0.025f * this.root.timescale);
                this.windUpAudioSource.pitch = Mathf.Clamp(this.windUpAudioSource.pitch - 0.05f * this.root.timescale, 0.0f, 10f);
            }
            this.fireDelay -= this.root.timescale;
            if ((double)this.animTimer >= 80.0 && this.fireWeapon && (double)this.fireDelay <= 0.0)
            {
                this.smokeParticle.Emit(this.root.generateEmitParams(this.pipe.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                this.shellParticle.Emit(this.root.generateEmitParams(this.pipe.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                GameObject bullet = this.root.getBullet(this.pipe.position - this.pipe.forward, Quaternion.LookRotation(-this.pipe.forward));
                BulletScript bulletScript = this.root.getBulletScript();
                bulletScript.bulletStrength = !this.tutorialTurret ? 0.1f : 2f;
                bulletScript.bulletSpeed = 20f;
                bulletScript.bulletLength = 2f;
                bulletScript.fromTransform = this.transform;
                bulletScript.turretBullet = true;
                bulletScript.friendly = this.tutorialTurret && !this.tutorialTurretInPosition;
                bulletScript.doPostSetup();
                this.root.getMuzzleFlash(2, this.pipe.position, Quaternion.LookRotation(-this.pipe.forward) * Quaternion.Euler(0.0f, 270f, 0.0f));
                bullet.transform.rotation *= Quaternion.Euler(UnityEngine.Random.Range(-1f, 1f), 0.0f, 0.0f);
                BoxCollider component = (BoxCollider)bullet.GetComponent(typeof(BoxCollider));
                Physics.IgnoreCollision((Collider)component, (Collider)this.shield1Collider, true);
                Physics.IgnoreCollision((Collider)component, (Collider)this.shield2Collider, true);
                bulletScript.collisionIgnoreLogic((Collider)this.shield1Collider);
                bulletScript.collisionIgnoreLogic((Collider)this.shield2Collider);
                this.shootAudioSource.volume = UnityEngine.Random.Range(0.9f, 1f);
                this.shootAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                this.shootAudioSource.Play();
                this.fireDelay = 4f;
            }
            this.hitColorOffset += (this.hitColorOffsetTarget - this.hitColorOffset) * Mathf.Clamp01(this.root.timescale * 0.05f);
            this.gunHolderRend.materials[1].SetTextureOffset("_MainTex", this.hitColorOffset);
        }
        else
        {
            if ((UnityEngine.Object)this.autoAimScript != (UnityEngine.Object)null)
                this.autoAimScript.enabled = false;
            if (this.tutorialTurret && this.root.alwaysDoDodgeHelper)
                this.tutorialTurret = false;
            this.turret.localRotation = Quaternion.RotateTowards(this.turret.localRotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), 3f * this.root.timescale);
            this.windUpAudioSource.volume = Mathf.Clamp01(this.windUpAudioSource.volume - 0.025f * this.root.timescale);
            this.windUpAudioSource.pitch = Mathf.Clamp(this.windUpAudioSource.pitch - 0.05f * this.root.timescale, 0.0f, 10f);
            if ((double)this.animTimer <= 0.0 && (double)this.health <= 0.0)
            {
                this.gunHolder.gameObject.SetActive(false);
                this.aimFromPoint.gameObject.SetActive(false);
                this.root.enemyEngagedWithPlayer = true;
                ((Behaviour)this.transform.GetComponent(typeof(GunTurretScript))).enabled = false;
            }
        }
        float num3 = Mathf.Lerp(0.0f, 0.25f, Mathf.Clamp01(this.animTimer / 15f));
        Vector3 localPosition1 = this.door.localPosition;
        double num4 = (double)(localPosition1.z = num3);
        Vector3 vector3_1 = this.door.localPosition = localPosition1;
        if ((double)this.door.localPosition.z <= 0.100000001490116)
        {
            if (!this.doorMovedDoOnce)
            {
                this.appearAudioSource.pitch = 0.5f;
                this.appearAudioSource.volume = 1f;
                this.appearAudioSource.Play();
                this.doorMovedDoOnce = true;
            }
        }
        else if (this.doorMovedDoOnce)
        {
            this.appearAudioSource.pitch = 1f;
            this.appearAudioSource.volume = 1f;
            this.appearAudioSource.Play();
            this.doorMovedDoOnce = false;
        }
        float num5 = Mathf.Lerp(this.gunHolderStartZPos, 0.0f, Mathf.Clamp01((this.animTimer - 20f) / 20f));
        Vector3 localPosition2 = this.gunHolder.localPosition;
        double num6 = (double)(localPosition2.z = num5);
        Vector3 vector3_2 = this.gunHolder.localPosition = localPosition2;
        float num7 = Mathf.Lerp(180f, 0.0f, Mathf.Clamp01((this.animTimer - 30f) / 40f));
        Quaternion localRotation1 = this.gunHolder.localRotation;
        Vector3 eulerAngles1 = localRotation1.eulerAngles;
        double num8 = (double)(eulerAngles1.z = num7);
        Vector3 vector3_3 = localRotation1.eulerAngles = eulerAngles1;
        Quaternion quaternion1 = this.gunHolder.localRotation = localRotation1;
        if (this.noShields)
            return;
        float num9 = Mathf.Lerp(0.0f, -75f, Mathf.Clamp01((this.animTimer - 40f) / 20f));
        Quaternion localRotation2 = this.shield1.localRotation;
        Vector3 eulerAngles2 = localRotation2.eulerAngles;
        double num10 = (double)(eulerAngles2.x = num9);
        Vector3 vector3_4 = localRotation2.eulerAngles = eulerAngles2;
        Quaternion quaternion2 = this.shield1.localRotation = localRotation2;
        float num11 = Mathf.Lerp(0.0f, 75f, Mathf.Clamp01((this.animTimer - 40f) / 20f));
        Quaternion localRotation3 = this.shield2.localRotation;
        Vector3 eulerAngles3 = localRotation3.eulerAngles;
        double num12 = (double)(eulerAngles3.x = num11);
        Vector3 vector3_5 = localRotation3.eulerAngles = eulerAngles3;
        Quaternion quaternion3 = this.shield2.localRotation = localRotation3;
    }

    public virtual void bulletHit(float strength)
    {
        if (this.tutorialTurret && (!this.tutorialTurret || !this.tutorialTurretInPosition) || (double)this.health <= 0.0)
            return;
        this.health -= strength;
        this.root.cCheckGiSc = true;
        this.root.giveScore(20f, this.root.GetTranslation("bul14"), false);
        if ((double)this.health <= 0.0)
        {
            this.root.cCheckGiSc = true;
            this.root.giveScore(3000f, this.root.GetTranslation("bul18"), true);
            this.localSwitchScript.output = -1f;
            this.root.explode(this.turret.position, 1.7f, 2, -this.transform.forward * 3f, "Yellow", true, true);
            this.health = 0.0f;
        }
        float num1 = this.health / 4.5f;
        Vector3 localScale = this.healthBar.localScale;
        double num2 = (double)(localScale.y = num1);
        Vector3 vector3 = this.healthBar.localScale = localScale;
        this.hitColorOffsetTarget = new Vector2((float)((1.0 - (double)this.health / 10.0) * -0.150000005960464), (float)((1.0 - (double)this.health / 10.0) * -0.0900000035762787));
        this.hitColorOffset = new Vector2(0.0f, 0.07f);
        this.hitTimer = 3f + (float)((10.0 - (double)this.health) * 2.0);
        this.electric.SetActive(true);
    }
}
