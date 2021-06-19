using System;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x020000BB RID: 187
[Serializable]
public class SkyfallEnemySpawnerScript : MonoBehaviour
{
    private BaseNetworkEntity networkHelper;

    // Token: 0x040010CF RID: 4303
    private RootScript root;

    // Token: 0x040010D0 RID: 4304
    public GameObject enemy;

    // Token: 0x040010D1 RID: 4305
    public bool isBoss;

    // Token: 0x040010D2 RID: 4306
    private PlayerScript theBoss;

    // Token: 0x040010D3 RID: 4307
    public bool beenActivated;

    // Token: 0x040010D4 RID: 4308
    private bool beenActivatedDoOnce;

    // Token: 0x040010D5 RID: 4309
    private float beenActivatedTimer;

    // Token: 0x040010D6 RID: 4310
    public SwitchScript[] inputSwitch;

    // Token: 0x040010D7 RID: 4311
    private float switchInput;

    // Token: 0x040010D8 RID: 4312
    private SwitchScript switchScript;

    // Token: 0x040010D9 RID: 4313
    private EnemyScript[] waveEnemies;

    // Token: 0x040010DA RID: 4314
    [Header("Enemies")]
    public Vector2[] spawnPos;

    // Token: 0x040010DB RID: 4315
    public float[] targetPos;

    // Token: 0x040010DC RID: 4316
    public float[] moveAmount;

    // Token: 0x040010DD RID: 4317
    public float[] moveSpeed;

    // Token: 0x040010DE RID: 4318
    [Header("Objects")]
    public GameObject[] objects;

    // Token: 0x040010DF RID: 4319
    public Vector4[] objTypeAndSizeAndStartPos;

    // Token: 0x040010E0 RID: 4320
    public Vector3[] targetPosAndMoveAmountAndSpeed;

    // Token: 0x040010E1 RID: 4321
    private bool beenActivatedS;

    // Token: 0x040010E2 RID: 4322
    private bool beenActivatedDoOnceS;

    // Token: 0x040010E3 RID: 4323
    private float beenActivatedTimerS;

    // Token: 0x040010E4 RID: 4324
    private float switchInputS;

    // Token: 0x040010E5 RID: 4325
    private EnemyScript[] waveEnemiesS;

    // Token: 0x040010E6 RID: 4326
    private float switchScriptOutputS;

    // Token: 0x0600053D RID: 1341 RVA: 0x000907A4 File Offset: 0x0008E9A4
    public virtual void saveState()
    {
        this.beenActivatedS = this.beenActivated;
        this.beenActivatedDoOnceS = this.beenActivatedDoOnce;
        this.beenActivatedTimerS = this.beenActivatedTimer;
        this.switchInputS = this.switchInput;
        for (int i = 0; i < Extensions.get_length(this.waveEnemies); i++)
        {
            this.waveEnemiesS[i] = this.waveEnemies[i];
        }
        this.switchScriptOutputS = this.switchScript.output;
    }

    // Token: 0x0600053E RID: 1342 RVA: 0x00090824 File Offset: 0x0008EA24
    public virtual void loadState()
    {
        this.beenActivated = this.beenActivatedS;
        this.beenActivatedDoOnce = this.beenActivatedDoOnceS;
        this.beenActivatedTimer = this.beenActivatedTimerS;
        this.switchInput = this.switchInputS;
        for (int i = 0; i < Extensions.get_length(this.waveEnemies); i++)
        {
            this.waveEnemies[i] = this.waveEnemiesS[i];
        }
        this.switchScript.output = this.switchScriptOutputS;
    }

    // Token: 0x0600053F RID: 1343 RVA: 0x00003EBA File Offset: 0x000020BA
    public virtual void LateUpdate()
    {

        if (MultiplayerManagerTest.inst.gamemode != MPGamemodes.Race)
            return;

        if (this.root.doCheckpointSave)
        {
            this.saveState();
        }
        if (this.root.doCheckpointLoad)
        {
            this.loadState();
        }
    }


    public void Awake() => networkHelper = gameObject.AddComponent<BaseNetworkEntity>();

    // Token: 0x06000540 RID: 1344 RVA: 0x000908A4 File Offset: 0x0008EAA4
    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.waveEnemies = new EnemyScript[Extensions.get_length(this.spawnPos)];
        this.switchScript = (SwitchScript)this.GetComponent(typeof(SwitchScript));
        this.waveEnemiesS = new EnemyScript[Extensions.get_length(this.waveEnemies)];
    }

    // Token: 0x06000541 RID: 1345 RVA: 0x0009091C File Offset: 0x0008EB1C
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


        if (this.switchInput >= (float)1 && !beenActivatedDoOnce)
        {
            if (MultiplayerManagerTest.playingAsHost && EMFDNS.isNull(networkHelper.interactingPlayer) || MultiplayerManagerTest.inst.gamemode == MPGamemodes.Race)
            {
                networkHelper.ignoreMaxPacketsDoOnce = true;
                PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });
            }
            //this.beenActivated = true;
        }


        //if (this.switchInput >= (float)1)
        //{
        //this.beenActivated = true;
        //	}



        if (!EMFDNS.isNull(networkHelper.interactingPlayer))
            beenActivated = true;

        if (this.beenActivated)
        {
            if (!this.beenActivatedDoOnce)
            {
                float num = 0f;
                for (num = (float)0; num < (float)Extensions.get_length(this.spawnPos); num += (float)1)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.enemy, new Vector3(this.spawnPos[(int)num].x, this.spawnPos[(int)num].y, (float)0), Quaternion.Euler((float)0, (float)0, (float)180));
                    if (this.isBoss)
                    {
                        this.theBoss = (PlayerScript)gameObject.GetComponent(typeof(PlayerScript));
                        this.theBoss.isEnemy = true;
                        this.theBoss.skyfall = true;
                    }
                    else
                    {
                        EnemyScript enemyScript = (EnemyScript)gameObject.GetComponent(typeof(EnemyScript));
                        this.waveEnemies[(int)num] = enemyScript;
                        enemyScript.skyfall = true;
                        enemyScript.skyfallYPos = this.targetPos[(int)num];
                        enemyScript.skyfallYMoveAmount = this.moveAmount[(int)num];
                        enemyScript.skyfallYMoveSpeed = this.moveSpeed[(int)num];
                    }
                }
                if (Extensions.get_length(this.objTypeAndSizeAndStartPos) > 0)
                {
                    for (num = (float)0; num < (float)Extensions.get_length(this.objTypeAndSizeAndStartPos); num += (float)1)
                    {
                        GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.objects[(int)this.objTypeAndSizeAndStartPos[(int)num].x], new Vector3(this.objTypeAndSizeAndStartPos[(int)num].z, this.objTypeAndSizeAndStartPos[(int)num].w, (float)0), Quaternion.Euler((float)0, (float)0, (float)0));
                        SkyfallObjectScript skyfallObjectScript = ((SkyfallObjectScript)gameObject2.AddComponent(typeof(SkyfallObjectScript))) as SkyfallObjectScript;
                        if ((SaveStateControllerScript)gameObject2.GetComponent(typeof(SaveStateControllerScript)) == null && (SaveStateSimpleControllerScript)gameObject2.GetComponent(typeof(SaveStateSimpleControllerScript)) == null)
                        {
                            SaveStateControllerScript saveStateControllerScript = ((SaveStateControllerScript)gameObject2.AddComponent(typeof(SaveStateControllerScript))) as SaveStateControllerScript;
                            saveStateControllerScript.createWrapper = true;
                        }
                        gameObject2.transform.localScale = Vector3.one * this.objTypeAndSizeAndStartPos[(int)num].y;
                        skyfallObjectScript.inputSwitch = (SwitchScript)this.GetComponent(typeof(SwitchScript));
                        skyfallObjectScript.skyfallYPos = this.targetPosAndMoveAmountAndSpeed[(int)num].x;
                        skyfallObjectScript.skyfallYMoveAmount = this.targetPosAndMoveAmountAndSpeed[(int)num].y;
                        skyfallObjectScript.skyfallYMoveSpeed = this.targetPosAndMoveAmountAndSpeed[(int)num].z;
                    }
                }
                this.beenActivatedDoOnce = true;
            }
            if (this.beenActivatedTimer < (float)120 && this.beenActivatedTimer < (float)9999)
            {
                this.beenActivatedTimer += this.root.timescale;
                if (this.beenActivatedTimer >= (float)120)
                {
                    this.root.forceCheckpointSave = true;
                    this.beenActivatedTimer = (float)10000;
                }
            }
            bool flag = true;
            if (this.isBoss)
            {
                if (this.theBoss.health > (float)0)
                {
                    flag = false;
                }
            }
            else
            {
                for (int j = 0; j < Extensions.get_length(this.waveEnemies); j++)
                {
                    if (this.waveEnemies[j].enabled)
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                this.switchScript.output = (float)1;
                ((SkyfallEnemySpawnerScript)this.GetComponent(typeof(SkyfallEnemySpawnerScript))).enabled = false;
            }
        }
    }

    // Token: 0x06000542 RID: 1346 RVA: 0x00090D38 File Offset: 0x0008EF38
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3((float)-8, (float)-10, (float)0), new Vector3((float)-8, (float)5, (float)0));
        Gizmos.DrawLine(new Vector3((float)8, (float)-10, (float)0), new Vector3((float)8, (float)5, (float)0));
        Gizmos.DrawLine(new Vector3((float)-8, (float)0, (float)0), new Vector3((float)8, (float)0, (float)0));
        int i = 0;
        if (Extensions.get_length(this.spawnPos) > 0)
        {
            for (i = 0; i < Extensions.get_length(this.spawnPos); i++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(new Vector3(this.spawnPos[i].x, this.spawnPos[i].y, (float)0), new Vector3(this.spawnPos[i].x, this.targetPos[i], (float)0));
                Gizmos.color = Color.green;
                Gizmos.DrawLine(new Vector3(this.spawnPos[i].x, this.targetPos[i] - this.moveAmount[i], -0.1f), new Vector3(this.spawnPos[i].x, this.targetPos[i] + this.moveAmount[i], -0.1f));
                Gizmos.DrawLine(new Vector3(this.spawnPos[i].x, this.targetPos[i] + Mathf.Sin(Time.realtimeSinceStartup * this.moveSpeed[i]) * this.moveAmount[i], -0.1f), new Vector3(this.spawnPos[i].x + 0.5f, this.targetPos[i] + Mathf.Sin(Time.realtimeSinceStartup * this.moveSpeed[i]) * this.moveAmount[i], -0.1f));
            }
        }
        if (Extensions.get_length(this.objTypeAndSizeAndStartPos) > 0)
        {
            for (i = 0; i < Extensions.get_length(this.objTypeAndSizeAndStartPos); i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(new Vector3(this.objTypeAndSizeAndStartPos[i].z, this.objTypeAndSizeAndStartPos[i].w, (float)0), new Vector3(this.objTypeAndSizeAndStartPos[i].z, this.targetPosAndMoveAmountAndSpeed[i].x, (float)0));
                Gizmos.color = Color.green;
                Gizmos.DrawLine(new Vector3(this.objTypeAndSizeAndStartPos[i].z, this.targetPosAndMoveAmountAndSpeed[i].x - this.targetPosAndMoveAmountAndSpeed[i].y, -0.1f), new Vector3(this.objTypeAndSizeAndStartPos[i].z, this.targetPosAndMoveAmountAndSpeed[i].x + this.targetPosAndMoveAmountAndSpeed[i].y, -0.1f));
                Gizmos.DrawLine(new Vector3(this.objTypeAndSizeAndStartPos[i].z, this.targetPosAndMoveAmountAndSpeed[i].x + Mathf.Sin(Time.realtimeSinceStartup * this.targetPosAndMoveAmountAndSpeed[i].z) * this.targetPosAndMoveAmountAndSpeed[i].y, -0.1f), new Vector3(this.objTypeAndSizeAndStartPos[i].z + 0.5f, this.targetPosAndMoveAmountAndSpeed[i].x + Mathf.Sin(Time.realtimeSinceStartup * this.targetPosAndMoveAmountAndSpeed[i].z) * this.targetPosAndMoveAmountAndSpeed[i].y, -0.1f));
            }
        }
    }
}
