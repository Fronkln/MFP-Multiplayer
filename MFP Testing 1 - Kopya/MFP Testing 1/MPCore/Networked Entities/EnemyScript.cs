// Decompiled with JetBrains decompiler
// Type: EnemyScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using Boo.Lang.Runtime;
using System.Collections;
using UnityEngine;
using UnityScript.Lang;

public class EnemyScript : MonoBehaviour
{
    private NetworkedEnemyScriptAttachment networkHelper;
    public NetworkedBaseTransform specialEnemySyncer;

    public bool wasActivatedByPlayers = false;

    [Header("-------------- Optimizations ---------------")]
    public bool simpleEnemy;
    private GameObject simpleHandR;
    private GameObject simpleHandL;
    private GameObject simpleCollision;
    private CapsuleCollider sCol;
    public bool mainBodyKinematic;
    private Vector3 fakeVelocity;
    public bool alwaysOnFlatGround;
    private float alwaysOnFlatGroundYRef;
    public bool dontDoLegBendRayCast;
    public bool dontDoArmBendRayCast;
    public bool dontWallCheckLeft;
    public bool dontWallCheckRight;
    [Header("-- Other optimizations")]
    public bool dontCheckDistanceBeforeRunningLogic;
    public bool runLogic;
    private bool runLogicDoOnce;
    private float playerDistance;
    private EnemyOptimizerScript enemyOptimizerScript;
    public float fallKillThreshold;
    public bool dontCheckGroundBeforeFacingPlayer;
    [Header("--------------------------------------------")]
    private Vector2 gamepadAimOffset;
    private Vector2 targetGamepadAimOffset;
    private Vector2 targetGamepadAimSpeed;
    private Vector2 rStick;
    private Vector3 gamepadAimingReferencePoint;
    private float damageMultiplier;
    private RectTransform aimHelper;
    private float aimHelperDistance;
    [HideInInspector]
    private float kCycleInput;
    private bool kCycleInputDoOnce;
    private string inputPText;
    private bool gamepad;
    private ParticleSystem pistolClipParticle;
    public float extraAutoAimYPosOffset;
    public bool skyfall;
    public float skyfallYPos;
    public float skyfallYMoveAmount;
    public float skyfallYMoveSpeed;
    public AnimatorOverrideController skyfallAnimController;
    public Transform motorcycle;
    private float motorcycleAim;
    private Transform motorcycleTransform;
    public Transform stickToTransform;
    private Transform useHat;
    private bool reloading;
    private float reloadLayerWeight;
    private float ammo;
    public bool dontGiveScore;
    public bool rappelling;
    public Transform rappellTransform;
    public bool unhingeFromRappell;
    public bool unhingeOnDeath;
    private StringScript rappellStringScript;
    public float rappellRopeLength;
    private float rapellTimer;
    [Header("Audio")]
    public AudioClip voice;
    public float voicePitchOffset;
    public AudioClip[] weaponSound;
    private AudioSource handRAudio;
    private AudioSource handLAudio;
    private AudioSource headAudio;
    public AudioClip[] deathScreams;
    private AudioSource generalAudioSource;
    public AudioClip punchSwooshSound;
    public AudioClip punchHitSound;
    [HideInInspector]
    public AudioSource deathAudioSource;
    private RootScript root;
    private RootSharedScript rootShared;
    private StatsTrackerScript statsTracker;
    public float targetXSpeed;
    private float xSpeed;
    private float ySpeed;
    private bool onGround;
    private bool okToStand;
    private bool wallTouchRight;
    private bool wallTouchLeft;
    private bool justJumped;
    private float legLength;
    private float standLegLength;
    private float crouchLegLength;
    private float raycastWidthRange;
    private bool kJump;
    private bool kCrouch;
    private bool kFire;
    private float timescale;
    private float timescaleRaw;
    private Animator animator;
    private Transform playerGraphics;
    [HideInInspector]
    public Vector3 mousePos;
    private Vector3 mousePosWithZOffset;
    private Camera curCamera;
    private CameraScript cameraScript;
    private Transform lowerBack;
    private Transform upperBack;
    private Transform neck;
    private Transform head;
    private Transform shoulderR;
    private Transform upperArmR;
    private Transform lowerArmR;
    private Transform handR;
    private Transform shoulderL;
    private Transform upperArmL;
    private Transform lowerArmL;
    private Transform handL;
    private Transform bulletPointR;
    private Transform bulletPointL;
    private Transform footR;
    private Transform footTipR;
    private Transform upperLegR;
    private Transform lowerLegR;
    private Transform footL;
    private Transform footTipL;
    private Transform upperLegL;
    private Transform lowerLegL;
    private Transform hipL;
    private Transform hipR;
    private Transform pistolR;
    private Transform pistolL;
    private Transform uziR;
    private Transform uziL;
    private Transform machineGun;
    private Transform shotgun;
    private Transform sniper;
    private Transform rifleLaser;
    private Transform crossbow;
    private Transform crossbowPipe;
    private Transform crossbowBow1;
    private Transform crossbowBow2;
    private GameObject[] crossbowArrows;
    private Transform club;
    private Transform center;
    private GameObject bulletPointRAimTarget;
    private GameObject bulletPointLAimTarget;
    private Quaternion lowerBackFakeRot;
    private Quaternion headFakeRot;
    private Quaternion shoulderRFakeRot;
    private Quaternion shoulderLFakeRot;
    private float aimBlend;
    private float aimBlendTarget;
    private float lookAimBlend;
    private float lookAimBlendTarget;
    private float fightPoseBlend;
    private float fightPoseBlendTimer;
    private bool aimWithLeftArm;
    [HideInInspector]
    public float fireDelay;
    private bool fireLeftGun;
    private float punchAnimNr;
    private float punchTimer;
    private float justJumpedAnimationBoolTimer;
    public float crouchAmount;
    private ParticleSystem shellParticle;
    private ParticleSystem smokeParticle;
    private bool fightMode;
    private bool pushedAgainstWall;
    private float upperArmAnimationBlend;
    private float upperArmAnimationBlendTimer;
    private bool fireWeapon;
    private float visualCrouchAmount;
    private float prevVisualCrouchAmount;
    private LayerMask layerMask;
    private LayerMask layerMaskSpotPlayer;
    private LayerMask layerMaskOnlyPlayer;
    private LayerMask layerMaskOnlyLevelCollision;
    private LayerMask layerMaskIncPlayerAndPlayerGameCollisionWithoutBulletPassthrough;
    private Transform mainPlayer;
    private Rigidbody rBody;
    private GameObject doorSpawnBulletCheck;
    [Header("------------------- Prefabs --------------------")]
    public GameObject bullet;
    public GameObject medkit;
    public GameObject crossbowArrow;
    public GameObject muzzleFlash;
    public GameObject gibUpperArm;
    public GameObject gibLowerArm;
    public GameObject gibUpperBody;
    public GameObject gibLowerBody;
    public GameObject gibHead;
    public GameObject gibHips;
    public GameObject gibUpperLeg;
    public GameObject gibLowerLeg;
    [Header("--------------- Models & Textures ---------------")]
    public int themeOverride;
    public Material[] handMaterials;
    public Mesh[] theme1Meshes;
    public Material[] theme1Materials;
    public Mesh[] theme2Meshes;
    public Material[] theme2Materials;
    public Mesh[] theme2HairMeshes;
    public Mesh theme3Mesh;
    public Material[] theme3Materials;
    public Mesh[] theme4Meshes;
    public Material[] theme4Materials;
    public Mesh[] theme4HairMeshes;
    public Mesh[] theme4HeavyMeshes;
    public Material[] theme4HeavyMaterials;
    [Header("Theme 5")]
    public Material theme5Material;
    public Mesh[] heads;
    public Mesh[] torsos;
    public Mesh[] legs;
    [Header("-----------------------------------------------")]
    public bool faceRight;
    [Header("-------------------- Idle ---------------------")]
    public string idleAnim;
    public string engageAnimName;
    public bool neverReturnToIdleAnim;
    public GameObject leftHandItem;
    public Vector3 leftHandItemPosOffset;
    public Vector3 leftHandItemRotOffset;
    public bool dropItemOnAlert;
    [Space]
    public bool dontFaceNoiseDirection;
    public bool standStill;
    public float walkLeftIdleAmount;
    public float walkRightIdleAmount;
    [Header("------------------ Hunt mode ------------------")]
    public float weapon;
    public bool disableWeaponPickup;
    public bool standStillInHuntMode;
    public float walkLeftAbsoluteAmount;
    public float walkRightAbsoluteAmount;
    [HideInInspector]
    public bool bulletHit;
    [HideInInspector]
    public string bulletHitName;
    [HideInInspector]
    public float bulletStrength;
    [HideInInspector]
    public Vector3 bulletHitPos;
    [HideInInspector]
    public Quaternion bulletHitRot;
    [HideInInspector]
    public Vector3 bulletHitVel;
    [HideInInspector]
    public bool allowGib;
    [HideInInspector]
    public bool allowBulletHit;
    [HideInInspector]
    public bool bulletKillOnHeadshot;
    [HideInInspector]
    public float doorHideTimer;
    [HideInInspector]
    public bool attackAfterDoorSpawn;
    [HideInInspector]
    public float bulletTimeAlive;
    [HideInInspector]
    public bool doorSpawn;
    [HideInInspector]
    public bool idle;
    public bool neverReturnToIdle;
    public EnemyAlarmScript runToAlarm;
    private bool runToAlarmDoOnce;
    [HideInInspector]
    public float electricutionTimer;
    private bool doneWithDoorAppearAnimationDoOnce;
    [HideInInspector]
    public float health;
    private float walkSpeed;
    private Vector3 startPos;
    private float playerCheckRayCastTimer;
    private float framesSincePlayerLastSeen;
    public float shootTimer;
    private float shotsInARow;
    private float idleWalkTimer;
    [HideInInspector]
    public bool startFacingRight;
    private PlayerScript playerScript;
    private float standStillTimer;
    private float targetCrouchAmount;
    private float timeRequiredBeforeCrouch;
    private bool havePushedAgainstWall;
    private bool shotInLeg;
    private ParticleSystem bloodDropsParticle;
    private ParticleSystem bloodMistParticle;
    private bool bleedFromHand;
    private float bleedFromHandTimer;
    private float bulletHitStrengthResetTimer;
    private Transform playerTurretGunTop;
    private bool aimForTurretGun;
    public bool hasBeenOnScreen;
    public float playerDetectionRadius;
    public float playerShootingDetectionRadius;
    public bool engageOnHearingShot;
    public bool alwaysEngageOnRemoteTrigger;
    public int enemyType;
    private float timeInbetweenShots;
    private float weaponShotsInRow;
    private float aimAngleOffset;
    private Rigidbody mainPlayerRigidBody;
    private bool initialAimComplete;
    private float initialAimFireDelay;
    [HideInInspector]
    public float alertAmount;
    [HideInInspector]
    public bool remoteAlerted;
    private float alertAmountTarget;
    private bool alertDoOnce;
    [HideInInspector]
    public float remoteTriggeredTimer;
    [HideInInspector]
    public bool hasBeenRemoteTriggered;
    private GameObject[] allEnemies;
    private EnemyScript[] enemiesInRangeAtStart;
    private bool attackModeDoOnce;
    [HideInInspector]
    public float knockedBackTimer;
    [HideInInspector]
    public Transform groundTransform;
    private bool beenGibbed;
    private bool doorSpawnDoOnce;
    private float aimSpeed;
    [HideInInspector]
    public bool engageAnimFinished;
    private float bulletHitLayerWeight;
    private float loadGameFireDelay;
    public float sniperAimOffset;
    public float sniperAimMoveAmount;
    public bool cantBeHarmed;
    public bool alwaysGetBulletHitHeadshot;
    [HideInInspector]
    public string bulletHitText;
    [HideInInspector]
    public string bulletKillText;
    [HideInInspector]
    public float bulletHitExtraScore;
    [HideInInspector]
    public float bulletKillExtraScore;
    [HideInInspector]
    public EnemySpeechHandlerScript enemySpeechHandlerScript;
    private AutoAimTargetScript autoAimTargetScript;
    private bool dontRagDoll;
    private int theme;
    private EnemyDeathBlinkScript enemyDeathBlinkScript;
    private bool bouncedOnBouncePad;
    private Collider[] collidersInChildren;
    private Rigidbody[] rigidBodiesInChildren;
    private bool enemyDiedFailSafe;
    private float timeAlive;


    public EnemyScript()
    {
        this.fallKillThreshold = -90f;
        this.damageMultiplier = 1f;
        this.okToStand = true;
        this.legLength = 1.8f;
        this.standLegLength = 1.6f;
        this.crouchLegLength = 0.6f;
        this.raycastWidthRange = 1.25f;
        this.timescale = 1f;
        this.timescaleRaw = 1f;
        this.aimBlend = 1f;
        this.aimBlendTarget = 1f;
        this.lookAimBlend = 1f;
        this.lookAimBlendTarget = 1f;
        this.punchAnimNr = 1f;
        this.themeOverride = -1;
        this.faceRight = true;
        this.walkLeftIdleAmount = 5f;
        this.walkRightIdleAmount = 5f;
        this.weapon = 1f;
        this.walkLeftAbsoluteAmount = 7f;
        this.walkRightAbsoluteAmount = 7f;
        this.idle = true;
        this.health = 1f;
        this.walkSpeed = 1.5f;
        this.playerDetectionRadius = 20f;
        this.playerShootingDetectionRadius = 25f;
        this.sniperAimOffset = 2f;
        this.sniperAimMoveAmount = 3f;
    }

    public virtual void Awake()
    {
        if (!this.mainBodyKinematic)
            return;
        this.rBody = (Rigidbody)this.gameObject.GetComponent(typeof(Rigidbody));
        this.rBody.isKinematic = true;
    }

    public virtual void Start()
    {

        dontCheckDistanceBeforeRunningLogic = true;

        networkHelper = gameObject.AddComponent<NetworkedEnemyScriptAttachment>();


        if (doorSpawn || rappelling || skyfall || motorcycle)
            networkHelper.registerSelfOnSpawn = true;

        if (skyfall || rappelling || motorcycle)
        {
            specialEnemySyncer = gameObject.AddComponent<NetworkedBaseTransform>();
            specialEnemySyncer.doLerpAuto = false;
            specialEnemySyncer.dontDoDebug = true;
        }

        this.root = RootScript.RootScriptInstance;
        this.rootShared = RootSharedScript.Instance;
        this.statsTracker = (StatsTrackerScript)this.rootShared.gameObject.GetComponent(typeof(StatsTrackerScript));
        this.enemyOptimizerScript = (EnemyOptimizerScript)this.GetComponent(typeof(EnemyOptimizerScript));
        UnityScript.Lang.Array array1 = new UnityScript.Lang.Array((IEnumerable)this.root.allEnemies);
        array1.Push((object)this.transform);
        this.root.allEnemies = array1.ToBuiltin(typeof(Transform)) as Transform[];
        this.mainPlayer = GameObject.Find("Player").transform;
        this.playerScript = (PlayerScript)this.mainPlayer.gameObject.GetComponent(typeof(PlayerScript));
        this.mainPlayerRigidBody = (Rigidbody)this.mainPlayer.gameObject.GetComponent(typeof(Rigidbody));
        this.playerGraphics = this.transform.Find("EnemyGraphics");
        this.animator = (Animator)this.playerGraphics.GetComponent(typeof(Animator));
        this.curCamera = (Camera)GameObject.Find("Main Camera").GetComponent(typeof(Camera));
        this.cameraScript = (CameraScript)this.curCamera.GetComponent(typeof(CameraScript));
        if (this.rBody == null)
            this.rBody = (Rigidbody)this.gameObject.GetComponent(typeof(Rigidbody));
        if (this.skyfall || this.motorcycle != null || !this.mainBodyKinematic)
        {
            this.mainBodyKinematic = false;
            this.rBody.isKinematic = false;
        }
        this.enemyDeathBlinkScript = (EnemyDeathBlinkScript)this.gameObject.GetComponent(typeof(EnemyDeathBlinkScript));
        this.doorSpawnBulletCheck = this.transform.Find("DoorSpawnBulletCheck").gameObject;
        this.lowerBack = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack");
        this.upperBack = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack");
        this.neck = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Neck");
        this.head = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Neck/Head");
        this.shoulderR = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R");
        this.upperArmR = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R");
        this.lowerArmR = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R");
        this.handR = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R");
        this.bulletPointR = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/BulletPoint");
        this.shoulderL = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L");
        this.upperArmL = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L");
        this.lowerArmL = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L/LowerArm_L");
        this.handL = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L");
        this.bulletPointL = this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L/BulletPoint");
        this.footR = this.transform.Find("EnemyGraphics/Armature/Center/Hip_R/UpperLeg_R/LowerLeg_R/Foot_R");
        this.footTipR = this.transform.Find("EnemyGraphics/Armature/Center/Hip_R/UpperLeg_R/LowerLeg_R/Foot_R/FootTip_R");
        this.upperLegR = this.transform.Find("EnemyGraphics/Armature/Center/Hip_R/UpperLeg_R");
        this.lowerLegR = this.transform.Find("EnemyGraphics/Armature/Center/Hip_R/UpperLeg_R/LowerLeg_R");
        this.footL = this.transform.Find("EnemyGraphics/Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L/Foot_L");
        this.footTipL = this.transform.Find("EnemyGraphics/Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L/Foot_L/FootTip_L");
        this.upperLegL = this.transform.Find("EnemyGraphics/Armature/Center/Hip_L/UpperLeg_L");
        this.lowerLegL = this.transform.Find("EnemyGraphics/Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L");
        this.hipR = this.transform.Find("EnemyGraphics/Armature/Center/Hip_R");
        this.hipL = this.transform.Find("EnemyGraphics/Armature/Center/Hip_L");
        this.center = this.transform.Find("EnemyGraphics/Armature/Center");
        this.handRAudio = (AudioSource)this.bulletPointR.GetComponent(typeof(AudioSource));
        this.handLAudio = (AudioSource)this.bulletPointL.GetComponent(typeof(AudioSource));
        this.headAudio = (AudioSource)this.head.GetComponent(typeof(AudioSource));
        this.deathAudioSource = (AudioSource)this.neck.GetComponent(typeof(AudioSource));
        this.generalAudioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.bulletPointRAimTarget = new GameObject("AimTarget");
        this.bulletPointRAimTarget.transform.parent = this.bulletPointR;
        this.bulletPointRAimTarget.transform.localPosition = new Vector3(0.0f, 0.0f, -1f);
        this.bulletPointLAimTarget = new GameObject("AimTarget");
        this.bulletPointLAimTarget.transform.parent = this.bulletPointL;
        this.bulletPointLAimTarget.transform.localPosition = new Vector3(0.0f, 0.0f, -1f);
        this.shellParticle = (ParticleSystem)GameObject.Find("Main Camera/ShellParticle").GetComponent(typeof(ParticleSystem));
        this.smokeParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeParticle").GetComponent(typeof(ParticleSystem));
        this.bloodDropsParticle = (ParticleSystem)GameObject.Find("Main Camera/BloodDropsParticle").GetComponent(typeof(ParticleSystem));
        this.bloodMistParticle = (ParticleSystem)GameObject.Find("Main Camera/BloodMistParticle").GetComponent(typeof(ParticleSystem));
        this.layerMask = (LayerMask)98560;
        this.layerMaskSpotPlayer = (LayerMask)65792;
        this.layerMaskOnlyPlayer = (LayerMask)2048;
        this.layerMaskOnlyLevelCollision = (LayerMask)256;
        this.layerMaskIncPlayerAndPlayerGameCollisionWithoutBulletPassthrough = (LayerMask)67841;
        this.pistolR = this.handR.Find("pistol");
        this.pistolL = this.handL.Find("pistol");
        this.uziR = this.handR.Find("uzi");
        this.uziL = this.handL.Find("uzi");
        this.machineGun = this.handR.Find("machinegun");
        this.shotgun = this.handR.Find("shotgun");
        this.sniper = this.handR.Find("Rifle");
        this.rifleLaser = this.sniper.Find("RifleLaser");
        this.crossbow = this.handR.Find("Crossbow");
        this.crossbowPipe = this.crossbow.Find("Crossbow_Pipe");
        this.crossbowBow1 = this.crossbowPipe.Find("Crossbow_Bow_1");
        this.crossbowBow2 = this.crossbowPipe.Find("Crossbow_Bow_2");
        this.crossbowArrows = new GameObject[4];
        for (int index = 0; index < 4; ++index)
            this.crossbowArrows[index] = this.crossbowPipe.Find(RuntimeServices.op_Addition("Crossbow_Arrow_", (object)index)).gameObject;
        this.club = this.handR.Find("Club");
        this.autoAimTargetScript = (AutoAimTargetScript)this.GetComponent(typeof(AutoAimTargetScript));
        if (this.simpleEnemy)
            this.alwaysOnFlatGround = true;
        if (this.alwaysOnFlatGround)
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(this.transform.position, Vector3.down, out hitInfo, 3f, (int)this.layerMask))
            {
                this.alwaysOnFlatGroundYRef = hitInfo.point.y;
                this.onGround = true;
                this.groundTransform = hitInfo.collider.transform;
                float num1 = hitInfo.point.y + this.legLength;
                Vector3 position = this.transform.position;
                double num2 = (double)(position.y = num1);
                Vector3 vector3 = this.transform.position = position;
                this.ySpeed = 0.0f;
            }
        }
        this.collidersInChildren = this.GetComponentsInChildren<Collider>();
        this.rigidBodiesInChildren = this.GetComponentsInChildren<Rigidbody>();
        if (!this.doorSpawn && this.motorcycle == null && !this.simpleEnemy)
        {
            int index = 0;
            Rigidbody[] bodiesInChildren = this.rigidBodiesInChildren;
            for (int length = bodiesInChildren.Length; index < length; ++index)
            {
                if (bodiesInChildren[index].gameObject.layer != 17 && bodiesInChildren[index].gameObject.layer != 14 && (bodiesInChildren[index].transform != this.useHat && this.disableWeaponPickUpDoThing(bodiesInChildren[index].transform)))
                {
                    RigidBodySlowMotion rigidBodySlowMotion = (RigidBodySlowMotion)bodiesInChildren[index].gameObject.AddComponent(typeof(RigidBodySlowMotion));
                    rigidBodySlowMotion.Start();
                    rigidBodySlowMotion.enabled = false;
                }
            }
            this.enemyOptimizerScript.getRigidBodySlowMotionComponents();
        }
        SkinnedMeshRenderer component1 = (SkinnedMeshRenderer)this.transform.Find("EnemyGraphics/Head02").GetComponent(typeof(SkinnedMeshRenderer));
        SkinnedMeshRenderer component2 = (SkinnedMeshRenderer)this.transform.Find("EnemyGraphics/TorsoWhiteTankTop").GetComponent(typeof(SkinnedMeshRenderer));
        SkinnedMeshRenderer component3 = (SkinnedMeshRenderer)this.transform.Find("EnemyGraphics/Legs01").GetComponent(typeof(SkinnedMeshRenderer));
        SkinnedMeshRenderer component4 = (SkinnedMeshRenderer)this.handR.Find("hand_01").GetComponent(typeof(SkinnedMeshRenderer));
        SkinnedMeshRenderer component5 = (SkinnedMeshRenderer)this.handL.Find("hand_01_L").GetComponent(typeof(SkinnedMeshRenderer));
        this.theme = this.themeOverride == -1 ? (this.enemyType == 1 || this.enemyType == 2 ? 5 : this.root.theme) : this.themeOverride;
        if (this.theme == 5)
        {
            if (this.enemyType == 1)
            {
                component1.sharedMesh = this.heads[1];
                component2.sharedMesh = this.torsos[1];
                component3.sharedMesh = this.legs[0];
                component1.sharedMaterial = component2.sharedMaterial = component3.sharedMaterial = this.theme5Material;
            }
            else if (this.enemyType == 2)
            {
                component1.sharedMesh = this.heads[2];
                component2.sharedMesh = this.torsos[2];
                component3.sharedMesh = this.legs[1];
                component1.sharedMaterial = component2.sharedMaterial = component3.sharedMaterial = this.theme5Material;
            }
            else
            {
                component1.sharedMesh = this.heads[0];
                component2.sharedMesh = this.torsos[0];
                component3.sharedMesh = this.legs[0];
                component2.sharedMaterial = component3.sharedMaterial = this.theme5Material;
                UnityScript.Lang.Array array2 = new UnityScript.Lang.Array(new object[2]
                {
          (object) this.theme5Material,
          (object) this.theme2Materials[Random.Range(0, this.theme2Materials.Length)]
                });
                component1.sharedMaterials = array2.ToBuiltin(typeof(Material)) as Material[];
            }
            this.head.Find("Hats").gameObject.SetActive(false);
            this.head.Find("gamer_hair").gameObject.SetActive(false);
        }
        else if (this.enemyType == 0 || this.enemyType == 3)
        {
            component1.enabled = false;
            component3.enabled = false;
            component4.sharedMaterial = component5.sharedMaterial = this.handMaterials[0];
            if (this.theme == 1)
            {
                component2.sharedMesh = this.theme1Meshes[Random.Range(0, Extensions.get_length((System.Array)this.theme1Meshes))];
                component2.sharedMaterial = this.theme1Materials[Random.Range(0, this.theme1Materials.Length)];
                this.head.Find("gamer_hair").gameObject.SetActive(false);
                Transform transform1 = this.head.Find("Hats");
                if ((double)Random.value > 0.5 && this.motorcycle == null && !this.simpleEnemy)
                {
                    this.useHat = transform1.GetChild(Random.Range(0, transform1.childCount));
                    ((Renderer)this.useHat.GetComponent(typeof(MeshRenderer))).sharedMaterial = component2.sharedMaterial;
                    IEnumerator enumerator = UnityRuntimeServices.GetEnumerator((object)transform1);
                    while (enumerator.MoveNext())
                    {
                        object obj = enumerator.Current;
                        if (!(obj is Transform))
                            obj = RuntimeServices.Coerce(obj, typeof(Transform));
                        Transform transform2 = (Transform)obj;
                        if (transform2 != this.useHat)
                        {
                            transform2.gameObject.SetActive(false);
                            UnityRuntimeServices.Update(enumerator, (object)transform2);
                        }
                    }
                }
                else
                    transform1.gameObject.SetActive(false);
            }
            else if (this.theme == 2)
            {
                int index1 = Random.Range(0, this.theme2Materials.Length);
                component2.sharedMesh = this.theme2Meshes[Random.Range(0, Extensions.get_length((System.Array)this.theme2Meshes))];
                component2.sharedMaterial = this.theme2Materials[index1];
                int index2 = Random.Range(0, this.theme2HairMeshes.Length + 1);
                if (index2 >= this.theme2HairMeshes.Length || this.simpleEnemy)
                {
                    this.head.Find("gamer_hair").gameObject.SetActive(false);
                }
                else
                {
                    ((MeshFilter)this.head.Find("gamer_hair").GetComponent(typeof(MeshFilter))).sharedMesh = this.theme2HairMeshes[index2];
                    if (index1 == 3 && index2 == 1)
                        ((Renderer)this.head.Find("gamer_hair").GetComponent(typeof(MeshRenderer))).sharedMaterial = component2.sharedMaterial;
                    else
                        ((Renderer)this.head.Find("gamer_hair").GetComponent(typeof(MeshRenderer))).sharedMaterial = this.theme2Materials[Random.Range(0, this.theme2Materials.Length)];
                }
                this.head.Find("Hats").gameObject.SetActive(false);
            }
            else if (this.theme == 3)
            {
                this.head.Find("Hats").gameObject.SetActive(false);
                this.head.Find("gamer_hair").gameObject.SetActive(false);
                component4.gameObject.SetActive(false);
                component5.gameObject.SetActive(false);
                component2.sharedMesh = this.theme3Mesh;
                UnityScript.Lang.Array array2 = new UnityScript.Lang.Array(new object[2]
                {
          (object) this.theme3Materials[0],
          (object) this.theme3Materials[1]
                });
                component2.sharedMaterials = array2.ToBuiltin(typeof(Material)) as Material[];
            }
            else if (this.theme == 4)
            {
                if (this.enemyType == 3)
                {
                    int index = Random.Range(0, this.theme4HeavyMeshes.Length);
                    component2.sharedMesh = this.theme4HeavyMeshes[index];
                    UnityScript.Lang.Array array2 = new UnityScript.Lang.Array(new object[2]
                    {
            (object) this.theme4Materials[Random.Range(0, this.theme4Materials.Length)],
            (object) this.theme4HeavyMaterials[index]
                    });
                    component2.sharedMaterials = array2.ToBuiltin(typeof(Material)) as Material[];
                    this.head.Find("gamer_hair").gameObject.SetActive(false);
                }
                else
                {
                    component2.sharedMesh = this.theme4Meshes[Random.Range(0, Extensions.get_length((System.Array)this.theme4Meshes))];
                    component2.sharedMaterial = this.theme4Materials[Random.Range(0, this.theme4Materials.Length)];
                    int index = Random.Range(0, this.theme4HairMeshes.Length + 1);
                    if (index >= this.theme4HairMeshes.Length || this.simpleEnemy)
                    {
                        this.head.Find("gamer_hair").gameObject.SetActive(false);
                    }
                    else
                    {
                        ((MeshFilter)this.head.Find("gamer_hair").GetComponent(typeof(MeshFilter))).sharedMesh = this.theme4HairMeshes[index];
                        ((Renderer)this.head.Find("gamer_hair").GetComponent(typeof(MeshRenderer))).sharedMaterial = component2.sharedMaterial;
                    }
                }
                this.head.Find("Hats").gameObject.SetActive(false);
            }
        }
        else
        {
            this.head.Find("Hats").gameObject.SetActive(false);
            this.head.Find("gamer_hair").gameObject.SetActive(false);
        }
        this.idleWalkTimer = (float)Random.Range(40, 150);
        this.startPos = this.transform.position;
        this.startFacingRight = this.faceRight;
        this.walkLeftIdleAmount = Mathf.Clamp(this.walkLeftIdleAmount, 0.0f, this.walkLeftAbsoluteAmount);
        this.walkRightIdleAmount = Mathf.Clamp(this.walkRightIdleAmount, 0.0f, this.walkRightAbsoluteAmount);
        if (this.doorSpawn)
        {
            this.autoAimTargetScript.enabled = false;
            if (this.attackAfterDoorSpawn)
            {
                this.idle = false;
                this.engageAnimFinished = true;
                this.aimBlend = this.aimBlendTarget = 0.0f;
                this.lookAimBlend = this.lookAimBlendTarget = 0.25f;
                this.doorHideTimer = (float)Random.Range(50, 70);
                this.animator.Play("DoorAppear1", 0);
            }
            this.playerGraphics.localRotation = Quaternion.Euler(0.0f, 180f, 0.0f);
        }
        else
        {
            ++this.root.nrOfEnemiesTotal;
            this.doorSpawnBulletCheck.SetActive(false);
        }
        this.allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        UnityScript.Lang.Array array3 = new UnityScript.Lang.Array();
        int index3 = 0;
        GameObject[] allEnemies1 = this.allEnemies;
        for (int length = allEnemies1.Length; index3 < length; ++index3)
        {
            if (allEnemies1[index3] != null && allEnemies1[index3] != this.gameObject && (allEnemies1[index3].layer == 13 && (double)Vector2.Distance((Vector2)allEnemies1[index3].transform.position, (Vector2)this.transform.position) < 40.0) && !Physics.Linecast(this.transform.position + new Vector3(0.0f, 1.5f, 0.0f), allEnemies1[index3].transform.position + new Vector3(0.0f, 1f, 0.0f), (int)this.layerMaskOnlyLevelCollision))
            {
                Debug.DrawLine(this.transform.position + new Vector3(0.0f, 1.5f, 0.0f), allEnemies1[index3].transform.position + new Vector3(0.0f, 1f, 0.0f), new Color(0.0f, 1f, 0.0f, 1f));
                array3.push((object)(EnemyScript)allEnemies1[index3].GetComponent(typeof(EnemyScript)));
            }
        }
        this.enemiesInRangeAtStart = array3.ToBuiltin(typeof(EnemyScript)) as EnemyScript[];
        if (this.enemiesInRangeAtStart.Length > 0)
            ++this.root.potentialMultipliersFromEnemies;
        if ((double)this.weapon == -1.0)
        {
            if (this.motorcycle == null && this.stickToTransform == null && (SavedData.GetInt("weaponActive1") == 1 && !this.root.isMiniLevel))
            {
                Random.InitState((int)this.transform.position.magnitude);
                this.weapon = (float)this.root.allowedWeaponsForEnemies[Random.Range(0, Extensions.get_length((System.Array)this.root.allowedWeaponsForEnemies))];
                if ((double)this.weapon == 6.0)
                {
                    int index1 = 0;
                    GameObject[] allEnemies2 = this.allEnemies;
                    for (int length = allEnemies2.Length; index1 < length; ++index1)
                    {
                        if (allEnemies2[index1] != this.gameObject)
                        {
                            EnemyScript component6 = (EnemyScript)allEnemies2[index1].GetComponent(typeof(EnemyScript));
                            if (component6 != null && (double)component6.weapon == 6.0 && (double)Vector3.Distance(this.transform.position, allEnemies2[index1].transform.position) < 25.0)
                            {
                                this.weapon = 1f;
                                break;
                            }
                        }
                    }
                }
                if (this.enemiesInRangeAtStart.Length <= 0 && (double)this.weapon >= 3.0)
                    this.weapon = (double)Random.value <= 0.899999976158142 ? 1f : 3f;
            }
            else
                this.weapon = 1f;
        }
        this.changeWeapon(this.weapon);
        if ((double)this.weapon == 9.0)
            this.hasBeenOnScreen = true;
        this.timeRequiredBeforeCrouch = (float)Random.Range(120, 240);
        this.playerTurretGunTop = this.mainPlayer.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/turret_gun/Turret_Gun_Pipe");
        this.pistolClipParticle = (ParticleSystem)GameObject.Find("Main Camera/PistolClipParticle").GetComponent(typeof(ParticleSystem));
        this.weaponShotsInRow = (double)this.weapon == 3.0 || (double)this.weapon == 4.0 || (double)this.weapon == 5.0 ? 5f : 3f;
        this.aimAngleOffset = 0.0f;
        this.initialAimFireDelay = 45f;
        this.timeInbetweenShots = 90f;
        this.aimSpeed = 0.1f;
        if (this.enemyType == 1)
        {
            this.health = 3f;
            this.initialAimFireDelay = 25f;
            this.timeInbetweenShots = 45f;
            this.walkSpeed = 2f;
        }
        else if (this.enemyType == 2)
        {
            this.health = 6f;
            this.initialAimFireDelay = 45f;
            this.timeInbetweenShots = 60f;
            this.walkSpeed = 1f;
            this.aimSpeed = 0.05f;
        }
        else if (this.enemyType == 3)
        {
            this.health = 1.6f;
            this.initialAimFireDelay = 45f;
            this.timeInbetweenShots = 60f;
            this.walkSpeed = 9f;
            this.standStill = true;
            this.idleAnim = "Bat_Idle";
            this.engageAnimName = (double)Random.value <= 0.5 ? "Bat_Detect_2" : "Bat_Detect";
            this.alwaysOnFlatGround = false;
            this.dontDoLegBendRayCast = false;
            this.dontDoArmBendRayCast = false;
            this.dontWallCheckLeft = false;
            this.dontWallCheckRight = false;
        }
        if ((double)this.weapon == 9.0)
            this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        if (this.engageAnimName == string.Empty)
            this.engageAnimFinished = true;
        if (this.rappelling)
        {
            this.fallKillThreshold = -999f;
            this.rappellStringScript = (StringScript)this.rappellTransform.parent.GetComponent(typeof(StringScript));
            this.rappellStringScript.updateRope();
        }
        if ((double)this.voicePitchOffset == 0.0)
            this.voicePitchOffset = Random.Range(-0.9f, 1.1f);
        if (this.idleAnim != (string)null)
        {
            this.aimBlendTarget = 0.0f;
            this.aimBlend = 0.0f;
            this.lookAimBlendTarget = 0.0f;
            this.lookAimBlend = 0.0f;
            this.animator.Play(this.idleAnim, 0);
        }
        if (this.leftHandItem != null)
        {
            this.leftHandItem = UnityEngine.Object.Instantiate<GameObject>(this.leftHandItem);
            this.leftHandItem.isStatic = false;
            this.leftHandItem.transform.parent = this.handL;
            this.leftHandItem.transform.localPosition = this.leftHandItemPosOffset;
            this.leftHandItem.transform.localRotation = Quaternion.Euler(this.leftHandItemRotOffset);
            KnifeScript component6 = (KnifeScript)this.leftHandItem.GetComponent(typeof(KnifeScript));
            if (component6 != null)
            {
                component6.friendly = false;
                component6.enabled = false;
                component6.transform.Find("Meat").gameObject.SetActive(false);
                ((Behaviour)component6.GetComponent(typeof(ObjectKickScript))).enabled = false;
                ((Rigidbody)component6.GetComponent(typeof(Rigidbody))).isKinematic = true;
            }
            if (this.dropItemOnAlert)
            {
                this.leftHandItem.layer = 22;
                if (this.leftHandItem.GetComponent(typeof(Rigidbody)) == null)
                    ((Rigidbody)this.leftHandItem.AddComponent(typeof(Rigidbody))).isKinematic = true;
                if (this.leftHandItem.GetComponent(typeof(Collider)) == null)
                {
                    Collider collider = (Collider)this.leftHandItem.AddComponent(typeof(BoxCollider));
                }
            }
        }
        if (this.skyfall)
        {
            this.autoAimTargetScript.posOffset.y = -1f;
            this.dontCheckDistanceBeforeRunningLogic = true;
            this.animator.runtimeAnimatorController = (RuntimeAnimatorController)this.skyfallAnimController;
            this.neverReturnToIdle = true;
            this.idle = false;
            this.standStill = true;
            this.standStillInHuntMode = true;
            this.fireDelay = (float)(180 + Random.Range(0, 40));
        }
        if (this.motorcycle != null || this.stickToTransform != null)
        {
            this.dontCheckDistanceBeforeRunningLogic = true;
            this.standStill = this.standStillInHuntMode = true;
            this.idle = false;
            this.neverReturnToIdle = true;
            if (this.motorcycle != null)
            {
                this.motorcycleTransform = this.motorcycle.Find("PlayerMotorcycleGraphics/PlayerPos");
                ((MotorcycleScript)this.motorcycle.GetComponent(typeof(MotorcycleScript))).linkedEnemy = (EnemyScript)this.GetComponent(typeof(EnemyScript));
                if (this.root.showNoBlood)
                {
                    this.head.Find("Hats").gameObject.SetActive(false);
                    this.head.Find("gamer_hair").gameObject.SetActive(false);
                    component4.enabled = false;
                    component5.enabled = false;
                    component2.enabled = false;
                    ((Renderer)this.pistolR.GetComponent(typeof(MeshRenderer))).enabled = false;
                    ((Renderer)this.pistolL.GetComponent(typeof(MeshRenderer))).enabled = false;
                    ((Renderer)this.uziR.GetComponent(typeof(MeshRenderer))).enabled = false;
                    ((Renderer)this.uziL.GetComponent(typeof(MeshRenderer))).enabled = false;
                }
            }
            if ((double)this.weapon == 1.0 || (double)this.weapon == 2.0)
                this.disableWeaponPickup = true;
        }
        Component[] componentsInChildren1 = this.GetComponentsInChildren(typeof(CharacterJoint));
        int index4 = 0;
        Component[] componentArray = componentsInChildren1;
        for (int length = componentArray.Length; index4 < length; ++index4)
            ((CharacterJoint)componentArray[index4]).enableProjection = true;
        if (this.rootShared.modBigHead)
            this.head.localScale *= 2f;


        foreach (CapsuleCollider rb in center.GetComponentsInChildren<CapsuleCollider>())
        {
            rb.GetComponent<Rigidbody>().useGravity = false;
            rb.GetComponent<Rigidbody>().isKinematic = false;
            rb.enabled = true;
        }

        dontCheckDistanceBeforeRunningLogic = true;

        if (!this.simpleEnemy)
            return;
        this.simpleHandR = new GameObject();
        this.simpleHandR.name = "HandR";
        this.simpleHandR.transform.SetParent(this.transform);
        this.simpleHandR.transform.localScale = Vector3.one;
        this.simpleHandR.transform.position = this.handR.position;
        this.simpleHandR.transform.rotation = this.handR.rotation;
        Transform[] componentsInChildren2 = this.handR.GetComponentsInChildren<Transform>(false);
        int index5 = 0;
        Transform[] transformArray1 = componentsInChildren2;
        for (int length = transformArray1.Length; index5 < length; ++index5)
        {
            if (transformArray1[index5] != this.handR && transformArray1[index5].parent == this.handR)
                transformArray1[index5].SetParent(this.simpleHandR.transform, true);
        }
        this.simpleHandL = new GameObject();
        this.simpleHandL.name = "HandL";
        this.simpleHandL.transform.SetParent(this.transform);
        this.simpleHandL.transform.localScale = Vector3.one;
        this.simpleHandL.transform.position = this.handL.position;
        this.simpleHandL.transform.rotation = this.handL.rotation;
        Transform[] componentsInChildren3 = this.handL.GetComponentsInChildren<Transform>(false);
        int index6 = 0;
        Transform[] transformArray2 = componentsInChildren3;
        for (int length = transformArray2.Length; index6 < length; ++index6)
        {
            if (transformArray2[index6] != this.handL && transformArray2[index6].parent == this.handL)
                transformArray2[index6].SetParent(this.simpleHandL.transform, true);
        }
        this.simpleCollision = new GameObject();
        this.simpleCollision.name = "sCol";
        this.simpleCollision.transform.SetParent(this.transform, false);
        this.simpleCollision.transform.localPosition = Vector3.zero;
        this.simpleCollision.layer = 10;
        this.simpleCollision.tag = "Enemy";
        this.sCol = (CapsuleCollider)this.simpleCollision.AddComponent(typeof(CapsuleCollider));
        this.sCol.center = new Vector3(0.0f, 0.25f, 0.0f);
        this.sCol.radius = 0.5f;
        this.sCol.height = 3.8f;
        this.sCol.direction = 1;
        this.sCol.isTrigger = true;
        this.enemyOptimizerScript.sCol = this.sCol;
        Transform transform = this.transform.Find("EnemyGraphics/Armature");
        CharacterJoint[] componentsInChildren4 = transform.GetComponentsInChildren<CharacterJoint>(true);
        int index7 = 0;
        CharacterJoint[] characterJointArray = componentsInChildren4;
        for (int length = characterJointArray.Length; index7 < length; ++index7)
            UnityEngine.Object.DestroyImmediate(characterJointArray[index7]);
        Collider[] componentsInChildren5 = transform.GetComponentsInChildren<Collider>(true);
        int index8 = 0;
        Collider[] colliderArray = componentsInChildren5;
        for (int length = colliderArray.Length; index8 < length; ++index8)
            UnityEngine.Object.DestroyImmediate(colliderArray[index8]);
        Rigidbody[] componentsInChildren6 = transform.GetComponentsInChildren<Rigidbody>(true);
        int index9 = 0;
        Rigidbody[] rigidbodyArray = componentsInChildren6;
        for (int length = rigidbodyArray.Length; index9 < length; ++index9)
            UnityEngine.Object.DestroyImmediate(rigidbodyArray[index9]);
        PhysicsSoundsScript[] componentsInChildren7 = transform.GetComponentsInChildren<PhysicsSoundsScript>(true);
        int index10 = 0;
        PhysicsSoundsScript[] physicsSoundsScriptArray = componentsInChildren7;
        for (int length = physicsSoundsScriptArray.Length; index10 < length; ++index10)
            UnityEngine.Object.DestroyImmediate(physicsSoundsScriptArray[index10]);
        transform.gameObject.SetActive(false);
        this.dontRagDoll = true;

        //start marker
    }

    public virtual void Update()
    {
        this.playerDistance = this.enemyOptimizerScript.playerDistance;
        // playerDistance = 0;
        if (!this.dontCheckDistanceBeforeRunningLogic)
        {
            this.runLogic = false;
            if ((double)this.playerDistance < 40.0)
                this.runLogic = true;
            else if ((double)this.playerDistance < 50.0 && this.enemiesInRangeAtStart.Length > 0)
            {
                int index = 0;
                EnemyScript[] enemiesInRangeAtStart = this.enemiesInRangeAtStart;
                for (int length = enemiesInRangeAtStart.Length; index < length; ++index)
                {
                    if (enemiesInRangeAtStart[index] != null && enemiesInRangeAtStart[index].runLogic)
                        this.runLogic = true;
                }
            }
        }
        else
            this.runLogic = true;
        if (!this.runLogic)
        {
            if (this.runLogicDoOnce)
                return;
            if (!this.mainBodyKinematic)
                this.rBody.velocity = this.rBody.angularVelocity = Vector3.zero;
            this.runLogicDoOnce = true;
        }
        else
        {
            if (this.runLogicDoOnce)
                this.runLogicDoOnce = false;
            if (this.simpleEnemy)
            {
                this.simpleHandR.transform.position = this.handR.position;
                this.simpleHandR.transform.rotation = this.handR.rotation;
                this.simpleHandL.transform.position = this.handL.position;
                this.simpleHandL.transform.rotation = this.handL.rotation;
            }
            float targetXspeed = this.targetXSpeed;
            this.timescale = this.root.timescale;
            this.timescaleRaw = this.root.timescaleRaw;
            if ((double)this.timeAlive < 600.0)
                this.timeAlive += this.timescale;
            this.kFire = false;
            if (!this.hasBeenOnScreen)
            {
                Vector3 viewportPoint = this.curCamera.WorldToViewportPoint(this.transform.position);
                if ((double)viewportPoint.x >= -0.100000001490116 && (double)viewportPoint.x <= 1.10000002384186 && ((double)viewportPoint.y >= -0.100000001490116 && (double)viewportPoint.y <= 1.10000002384186))
                    this.hasBeenOnScreen = true;
            }
            float playerDistance1 = 0;
            if (this.hasBeenOnScreen && (this.doorSpawn || this.idle))
            {
                playerDistance1 = this.playerDistance;
                if (this.playerScript.weapon != 10 && (double)this.remoteTriggeredTimer <= 0.0 && ((double)playerDistance1 < (double)this.playerShootingDetectionRadius && this.playerScript.fireWeapon || this.remoteAlerted))
                {
                    if (this.enemyType != 3)
                    {
                        this.remoteTriggeredTimer = (float)Random.Range(160, 200);
                        this.alertAmountTarget = Random.Range(0.75f, 1f);
                        if (!this.alertDoOnce)
                        {
                            this.alertDoOnce = true;
                            this.alertEnemiesInRange(false);
                        }
                        this.remoteAlerted = false;
                        this.idleWalkTimer = (float)Random.Range(100, 140);
                        this.targetXSpeed = 0.0f;
                        if (!this.doorSpawn && !this.rappelling && (double)this.weapon != 9.0)
                        {
                            this.dropLeftHandItemCheck();
                            this.animator.CrossFadeInFixedTime("HeardNoise", 0.25f, 0, Random.Range(0.0f, 0.3f));
                            if (this.enemySpeechHandlerScript == null || this.enemySpeechHandlerScript != null && !this.enemySpeechHandlerScript.dontInterrupt)
                            {
                                if (this.enemySpeechHandlerScript != null && this.enemySpeechHandlerScript.speaking)
                                    this.speak(this.root.GetTranslation("eHeardNoise1"), 0.9f, true);
                                else if ((double)this.root.timeSinceEnemySpokeOnHearing > 300.0 && (double)playerDistance1 < (double)this.playerShootingDetectionRadius / 1.5 && (double)Random.value > 0.349999994039536)
                                {
                                    float num = Random.value;
                                    if (this.enemiesInRangeAtStart.Length > 0)
                                    {
                                        if ((double)num > 0.75)
                                        {
                                            if (this.theme == 1)
                                                this.speak(this.root.GetTranslation("eHeardNoise2"), 1f, false);
                                            else if (this.theme == 2)
                                                this.speak(this.root.GetTranslation("eHeardNoise10"), 1f, false);
                                            else if (this.theme == 3)
                                                this.speak(this.root.GetTranslation("eHeardNoise18"), 1f, false);
                                            else if (this.theme == 4)
                                                this.speak(this.root.GetTranslation("eHeardNoise26"), 1f, false);
                                            else if (this.theme == 5)
                                                this.speak(this.root.GetTranslation("eHeardNoise34"), 1f, false);
                                        }
                                        else if ((double)num > 0.5)
                                        {
                                            if (this.theme == 1)
                                                this.speak(this.root.GetTranslation("eHeardNoise3"), 1f, false);
                                            else if (this.theme == 2)
                                                this.speak(this.root.GetTranslation("eHeardNoise11"), 1f, false);
                                            else if (this.theme == 3)
                                                this.speak(this.root.GetTranslation("eHeardNoise19"), 1f, false);
                                            else if (this.theme == 4)
                                                this.speak(this.root.GetTranslation("eHeardNoise27"), 1f, false);
                                            else if (this.theme == 5)
                                                this.speak(this.root.GetTranslation("eHeardNoise35"), 1f, false);
                                        }
                                        else if ((double)num > 0.25)
                                        {
                                            if (this.theme == 1)
                                                this.speak(this.root.GetTranslation("eHeardNoise4"), 1f, false);
                                            else if (this.theme == 2)
                                                this.speak(this.root.GetTranslation("eHeardNoise12"), 1f, false);
                                            else if (this.theme == 3)
                                                this.speak(this.root.GetTranslation("eHeardNoise20"), 1f, false);
                                            else if (this.theme == 4)
                                                this.speak(this.root.GetTranslation("eHeardNoise28"), 1f, false);
                                            else if (this.theme == 5)
                                                this.speak(this.root.GetTranslation("eHeardNoise36"), 1f, false);
                                        }
                                        else if (this.theme == 1)
                                            this.speak(this.root.GetTranslation("eHeardNoise5"), 1f, false);
                                        else if (this.theme == 2)
                                            this.speak(this.root.GetTranslation("eHeardNoise13"), 1f, false);
                                        else if (this.theme == 3)
                                            this.speak(this.root.GetTranslation("eHeardNoise21"), 1f, false);
                                        else if (this.theme == 4)
                                            this.speak(this.root.GetTranslation("eHeardNoise29"), 1f, false);
                                        else if (this.theme == 5)
                                            this.speak(this.root.GetTranslation("eHeardNoise37"), 1f, false);
                                    }
                                    else if ((double)num > 0.75)
                                    {
                                        if (this.theme == 1)
                                            this.speak(this.root.GetTranslation("eHeardNoise6"), 1f, false);
                                        else if (this.theme == 2)
                                            this.speak(this.root.GetTranslation("eHeardNoise14"), 1f, false);
                                        else if (this.theme == 3)
                                            this.speak(this.root.GetTranslation("eHeardNoise22"), 1f, false);
                                        else if (this.theme == 4)
                                            this.speak(this.root.GetTranslation("eHeardNoise30"), 1f, false);
                                        else if (this.theme == 5)
                                            this.speak(this.root.GetTranslation("eHeardNoise38"), 1f, false);
                                    }
                                    else if ((double)num > 0.5)
                                    {
                                        if (this.theme == 1)
                                            this.speak(this.root.GetTranslation("eHeardNoise7"), 1f, false);
                                        else if (this.theme == 2)
                                            this.speak(this.root.GetTranslation("eHeardNoise15"), 1f, false);
                                        else if (this.theme == 3)
                                            this.speak(this.root.GetTranslation("eHeardNoise23"), 1f, false);
                                        else if (this.theme == 4)
                                            this.speak(this.root.GetTranslation("eHeardNoise31"), 1f, false);
                                        else if (this.theme == 5)
                                            this.speak(this.root.GetTranslation("eHeardNoise39"), 1f, false);
                                    }
                                    else if ((double)num > 0.25)
                                    {
                                        if (this.theme == 1)
                                            this.speak(this.root.GetTranslation("eHeardNoise8"), 1f, false);
                                        else if (this.theme == 2)
                                            this.speak(this.root.GetTranslation("eHeardNoise16"), 1f, false);
                                        else if (this.theme == 3)
                                            this.speak(this.root.GetTranslation("eHeardNoise24"), 1f, false);
                                        else if (this.theme == 4)
                                            this.speak(this.root.GetTranslation("eHeardNoise32"), 1f, false);
                                        else if (this.theme == 5)
                                            this.speak(this.root.GetTranslation("eHeardNoise40"), 1f, false);
                                    }
                                    else if (this.theme == 1)
                                        this.speak(this.root.GetTranslation("eHeardNoise9"), 1f, false);
                                    else if (this.theme == 2)
                                        this.speak(this.root.GetTranslation("eHeardNoise17"), 1f, false);
                                    else if (this.theme == 3)
                                        this.speak(this.root.GetTranslation("eHeardNoise25"), 1f, false);
                                    else if (this.theme == 4)
                                        this.speak(this.root.GetTranslation("eHeardNoise33"), 1f, false);
                                    else if (this.theme == 5)
                                        this.speak(this.root.GetTranslation("eHeardNoise41"), 1f, false);
                                    this.root.timeSinceEnemySpokeOnHearing = 0.0f;
                                }
                            }
                        }
                    }
                    if (!this.dontFaceNoiseDirection && (double)playerDistance1 < 15.0)
                        this.mousePos = this.head.position + (this.mainPlayer.position - this.transform.position).normalized;
                }
                if (this.engageOnHearingShot && (double)this.remoteTriggeredTimer < 100.0 && (double)this.alertAmountTarget > 0.0)
                {
                    this.idle = false;
                    this.engageAnimFinished = true;
                }
            }
            if ((double)this.loadGameFireDelay > 0.0)
                this.loadGameFireDelay -= this.timescale;
            if ((double)this.electricutionTimer > 0.0)
            {
                this.animator.PlayInFixedTime("Electricuted", 0, Random.value);
                this.crouchAmount = 0.0f;
                this.visualCrouchAmount = 0.0f;
                this.targetCrouchAmount = 0.0f;
                this.aimBlendTarget = 0.0f;
                this.aimBlend = 0.0f;
                this.lookAimBlendTarget = 0.0f;
                this.lookAimBlend = 0.0f;
                this.standStillTimer = 0.0f;
                this.fightMode = false;
                this.fightPoseBlendTimer = 0.0f;
                this.knockedBackTimer = 0.0f;
                this.xSpeed = this.targetXSpeed = 0.0f;
                float num1 = this.transform.position.x + Random.Range(-0.03f, 0.03f);
                Vector3 position = this.transform.position;
                double num2 = (double)(position.x = num1);
                Vector3 vector3_1 = this.transform.position = position;
                this.ySpeed += Random.Range(0.01f, 0.04f);
                float num3 = Random.value;
                Vector3 vector3_2 = new Vector3();
                this.smokeParticle.Emit(this.root.generateEmitParams((double)num3 >= 0.150000005960464 ? ((double)num3 >= 0.300000011920929 ? ((double)num3 >= 0.449999988079071 ? ((double)num3 >= 0.600000023841858 ? ((double)num3 >= 0.75 ? ((double)num3 >= 0.300000011920929 ? this.transform.position : this.head.position) : this.lowerBack.position) : this.upperArmR.position) : this.upperArmL.position) : this.lowerArmR.position) : this.lowerArmL.position, new Vector3((float)Random.Range(-1, 1), (float)Random.Range(-1, 1), Random.Range(-0.2f, 0.2f)), 3f, 2f, new Color(1f, 1f, 1f, 0.035f)), 1);
                this.electricutionTimer -= this.timescale;
            }
            else if ((double)this.knockedBackTimer > 0.0)
            {
                this.knockedBackTimer -= this.timescale;
                this.crouchAmount = 0.0f;
                this.visualCrouchAmount = 0.0f;
                this.targetCrouchAmount = 0.0f;
                this.aimBlendTarget = 0.0f;
                this.aimBlend *= Mathf.Pow(0.7f, this.timescale);
                this.lookAimBlendTarget = 0.0f;
                this.lookAimBlend = 0.0f;
                this.standStillTimer = 0.0f;
                this.fightMode = false;
                this.fightPoseBlendTimer = 1f;
                this.fightPoseBlend = 1f;
            }
            else if (this.doorSpawn)
            {
                if (!this.doorSpawnDoOnce)
                {
                    this.autoAimTargetScript.enabled = false;
                    this.doorSpawnDoOnce = true;
                }
                this.ySpeed = 0.0f;
                if (this.attackAfterDoorSpawn)
                {
                    this.doorHideTimer -= this.timescale;
                    if ((double)this.doorHideTimer > 0.0 && !this.doneWithDoorAppearAnimationDoOnce)
                    {
                        this.transform.position = this.root.DampV3(new Vector3(this.startPos.x - 0.3f, this.startPos.y, this.startPos.z - 0.1f), this.transform.position, 0.2f);
                        this.mousePos = new Vector3(Mathf.Clamp(this.mainPlayer.position.x, this.transform.position.x - 4f, this.transform.position.x + 0.5f), this.mainPlayer.position.y + 1.5f, this.transform.position.z);
                    }
                    else
                    {
                        this.transform.position = this.root.DampV3(new Vector3(this.startPos.x - 1f, this.startPos.y, -0.2f), this.transform.position, 0.1f);
                        this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, (double)this.mousePos.x <= (double)this.transform.position.x ? 270f : 90f, 0.0f), this.playerGraphics.localRotation, 0.3f);
                        if (!this.doneWithDoorAppearAnimationDoOnce)
                        {
                            this.lookAimBlendTarget = this.aimBlendTarget = 1f;
                            this.animator.CrossFadeInFixedTime("DoorAppearLeavingDoor", 0.05f, 0, 0.0f);
                            this.doneWithDoorAppearAnimationDoOnce = true;
                        }
                    }
                }
                else
                {
                    this.targetXSpeed = -2.2f;
                    float num1 = this.transform.position.x - 0.015f * this.root.timescale;
                    Vector3 position = this.transform.position;
                    double num2 = (double)(position.x = num1);
                    Vector3 vector3 = this.transform.position = position;
                    this.mousePos = new Vector3(this.transform.position.x + (!this.startFacingRight ? -4f : 4f), this.transform.position.y + 1.5f, this.transform.position.z);
                }
                if ((double)this.transform.position.z <= (double)this.startPos.z - 1.20000004768372)
                {
                    if (!this.attackAfterDoorSpawn)
                    {
                        this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, !this.startFacingRight ? 270f : 90f, 0.0f), this.playerGraphics.localRotation, 0.03f);
                        if (this.startFacingRight)
                        {
                            float num1 = this.transform.position.x + 0.03f * this.root.timescale;
                            Vector3 position = this.transform.position;
                            double num2 = (double)(position.x = num1);
                            Vector3 vector3 = this.transform.position = position;
                        }
                    }
                    this.doorSpawnBulletCheck.SetActive(false);
                    this.allowBulletHit = true;
                    this.autoAimTargetScript.enabled = true;
                }
                else if (!this.attackAfterDoorSpawn && (double)this.doorHideTimer > 0.0)
                {
                    this.doorHideTimer = 60f;
                    this.attackAfterDoorSpawn = true;
                    this.animator.CrossFadeInFixedTime("DoorAppear1", 0.25f, 0, 0.0f);
                }
                if ((double)this.transform.position.z <= 0.0)
                {
                    ((Collider)this.GetComponent(typeof(Collider))).enabled = true;
                    Vector3 zero = Vector3.zero;
                    Quaternion rotation = this.transform.rotation;
                    Vector3 vector3_1 = rotation.eulerAngles = zero;
                    Quaternion quaternion = this.transform.rotation = rotation;
                    if (this.mainBodyKinematic)
                    {
                        this.fakeVelocity.z = 0.0f;
                    }
                    else
                    {
                        this.rBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                        int num1 = 0;
                        Vector3 velocity = this.rBody.velocity;
                        double num2 = (double)(velocity.z = (float)num1);
                        Vector3 vector3_2 = this.rBody.velocity = velocity;
                    }
                    this.targetXSpeed = !this.attackAfterDoorSpawn ? (this.xSpeed = !this.startFacingRight ? -this.walkSpeed : this.walkSpeed) : 0.0f;
                    int num3 = 0;
                    Vector3 position = this.transform.position;
                    double num4 = (double)(position.z = (float)num3);
                    Vector3 vector3_3 = this.transform.position = position;
                    this.startPos = this.transform.position;
                    this.initialAimFireDelay = (float)Random.Range(20, 40);
                    this.doorSpawn = false;
                }
            }
            else if (this.idle)
            {

                //super important colliders happen here
                if (this.hasBeenOnScreen)
                {

                    this.allowBulletHit = true;
                    this.playerCheckRayCastTimer = Mathf.Clamp(this.playerCheckRayCastTimer + this.timescale, 0.0f, 20f);
                    this.standStillTimer = 0.0f;
                    if (wasActivatedByPlayers || (double)playerDistance1 < (double)this.playerDetectionRadius && ((double)playerDistance1 < 4.0 || this.faceRight && (double)this.mainPlayer.position.x > (double)this.transform.position.x || (!this.faceRight && (double)this.mainPlayer.position.x < (double)this.transform.position.x || this.playerScript.fireWeapon && this.playerScript.weapon != 10)) && (double)this.playerCheckRayCastTimer > 15.0)
                    {
                        if (!wasActivatedByPlayers)
                            PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });

                        //setting idle to false will make the enemy aggro, also maybe set mainplayer to nearest player
                        if (wasActivatedByPlayers || !Physics.Linecast(this.head.position, this.mainPlayer.position + new Vector3(0.0f, 1f, 0.0f), (int)this.layerMaskSpotPlayer))
                            this.idle = false;
                        else if (wasActivatedByPlayers && this.playerScript.weapon == 7 && this.playerScript.kSecondaryAim && !Physics.Linecast(this.head.position, this.playerTurretGunTop.position, (int)this.layerMaskSpotPlayer))
                        {
                            this.idle = false;
                            this.aimForTurretGun = true;
                        }
                        this.playerCheckRayCastTimer = 0.0f;
                    }

                    if (!this.standStill)
                    {
                        this.idleWalkTimer -= this.timescale;
                        if ((double)this.idleWalkTimer <= 0.0)
                        {
                            this.targetXSpeed = (double)this.targetXSpeed != 0.0 ? 0.0f : (!this.faceRight ? -this.walkSpeed : this.walkSpeed);
                            this.idleWalkTimer = (float)Random.Range(120, 240);
                        }
                        if (((double)this.transform.position.x > (double)this.startPos.x + (double)this.walkRightIdleAmount - 0.5 || this.wallTouchRight) && (double)this.targetXSpeed > 0.0 || ((double)this.transform.position.x < (double)this.startPos.x - (double)this.walkLeftIdleAmount + 0.5 || this.wallTouchLeft) && (double)this.targetXSpeed < 0.0)
                        {
                            this.targetXSpeed *= -1f;
                            this.faceRight = !this.faceRight;
                            this.idleWalkTimer += 90f;
                        }
                    }
                    else
                    {
                        this.targetXSpeed = (double)this.startPos.x >= (double)this.transform.position.x - 0.5 ? ((double)this.startPos.x <= (double)this.transform.position.x + 0.5 ? 0.0f : this.walkSpeed) : -this.walkSpeed;
                        this.faceRight = this.startFacingRight;
                    }
                    if ((double)this.remoteTriggeredTimer <= 0.0)
                        this.mousePos = new Vector3(this.transform.position.x + (!this.faceRight ? -1f : 1f), this.head.transform.position.y, 0.0f);
                    if ((double)this.weapon == 9.0 && this.standStill)
                    {
                        this.targetCrouchAmount = 1f;
                        this.aimBlendTarget = this.aimBlend = 1f;
                        this.lookAimBlendTarget = this.lookAimBlend = 1f;
                        this.mousePos = new Vector3(this.transform.position.x + (float)((7.0 + (double)this.sniperAimMoveAmount * (double)Mathf.Sin(Time.time * 0.25f)) * (!this.faceRight ? -1.0 : 1.0)), (float)((double)this.transform.position.y - (double)this.sniperAimOffset + (double)this.sniperAimMoveAmount * (double)Mathf.Sin(Time.time * 0.3f)), 0.0f);
                    }
                    else
                        this.targetCrouchAmount = 0.0f;
                    if ((double)this.alertAmount < 0.100000001490116 && (double)this.remoteTriggeredTimer <= 0.0 && this.alertDoOnce)
                    {
                        if (this.idleAnim != (string)null && !this.neverReturnToIdleAnim)
                            this.animator.CrossFadeInFixedTime(this.idleAnim, 0.3f, 0);
                        this.alertDoOnce = false;
                    }
                    if (!this.idle)
                    {
                        this.fireDelay = (double)this.weapon != 9.0 ? 0.0f : (float)Random.Range(80, 100);
                        this.shootTimer = 0.0f;
                        this.shotsInARow = 0.0f;
                        if (this.engageAnimName != string.Empty)
                            this.animator.CrossFadeInFixedTime(this.engageAnimName, 0.25f, 0);
                    }
                }

            }
            else if (this.runToAlarm != null && !this.runToAlarm.hasBeenActivatedAtleastOnce && !this.runToAlarm.activated && (this.runToAlarm.currentlyEngagedEnemy == this.transform || this.runToAlarm.currentlyEngagedEnemy == null))
            {
                if (!this.runToAlarmDoOnce)
                {
                    this.speak(this.root.GetTranslation("eAlarmAlert1"), 2f, true);
                    this.runToAlarm.currentlyEngagedEnemy = this.transform;
                    this.animator.CrossFadeInFixedTime("OnGround Blend Tree", 0.25f, 0);
                    this.remoteTriggerEnemies();
                    this.runToAlarmDoOnce = true;
                }
                this.targetXSpeed = Mathf.Clamp((this.runToAlarm.transform.position.x - this.transform.position.x) * 3f, -this.walkSpeed * 4f, this.walkSpeed * 4f);
                this.mousePos = this.runToAlarm.transform.position;
                this.aimBlendTarget = this.aimBlend = 0.0f;
                if ((double)Mathf.Abs(this.runToAlarm.transform.position.x - this.transform.position.x) < 1.0)
                    this.runToAlarm.activated = true;
            }
            else if (!this.engageAnimFinished)
            {
                this.targetXSpeed = 0.0f;
                this.mousePos = this.root.DampV3(this.aimForTurretGun ? this.playerTurretGunTop.position : new Vector3(this.mainPlayer.position.x, this.mainPlayer.position.y + 1f, this.mainPlayer.position.z), this.mousePos, this.aimSpeed);
                this.targetCrouchAmount = 0.0f;
                this.aimBlendTarget = this.aimBlend = 0.0f;
                this.lookAimBlendTarget = this.lookAimBlend = 0.0f;
            }
            else
            {
                if (!this.attackModeDoOnce)
                {
                    if (!this.rappelling)
                        this.animator.CrossFadeInFixedTime("OnGround Blend Tree", 0.25f, 0);
                    this.remoteTriggerEnemies();
                    if (this.enemyType == 3)
                    {
                        this.animator.Play("Bat_TopBodyRun", 1);
                        this.initialAimComplete = true;
                        this.initialAimFireDelay = 0.0f;
                    }
                    if ((double)this.root.timeSinceEnemySpokeOnDetection > 240.0 && (double)Random.value > 0.300000011920929 || this.enemySpeechHandlerScript != null && this.enemySpeechHandlerScript.speaking)
                    {
                        if (this.theme == 1)
                        {
                            if (this.root.lastUsedEnemyDetectSpeech == 0)
                                this.speak(this.root.GetTranslation("eAlerted1"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 1)
                                this.speak(this.root.GetTranslation("eAlerted2"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 2)
                                this.speak(this.root.GetTranslation("eAlerted3"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 3)
                                this.speak(this.root.GetTranslation("eAlerted4"), 3f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 4)
                                this.speak(this.root.GetTranslation("eAlerted5"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 5)
                                this.speak(this.root.GetTranslation("eAlerted6"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 6)
                                this.speak(this.root.GetTranslation("eAlerted7"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 7)
                                this.speak(this.root.GetTranslation("eAlerted8"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 8)
                            {
                                this.speak(this.root.GetTranslation("eAlerted9"), 2.5f, true);
                            }
                            else
                            {
                                this.speak(this.root.GetTranslation("eAlerted10"), 3f, true);
                                this.root.lastUsedEnemyDetectSpeech = -1;
                            }
                        }
                        else if (this.theme == 2)
                        {
                            if (this.root.lastUsedEnemyDetectSpeech == 0)
                                this.speak(this.root.GetTranslation("eAlerted11"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 1)
                                this.speak(this.root.GetTranslation("eAlerted12"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 2)
                                this.speak(this.root.GetTranslation("eAlerted13"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 3)
                                this.speak(this.root.GetTranslation("eAlerted14"), 3f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 4)
                                this.speak(this.root.GetTranslation("eAlerted15"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 5)
                                this.speak(this.root.GetTranslation("eAlerted16"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 6)
                                this.speak(this.root.GetTranslation("eAlerted17"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 7)
                                this.speak(this.root.GetTranslation("eAlerted18"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 8)
                            {
                                this.speak(this.root.GetTranslation("eAlerted19"), 2.5f, true);
                            }
                            else
                            {
                                this.speak(this.root.GetTranslation("eAlerted20"), 3f, true);
                                this.root.lastUsedEnemyDetectSpeech = -1;
                            }
                        }
                        else if (this.theme == 3)
                        {
                            if (this.root.lastUsedEnemyDetectSpeech == 0)
                                this.speak(this.root.GetTranslation("eAlerted21"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 1)
                                this.speak(this.root.GetTranslation("eAlerted22"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 2)
                                this.speak(this.root.GetTranslation("eAlerted23"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 3)
                                this.speak(this.root.GetTranslation("eAlerted24"), 3f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 4)
                                this.speak(this.root.GetTranslation("eAlerted25"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 5)
                                this.speak(this.root.GetTranslation("eAlerted26"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 6)
                                this.speak(this.root.GetTranslation("eAlerted27"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 7)
                                this.speak(this.root.GetTranslation("eAlerted28"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 8)
                            {
                                this.speak(this.root.GetTranslation("eAlerted29"), 2.5f, true);
                            }
                            else
                            {
                                this.speak(this.root.GetTranslation("eAlerted30"), 3f, true);
                                this.root.lastUsedEnemyDetectSpeech = -1;
                            }
                        }
                        else if (this.theme == 4)
                        {
                            if (this.root.lastUsedEnemyDetectSpeech == 0)
                                this.speak(this.root.GetTranslation("eAlerted31"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 1)
                                this.speak(this.root.GetTranslation("eAlerted32"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 2)
                                this.speak(this.root.GetTranslation("eAlerted33"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 3)
                                this.speak(this.root.GetTranslation("eAlerted34"), 3f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 4)
                                this.speak(this.root.GetTranslation("eAlerted35"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 5)
                                this.speak(this.root.GetTranslation("eAlerted36"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 6)
                                this.speak(this.root.GetTranslation("eAlerted37"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 7)
                                this.speak(this.root.GetTranslation("eAlerted38"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 8)
                            {
                                this.speak(this.root.GetTranslation("eAlerted39"), 2.5f, true);
                            }
                            else
                            {
                                this.speak(this.root.GetTranslation("eAlerted40"), 3f, true);
                                this.root.lastUsedEnemyDetectSpeech = -1;
                            }
                        }
                        else if (this.theme == 5)
                        {
                            if (this.root.lastUsedEnemyDetectSpeech == 0)
                                this.speak(this.root.GetTranslation("eAlerted41"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 1)
                                this.speak(this.root.GetTranslation("eAlerted42"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 2)
                                this.speak(this.root.GetTranslation("eAlerted43"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 3)
                                this.speak(this.root.GetTranslation("eAlerted44"), 3f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 4)
                                this.speak(this.root.GetTranslation("eAlerted45"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 5)
                                this.speak(this.root.GetTranslation("eAlerted46"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 6)
                                this.speak(this.root.GetTranslation("eAlerted47"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 7)
                                this.speak(this.root.GetTranslation("eAlerted48"), 2.5f, true);
                            else if (this.root.lastUsedEnemyDetectSpeech == 8)
                            {
                                this.speak(this.root.GetTranslation("eAlerted49"), 2.5f, true);
                            }
                            else
                            {
                                this.speak(this.root.GetTranslation("eAlerted50"), 3f, true);
                                this.root.lastUsedEnemyDetectSpeech = -1;
                            }
                        }
                        ++this.root.lastUsedEnemyDetectSpeech;
                        this.root.timeSinceEnemySpokeOnDetection = 0.0f;
                    }
                    this.dropLeftHandItemCheck();
                    this.attackModeDoOnce = true;
                }
                if (this.enemyType == 3)
                {
                    this.aimBlendTarget = this.aimBlend = 0.0f;
                    this.fightPoseBlendTimer = 60f;
                    this.fightPoseBlend = 1f;
                    this.bulletHitLayerWeight *= Mathf.Pow(0.95f, this.timescale);
                    this.animator.SetLayerWeight(2, this.bulletHitLayerWeight / 3f);
                }
                this.root.musicIntenseFactor = Mathf.Clamp01(this.root.musicIntenseFactor + 0.01f);
                this.allowBulletHit = true;
                this.playerCheckRayCastTimer += this.timescale;
                if ((double)this.targetXSpeed == 0.0 || (double)this.xSpeed == 0.0)
                    this.standStillTimer += !this.pushedAgainstWall ? this.timescale : this.timescale * 1.5f;
                else
                    this.standStillTimer -= this.timescale * 2f;
                this.standStillTimer = Mathf.Clamp(this.standStillTimer, 0.0f, this.timeRequiredBeforeCrouch + 240f);
                if ((double)this.standStillTimer >= (double)this.timeRequiredBeforeCrouch + 240.0)
                    this.standStillTimer = 0.0f;
                if (!this.havePushedAgainstWall && this.pushedAgainstWall)
                {
                    this.standStillTimer = this.timeRequiredBeforeCrouch - 30f;
                    this.havePushedAgainstWall = true;
                }
                this.targetCrouchAmount = (double)this.standStillTimer < (double)this.timeRequiredBeforeCrouch / (double)this.root.difficulty || (double)this.shotsInARow <= (double)this.root.difficulty || this.bouncedOnBouncePad ? 0.0f : 1f;
                if ((double)this.playerCheckRayCastTimer > 40.0)
                {
                    if (!Physics.Linecast(this.head.position, this.mainPlayer.position + new Vector3(0.0f, 1f, 0.0f), (int)this.layerMaskSpotPlayer))
                    {
                        this.framesSincePlayerLastSeen = 0.0f;
                        this.aimForTurretGun = false;
                    }
                    else if (this.playerScript.weapon == 7 && this.playerScript.kSecondaryAim && !Physics.Linecast(this.head.position, this.playerTurretGunTop.position, (int)this.layerMaskSpotPlayer))
                    {
                        this.framesSincePlayerLastSeen = 0.0f;
                        this.aimForTurretGun = true;
                    }
                    this.playerCheckRayCastTimer = 0.0f;
                }
                if ((double)this.framesSincePlayerLastSeen < 1000.0)
                    this.framesSincePlayerLastSeen += this.timescale;
                if ((double)this.framesSincePlayerLastSeen < 80.0)
                {
                    if ((double)this.weapon == 9.0)
                    {
                        float num1 = Mathf.Clamp(this.fireDelay * 10f - 300f, 0.0f, this.fireDelay) * ((float)System.Math.PI / 180f);
                        float num2 = Mathf.Clamp01(this.playerDistance * 0.5f - 2f);
                        this.mousePos = this.root.DampV3(this.mainPlayer.position + new Vector3((float)((double)Mathf.Cos(num1 - 90f) * (double)this.fireDelay * 0.0399999991059303) * num2, (float)((double)Mathf.Sin(num1 - 90f) * (double)this.fireDelay * 0.0399999991059303) * num2 + 1f, 0.0f), this.mousePos, 0.2f);
                        if (!this.pushedAgainstWall && this.standStillInHuntMode)
                            this.targetCrouchAmount = 1f;
                    }
                    else if (!this.initialAimComplete || (double)this.initialAimFireDelay > 0.0 || ((double)this.shotsInARow >= (double)this.weaponShotsInRow || (double)Vector3.Distance(this.mousePos, this.transform.position) < 6.5))
                        this.mousePos = this.root.DampV3(this.aimForTurretGun ? this.playerTurretGunTop.position : new Vector3(this.mainPlayer.position.x, this.mainPlayer.position.y + 1f, this.mainPlayer.position.z), this.mousePos, this.aimSpeed);
                    if (!this.initialAimComplete && (double)Vector3.Distance(this.mainPlayer.position, this.mousePos) < 2.0)
                        this.initialAimComplete = true;
                    float playerDistance2 = this.playerDistance;
                    float num3 = Mathf.Abs(this.mainPlayer.position.x - this.transform.position.x);
                    if ((double)playerDistance2 < 4.0 && (double)num3 < 2.5)
                    {
                        this.standStillTimer = 0.0f;
                        if (!this.disableWeaponPickup)
                            this.root.doMeleeHint = true;
                    }
                    if (!this.standStillInHuntMode)
                    {
                        if (this.enemyType == 3)
                        {
                            this.targetXSpeed = Mathf.Clamp((this.mainPlayer.position.x + (!this.faceRight ? 1.5f : -1.5f) - this.transform.position.x) * 5f, -this.walkSpeed, this.walkSpeed);
                        }
                        else
                        {
                            if ((double)playerDistance2 < 4.0 && (double)num3 < 2.5 && !this.playerScript.onGround)
                                this.targetXSpeed = 0.0f;
                            else if ((double)this.health >= 1.0)
                            {
                                float num1 = 1f - Mathf.Clamp(Mathf.Abs(this.mainPlayer.position.y - (this.transform.position.y - this.legLength)) / 8f, 0.0f, 1f);
                                this.targetXSpeed = (double)this.mainPlayer.position.x >= (double)this.transform.position.x ? this.walkSpeed : -this.walkSpeed;
                                if ((double)num3 > 1.0 && (double)num3 < 1.0 + 4.0 * (double)num1)
                                    this.targetXSpeed = (float)(-(double)this.targetXSpeed * (this.enemyType != 1 ? 1.0 : 1.5));
                                else if ((double)num3 < 1.0 + 7.0 * (double)num1)
                                    this.targetXSpeed = 0.0f;
                            }
                            else
                                this.targetXSpeed = (double)this.mainPlayer.position.x >= (double)this.transform.position.x ? (float)(-(double)this.walkSpeed * 1.5) : this.walkSpeed * 1.5f;
                            if (this.pushedAgainstWall && this.kCrouch)
                                this.targetXSpeed = 0.0f;
                        }
                    }
                    else
                        this.targetXSpeed = 0.0f;
                    if (this.initialAimComplete)
                    {
                        if ((double)this.initialAimFireDelay > 0.0)
                            this.initialAimFireDelay -= this.timescale * (this.root.difficulty + (float)((double)this.root.difficulty * 1.5 - 1.5));
                        this.shootTimer += this.timescale * this.root.difficulty;
                    }
                    if (this.initialAimComplete && (double)this.loadGameFireDelay <= 0.0 && (double)this.initialAimFireDelay <= 0.0 && (((double)this.playerScript.dodgingCoolDown <= (double)this.root.difficulty * 1.5 - 30.0 + (double)Random.Range(0, 10) || this.playerScript.dodging) && (double)playerDistance2 < 50.0) && ((double)this.shotsInARow < (double)this.weaponShotsInRow * (double)this.root.difficulty && (double)this.fireDelay <= 0.0 || (double)this.shootTimer >= (double)this.timeInbetweenShots))
                    {
                        this.shootTimer = 0.0f;
                        this.shotsInARow = (double)this.shotsInARow < (double)this.weaponShotsInRow * (double)this.root.difficulty ? this.shotsInARow + 1f : Random.Range(1f, 3f * this.root.difficulty);
                        this.kFire = true;
                    }
                    if (this.kFire && (double)this.fireDelay <= 0.0)
                    {
                        if (!this.root.pleaseESRB && ((double)this.playerScript.rotationSpeed == 0.0 && ((double)this.playerScript.dodgingCoolDown <= 0.0 || this.root.difficultyMode == 2 && !this.root.isTutorialLevel) && ((double)this.shotsInARow + (this.root.difficultyMode != 1 ? 0.0 : 1.0) >= (double)this.weaponShotsInRow || this.root.difficultyMode == 2 && !this.root.isTutorialLevel) || ((double)this.weapon == 9.0 || (double)this.weapon == 10.0)))
                        {
                            float num1 = new Vector2(this.playerScript.xSpeed, this.playerScript.ySpeed).magnitude / 4f;
                            this.aimAngleOffset = Random.Range(num1 - 2f, 2f + num1);
                        }
                        else
                            this.aimAngleOffset = (float)((double)Random.Range(20, 25) * (double)Mathf.Clamp(1f - Mathf.Clamp01(this.playerDistance / 25f), 0.25f, 1f) * ((double)this.mainPlayerRigidBody.velocity.y >= 0.0 ? ((double)this.mainPlayerRigidBody.velocity.y != 0.0 ? -1.0 : ((double)Random.value <= 0.5 ? -1.0 : 1.0)) : 1.0));
                    }
                }
                else
                {
                    this.initialAimComplete = false;
                    this.shootTimer = 0.0f;
                    this.shotsInARow = 2f;
                    if ((double)this.framesSincePlayerLastSeen > 180.0)
                        this.targetXSpeed = 0.0f;
                    else if ((double)this.targetXSpeed == 0.0 && !this.standStillInHuntMode)
                        this.targetXSpeed = (double)this.mousePos.x >= (double)this.transform.position.x ? ((double)this.mousePos.x <= (double)this.transform.position.x ? 0.0f : 4f) : -4f;
                }
                if ((double)this.targetXSpeed < 0.0 && (double)this.transform.position.x < (double)this.startPos.x - (double)this.walkLeftAbsoluteAmount + 0.5 || (double)this.targetXSpeed > 0.0 && (double)this.transform.position.x > (double)this.startPos.x + (double)this.walkRightAbsoluteAmount - 0.5)
                    this.targetXSpeed = 0.0f;
                if ((double)this.framesSincePlayerLastSeen > 500.0 && !this.neverReturnToIdle)
                {
                    this.idle = true;
                    this.framesSincePlayerLastSeen = 0.0f;
                }
                this.alertAmount = this.root.Damp(1f, this.alertAmount, 0.3f);
            }
            if (!this.idle || this.doorSpawn)
                this.root.enemyEngagedWithPlayer = true;
            if (!this.doorSpawn)
            {
                if (this.doorSpawnDoOnce)
                {
                    this.doorSpawnBulletCheck.SetActive(false);
                    this.doorSpawnDoOnce = false;
                }
                float target = this.startPos.z;
                if (this.enemiesInRangeAtStart.Length > 0)
                {
                    int index = 0;
                    EnemyScript[] enemiesInRangeAtStart = this.enemiesInRangeAtStart;
                    for (int length = enemiesInRangeAtStart.Length; index < length; ++index)
                    {
                        if (enemiesInRangeAtStart[index] != null && (double)Vector2.Distance((Vector2)enemiesInRangeAtStart[index].transform.position, (Vector2)this.transform.position) < 2.0 && (enemiesInRangeAtStart[index] != null && enemiesInRangeAtStart[index].enabled) && (double)enemiesInRangeAtStart[index].health > 0.0)
                            target = this.gameObject.GetInstanceID() <= enemiesInRangeAtStart[index].gameObject.GetInstanceID() ? this.startPos.z - 0.75f : this.startPos.z + 0.75f;
                    }
                }
                float num1 = this.root.Damp(target, this.transform.position.z, 0.05f);
                Vector3 position = this.transform.position;
                double num2 = (double)(position.z = num1);
                Vector3 vector3 = this.transform.position = position;
            }
            this.remoteTriggeredTimer = Mathf.Clamp(this.remoteTriggeredTimer - this.timescale, 0.0f, this.remoteTriggeredTimer);
            if (!this.attackModeDoOnce)
            {
                if ((double)this.remoteTriggeredTimer > 0.0)
                {
                    if ((double)this.weapon != 9.0 && (double)this.remoteTriggeredTimer > 100.0 && ((double)this.remoteTriggeredTimer < 120.0 && this.hasBeenRemoteTriggered))
                    {
                        this.mousePos = this.head.position + (this.mainPlayer.position - this.transform.position).normalized;
                        if (this.alwaysEngageOnRemoteTrigger)
                            this.idle = false;
                    }
                    this.alertAmount = this.hasBeenRemoteTriggered ? 1f : ((double)this.remoteTriggeredTimer <= 80.0 ? this.root.Damp(this.alertAmountTarget, this.alertAmount, 0.2f) : this.root.Damp(1f, this.alertAmount, 0.2f));
                    if (this.idleAnim != (string)null && (this.engageAnimName == (string)null || this.engageAnimName == string.Empty || this.engageAnimName != (string)null && this.engageAnimFinished) && (double)this.alertAmount > 0.100000001490116 && this.animator.GetCurrentAnimatorStateInfo(0).IsName(this.idleAnim))
                        this.animator.CrossFadeInFixedTime("OnGround Blend Tree", 0.25f, 0);
                }
                else
                    this.alertAmount = (double)this.alertAmount <= (double)this.alertAmountTarget - 0.200000002980232 ? this.root.Damp(0.0f, this.alertAmount, 0.05f) : Mathf.Clamp01(this.alertAmount - this.timescale * 0.0005f * this.alertAmountTarget);
            }
            if (this.pushedAgainstWall && this.kFire && this.kCrouch)
            {
                --this.shotsInARow;
                this.kFire = false;
            }
            this.animator.SetFloat("AlertAmount", this.alertAmount);
            if (this.cantBeHarmed)
                this.bulletHit = false;
            if (this.bulletHit && !this.cantBeHarmed)
            {
                if (this.alwaysGetBulletHitHeadshot)
                    this.bulletHitName = "Head";
                else if (this.bulletHitName == "sCol")
                    this.bulletHitName = (double)this.bulletHitPos.y <= (double)this.head.position.y ? ((double)this.bulletHitPos.y <= (double)this.upperBack.position.y ? ((double)this.bulletHitPos.y >= (double)this.upperLegL.position.y ? "Center" : "UpperLeg_L") : "UpperBack") : "Head";
                this.root.changeDifficulty(Mathf.Clamp(this.bulletStrength, 0.0f, 0.5f) * 0.025f);
                if (this.useHat != null && this.bulletHitName == "Head")
                {
                    this.useHat.gameObject.layer = 22;
                    this.useHat.parent = (Transform)null;
                    BoxCollider component1 = (BoxCollider)this.useHat.GetComponent(typeof(BoxCollider));
                    component1.enabled = true;
                    Physics.IgnoreCollision((Collider)this.head.GetComponent(typeof(SphereCollider)), (Collider)component1);
                    Rigidbody component2 = (Rigidbody)this.useHat.GetComponent(typeof(Rigidbody));
                    component2.isKinematic = false;
                    component2.AddForce(new Vector3(Mathf.Clamp(this.bulletHitVel.x * Random.Range(0.5f, 0.8f), -3f, 3f), 0.0f, 0.0f) + Vector3.up * (float)Random.Range(3, 5), ForceMode.VelocityChange);
                    component2.angularVelocity = new Vector3(0.0f, 0.0f, -this.bulletHitVel.x);
                    this.useHat = (Transform)null;
                }
                string lhs = (string)null;
                float num1 = new float();
                if (this.bulletHitText != (string)null && this.bulletHitText != string.Empty)
                    lhs = this.bulletHitText;
                if ((double)this.bulletHitExtraScore != 0.0)
                    num1 = this.bulletHitExtraScore;
                string name;
                float amount;
                if (this.bulletHitName == "Head")
                {
                    name = RuntimeServices.op_Addition(lhs, this.root.GetTranslation("bul13"));
                    amount = num1 + 70f;
                }
                else
                {
                    name = RuntimeServices.op_Addition(lhs, this.root.GetTranslation("bul14"));
                    amount = num1 + 5f;
                }
                if (!this.dontGiveScore)
                {
                    this.root.cCheckGiSc = true;
                    this.root.giveScore(amount, name, false);
                }
                if (this.enemyType != 3 && this.enemyType != 2 && (this.bulletKillOnHeadshot && this.bulletHitName == "Head") && (double)this.bulletTimeAlive != 999.0)
                {
                    this.health -= this.calculateActualBulletHitStrength();
                    if (this.rootShared.modOneShotEnemies)
                        this.health = Mathf.Clamp(this.health, -999999f, 0.0f);
                    if ((double)this.health > 0.0)
                    {
                        if (this.enemyType == 1)
                            this.shootTimer = this.timeInbetweenShots / 2f;
                        this.knockBack((double)this.bulletHitVel.x > 0.0, 20f);
                    }
                }
                else
                {
                    this.health -= this.calculateActualBulletHitStrength();
                    if (this.rootShared.modOneShotEnemies)
                        this.health = Mathf.Clamp(this.health, -999999f, 0.0f);
                    if ((double)this.health > 0.0)
                    {
                        if (this.bulletHitName == "UpperBack" || this.bulletHitName == "Head")
                        {
                            if ((double)this.bulletHitPos.z < 0.0)
                            {
                                this.animator.CrossFade("BulletHitShoulderL", 0.1f, 2, 0.0f);
                                this.animator.CrossFade("BulletHitShoulderL", 0.1f, 3, 0.0f);
                            }
                            else
                            {
                                this.animator.CrossFade("BulletHitShoulderR", 0.1f, 2, 0.0f);
                                this.animator.CrossFade("BulletHitShoulderR", 0.1f, 3, 0.0f);
                            }
                            this.bulletHitLayerWeight = 1f;
                            if (this.enemyType != 3)
                                this.animator.SetLayerWeight(2, this.bulletHitLayerWeight);
                            this.bleedFromHand = true;
                            if (this.enemyType == 1 && (double)this.health > 0.0)
                            {
                                this.shootTimer = this.timeInbetweenShots / 2f;
                                if ((double)this.health < 1.0)
                                    this.knockBack((double)this.bulletHitVel.x > 0.0, 10f);
                            }
                        }
                        else if (this.bulletHitName == "Center" || this.bulletHitName == "LowerBack")
                        {
                            this.animator.CrossFade("BulletHitStomach", 0.1f, 2, 0.0f);
                            this.animator.CrossFade("BulletHitStomach", 0.1f, 3, 0.0f);
                            this.bulletHitLayerWeight = 1f;
                            if (this.enemyType != 3)
                                this.animator.SetLayerWeight(2, this.bulletHitLayerWeight);
                            this.bleedFromHand = true;
                            if (this.enemyType == 1 && (double)this.health > 0.0)
                            {
                                this.shootTimer = this.timeInbetweenShots / 2f;
                                if ((double)this.health < 1.0)
                                    this.knockBack((double)this.bulletHitVel.x > 0.0, 10f);
                            }
                        }
                        else if (this.bulletHitName == "UpperLeg_L" || this.bulletHitName == "LowerLeg_L" || (this.bulletHitName == "UpperLeg_R" || this.bulletHitName == "LowerLeg_R"))
                        {
                            if ((double)this.bulletHitPos.z < 0.0)
                                this.animator.CrossFade("BulletHitLegL", 0.2f, 4, 0.0f);
                            else
                                this.animator.CrossFade("BulletHitLegR", 0.2f, 4, 0.0f);
                            this.shotInLeg = true;
                            this.bleedFromHand = false;
                        }
                    }
                }
                bool flag1 = false;
                if ((double)this.health <= 0.0)
                {
                    PacketSender.BaseNetworkedEntityRPC("OnEnemyDeath", networkHelper.entityIdentifier);

                    if (this.enemyDiedFailSafe)
                    {
                        ((Behaviour)this.gameObject.GetComponent(typeof(EnemyScript))).enabled = false;
                        this.gameObject.SetActive(false);
                    }
                    else
                    {
                        this.enemyDiedFailSafe = true;
                        this.root.enemyEngagedWithPlayer = true;
                        this.speak(string.Empty, 1f, false);
                        this.alertEnemiesInRange(true);
                        this.dropLeftHandItemCheck();
                        if (!this.simpleEnemy && this.headAudio.isActiveAndEnabled)
                        {
                            this.headAudio.loop = false;
                            this.headAudio.clip = this.deathScreams[Random.Range(0, Extensions.get_length((System.Array)this.deathScreams))];
                            this.headAudio.volume = Random.Range(0.8f, 1f);
                            this.headAudio.pitch = Random.Range(0.95f, 1.05f) + 0.5f;
                            this.headAudio.time = 0.0f;
                            this.headAudio.Play();
                        }
                        if (this.bulletHitName == "Explosion")
                        {
                            this.bulletKillText = this.root.GetTranslation("bul21");
                            this.bulletHitText = RuntimeServices.op_Addition(this.root.GetTranslation("bul22"), "-");
                            this.bulletHitExtraScore = 200f;
                        }
                        this.root.doBloodScoreSplat(this.bulletHitPos, this.bulletHitVel.normalized, this.bulletKillText);
                        if (!this.dontGiveScore)
                        {
                            this.root.cCheckGiSc = true;
                            this.root.giveScore(500f + this.bulletHitExtraScore, RuntimeServices.op_Addition(this.bulletHitText, this.root.GetTranslation("bul12")), true);
                        }
                        if (!this.root.dead)
                        {
                            Vector2 viewportPoint = (Vector2)this.curCamera.WorldToViewportPoint(this.transform.position);
                            int num2 = !this.rootShared.modCinematicCamera ? 1 : 0;
                            if (num2 == 0)
                            {
                                int num3 = this.rootShared.modCinematicCamera ? 1 : 0;
                                if (num3 != 0)
                                    num3 = this.root.kAction ? 1 : 0;
                                num2 = num3 == 0 ? 1 : 0;
                            }
                            if (num2 != 0)
                                num2 = (double)viewportPoint.x > 0.25 ? 1 : 0;
                            if (num2 != 0)
                                num2 = (double)viewportPoint.x < 0.75 ? 1 : 0;
                            if (num2 != 0)
                                num2 = (double)viewportPoint.y > 0.200000002980232 ? 1 : 0;
                            if (num2 != 0)
                                num2 = (double)viewportPoint.y < 0.800000011920929 ? 1 : 0;
                            bool doCrop = num2 != 0;
                            ((Record)this.curCamera.GetComponent(typeof(Record))).captureMoment((float)((double)amount + ((double)this.root.timeSinceScoreLastGiven >= 100.0 ? 0.0 : (double)this.root.lastEnemyKillScore) + (double)this.root.tempScore * 9.99999974737875E-05 + (!this.root.kAction ? 0.0 : 50.0 + (double)Mathf.Abs(this.playerScript.rotationSpeed * 10f))), doCrop);
                            this.root.hasCapturedMoment = true;
                        }
                        this.root.lastEnemyKillScore = amount;
                        if (!this.root.doGore && !this.root.trailerMode)
                            this.enemyDeathBlinkScript.isEnabled = true;
                        ++this.root.nrOfEnemiesKilled;
                        this.root.changeDifficulty(0.04f);
                        ++this.statsTracker.enemiesKilled;
                        this.statsTracker.achievementCheck();
                        ++this.root.killsSinceMedkitDrop;
                        Random.InitState((int)this.startPos.magnitude);
                        if (!this.disableWeaponPickup && this.root.killsSinceMedkitDrop > 7 && ((double)Random.value > 0.300000011920929 && (double)this.playerScript.health <= 0.660000026226044))
                        {
                            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.medkit, this.transform.position, Quaternion.Euler((float)Random.Range(-60, -120), 270f, -90f));
                            Rigidbody component = (Rigidbody)gameObject.GetComponent(typeof(Rigidbody));
                            component.isKinematic = false;
                            component.velocity = this.bulletHitVel * 0.1f + Vector3.up * 8f;
                            component.angularVelocity = new Vector3((float)Random.Range(-1, 1), (float)Random.Range(-1, 1), (float)Random.Range(-1, 1));
                            if (this.motorcycle != null)
                                ((WeaponPickupScript)gameObject.GetComponent(typeof(WeaponPickupScript))).motorcycle = true;
                            this.root.killsSinceMedkitDrop = 0;
                        }
                        if (this.runToAlarm != null && this.runToAlarm.currentlyEngagedEnemy == this.transform)
                            this.runToAlarm.currentlyEngagedEnemy = (Transform)null;
                        if (this.rappelling && this.unhingeOnDeath)
                        {
                            this.rappelling = false;
                            this.rappellStringScript.weightedString = false;
                        }
                        if (this.motorcycle != null)
                        {
                            if (!((MotorcycleScript)this.motorcycle.GetComponent(typeof(MotorcycleScript))).isCar)
                            {
                                this.allowGib = true;
                                this.bulletTimeAlive = 0.0f;
                                this.bulletHitName = "Explosion";
                                this.bulletHitVel = new Vector3(-20f, 30f, 0.0f);
                            }
                            else
                                this.bulletHitVel = new Vector3(-100f, 60f, -5f);
                        }
                        int index1 = 0;
                        Collider[] collidersInChildren = this.collidersInChildren;
                        for (int length = collidersInChildren.Length; index1 < length; ++index1)
                        {
                            if (collidersInChildren[index1] != null && collidersInChildren[index1].gameObject.layer != 17 && (!(collidersInChildren[index1].transform == this.useHat) && this.disableWeaponPickUpDoThing(collidersInChildren[index1].transform)))
                            {
                                collidersInChildren[index1].isTrigger = false;
                                collidersInChildren[index1].enabled = true;
                            }
                        }
                        if (!this.dontRagDoll)
                        {
                            bool flag2 = new bool();
                            int index2 = 0;
                            Rigidbody[] bodiesInChildren = this.rigidBodiesInChildren;
                            for (int length = bodiesInChildren.Length; index2 < length; ++index2)
                            {
                                if (bodiesInChildren[index2] != null && (!this.mainBodyKinematic || bodiesInChildren[index2].transform != this.transform) && (bodiesInChildren[index2].gameObject.layer != 17 && bodiesInChildren[index2].gameObject.layer != 14 && (bodiesInChildren[index2].transform != this.useHat && this.disableWeaponPickUpDoThing(bodiesInChildren[index2].transform))))
                                {
                                    bodiesInChildren[index2].isKinematic = false;
                                    bodiesInChildren[index2].gameObject.layer = 12;
                                    RigidBodySlowMotion rigidBodySlowMotion = (RigidBodySlowMotion)bodiesInChildren[index2].gameObject.GetComponent(typeof(RigidBodySlowMotion));
                                    if (rigidBodySlowMotion == null)
                                    {
                                        rigidBodySlowMotion = (RigidBodySlowMotion)bodiesInChildren[index2].gameObject.AddComponent(typeof(RigidBodySlowMotion));
                                        flag2 = true;
                                    }
                                    else
                                        rigidBodySlowMotion.enabled = true;
                                    if (!this.skyfall)
                                    {
                                        rigidBodySlowMotion.limitVel = true;
                                        rigidBodySlowMotion.isRagdoll = true;
                                    }
                                    if (this.motorcycle != null || this.stickToTransform != null)
                                        rigidBodySlowMotion.motorcycle = true;
                                    if (bodiesInChildren[index2].gameObject.name == "pistol" || bodiesInChildren[index2].gameObject.name == "uzi" || (bodiesInChildren[index2].gameObject.name == "machinegun" || bodiesInChildren[index2].gameObject.name == "shotgun"))
                                        bodiesInChildren[index2].velocity = this.bulletHitVel * 0.1f + Vector3.up * 4f;
                                    else if (bodiesInChildren[index2].gameObject.name != "UpperLeg_L" && bodiesInChildren[index2].gameObject.name != "UpperLeg_R" && (bodiesInChildren[index2].gameObject.name != "LowerLeg_L" && bodiesInChildren[index2].gameObject.name != "LowerLeg_R"))
                                        bodiesInChildren[index2].velocity = !(bodiesInChildren[index2].gameObject.name == "LowerArm_R") || (double)Random.value <= 0.699999988079071 ? (!(bodiesInChildren[index2].gameObject.name == "LowerArm_L") || (double)Random.value <= 0.699999988079071 ? (!(bodiesInChildren[index2].gameObject.name == this.bulletHitName) ? new Vector3(this.bulletHitVel.x, (double)this.bulletHitVel.y >= 0.0 ? this.bulletHitVel.y : 0.0f, this.bulletHitVel.z + Random.Range(-1.5f, 1.5f)) * 0.75f : new Vector3(this.bulletHitVel.x, (float)(((double)this.bulletHitVel.y >= 1.0 ? (double)this.bulletHitVel.y : 1.0) * 2.0), this.bulletHitVel.z) * 1.6f) : new Vector3(this.bulletHitVel.x * 2f, this.bulletHitVel.y, this.bulletHitVel.z)) : new Vector3(this.bulletHitVel.x * -1f, this.bulletHitVel.y, this.bulletHitVel.z);
                                    if (this.skyfall)
                                    {
                                        float num2 = bodiesInChildren[index2].velocity.y + 15f;
                                        Vector3 velocity = bodiesInChildren[index2].velocity;
                                        double num3 = (double)(velocity.y = num2);
                                        Vector3 vector3 = bodiesInChildren[index2].velocity = velocity;
                                    }
                                }
                            }
                            if (flag2)
                                this.enemyOptimizerScript.getRigidBodySlowMotionComponents();
                        }
                        if (!this.disableWeaponPickup)
                        {
                            WeaponPickupScript weaponPickupScript1 = (WeaponPickupScript)null;
                            WeaponPickupScript weaponPickupScript2 = (WeaponPickupScript)null;
                            if ((double)this.weapon == 1.0 || (double)this.weapon == 2.0)
                            {
                                this.pistolR.gameObject.layer = 22;
                                this.pistolR.transform.parent = (Transform)null;
                                weaponPickupScript1 = (WeaponPickupScript)this.pistolR.GetComponent(typeof(WeaponPickupScript));
                                if ((double)this.weapon == 2.0)
                                {
                                    this.pistolL.gameObject.layer = 22;
                                    this.pistolL.transform.parent = (Transform)null;
                                    weaponPickupScript2 = (WeaponPickupScript)this.pistolL.GetComponent(typeof(WeaponPickupScript));
                                }
                            }
                            else if ((double)this.weapon == 3.0 || (double)this.weapon == 4.0)
                            {
                                this.uziR.gameObject.layer = 22;
                                this.uziR.transform.parent = (Transform)null;
                                weaponPickupScript1 = (WeaponPickupScript)this.uziR.GetComponent(typeof(WeaponPickupScript));
                                if ((double)this.weapon == 4.0)
                                {
                                    this.uziL.gameObject.layer = 22;
                                    this.uziL.transform.parent = (Transform)null;
                                    weaponPickupScript2 = (WeaponPickupScript)this.uziL.GetComponent(typeof(WeaponPickupScript));
                                }
                            }
                            else if ((double)this.weapon == 5.0)
                            {
                                this.machineGun.gameObject.layer = 22;
                                this.machineGun.transform.parent = (Transform)null;
                                weaponPickupScript1 = (WeaponPickupScript)this.machineGun.GetComponent(typeof(WeaponPickupScript));
                            }
                            else if ((double)this.weapon == 6.0)
                            {
                                this.shotgun.gameObject.layer = 22;
                                this.shotgun.transform.parent = (Transform)null;
                                weaponPickupScript1 = (WeaponPickupScript)this.shotgun.GetComponent(typeof(WeaponPickupScript));
                            }
                            else if ((double)this.weapon == 9.0)
                            {
                                this.sniper.gameObject.layer = 22;
                                this.sniper.transform.parent = (Transform)null;
                                weaponPickupScript1 = (WeaponPickupScript)this.sniper.GetComponent(typeof(WeaponPickupScript));
                            }
                            else if ((double)this.weapon == 10.0)
                            {
                                this.crossbow.gameObject.layer = 22;
                                this.crossbow.transform.parent = (Transform)null;
                                weaponPickupScript1 = (WeaponPickupScript)this.crossbow.GetComponent(typeof(WeaponPickupScript));
                            }
                            if ((double)this.weapon != 0.0)
                            {
                                weaponPickupScript1.enabled = true;
                                weaponPickupScript1.weapon = (double)this.weapon != 2.0 ? ((double)this.weapon != 4.0 ? this.weapon : 3f) : 1f;
                                weaponPickupScript1.doSetup();
                            }
                            if ((double)this.weapon == 2.0 || (double)this.weapon == 4.0)
                            {
                                weaponPickupScript2.enabled = true;
                                weaponPickupScript2.weapon = (double)this.weapon != 2.0 ? ((double)this.weapon != 4.0 ? this.weapon : 3f) : 1f;
                                weaponPickupScript2.doSetup();
                            }
                            if (this.motorcycle != null)
                            {
                                weaponPickupScript1.motorcycle = true;
                                if (weaponPickupScript2 != null)
                                    weaponPickupScript2.motorcycle = true;
                            }
                        }
                        if (this.mainBodyKinematic)
                            this.fakeVelocity = Vector3.zero;
                        else
                            this.rBody.velocity = Vector3.zero;
                        ((Collider)this.gameObject.GetComponent(typeof(CapsuleCollider))).enabled = false;
                        ((Behaviour)this.transform.Find("EnemyGraphics").GetComponent(typeof(Animator))).enabled = false;
                        flag1 = true;
                        if (this.root.doGore && this.allowGib && (double)this.bulletTimeAlive <= 9.0)
                        {
                            GameObject gameObject1 = (GameObject)null;
                            GameObject gameObject2 = (GameObject)null;
                            GameObject gameObject3 = (GameObject)null;
                            GameObject gameObject4 = (GameObject)null;
                            GameObject gameObject5 = (GameObject)null;
                            GameObject gameObject6 = (GameObject)null;
                            GameObject gameObject7 = (GameObject)null;
                            GameObject gameObject8 = (GameObject)null;
                            GameObject gameObject9 = (GameObject)null;
                            GameObject gameObject10 = (GameObject)null;
                            this.bulletHitVel *= 1.4f;
                            if (this.bulletHitName == "Explosion" || this.bulletHitName == "Center" || (this.bulletHitName == "LowerBack" || this.bulletHitName == "UpperBack"))
                            {
                                flag1 = false;
                                GameObject gameObject11 = UnityEngine.Object.Instantiate<GameObject>(this.gibLowerBody, this.lowerBack.position, this.lowerBack.rotation);
                                gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.gibUpperBody, this.upperBack.position, this.upperBack.rotation);
                                gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.gibUpperArm, this.upperArmL.position, this.upperArmL.rotation);
                                gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.gibLowerArm, this.lowerArmL.position, this.lowerArmL.rotation);
                                gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.gibUpperArm, this.upperArmR.position, this.upperArmR.rotation);
                                gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.gibLowerArm, this.lowerArmR.position, this.lowerArmR.rotation);
                                gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.gibHead, this.head.position, this.head.rotation);
                                gameObject11.layer = 14;
                                gameObject1.layer = 14;
                                gameObject4.layer = 14;
                                gameObject5.layer = 14;
                                gameObject2.layer = 14;
                                gameObject3.layer = 14;
                                gameObject6.layer = 14;
                                gameObject11.tag = "Gib";
                                gameObject1.tag = "Gib";
                                gameObject4.tag = "Gib";
                                gameObject5.tag = "Gib";
                                gameObject2.tag = "Gib";
                                gameObject3.tag = "Gib";
                                gameObject6.tag = "Gib";
                                gameObject11.transform.parent = this.center;
                                ((Rigidbody)gameObject1.GetComponent(typeof(Rigidbody))).velocity = (new Vector3(Random.Range(-2f, 1f), Random.Range(-4f, 2f), 0.0f) + this.bulletHitVel) * 0.5f;
                                ((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody))).velocity = (new Vector3(Random.Range(-2f, 1f), Random.Range(-4f, 2f), 0.0f) + this.bulletHitVel) * 0.5f;
                                ((Rigidbody)gameObject4.GetComponent(typeof(Rigidbody))).velocity = (new Vector3(Random.Range(-2f, 1f), Random.Range(-4f, 2f), 0.0f) + this.bulletHitVel) * 0.5f;
                                ((Rigidbody)gameObject3.GetComponent(typeof(Rigidbody))).velocity = (new Vector3(Random.Range(-2f, 1f), Random.Range(-4f, 2f), 0.0f) + this.bulletHitVel) * 0.5f;
                                ((Rigidbody)gameObject5.GetComponent(typeof(Rigidbody))).velocity = new Vector3(Random.Range(-0.5f, 0.5f), (float)Random.Range(6, 8), 0.0f) + this.bulletHitVel * 0.2f;
                                ((Rigidbody)gameObject6.GetComponent(typeof(Rigidbody))).velocity = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(8f, 9f), 0.0f) + this.bulletHitVel * 0.15f;
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.upperArmR.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f) * 0.4f, (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.lowerArmR.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f) * 0.4f, (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.upperArmL.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f) * 0.4f, (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.lowerArmL.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f) * 0.4f, (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.lowerBack.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f) * 0.4f, (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.upperBack.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f) * 0.4f, (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.head.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f) * 0.4f, (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.lowerBack.localScale = Vector3.zero;
                                if (!this.simpleEnemy)
                                {
                                    ((Collider)this.lowerBack.GetComponent(typeof(Collider))).enabled = false;
                                    ((Collider)this.upperBack.GetComponent(typeof(Collider))).enabled = false;
                                    ((Collider)this.upperArmL.GetComponent(typeof(Collider))).enabled = false;
                                    ((Collider)this.lowerArmL.GetComponent(typeof(Collider))).enabled = false;
                                    ((Collider)this.upperArmR.GetComponent(typeof(Collider))).enabled = false;
                                    ((Collider)this.lowerArmR.GetComponent(typeof(Collider))).enabled = false;
                                    ((Collider)this.head.GetComponent(typeof(Collider))).enabled = false;
                                    ((Joint)this.lowerBack.GetComponent(typeof(CharacterJoint))).connectedBody = (Rigidbody)null;
                                    ((Rigidbody)this.lowerBack.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                    ((Rigidbody)this.upperBack.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                    ((Rigidbody)this.upperArmL.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                    ((Rigidbody)this.lowerArmL.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                    ((Rigidbody)this.upperArmR.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                    ((Rigidbody)this.lowerArmR.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                    ((Rigidbody)this.head.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                }
                                this.beenGibbed = true;
                                if (!this.simpleEnemy)
                                {
                                    ((Behaviour)this.lowerBack.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                    ((Behaviour)this.upperBack.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                    ((Behaviour)this.upperArmL.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                    ((Behaviour)this.lowerArmL.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                    ((Behaviour)this.upperArmR.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                    ((Behaviour)this.lowerArmR.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                    ((Behaviour)this.head.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                }
                            }
                            if (this.bulletHitName == "Explosion" || !this.simpleEnemy && this.bulletHitName == "UpperLeg_L" || (this.bulletHitName == "LowerLeg_L" || this.bulletHitName == "UpperLeg_R" || this.bulletHitName == "LowerLeg_R"))
                            {
                                GameObject gameObject11 = UnityEngine.Object.Instantiate<GameObject>(this.gibHips, this.center.position, this.center.rotation);
                                gameObject11.transform.parent = this.hipR;
                                gameObject7 = UnityEngine.Object.Instantiate<GameObject>(this.gibUpperLeg, this.upperLegR.position, this.upperLegR.rotation);
                                gameObject8 = UnityEngine.Object.Instantiate<GameObject>(this.gibLowerLeg, this.lowerLegR.position, this.lowerLegR.rotation);
                                gameObject9 = UnityEngine.Object.Instantiate<GameObject>(this.gibUpperLeg, this.upperLegL.position, this.upperLegL.rotation);
                                gameObject10 = UnityEngine.Object.Instantiate<GameObject>(this.gibLowerLeg, this.lowerLegL.position, this.lowerLegL.rotation);
                                gameObject11.layer = 14;
                                gameObject7.layer = 14;
                                gameObject8.layer = 14;
                                gameObject9.layer = 14;
                                gameObject10.layer = 14;
                                gameObject11.tag = "Gib";
                                gameObject7.tag = "Gib";
                                gameObject8.tag = "Gib";
                                gameObject9.tag = "Gib";
                                gameObject10.tag = "Gib";
                                ((Rigidbody)gameObject7.GetComponent(typeof(Rigidbody))).velocity = (new Vector3(Random.Range(-2f, 1f), Random.Range(-4f, 2f), 0.0f) + this.bulletHitVel) * 0.5f;
                                ((Rigidbody)gameObject8.GetComponent(typeof(Rigidbody))).velocity = (new Vector3(Random.Range(-2f, 1f), Random.Range(-4f, 2f), 0.0f) + this.bulletHitVel) * 0.5f;
                                ((Rigidbody)gameObject9.GetComponent(typeof(Rigidbody))).velocity = (new Vector3(Random.Range(-2f, 1f), Random.Range(-4f, 2f), 0.0f) + this.bulletHitVel) * 0.5f;
                                ((Rigidbody)gameObject10.GetComponent(typeof(Rigidbody))).velocity = (new Vector3(Random.Range(-2f, 1f), Random.Range(-4f, 2f), 0.0f) + this.bulletHitVel) * 0.5f;
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.upperLegR.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f), (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.lowerLegR.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f), (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.upperLegL.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f), (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.lowerLegL.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / Random.Range(1.3f, 1.1f), (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.upperLegR.localScale = Vector3.zero;
                                this.upperLegL.localScale = Vector3.zero;
                                if (!this.simpleEnemy)
                                {
                                    ((Collider)this.upperLegR.GetComponent(typeof(Collider))).enabled = false;
                                    ((Collider)this.lowerLegR.GetComponent(typeof(Collider))).enabled = false;
                                    ((Collider)this.upperLegL.GetComponent(typeof(Collider))).enabled = false;
                                    ((Collider)this.lowerLegL.GetComponent(typeof(Collider))).enabled = false;
                                    ((Joint)this.upperLegR.GetComponent(typeof(CharacterJoint))).connectedBody = (Rigidbody)null;
                                    ((Joint)this.upperLegL.GetComponent(typeof(CharacterJoint))).connectedBody = (Rigidbody)null;
                                    ((Rigidbody)this.upperLegR.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                    ((Rigidbody)this.lowerLegR.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                    ((Rigidbody)this.upperLegL.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                    ((Rigidbody)this.lowerLegL.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                }
                                this.beenGibbed = true;
                                if (!this.simpleEnemy)
                                {
                                    ((Behaviour)this.upperLegR.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                    ((Behaviour)this.lowerLegR.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                    ((Behaviour)this.upperLegL.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                    ((Behaviour)this.lowerLegL.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                                }
                            }
                            else if (this.bulletHitName == "Head")
                            {
                                gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.gibHead, this.head.position, this.head.rotation);
                                gameObject6.layer = 14;
                                int num2 = gameObject6.tag == "Gib" ? 1 : 0;
                                ((Rigidbody)gameObject6.GetComponent(typeof(Rigidbody))).velocity = (new Vector3(Random.Range(-1f, 1f), Random.Range(-4f, 2f), 0.0f) + this.bulletHitVel) * 0.5f;
                                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.head.position + new Vector3(0.0f, 0.0f, -1f), this.bulletHitVel / 2f, (float)Random.Range(3, 5), Random.Range(0.3f, 1f), new Color(1f, 1f, 1f, 1f)), 1);
                                this.head.localScale = Vector3.zero;
                                if (!this.simpleEnemy)
                                {
                                    ((Collider)this.head.GetComponent(typeof(Collider))).enabled = false;
                                    ((Joint)this.head.GetComponent(typeof(CharacterJoint))).connectedBody = (Rigidbody)null;
                                    ((Rigidbody)this.head.GetComponent(typeof(Rigidbody))).isKinematic = true;
                                }
                                this.beenGibbed = true;
                                if (!this.simpleEnemy)
                                    ((Behaviour)this.head.GetComponent(typeof(RigidBodySlowMotion))).enabled = false;
                            }
                            if (this.motorcycle != null)
                            {
                                if (gameObject1 != null)
                                {
                                    ((RigidBodySlowMotion)gameObject1.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                    ((RigidBodySlowMotion)gameObject2.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                    ((RigidBodySlowMotion)gameObject3.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                    ((RigidBodySlowMotion)gameObject4.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                    ((RigidBodySlowMotion)gameObject5.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                    ((RigidBodySlowMotion)gameObject6.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                }
                                if (gameObject7 != null)
                                {
                                    ((RigidBodySlowMotion)gameObject7.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                    ((RigidBodySlowMotion)gameObject8.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                    ((RigidBodySlowMotion)gameObject9.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                    ((RigidBodySlowMotion)gameObject10.GetComponent(typeof(RigidBodySlowMotion))).motorcycle = true;
                                }
                            }
                            if (this.bulletHitName == "Explosion")
                                this.transform.localScale = Vector3.one * 0.4f;
                        }
                        else if (!this.simpleEnemy && (double)Random.value > 0.600000023841858)
                            ((Behaviour)this.GetComponent(typeof(EnemyDeathGrowlScript))).enabled = true;
                        if (!this.simpleEnemy && !this.deathAudioSource.isPlaying)
                        {
                            this.deathAudioSource.loop = false;
                            this.deathAudioSource.volume = Random.Range(0.8f, 1f);
                            this.deathAudioSource.pitch = Random.Range(0.95f, 1.05f);
                            this.deathAudioSource.Play();
                        }
                        SwitchScript component1 = (SwitchScript)this.GetComponent(typeof(SwitchScript));
                        if (component1 != null)
                            component1.output = 1f;
                        this.doorSpawnBulletCheck.SetActive(false);
                        this.autoAimTargetScript.enabled = false;
                        if (this.simpleEnemy)
                        {
                            this.gameObject.layer = 12;
                            this.simpleCollision.gameObject.layer = 12;
                            this.simpleCollision.transform.localPosition = new Vector3(0.0f, -1.5f, 0.0f);
                            this.sCol.height = 0.5f;
                            this.sCol.isTrigger = false;
                            this.rBody.isKinematic = true;
                        }
                      ((Behaviour)this.gameObject.GetComponent(typeof(EnemyScript))).enabled = false;
                    }
                }
                this.idle = false;
                this.engageAnimFinished = true;
                if (this.rappelling)
                {
                    this.rappellStringScript.stringTopRotSpeed += this.bulletHitVel.normalized.x * -0.3f;
                    this.rappellStringScript.stringEndRotSpeed += this.bulletHitVel.normalized.x * -0.6f;
                    if (flag1)
                    {
                        StickToTransformScript toTransformScript = (StickToTransformScript)this.lowerBack.gameObject.AddComponent(typeof(StickToTransformScript));
                        toTransformScript.transformToStickTo = this.rappellTransform;
                        toTransformScript.usePhysics = true;
                        ((SaveStateControllerScript)this.GetComponent(typeof(SaveStateControllerScript))).addComponentToChecklist((Behaviour)toTransformScript);
                    }
                }
                this.bulletKillText = string.Empty;
                this.bulletHitExtraScore = 0.0f;
                this.bulletHitText = string.Empty;
                this.bulletHit = false;
            }
            if (this.root.doGore && this.bleedFromHand && !this.root.cinematicShot)
            {
                this.bleedFromHandTimer -= this.timescale;
                if ((double)this.bleedFromHandTimer <= 0.0)
                {
                    this.bloodDropsParticle.Emit(this.root.generateEmitParams(this.handL.position, Vector3.zero, Random.Range(0.1f, 0.2f), 1f, new Color(1f, 1f, 1f, 1f)), 1);
                    this.bleedFromHandTimer = (float)Random.Range(30, 90);
                }
            }
            this.animator.SetLayerWeight(4, 1f - this.crouchAmount);
            if (this.motorcycle == null && this.stickToTransform == null)
            {
                if ((double)this.transform.position.x < (double)this.startPos.x - (double)this.walkLeftAbsoluteAmount)
                {
                    float num1 = this.startPos.x - this.walkLeftAbsoluteAmount;
                    Vector3 position = this.transform.position;
                    double num2 = (double)(position.x = num1);
                    Vector3 vector3 = this.transform.position = position;
                    this.targetXSpeed = 0.0f;
                }
                else if ((double)this.transform.position.x > (double)this.startPos.x + (double)this.walkRightAbsoluteAmount)
                {
                    float num1 = this.startPos.x + this.walkRightAbsoluteAmount;
                    Vector3 position = this.transform.position;
                    double num2 = (double)(position.x = num1);
                    Vector3 vector3 = this.transform.position = position;
                    this.targetXSpeed = 0.0f;
                }
            }
            this.kCrouch = (double)this.targetCrouchAmount != 0.0;
            if (this.shotInLeg)
                this.targetXSpeed *= 0.5f;
            this.bulletStrength = 0.0f;
            this.mousePosWithZOffset = new Vector3(this.mousePos.x, this.mousePos.y, this.mousePos.z - 1f);
            if (!this.doorSpawn && (double)targetXspeed == 0.0 && (double)this.targetXSpeed != 0.0 && (this.animator.GetCurrentAnimatorStateInfo(0).IsName("OnGround Blend Tree") && (double)Mathf.Abs(this.xSpeed) < 0.5))
                this.animator.Play("OnGround Blend Tree", 0, 0.2f);
            if (this.kCrouch && (double)this.targetXSpeed != 0.0 && (this.onGround && !this.pushedAgainstWall))
            {
                if ((double)this.standStillTimer >= (double)this.timeRequiredBeforeCrouch && (double)this.standStillTimer < (double)this.timeRequiredBeforeCrouch + 90.0 && (double)targetXspeed == 0.0)
                {
                    this.targetXSpeed = 0.0f;
                    this.xSpeed = 0.0f;
                }
                else
                {
                    this.targetCrouchAmount = 0.3f;
                    this.targetXSpeed *= 0.9f;
                }
            }
            this.xSpeed = !this.onGround ? this.root.Damp(this.targetXSpeed, this.xSpeed, 0.05f) : this.root.Damp(this.targetXSpeed, this.xSpeed, !this.idle ? (this.enemyType != 3 ? 0.2f : 0.025f) : 0.06f);
            if (this.motorcycle == null && this.stickToTransform == null)
            {
                this.ySpeed -= 0.5f * this.root.timescale;
                if (!this.onGround && !this.skyfall && ((double)this.fallKillThreshold != -999.0 && (double)this.ySpeed <= (double)this.fallKillThreshold))
                {
                    this.health = 0.0f;
                    this.bulletKillText = this.root.GetTranslation("bul23");
                    this.bulletHit = true;
                    ++this.bulletStrength;
                    this.bulletHitName = "LowerLeg_L";
                    this.bulletHitPos = this.transform.position;
                    this.bulletHitRot = this.transform.rotation;
                    this.bulletHitVel = this.transform.up * -4f;
                    this.allowGib = false;
                    this.bulletTimeAlive = 999f;
                    this.bulletKillOnHeadshot = true;
                    this.dontRagDoll = true;
                }
                if (this.alwaysOnFlatGround)
                {
                    if (!this.skyfall)
                    {
                        this.onGround = true;
                        float num1 = this.root.Damp(this.alwaysOnFlatGroundYRef + this.legLength, this.transform.position.y, 0.2f);
                        Vector3 position = this.transform.position;
                        double num2 = (double)(position.y = num1);
                        Vector3 vector3 = this.transform.position = position;
                        this.ySpeed = 0.0f;
                        this.visualCrouchAmount = Mathf.Clamp((float)(1.0 - ((double)this.transform.position.y - (double)this.crouchLegLength - (double)this.alwaysOnFlatGroundYRef)), 0.0f, 1f);
                        if ((double)this.crouchAmount < (double)this.visualCrouchAmount * 0.899999976158142)
                            this.visualCrouchAmount = (double)this.visualCrouchAmount >= (double)this.prevVisualCrouchAmount ? Mathf.Clamp(this.prevVisualCrouchAmount - 0.1f, 0.0f, 1f) : this.visualCrouchAmount;
                        this.animator.SetFloat("CrouchAmount", this.visualCrouchAmount);
                        this.prevVisualCrouchAmount = (double)this.visualCrouchAmount > 0.00999999977648258 ? this.visualCrouchAmount : 0.0f;
                    }
                    else
                    {
                        this.onGround = false;
                        this.groundTransform = (Transform)null;
                    }
                }
                else
                {
                    RaycastHit hitInfo = new RaycastHit();
                    if (!this.skyfall && Physics.Raycast(this.transform.position, Vector3.down, out hitInfo, this.justJumped || (double)this.ySpeed < -0.600000023841858 || (double)this.ySpeed >= 0.0 ? this.legLength : this.legLength + 0.8f, (int)this.layerMask))
                    {
                        bool flag = new bool();
                        string tag = hitInfo.transform.tag;
                        if (tag == "BouncePad")
                        {
                            BouncePadScript component = (BouncePadScript)hitInfo.transform.GetComponent(typeof(BouncePadScript));
                            component.doVisualThing();
                            this.ySpeed = hitInfo.transform.forward.y * component.bounceStrength;
                            this.xSpeed = hitInfo.transform.forward.x * component.bounceStrength;
                            this.bouncedOnBouncePad = true;
                            this.animator.CrossFadeInFixedTime("InAir", 0.25f, 0, 0.0f);
                        }
                        else
                            this.bouncedOnBouncePad = false;
                        if (!this.onGround)
                        {
                            if (this.rappelling && tag == "Glass")
                            {
                                ((GlassWindowScript)hitInfo.transform.GetComponent(typeof(GlassWindowScript))).totallyBreakGlass(this.rBody.velocity);
                                flag = true;
                            }
                            this.justJumped = false;
                        }
                        if (!flag)
                        {
                            if (!this.onGround && (double)this.fallKillThreshold != -999.0)
                            {
                                if (this.enemyType != 3 && (double)this.ySpeed <= -24.0)
                                {
                                    this.health = 0.0f;
                                    this.bulletKillText = this.root.GetTranslation("bul23");
                                    this.bulletHit = true;
                                    ++this.bulletStrength;
                                    this.bulletHitName = "LowerLeg_L";
                                    this.bulletHitPos = this.transform.position;
                                    this.bulletHitRot = this.transform.rotation;
                                    this.bulletHitVel = this.transform.up * -4f;
                                    this.allowGib = false;
                                    this.bulletTimeAlive = 999f;
                                    this.bulletKillOnHeadshot = true;
                                }
                                this.onGround = true;
                            }
                            if (tag == "ABMoveFollow")
                            {
                                SwitchABMoveScript component = (SwitchABMoveScript)hitInfo.transform.GetComponent(typeof(SwitchABMoveScript));
                                this.transform.position = this.transform.position + component.movePos.normalized * (component.moveSpeed * component.movePos.magnitude) * this.timescale;
                            }
                            this.groundTransform = hitInfo.collider.transform;
                            float num1 = this.root.Damp(hitInfo.point.y + this.legLength, this.transform.position.y, 0.2f);
                            Vector3 position = this.transform.position;
                            double num2 = (double)(position.y = num1);
                            Vector3 vector3 = this.transform.position = position;
                            this.ySpeed = (double)this.ySpeed >= 0.100000001490116 ? this.ySpeed * 0.95f : 0.0f;
                            this.visualCrouchAmount = Mathf.Clamp((float)(1.0 - ((double)this.transform.position.y - (double)this.crouchLegLength - (double)hitInfo.point.y)), 0.0f, 1f);
                            if ((double)this.crouchAmount < (double)this.visualCrouchAmount * 0.899999976158142)
                                this.visualCrouchAmount = (double)this.visualCrouchAmount >= (double)this.prevVisualCrouchAmount ? Mathf.Clamp(this.prevVisualCrouchAmount - 0.1f, 0.0f, 1f) : this.visualCrouchAmount;
                            this.animator.SetFloat("CrouchAmount", this.visualCrouchAmount);
                            this.prevVisualCrouchAmount = (double)this.visualCrouchAmount > 0.00999999977648258 ? this.visualCrouchAmount : 0.0f;
                        }
                    }
                    else
                    {
                        this.onGround = false;
                        this.groundTransform = (Transform)null;
                    }
                }
                if ((double)this.weapon == 9.0)
                {
                    this.targetCrouchAmount = 1f;
                    this.kCrouch = true;
                }
                this.crouchAmount = this.kCrouch ? this.root.Damp(this.targetCrouchAmount, this.crouchAmount, 0.5f) : this.root.Damp(0.0f, this.crouchAmount, 0.3f);
                if (this.skyfall || this.bouncedOnBouncePad)
                {
                    this.kCrouch = false;
                    this.targetCrouchAmount = this.crouchAmount = 0.0f;
                }
                this.legLength = Mathf.Lerp(this.standLegLength, this.crouchLegLength, this.crouchAmount);
                if (this.onGround && (double)this.weapon != 9.0 && (this.kCrouch || (double)this.targetXSpeed != 0.0))
                {
                    if (!this.dontWallCheckRight)
                    {
                        if (((double)this.targetXSpeed > 0.0 || (double)this.mousePos.x > (double)this.transform.position.x) && Physics.Raycast(this.transform.position, Vector3.right, this.raycastWidthRange, (int)this.layerMask))
                        {
                            if ((double)this.targetXSpeed > 0.0)
                                this.xSpeed = 0.0f;
                            this.wallTouchRight = true;
                        }
                        else
                            this.wallTouchRight = false;
                    }
                    if (!this.dontWallCheckLeft)
                    {
                        if (((double)this.targetXSpeed < 0.0 || (double)this.mousePos.x < (double)this.transform.position.x) && Physics.Raycast(this.transform.position, Vector3.left, this.raycastWidthRange, (int)this.layerMask))
                        {
                            if ((double)this.targetXSpeed < 0.0)
                                this.xSpeed = 0.0f;
                            this.wallTouchLeft = true;
                        }
                        else
                            this.wallTouchLeft = false;
                    }
                }
                else
                {
                    if (this.wallTouchRight && (double)this.targetXSpeed > 0.0)
                        this.xSpeed = 0.0f;
                    if (this.wallTouchLeft && (double)this.targetXSpeed < 0.0)
                        this.xSpeed = 0.0f;
                }
            }
            if (this.enemyType == 3)
            {
                this.kJump = false;
                if ((double)Mathf.Abs(this.targetXSpeed) > 0.0)
                {
                    RaycastHit hitInfo = new RaycastHit();
                    float maxDistance = Mathf.Clamp(Mathf.Abs(this.startPos.x + this.walkRightAbsoluteAmount - this.transform.position.x), 0.0f, Mathf.Clamp(Mathf.Abs(this.startPos.x - this.walkLeftAbsoluteAmount - this.transform.position.x), 0.0f, 3f));
                    if ((this.wallTouchLeft || Physics.Raycast(this.transform.position, (double)this.xSpeed <= 0.0 ? Vector3.left : Vector3.right, out hitInfo, maxDistance, (int)this.layerMask)) && ((double)Mathf.Abs(this.mainPlayer.position.x - this.transform.position.x) > (double)Mathf.Abs(hitInfo.point.x - this.transform.position.x) && !Physics.Raycast(this.transform.position + Vector3.up * 3f, (double)this.xSpeed <= 0.0 ? Vector3.left : Vector3.right, 3.5f, (int)this.layerMask)))
                        this.kJump = true;
                }
            }
            if (this.fightMode || (double)Vector3.Distance(this.mousePos, this.neck.position) < (this.enemyType != 3 ? 1.70000004768372 : 3.0))
            {
                if (this.enemyType == 3 && !this.fightMode && (!this.idle && this.onGround) && (double)Random.value > 0.400000005960464)
                {
                    this.kFire = true;
                    this.fightPoseBlend = 1f;
                    this.fightPoseBlendTimer = 120f;
                    this.punchTimer = 0.0f;
                    this.ySpeed = 8f;
                }
                this.fightMode = true;
                if ((double)Vector3.Distance(this.mousePos, this.neck.position) > (this.enemyType != 3 ? 2.29999995231628 : 4.0))
                {
                    this.fightMode = false;
                    this.fightPoseBlendTimer = 0.0f;
                }
            }
            if (this.kJump && this.onGround)
            {
                this.ySpeed = (float)Random.Range(10, 12);
                this.justJumped = true;
                this.justJumpedAnimationBoolTimer = 20f;
                this.xSpeed *= Mathf.Pow(1.1f, this.root.fixedTimescale);
                if ((double)this.crouchAmount > 0.100000001490116)
                    this.animator.CrossFade("JumpCrouch", 0.05f, 0);
                else if (this.faceRight && (double)this.xSpeed > 2.0 || !this.faceRight && (double)this.xSpeed < -2.0)
                    this.animator.CrossFade("JumpForward", 0.05f, 0);
                else if (!this.faceRight && (double)this.xSpeed > 2.0 || this.faceRight && (double)this.xSpeed < -2.0)
                    this.animator.CrossFade("JumpBackwards", 0.05f, 0);
            }
            if ((double)this.health > 0.0)
            {
                if (!this.doorSpawn)
                {
                    if (this.mainBodyKinematic)
                    {
                        this.fakeVelocity.x = this.xSpeed * this.timescaleRaw;
                    }
                    else
                    {
                        float num1 = this.xSpeed * this.timescaleRaw;
                        Vector3 velocity = this.rBody.velocity;
                        double num2 = (double)(velocity.x = num1);
                        Vector3 vector3 = this.rBody.velocity = velocity;
                    }
                }
                else if (this.mainBodyKinematic)
                {
                    this.fakeVelocity.x = 0.0f;
                    this.fakeVelocity.z = this.xSpeed * this.timescaleRaw;
                }
                else
                {
                    int num1 = 0;
                    Vector3 velocity1 = this.rBody.velocity;
                    double num2 = (double)(velocity1.x = (float)num1);
                    Vector3 vector3_1 = this.rBody.velocity = velocity1;
                    float num3 = this.xSpeed * this.timescaleRaw;
                    Vector3 velocity2 = this.rBody.velocity;
                    double num4 = (double)(velocity2.z = num3);
                    Vector3 vector3_2 = this.rBody.velocity = velocity2;
                }
                if (this.skyfall)
                {
                    float num1 = (float)-((double)this.transform.position.y - (double)this.skyfallYPos + (double)Mathf.Sin(Time.time * this.skyfallYMoveSpeed) * (double)this.skyfallYMoveAmount + (double)Mathf.Sin(Time.time * 2f) * 2.0 + (double)Random.Range(-1.6f, 1.6f));
                    Vector3 velocity = this.rBody.velocity;
                    double num2 = (double)(velocity.y = num1);
                    Vector3 vector3 = this.rBody.velocity = velocity;
                }
                else if (this.mainBodyKinematic)
                {
                    this.fakeVelocity.y = this.ySpeed * this.timescaleRaw;
                }
                else
                {
                    float num1 = this.ySpeed * this.timescaleRaw;
                    Vector3 velocity = this.rBody.velocity;
                    double num2 = (double)(velocity.y = num1);
                    Vector3 vector3 = this.rBody.velocity = velocity;
                }
                if (this.mainBodyKinematic)
                {
                    if (!this.doorSpawn && !this.playerScript.justJumped && (double)this.playerDistance < 1.5 && (double)Mathf.Abs(this.mainPlayer.position.z - this.transform.position.z) < 1.0)
                    {
                        if ((double)this.mainPlayer.position.x < (double)this.transform.position.x)
                        {
                            if ((double)this.playerScript.targetXSpeed >= 0.0)
                                this.fakeVelocity.x += 1f * this.root.timescale;
                        }
                        else if ((double)this.playerScript.targetXSpeed <= 0.0)
                            this.fakeVelocity.x -= 1f * this.root.timescale;
                    }
                    this.transform.position = this.transform.position + this.fakeVelocity / 60f * this.timescale;
                }
            }
            if (this.skyfall)
                this.faceRight = (double)this.mousePos.x <= (double)this.transform.position.x;
            else if (this.onGround || this.dontCheckGroundBeforeFacingPlayer || this.rappelling && !this.idle)
                this.faceRight = (double)this.mousePos.x > (double)this.transform.position.x;
            if ((double)this.weapon == 9.0)
            {
                if ((!this.idle || this.idle && this.standStill) && ((double)this.health > 0.0 && (!this.pushedAgainstWall || (double)this.targetCrouchAmount <= 0.0)))
                {
                    this.rifleLaser.gameObject.SetActive(true);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(this.rifleLaser.position, -new Vector3(this.rifleLaser.right.x, this.rifleLaser.right.y, 0.0f), out hitInfo, 60f, (int)this.layerMaskIncPlayerAndPlayerGameCollisionWithoutBulletPassthrough))
                    {
                        float distance = hitInfo.distance;
                        Vector3 localScale = this.rifleLaser.localScale;
                        double num = (double)(localScale.x = distance);
                        Vector3 vector3 = this.rifleLaser.localScale = localScale;
                    }
                    else
                    {
                        int num1 = 60;
                        Vector3 localScale = this.rifleLaser.localScale;
                        double num2 = (double)(localScale.x = (float)num1);
                        Vector3 vector3 = this.rifleLaser.localScale = localScale;
                    }
                }
                else
                    this.rifleLaser.gameObject.SetActive(false);
            }
            else if ((double)this.weapon == 10.0)
            {
                float num1 = Mathf.Clamp(1f - this.fireDelay / 45f, 0.3f, 1f);
                Vector3 localScale = this.crossbowBow1.localScale;
                double num2 = (double)(localScale.z = num1);
                Vector3 vector3 = this.crossbowBow1.localScale = localScale;
                this.crossbowPipe.localRotation = Quaternion.Slerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90f), Mathf.SmoothStep(0.0f, 1f, 1f - this.fireDelay / 45f));
            }
            this.fireWeapon = false;
            if ((double)this.fireDelay > 0.0)
                this.fireDelay -= this.timescale;
            if (this.kFire)
            {
                if (this.fightMode)
                {
                    if ((double)this.punchTimer <= 0.0)
                    {
                        if (this.enemyType == 3)
                        {
                            this.animator.CrossFade((double)Random.value <= 0.5 ? "Bat_Attack" : "Bat_Attack_2", 0.05f, 1, 0.0f);
                            this.punchTimer = 50f + Random.value * 10f;
                        }
                        else
                        {
                            this.animator.CrossFade(RuntimeServices.op_Addition("Punch", (object)Mathf.Round(Random.Range(0.5f, 2.5f))), 0.1f, 1, 0.0f);
                            this.punchAnimNr = (double)this.punchAnimNr >= 2.0 ? 1f : this.punchAnimNr + 1f;
                            this.punchTimer = 30f;
                            this.bulletHitLayerWeight = 0.0f;
                            if (this.enemyType != 3)
                                this.animator.SetLayerWeight(2, this.bulletHitLayerWeight);
                            this.generalAudioSource.clip = this.punchSwooshSound;
                            this.generalAudioSource.volume = Random.Range(0.8f, 1f);
                            this.generalAudioSource.pitch = Random.Range(0.85f, 1.15f);
                            this.generalAudioSource.Play();
                        }
                        this.fireDelay = 12f;
                    }
                    this.fightPoseBlendTimer = 120f;
                }
                else if ((double)this.fireDelay <= 0.0)
                {
                    this.upperArmAnimationBlendTimer = 120f;
                    this.upperArmAnimationBlend = 0.0f;
                    this.fireWeapon = true;
                }
            }
            this.punchTimer = Mathf.Clamp(this.punchTimer - this.timescale, 0.0f, this.punchTimer);
            this.animator.speed = this.timescaleRaw;
            this.animator.SetFloat("xSpeed", this.doorSpawn || this.faceRight && (double)this.xSpeed > 0.0 || !this.faceRight && (double)this.xSpeed < 0.0 ? Mathf.Abs(this.xSpeed) : -Mathf.Abs(this.xSpeed));
            this.pushedAgainstWall = (double)this.weapon != 9.0 && (this.wallTouchRight && (double)this.mousePos.x > (double)this.transform.position.x || this.wallTouchLeft && (double)this.mousePos.x < (double)this.transform.position.x);
            int num5 = !this.skyfall ? 1 : 0;
            if (num5 != 0)
                num5 = !this.alwaysOnFlatGround ? 1 : 0;
            if (num5 != 0)
                num5 = !this.onGround ? 1 : 0;
            if (num5 != 0)
                num5 = (double)this.ySpeed < 0.0 ? 1 : 0;
            if (num5 != 0)
                num5 = Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Vector3.down, this.legLength + 1.1f * Mathf.Abs(this.ySpeed * 0.11f), (int)this.layerMask) ? 1 : 0;
            bool flag3 = num5 != 0;
            if (!this.doorSpawn)
            {
                this.aimBlendTarget = 1f;
                this.lookAimBlendTarget = 1f;
            }
            Animator animator = this.animator;
            int num6 = this.bouncedOnBouncePad ? 1 : 0;
            if (num6 == 0)
            {
                int num1 = this.onGround ? 1 : 0;
                if (num1 == 0)
                    num1 = flag3 ? 1 : 0;
                if (num1 == 0)
                    num1 = !this.skyfall ? 1 : 0;
                num6 = num1 == 0 ? 1 : 0;
            }
            animator.SetBool("InAir", num6 != 0);
            if ((this.wallTouchLeft || this.wallTouchRight) && (this.kCrouch && this.onGround))
            {
                if (this.pushedAgainstWall)
                {
                    this.aimBlendTarget = 0.0f;
                    this.lookAimBlendTarget = 0.0f;
                }
                else
                {
                    this.aimBlendTarget = 1f;
                    this.lookAimBlendTarget = 1f;
                }
            }
            if (this.fightMode)
            {
                this.lookAimBlendTarget = 1f;
                this.aimBlendTarget = 0.0f;
            }
            if (this.reloading)
            {
                this.lookAimBlendTarget = 1f;
                this.aimBlendTarget = 0.0f;
                this.fightMode = false;
                this.reloadLayerWeight = this.root.Damp(1f, this.reloadLayerWeight, 0.3f);
            }
            else
                this.reloadLayerWeight = this.root.Damp(0.0f, this.reloadLayerWeight, 0.3f);
            this.animator.SetLayerWeight(5, this.reloadLayerWeight);
            this.fightPoseBlendTimer = Mathf.Clamp(this.fightPoseBlendTimer - this.timescale, 0.0f, this.fightPoseBlendTimer - this.timescale);
            this.fightPoseBlend = (double)this.fightPoseBlendTimer <= 0.0 ? this.root.Damp(0.0f, this.fightPoseBlend, 0.1f) : this.root.Damp(1f, this.fightPoseBlend, 0.3f);
            this.aimBlend = (double)this.aimBlendTarget != 0.0 || (double)this.aimBlend >= 0.00999999977648258 ? this.root.Damp(this.aimBlendTarget, this.aimBlend, 0.2f) : 0.0f;
            this.lookAimBlend = (double)this.lookAimBlendTarget != 0.0 || (double)this.lookAimBlend >= 0.00999999977648258 ? this.root.Damp(this.lookAimBlendTarget, this.lookAimBlend, 0.2f) : 0.0f;
            this.animator.SetLayerWeight(1, this.fightPoseBlend);
            if (this.kCrouch && (this.onGround || flag3) && (double)this.targetXSpeed == 0.0)
                this.animator.SetBool("Crouching", true);
            else
                this.animator.SetBool("Crouching", false);
            if ((double)this.justJumpedAnimationBoolTimer > 0.0)
                this.justJumpedAnimationBoolTimer -= this.timescale;
            this.animator.SetBool("JustJumped", (double)this.justJumpedAnimationBoolTimer > 0.0);
            if (flag3)
            {
                if (!this.kCrouch)
                    this.visualCrouchAmount = this.prevVisualCrouchAmount = 0.0f;
                else if ((double)this.crouchAmount > 0.100000001490116)
                    this.visualCrouchAmount = this.prevVisualCrouchAmount = 1f;
                this.animator.SetFloat("CrouchAmount", this.visualCrouchAmount);
            }
            this.upperArmAnimationBlendTimer = Mathf.Clamp(this.upperArmAnimationBlendTimer - this.timescale, 0.0f, this.upperArmAnimationBlendTimer);
            this.upperArmAnimationBlend = this.root.Damp((double)this.upperArmAnimationBlendTimer > 0.0 || !this.onGround ? 0.0f : 1f, this.upperArmAnimationBlend, 0.2f);
            if (!this.skyfall)
                this.autoAimTargetScript.posOffset.y = (float)(1.0 - (double)this.crouchAmount * 0.5) + this.extraAutoAimYPosOffset;
            if (this.motorcycle != null)
            {
                this.transform.position = this.motorcycleTransform.position;
                this.transform.rotation = this.motorcycleTransform.rotation;
                this.onGround = true;
                this.ySpeed = this.xSpeed = this.targetXSpeed = 0.0f;
                this.faceRight = true;
                this.aimWithLeftArm = false;
                this.animator.SetLayerWeight(2, 0.0f);
                this.animator.SetLayerWeight(3, 0.0f);
                this.animator.SetLayerWeight(4, 0.0f);
                this.animator.Play("MotorcycleBlend", 0);
                this.motorcycleAim = this.root.Damp((float)(-(double)this.transform.InverseTransformPoint(this.mousePos).x * 0.200000002980232 + 0.5), this.motorcycleAim, 0.2f);
                this.animator.SetFloat("MotorcycleAim", this.motorcycleAim);
                this.lookAimBlend = 0.6f;
                if (this.root.showNoBlood)
                    this.fireDelay = 9999f;
            }
            if (this.stickToTransform != null)
            {
                this.transform.position = this.stickToTransform.position;
                this.transform.rotation = this.stickToTransform.rotation;
                this.onGround = true;
                this.ySpeed = this.xSpeed = this.targetXSpeed = 0.0f;
            }

            if (specialEnemySyncer != null)
                specialEnemySyncer.DoLerp();

            if (!this.root.dead || !(this.enemySpeechHandlerScript != null) || !this.enemySpeechHandlerScript.speaking)
                return;
            this.speak(string.Empty, 1f, false);

            //update marker

            if (wasActivatedByPlayers) //move this code later on you cant yet caause you gotta test with abyeon
                idle = false;
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = (Color)new Vector4(1f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawSphere(this.mousePos, 0.5f);
    }

    public virtual void LateUpdate()
    {
        if (this.runLogic)
        {
            float num1 = new float();
            float num2 = new float();
            RaycastHit hitInfo1 = new RaycastHit();
            Quaternion quaternion = new Quaternion();
            float num3 = Mathf.Clamp((double)this.weapon == 9.0 || (double)this.weapon == 10.0 ? this.fireDelay / 60f : this.fireDelay, 0.0f, 8f);
            if (this.faceRight)
            {
                if (!this.idle || (double)this.weapon == 9.0 && this.standStill)
                {
                    if (this.motorcycle == null)
                    {
                        this.lowerBackFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.lowerBack.rotation, Quaternion.LookRotation(this.mousePosWithZOffset - this.lowerBack.position, Vector3.back), (float)(((double)this.fightPoseBlendTimer <= 0.0 ? (double)this.aimBlend : (double)this.lookAimBlend) * 0.300000011920929)), this.lowerBackFakeRot, 0.25f);
                        this.lowerBack.rotation = this.lowerBackFakeRot;
                    }
                    if ((double)this.weapon == 5.0 || (double)this.weapon == 6.0 || ((double)this.weapon == 9.0 || (double)this.weapon == 10.0))
                    {
                        this.shoulderR.localRotation = Quaternion.Slerp(this.shoulderR.localRotation, Quaternion.Euler(346.5f, 280.8f, 310f), this.aimBlend);
                        this.shoulderL.localRotation = Quaternion.Slerp(this.shoulderL.localRotation, Quaternion.Euler(13.5f, 280.8f, 110f), this.aimBlend);
                        this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.upperArmR.position, Vector3.forward) * Quaternion.Euler(180f, (float)(45.0 + (double)num3 * 0.5 + (double)this.aimAngleOffset / 1.5), 290f), this.aimBlend);
                        this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.lowerArmR.position, Vector3.forward) * Quaternion.Euler(180f, 90f + num3 * 1f, 45f), this.aimBlend);
                        this.handR.rotation = Quaternion.Slerp(this.handR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.handR.position, Vector3.forward) * Quaternion.Euler(235f, (float)(92.0 + (double)num3 * 1.0 + (double)this.aimAngleOffset / 1.5), 350f), this.aimBlend);
                        this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.upperArmL.position, Vector3.forward) * Quaternion.Euler(180f, 55f + num3 * 0.5f, 350f), this.aimBlend);
                        this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(this.bulletPointR.position - this.lowerArmL.position, Vector3.forward) * Quaternion.Euler(180f, 80f + num3 * 1f, 0.0f), this.aimBlend);
                        this.handL.localRotation = Quaternion.Euler(180f, 0.0f, 45f);
                    }
                    else
                    {
                        this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.upperArmR.position, Vector3.forward) * Quaternion.Euler(180f, (float)(75.0 + (!this.fireLeftGun ? 0.0 : (double)this.fireDelay * 0.5) + (double)this.aimAngleOffset / 2.0), 345f), this.aimBlend * (float)(1.0 - (double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend));
                        if (this.aimWithLeftArm)
                            this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.upperArmL.position, Vector3.forward) * Quaternion.Euler(180f, (float)(75.0 + (this.fireLeftGun ? 0.0 : (double)this.fireDelay * 0.5) + (double)this.aimAngleOffset / 2.0), 5f), this.aimBlend * (float)(1.0 - (double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend));
                        if (!this.dontDoArmBendRayCast)
                        {
                            if (Physics.Raycast(this.shoulderR.position, -this.upperArmR.right, out hitInfo1, 2f, (int)this.layerMask))
                                this.upperArmR.localRotation *= Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderR.position, hitInfo1.point) / 2.0) * 140.0) * this.aimBlend, 0.0f);
                            if (this.aimWithLeftArm && Physics.Raycast(this.shoulderL.position, -this.upperArmL.right, out hitInfo1, 2f, (int)this.layerMask))
                                this.upperArmL.localRotation *= Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderL.position, hitInfo1.point) / 2.0) * 140.0) * this.aimBlend, 0.0f);
                        }
                        this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.lowerArmR.position, Vector3.forward) * Quaternion.Euler(180f, (float)(90.0 + (!this.fireLeftGun ? 0.0 : (double)this.fireDelay * 1.0) + (double)this.aimAngleOffset / 2.0), 0.0f), this.aimBlend * (float)(1.0 - 0.300000011920929 * ((double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend)));
                        if (this.aimWithLeftArm)
                            this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.lowerArmL.position, Vector3.forward) * Quaternion.Euler(180f, (float)(90.0 + (this.fireLeftGun ? 0.0 : (double)this.fireDelay * 1.0) + (double)this.aimAngleOffset / 2.0), 0.0f), this.aimBlend * (float)(1.0 - 0.300000011920929 * ((double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend)));
                    }
                }
                if (!this.doorSpawn)
                    this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, !this.pushedAgainstWall || !this.kCrouch || !this.onGround ? 90f : 180f, 0.0f), this.playerGraphics.localRotation, 0.25f);
                this.headFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.head.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.head.position.z) - this.head.position, Vector3.back), this.lookAimBlend * ((double)this.transform.InverseTransformPoint(this.mousePos).x <= 0.0 ? 0.0f : 0.5f)), this.headFakeRot, 0.25f);
                this.head.rotation = this.headFakeRot;
            }
            else
            {
                if (!this.idle || (double)this.weapon == 9.0 && this.standStill)
                {
                    this.lowerBackFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.lowerBack.rotation, Quaternion.LookRotation(this.mousePosWithZOffset - this.lowerBack.position, Vector3.forward), (float)(((double)this.fightPoseBlendTimer <= 0.0 ? (double)this.aimBlend : (double)this.lookAimBlend) * 0.300000011920929)), this.lowerBackFakeRot, 0.25f);
                    this.lowerBack.rotation = this.lowerBackFakeRot;
                    if ((double)this.weapon == 5.0 || (double)this.weapon == 6.0 || ((double)this.weapon == 9.0 || (double)this.weapon == 10.0))
                    {
                        this.shoulderR.localRotation = Quaternion.Slerp(this.shoulderR.localRotation, Quaternion.Euler(346.5f, 280.8f, 310f), this.aimBlend);
                        this.shoulderL.localRotation = Quaternion.Slerp(this.shoulderL.localRotation, Quaternion.Euler(13.5f, 280.8f, 110f), this.aimBlend);
                        this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.upperArmR.position, Vector3.back) * Quaternion.Euler(180f, (float)(45.0 + (double)num3 * 0.5 + (double)this.aimAngleOffset / 1.5), 290f), this.aimBlend);
                        this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.lowerArmR.position, Vector3.back) * Quaternion.Euler(180f, 90f + num3 * 1f, 45f), this.aimBlend);
                        this.handR.rotation = Quaternion.Slerp(this.handR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.handR.position, Vector3.back) * Quaternion.Euler(235f, (float)(92.0 + (double)num3 * 1.0 + (double)this.aimAngleOffset / 1.5), 350f), this.aimBlend);
                        this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.upperArmL.position, Vector3.back) * Quaternion.Euler(180f, 55f + num3 * 0.5f, 350f), this.aimBlend);
                        this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(this.bulletPointR.position - this.lowerArmL.position, Vector3.back) * Quaternion.Euler(180f, 80f + num3 * 1f, 0.0f), this.aimBlend);
                        this.handL.localRotation = Quaternion.Euler(180f, 0.0f, 45f);
                    }
                    else
                    {
                        this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.upperArmR.position, Vector3.back) * Quaternion.Euler(180f, (float)(75.0 + (!this.fireLeftGun ? 0.0 : (double)this.fireDelay * 0.5) + (double)this.aimAngleOffset / 2.0), 355f), this.aimBlend * (float)(1.0 - (double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend));
                        if (this.aimWithLeftArm)
                            this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.upperArmL.position, Vector3.back) * Quaternion.Euler(180f, (float)(75.0 + (this.fireLeftGun ? 0.0 : (double)this.fireDelay * 0.5) + (double)this.aimAngleOffset / 2.0), 15f), this.aimBlend * (float)(1.0 - (double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend));
                        if (!this.dontDoArmBendRayCast)
                        {
                            if (Physics.Raycast(this.shoulderR.position, -this.upperArmR.right, out hitInfo1, 2f, (int)this.layerMask))
                                this.upperArmR.localRotation *= Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderR.position, hitInfo1.point) / 2.0) * 140.0) * this.aimBlend, 0.0f);
                            if (this.aimWithLeftArm && Physics.Raycast(this.shoulderL.position, -this.upperArmL.right, out hitInfo1, 2f, (int)this.layerMask))
                                this.upperArmL.localRotation *= Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderL.position, hitInfo1.point) / 2.0) * 140.0) * this.aimBlend, 0.0f);
                        }
                        this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.lowerArmR.position, Vector3.back) * Quaternion.Euler(180f, (float)(90.0 + (!this.fireLeftGun ? 0.0 : (double)this.fireDelay * 1.0) + (double)this.aimAngleOffset / 2.0), 0.0f), this.aimBlend * (float)(1.0 - 0.300000011920929 * ((double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend)));
                        if (this.aimWithLeftArm)
                            this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.lowerArmL.position, Vector3.back) * Quaternion.Euler(180f, (float)(90.0 + (this.fireLeftGun ? 0.0 : (double)this.fireDelay * 1.0) + (double)this.aimAngleOffset / 2.0), 0.0f), this.aimBlend * (float)(1.0 - 0.300000011920929 * ((double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend)));
                    }
                }
                if (!this.doorSpawn)
                    this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, !this.pushedAgainstWall || !this.kCrouch || !this.onGround ? 270f : 180f, 0.0f), this.playerGraphics.localRotation, !this.idle ? 0.2f : 0.07f);
                this.headFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.head.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.head.position.z) - this.head.position, Vector3.forward), this.lookAimBlend * ((double)this.transform.InverseTransformPoint(this.mousePos).x >= 0.0 ? 0.0f : 0.5f)), this.headFakeRot, 0.25f);
                this.head.rotation = this.headFakeRot;
            }
            if (!this.idle && (double)this.weapon != 5.0 && ((double)this.weapon != 6.0 && (double)this.weapon != 9.0) && (double)this.weapon != 10.0)
            {
                this.handR.localRotation = Quaternion.Slerp(this.handR.localRotation, Quaternion.Euler(45f, 0.0f, (float)((this.fireLeftGun || !this.aimWithLeftArm ? (double)num3 * 4.0 : 0.0) - 8.0) + this.aimAngleOffset), this.aimBlend);
                if (this.aimWithLeftArm)
                    this.handL.localRotation = Quaternion.Slerp(this.handL.localRotation, Quaternion.Euler(-45f, 0.0f, (float)(8.0 - (this.fireLeftGun ? 0.0 : (double)this.fireDelay * 4.0)) + this.aimAngleOffset), this.aimBlend);
            }
            if (this.onGround && (double)this.visualCrouchAmount <= 0.0 && !this.dontDoLegBendRayCast)
            {
                this.prevVisualCrouchAmount = this.visualCrouchAmount = 0.0f;
                RaycastHit hitInfo2 = new RaycastHit();
                float num4 = new float();
                float legLength = this.legLength;
                float num5 = new float();
                if (Physics.Raycast(this.footTipR.position + new Vector3(0.0f, legLength, 0.0f), Vector3.down, out hitInfo2, legLength, (int)this.layerMask))
                {
                    float num6 = Vector3.Distance(this.footTipR.position + new Vector3(0.0f, legLength, 0.0f), hitInfo2.point);
                    if ((double)num6 > 0.219999998807907)
                    {
                        float y = Mathf.Clamp((float)(1.0 - (double)num6 / (double)legLength) * -180f, -90f, 0.0f);
                        this.upperLegR.localRotation *= Quaternion.Euler(y * 0.5f, y, 0.0f);
                        this.lowerLegR.localRotation *= Quaternion.Euler(0.0f, -y, -y);
                    }
                }
                if (Physics.Raycast(this.footTipL.position + new Vector3(0.0f, legLength, 0.0f), Vector3.down, out hitInfo2, legLength, (int)this.layerMask))
                {
                    float num6 = Vector3.Distance(this.footTipL.position + new Vector3(0.0f, legLength, 0.0f), hitInfo2.point);
                    if ((double)num6 > 0.219999998807907)
                    {
                        float num7 = Mathf.Clamp((float)(1.0 - (double)num6 / (double)legLength) * -180f, -45f, 0.0f) * 2f;
                        this.upperLegL.localRotation *= Quaternion.Euler(num7 * 0.309f, num7 * 0.62f, num7 * -0.526f);
                        this.lowerLegL.localRotation *= Quaternion.Euler(num7 * -0.309f, num7 * -0.62f, num7 * 0.426f);
                    }
                }
            }
            if (this.fireWeapon)
            {
                if ((double)this.ammo > 0.0)
                {
                    if (!this.reloading)
                    {
                        GameObject gameObject1 = (GameObject)null;
                        BulletScript bulletScript = (BulletScript)null;
                        if ((double)this.weapon == 1.0)
                        {
                            this.playGunSound(true);
                            this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                            this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(Random.Range(-0.8f, 0.8f), (float)Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                            GameObject bullet = this.root.getBullet(this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                            bulletScript = this.root.getBulletScript();
                            bulletScript.bulletStrength = 0.35f;
                            bulletScript.bulletSpeed = 10f;
                            bulletScript.friendly = false;
                            bulletScript.doPostSetup();
                            this.root.getMuzzleFlash(0, this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                            bullet.transform.rotation *= Quaternion.Euler(Random.Range(-1f, 1f), 0.0f, 0.0f);
                            this.fireDelay = 18f;
                        }
                        else if ((double)this.weapon == 2.0)
                        {
                            if (this.fireLeftGun)
                            {
                                this.playGunSound(false);
                                this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointL.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                                this.shellParticle.Emit(this.root.generateEmitParams(this.handL.position, new Vector3(Random.Range(-0.8f, 0.8f), (float)Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                                gameObject1 = this.root.getBullet(this.bulletPointL.position, Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position));
                                bulletScript = this.root.getBulletScript();
                                bulletScript.bulletStrength = 0.35f;
                                bulletScript.bulletSpeed = 10f;
                                bulletScript.friendly = false;
                                bulletScript.doPostSetup();
                                this.root.getMuzzleFlash(0, this.bulletPointL.position, Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                            }
                            if (!this.fireLeftGun)
                            {
                                this.playGunSound(true);
                                this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                                this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(Random.Range(-0.8f, 0.8f), (float)Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                                gameObject1 = this.root.getBullet(this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                                bulletScript = this.root.getBulletScript();
                                bulletScript.bulletStrength = 0.35f;
                                bulletScript.bulletSpeed = 10f;
                                bulletScript.friendly = false;
                                bulletScript.doPostSetup();
                                this.root.getMuzzleFlash(0, this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                            }
                            gameObject1.transform.rotation *= Quaternion.Euler(Random.Range(-2f, 2f), 0.0f, 0.0f);
                            this.fireLeftGun = !this.fireLeftGun;
                            this.fireDelay = 9f;
                        }
                        else if ((double)this.weapon == 3.0)
                        {
                            this.playGunSound(true);
                            this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                            this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(Random.Range(-0.8f, 0.8f), (float)Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                            GameObject bullet = this.root.getBullet(this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                            this.root.getMuzzleFlash(1, this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                            bulletScript = this.root.getBulletScript();
                            bulletScript.bulletStrength = 0.2f;
                            bulletScript.bulletSpeed = 12f;
                            bulletScript.friendly = false;
                            bulletScript.doPostSetup();
                            bullet.transform.rotation *= Quaternion.Euler(Random.Range(-2f, 2f), 0.0f, 0.0f);
                            this.fireDelay = 8f;
                        }
                        else if ((double)this.weapon == 4.0)
                        {
                            if (this.fireLeftGun)
                            {
                                this.playGunSound(false);
                                this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointL.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                                this.shellParticle.Emit(this.root.generateEmitParams(this.handL.position, new Vector3(Random.Range(-0.8f, 0.8f), (float)Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                                gameObject1 = this.root.getBullet(this.bulletPointL.position, Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position));
                                this.root.getMuzzleFlash(1, this.bulletPointL.position, Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                            }
                            if (!this.fireLeftGun)
                            {
                                this.playGunSound(true);
                                this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                                this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(Random.Range(-0.8f, 0.8f), (float)Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                                gameObject1 = this.root.getBullet(this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                                this.root.getMuzzleFlash(1, this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                            }
                            bulletScript = this.root.getBulletScript();
                            bulletScript.bulletStrength = 0.2f;
                            bulletScript.bulletSpeed = 12f;
                            bulletScript.friendly = false;
                            bulletScript.doPostSetup();
                            gameObject1.transform.rotation *= Quaternion.Euler(Random.Range(-3f, 3f), 0.0f, 0.0f);
                            this.fireLeftGun = !this.fireLeftGun;
                            this.fireDelay = 4f;
                        }
                        else if ((double)this.weapon == 5.0)
                        {
                            this.playGunSound(true);
                            this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                            this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(Random.Range(-0.8f, 0.8f), (float)Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                            GameObject bullet = this.root.getBullet(this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                            bulletScript = this.root.getBulletScript();
                            bulletScript.bulletStrength = 0.25f;
                            bulletScript.bulletSpeed = 14f;
                            bulletScript.bulletLength = 2f;
                            bulletScript.friendly = false;
                            bulletScript.doPostSetup();
                            this.root.getMuzzleFlash(2, this.bulletPointR.position - this.bulletPointR.forward * 0.5f, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                            bullet.transform.rotation *= Quaternion.Euler(Random.Range(-1f, 1f), 0.0f, 0.0f);
                            this.fireDelay = 4f;
                        }
                        else if ((double)this.weapon == 6.0)
                        {
                            this.playGunSound(true);
                            this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                            this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(Random.Range(-0.8f, 0.8f), (float)Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                            for (int index = 0; index < 8; ++index)
                            {
                                GameObject bullet = this.root.getBullet(this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                                bulletScript = this.root.getBulletScript();
                                bulletScript.bulletStrength = 0.1f;
                                bulletScript.bulletSpeed = 10f + (float)index * 0.5f;
                                bulletScript.bulletLength = 0.4f;
                                bulletScript.allowGib = true;
                                bulletScript.friendly = false;
                                bullet.transform.rotation *= Quaternion.Euler(Random.Range(-9f, 9f), 0.0f, 0.0f);
                                bulletScript.fromTransform = this.transform;
                                bulletScript.doPostSetup();
                            }
                            this.root.getMuzzleFlash(3, this.bulletPointR.position - this.bulletPointR.forward * 0.5f, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                            this.fireDelay = 70f;
                        }
                        else if ((double)this.weapon == 9.0)
                        {
                            this.playGunSound(true);
                            this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                            this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(Random.Range(-0.8f, 0.8f), (float)Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                            this.root.getBullet(this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                            bulletScript = this.root.getBulletScript();
                            bulletScript.bulletStrength = 1.1f;
                            bulletScript.bulletSpeed = 17f;
                            bulletScript.bulletLength = 2.5f;
                            bulletScript.knockBack = true;
                            bulletScript.friendly = false;
                            bulletScript.doPostSetup();
                            this.root.getMuzzleFlash(4, this.bulletPointR.position - this.bulletPointR.forward * 0.65f, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                            this.fireDelay = 180f;
                        }
                        else if ((double)this.weapon == 10.0)
                        {
                            this.playGunSound(true);
                            this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.crossbowArrow, this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                            ((CrossbowArrowScript)gameObject2.GetComponent(typeof(CrossbowArrowScript))).friendly = false;
                            gameObject2.transform.rotation *= Quaternion.Euler(Random.Range(-1f, 1f), 0.0f, 0.0f);
                            this.fireDelay = 120f;
                        }
                        if (bulletScript != null)
                        {
                            bulletScript.friendly = false;
                            bulletScript.fromTransform = this.transform;
                        }
                        --this.ammo;
                    }
                }
                else
                    this.reload();
            }
            if (this.rappelling)
            {
                this.animator.Play("Rappelling", 0);
                this.rappellStringScript.ropeLength -= this.ySpeed / 60f * this.root.timescale;
                if ((double)this.rappellStringScript.ropeLength > (double)this.rappellRopeLength)
                {
                    this.ySpeed -= this.root.DampAdd(this.rappellRopeLength, this.rappellStringScript.ropeLength, 0.9f);
                    this.ySpeed *= Mathf.Pow(0.95f, this.root.timescale);
                    if (this.unhingeFromRappell)
                        this.rapellTimer += this.root.timescale;
                }
                if (this.playerScript.swinging && this.playerScript.swingTransform == this.rappellTransform.parent && (double)this.playerScript.grabLength > (double)this.playerScript.ropeLength - 4.5)
                    this.playerScript.grabLength = Mathf.Clamp(this.playerScript.grabLength - 0.15f * this.timescale, 1f, this.playerScript.ropeLength - 0.1f);
                this.transform.position = this.rappellTransform.position + new Vector3(0.0f, -0.3f, 0.0f);
                this.xSpeed = this.targetXSpeed = 0.0f;
                this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(new Vector3(this.transform.position.x, this.transform.position.y - 0.3f, this.transform.position.z) + this.rappellTransform.forward * 1.5f - this.lowerArmL.position, Vector3.back) * Quaternion.Euler(180f, 90f, 0.0f), 1f);
                this.allowBulletHit = true;
                if ((double)this.timeAlive < 45.0 && this.rootShared.modOneShotEnemies)
                    this.allowBulletHit = false;
                if ((double)this.rapellTimer >= 100.0)
                {
                    this.animator.CrossFadeInFixedTime("InAir", 0.25f, 0, 0.0f);
                    this.alertAmount = 1f;
                    this.alertAmountTarget = Random.Range(0.75f, 1f);
                    this.rappelling = false;
                    this.rappellStringScript.weightedString = false;
                }
            }
            if (this.skyfall && !this.root.paused && !this.root.dead)
            {
                float num4 = 0.05f;
                this.lowerBack.localScale = Vector3.one + new Vector3((float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4);
                this.lowerArmR.localScale = Vector3.one + new Vector3((float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4) * 0.5f;
                this.lowerArmL.localScale = Vector3.one + new Vector3((float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4) * 0.5f;
                this.upperLegR.localScale = Vector3.one + new Vector3((float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4);
                this.upperLegL.localScale = Vector3.one + new Vector3((float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4);
                this.lowerLegR.localScale = Vector3.one + new Vector3((float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4);
                this.lowerLegL.localScale = Vector3.one + new Vector3((float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4, (float)Random.Range(-1, 1) * num4);
                this.transform.localScale = new Vector3(Random.Range(0.97f, 1.03f), Random.Range(0.97f, 1.03f), Random.Range(0.97f, 1.03f));
            }
        }
    }

    public virtual void OnCollisionEnter(Collision col)
    {
        if (!this.runLogic || col.collider.gameObject.layer != 21)
            return;
        this.cameraScript.kickBack(0.4f);
        this.cameraScript.screenShake += 0.04f;
        this.cameraScript.bigScreenShake = 0.6f;
        this.bulletHit = true;
        this.bulletStrength += 999f;
        Vector3 point = col.contacts[0].point;
        this.bulletHitName = (double)Vector3.Distance(point, this.transform.position) >= 0.5 ? ((double)point.y <= (double)this.transform.position.y ? "UpperLeg_R" : "Center") : "Explosion";
        this.bulletHitPos = col.transform.position;
        this.bulletHitRot = this.transform.rotation;
        this.bulletHitVel = Vector3.zero;
        this.allowGib = true;
        this.bulletTimeAlive = 1f;
        ExplosionVisualScript component = (ExplosionVisualScript)col.gameObject.GetComponent(typeof(ExplosionVisualScript));
        if (!(component != null))
            return;
        ++component.nrOfKills;
    }

    public virtual void dropLeftHandItemCheck()
    {
        if (!(this.leftHandItem != null) || !this.dropItemOnAlert || !(this.leftHandItem.transform.parent != null))
            return;
        this.leftHandItem.transform.parent = (Transform)null;
        ((Rigidbody)this.leftHandItem.GetComponent(typeof(Rigidbody))).isKinematic = false;
    }

    public virtual void remoteTriggerEnemies()
    {
        if (this.enemiesInRangeAtStart.Length <= 0)
            return;
        if (enemySpeechHandlerScript != null && !enemySpeechHandlerScript.dontInterrupt && (!hasBeenRemoteTriggered && (double)root.timeSinceEnemySpokeOnDetection > 120.0) && (double)Random.value > 0.100000001490116)
        {
            float num = Random.value;
            if ((double)num > 0.75)
            {
                if (theme == 1)
                    speak(root.GetTranslation("eRemoteAlert1"), 2f, true);
                else if (theme == 2)
                    speak(root.GetTranslation("eRemoteAlert5"), 2f, true);
                else if (theme == 3)
                    speak(root.GetTranslation("eRemoteAlert9"), 2f, true);
                else if (theme == 4)
                    speak(root.GetTranslation("eRemoteAlert13"), 2f, true);
                else if (theme == 5)
                    speak(root.GetTranslation("eRemoteAlert17"), 2f, true);
            }
            else if ((double)num > 0.5)
            {
                if (theme == 1)
                    speak(root.GetTranslation("eRemoteAlert2"), 2f, true);
                else if (theme == 2)
                    speak(root.GetTranslation("eRemoteAlert6"), 2f, true);
                else if (theme == 3)
                    speak(root.GetTranslation("eRemoteAlert10"), 2f, true);
                else if (theme == 4)
                    speak(root.GetTranslation("eRemoteAlert14"), 2f, true);
                else if (theme == 5)
                    speak(root.GetTranslation("eRemoteAlert18"), 2f, true);
            }
            else if ((double)num > 0.25)
            {
                if (theme == 1)
                    speak(root.GetTranslation("eRemoteAlert3"), 2f, true);
                else if (theme == 2)
                    speak(root.GetTranslation("eRemoteAlert7"), 2f, true);
                else if (theme == 3)
                    speak(root.GetTranslation("eRemoteAlert11"), 2f, true);
                else if (theme == 4)
                    speak(root.GetTranslation("eRemoteAlert15"), 2f, true);
                else if (theme == 5)
                    speak(root.GetTranslation("eRemoteAlert19"), 2f, true);
            }
            else if (theme == 1)
                speak(root.GetTranslation("eRemoteAlert4"), 2f, true);
            else if (theme == 2)
                speak(root.GetTranslation("eRemoteAlert8"), 2f, true);
            else if (theme == 3)
                speak(root.GetTranslation("eRemoteAlert12"), 2f, true);
            else if (theme == 4)
                speak(root.GetTranslation("eRemoteAlert16"), 2f, true);
            else if (theme == 5)
                speak(root.GetTranslation("eRemoteAlert20"), 2f, true);
            root.timeSinceEnemySpokeOnDetection = 0.0f;
        }
        int index = 0;
        EnemyScript[] enemiesInRangeAtStart = this.enemiesInRangeAtStart;
        for (int length = enemiesInRangeAtStart.Length; index < length; ++index)
        {
            if (enemiesInRangeAtStart[index] != null)
            {
                if (enemiesInRangeAtStart[index].idle)
                    enemiesInRangeAtStart[index].mousePos = this.transform.position;
                enemiesInRangeAtStart[index].faceRight = (double)enemiesInRangeAtStart[index].transform.position.x < (double)enemiesInRangeAtStart[index].mousePos.x;
                enemiesInRangeAtStart[index].remoteTriggeredTimer = 180f;
                enemiesInRangeAtStart[index].hasBeenRemoteTriggered = true;
            }
        }
    }

    public virtual void alertEnemiesInRange(bool lookAtAlertingEnemy)
    {
        if (this.enemiesInRangeAtStart.Length <= 0)
            return;
        int index = 0;
        EnemyScript[] enemiesInRangeAtStart = this.enemiesInRangeAtStart;
        for (int length = enemiesInRangeAtStart.Length; index < length; ++index)
        {
            if (enemiesInRangeAtStart[index] != null)
            {
                if (lookAtAlertingEnemy && enemiesInRangeAtStart[index].idle)
                {
                    enemiesInRangeAtStart[index].mousePos = this.transform.position;
                    enemiesInRangeAtStart[index].faceRight = (double)enemiesInRangeAtStart[index].transform.position.x < (double)enemiesInRangeAtStart[index].mousePos.x;
                }
                if ((double)enemiesInRangeAtStart[index].alertAmount < 0.100000001490116)
                    enemiesInRangeAtStart[index].remoteAlerted = true;
            }
        }
    }

    public virtual void changeWeapon(float w)
    {
        pistolL.gameObject.SetActive(false);
        pistolR.gameObject.SetActive(false);
        uziR.gameObject.SetActive(false);
        uziL.gameObject.SetActive(false);
        machineGun.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
        sniper.gameObject.SetActive(false);
        crossbow.gameObject.SetActive(false);
        if (club != null)
            club.gameObject.SetActive(false);
        SkinnedMeshRenderer component1 = (SkinnedMeshRenderer)handR.Find("hand_01").GetComponent(typeof(SkinnedMeshRenderer));
        SkinnedMeshRenderer component2 = (SkinnedMeshRenderer)handL.Find("hand_01_L").GetComponent(typeof(SkinnedMeshRenderer));
        if ((double)w == 0.0)
        {
            club.gameObject.SetActive(true);
            component1.SetBlendShapeWeight(0, 100f);
            component2.SetBlendShapeWeight(0, 0.0f);
            aimWithLeftArm = false;
        }
        else if ((double)w == 1.0)
        {
            pistolR.gameObject.SetActive(true);
            component1.SetBlendShapeWeight(0, 100f);
            component2.SetBlendShapeWeight(0, 0.0f);
            aimWithLeftArm = false;
        }
        else if ((double)w == 2.0)
        {
            pistolR.gameObject.SetActive(true);
            pistolL.gameObject.SetActive(true);
            component1.SetBlendShapeWeight(0, 100f);
            component2.SetBlendShapeWeight(0, 100f);
            aimWithLeftArm = true;
        }
        else if ((double)w == 3.0)
        {
            uziR.gameObject.SetActive(true);
            component1.SetBlendShapeWeight(0, 100f);
            component2.SetBlendShapeWeight(0, 0.0f);
            aimWithLeftArm = false;
        }
        else if ((double)w == 4.0)
        {
            uziR.gameObject.SetActive(true);
            uziL.gameObject.SetActive(true);
            component1.SetBlendShapeWeight(0, 100f);
            component2.SetBlendShapeWeight(0, 100f);
            aimWithLeftArm = true;
        }
        else if ((double)w == 5.0)
        {
            machineGun.gameObject.SetActive(true);
            component1.SetBlendShapeWeight(0, 100f);
            component2.SetBlendShapeWeight(0, 50f);
            aimWithLeftArm = false;
        }
        else if ((double)w == 6.0)
        {
            shotgun.gameObject.SetActive(true);
            component1.SetBlendShapeWeight(0, 100f);
            component2.SetBlendShapeWeight(0, 50f);
            aimWithLeftArm = false;
        }
        else if ((double)w == 9.0)
        {
            sniper.gameObject.SetActive(true);
            component1.SetBlendShapeWeight(0, 100f);
            component2.SetBlendShapeWeight(0, 50f);
            aimWithLeftArm = false;
        }
        else if ((double)w == 10.0)
        {
            crossbow.gameObject.SetActive(true);
            component1.SetBlendShapeWeight(0, 100f);
            component2.SetBlendShapeWeight(0, 50f);
            aimWithLeftArm = false;
        }
        if ((double)weapon != 0.0 && weaponSound[(int)w] != null)
        {
            handRAudio.clip = weaponSound[(int)w];
            handLAudio.clip = weaponSound[(int)w];
        }
        weapon = w;
        ammo = root.ammoFullClip[(int)weapon];
    }

    public virtual void playGunSound(bool isRight)
    {
        if (!handRAudio.gameObject.activeInHierarchy)
            return;
        if (isRight)
        {
            handRAudio.pitch = Random.Range(0.95f, 1.05f);
            handRAudio.volume = Random.Range(0.95f, 1.1f);
            handRAudio.Play();
        }
        else
        {
            handLAudio.pitch = Random.Range(0.95f, 1.05f);
            handLAudio.volume = Random.Range(0.95f, 1.1f);
            handLAudio.Play();
        }
    }

    public virtual void punch(int fist)
    {
        if (reloading)
            return;
        RaycastHit hitInfo = new RaycastHit();
        Vector3 vector3 = new Vector3(neck.position.x, neck.position.y, transform.position.z) + Vector3.right * (!faceRight ? -3f : 3f);
        if (!Physics.Raycast(transform.position, (mainPlayer.position - transform.position).normalized, out hitInfo, 2.5f, (int)layerMaskOnlyPlayer))
            return;
        playerScript.bulletHit = true;
        playerScript.bulletStrength += 0.4f;
        playerScript.bulletFromTransform = transform;
        playerScript.bulletHitVel = new Vector3(!faceRight ? -10f : 10f, 1f, 0.0f);
        generalAudioSource.clip = punchHitSound;
        generalAudioSource.volume = Random.Range(0.8f, 1f);
        generalAudioSource.pitch = Random.Range(0.85f, 1.15f);
        generalAudioSource.Play();
        cameraScript.screenShake += 0.5f;
        cameraScript.bigScreenShake += 0.3f;
        if (root.showNoBlood)
            return;
        bloodMistParticle.Emit(root.generateEmitParams(hitInfo.point, Vector3.zero, (float)Random.Range(3, 5), Random.Range(0.3f, 1f), !root.doGore ? new Color(0.0f, 0.0f, 0.0f, 0.5f) : new Color(1f, 1f, 1f, 0.5f)), 1);
        for (int index = 0; index < 10; ++index)
            bloodDropsParticle.Emit(root.generateEmitParams(hitInfo.point, new Vector3((float)(-(double)transform.forward.x * 3.5) + (float)Random.Range(-4, 4), (float)(-(double)transform.forward.y * 3.5) + (float)Random.Range(1, 6), Random.Range(-0.5f, 0.5f)), Random.Range(0.05f, 0.2f), Random.Range(0.8f, 1.3f), !root.doGore ? new Color(0.0f, 0.0f, 0.0f, 1f) : new Color(1f, 1f, 1f, 1f)), 1);
    }

    public virtual void knockBack(bool toRight, float knockTimer)
    {
        if (cantBeHarmed)
            return;
        if ((double)weapon != 9.0 && animator != null)
        {
            animator.CrossFadeInFixedTime("KnockedBack", 0.1f, 1, 0.0f);
            knockedBackTimer = !alwaysGetBulletHitHeadshot ? knockTimer : knockTimer / 3f;
        }
        if (doorSpawn)
            return;
        xSpeed = (float)((!toRight ? -12 : 12) / (enemyType == 1 || enemyType == 3 ? 3 : 1));
        targetXSpeed = (float)((!toRight ? -2 : 2) / (enemyType == 1 || enemyType == 3 ? 2 : 1));
        if (enemyType != 3)
            return;
        engageAnimFinished = true;
    }

    public virtual void fixFakeHeadRot()
    {
        headFakeRot = Quaternion.LookRotation(new Vector3(mousePos.x, mousePos.y, head.position.z) - head.position, !faceRight ? Vector3.forward : Vector3.back);
    }

    public virtual void reload()
    {
        if (reloading || (double)ammo >= (double)playerScript.ammoFullClip[(int)weapon] || (double)weapon == 0.0)
            return;
        reloading = true;
        if ((double)weapon == 1.0)
            animator.Play("SinglePistolReload", 5, 0.0f);
        else if ((double)weapon == 2.0 || (double)weapon == 4.0)
            animator.Play("DoublePistolReload", 5, 0.0f);
        else if ((double)weapon == 6.0)
            animator.Play("ShotgunReload", 5, 0.0f);
        else
            animator.Play("SinglePistolReload", 5, 0.0f);
    }

    public virtual void finishedReloading()
    {
        if (!reloading)
            return;
        reloading = false;
        ammo = playerScript.ammoFullClip[(int)weapon];
    }

    public virtual void shotgunReload()
    {
        if (!reloading)
            return;
        ++ammo;
        if ((double)ammo >= (double)playerScript.ammoFullClip[(int)weapon])
            return;
        animator.CrossFade("ShotgunReload", 0.05f, 5, 0.5f);
    }

    public virtual void shotgunFinishedReloading()
    {
        reloading = false;
    }

    public virtual void emitClip(int h)
    {
        if ((double)weapon == 1.0 || (double)weapon == 3.0)
        {
            pistolClipParticle.Emit(root.generateEmitParams(handR.position, Vector3.zero, 0.2f, 2f, Color.white), 1);
        }
        else
        {
            if ((double)weapon != 2.0 && (double)weapon != 4.0)
                return;
            if (h == 1)
                pistolClipParticle.Emit(root.generateEmitParams(handR.position, Vector3.zero, 0.2f, 2f, Color.white), 1);
            else
                pistolClipParticle.Emit(root.generateEmitParams(handL.position, Vector3.zero, 0.2f, 2.3f, Color.white), 1);
        }
    }

    public virtual void speak(string txt, float speed, bool additive)
    {
        if (!(motorcycle == null) || root.trailerMode)
            return;
        enemySpeechHandlerScript = (EnemySpeechHandlerScript)GetComponent(typeof(EnemySpeechHandlerScript));
        if (enemySpeechHandlerScript == null)
            enemySpeechHandlerScript = (EnemySpeechHandlerScript)gameObject.AddComponent(typeof(EnemySpeechHandlerScript));
        enemySpeechHandlerScript.speak(txt, speed, additive);
    }

    public virtual void speakNoAnimation(string txt, float speed, bool additive)
    {
        enemySpeechHandlerScript = (EnemySpeechHandlerScript)GetComponent(typeof(EnemySpeechHandlerScript));
        if (enemySpeechHandlerScript == null)
            enemySpeechHandlerScript = (EnemySpeechHandlerScript)gameObject.AddComponent(typeof(EnemySpeechHandlerScript));
        enemySpeechHandlerScript.dontAnimate = true;
        enemySpeechHandlerScript.speak(txt, speed, additive);
    }

    public virtual float calculateActualBulletHitStrength()
    {
        return enemyType != 3 && enemyType != 2 && (bulletKillOnHeadshot && !alwaysGetBulletHitHeadshot) && (bulletHitName == "Head" && (double)bulletTimeAlive != 999.0) ? (0.5f + bulletStrength) * damageMultiplier : (!bulletHitName.Contains("Leg") ? bulletStrength : bulletStrength / 1.5f) * damageMultiplier;
    }

    private bool disableWeaponPickUpDoThing(Transform trnsfrm)
    {
        int num1 = !disableWeaponPickup ? 1 : 0;
        if (num1 != 0)
            return num1 != 0;
        int num2 = disableWeaponPickup ? 1 : 0;
        if (num2 != 0)
            num2 = trnsfrm != pistolR ? 1 : 0;
        if (num2 != 0)
            num2 = trnsfrm != pistolL ? 1 : 0;
        if (num2 != 0)
            num2 = trnsfrm != uziR ? 1 : 0;
        if (num2 != 0)
            num2 = trnsfrm != uziL ? 1 : 0;
        if (num2 != 0)
            num2 = trnsfrm != shotgun ? 1 : 0;
        if (num2 != 0)
            num2 = trnsfrm != machineGun ? 1 : 0;
        return num2 == 0 ? num2 != 0 : trnsfrm != sniper;
    }
}
