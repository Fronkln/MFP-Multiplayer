using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
[Serializable]
public class BarrelScript : MonoBehaviour
{
    private bool playerIsOnBarrel = false;

    private BaseNetworkEntity networkHelper;

    // Token: 0x0400011E RID: 286
    private RootScript root;

    // Token: 0x0400011F RID: 287
    private StatsTrackerScript statsTracker;

    // Token: 0x04000120 RID: 288
    private Transform mainPlayer;

    // Token: 0x04000121 RID: 289
    private PlayerScript playerScript;

    // Token: 0x04000122 RID: 290
    private CameraScript cameraScript;

    // Token: 0x04000123 RID: 291
    private Rigidbody rBody;

    // Token: 0x04000124 RID: 292
    private bool attractPlayer;

    // Token: 0x04000125 RID: 293
    private Transform innerBoxCollider;

    // Token: 0x04000126 RID: 294
    private Quaternion innerBoxColliderStartRot;

    // Token: 0x04000127 RID: 295
    private bool onGround;

    // Token: 0x04000128 RID: 296
    private LayerMask layerMask;

    // Token: 0x04000129 RID: 297
    private Vector3 prevPos;

    // Token: 0x0400012A RID: 298
    private float extraGravityCyclesCounter;

    // Token: 0x0400012B RID: 299
    private bool playerPushing;

    // Token: 0x0400012C RID: 300
    public bool dontShine;

    // Token: 0x0400012D RID: 301
    private GameObject shine;

    // Token: 0x0400012E RID: 302
    private bool shineDoOnce;

    // Token: 0x0400012F RID: 303
    private bool playerPushedAgainst;

    // Token: 0x04000130 RID: 304
    private bool playerOnTop;

    // Token: 0x04000131 RID: 305
    private bool attractPlayerS;

    // Token: 0x04000132 RID: 306
    private bool onGroundS;

    // Token: 0x04000133 RID: 307
    private Vector3 prevPosS;

    // Token: 0x04000134 RID: 308
    private float extraGravityCyclesCounterS;

    // Token: 0x04000135 RID: 309
    private bool playerPushingS;

    // Token: 0x04000136 RID: 310
    private bool dontShineS;

    // Token: 0x04000137 RID: 311
    private bool shineDoOnceS;

    // Token: 0x04000138 RID: 312
    private bool playerPushedAgainstS;

    // Token: 0x04000139 RID: 313
    private bool playerOnTopS;

    // Token: 0x06000058 RID: 88 RVA: 0x000020A9 File Offset: 0x000002A9
    public BarrelScript()
    {
    }

    // Token: 0x06000059 RID: 89 RVA: 0x00009F88 File Offset: 0x00008188
    public virtual void saveState()
    {
        this.attractPlayerS = this.attractPlayer;
        this.onGroundS = this.onGround;
        this.prevPosS = this.prevPos;
        this.extraGravityCyclesCounterS = this.extraGravityCyclesCounter;
        this.playerPushingS = this.playerPushing;
        this.dontShineS = this.dontShine;
        this.shineDoOnceS = this.shineDoOnce;
        this.playerPushedAgainstS = this.playerPushedAgainst;
        this.playerOnTopS = this.playerOnTop;
    }

    // Token: 0x0600005A RID: 90 RVA: 0x0000A004 File Offset: 0x00008204
    public virtual void loadState()
    {
        this.attractPlayer = this.attractPlayerS;
        this.onGround = this.onGroundS;
        this.prevPos = this.prevPosS;
        this.extraGravityCyclesCounter = this.extraGravityCyclesCounterS;
        this.playerPushing = this.playerPushingS;
        this.dontShine = this.dontShineS;
        this.shineDoOnce = this.shineDoOnceS;
        this.playerPushedAgainst = this.playerPushedAgainstS;
        this.playerOnTop = this.playerOnTopS;
    }

    // Token: 0x0600005B RID: 91 RVA: 0x000022EF File Offset: 0x000004EF
    public virtual void LateUpdate()
    {
        if (this.root.doCheckpointSave)
        {
            this.saveState();
        }
        if (this.root.doCheckpointLoad)
        {
            this.loadState();
        }
    }


    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<NetworkedBaseRigidbody>();
    }

    // Token: 0x0600005C RID: 92 RVA: 0x0000A080 File Offset: 0x00008280
    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.statsTracker = (StatsTrackerScript)GameObject.Find("RootShared").GetComponent(typeof(StatsTrackerScript));
        this.mainPlayer = GameObject.Find("Player").transform;
        this.playerScript = (PlayerScript)this.mainPlayer.GetComponent(typeof(PlayerScript));
        this.cameraScript = (CameraScript)GameObject.Find("Main Camera").GetComponent(typeof(CameraScript));
        this.rBody = (Rigidbody)this.GetComponent(typeof(Rigidbody));
        this.innerBoxCollider = this.transform.Find("BoxCollider");
        this.innerBoxColliderStartRot = this.innerBoxCollider.rotation;
        this.layerMask = 33652992;
        this.prevPos = this.transform.position;
        this.shine = this.transform.Find("Barrel_Shine").gameObject;
        if (this.dontShine)
        {
            this.shine.SetActive(false);
        }
    }

    // Token: 0x0600005D RID: 93 RVA: 0x0000A1C4 File Offset: 0x000083C4
    public virtual void Update()
    {
        this.onGround = Physics.Raycast(this.transform.position + Vector3.down * (this.transform.localScale.y / (float)3), Vector3.down, this.transform.localScale.y, this.layerMask);
        this.playerPushedAgainst = ((this.playerScript.wallTouchLeftTransform == this.transform) || (this.playerScript.wallTouchRightTransform == this.transform));
        if (!this.dontShine)
        {
            if (this.rBody.velocity.magnitude < (float)1)
            {
                if (!this.shineDoOnce)
                {
                    this.shine.SetActive(true);
                    this.shineDoOnce = true;
                }
            }
            else if (this.shineDoOnce)
            {
                this.shine.SetActive(false);
                this.shineDoOnce = false;
            }
        }
        this.playerOnTop = (this.playerScript.groundTransform == this.transform);

        if (networkHelper.interactingPlayer != MultiplayerManagerTest.inst.playerID)
        {
            networkHelper.ignoreMaxPacketsDoOnce = true;
            PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });
        }



        if (this.playerOnTop)
        {
            playerIsOnBarrel = true;
            this.root.autoSaveTimer = (float)0;
            float num = Mathf.Clamp01(Mathf.Abs(this.rBody.velocity.x * 0.1f));
            if (num > 0.5f)
            {
                this.root.rumble((this.rBody.velocity.x <= (float)0) ? 1 : 0, (num - 0.5f) * 0.03f, 0.03f);
            }
            if (this.playerScript.kJump && Mathf.Abs(this.rBody.velocity.x) > 7.5f)
            {
                this.playerScript.xSpeed = this.rBody.velocity.x * 0.9f;
                this.playerScript.justWallJumped = true;
                this.playerScript.justJumpedFromSkateboard = true;
                this.playerScript.wallJumpTimer = (float)60;
            }
            this.cameraScript.invertGamepadMouseOffset = true;
            this.cameraScript.externalCameraOffset.x = this.rBody.velocity.x * 0.5f;
            this.attractPlayer = true;
        }
        else
        {
            if (this.playerScript.onGround && this.playerScript.crouchAmount < 0.3f)
            {
                if (Vector2.Distance(this.mainPlayer.position, this.transform.position) < ((!this.playerPushing) ? ((float)2) : (2.5f + 0.5f * Mathf.Clamp01(Mathf.Abs(this.rBody.velocity.x / (float)3)))) && Mathf.Abs(this.transform.position.y - this.mainPlayer.position.y) < 2.5f)
                {
                    this.playerPushing = true;
                }
                else
                {
                    this.playerPushing = false;
                }
            }
            else
            {
                this.playerPushing = false;
            }
            this.attractPlayer = false;
        }


        if (!playerOnTop && playerIsOnBarrel)
        {
            networkHelper.ignoreMaxPacketsDoOnce = true;
            PacketSender.BaseNetworkedEntityRPC("OnPlayerStopInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });
            playerIsOnBarrel = false;
        }



        this.innerBoxCollider.rotation = this.innerBoxColliderStartRot;
        if (this.attractPlayer)
        {
            float x = this.root.Damp(this.transform.position.x, this.mainPlayer.position.x, 0.85f * Mathf.Clamp01(Mathf.Abs(this.rBody.velocity.x) / (float)5));
            Vector3 position = this.mainPlayer.position;
            float num2 = position.x = x;
            Vector3 vector = this.mainPlayer.position = position;
            float y = this.mainPlayer.position.y + (this.transform.position.y - this.prevPos.y);
            Vector3 position2 = this.mainPlayer.position;
            float num3 = position2.y = y;
            Vector3 vector2 = this.mainPlayer.position = position2;
        }
        this.prevPos = this.transform.position;
    }

    // Token: 0x0600005E RID: 94 RVA: 0x0000A640 File Offset: 0x00008840
    public virtual void FixedUpdate()
    {
        float fixedTimescale = this.root.fixedTimescale;
        if (this.playerPushedAgainst)
        {
            if (Mathf.Abs(this.rBody.velocity.x) < (float)1)
            {
                int num = 0;
                Vector3 velocity = this.rBody.velocity;
                float num2 = velocity.x = (float)num;
                Vector3 vector = this.rBody.velocity = velocity;
            }
            else
            {
                float x = this.root.DampFixed(this.playerScript.xSpeed / (float)10, this.rBody.velocity.x, 0.4f);
                Vector3 velocity2 = this.rBody.velocity;
                float num3 = velocity2.x = x;
                Vector3 vector2 = this.rBody.velocity = velocity2;
            }
        }
        this.playerOnTop = (this.playerScript.groundTransform == this.transform);

        if (this.playerOnTop)
        {
            if (Mathf.Abs(this.playerScript.xSpeed) > (float)2)
            {
                float x2 = Mathf.Clamp(this.rBody.velocity.x - this.playerScript.xSpeed / (float)9 * ((!this.onGround) ? 0.5f : ((float)1)) * Mathf.Clamp(Mathf.Abs(this.rBody.angularVelocity.z) / 1.5f, 0.75f, (float)1), (float)-11, (float)11);
                Vector3 velocity3 = this.rBody.velocity;
                float num4 = velocity3.x = x2;
                Vector3 vector3 = this.rBody.velocity = velocity3;
                this.playerScript.xSpeed = this.playerScript.xSpeed * Mathf.Pow((float)1 - 0.5f * ((float)1 - Mathf.Clamp01(Mathf.Abs(this.rBody.angularVelocity.z) / (float)5)), this.root.fixedTimescale);
            }
            else if (this.onGround)
            {
                float x3 = this.rBody.velocity.x * Mathf.Pow((float)1 - 0.1f * Mathf.Clamp01(Mathf.Abs(this.rBody.velocity.x) / (float)3), this.root.fixedTimescale);
                Vector3 velocity4 = this.rBody.velocity;
                float num5 = velocity4.x = x3;
                Vector3 vector4 = this.rBody.velocity = velocity4;
            }
        }
        else if (this.playerPushing)
        {
            if ((this.mainPlayer.position.x < this.transform.position.x && this.playerScript.targetXSpeed > (float)0) || (this.mainPlayer.position.x > this.transform.position.x && this.playerScript.targetXSpeed < (float)0))
            {
                float x4 = this.root.DampFixed(this.playerScript.targetXSpeed * ((Mathf.Abs(this.mainPlayer.position.x - this.transform.position.x) >= 1.8f) ? 0.1f : 0.6f), this.rBody.velocity.x, (!this.playerPushedAgainst) ? 0.25f : 0.5f);
                Vector3 velocity5 = this.rBody.velocity;
                float num6 = velocity5.x = x4;
                Vector3 vector5 = this.rBody.velocity = velocity5;
                if (!this.playerPushedAgainst)
                {
                    this.playerScript.xSpeed = this.playerScript.targetXSpeed * 0.3f;
                }
            }
            else
            {
                float x5 = this.rBody.velocity.x * Mathf.Pow(0.95f, this.root.fixedTimescale);
                Vector3 velocity6 = this.rBody.velocity;
                float num7 = velocity6.x = x5;
                Vector3 vector6 = this.rBody.velocity = velocity6;
            }
        }
        if (this.attractPlayer)
        {
            float x6 = this.root.DampFixed(this.transform.position.x, this.mainPlayer.position.x, 0.85f * Mathf.Clamp01(Mathf.Abs(this.rBody.velocity.x) / (float)5));
            Vector3 position = this.mainPlayer.position;
            float num8 = position.x = x6;
            Vector3 vector7 = this.mainPlayer.position = position;
            float y = this.mainPlayer.position.y + (this.transform.position.y - this.prevPos.y);
            Vector3 position2 = this.mainPlayer.position;
            float num9 = position2.y = y;
            Vector3 vector8 = this.mainPlayer.position = position2;
        }
        if (this.rBody.velocity.sqrMagnitude > 0.1f)
        {
            this.extraGravityCyclesCounter = (float)10;
        }
        if (this.extraGravityCyclesCounter > (float)0)
        {
            this.extraGravityCyclesCounter -= (float)1;
            this.rBody.AddForce((float)150 * Physics.gravity);
        }
        this.innerBoxCollider.rotation = this.innerBoxColliderStartRot;
        this.prevPos = this.transform.position;
    }

    // Token: 0x0600005F RID: 95 RVA: 0x0000AC50 File Offset: 0x00008E50
    public virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 13)
        {
            EnemyScript enemyScript = (EnemyScript)col.collider.GetComponent(typeof(EnemyScript));
            if (enemyScript != null)
            {
                if (col.relativeVelocity.magnitude > 4.5f)
                {
                    enemyScript.bulletHit = true;
                    enemyScript.bulletStrength += (float)3;
                    enemyScript.bulletHitName = col.gameObject.name;
                    enemyScript.bulletHitPos = this.transform.position;
                    enemyScript.bulletHitRot = this.transform.rotation;
                    enemyScript.bulletHitVel = this.rBody.velocity;
                    enemyScript.allowGib = false;
                    enemyScript.bulletTimeAlive = (float)999;
                    enemyScript.bulletKillText = this.root.GetTranslation("bul24");
                    enemyScript.bulletHitText = this.root.GetTranslation("bul25") + "-";
                    enemyScript.bulletHitExtraScore = (float)200;
                    if (this.playerOnTop)
                    {
                        this.root.rumble((this.rBody.velocity.x <= (float)0) ? 0 : 1, 0.9f, 0.25f);
                    }
                    this.statsTracker.barrelKills = this.statsTracker.barrelKills + 1;
                    this.statsTracker.achievementCheck();
                }
                else
                {
                    float x = this.rBody.velocity.x * Mathf.Pow(0.8f, this.root.fixedTimescale);
                    Vector3 velocity = this.rBody.velocity;
                    float num = velocity.x = x;
                    Vector3 vector = this.rBody.velocity = velocity;
                }
            }
        }
        if (!this.attractPlayer && this.onGround && col.gameObject.layer == 9)
        {
            BulletScript bulletScript = (BulletScript)col.gameObject.GetComponent(typeof(BulletScript));
            float x2 = this.root.DampFixed((col.gameObject.transform.forward.x <= (float)0) ? -4.5f : 4.5f, this.rBody.velocity.x, 0.3f + bulletScript.bulletStrength / 1.25f);
            Vector3 velocity2 = this.rBody.velocity;
            float num2 = velocity2.x = x2;
            Vector3 vector2 = this.rBody.velocity = velocity2;
        }
    }
}
