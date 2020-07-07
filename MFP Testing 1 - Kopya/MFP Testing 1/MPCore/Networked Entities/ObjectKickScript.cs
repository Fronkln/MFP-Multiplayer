using System;
using UnityEngine;

// Token: 0x02000081 RID: 129
[Serializable]
public class ObjectKickScript : MonoBehaviour
{

    public NetworkedBaseRigidbody networkHelper;

    // Token: 0x06000323 RID: 803 RVA: 0x000038A8 File Offset: 0x00001AA8
    public ObjectKickScript()
    {
        this.kickRange = 1.85f;
        this.reactOnEnemyBulletHit = true;
        this.bulletHitReactMultiplier = (float)1;
        this.bounceOnEnemyHit = true;
    }

    // Token: 0x06000324 RID: 804 RVA: 0x000467F8 File Offset: 0x000449F8

    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<NetworkedBaseRigidbody>();
        networkHelper.dontDoDebug = true;
    }


    // Token: 0x06000327 RID: 807 RVA: 0x00046980 File Offset: 0x00044B80
    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.statsTracker = (StatsTrackerScript)GameObject.Find("RootShared").GetComponent(typeof(StatsTrackerScript));
        this.thisObjectKickScript = (ObjectKickScript)this.GetComponent(typeof(ObjectKickScript));
        this.startPos = this.transform.position;
        this.rBody = (Rigidbody)this.gameObject.GetComponent(typeof(Rigidbody));
        this.mainPlayer = GameObject.Find("Player").transform;
        this.playerLFoot = this.mainPlayer.Find("PlayerGraphics/Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L/Foot_L");
        this.playerScript = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
        if (this.playerScript.onMotorcycle)
            ((ObjectKickScript)this.GetComponent(typeof(ObjectKickScript))).enabled = false;

        this.physicsSoundsScript = (PhysicsSoundsScript)this.GetComponent(typeof(PhysicsSoundsScript));
        this.audioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.layerMask = 33652992;
    }

    // Token: 0x06000328 RID: 808 RVA: 0x00046AE4 File Offset: 0x00044CE4
    public virtual void Update()
    {
        this.playerDistMag = (new Vector3(this.transform.position.x, this.transform.position.y, (float)0) - new Vector3(this.mainPlayer.position.x, this.mainPlayer.position.y, (float)0)).magnitude;
        if (this.playerDistMag < this.kickRange)
        {
            this.playerScript.allowKickItem(this.rBody, this.extraGravity, this.kickRange, this.centerOffset, this.physicsSoundsScript, this.thisObjectKickScript);
        }
        this.knockBackCoolDown = Mathf.Clamp(this.knockBackCoolDown - this.root.timescale, (float)0, (float)60);
    }

    // Token: 0x06000329 RID: 809 RVA: 0x00046BC4 File Offset: 0x00044DC4
    public virtual void FixedUpdate()
    {
        float fixedTimescale = this.root.fixedTimescale;
        if (this.zPosOffsetTimer > (float)0)
        {
            float z = this.root.DampFixed(this.startPos.z + (float)2, this.transform.position.z, 0.1f);
            Vector3 position = this.transform.position;
            float num = position.z = z;
            Vector3 vector = this.transform.position = position;
            this.zPosOffsetTimer -= fixedTimescale;
        }
        else
        {
            float z2 = this.root.DampFixed(this.startPos.z, this.transform.position.z, 0.1f);
            Vector3 position2 = this.transform.position;
            float num2 = position2.z = z2;
            Vector3 vector2 = this.transform.position = position2;
        }
        if (this.autoTargetTransform == null)
        {
            this.autoTargetTransformTimer = Mathf.Clamp(this.autoTargetTransformTimer - fixedTimescale, (float)0, (float)60);
            if (this.extraGravity != (float)0)
            {
                if (this.rBody.velocity.sqrMagnitude > 0.1f)
                {
                    this.extraGravityCyclesCounter = 10;
                }
                if (this.extraGravityCyclesCounter > 0)
                {
                    this.extraGravityCyclesCounter--;
                    this.rBody.AddForce(this.extraGravity * Physics.gravity);
                }
            }
            if (this.followPlayer)
            {
                float num3 = this.mainPlayer.position.x - this.transform.position.x + Mathf.Clamp(this.playerScript.xSpeed, -0.8f, 0.8f) + ((!this.playerScript.faceRight) ? -0.2f : 0.2f);
                float f = this.playerLFoot.position.y - this.transform.position.y;
                if (Mathf.Abs(this.rBody.velocity.y) < 0.3f && Mathf.Abs(num3) < 1.5f && Mathf.Abs(f) < (float)1)
                {
                    float x = this.rBody.velocity.x + num3 * 0.5f * Mathf.Clamp01(Mathf.Abs(this.playerScript.xSpeed / (float)3)) * fixedTimescale;
                    Vector3 velocity = this.rBody.velocity;
                    float num4 = velocity.x = x;
                    Vector3 vector3 = this.rBody.velocity = velocity;
                    if (Mathf.Abs(num3) < 0.9f)
                    {
                        float x2 = this.rBody.velocity.x * Mathf.Pow(0.7f, fixedTimescale);
                        Vector3 velocity2 = this.rBody.velocity;
                        float num5 = velocity2.x = x2;
                        Vector3 vector4 = this.rBody.velocity = velocity2;
                    }
                }
            }
        }
        else
        {
            this.autoTargetTransformTimer = (float)30;
            Vector3 vector5 = this.autoTargetTransform.position - this.transform.position;
            this.rBody.velocity = vector5.normalized * (float)25;
            if (Mathf.Abs(vector5.magnitude) < (float)1 || (this.autoTargetEnemyScript != null && (!this.autoTargetEnemyScript.enabled || this.autoTargetEnemyScript.health <= (float)0)))
            {
                this.autoTargetTransform = null;
            }
        }
    }

    // Token: 0x0600032A RID: 810 RVA: 0x00046F98 File Offset: 0x00045198
    public virtual void OnCollisionEnter(Collision col)
    {
        if (this.rBody != null)
        {
            this.kickJuggleAmount = 0;
            if (this.autoTargetTransformTimer <= (float)25)
            {
                this.autoTargetTransform = null;
            }
            if (this.reactOnBulletHit && col.gameObject.layer == 9)
            {
                bool flag = default(bool);
                if (this.reactOnEnemyBulletHit)
                {
                    flag = true;
                }
                else if (((BulletScript)col.gameObject.GetComponent(typeof(BulletScript))).friendly)
                {
                    flag = true;
                }
                if (flag)
                {
                    this.rBody.isKinematic = false;
                    this.rBody.velocity = this.rBody.velocity * 0.2f + col.rigidbody.velocity.normalized * (float)2 + new Vector3((float)0, (float)5 * this.bulletHitReactMultiplier, (float)0);
                    this.rBody.angularVelocity = this.rBody.velocity * (float)2;
                }
            }
        }
    }

    // Token: 0x0600032B RID: 811 RVA: 0x000038FF File Offset: 0x00001AFF
    public virtual void OnTriggerStay(Collider col)
    {
        if (col.tag == "Z Pusher")
        {
            this.zPosOffsetTimer = (float)3;
        }
    }

    // Token: 0x0600032C RID: 812 RVA: 0x000470B4 File Offset: 0x000452B4
    public virtual void OnTriggerEnter(Collider col)
    {
        if (this.rBody != null && !this.dontHurtEnemyOnCollision)
        {
            EnemyScript enemyScript = null;
            float magnitude = this.rBody.velocity.magnitude;
            if (magnitude > (float)11 && this.rBody.velocity.normalized.y <= 0.8f && (col.transform.name == "Head" || this.autoTargetTransformTimer > (float)0) && col.gameObject.layer == 10)
            {
                if (this.bounceOnEnemyHit)
                {
                    this.rBody.velocity = new Vector3(Mathf.Clamp(this.mainPlayer.position.x - this.transform.position.x, (float)-2, (float)2), (float)10, (float)0);
                }
                if (this.objType == 1)
                {
                    this.statsTracker.fryingPanHeadKick = this.statsTracker.fryingPanHeadKick + 1;
                    this.statsTracker.achievementCheck();
                }
                else if (this.objType == 2)
                {
                    this.statsTracker.skateboardHeadKick = this.statsTracker.skateboardHeadKick + 1;
                    this.statsTracker.achievementCheck();
                }
                else if (this.objType == 3)
                {
                    this.statsTracker.gasCanisterHeadKick = this.statsTracker.gasCanisterHeadKick + 1;
                    this.statsTracker.achievementCheck();
                }
                enemyScript = (EnemyScript)col.transform.GetComponentInParent(typeof(EnemyScript));
                enemyScript.bulletHit = true;
                enemyScript.bulletStrength = magnitude;
                enemyScript.bulletHitName = "Head";
                enemyScript.bulletHitPos = this.transform.position;
                enemyScript.bulletHitRot = this.transform.rotation;
                enemyScript.bulletHitVel = this.rBody.velocity / 1.5f / (float)4;
                enemyScript.allowGib = false;
                enemyScript.bulletKillText = this.root.GetTranslation("bul26");
                enemyScript.bulletHitText = this.root.GetTranslation("bul27") + "-";
                enemyScript.bulletHitExtraScore = (float)200;
                this.autoTargetTransformTimer = (float)0;
                this.autoTargetTransform = null;
                if (this.physicsSoundsScript != null)
                {
                    this.physicsSoundsScript.triggerCollisionSound((float)5, true, (float)1);
                }
            }
            if (col.gameObject.tag == "Enemy")
            {
                if (enemyScript == null)
                {
                    enemyScript = (EnemyScript)col.transform.GetComponentInParent(typeof(EnemyScript));
                }
                if (this.knockBackCoolDown <= (float)0 && (magnitude > (float)3 || this.autoTargetTransformTimer > (float)0))
                {
                    enemyScript.knockBack(this.rBody.velocity.x > (float)0, (float)30);
                    this.knockBackCoolDown = (float)30;
                }
                enemyScript.idle = false;
                if (enemyScript.enemyType == 3)
                {
                    enemyScript.engageAnimFinished = true;
                }
                if (this.bounceOnEnemyHit && !enemyScript.bulletHit)
                {
                    float x = (this.transform.position.x >= this.rBody.transform.position.x) ? Mathf.Abs(this.rBody.velocity.x * 0.6f) : (-Mathf.Abs(this.rBody.velocity.x * 0.6f));
                    Vector3 velocity = this.rBody.velocity;
                    float num = velocity.x = x;
                    Vector3 vector = this.rBody.velocity = velocity;
                    if (this.physicsSoundsScript != null)
                    {
                        this.physicsSoundsScript.triggerCollisionSound(magnitude, false, (float)0);
                    }
                }
            }
        }
    }

    // Token: 0x0600032D RID: 813 RVA: 0x000020A7 File Offset: 0x000002A7
    public virtual void Main()
    {
    }

    // Token: 0x0400096E RID: 2414
    private RootScript root;

    // Token: 0x0400096F RID: 2415
    private StatsTrackerScript statsTracker;

    // Token: 0x04000970 RID: 2416
    private Vector3 startPos;

    // Token: 0x04000971 RID: 2417
    private Rigidbody rBody;

    // Token: 0x04000972 RID: 2418
    private Transform mainPlayer;

    // Token: 0x04000973 RID: 2419
    private Transform playerLFoot;

    // Token: 0x04000974 RID: 2420
    private PlayerScript playerScript;

    // Token: 0x04000975 RID: 2421
    private int extraGravityCyclesCounter;

    // Token: 0x04000976 RID: 2422
    public float kickRange;

    // Token: 0x04000977 RID: 2423
    public float extraGravity;

    // Token: 0x04000978 RID: 2424
    public Vector3 centerOffset;

    // Token: 0x04000979 RID: 2425
    public bool reactOnBulletHit;

    // Token: 0x0400097A RID: 2426
    public bool reactOnEnemyBulletHit;

    // Token: 0x0400097B RID: 2427
    public float bulletHitReactMultiplier;

    // Token: 0x0400097C RID: 2428
    public bool followPlayer;

    // Token: 0x0400097D RID: 2429
    public bool bounceOnEnemyHit;

    // Token: 0x0400097E RID: 2430
    [HideInInspector]
    public Transform autoTargetTransform;

    // Token: 0x0400097F RID: 2431
    private float autoTargetTransformTimer;

    // Token: 0x04000980 RID: 2432
    [HideInInspector]
    public EnemyScript autoTargetEnemyScript;

    // Token: 0x04000981 RID: 2433
    private float knockBackCoolDown;

    // Token: 0x04000982 RID: 2434
    public bool dontHurtEnemyOnCollision;

    // Token: 0x04000983 RID: 2435
    private float zPosOffsetTimer;

    // Token: 0x04000984 RID: 2436
    private PhysicsSoundsScript physicsSoundsScript;

    // Token: 0x04000985 RID: 2437
    private AudioSource audioSource;

    // Token: 0x04000986 RID: 2438
    public bool pushAwayFromCeiling;

    // Token: 0x04000987 RID: 2439
    [Header("For stats tracking - 1 = Frying pan, 2 = Skateboard, 3 = Gas Canister, 4 = Basketball")]
    public int objType;

    // Token: 0x04000988 RID: 2440
    [HideInInspector]
    public int kickJuggleAmount;

    // Token: 0x04000989 RID: 2441
    private ObjectKickScript thisObjectKickScript;

    // Token: 0x0400098A RID: 2442
    private LayerMask layerMask;

    // Token: 0x0400098B RID: 2443
    private float playerDistMag;

    // Token: 0x0400098C RID: 2444
    private int extraGravityCyclesCounterS;

    // Token: 0x0400098D RID: 2445
    private float kickRangeS;

    // Token: 0x0400098E RID: 2446
    private float extraGravityS;

    // Token: 0x0400098F RID: 2447
    private Vector3 centerOffsetS;

    // Token: 0x04000990 RID: 2448
    private bool reactOnBulletHitS;

    // Token: 0x04000991 RID: 2449
    private bool reactOnEnemyBulletHitS;

    // Token: 0x04000992 RID: 2450
    private float bulletHitReactMultiplierS;

    // Token: 0x04000993 RID: 2451
    private bool followPlayerS;

    // Token: 0x04000994 RID: 2452
    private bool bounceOnEnemyHitS;

    // Token: 0x04000995 RID: 2453
    private Transform autoTargetTransformS;

    // Token: 0x04000996 RID: 2454
    private float autoTargetTransformTimerS;

    // Token: 0x04000997 RID: 2455
    private EnemyScript autoTargetEnemyScriptS;

    // Token: 0x04000998 RID: 2456
    private float knockBackCoolDownS;

    // Token: 0x04000999 RID: 2457
    private bool dontHurtEnemyOnCollisionS;

    // Token: 0x0400099A RID: 2458
    private float zPosOffsetTimerS;
}
