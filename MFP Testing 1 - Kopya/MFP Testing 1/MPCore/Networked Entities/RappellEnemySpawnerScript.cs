using System;
using UnityEngine;

[Serializable]
public class RappellEnemySpawnerScript : MonoBehaviour
{

    public bool doMpSpawn = false;

    private BaseNetworkEntity networkHelper;

    private RootScript root;

    public SwitchScript[] inputSwitch;

    private float switchInput;

    public GameObject enemy;

    public GameObject rope;

    [Header("Rappell Settings")]
    public float ropeLength;

    public bool unhingeFromRappell;

    public bool unhingeOnDeath;

    public bool dontAllowSwinging;

    public bool doRaycastCheck;

    [Header("-----------------------------------------------")]
    public bool faceRight;

    [Header("-------------------- Idle ---------------------")]
    public bool dontFaceNoiseDirection;

    public bool standStill;

    public float walkLeftIdleAmount;

    public float walkRightIdleAmount;

    [Header("------------------ Hunt mode ------------------")]
    public float weapon;

    public bool standStillInHuntMode;

    public float walkLeftAbsoluteAmount;

    public float walkRightAbsoluteAmount;

    public int enemyType;

    private bool beenActivated;

    [Header("Limits - <-- Left")]
    public float hardLimitLeft;

    public float hardLimitLeftTop;

    public float hardLimitLeftTopTilt;

    public float hardLimitLeftBottom;

    public float hardLimitLeftBottomTilt;

    [Header("Limits - Right -->")]
    public float hardLimitRight;

    public float hardLimitRightTop;

    public float hardLimitRightTopTilt;

    public float hardLimitRightBottom;

    public float hardLimitRightBottomTilt;



    public RappellEnemySpawnerScript()
    {
        this.faceRight = true;
        this.walkLeftIdleAmount = (float)5;
        this.walkRightIdleAmount = (float)5;
        this.weapon = (float)1;
        this.walkLeftAbsoluteAmount = (float)7;
        this.walkRightAbsoluteAmount = (float)7;
        this.enemyType = 1;
        this.hardLimitLeftTopTilt = (float)1;
        this.hardLimitLeftBottomTilt = (float)1;
        this.hardLimitRightTopTilt = (float)1;
        this.hardLimitRightBottomTilt = (float)1;
    }


    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
    }

    public virtual void Update()
    {
        if (!doMpSpawn && !EMFDNS.isNull(networkHelper.interactingPlayer))
            doMpSpawn = true;



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


        if (this.switchInput >= (float)1 && !doMpSpawn)
            PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });


        if (doMpSpawn && !beenActivated)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.rope, this.transform.position, Quaternion.Euler((float)90, (float)180, (float)0));
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.enemy, this.transform.position + new Vector3((float)0, (float)-2, (float)0), Quaternion.Euler((float)0, (float)0, (float)0));
            StringScript stringScript = (StringScript)gameObject.GetComponent(typeof(StringScript));
            stringScript.dontAllowSwinging = this.dontAllowSwinging;
            stringScript.doRaycastCheck = this.doRaycastCheck;
            stringScript.allowDynamicLength = true;
            stringScript.ropeLength = (float)2;
            stringScript.weightedString = true;
            stringScript.hardLimitLeft = this.hardLimitLeft;
            stringScript.hardLimitLeftTop = this.hardLimitLeftTop;
            stringScript.hardLimitLeftTopTilt = this.hardLimitLeftTopTilt;
            stringScript.hardLimitLeftBottom = this.hardLimitLeftBottom;
            stringScript.hardLimitLeftBottomTilt = this.hardLimitLeftBottomTilt;
            stringScript.hardLimitRight = this.hardLimitRight;
            stringScript.hardLimitRightTop = this.hardLimitRightTop;
            stringScript.hardLimitRightTopTilt = this.hardLimitRightTopTilt;
            stringScript.hardLimitRightBottom = this.hardLimitRightBottom;
            stringScript.hardLimitRightBottomTilt = this.hardLimitRightBottomTilt;
            EnemyScript enemyScript = (EnemyScript)gameObject2.GetComponent(typeof(EnemyScript));
            enemyScript.themeOverride = 5;
            enemyScript.rappellRopeLength = this.ropeLength;
            enemyScript.rappelling = true;
            enemyScript.unhingeFromRappell = this.unhingeFromRappell;
            enemyScript.unhingeOnDeath = this.unhingeOnDeath;
            enemyScript.rappellTransform = gameObject.transform.Find("EndPiece");
            ((MeshRenderer)enemyScript.rappellTransform.GetComponent(typeof(MeshRenderer))).enabled = false;
            enemyScript.faceRight = this.faceRight;
            enemyScript.dontFaceNoiseDirection = this.dontFaceNoiseDirection;
            enemyScript.standStill = this.standStill;
            enemyScript.walkLeftIdleAmount = this.walkLeftIdleAmount;
            enemyScript.walkRightIdleAmount = this.walkRightIdleAmount;
            enemyScript.weapon = this.weapon;
            enemyScript.standStillInHuntMode = this.standStillInHuntMode;
            enemyScript.walkLeftAbsoluteAmount = this.walkLeftAbsoluteAmount;
            enemyScript.walkRightAbsoluteAmount = this.walkRightAbsoluteAmount;
            enemyScript.enemyType = this.enemyType;
            beenActivated = true;
        }
    }
}
