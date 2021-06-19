using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class MotorcycleWaveSpawnerScript : MonoBehaviour
{
    private BaseNetworkEntity networkHelper;


    private RootScript root;

    public GameObject enemy;

    public GameObject enemyCar;

    public GameObject ramp;

    public bool beenActivated;

    private bool beenActivatedDoOnce;

    public bool allowMultipleActivations;

    public bool dontGiveScore;

    public SwitchScript[] inputSwitch;

    private float switchInput;

    private SwitchScript switchScript;

    private MotorcycleScript[] waveEnemies;

    [Header("Props")]
    public Vector2[] rampPos;

    public Vector3[] rampSize;

    [Header("Enemies")]
    public bool[] isCar;

    public bool[] restrictMovement;

    public Vector2[] spawnPos;

    public Vector2[] movementZone;

    public Vector2[] movementZoneSize;

    public float[] speedMultiplier;

    public bool[] followX;

    public bool[] followZ;

    public bool[] pingPongX;

    public bool[] pingPongZ;

    private float saveTimer;

    private bool beenActivatedS;

    private bool beenActivatedDoOnceS;

    private float switchInputS;

    private MotorcycleScript[] waveEnemiesS;


    public virtual void saveState()
    {
        this.beenActivatedS = this.beenActivated;
        this.beenActivatedDoOnceS = this.beenActivatedDoOnce;
        this.switchInputS = this.switchInput;
        for (int i = 0; i < Extensions.get_length(this.waveEnemies); i++)
        {
            this.waveEnemiesS[i] = this.waveEnemies[i];
        }
    }

    public virtual void loadState()
    {
        this.beenActivated = this.beenActivatedS;
        this.beenActivatedDoOnce = this.beenActivatedDoOnceS;
        this.switchInput = this.switchInputS;
        for (int i = 0; i < Extensions.get_length(this.waveEnemies); i++)
        {
            this.waveEnemies[i] = this.waveEnemiesS[i];
        }
    }

    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.waveEnemies = new MotorcycleScript[Extensions.get_length(this.spawnPos)];
        this.waveEnemiesS = new MotorcycleScript[Extensions.get_length(this.waveEnemies)];
        this.switchScript = (SwitchScript)this.GetComponent(typeof(SwitchScript));
    }

    public virtual void Update()
    {
        this.switchInput = (float)-1;
        int i = 0;
        SwitchScript[] array = this.inputSwitch;
        int length = array.Length;
        while (i < length)
        {
            if (array[i].output > this.switchInput)
            {
                this.switchInput = array[i].output;
            }
            i++;
        }
        if (this.switchInput >= (float)1)
        {
            if (MultiplayerManagerTest.playingAsHost && EMFDNS.isNull(networkHelper.interactingPlayer) || MultiplayerManagerTest.inst.gamemode == MPGamemodes.Race)
            {
                networkHelper.ignoreMaxPacketsDoOnce = true;
                PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });
            }
            //this.beenActivated = true;
        }

        if (!EMFDNS.isNull(networkHelper.interactingPlayer))
            beenActivated = true;

        if (this.beenActivated)
        {
            if (!this.allowMultipleActivations && this.saveTimer < (float)15)
            {
                this.saveTimer += this.root.timescale;
                if (this.saveTimer >= (float)15)
                {
                    this.root.forceCheckpointSave = true;
                }
            }
            if (!this.beenActivatedDoOnce)
            {
                for (int j = 0; j < Extensions.get_length(this.rampPos); j++)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ramp, new Vector3(this.rampPos[j].x, (float)0, this.rampPos[j].y), Quaternion.Euler((float)270, (float)180, (float)0));
                    gameObject.transform.localScale = new Vector3(this.rampSize[j].x, this.rampSize[j].z, this.rampSize[j].y);
                }
                for (int k = 0; k < Extensions.get_length(this.spawnPos); k++)
                {
                    GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>((!this.isCar[k]) ? this.enemy : this.enemyCar, new Vector3(this.spawnPos[k].x, (float)1, this.spawnPos[k].y), Quaternion.Euler((float)270, (float)90, (float)0));
                    MotorcycleScript motorcycleScript = (MotorcycleScript)gameObject2.GetComponent(typeof(MotorcycleScript));
                    this.waveEnemies[k] = motorcycleScript;
                    motorcycleScript.isCar = this.isCar[k];
                    motorcycleScript.restrictMovement = this.restrictMovement[k];
                    motorcycleScript.movementZone = this.movementZone[k];
                    motorcycleScript.movementZoneSize = this.movementZoneSize[k];
                    motorcycleScript.speedMultiplier = this.speedMultiplier[k];
                    motorcycleScript.followX = this.followX[k];
                    motorcycleScript.followZ = this.followZ[k];
                    motorcycleScript.pingPongX = this.pingPongX[k];
                    motorcycleScript.pingPongZ = this.pingPongZ[k];
                    motorcycleScript.dontGiveScore = this.dontGiveScore;
                }
                this.beenActivatedDoOnce = true;
            }
            bool flag = true;
            for (int l = 0; l < Extensions.get_length(this.waveEnemies); l++)
            {
                if (!this.waveEnemies[l].dead)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                this.switchScript.output = (float)1;
                if (!this.allowMultipleActivations)
                {
                    ((MotorcycleWaveSpawnerScript)this.GetComponent(typeof(MotorcycleWaveSpawnerScript))).enabled = false;
                }
                else
                {
                    networkHelper.interactingPlayer = (Steamworks.CSteamID)0;
                    this.beenActivated = false;
                    this.beenActivatedDoOnce = false;
                }
            }
        }
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3((float)-16, (float)0, (float)16), new Vector3((float)16, (float)0, (float)16));
        Gizmos.DrawLine(new Vector3((float)-16, (float)0, -0.5f), new Vector3((float)16, (float)0, -0.5f));
        Gizmos.color = Color.cyan;
        for (int i = 0; i < Extensions.get_length(this.restrictMovement); i++)
        {
            if (!RuntimeServices.EqualityOperator(this.movementZone[i], null) && !RuntimeServices.EqualityOperator(this.movementZoneSize[i], null) && !RuntimeServices.EqualityOperator(this.spawnPos[i], null))
            {
                Gizmos.DrawWireCube(new Vector3(this.movementZone[i].x, (float)0, this.movementZone[i].y), new Vector3(this.movementZoneSize[i].x, (float)0, this.movementZoneSize[i].y));
                Gizmos.DrawLine(new Vector3(this.spawnPos[i].x, (float)0, this.spawnPos[i].y), new Vector3(this.movementZone[i].x, (float)0, this.movementZone[i].y));
            }
        }
        Gizmos.color = Color.yellow;
        for (int j = 0; j < Extensions.get_length(this.rampPos); j++)
        {
            Gizmos.DrawWireCube(new Vector3(this.rampPos[j].x, this.rampSize[j].y, this.rampPos[j].y), new Vector3(this.rampSize[j].x * (float)4, this.rampSize[j].y * (float)2, this.rampSize[j].z * (float)6));
        }
    }
}
