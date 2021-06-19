// Decompiled with JetBrains decompiler
// Type: PlayerScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using Boo.Lang.Runtime;
using Rewired;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityScript.Lang;
using UnityStandardAssets.ImageEffects;

[Serializable]
public class PlayerScript : MonoBehaviour
{

    public MeshRenderer[] allWepRenderers;


    public int playerNr;
    public bool isEnemy;
    public bool isEnemyActivated;
    private float isEnemyActivatedTimer;
    private float isEnemyDodgeCoolDown;
    private float isEnemyFireCoolDown;
    private Transform actualPlayer;
    private Transform actualPlayerKickFoot;
    private PlayerScript actualPlayerScript;
    public bool onMotorcycle;
    private Transform motorcyclePlayerPos;
    private MotorcycleScript motorcycleScript;
    public bool pedroBoss;
    public bool doGiveFullAmmo;
    public int forceWeaponAtStart;
    public bool propellerHat;
    public bool skyfall;
    private float skyfallYPos;
    [Header("Audio")]
    public bool dontMakeFootstepSound;
    public AudioClip lowAmmoSound;
    public AudioClip emptyGunSound;
    public AudioClip[] weaponSound;
    public AudioClip grenadeLauncher;
    private AudioSource handRAudio;
    private AudioSource handLAudio;
    private AudioSource ammoLeftAudio;
    private AudioSource kickWooshAudio;
    private AudioSource playerJumpAudio;
    public AudioClip[] playerJumpSound;
    public AudioClip[] playerWallJumpSound;
    public AudioClip kickWooshSound;
    public AudioClip kickEnemyHitSound;
    private AudioSource movementSound;
    private Vector3 prevHandRPos;
    private Vector3 prevFootRPos;
    private AudioSource dodgeAudioSource;
    public AudioClip bulletDodgeSound;
    public AudioClip splitAimStartSound;
    public AudioClip splitAimStopSound;
    [HideInInspector]
    public AudioSource playerAudioSource;
    private AudioSource hudAudioSource;
    public AudioClip weaponPickUpSound;
    public AudioClip healthPickUpSound;
    public AudioClip itemPickUpFullSound;
    private PlayerAnimationScripts playerAnimationsScript;
    private RootScript root;
    private RootSharedScript rootShared;
    private StatsTrackerScript statsTracker;
    private InputHelperScript inputHelperScript;
    private Rigidbody rBody;
    private SphereCollider playerCollider;
    public float targetXSpeed;
    public float xSpeed;
    public float ySpeed;
    [Header("Other Stuff")]
    public Transform groundTransform;
    public Vector3 followSpeed;
    private float lastXSpeedThatWasntZero;
    public float targetRotation;
    public float rotationSpeed;
    public bool onGround;
    private bool okToStand;
    private bool wallTouch;
    private bool wallTouchRight;
    private bool wallTouchLeft;
    public Transform wallTouchRightTransform;
    public Transform wallTouchLeftTransform;
    public bool justWallJumped;
    public bool justJumpedFromSkateboard;
    public float wallJumpTimer;
    public float dontAllowWallJumpTimer;
    public bool justJumped;
    public bool jumpedFromSwinging;
    private float zPushBack;
    private float legLength;
    private float topLength;
    private float standLegLength;
    private float crouchLegLength;
    private float raycastWidthRange;
    public bool overrideControls;
    private float disableInputTimer;
    public bool dontResetMousePosOnOverrideControls;
    public float kXDir;
    public bool kJump;
    public bool kJumpHeldDown;
    public bool kAction;
    public bool kCrouch;
    public bool kFire;
    private float kFireTriggerPrev;
    private bool kFireDown;
    public bool kSecondaryAim;
    private bool kSecondaryAimDown;
    public bool kReload;
    private float kScrollWheel;
    private bool kScrollWheelDown;
    private float kScrollWheelCoolDownTimer;
    public bool kUse;
    public bool kUseHeldDown;
    public bool kChangeWeapon;
    public bool kDodge;
    private bool kDodgeDown;
    private bool kPunch;
    public bool floatJump;
    private float jumpCoolDown;
    private bool rolling;
    private float framesSinceRollStart;
    public bool startedRolling;
    private float timescale;
    public Animator playerAnimator { get { return animator; } }
    private Animator animator;
    private Transform playerGraphics;
    public Vector3 mousePos;
    public Vector3 mousePos2;
    private Vector3 mousePosWithZOffset;
    private Camera curCamera;
    private CameraScript cameraScript;
    private VignetteAndChromaticAberration cameraVignetteOverlay;
    private ColorCorrectionCurves bloodCameraColourCorrection;
    private Image screenCornerBlood;
    private bool seperateAim;

    public Transform center;

    public Transform lowerBackPublic { get { return lowerBack; } }
    private Transform lowerBack;
    private Transform upperBack;
    public Transform neckPublic { get { return neck; } }
    private Transform neck;
    public Transform headPublic { get { return head; } }
    private Transform head;
    public Transform shoulderRPublic { get { return shoulderR; } }
    private Transform shoulderR;
    public Transform upperArmRPublic { get { return upperArmR; } }
    private Transform upperArmR;
    public Transform lowerArmRPublic { get { return lowerArmR; } }
    private Transform lowerArmR;
    private Transform handR;
    public Transform handRPublic { get { return handR; } }
    public Transform shoulderLPublic { get { return shoulderL; } }
    private Transform shoulderL;
    public Transform upperArmLPublic { get { return upperArmL; } }
    private Transform upperArmL;
    public Transform lowerArmLPublic { get { return lowerArmL; } }
    private Transform lowerArmL;
    private Transform handL;
    public Transform handLPublic { get { return handL; } }

    public Transform bulletPointRPublic { get { return bulletPointR; } }
    private Transform bulletPointR;

    public Transform bulletPointLPublic { get { return bulletPointL; } }
    private Transform bulletPointL;

    public Transform footRPublic { get { return footR; } }
    private Transform footR;
    private Transform grabPoint;
    public Transform bulletPointR2Public { get { return bulletPointR2; } }
    private Transform bulletPointR2;
    private Transform footTipR;
    public Transform upperLegRPublic { get { return upperLegR; } }
    private Transform upperLegR;
    public Transform lowerLegRPublic { get { return lowerLegR; } }
    private Transform lowerLegR;
    private Transform footL;
    public Transform footLPublic { get { return footL; } }
    private Transform footTipL;
    private Transform upperLegL;
    public Transform upperLegLPublic { get { return upperLegL; } }
    private Transform lowerLegL;
    public Transform lowerLegLPublic { get { return lowerLegL; } }
    private Transform hipL;
    private Transform hipR;
    private ParticleSystem bloodParticle;
    private ParticleSystem bloodMistParticle;
    private Transform pistolR;
    private Transform pistolL;
    private Transform uziR;
    private Transform uziL;
    private Transform machineGun;
    private Transform shotgun;
    private Transform turretGun;
    private Transform turretGunTop;
    private Transform shockRifle;
    private Transform shockRifleShield;
    private Material shockRifleShieldMaterial;
    private BoxCollider shockRifleShieldCollider;
    private Transform rifle;
    private Transform rifleLaser;
    private Transform crossbow;
    private Transform crossbowPipe;
    private Transform crossbowBow1;
    private Transform crossbowBow2;
    private GameObject[] crossbowArrows;
    private TurretGunScript turretGunScript;

    public GameObject bulletPointRAimTargetPublic { get { return bulletPointRAimTarget; } }
    private GameObject bulletPointRAimTarget;

    public GameObject bulletPointLAimTargetPublic { get { return bulletPointLAimTarget; } }
    private GameObject bulletPointLAimTarget;

    public GameObject bulletPointLRAimTarget2Public { get { return bulletPointRAimTarget2; } }
    private GameObject bulletPointRAimTarget2;

    private Quaternion lowerBackFakeRot;
    private Quaternion headFakeRot;
    private Quaternion fakeUpperArmLRotation;
    private Quaternion shoulderRFakeRot;
    private Quaternion shoulderLFakeRot;
    private float aimBlend;
    private float aimBlendTarget;
    private float lookAimBlend;
    private float lookAimBlendTarget;
    private float fightPoseBlend;
    private float fightPoseBlendTimer;
    public bool aimWithLeftArm;
    private bool twoHandedWeapon;
    private bool automaticWeapon;
    public float fireDelay;
    private float secondaryFireDelay;
    public bool fireLeftGunPublic { get { return fireLeftGun; } }
    private bool fireLeftGun;
    private float punchAnimNr;
    public float punchTimer;
    public bool meleeKickHit;
    private bool didShootWhileKicking;
    private float meleeWallKickDetection;
    private float justJumpedAnimationBoolTimer;
    public float crouchAmount;
    private ParticleSystem shellParticle;
    private ParticleSystem pistolClipParticle;
    private ParticleSystem smokeParticle;
    public ParticleSystem smokeParticlePublic { get { return smokeParticle; } }
    public ParticleSystem shellParticlePublic { get { return shellParticle; } }
    public ParticleSystem pistolClipParticlePublic { get { return pistolClipParticle; } }
    private bool rollInMidAir;
    private bool fightMode;
    private bool pushedAgainstWall;
    private float dualWieldFrontTurnSmoothing;
    private float topDuckAmount;
    private float upperArmAnimationBlend;
    private float upperArmAnimationBlendTimer;
    private Quaternion armROffset;
    private Quaternion armROffset2;
    private Quaternion armROffset3;
    private Quaternion armLOffset;
    public bool fireWeapon;
    private bool fireSecondaryWeapon;
    private float fireWeaponDelay;
    private bool fireWeaponDelayDoOnce;
    private float visualCrouchAmount;
    private float prevVisualCrouchAmount;
    private LayerMask layerMask;
    private LayerMask layerMask2;
    private LayerMask layerMaskIncEnemies;
    private LayerMask layerMaskIncEnemiesMinusLvlColIgnoreBullet;
    private LayerMask layerMaskIncEnemiesAndEnemyGameCollision;
    private LayerMask layerMaskIncEnemiesAndEnemyGameCollisionWithoutBulletPassthrough;
    private LayerMask layerMaskOnlyEnemies;
    private float sideStepAmount;
    private bool dualAimMode;
    public float health;
    private TextMeshProUGUI ammoText;
    private TextMeshProUGUI secondaryAmmoText;
    private RectTransform ammoHudAmmoBar;
    private RectTransform ammoHudBullet;
    private Image[] ammoHudBullets;
    private int maxAmountOfBullets;
    private RectTransform ammoHudInfiniteSymbol;
    private GameObject ammoHudAmmoAlert;
    private RectTransform weaponPanel;
    private Vector2 weaponPanelStartPos;
    public bool reloading;
    private float reloadLayerWeight;
    private bool fireWhileReloading;
    private bool allowFireWhileReloading;
    private float reloadingSafteyTimer;
    private bool flippingTable;
    private RectTransform mainCursor;
    private Image mainCursorImage;
    private Color mainCursorImageStartColour;
    private RectTransform secondaryCursor;
    private Image secondaryCursorImage;
    private UIWeaponSelectorScript uiWeaponSelectorScript;
    private RectTransform aimHelper;
    private Image aimHelperImage;
    private Color aimHelperImageStartColour;
    private float aimHelperDistance;
    private Image mainCursorDisabledImage;
    [HideInInspector]
    public Vector3 fakeGamepadMousePos;
    private RectTransform hudCanvasRect;
    private Light fireLightR;
    private Light fireLightL;
    public float[] ammo;
    public float[] ammoTotal;
    public bool[] weaponActive;
    public float[] ammoFullClip;
    private bool[] usesSecondaryAmmo;
    public float[] secondaryAmmo;
    public int weapon;
    public int nrOfWeapons;
    public bool faceRight;
    public GameObject bullet;
    public GameObject muzzleFlash;
    public GameObject machineGunGrenade;
    public GameObject shockRifleBullet;
    public GameObject crossbowArrow;
    public bool bulletHit;
    private bool bulletHitDoOnce;
    public Quaternion bulletHitRotation;
    public Vector3 bulletHitVel;
    public float bulletStrength;
    public Transform bulletFromTransform;
    public bool bulletHitDoSound;
    public float timeSinceBulletHit;
    public float grabLength;
    public float ropeLength;
    public Transform swingTransform;
    public bool swinging;
    public bool hipSwing;
    private float framesInSwingMode;
    private float swingOnGroundContractTimer;
    private float armTwist;
    public float extraJumpPower;
    private Quaternion lowerBackTargetLookRotation;
    private float shootFromCoverLayerWeight;
    private float pushedAgainstWallShootTimer;
    private float heartBeat;
    private float heartBeatMultiplier;
    public float recoverTimer;
    private AudioSource heartBeatAudioSource;
    private AudioSource playerHitAudioSource;
    private RectTransform healthBar;
    private Vector2 healthBarStartPos;
    private Vector2 healthBarStartSize;
    private float healthBarSpeed;
    private RectTransform healthIcon;
    private Vector2 healthIconStartPos;
    private Image healthIconImage;
    private Color healthIconImageStartColour;
    private Image healthBar1HUD;
    private Image healthBar2HUD;
    private Image healthBar3HUD;
    private RectTransform healthBar1HUDRect;
    private RectTransform healthBar2HUDRect;
    private RectTransform healthBar3HUDRect;
    private Color healthBarStartColour;
    private Image weaponHUDIcon;
    private OutlineEffect outlineEffect;
    private Transform trajectory;
    private Material trajectoryMaterial;
    private Transform[] trajBone;
    public bool cutsceneMode;
    private bool cutsceneModeDoOnce;
    public float degreesRotatedInAir;
    private GameObject blackBarTop;
    private GameObject blackBarBottom;
    private bool controlOverrideDoOnce;
    public GameObject damageIndicator;
    private DamageIndicatorScript[] damageIndicatorScripts;
    private int curDamageIndicatorScript;
    public GameObject playerHudBlood;
    private PlayerHudBloodScript[] playerHudBloodScripts;
    private int curPlayerHudBloodScript;
    public float timeSinceDodgeUsed;
    public int bulletHitsSinceDodgeUsed;
    public int bulletHitsSinceSlowMotionUsed;
    public float timeSinceMeleeUsed;
    public float timeSinceShotFired;
    public float timeSinceKSecondaryAimForHipSwingPurpose;
    public float timeSinceSplitAim;
    public float timeSinceSniperAim;
    public float timeSinceGrenadeLaunched;
    private int weaponAdjusted;
    public Vector3 interpolatedPosition;
    private int i;
    private float prevTargetXSpeed;
    private bool withinOkToStandAngles;
    private RaycastHit legRayHit;
    private PlatformLiftScript tempPlatformScript;
    private float tempTopDuckAmountCheck;
    private RaycastHit topRayHit;
    private float hitDist;
    private RaycastHit rollInMidAirRayHit;
    private RaycastHit sideRayHit;
    private bool tempHittingGlass;
    private bool tempHittingGlassL;
    private int b;
    private bool almostOkToStand;
    private bool aboutToHitGround;
    private float upperArmRTempVarForHandRotation;
    private float upperArmLTempVarForHandRotation;
    private float dualWieldFrontTurnSmoothing2;
    private float tempShoulderRBulletPointR2DistanceMultiplier;
    private float tempShoulderRBulletPointR2Distance;
    private RaycastHit armRayHit;
    private float invertedNormalizedTopDuckAmount;
    private float kickBack;
    private int faceRightInvertMultiplier;
    private float dst;
    private Vector3 vect;
    private RaycastHit footRayHit;
    private float legRotOffset;
    private float tempLegLength;
    private float tempDist;
    private GameObject tempBulletVar;
    private BulletScript tempBulletScript;
    private GlassPieceScript tempGlassPieceScript;
    private float tempYPosCheck;
    private SkinnedMeshRenderer vHandR;
    private SkinnedMeshRenderer vHandL;
    private int ammoTotalWeaponToDisplay;
    private float amountToReload;
    private int ammoTotalWeaponToUse;
    private RaycastHit punchRayHit;
    private float zOffset;
    private EnemyScript enemyScript;
    private RaycastHit rayHit;
    private Vector3 inverseTransformPointMousePos;
    private float mouseAngle;
    private float bodyTargetAngle;
    private Vector3 twoHandedWeaponDirectionalVector;
    private float kickTimer;
    private float kickFreezeTimer;
    private float touchedEnemyTimer;
    public float speechBubbleCoolDownTimer;
    public float aimSpreadPublic { get { return aimSpread; } }
    private float aimSpread;
    public float aimSpread2Public { get { return aimSpread2; } }
    private float aimSpread2;
    private UINewWeaponScript newWeaponPromptScript;
    private RectTransform ammoHud;
    private RectTransform healthHud;
    private RectTransform scoreHud;
    public float healthPackEffect;
    public float kJumpDownTimer;
    public float justLandedTimer;
    private float onGroundTimer;
    public bool demoLevel;
    private bool justPulseJumped;
    private int shotsInARow;
    private bool justJumpedOffEnemy;
    public float dramaticEntranceTimer;
    public float dodgeSpinAngle;
    public bool dodging;
    public float dodgingCoolDown;
    private float dodgeAmount;
    public int nrOfDodgeSpins;
    private float motorcycleAim;
    public Vector3 lastPosOnGround;
    public float inAirTimer;
    public bool dontLockTowall;
    public bool disableFloatJump;
    private int gamepadMode;
    private Vector2 extraGamepadPos;
    public bool gamepad;
    private Vector2 gamepadAimOffset;
    private Vector2 targetGamepadAimOffset;
    private Vector2 targetGamepadAimSpeed;
    private Vector2 rStick;
    private Vector2 rStick2;
    private Vector2 actualRStick;
    private bool rStickAiming;
    private float dontFaceWalkingDirTimer;
    private bool movedSinceUsingRStick;
    private float rStick2TargetAngle;
    private Quaternion rStick2TargetQuaternion;
    private Quaternion rStick2Quaternion;
    private float rStickQuickTurnTimer;
    public AnimationCurve gamepadAimSensCurve;
    private float prevRStickMag;
    private float rStickAimCoolDown;
    private bool rStickInOuterFieldDoOnce;
    private Vector3 gamepadAimingReferencePoint;
    private bool doNormalAutoAim;
    public Transform normalAutoAimEnemy;
    public Transform normalAutoAimEnemyForCinematicCamera;
    private Transform secondaryAimTarget;
    private Vector3 secondaryAimOffset;
    private EnemyScript secondaryAimEnemyScript;
    public string inputPText;
    private GameObject bossHUD;
    private CanvasGroup bossHUDCanvasGroup;
    private RectTransform enemyHealthBar;
    private Image healthBarImg;
    private Color healthBarStartColourEnemy;
    private float walkTimer;
    private float runBend;
    public AudioMixerSnapshot normalStateAudioSnapshot;
    public AudioMixerSnapshot motorcycleNormalStateAudioSnapshot;
    public AudioMixerSnapshot hurtStateAudioSnapshot;
    public AudioMixerSnapshot actionStateAudioSnapshot;
    private float itemKickCoolDown;
    private float orgFogEndValue;
    private float fogEndTarget;
    private bool disableShootingDoOnce;
    public Controller lastUsedController;
    private Player player;
    public Vector3 prevFixedPos;
    public Vector3 physicsInterpolationOffset;
    public float speedModifier;
    private Vector3 interpolatedPositionS;
    private Vector3 prevFixedPosS;
    private Vector3 physicsInterpolationOffsetS;
    private float isEnemyActivatedTimerS;
    private float isEnemyDodgeCoolDownS;
    private float isEnemyFireCoolDownS;
    private float targetXSpeedS;
    private float xSpeedS;
    private float ySpeedS;
    private Vector3 followSpeedS;
    private float lastXSpeedThatWasntZeroS;
    private float targetRotationS;
    private float rotationSpeedS;
    private bool onGroundS;
    private bool justWallJumpedS;
    private float wallJumpTimerS;
    private bool justJumpedS;
    private bool jumpedFromSwingingS;
    private float zPushBackS;
    private bool overrideControlsS;
    private bool kActionS;
    private bool rollingS;
    private float framesSinceRollStartS;
    private bool startedRollingS;
    private float timescaleS;
    private Vector3 mousePosS;
    private Vector3 mousePos2S;
    private bool seperateAimS;
    private Transform grabPointS;
    private Quaternion lowerBackFakeRotS;
    private Quaternion headFakeRotS;
    private Quaternion fakeUpperArmLRotationS;
    private Quaternion shoulderRFakeRotS;
    private Quaternion shoulderLFakeRotS;
    private float aimBlendS;
    private float aimBlendTargetS;
    private float lookAimBlendS;
    private float lookAimBlendTargetS;
    private float fightPoseBlendS;
    private float fightPoseBlendTimerS;
    private bool aimWithLeftArmS;
    private bool twoHandedWeaponS;
    private bool automaticWeaponS;
    private float fireDelayS;
    private float secondaryFireDelayS;
    private bool fireLeftGunS;
    private float punchAnimNrS;
    private float punchTimerS;
    private float justJumpedAnimationBoolTimerS;
    private float crouchAmountS;
    private bool rollInMidAirS;
    private bool fightModeS;
    private bool pushedAgainstWallS;
    private float dualWieldFrontTurnSmoothingS;
    private float topDuckAmountS;
    private float upperArmAnimationBlendS;
    private float upperArmAnimationBlendTimerS;
    private Quaternion armROffsetS;
    private Quaternion armROffset2S;
    private Quaternion armROffset3S;
    private Quaternion armLOffsetS;
    private bool fireWeaponS;
    private bool fireSecondaryWeaponS;
    private float fireWeaponDelayS;
    private bool fireWeaponDelayDoOnceS;
    private float visualCrouchAmountS;
    private float prevVisualCrouchAmountS;
    private float sideStepAmountS;
    private bool dualAimModeS;
    private float healthS;
    private bool reloadingS;
    private float reloadLayerWeightS;
    private bool fireWhileReloadingS;
    private bool allowFireWhileReloadingS;
    private bool flippingTableS;
    private int weaponS;
    private bool faceRightS;
    private float grabLengthS;
    private float ropeLengthS;
    private Transform swingTransformS;
    private bool swingingS;
    private bool hipSwingS;
    private float framesInSwingModeS;
    private float armTwistS;
    private float extraJumpPowerS;
    private Quaternion lowerBackTargetLookRotationS;
    private float shootFromCoverLayerWeightS;
    private float pushedAgainstWallShootTimerS;
    private float heartBeatS;
    private float recoverTimerS;
    private bool cutsceneModeS;
    private bool cutsceneModeDoOnceS;
    private float degreesRotatedInAirS;
    private bool controlOverrideDoOnceS;
    private float kickTimerS;
    private float kickFreezeTimerS;
    private float touchedEnemyTimerS;
    private float speechBubbleCoolDownTimerS;
    private float aimSpreadS;
    private float aimSpread2S;
    private bool justPulseJumpedS;
    private bool floatJumpS;
    private bool disableShootingDoOnceS;
    private float enemyHealthBarLocalScaleXS;
    private float timeSinceKSecondaryAimForHipSwingPurposeS;
    private float[] ammoS;
    private float[] ammoTotalS;
    private bool[] weaponActiveS;
    private float[] secondaryAmmoS;
    [NonSerialized]
    public static PlayerScript PlayerInstance;

    public PlayerScript()
    {
        this.lastXSpeedThatWasntZero = 0.1f;
        this.okToStand = true;
        this.legLength = 1.8f;
        this.topLength = 1.8f;
        this.standLegLength = 1.6f;
        this.crouchLegLength = 0.6f;
        this.raycastWidthRange = 0.56f;
        this.timescale = 1f;
        this.aimBlend = 1f;
        this.aimBlendTarget = 1f;
        this.lookAimBlend = 1f;
        this.lookAimBlendTarget = 1f;
        this.aimWithLeftArm = true;
        this.punchAnimNr = 1f;
        this.health = 1f;
        this.faceRight = true;
        this.grabLength = 7f;
        this.ropeLength = 8f;
        this.gamepadMode = 5;
    }

    public virtual void saveState()
    {
        this.interpolatedPositionS = this.interpolatedPosition;
        this.prevFixedPosS = this.prevFixedPos;
        this.physicsInterpolationOffsetS = this.physicsInterpolationOffset;
        this.isEnemyActivatedTimerS = this.isEnemyActivatedTimer;
        this.isEnemyDodgeCoolDownS = this.isEnemyDodgeCoolDown;
        this.isEnemyFireCoolDownS = this.isEnemyFireCoolDown;
        this.targetXSpeedS = this.targetXSpeed;
        this.xSpeedS = this.xSpeed;
        this.ySpeedS = this.ySpeed;
        this.followSpeedS = this.followSpeed;
        this.lastXSpeedThatWasntZeroS = this.lastXSpeedThatWasntZero;
        this.targetRotationS = this.targetRotation;
        this.rotationSpeedS = this.rotationSpeed;
        this.onGroundS = this.onGround;
        this.justWallJumpedS = this.justWallJumped;
        this.wallJumpTimerS = this.wallJumpTimer;
        this.justJumpedS = this.justJumped;
        this.jumpedFromSwingingS = this.jumpedFromSwinging;
        this.zPushBackS = this.zPushBack;
        this.overrideControlsS = this.overrideControls;
        this.kActionS = this.kAction;
        this.rollingS = this.rolling;
        this.framesSinceRollStartS = this.framesSinceRollStart;
        this.startedRollingS = this.startedRolling;
        this.timescaleS = this.timescale;
        this.mousePosS = this.mousePos;
        this.mousePos2S = this.mousePos2;
        this.seperateAimS = this.seperateAim;
        this.grabPointS = this.grabPoint;
        this.lowerBackFakeRotS = this.lowerBackFakeRot;
        this.headFakeRotS = this.headFakeRot;
        this.fakeUpperArmLRotationS = this.fakeUpperArmLRotation;
        this.shoulderRFakeRotS = this.shoulderRFakeRot;
        this.shoulderLFakeRotS = this.shoulderLFakeRot;
        this.aimBlendS = this.aimBlend;
        this.aimBlendTargetS = this.aimBlendTarget;
        this.lookAimBlendS = this.lookAimBlend;
        this.lookAimBlendTargetS = this.lookAimBlendTarget;
        this.fightPoseBlendS = this.fightPoseBlend;
        this.fightPoseBlendTimerS = this.fightPoseBlendTimer;
        this.aimWithLeftArmS = this.aimWithLeftArm;
        this.twoHandedWeaponS = this.twoHandedWeapon;
        this.automaticWeaponS = this.automaticWeapon;
        this.fireDelayS = this.fireDelay;
        this.secondaryFireDelayS = this.secondaryFireDelay;
        this.fireLeftGunS = this.fireLeftGun;
        this.punchAnimNrS = this.punchAnimNr;
        this.punchTimerS = this.punchTimer;
        this.justJumpedAnimationBoolTimerS = this.justJumpedAnimationBoolTimer;
        this.crouchAmountS = this.crouchAmount;
        this.rollInMidAirS = this.rollInMidAir;
        this.fightModeS = this.fightMode;
        this.pushedAgainstWallS = this.pushedAgainstWall;
        this.dualWieldFrontTurnSmoothingS = this.dualWieldFrontTurnSmoothing;
        this.topDuckAmountS = this.topDuckAmount;
        this.upperArmAnimationBlendS = this.upperArmAnimationBlend;
        this.upperArmAnimationBlendTimerS = this.upperArmAnimationBlendTimer;
        this.armROffsetS = this.armROffset;
        this.armROffset2S = this.armROffset2;
        this.armROffset3S = this.armROffset3;
        this.armLOffsetS = this.armLOffset;
        this.fireWeaponS = this.fireWeapon;
        this.fireSecondaryWeaponS = this.fireSecondaryWeapon;
        this.fireWeaponDelayS = this.fireWeaponDelay;
        this.fireWeaponDelayDoOnceS = this.fireWeaponDelayDoOnce;
        this.visualCrouchAmountS = this.visualCrouchAmount;
        this.prevVisualCrouchAmountS = this.prevVisualCrouchAmount;
        this.sideStepAmountS = this.sideStepAmount;
        this.dualAimModeS = this.dualAimMode;
        this.healthS = this.health;
        this.reloadingS = this.reloading;
        this.reloadLayerWeightS = this.reloadLayerWeight;
        this.fireWhileReloadingS = this.fireWhileReloading;
        this.allowFireWhileReloadingS = this.allowFireWhileReloading;
        this.flippingTableS = this.flippingTable;
        this.weaponS = this.weapon;
        this.faceRightS = this.faceRight;
        this.grabLengthS = this.grabLength;
        this.ropeLengthS = this.ropeLength;
        this.swingTransformS = this.swingTransform;
        this.swingingS = this.swinging;
        this.hipSwingS = this.hipSwing;
        this.framesInSwingModeS = this.framesInSwingMode;
        this.armTwistS = this.armTwist;
        this.extraJumpPowerS = this.extraJumpPower;
        this.lowerBackTargetLookRotationS = this.lowerBackTargetLookRotation;
        this.shootFromCoverLayerWeightS = this.shootFromCoverLayerWeight;
        this.pushedAgainstWallShootTimerS = this.pushedAgainstWallShootTimer;
        this.heartBeatS = this.heartBeat;
        this.recoverTimerS = this.recoverTimer;
        this.cutsceneModeS = this.cutsceneMode;
        this.cutsceneModeDoOnceS = this.cutsceneModeDoOnce;
        this.degreesRotatedInAirS = this.degreesRotatedInAir;
        this.controlOverrideDoOnceS = this.controlOverrideDoOnce;
        this.kickTimerS = this.kickTimer;
        this.kickFreezeTimerS = this.kickFreezeTimer;
        this.touchedEnemyTimerS = this.touchedEnemyTimer;
        this.speechBubbleCoolDownTimerS = this.speechBubbleCoolDownTimer;
        this.aimSpreadS = this.aimSpread;
        this.aimSpread2S = this.aimSpread2;
        this.justPulseJumpedS = this.justPulseJumped;
        this.floatJumpS = this.floatJump;
        this.disableShootingDoOnceS = this.disableShootingDoOnce;
        this.timeSinceKSecondaryAimForHipSwingPurposeS = this.timeSinceKSecondaryAimForHipSwingPurpose;
        if (this.isEnemy)
            this.enemyHealthBarLocalScaleXS = this.enemyHealthBar.localScale.x;
        int num = new int();
        for (int index = 0; index < Extensions.get_length((System.Array)this.ammo); ++index)
            this.ammoS[index] = this.ammo[index];
        for (int index = 0; index < Extensions.get_length((System.Array)this.ammoTotal); ++index)
            this.ammoTotalS[index] = this.ammoTotal[index];
        for (int index = 0; index < Extensions.get_length((System.Array)this.weaponActive); ++index)
            this.weaponActiveS[index] = this.weaponActive[index];
        for (int index = 0; index < Extensions.get_length((System.Array)this.secondaryAmmo); ++index)
            this.secondaryAmmoS[index] = this.secondaryAmmo[index];
    }

    public virtual void loadState()
    {
        this.interpolatedPosition = this.interpolatedPositionS;
        this.prevFixedPos = this.prevFixedPosS;
        this.physicsInterpolationOffset = this.physicsInterpolationOffsetS;
        this.isEnemyActivatedTimer = this.isEnemyActivatedTimerS;
        this.isEnemyDodgeCoolDown = this.isEnemyDodgeCoolDownS;
        this.isEnemyFireCoolDown = this.isEnemyFireCoolDownS;
        this.onGround = this.onGroundS;
        if (this.onGround)
        {
            this.targetXSpeed = 0.0f;
            this.xSpeed = 0.0f;
            int num1 = 0;
            Vector3 velocity = this.rBody.velocity;
            double num2 = (double)(velocity.x = (float)num1);
            Vector3 vector3 = this.rBody.velocity = velocity;
        }
        else
        {
            this.targetXSpeed = this.targetXSpeedS;
            this.xSpeed = this.xSpeedS;
        }
        this.ySpeed = this.ySpeedS;
        this.followSpeed = this.followSpeedS;
        this.lastXSpeedThatWasntZero = this.lastXSpeedThatWasntZeroS;
        this.targetRotation = this.targetRotationS;
        this.rotationSpeed = this.rotationSpeedS;
        this.justWallJumped = this.justWallJumpedS;
        this.wallJumpTimer = this.wallJumpTimerS;
        this.justJumped = this.justJumpedS;
        this.jumpedFromSwinging = this.jumpedFromSwingingS;
        this.zPushBack = this.zPushBackS;
        this.overrideControls = this.overrideControlsS;
        this.kAction = this.kActionS;
        this.rolling = this.rollingS;
        this.framesSinceRollStart = this.framesSinceRollStartS;
        this.startedRolling = this.startedRollingS;
        this.timescale = this.timescaleS;
        this.mousePos = this.mousePosS;
        this.mousePos2 = this.mousePos2S;
        this.seperateAim = this.seperateAimS;
        this.grabPoint = this.grabPointS;
        this.lowerBackFakeRot = this.lowerBackFakeRotS;
        this.headFakeRot = this.headFakeRotS;
        this.fakeUpperArmLRotation = this.fakeUpperArmLRotationS;
        this.shoulderRFakeRot = this.shoulderRFakeRotS;
        this.shoulderLFakeRot = this.shoulderLFakeRotS;
        this.aimBlend = this.aimBlendS;
        this.aimBlendTarget = this.aimBlendTargetS;
        this.lookAimBlend = this.lookAimBlendS;
        this.lookAimBlendTarget = this.lookAimBlendTargetS;
        this.fightPoseBlend = this.fightPoseBlendS;
        this.fightPoseBlendTimer = this.fightPoseBlendTimerS;
        this.aimWithLeftArm = this.aimWithLeftArmS;
        this.twoHandedWeapon = this.twoHandedWeaponS;
        this.automaticWeapon = this.automaticWeaponS;
        this.fireDelay = this.fireDelayS;
        this.secondaryFireDelay = this.secondaryFireDelayS;
        this.fireLeftGun = this.fireLeftGunS;
        this.punchAnimNr = this.punchAnimNrS;
        this.punchTimer = this.punchTimerS;
        this.justJumpedAnimationBoolTimer = this.justJumpedAnimationBoolTimerS;
        this.crouchAmount = this.crouchAmountS;
        this.rollInMidAir = this.rollInMidAirS;
        this.fightMode = this.fightModeS;
        this.pushedAgainstWall = this.pushedAgainstWallS;
        this.dualWieldFrontTurnSmoothing = this.dualWieldFrontTurnSmoothingS;
        this.topDuckAmount = this.topDuckAmountS;
        this.upperArmAnimationBlend = this.upperArmAnimationBlendS;
        this.upperArmAnimationBlendTimer = this.upperArmAnimationBlendTimerS;
        this.armROffset = this.armROffsetS;
        this.armROffset2 = this.armROffset2S;
        this.armROffset3 = this.armROffset3S;
        this.armLOffset = this.armLOffsetS;
        this.fireWeapon = false;
        this.fireSecondaryWeapon = this.fireSecondaryWeaponS;
        this.fireWeaponDelay = this.fireWeaponDelayS;
        this.fireWeaponDelayDoOnce = this.fireWeaponDelayDoOnceS;
        this.visualCrouchAmount = this.visualCrouchAmountS;
        this.prevVisualCrouchAmount = this.prevVisualCrouchAmountS;
        this.sideStepAmount = this.sideStepAmountS;
        this.dualAimMode = this.dualAimModeS;
        this.health = this.healthS;
        this.reloading = this.reloadingS;
        this.reloadLayerWeight = this.reloadLayerWeightS;
        this.fireWhileReloading = this.fireWhileReloadingS;
        this.allowFireWhileReloading = this.allowFireWhileReloadingS;
        this.flippingTable = this.flippingTableS;
        // this.weapon = this.weaponS;
        this.faceRight = this.faceRightS;
        this.grabLength = this.grabLengthS;
        this.ropeLength = this.ropeLengthS;
        this.swingTransform = this.swingTransformS;
        this.hipSwing = this.hipSwingS;
        this.swinging = this.swingingS;
        this.framesInSwingMode = this.framesInSwingModeS;
        this.armTwist = this.armTwistS;
        this.extraJumpPower = this.extraJumpPowerS;
        this.lowerBackTargetLookRotation = this.lowerBackTargetLookRotationS;
        this.shootFromCoverLayerWeight = this.shootFromCoverLayerWeightS;
        this.pushedAgainstWallShootTimer = this.pushedAgainstWallShootTimerS;
        this.heartBeat = this.heartBeatS;
        this.recoverTimer = this.recoverTimerS;
        this.cutsceneMode = this.cutsceneModeS;
        this.cutsceneModeDoOnce = this.cutsceneModeDoOnceS;
        this.degreesRotatedInAir = this.degreesRotatedInAirS;
        this.controlOverrideDoOnce = this.controlOverrideDoOnceS;
        this.kickTimer = this.kickTimerS;
        this.kickFreezeTimer = this.kickFreezeTimerS;
        this.touchedEnemyTimer = this.touchedEnemyTimerS;
        this.speechBubbleCoolDownTimer = this.speechBubbleCoolDownTimerS;
        this.aimSpread = this.aimSpreadS;
        this.aimSpread2 = this.aimSpread2S;
        this.justPulseJumped = this.justPulseJumpedS;
        this.floatJump = this.floatJumpS;
        this.disableShootingDoOnce = this.disableShootingDoOnceS;
        this.timeSinceKSecondaryAimForHipSwingPurpose = this.timeSinceKSecondaryAimForHipSwingPurposeS;
        if (!this.isEnemy)
        {
            int num = new int();
            for (int index = 0; index < Extensions.get_length((System.Array)this.ammo); ++index)
                this.ammo[index] = this.ammoS[index];
            for (int index = 0; index < Extensions.get_length((System.Array)this.ammoTotal); ++index)
                this.ammoTotal[index] = this.ammoTotalS[index];
            for (int index = 0; index < Extensions.get_length((System.Array)this.weaponActive); ++index)
                this.weaponActive[index] = this.weaponActiveS[index];
            for (int index = 0; index < Extensions.get_length((System.Array)this.secondaryAmmo); ++index)
                this.secondaryAmmo[index] = this.secondaryAmmoS[index];
            this.blackBarTop.SetActive(false);
            this.blackBarBottom.SetActive(false);
            this.uiWeaponSelectorScript.prepareUI();
            this.changeWeapon((float)this.weapon);
            this.health = Mathf.Clamp01(this.health + (this.root.difficultyMode != 2 ? 0.3f : 0.5f));
            this.root.slowMotionAmount = Mathf.Clamp01(this.root.slowMotionAmount + 0.5f);
        }
        else
        {
            float healthBarLocalScaleXs = this.enemyHealthBarLocalScaleXS;
            Vector3 localScale = this.enemyHealthBar.localScale;
            double num = (double)(localScale.x = healthBarLocalScaleXs);
            Vector3 vector3 = this.enemyHealthBar.localScale = localScale;
        }
        this.animator.SetBool("Dodging", false);

        //Spawn on a random player

        /*
		if(MultiplayerManagerTest.connected)
			if(Steamworks.SteamMatchmaking.GetNumLobbyMembers(MultiplayerManagerTest.inst.globalID) > 1)
				transform.position = MultiplayerManagerTest.inst.playerObjects[UnityEngine.Random.Range(0, MultiplayerManagerTest.inst.playerObjects.Count)].transform.position;*/
    }

    public virtual void unlockAllWeapons()
    {
        for (int index = 0; index < this.weaponActive.Length; ++index)
        {
            this.weaponActive[index] = true;
            this.ammoTotal[index] = 999f;
            this.ammo[index] = this.ammoFullClip[index];
            this.secondaryAmmo[index] = 3f;
        }
        this.weaponActive[0] = this.weaponActive[10] = this.weaponActive[8] = this.weaponActive[7] = false;
        this.root.slowMotionAmount = 1f;
        if (this.isEnemy || this.root.multiplayer)
            return;
        this.uiWeaponSelectorScript.prepareUI();
        this.health = 1f;
        this.updateAmmoHUD();
    }

    public virtual void Awake()
    {
        if (!this.isEnemy)
            PlayerScript.PlayerInstance = this;
        else
            gameObject.AddComponent<NetworkedBaseTransform>();

        this.player = ReInput.players.GetPlayer(0);


        allWepRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    public virtual void Start()
    {

        if (MultiplayerManagerTest.inst == null)
            new GameObject().AddComponent<MultiplayerManagerTest>();

        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.rootShared = (RootSharedScript)GameObject.Find("RootShared").GetComponent(typeof(RootSharedScript));
        this.statsTracker = (StatsTrackerScript)GameObject.Find("RootShared").GetComponent(typeof(StatsTrackerScript));
        this.inputHelperScript = (InputHelperScript)GameObject.Find("Rewired Input Manager").GetComponent(typeof(InputHelperScript));
        this.rBody = (Rigidbody)this.GetComponent(typeof(Rigidbody));
        this.playerCollider = (SphereCollider)this.GetComponent(typeof(SphereCollider));
        if ((double)this.rootShared.modPlayerSpeed != 30.0)
        {
            if ((double)this.rootShared.modPlayerSpeed < 30.0)
                this.speedModifier = 1f - (float)((30.0 - (double)this.rootShared.modPlayerSpeed) / 100.0);
            else if ((double)this.rootShared.modPlayerSpeed > 30.0)
                this.speedModifier = 1f + (float)(((double)this.rootShared.modPlayerSpeed - 30.0) / 70.0 * 0.600000023841858);
        }
        else
            this.speedModifier = 1f;
        this.playerGraphics = this.transform.Find("PlayerGraphics");
        this.playerAnimationsScript = (PlayerAnimationScripts)this.playerGraphics.GetComponent(typeof(PlayerAnimationScripts));
        this.animator = (Animator)this.playerGraphics.GetComponent(typeof(Animator));
        this.curCamera = (Camera)GameObject.Find("Main Camera").GetComponent(typeof(Camera));
        this.cameraScript = (CameraScript)this.curCamera.GetComponent(typeof(CameraScript));
        this.cameraVignetteOverlay = (VignetteAndChromaticAberration)this.curCamera.GetComponent(typeof(VignetteAndChromaticAberration));
        this.bloodCameraColourCorrection = (ColorCorrectionCurves)this.curCamera.GetComponent(typeof(ColorCorrectionCurves));
        this.screenCornerBlood = (Image)GameObject.Find("HUD/Canvas/DamageCanvas/ScreenCornerBlood").GetComponent(typeof(Image));
        Transform transform = GameObject.Find("HUD/Canvas/DamageCanvas").transform;
        this.playerHudBloodScripts = new PlayerHudBloodScript[3];
        this.playerHudBloodScripts[0] = (PlayerHudBloodScript)UnityEngine.Object.Instantiate<GameObject>(this.playerHudBlood).GetComponent(typeof(PlayerHudBloodScript));
        this.playerHudBloodScripts[1] = (PlayerHudBloodScript)UnityEngine.Object.Instantiate<GameObject>(this.playerHudBlood).GetComponent(typeof(PlayerHudBloodScript));
        this.playerHudBloodScripts[2] = (PlayerHudBloodScript)UnityEngine.Object.Instantiate<GameObject>(this.playerHudBlood).GetComponent(typeof(PlayerHudBloodScript));
        this.damageIndicatorScripts = new DamageIndicatorScript[3];
        this.damageIndicatorScripts[0] = (DamageIndicatorScript)UnityEngine.Object.Instantiate<GameObject>(this.damageIndicator).GetComponent(typeof(DamageIndicatorScript));
        this.damageIndicatorScripts[1] = (DamageIndicatorScript)UnityEngine.Object.Instantiate<GameObject>(this.damageIndicator).GetComponent(typeof(DamageIndicatorScript));
        this.damageIndicatorScripts[2] = (DamageIndicatorScript)UnityEngine.Object.Instantiate<GameObject>(this.damageIndicator).GetComponent(typeof(DamageIndicatorScript));

        center = GameObject.Find("Player/PlayerGraphics/Armature/Center").transform;

        this.lowerBack = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack");
        this.upperBack = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack");
        this.neck = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Neck");
        this.head = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Neck/Head");
        this.shoulderR = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R");
        this.upperArmR = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R");
        this.lowerArmR = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R");
        this.handR = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R");
        this.bulletPointR = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/BulletPoint");
        this.shoulderL = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L");
        this.upperArmL = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L");
        this.lowerArmL = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L/LowerArm_L");
        this.handL = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L");
        this.bulletPointL = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L/BulletPoint");
        this.footR = this.transform.Find("PlayerGraphics/Armature/Center/Hip_R/UpperLeg_R/LowerLeg_R/Foot_R");
        this.grabPoint = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/GrabPoint");
        this.bulletPointR2 = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/BulletPoint2");
        this.footTipR = this.transform.Find("PlayerGraphics/Armature/Center/Hip_R/UpperLeg_R/LowerLeg_R/Foot_R/FootTip_R");
        this.upperLegR = this.transform.Find("PlayerGraphics/Armature/Center/Hip_R/UpperLeg_R");
        this.lowerLegR = this.transform.Find("PlayerGraphics/Armature/Center/Hip_R/UpperLeg_R/LowerLeg_R");
        this.footL = this.transform.Find("PlayerGraphics/Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L/Foot_L");
        this.footTipL = this.transform.Find("PlayerGraphics/Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L/Foot_L/FootTip_L");
        this.upperLegL = this.transform.Find("PlayerGraphics/Armature/Center/Hip_L/UpperLeg_L");
        this.lowerLegL = this.transform.Find("PlayerGraphics/Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L");
        this.hipR = this.transform.Find("PlayerGraphics/Armature/Center/Hip_R");
        this.hipL = this.transform.Find("PlayerGraphics/Armature/Center/Hip_L");
        this.bloodParticle = (ParticleSystem)GameObject.Find("Main Camera/BloodDropsParticle").GetComponent(typeof(ParticleSystem));
        this.bloodMistParticle = (ParticleSystem)GameObject.Find("Main Camera/BloodMistParticle").GetComponent(typeof(ParticleSystem));
        this.pistolR = this.handR.Find("pistol");
        this.pistolL = this.handL.Find("pistol");
        this.uziR = this.handR.Find("uzi");
        this.uziL = this.handL.Find("uzi");
        this.machineGun = this.handR.Find("machinegun");
        this.shotgun = this.handR.Find("shotgun");
        this.turretGun = this.handR.Find("turret_gun");
        this.turretGunTop = this.turretGun.Find("Turret_Gun_Pipe/BulletPoint");
        this.shockRifle = this.handR.Find("ShockRifle");
        this.shockRifleShield = this.shockRifle.Find("ShockRifle_Shield");
        this.shockRifleShieldMaterial = ((Renderer)this.shockRifleShield.GetComponent(typeof(Renderer))).sharedMaterials[1];
        this.shockRifleShieldCollider = (BoxCollider)this.shockRifleShield.GetComponent(typeof(BoxCollider));
        this.rifle = this.handR.Find("Rifle");
        this.rifleLaser = this.rifle.Find("RifleLaser");
        this.crossbow = this.handR.Find("Crossbow");
        this.crossbowPipe = this.crossbow.Find("Crossbow_Pipe");
        this.crossbowBow1 = this.crossbowPipe.Find("Crossbow_Bow_1");
        this.crossbowBow2 = this.crossbowPipe.Find("Crossbow_Bow_2");
        this.crossbowArrows = new GameObject[4];
        for (int index = 0; index < 4; ++index)
            this.crossbowArrows[index] = this.crossbowPipe.Find(RuntimeServices.op_Addition("Crossbow_Arrow_", (object)index)).gameObject;
        this.handRAudio = (AudioSource)this.handR.GetComponent(typeof(AudioSource));
        this.handLAudio = (AudioSource)this.handL.GetComponent(typeof(AudioSource));
        this.ammoLeftAudio = (AudioSource)this.transform.Find("LowAmmoSound").GetComponent(typeof(AudioSource));
        this.kickWooshAudio = (AudioSource)this.footTipL.GetComponent(typeof(AudioSource));
        this.playerJumpAudio = (AudioSource)this.head.GetComponent(typeof(AudioSource));
        this.ammoLeftAudio.clip = this.lowAmmoSound;
        this.movementSound = (AudioSource)this.transform.Find("MovementSound").GetComponent(typeof(AudioSource));
        this.movementSound.volume = 0.0f;
        this.dodgeAudioSource = (AudioSource)this.transform.Find("DodgeSound").GetComponent(typeof(AudioSource));
        this.playerAudioSource = (AudioSource)this.transform.GetComponent(typeof(AudioSource));
        this.heartBeatAudioSource = (AudioSource)this.root.transform.Find("GeneralSounds/Heartbeat").GetComponent(typeof(AudioSource));
        this.playerHitAudioSource = (AudioSource)this.root.transform.Find("GeneralSounds/PlayerHit").GetComponent(typeof(AudioSource));
        this.hudAudioSource = (AudioSource)GameObject.Find("HUD/Canvas").GetComponent(typeof(AudioSource));
        this.outlineEffect = (OutlineEffect)GameObject.Find("Main Camera").GetComponent(typeof(OutlineEffect));
        if (!this.isEnemy)
        {
            this.trajectory = GameObject.Find("HUD/Trajectory").transform;
            this.trajectoryMaterial = ((Renderer)this.trajectory.Find("Trajectory").GetComponent(typeof(SkinnedMeshRenderer))).sharedMaterial;
            this.trajBone = new Transform[15];
            this.trajBone[0] = this.trajectory.Find("Armature/Bone");
            this.trajBone[1] = this.trajectory.Find("Armature/Bone/Bone_002");
            this.trajBone[2] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003");
            this.trajBone[3] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004");
            this.trajBone[4] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005");
            this.trajBone[5] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006");
            this.trajBone[6] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006/Bone_007");
            this.trajBone[7] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006/Bone_007/Bone_008");
            this.trajBone[8] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006/Bone_007/Bone_008/Bone_009");
            this.trajBone[9] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006/Bone_007/Bone_008/Bone_009/Bone_010");
            this.trajBone[10] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006/Bone_007/Bone_008/Bone_009/Bone_010/Bone_011");
            this.trajBone[11] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006/Bone_007/Bone_008/Bone_009/Bone_010/Bone_011/Bone_012");
            this.trajBone[12] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006/Bone_007/Bone_008/Bone_009/Bone_010/Bone_011/Bone_012/Bone_013");
            this.trajBone[13] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006/Bone_007/Bone_008/Bone_009/Bone_010/Bone_011/Bone_012/Bone_013/Bone_014");
            this.trajBone[14] = this.trajectory.Find("Armature/Bone/Bone_002/Bone_003/Bone_004/Bone_005/Bone_006/Bone_007/Bone_008/Bone_009/Bone_010/Bone_011/Bone_012/Bone_013/Bone_014/Bone_015");
        }
        this.turretGunScript = (TurretGunScript)this.turretGun.Find("Turret_Gun_Pipe").GetComponent(typeof(TurretGunScript));
        this.bulletPointRAimTarget = new GameObject("AimTarget");
        this.bulletPointRAimTarget.transform.parent = this.bulletPointR;
        this.bulletPointRAimTarget.transform.localPosition = new Vector3(0.0f, 0.0f, -1f);
        this.bulletPointLAimTarget = new GameObject("AimTarget");
        this.bulletPointLAimTarget.transform.parent = this.bulletPointL;
        this.bulletPointLAimTarget.transform.localPosition = new Vector3(0.0f, 0.0f, -1f);
        this.bulletPointRAimTarget2 = new GameObject("AimTarget");
        this.bulletPointRAimTarget2.transform.parent = this.bulletPointR2;
        this.bulletPointRAimTarget2.transform.localPosition = new Vector3(0.0f, 0.0f, -1f);
        this.shellParticle = (ParticleSystem)GameObject.Find("Main Camera/ShellParticle").GetComponent(typeof(ParticleSystem));
        this.pistolClipParticle = (ParticleSystem)GameObject.Find("Main Camera/PistolClipParticle").GetComponent(typeof(ParticleSystem));
        this.smokeParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeParticle").GetComponent(typeof(ParticleSystem));
        this.mainCursor = (RectTransform)GameObject.Find("HUD/Canvas/Cursors/MainCursor").GetComponent(typeof(RectTransform));
        this.secondaryCursor = (RectTransform)GameObject.Find("HUD/Canvas/Cursors/SecondaryCursor").GetComponent(typeof(RectTransform));
        this.mainCursorDisabledImage = (Image)this.mainCursor.Find("Disabled").GetComponent(typeof(Image));
        this.aimHelper = (RectTransform)GameObject.Find("HUD/Canvas/Cursors/AimHelper").GetComponent(typeof(RectTransform));
        this.aimHelperImage = (Image)this.aimHelper.GetComponent(typeof(Image));
        this.aimHelperImageStartColour = this.aimHelperImage.color;
        this.hudCanvasRect = (RectTransform)GameObject.Find("HUD/Canvas").GetComponent(typeof(RectTransform));
        this.healthBar = (RectTransform)GameObject.Find("HUD/Canvas/HealthAndSlowMo/HealthBar").GetComponent(typeof(RectTransform));
        this.healthBarStartPos = this.healthBar.anchoredPosition;
        this.healthBarStartSize = this.healthBar.sizeDelta;
        this.healthIcon = (RectTransform)GameObject.Find("HUD/Canvas/HealthAndSlowMo/HealthIcon").GetComponent(typeof(RectTransform));
        this.healthIconStartPos = this.healthIcon.anchoredPosition;
        this.healthIconImage = (Image)this.healthIcon.GetComponent(typeof(Image));
        this.healthIconImageStartColour = this.healthIconImage.color;
        this.healthBar1HUD = (Image)GameObject.Find("HUD/Canvas/HealthAndSlowMo/HealthBar/HealthBar1/Bar").GetComponent(typeof(Image));
        this.healthBar2HUD = (Image)GameObject.Find("HUD/Canvas/HealthAndSlowMo/HealthBar/HealthBar2/Bar").GetComponent(typeof(Image));
        this.healthBar3HUD = (Image)GameObject.Find("HUD/Canvas/HealthAndSlowMo/HealthBar/HealthBar3/Bar").GetComponent(typeof(Image));
        this.weaponHUDIcon = (Image)GameObject.Find("HUD/Canvas/WeaponPanel/WeaponIcon").GetComponent(typeof(Image));
        this.healthBar1HUDRect = (RectTransform)this.healthBar1HUD.GetComponent(typeof(RectTransform));
        this.healthBar2HUDRect = (RectTransform)this.healthBar2HUD.GetComponent(typeof(RectTransform));
        this.healthBar3HUDRect = (RectTransform)this.healthBar3HUD.GetComponent(typeof(RectTransform));
        this.ammoHud = (RectTransform)GameObject.Find("HUD/Canvas/WeaponPanel").GetComponent(typeof(RectTransform));
        this.healthHud = (RectTransform)GameObject.Find("HUD/Canvas/HealthAndSlowMo").GetComponent(typeof(RectTransform));
        this.scoreHud = (RectTransform)GameObject.Find("HUD/Canvas/ScoreHud").GetComponent(typeof(RectTransform));
        int num1 = 105;
        Vector2 anchoredPosition1 = this.ammoHud.anchoredPosition;
        double num2 = (double)(anchoredPosition1.x = (float)num1);
        Vector2 vector2_1 = this.ammoHud.anchoredPosition = anchoredPosition1;
        int num3 = -130;
        Vector2 anchoredPosition2 = this.healthHud.anchoredPosition;
        double num4 = (double)(anchoredPosition2.x = (float)num3);
        Vector2 vector2_2 = this.healthHud.anchoredPosition = anchoredPosition2;
        int num5 = -50;
        Vector2 anchoredPosition3 = this.scoreHud.anchoredPosition;
        double num6 = (double)(anchoredPosition3.y = (float)num5);
        Vector2 vector2_3 = this.scoreHud.anchoredPosition = anchoredPosition3;
        this.disableShootingDoOnce = !this.root.disableShooting;
        if (!this.isEnemy)
        {
            this.ammoText = (TextMeshProUGUI)GameObject.Find("HUD/Canvas/WeaponPanel/AmmoText").GetComponent(typeof(TextMeshProUGUI));
            this.secondaryAmmoText = (TextMeshProUGUI)GameObject.Find("HUD/Canvas/WeaponPanel/SecondaryAmmoText").GetComponent(typeof(TextMeshProUGUI));
            this.ammoHudAmmoBar = (RectTransform)GameObject.Find("HUD/Canvas/WeaponPanel/AmmoBar").GetComponent(typeof(RectTransform));
            this.ammoHudBullet = (RectTransform)this.ammoHudAmmoBar.Find("Bullet").GetComponent(typeof(RectTransform));
            this.ammoHudBullets = new Image[0];
            this.ammoHudInfiniteSymbol = (RectTransform)GameObject.Find("HUD/Canvas/WeaponPanel/InfiniteSymbol").GetComponent(typeof(RectTransform));
            this.ammoHudAmmoAlert = GameObject.Find("HUD/Canvas/WeaponPanel/AmmoAlert");
            this.weaponPanel = (RectTransform)GameObject.Find("HUD/Canvas/WeaponPanel").GetComponent(typeof(RectTransform));
            this.weaponPanelStartPos = this.weaponPanel.anchoredPosition;
            int num7 = 105;
            Vector2 anchoredPosition4 = this.ammoHud.anchoredPosition;
            double num8 = (double)(anchoredPosition4.x = (float)num7);
            Vector2 vector2_4 = this.ammoHud.anchoredPosition = anchoredPosition4;
            int num9 = -130;
            Vector2 anchoredPosition5 = this.healthHud.anchoredPosition;
            double num10 = (double)(anchoredPosition5.x = (float)num9);
            Vector2 vector2_5 = this.healthHud.anchoredPosition = anchoredPosition5;
        }
        this.blackBarTop = GameObject.Find("HUD/Canvas/BlackBarTop");
        this.blackBarBottom = GameObject.Find("HUD/Canvas/BlackBarBottom");
        if (this.root.multiplayer && this.playerNr > 0)
        {
            Transform parent1 = this.mainCursor.transform.parent;
            this.mainCursor = (RectTransform)UnityEngine.Object.Instantiate<GameObject>(this.mainCursor.gameObject).GetComponent(typeof(RectTransform));
            this.mainCursor.transform.parent = parent1;
            this.mainCursor.transform.localScale = Vector3.one;
            Transform parent2 = this.secondaryCursor.transform.parent;
            this.secondaryCursor = (RectTransform)UnityEngine.Object.Instantiate<GameObject>(this.secondaryCursor.gameObject).GetComponent(typeof(RectTransform));
            this.secondaryCursor.transform.parent = parent2;
            this.secondaryCursor.transform.localScale = Vector3.one;
            Transform parent3 = this.aimHelper.transform.parent;
            this.aimHelper = (RectTransform)UnityEngine.Object.Instantiate<GameObject>(this.aimHelper.gameObject).GetComponent(typeof(RectTransform));
            this.aimHelper.transform.parent = parent3;
            this.aimHelper.transform.localScale = Vector3.one;
            Transform parent4 = this.ammoHud.transform.parent;
            this.ammoHud = (RectTransform)UnityEngine.Object.Instantiate<GameObject>(this.ammoHud.gameObject).GetComponent(typeof(RectTransform));
            this.ammoHud.transform.parent = parent4;
            this.ammoHud.transform.localScale = Vector3.one;
            float num7 = 49.1f;
            Vector2 anchoredPosition4 = this.ammoHud.anchoredPosition;
            double num8 = (double)(anchoredPosition4.y = num7);
            Vector2 vector2_4 = this.ammoHud.anchoredPosition = anchoredPosition4;
            this.healthBar1HUD = (Image)this.ammoHud.Find("Healthbar_1").GetComponent(typeof(Image));
            this.healthBar2HUD = (Image)this.ammoHud.Find("Healthbar_2").GetComponent(typeof(Image));
            this.healthBar3HUD = (Image)this.ammoHud.Find("Healthbar_3").GetComponent(typeof(Image));
            this.weaponHUDIcon = (Image)this.ammoHud.Find("WeaponIcon").GetComponent(typeof(Image));
            this.ammoText = (TextMeshProUGUI)this.ammoHud.Find("AmmoText").GetComponent(typeof(TextMeshProUGUI));
            this.secondaryAmmoText = (TextMeshProUGUI)this.ammoHud.Find("SecondaryAmmoText").GetComponent(typeof(TextMeshProUGUI));
        }
        this.mainCursorImage = (Image)this.mainCursor.GetComponent(typeof(Image));
        this.mainCursorImageStartColour = this.mainCursorImage.color;
        this.secondaryCursorImage = (Image)this.secondaryCursor.GetComponent(typeof(Image));
        int num11 = 0;
        Color color1 = this.secondaryCursorImage.color;
        double num12 = (double)(color1.a = (float)num11);
        Color color2 = this.secondaryCursorImage.color = color1;
        this.aimHelper.gameObject.SetActive(false);
        this.uiWeaponSelectorScript = (UIWeaponSelectorScript)GameObject.Find("HUD/Canvas/WeaponSelector").GetComponent(typeof(UIWeaponSelectorScript));
        if (this.root.difficultyMode == 2)
        {
            int num7 = 450;
            Vector2 sizeDelta = ((RectTransform)this.healthBar2HUD.transform.parent.parent.GetComponent(typeof(RectTransform))).sizeDelta;
            double num8 = (double)(sizeDelta.x = (float)num7);
            Vector2 vector2_4 = ((RectTransform)this.healthBar2HUD.transform.parent.parent.GetComponent(typeof(RectTransform))).sizeDelta = sizeDelta;
            this.healthBar1HUD.transform.parent.gameObject.SetActive(false);
            this.healthBar3HUD.transform.parent.gameObject.SetActive(false);
        }
        this.healthBarStartColour = this.healthBar1HUD.color;
        this.fireLightR = (Light)this.handR.Find("FireLight").GetComponent(typeof(Light));
        this.fireLightL = (Light)this.handL.Find("FireLight").GetComponent(typeof(Light));
        this.layerMask = (LayerMask)33652992;
        this.layerMask2 = (LayerMask)33620224;
        this.layerMaskIncEnemies = (LayerMask)33654016;
        this.layerMaskIncEnemiesMinusLvlColIgnoreBullet = (LayerMask)33621248;
        this.layerMaskIncEnemiesAndEnemyGameCollision = (LayerMask)33662208;
        this.layerMaskIncEnemiesAndEnemyGameCollisionWithoutBulletPassthrough = (LayerMask)33629440;
        this.layerMaskOnlyEnemies = (LayerMask)9216;
        this.nrOfWeapons = this.root.nrOfWeapons;
        this.ammo = new float[this.nrOfWeapons];
        this.ammoTotal = new float[this.nrOfWeapons];
        this.ammoFullClip = new float[this.nrOfWeapons];
        this.weaponActive = new bool[this.nrOfWeapons];
        this.secondaryAmmo = new float[this.nrOfWeapons];
        this.usesSecondaryAmmo = new bool[this.nrOfWeapons];
        this.resetWeapons();
        this.maxAmountOfBullets = 0;
        for (int index = 0; index < this.nrOfWeapons; ++index)
        {
            if ((double)this.ammoFullClip[index] > (double)this.maxAmountOfBullets)
                this.maxAmountOfBullets = (int)this.ammoFullClip[index];
        }
        if (!this.isEnemy)
            this.createInitialAmmoHudBullets();
        this.weapon = 0;
        if (this.root.loadStateOnStart && !this.isEnemy)
            this.root.loadProgress();
        if (Extensions.get_length((System.Array)this.root.startingWeapons) > 0)
        {
            int index = 0;
            int[] startingWeapons = this.root.startingWeapons;
            for (int length = startingWeapons.Length; index < length; ++index)
            {
                this.weaponActive[startingWeapons[index]] = true;
                this.ammo[startingWeapons[index]] = this.ammoFullClip[startingWeapons[index]];
                this.ammoTotal[startingWeapons[index]] = this.ammoTotal[startingWeapons[index]] + this.ammoFullClip[startingWeapons[index]] * 2f;
                this.changeWeapon((float)startingWeapons[index]);
            }
        }
        if (this.forceWeaponAtStart != 0)
            this.weapon = this.forceWeaponAtStart;
        this.changeWeapon((float)this.weapon);
        this.swinging = false;
        this.recoverTimer = 1f;
        this.newWeaponPromptScript = (UINewWeaponScript)GameObject.Find("HUD/Canvas/NewWeapon").GetComponent(typeof(UINewWeaponScript));
        this.newWeaponPromptScript.gameObject.SetActive(false);
        this.ammoS = new float[Extensions.get_length((System.Array)this.ammo)];
        this.ammoTotalS = new float[Extensions.get_length((System.Array)this.ammoTotal)];
        this.weaponActiveS = new bool[Extensions.get_length((System.Array)this.weaponActive)];
        this.secondaryAmmoS = new float[Extensions.get_length((System.Array)this.secondaryAmmo)];
        this.orgFogEndValue = this.fogEndTarget = RenderSettings.fogEndDistance;
        this.disableInputTimer = 5f;
        if (this.demoLevel)
            this.giveLoadsOfAmmo();
        if (this.doGiveFullAmmo)
            this.giveFullAmmo();
        if (this.onMotorcycle)
        {
            this.animator.Play("MotorcycleBlend", 0);
            ((Collider)this.GetComponent(typeof(SphereCollider))).enabled = false;
            this.health = 1f;
            this.ammo[3] = this.root.ammoFullClip[3];
            this.ammo[4] = this.root.ammoFullClip[4];
            this.ammoTotal[3] = this.root.ammoMax[3];
            this.changeWeapon(4f);
            this.motorcyclePlayerPos = GameObject.Find("PlayerMotorcycle").transform.Find("PlayerMotorcycleGraphics/PlayerPos");
            this.motorcycleScript = (MotorcycleScript)GameObject.Find("PlayerMotorcycle").GetComponent(typeof(MotorcycleScript));
        }
        int num13 = this.root.multiplayer ? 1 : 0;
        if (num13 != 0)
            num13 = this.playerNr > 0 ? 1 : 0;
        bool flag = num13 != 0;
        if (flag)
            this.inputPText = RuntimeServices.op_Addition("P", (object)this.playerNr);
        else if (!this.isEnemy)
        {
            if (PlatformPlayerPrefs.GetInt("gamepad") == 1)
                this.enableGamepad();
            else
                this.disableGamepad();
        }
        if (this.skyfall)
        {
            this.actualRStick = new Vector2(0.1f, -7f);
            this.gamepadAimOffset = this.rStick = this.rStick2 = this.targetGamepadAimOffset = new Vector2(0.1f, -7f);
            this.rStick2TargetAngle = -Vector2.Angle(Vector2.up, this.rStick);
            this.rStick2Quaternion = this.rStick2TargetQuaternion = Quaternion.Euler(0.0f, 0.0f, this.rStick2TargetAngle);
        }
        this.bossHUD = GameObject.Find("HUD/Canvas").transform.Find("BossHealth").gameObject;
        if (this.isEnemy || flag)
        {
            if (!flag)
            {
                this.actualPlayer = GameObject.Find("Player").transform;
                this.actualPlayerKickFoot = this.actualPlayer.Find("PlayerGraphics/Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L/Foot_L");
                this.actualPlayerScript = (PlayerScript)this.actualPlayer.GetComponent(typeof(PlayerScript));
                this.overrideControls = true;
                this.health = !this.skyfall ? 18f : 2f;
                this.actualPlayerScript.health = 1f;
                this.enemyHealthBar = (RectTransform)this.bossHUD.transform.Find("Outline/Bar").GetComponent(typeof(RectTransform));
                this.healthBarImg = (Image)this.enemyHealthBar.GetComponent(typeof(Image));
                this.healthBarStartColourEnemy = this.healthBarImg.color;
                ((Text)this.bossHUD.transform.Find("Outline/Text").GetComponent(typeof(Text))).text = this.root.GetTranslation("Ophelia");
                this.bossHUD.SetActive(true);
                this.weaponActive[5] = true;
                this.ammo[5] = this.ammoFullClip[5];
                this.ammoTotal[5] = this.ammoTotal[5] + 99999f;
                this.changeWeapon(5f);
                ++this.root.nrOfEnemiesTotal;
            }
            else
                this.changeWeapon(2f);
            int index = 0;
            Component[] componentsInChildren = this.gameObject.GetComponentsInChildren(typeof(Transform));
            for (int length = componentsInChildren.Length; index < length; ++index)
            {
                if (componentsInChildren[index].tag == "Player")
                    componentsInChildren[index].tag = "EvilPlayer";
            }
        }
        else
            this.bossHUDCanvasGroup = (CanvasGroup)this.bossHUD.AddComponent(typeof(CanvasGroup));
        if (this.root.multiplayer)
        {
            this.resetWeapons();
            this.pickedUpWeapon(1f);
            this.pickedUpWeapon(1f);
        }
        if (this.root.trailerMode)
            this.unlockAllWeapons();
        if (this.rootShared.modAllWeapons)
        {
            for (int index = 0; index < this.weaponActive.Length; ++index)
            {
                if (!this.weaponActive[index])
                {
                    this.weaponActive[index] = true;
                    this.ammo[index] = this.ammoFullClip[index];
                    this.ammoTotal[index] = this.ammoFullClip[index] * 5f;
                    this.secondaryAmmo[index] = 3f;
                }
            }
            this.weaponActive[0] = this.weaponActive[10] = this.weaponActive[8] = this.weaponActive[7] = false;
            if (!this.isEnemy && !this.root.multiplayer)
                this.updateAmmoHUD();
            if (this.weapon == 0)
                this.changeWeapon(2f);
        }
        if ((double)this.rootShared.modPlayerSize != 100.0)
        {
            Cloth component = this.transform.Find("PlayerGraphics/TorsorBlackLongSleeve").GetComponent<Cloth>();
            if ((UnityEngine.Object)component != (UnityEngine.Object)null)
                component.enabled = false;
            this.transform.localScale = this.transform.localScale * (float)(0.5 + (double)this.rootShared.modPlayerSize / 100.0 * 0.5);
            this.animator.SetFloat("SpeedMultiplierSpecial", 1f / this.transform.localScale.y);
        }
        if (this.rootShared.modBigHead)
        {
            Cloth component = this.transform.Find("PlayerGraphics/TorsorBlackLongSleeve").GetComponent<Cloth>();
            if ((UnityEngine.Object)component != (UnityEngine.Object)null)
                component.enabled = false;
            this.head.localScale *= 2f + (float)((1.0 - (double)this.rootShared.modPlayerSize / 100.0) * 2.0);
        }
        if (this.rootShared.modInfiniteAmmo)
        {
            this.secondaryAmmo[5] = 5f;
            this.updateAmmoHUD();
        }
        this.prevFixedPos = this.transform.position;
    }

    public virtual void resetWeapons()
    {
        this.ammo[0] = 1f;
        this.ammoTotal[0] = 1f;
        this.ammoFullClip[0] = this.root.ammoFullClip[0];
        this.weaponActive[0] = true;
        this.secondaryAmmo[0] = 0.0f;
        this.usesSecondaryAmmo[0] = false;
        this.ammo[1] = 8f;
        this.ammoTotal[1] = 36f;
        this.ammoFullClip[1] = this.root.ammoFullClip[1];
        this.weaponActive[1] = false;
        this.secondaryAmmo[1] = 0.0f;
        this.usesSecondaryAmmo[1] = false;
        this.ammo[2] = this.ammo[0];
        this.ammoTotal[2] = this.ammoTotal[0];
        this.ammoFullClip[2] = this.root.ammoFullClip[2];
        this.weaponActive[2] = false;
        this.secondaryAmmo[2] = 0.0f;
        this.usesSecondaryAmmo[2] = false;
        this.ammo[3] = 0.0f;
        this.ammoTotal[3] = 0.0f;
        this.ammoFullClip[3] = this.root.ammoFullClip[3];
        this.weaponActive[3] = false;
        this.secondaryAmmo[3] = 0.0f;
        this.usesSecondaryAmmo[3] = false;
        this.ammo[4] = this.ammo[2];
        this.ammoTotal[4] = this.ammoTotal[2];
        this.ammoFullClip[4] = this.root.ammoFullClip[4];
        this.weaponActive[4] = false;
        this.secondaryAmmo[4] = 0.0f;
        this.usesSecondaryAmmo[4] = false;
        this.ammo[5] = 0.0f;
        this.ammoTotal[5] = 0.0f;
        this.ammoFullClip[5] = this.root.ammoFullClip[5];
        this.weaponActive[5] = false;
        this.secondaryAmmo[5] = 0.0f;
        this.usesSecondaryAmmo[5] = true;
        this.ammo[6] = 0.0f;
        this.ammoTotal[6] = 0.0f;
        this.ammoFullClip[6] = this.root.ammoFullClip[6];
        this.weaponActive[6] = false;
        this.secondaryAmmo[6] = 0.0f;
        this.usesSecondaryAmmo[6] = false;
        this.ammo[7] = 0.0f;
        this.ammoTotal[7] = 0.0f;
        this.ammoFullClip[7] = this.root.ammoFullClip[7];
        this.weaponActive[7] = false;
        this.secondaryAmmo[7] = 0.0f;
        this.usesSecondaryAmmo[7] = false;
        this.ammo[8] = 0.0f;
        this.ammoTotal[8] = 0.0f;
        this.ammoFullClip[8] = this.root.ammoFullClip[8];
        this.weaponActive[8] = false;
        this.secondaryAmmo[8] = 1f;
        this.usesSecondaryAmmo[8] = true;
        this.ammo[9] = 0.0f;
        this.ammoTotal[9] = 0.0f;
        this.ammoFullClip[9] = this.root.ammoFullClip[9];
        this.weaponActive[9] = false;
        this.secondaryAmmo[9] = 0.0f;
        this.usesSecondaryAmmo[9] = false;
        this.ammo[10] = 0.0f;
        this.ammoTotal[10] = 0.0f;
        this.ammoFullClip[10] = this.root.ammoFullClip[10];
        this.weaponActive[10] = false;
        this.secondaryAmmo[10] = 0.0f;
        this.usesSecondaryAmmo[10] = false;
    }

    public virtual void giveLoadsOfAmmo()
    {
        for (int index = 0; index < this.nrOfWeapons; ++index)
        {
            this.ammoTotal[index] = 999f;
            this.secondaryAmmo[index] = 99f;
        }
    }

    public virtual void enemyDodgeLogic(float ranValCheck)
    {
        this.kDodge = false;
        if ((double)UnityEngine.Random.value <= (double)ranValCheck)
            return;
        BulletScript[] objectsOfType = (BulletScript[])UnityEngine.Object.FindObjectsOfType(typeof(BulletScript)) as BulletScript[];
        int index = 0;
        BulletScript[] bulletScriptArray = objectsOfType;
        for (int length = bulletScriptArray.Length; index < length; ++index)
        {
            if (bulletScriptArray[index].friendly && (double)Vector2.Distance((Vector2)bulletScriptArray[index].transform.position, (Vector2)this.transform.position) < 3.0)
            {
                this.kDodge = true;
                this.isEnemyDodgeCoolDown = Mathf.Clamp01((float)((double)this.health / 10.0 - 0.5)) * 400f;
                break;
            }
        }
    }

    public virtual void Update()
    {
        if (this.rootShared.allowDebugMenu && this.root.allowDebug)
        {
            if (Input.GetKeyDown(KeyCode.B))
                this.root.explode(new Vector3(this.mousePos.x, this.mousePos.y, 0.0f), 3f, 4, Vector3.zero, "Yellow", false, true);
            if (Input.GetKeyDown(KeyCode.K) || Input.GetButton("RightStickClickP1") && Input.GetButton("PunchP1"))
                this.unlockAllWeapons();
            if (Input.GetKeyDown(KeyCode.L))
            {
                this.transform.position = new Vector3(this.mousePos.x, this.mousePos.y, 0.0f);
                this.ySpeed = 1f;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                this.transform.position = this.lastPosOnGround;
                this.ySpeed = 0.0f;
                this.xSpeed = 0.0f;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                this.resetWeapons();
                this.pickedUpWeapon(1f);
                this.pickedUpWeapon(1f);
                this.root.difficulty = 1f;
            }
            if (Input.GetKey("home"))
                Application.targetFrameRate = UnityEngine.Random.Range(3, 60);
            if (Input.GetKey(KeyCode.PageUp))
                Application.targetFrameRate = 60;
            if (Input.GetKey(KeyCode.PageDown))
                Application.targetFrameRate = 30;
            if (Input.GetKey(KeyCode.End))
                Application.targetFrameRate = 300;
            if (Input.GetKey(KeyCode.Insert))
                Application.targetFrameRate = 3;
        }
        float timescale = this.root.timescale;
        float fixedTimescale = this.root.fixedTimescale;
        float unscaledTimescale = this.root.unscaledTimescale;
        if ((double)this.speedModifier != 1.0)
        {
            this.root.timescale *= this.speedModifier;
            this.root.fixedTimescale *= this.speedModifier;
            this.root.unscaledTimescale *= this.speedModifier;
        }
        this.timescale = this.root.timescale;
        this.physicsInterpolationOffset = Vector3.Lerp(this.prevFixedPos - this.transform.position, Vector3.zero, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
        this.playerGraphics.position = Vector3.Lerp(this.prevFixedPos, this.transform.position, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
        this.interpolatedPosition = this.playerGraphics.position;
        this.playerGraphics.position += this.transform.up * -1.55f * this.transform.localScale.y;
        this.lastXSpeedThatWasntZero = (double)Mathf.Abs(this.xSpeed) <= 0.0 ? this.lastXSpeedThatWasntZero : this.xSpeed;
        this.timeSinceBulletHit += this.timescale;
        if (this.reloading && (double)this.reloadingSafteyTimer > 0.0)
        {
            this.reloadingSafteyTimer -= this.timescale;
            if ((double)this.reloadingSafteyTimer <= 0.0)
                this.finishedReloading();
        }
        if (this.isEnemy)
        {
            if (this.isEnemyActivated)
            {
                if (this.skyfall)
                {
                    Vector3 vector3_1 = new Vector3(0.0f, -5f, 0.0f) - this.actualPlayer.position;
                    Vector3 vector3_2 = new Vector3(0.0f, -5f, 0.0f) - this.transform.position;
                    if ((double)vector3_2.magnitude < 6.0)
                        this.transform.position = this.transform.position + -vector3_2.normalized * (1f - vector3_2.magnitude / 6f);
                    this.skyfallYPos += (vector3_1.y - 5f - this.skyfallYPos) * Mathf.Clamp01(0.1f * this.timescale);
                    this.kXDir = (double)Mathf.Abs(vector3_1.x - this.transform.position.x) <= 2.0 ? 0.0f : ((double)vector3_1.x >= (double)this.transform.position.x ? 1f : -1f);
                    this.mousePos += (this.actualPlayer.position - this.mousePos) * Mathf.Clamp01(0.03f * this.timescale);
                    this.kFire = false;
                    if (this.bulletHit || (double)UnityEngine.Random.value > 0.990000009536743)
                        this.kFire = true;
                    else if ((double)UnityEngine.Random.value > 0.5 && !Physics.Linecast(this.head.position, this.actualPlayer.position, (int)this.layerMask))
                        this.kFire = true;
                    if (this.reloading)
                        this.ammoTotal[5] = 99999f;
                    this.enemyDodgeLogic(0.9f);
                }
                else
                {
                    this.mousePos = this.root.DampV3(this.actualPlayer.position, this.mousePos, 0.1f);
                    if (this.justWallJumped)
                        this.kAction = false;
                    else if (this.onGround && (double)UnityEngine.Random.value > 0.990000009536743)
                        this.kAction = (double)UnityEngine.Random.value > 0.5;
                    if (this.onGround && !this.startedRolling && (double)UnityEngine.Random.value > 0.980000019073486)
                        this.kXDir = !this.wallTouchLeft ? (!this.wallTouchRight ? ((double)UnityEngine.Random.value <= 0.5 ? -1f : 1f) : -1f) : 1f;
                    this.kJump = false;
                    if (!this.startedRolling && !this.justWallJumped && ((double)UnityEngine.Random.value > 0.990000009536743 || !this.onGround && !this.aboutToHitGround && (double)this.extraJumpPower < 0.5 && (this.wallTouchRight || this.wallTouchLeft)))
                    {
                        this.kJump = true;
                        this.kJumpHeldDown = true;
                    }
                    if (this.aboutToHitGround && this.justWallJumped)
                        this.kXDir = (double)UnityEngine.Random.value <= 0.949999988079071 ? ((double)this.xSpeed <= 0.0 ? -1f : 1f) : 0.0f;
                    if ((double)this.actualPlayerScript.punchTimer > 15.0 && !this.actualPlayerScript.meleeKickHit && (double)Vector2.Distance((Vector2)this.actualPlayerKickFoot.position, (Vector2)this.transform.position) < 2.0 && (double)Mathf.Abs(this.actualPlayerKickFoot.position.x - this.transform.position.x) < 0.649999976158142)
                    {
                        Vector3 vector3 = this.actualPlayer.position - this.transform.position;
                        this.bulletHit = true;
                        this.bulletHitRotation = this.transform.rotation;
                        this.bulletHitVel = vector3;
                        this.bulletStrength = 0.1f;
                        this.bulletFromTransform = this.actualPlayer;
                        this.bulletHitDoSound = true;
                        this.playerAudioSource.clip = this.kickEnemyHitSound;
                        this.playerAudioSource.pitch = UnityEngine.Random.Range(0.975f, 1.025f);
                        this.playerAudioSource.volume = UnityEngine.Random.Range(0.85f, 1f);
                        this.playerAudioSource.Play();
                        this.root.rumble((double)vector3.x <= 0.0 ? 0 : 1, 0.6f, 0.2f);
                        ++this.statsTracker.nrOfTimesEnemiesKicked;
                        this.statsTracker.achievementCheck();
                        if (!this.root.showNoBlood)
                        {
                            this.bloodMistParticle.Emit(this.root.generateEmitParams(this.actualPlayerKickFoot.position, Vector3.zero, (float)UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(0.3f, 1f), !this.root.doGore ? new Color(0.0f, 0.0f, 0.0f, 0.5f) : new Color(1f, 1f, 1f, 0.5f)), 1);
                            for (this.b = 0; this.b < 10; ++this.b)
                                this.bloodParticle.Emit(this.root.generateEmitParams(this.actualPlayerKickFoot.position, new Vector3((float)(-(double)this.transform.forward.x * 3.5) + (float)UnityEngine.Random.Range(-4, 4), (float)(-(double)this.transform.forward.y * 3.5) + (float)UnityEngine.Random.Range(1, 6), UnityEngine.Random.Range(-0.5f, 0.5f)), UnityEngine.Random.Range(0.05f, 0.2f), UnityEngine.Random.Range(0.8f, 1.3f), !this.root.doGore ? new Color(0.0f, 0.0f, 0.0f, 1f) : new Color(1f, 1f, 1f, 1f)), 1);
                        }
                        this.cameraScript.screenShake += 0.05f;
                        this.cameraScript.kickBack(0.8f);
                        this.cameraScript.fakePos.z -= 0.25f;
                        this.root.doMeleeHint = false;
                        this.root.meleeHintCoolDown = 7200f;
                        this.actualPlayerScript.meleeKickHit = true;
                    }
                    if (this.bulletHit)
                    {
                        this.isEnemyFireCoolDown *= 0.6f;
                        this.isEnemyDodgeCoolDown -= this.bulletStrength * 10f;
                    }
                    if ((double)this.isEnemyDodgeCoolDown > 0.0)
                        this.isEnemyDodgeCoolDown -= this.timescale;
                    else
                        this.enemyDodgeLogic(0.96f);
                    if ((double)this.isEnemyFireCoolDown > 0.0)
                        this.isEnemyFireCoolDown -= this.timescale;
                    if ((double)this.isEnemyActivatedTimer > 90.0)
                    {
                        if (!this.kDodge && !this.startedRolling && (!this.kFire && (double)this.isEnemyFireCoolDown <= 0.0 || this.kFire) && (double)UnityEngine.Random.value > 0.949999988079071)
                        {
                            this.kFire = !this.kFire;
                            this.isEnemyFireCoolDown = Mathf.Clamp01((float)((double)this.health / 12.0 - 0.5)) * 60f;
                        }
                    }
                    else
                    {
                        this.isEnemyActivatedTimer += this.timescale;
                        this.kXDir = 1f;
                        this.kFire = false;
                    }
                    if ((double)this.actualPlayerScript.dodgingCoolDown > -30.0 && (double)this.actualPlayerScript.dodgingCoolDown < 0.0)
                        this.kFire = false;
                    if (this.kDodge || this.kJump)
                        this.kCrouch = false;
                    else if ((double)UnityEngine.Random.value > 0.990000009536743)
                        this.kCrouch = !this.kCrouch;
                    if (this.kDodge || this.startedRolling)
                    {
                        this.kFire = false;
                        this.kCrouch = false;
                        this.isEnemyFireCoolDown = 0.0f;
                    }
                    if ((double)this.isEnemyActivatedTimer > 90.0 && (double)this.timeSinceShotFired > 120.0)
                        this.kFire = true;
                }
            }
            else
                this.mousePos = this.root.DampV3(this.actualPlayer.position, this.mousePos, 0.1f);
            if (this.bulletHit)
            {
                float num1 = Mathf.Clamp01(this.health / (!this.skyfall ? 18f : 2f));
                Vector3 localScale = this.enemyHealthBar.localScale;
                double num2 = (double)(localScale.x = num1);
                Vector3 vector3 = this.enemyHealthBar.localScale = localScale;
                this.healthBarImg.color = Color.white;
                this.root.slowMotionAmount = Mathf.Clamp01(this.root.slowMotionAmount + 0.05f);
            }
            else
                this.healthBarImg.color = Color.Lerp(this.healthBarImg.color, this.healthBarStartColourEnemy, 0.3f * this.root.timescale);
        }
        if ((double)this.disableInputTimer > 0.0)
            this.disableInputTimer -= this.timescale;
        float axisRaw = 0;

        if (root.dead)
        {
            xSpeed = 0;
            targetXSpeed = 0;
            kXDir = 0;
        }

        if ((double)this.health > 0.0 && (!this.root.sbClickCont || this.root.sbClickContDontFreeze) && (!this.overrideControls && (double)this.disableInputTimer <= 0.0 && !this.root.paused))
        {
            if (!this.onMotorcycle && !MFPMPUI.isTyping)
            {
                this.kXDir = Mathf.Round(Mathf.Clamp(this.player.GetAxisRaw("Move") * 0.85f, -1f, 1f));
                if ((double)this.speechBubbleCoolDownTimer <= 0.0)
                {
                    this.kJump = this.player.GetButtonDown("Jump");
                    this.kJumpHeldDown = this.player.GetButton("Jump");
                }
                if (!this.gamepad)
                {
                    this.kCrouch = this.player.GetNegativeButton("Crouch");
                    this.kDodge = this.player.GetButton("Dodge");
                }
                else
                {
                    axisRaw = this.player.GetAxisRaw("Crouch");
                    this.kCrouch = (double)axisRaw < (!this.swinging ? (this.startedRolling || this.kCrouch ? -0.300000011920929 : -0.800000011920929) : -0.200000002980232);
                    this.kDodge = this.player.GetButton("Dodge");
                }
            }
            this.kAction = !this.swinging && this.root.kAction;
            this.kChangeWeapon = this.gamepad ? this.player.GetButton("Change Weapon") : this.player.GetButton("Change Weapon");
            if (this.kChangeWeapon)
                this.kScrollWheelCoolDownTimer = 60f;
            else if ((double)this.kScrollWheelCoolDownTimer > 0.0)
                this.kScrollWheelCoolDownTimer -= this.timescale;
            if ((double)this.speechBubbleCoolDownTimer <= 0.0)
            {
                if (!this.kChangeWeapon)
                {
                    if (!this.gamepad)
                    {
                        this.kFire = this.player.GetButton("Fire");
                    }
                    else
                    {
                        float num = -this.player.GetAxisRawDelta("Fire");
                        if ((double)num < -0.0199999995529652 || (double)this.player.GetAxisRaw("Fire") >= 1.0)
                            this.kFire = true;
                        else if ((double)num > 0.0199999995529652 || (double)this.player.GetAxisRaw("Fire") <= 0.0)
                            this.kFire = false;
                    }
                }
            }
            else
                this.speechBubbleCoolDownTimer -= this.timescale;
            if (!this.onMotorcycle)
                this.kSecondaryAim = this.gamepad ? (double)this.player.GetAxisRaw("Fire2") > 0.100000001490116 : this.player.GetButton("Fire2");
            this.kReload = this.player.GetButtonDown("Reload");
            this.kScrollWheel = this.player.GetAxisRaw("Scroll Weapon");
            this.kUse = this.player.GetButtonDown("Interact");
            this.kUseHeldDown = this.player.GetButton("Interact");
            if (!this.onMotorcycle)
                this.kPunch = this.player.GetButtonDown("Kick");
            if (this.root.disableShooting && this.kSecondaryAim)
            {
                this.kPunch = true;
                this.kSecondaryAim = false;
            }
            if (!this.kChangeWeapon)
            {
                if (!this.gamepad)
                    this.mousePos = !this.rootShared.simulateMousePos ? this.GetWorldPositionOnPlane(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f), this.transform.position.z) : this.GetWorldPositionOnPlane(new Vector3(this.rootShared.fakeMousePos.x, this.rootShared.fakeMousePos.y, 0.0f), this.transform.position.z);
                else if (this.gamepadMode == 3)
                {
                    this.gamepadAimingReferencePoint = this.transform.position + (!this.startedRolling ? this.transform.up : Vector3.up) * (!this.onMotorcycle ? (float)(1.5 - (double)this.crouchAmount * 0.5) : 0.75f) * this.transform.localScale.y;
                    this.rStick = new Vector2(this.player.GetAxisRaw("Aim Horizontal"), this.player.GetAxisRaw("Aim Vertical"));
                    if ((double)this.rStick.magnitude > 0.100000001490116)
                        this.gamepadAimOffset = this.rStick.normalized * 5f;
                    this.mousePos = this.gamepadAimingReferencePoint + (Vector3)this.gamepadAimOffset;
                }
                else if (this.gamepadMode == 2)
                {
                    float num1 = 1f;
                    Vector3 vector3 = new Vector3();
                    int index = 0;
                    Transform[] allEnemies = this.root.allEnemies;
                    for (int length = allEnemies.Length; index < length; ++index)
                    {
                        float num2 = Vector2.Distance((Vector2)this.mousePos, (Vector2)allEnemies[index].position);
                        EnemyScript enemyScript = (EnemyScript)null;
                        if ((double)num2 < 4.0)
                        {
                            if ((UnityEngine.Object)enemyScript == (UnityEngine.Object)null)
                                enemyScript = (EnemyScript)allEnemies[index].GetComponent(typeof(EnemyScript));
                            if (enemyScript.enabled)
                            {
                                num1 = Mathf.Clamp(num2 / 4f, 0.1f, 1f);
                                vector3 = allEnemies[index].position;
                            }
                        }
                    }
                    this.extraGamepadPos += new Vector2(this.player.GetAxisRaw("Aim Horizontal"), this.player.GetAxisRaw("Aim Vertical")) * 40f * num1;
                    this.extraGamepadPos.x = Mathf.Clamp(this.extraGamepadPos.x, 0.0f, (float)Screen.width);
                    this.extraGamepadPos.y = Mathf.Clamp(this.extraGamepadPos.y, 0.0f, (float)Screen.height);
                    this.mousePos = this.GetWorldPositionOnPlane(new Vector3(this.extraGamepadPos.x, this.extraGamepadPos.y, 0.0f), this.transform.position.z);
                    if (vector3 != Vector3.zero)
                        this.mousePos += (vector3 - this.mousePos) * 0.5f;
                }
                else if (this.gamepadMode == 0 || this.gamepadMode == 1 || (this.gamepadMode == 4 || this.gamepadMode == 5))
                {
                    this.gamepadAimingReferencePoint = this.interpolatedPosition + (!this.startedRolling ? this.transform.up : Vector3.up) * (!this.onMotorcycle ? (float)(1.5 - (double)this.crouchAmount * 0.5) : 0.75f) * this.transform.localScale.y;
                    if ((double)this.shootFromCoverLayerWeight > 0.0)
                        this.gamepadAimingReferencePoint += Vector3.up;
                    float num1 = new float();
                    this.actualRStick = new Vector2(this.player.GetAxisRaw("Aim Horizontal"), this.player.GetAxisRaw("Aim Vertical"));
                    if (this.rootShared.aimAssistMode > 0)
                    {
                        if ((double)this.actualRStick.magnitude > 0.00999999977648258)
                        {
                            this.dontFaceWalkingDirTimer = this.rootShared.aimAssistMode <= 1 ? 180f : 120f;
                            this.movedSinceUsingRStick = false;
                            this.rStickAiming = true;
                        }
                        else
                            this.rStickAiming = false;
                        float num2 = !this.onMotorcycle ? this.kXDir : this.motorcycleScript.xDir;
                        if ((double)this.dontFaceWalkingDirTimer > 0.0)
                        {
                            if (this.rootShared.aimAssistMode != 1 || this.rootShared.aimAssistMode == 1 && !this.justWallJumped)
                                this.dontFaceWalkingDirTimer -= this.root.unscaledTimescale;
                        }
                        else if ((double)num2 > 0.0 || this.movedSinceUsingRStick && (double)num2 == 0.0 && (double)this.targetGamepadAimOffset.x > 0.0)
                        {
                            this.actualRStick = new Vector2(1f, 0.0f);
                            this.gamepadAimOffset = this.targetGamepadAimOffset = new Vector2(7f, 0.0f);
                            this.rStick2TargetAngle = -Vector2.Angle(Vector2.up, new Vector2(-1f, 0.0f));
                            this.rStick2Quaternion = this.rStick2TargetQuaternion = Quaternion.Euler(0.0f, 0.0f, this.rStick2TargetAngle);
                            this.rStickQuickTurnTimer = 1f;
                            this.movedSinceUsingRStick = true;
                        }
                        else if ((double)num2 < 0.0 || this.movedSinceUsingRStick && (double)num2 == 0.0 && (double)this.targetGamepadAimOffset.x < 0.0)
                        {
                            this.actualRStick = new Vector2(-1f, 0.0f);
                            this.gamepadAimOffset = this.targetGamepadAimOffset = new Vector2(-7f, 0.0f);
                            this.rStick2TargetAngle = Vector2.Angle(Vector2.up, new Vector2(-1f, 0.0f));
                            this.rStick2Quaternion = this.rStick2TargetQuaternion = Quaternion.Euler(0.0f, 0.0f, this.rStick2TargetAngle);
                            this.rStickQuickTurnTimer = 1f;
                            this.movedSinceUsingRStick = true;
                        }
                    }
                    if (this.rootShared.aimAssistMode == 0 && (double)this.rootShared.gamepadAimSens >= 1.0)
                    {
                        this.rStick2 = this.actualRStick;
                    }
                    else
                    {
                        Vector2 actualRstick = this.actualRStick;
                        float magnitude = Vector2.ClampMagnitude(actualRstick, 1f).magnitude;
                        if ((double)magnitude > 0.00999999977648258)
                        {
                            this.rStick2TargetAngle = Vector2.Angle(Vector2.up, actualRstick) * ((double)actualRstick.x >= 0.0 ? -1f : 1f);
                            this.rStick2TargetQuaternion = Quaternion.Euler(0.0f, 0.0f, this.rStick2TargetAngle);
                            float num2 = new float();
                            if ((double)this.rStickQuickTurnTimer > 0.0)
                            {
                                this.rStickQuickTurnTimer = (double)Vector2.Angle(this.rStick2, actualRstick) <= 20.0 ? Mathf.Clamp01(this.rStickQuickTurnTimer - this.root.unscaledTimescale / 10f) : 1f;
                                num2 = this.rStickQuickTurnTimer;
                            }
                            else if ((double)magnitude > 0.800000011920929 && (double)Vector2.Angle(this.rStick2, actualRstick) > 165.0 - (double)Mathf.Clamp01(magnitude - 0.8f) / 0.200000002980232 * 10.0)
                            {
                                num2 = 1f;
                                this.rStickQuickTurnTimer = 1f;
                            }
                            float num3 = !this.doNormalAutoAim ? this.rootShared.gamepadAimSens : 1f;
                            this.rStick2Quaternion = !this.doNormalAutoAim || (double)magnitude <= 0.5 ? this.root.DampSlerpUnscaled(this.rStick2TargetQuaternion, this.rStick2Quaternion, this.gamepadAimSensCurve.Evaluate(Mathf.Clamp01(num3 + num2 * (float)(0.300000011920929 + (double)num3 * 0.100000001490116))) * this.gamepadAimSensCurve.Evaluate(magnitude)) : this.rStick2TargetQuaternion;
                        }
                        else
                            this.rStickQuickTurnTimer = 0.0f;
                        this.rStick2 = (Vector2)(this.rStick2Quaternion * (Vector3)Vector2.up);
                    }
                    this.rStick = this.rStick2;
                    this.rStick = (double)this.rStick.magnitude <= 0.100000001490116 ? Vector2.zero : this.rStick.normalized;
                    float num4 = 1f;
                    Vector3 vector3 = Vector3.one * 999f;
                    float num5 = 999f;
                    float num6 = 999f;
                    float num7 = 999f;
                    Transform transform1 = (Transform)null;
                    float num8 = 999f;
                    Transform transform2 = (Transform)null;
                    this.normalAutoAimEnemyForCinematicCamera = (Transform)null;
                    this.doNormalAutoAim = false;
                    if (!this.trajectory.gameObject.activeInHierarchy)
                    {
                        int index = 0;
                        AutoAimTargetScript[] allAutoAimTargets = this.root.allAutoAimTargets;
                        for (int length = allAutoAimTargets.Length; index < length; ++index)
                        {
                            float num2 = Vector2.Distance((Vector2)this.gamepadAimingReferencePoint, (Vector2)allAutoAimTargets[index].transform.position);
                            if ((this.pedroBoss || (double)num2 < 30.0 || this.weapon == 9 && this.kSecondaryAim && (double)num2 < 40.0) && (allAutoAimTargets[index].enabled && allAutoAimTargets[index].gameObject.activeInHierarchy && (allAutoAimTargets[index].alwaysAutoAim || !allAutoAimTargets[index].alwaysAutoAim && this.rStickAiming)))
                            {
                                float num3 = Vector2.Angle((double)num4 <= 0.899999976158142 ? this.gamepadAimOffset : this.rStick, (Vector2)(allAutoAimTargets[index].transform.position + allAutoAimTargets[index].posOffset - this.gamepadAimingReferencePoint)) * (this.rootShared.aimAssistMode <= 0 ? Mathf.Clamp(num2 * 0.6f - 4f, 0.5f, 8f) * allAutoAimTargets[index].inverseWeight : 1f);
                                if (this.rootShared.modCinematicCamera && (double)num2 * (1.0 + (double)Mathf.Abs(this.rStick.y) * 0.5) < 14.0 && (double)num3 < (double)num7)
                                {
                                    this.normalAutoAimEnemyForCinematicCamera = allAutoAimTargets[index].transform;
                                    num7 = num3;
                                }
                                if (this.rootShared.aimAssistMode > 0 && this.justWallJumped || this.rootShared.aimAssistMode > 1 && this.kAction && (!this.onGround && (double)this.targetRotation != 0.0) || (double)num3 < (this.rootShared.aimAssistMode <= 0 ? 25.0 : (double)allAutoAimTargets[index].autoAimAngle))
                                {
                                    int num9 = (double)num2 < 14.0 ? 1 : 0;
                                    if (num9 != 0)
                                        num9 = (UnityEngine.Object)allAutoAimTargets[index].transform == (UnityEngine.Object)this.normalAutoAimEnemy ? 1 : 0;
                                    bool flag = num9 != 0;
                                    if ((double)num3 < (double)num5 && (double)num4 > 0.899999976158142 && (flag || !Physics.Linecast(allAutoAimTargets[index].transform.position + allAutoAimTargets[index].posOffset, this.gamepadAimingReferencePoint, (int)this.layerMask2)))
                                    {
                                        if (this.rootShared.aimAssistMode != 1 || flag || this.rootShared.aimAssistMode == 1 && (this.rStickAiming || this.justWallJumped))
                                        {
                                            num5 = num3;
                                            this.normalAutoAimEnemy = allAutoAimTargets[index].transform;
                                            vector3 = allAutoAimTargets[index].transform.position + allAutoAimTargets[index].posOffset;
                                            this.doNormalAutoAim = true;
                                        }
                                        if (this.rootShared.aimAssistMode == 1)
                                            this.dontFaceWalkingDirTimer = 60f;
                                    }
                                    if (this.rootShared.aimAssistMode > 0)
                                        num3 *= Mathf.Clamp(num2 * 0.6f - 4f, 0.5f, 8f) * allAutoAimTargets[index].inverseWeight;
                                    if ((double)num3 < (double)num6)
                                    {
                                        transform2 = allAutoAimTargets[index].transform;
                                        this.secondaryAimOffset = allAutoAimTargets[index].posOffset;
                                        num6 = num3;
                                    }
                                }
                            }
                        }
                    }
                    int index1 = 0;
                    Transform[] allEnemies = this.root.allEnemies;
                    for (int length = allEnemies.Length; index1 < length; ++index1)
                    {
                        float num2 = Vector2.Distance((Vector2)this.gamepadAimingReferencePoint, (Vector2)allEnemies[index1].position);
                        EnemyScript enemyScript = (EnemyScript)null;
                        if ((double)num2 < 20.0 && (double)Vector2.Distance((Vector2)(this.gamepadAimingReferencePoint + (Vector3)(this.targetGamepadAimOffset.normalized * num2)), (Vector2)allEnemies[index1].position) < 4.0 * (double)Mathf.Clamp01(num2 / (!this.justWallJumped ? 10f : 3f)))
                        {
                            if ((UnityEngine.Object)enemyScript == (UnityEngine.Object)null)
                                enemyScript = (EnemyScript)allEnemies[index1].GetComponent(typeof(EnemyScript));
                            if (enemyScript.enabled && (double)num2 < (double)num8 && !Physics.Linecast(allEnemies[index1].position, this.gamepadAimingReferencePoint, (int)this.layerMaskIncEnemiesAndEnemyGameCollisionWithoutBulletPassthrough))
                            {
                                num8 = num2;
                                transform1 = allEnemies[index1];
                            }
                        }
                    }
                    if ((UnityEngine.Object)transform1 != (UnityEngine.Object)null)
                        this.targetGamepadAimOffset += this.root.DampAddV2Unscaled((Vector2)(transform1.position + new Vector3(0.0f, 1.35f, 0.0f) - this.gamepadAimingReferencePoint), this.targetGamepadAimOffset, (float)(0.165000006556511 * (((double)Mathf.Clamp01(Mathf.Abs(this.ySpeed) / 7f) * 0.100000001490116 + (!this.onGround ? 0.150000005960464 : 0.0) + (!this.justWallJumped ? 0.0 : 0.800000011920929)) * (0.200000002980232 + (1.0 - (double)this.rStick.magnitude) * 0.800000011920929)) * (!this.justWallJumped ? (double)Mathf.Clamp01(num8 / 10f) : 1.0)));
                    if ((UnityEngine.Object)transform2 != (UnityEngine.Object)null && this.kSecondaryAim && (!this.kSecondaryAimDown && (UnityEngine.Object)transform2 != (UnityEngine.Object)null))
                    {
                        this.secondaryAimTarget = transform2;
                        this.secondaryAimEnemyScript = (EnemyScript)this.secondaryAimTarget.GetComponent(typeof(EnemyScript));
                    }
                    if (this.doNormalAutoAim)
                    {
                        if (this.rootShared.aimAssistMode == 0)
                        {
                            this.targetGamepadAimOffset += this.root.DampAddV2Unscaled((Vector2)(vector3 - this.gamepadAimingReferencePoint), this.targetGamepadAimOffset, 0.17f);
                        }
                        else
                        {
                            this.targetGamepadAimOffset = (Vector2)(vector3 - this.gamepadAimingReferencePoint);
                            this.gamepadAimOffset = this.rStick = this.rStick2 = this.targetGamepadAimOffset;
                            this.rStick2TargetAngle = Vector2.Angle(Vector2.up, this.rStick) * ((double)this.rStick.x >= 0.0 ? -1f : 1f);
                            this.rStick2TargetQuaternion = Quaternion.Euler(0.0f, 0.0f, this.rStick2TargetAngle);
                            this.rStick2Quaternion = this.rStick2TargetQuaternion;
                            if (this.rootShared.aimAssistMode > 1)
                                this.dontFaceWalkingDirTimer = !((UnityEngine.Object)this.secondaryAimTarget != (UnityEngine.Object)this.normalAutoAimEnemy) ? 0.0f : 30f;
                        }
                    }
                    else
                        this.normalAutoAimEnemy = (Transform)null;
                    if (this.gamepadMode == 5 && (UnityEngine.Object)transform1 == (UnityEngine.Object)null && !this.doNormalAutoAim)
                    {
                        float magnitude = this.rStick2.magnitude;
                        if ((double)magnitude > 0.949999988079071)
                        {
                            this.targetGamepadAimOffset = this.rStick * 7f;
                            this.rStickAimCoolDown = 10f;
                        }
                        else if ((double)this.rStickAimCoolDown > 0.0)
                            this.rStickAimCoolDown -= this.root.unscaledTimescale;
                        else if ((double)magnitude > 0.200000002980232)
                        {
                            if ((double)magnitude - (double)this.prevRStickMag < -0.100000001490116)
                                this.rStickAimCoolDown = 10f;
                            else
                                this.targetGamepadAimOffset = this.rStick * 7f;
                        }
                    }
                    else
                    {
                        Vector2 vector2 = this.targetGamepadAimOffset.normalized - this.rStick.normalized;
                        bool flag = (double)this.targetGamepadAimOffset.magnitude > 5.90000009536743;
                        this.targetGamepadAimSpeed = this.gamepadMode != 1 ? (Vector2)this.root.DampV3Unscaled((Vector3)(this.rStick * (!flag ? 3f : 0.35f) * ((double)num4 <= 0.850000023841858 ? 0.8f : 2.5f) * num4 * num4), (Vector3)this.targetGamepadAimSpeed, 0.7f) : (Vector2)this.root.DampV3Unscaled((Vector3)(this.rStick * 6f), (Vector3)this.targetGamepadAimSpeed, 0.9f);
                        this.targetGamepadAimOffset += this.targetGamepadAimSpeed * this.root.unscaledTimescale;
                        this.targetGamepadAimOffset += this.targetGamepadAimOffset.normalized * 0.05f * this.root.unscaledTimescale;
                        if ((double)this.targetGamepadAimOffset.magnitude < 6.0)
                            this.targetGamepadAimOffset = (Vector2)this.root.DampV3Unscaled((Vector3)(this.targetGamepadAimOffset.normalized * 6f), (Vector3)this.targetGamepadAimOffset, (float)(0.0500000007450581 * (1.0 - (double)num4)));
                        if ((double)this.targetGamepadAimOffset.magnitude > 7.0)
                            this.targetGamepadAimOffset = this.targetGamepadAimOffset.normalized * 7f;
                    }
                    this.gamepadAimOffset = (Vector2)this.root.DampV3Unscaled((Vector3)this.targetGamepadAimOffset, (Vector3)this.gamepadAimOffset, this.gamepadMode != 1 ? 0.4f : 0.9f);
                    this.mousePos = this.gamepadAimingReferencePoint + (Vector3)this.gamepadAimOffset;
                    this.prevRStickMag = this.rStick2.magnitude;
                }
            }
            this.mousePosWithZOffset = new Vector3(this.mousePos.x, this.mousePos.y, this.mousePos.z - 1f);
            if (this.player.GetButtonDown("Pistol"))
                this.changeWeapon(1f);
            else if (this.player.GetButtonDown("Dual Pistols"))
                this.changeWeapon(2f);
            else if (this.player.GetButtonDown("Submachine Gun"))
                this.changeWeapon(3f);
            else if (this.player.GetButtonDown("Dual Submachine Guns"))
                this.changeWeapon(4f);
            else if (this.player.GetButtonDown("Assault Rifle"))
                this.changeWeapon(5f);
            else if (this.player.GetButtonDown("Shotgun"))
                this.changeWeapon(6f);
            else if (this.player.GetButtonDown("Rifle"))
                this.changeWeapon(9f);
            if (this.weapon != 0)
            {
                if ((double)this.kScrollWheel != 0.0 && (double)this.kScrollWheelCoolDownTimer <= 0.0)
                {
                    if (!this.kScrollWheelDown)
                    {
                        if ((double)this.kScrollWheel < 0.0)
                        {
                            for (this.i = this.weaponAdjusted != 1 ? this.weaponAdjusted - 1 : 10; this.i >= 1; --this.i)
                            {
                                if (this.weaponActive[this.root.adjustedWeaponOrder[this.i]] && (double)this.ammoTotal[this.root.adjustedWeaponOrder[this.i] != 4 ? this.root.adjustedWeaponOrder[this.i] : 3] + (double)this.ammo[this.root.adjustedWeaponOrder[this.i]] > 0.0)
                                {
                                    this.changeWeapon((float)this.root.adjustedWeaponOrder[this.i]);
                                    this.weaponAdjusted = this.i;
                                    this.i = 0;
                                }
                                else if (this.i == 1)
                                    this.i = 10;
                            }
                        }
                        else if ((double)this.kScrollWheel > 0.0)
                        {
                            for (this.i = this.weaponAdjusted != 10 ? this.weaponAdjusted + 1 : 1; this.i <= 10; ++this.i)
                            {
                                if (this.weaponActive[this.root.adjustedWeaponOrder[this.i]] && (double)this.ammoTotal[this.root.adjustedWeaponOrder[this.i] != 4 ? this.root.adjustedWeaponOrder[this.i] : 3] + (double)this.ammo[this.root.adjustedWeaponOrder[this.i]] > 0.0)
                                {
                                    this.changeWeapon((float)this.root.adjustedWeaponOrder[this.i]);
                                    this.weaponAdjusted = this.i;
                                    this.i = 11;
                                }
                                else if (this.i == 10)
                                    this.i = 0;
                            }
                        }
                        this.kScrollWheelDown = true;
                    }
                }
                else if (this.kScrollWheelDown)
                    this.kScrollWheelDown = false;
            }
        }
        else if (this.root.sbClickCont && !this.root.sbClickContDontFreeze && !this.overrideControls)
        {
            this.kXDir = 0.0f;
            this.kJump = false;
            this.kJumpHeldDown = false;
            this.kCrouch = false;
            this.kFire = false;
            this.kSecondaryAim = false;
            this.kReload = false;
            this.kScrollWheel = 0.0f;
            this.kUse = false;
            this.kUseHeldDown = false;
            this.kChangeWeapon = false;
            this.kDodge = false;
            this.kPunch = false;
        }
        if (!this.root.multiplayer)
        {
            Controller activeController = this.player.controllers.GetLastActiveController();
            if (!RuntimeServices.EqualityOperator((object)activeController, (object)null))
            {
                if (this.gamepad)
                {
                    if (activeController.type == ControllerType.Keyboard || activeController.type == ControllerType.Mouse)
                        this.disableGamepad();
                }
                else if (activeController.type == ControllerType.Joystick)
                    this.enableGamepad();
                if (!RuntimeServices.EqualityOperator((object)this.lastUsedController, (object)activeController) && activeController.type != ControllerType.Mouse)
                {
                    this.root.updateInputIcons = true;
                    this.lastUsedController = activeController;
                }
            }
        }
        else if (this.playerNr > 0)
        {
            this.rStick = new Vector2(this.player.GetAxisRaw("Aim Horizontal"), this.player.GetAxisRaw("Aim Vertical"));
            if (!this.gamepad)
            {
                this.aimHelper.gameObject.SetActive(true);
                this.gamepad = true;
            }
        }
        if (!this.isEnemy && this.playerNr == 0)
        {
            float num1 = this.trajectoryMaterial.mainTextureOffset.x - 0.01f * this.timescale;
            Vector2 mainTextureOffset1 = this.trajectoryMaterial.mainTextureOffset;
            double num2 = (double)(mainTextureOffset1.x = num1);
            Vector2 vector2_1 = this.trajectoryMaterial.mainTextureOffset = mainTextureOffset1;
            if ((double)this.trajectoryMaterial.mainTextureOffset.x < 0.0)
            {
                float num3 = this.trajectoryMaterial.mainTextureOffset.x + 1f;
                Vector2 mainTextureOffset2 = this.trajectoryMaterial.mainTextureOffset;
                double num4 = (double)(mainTextureOffset2.x = num3);
                Vector2 vector2_2 = this.trajectoryMaterial.mainTextureOffset = mainTextureOffset2;
            }
            if (this.root.sbClickCont && !this.root.sbClickContDontFreeze || (this.overrideControls || (double)this.disableInputTimer > 0.0))
            {
                if (!this.controlOverrideDoOnce)
                {
                    this.root.clearHintText();
                    this.bossHUDCanvasGroup.alpha = 0.0f;
                    this.blackBarTop.SetActive(true);
                    this.blackBarBottom.SetActive(true);
                    if (this.gamepad)
                    {
                        this.aimHelper.gameObject.SetActive(false);
                        this.mainCursor.gameObject.SetActive(false);
                    }
                    this.controlOverrideDoOnce = true;
                }
                float num3 = this.ammoHud.anchoredPosition.x + (105f - this.ammoHud.anchoredPosition.x) * Mathf.Clamp01(0.2f * this.timescale);
                Vector2 anchoredPosition1 = this.ammoHud.anchoredPosition;
                double num4 = (double)(anchoredPosition1.x = num3);
                Vector2 vector2_2 = this.ammoHud.anchoredPosition = anchoredPosition1;
                float num5 = this.healthHud.anchoredPosition.x + (-130f - this.healthHud.anchoredPosition.x) * Mathf.Clamp01(0.2f * this.timescale);
                Vector2 anchoredPosition2 = this.healthHud.anchoredPosition;
                double num6 = (double)(anchoredPosition2.x = num5);
                Vector2 vector2_3 = this.healthHud.anchoredPosition = anchoredPosition2;
                float num7 = this.scoreHud.anchoredPosition.y + (-50f - this.scoreHud.anchoredPosition.y) * Mathf.Clamp01(0.2f * this.timescale);
                Vector2 anchoredPosition3 = this.scoreHud.anchoredPosition;
                double num8 = (double)(anchoredPosition3.y = num7);
                Vector2 vector2_4 = this.scoreHud.anchoredPosition = anchoredPosition3;
            }
            else
            {
                if (this.controlOverrideDoOnce)
                {
                    this.bossHUDCanvasGroup.alpha = 1f;
                    this.blackBarTop.SetActive(false);
                    this.blackBarBottom.SetActive(false);
                    if (this.gamepad)
                        this.aimHelper.gameObject.SetActive(true);
                    this.mainCursor.gameObject.SetActive(true);
                    this.controlOverrideDoOnce = false;
                }
                if ((double)Time.timeSinceLevelLoad > 0.5 && this.root.updateFramesDone > 10)
                {
                    if (this.root.showScoreHud)
                    {
                        float num3 = this.scoreHud.anchoredPosition.y + (65f - this.scoreHud.anchoredPosition.y) * Mathf.Clamp01(0.1f * this.timescale);
                        Vector2 anchoredPosition = this.scoreHud.anchoredPosition;
                        double num4 = (double)(anchoredPosition.y = num3);
                        Vector2 vector2_2 = this.scoreHud.anchoredPosition = anchoredPosition;
                    }
                    if (this.root.showHealthHud)
                    {
                        float num3 = this.healthHud.anchoredPosition.x + (116f - this.healthHud.anchoredPosition.x) * Mathf.Clamp01(0.1f * this.timescale);
                        Vector2 anchoredPosition = this.healthHud.anchoredPosition;
                        double num4 = (double)(anchoredPosition.x = num3);
                        Vector2 vector2_2 = this.healthHud.anchoredPosition = anchoredPosition;
                    }
                    if (this.root.showWeaponHud)
                    {
                        float num3 = this.ammoHud.anchoredPosition.x + (-79f - this.ammoHud.anchoredPosition.x) * Mathf.Clamp01(0.1f * this.timescale);
                        Vector2 anchoredPosition = this.ammoHud.anchoredPosition;
                        double num4 = (double)(anchoredPosition.x = num3);
                        Vector2 vector2_2 = this.ammoHud.anchoredPosition = anchoredPosition;
                    }
                }
            }
        }
        else if (this.root.multiplayer && this.playerNr > 0)
        {
            float num1 = this.ammoHud.anchoredPosition.x + (-888f - this.ammoHud.anchoredPosition.x) * Mathf.Clamp01(0.1f * this.timescale);
            Vector2 anchoredPosition = this.ammoHud.anchoredPosition;
            double num2 = (double)(anchoredPosition.x = num1);
            Vector2 vector2 = this.ammoHud.anchoredPosition = anchoredPosition;
        }
        this.fogEndTarget = this.orgFogEndValue;
        if (this.weapon == 8)
        {
            this.updateAmmoHUD();
            if (this.kSecondaryAim && (double)this.secondaryAmmo[8] > 0.0 && (!this.reloading && !this.startedRolling))
            {
                this.secondaryAmmo[8] = Mathf.Clamp01(this.secondaryAmmo[8] - 3f / 500f * this.timescale);
                this.kFire = false;
                this.shockRifleShieldCollider.enabled = true;
                float num1 = this.shockRifleShield.localRotation.eulerAngles.z + 93f * this.timescale;
                Quaternion localRotation1 = this.shockRifleShield.localRotation;
                Vector3 eulerAngles1 = localRotation1.eulerAngles;
                double num2 = (double)(eulerAngles1.z = num1);
                Vector3 vector3_1 = localRotation1.eulerAngles = eulerAngles1;
                Quaternion quaternion1 = this.shockRifleShield.localRotation = localRotation1;
                int num3 = 0;
                Quaternion localRotation2 = this.shockRifleShield.localRotation;
                Vector3 eulerAngles2 = localRotation2.eulerAngles;
                double num4 = (double)(eulerAngles2.x = (float)num3);
                Vector3 vector3_2 = localRotation2.eulerAngles = eulerAngles2;
                Quaternion quaternion2 = this.shockRifleShield.localRotation = localRotation2;
                float num5 = this.shockRifleShield.localPosition.z + (-1f - this.shockRifleShield.localPosition.z) * Mathf.Clamp01(0.1f * this.timescale);
                Vector3 localPosition = this.shockRifleShield.localPosition;
                double num6 = (double)(localPosition.z = num5);
                Vector3 vector3_3 = this.shockRifleShield.localPosition = localPosition;
                float num7 = this.shockRifleShieldMaterial.mainTextureOffset.y - 0.05f * this.timescale;
                Vector2 mainTextureOffset1 = this.shockRifleShieldMaterial.mainTextureOffset;
                double num8 = (double)(mainTextureOffset1.y = num7);
                Vector2 vector2_1 = this.shockRifleShieldMaterial.mainTextureOffset = mainTextureOffset1;
                if ((double)this.shockRifleShieldMaterial.mainTextureOffset.y < 0.0)
                {
                    float num9 = this.shockRifleShieldMaterial.mainTextureOffset.y + 1f;
                    Vector2 mainTextureOffset2 = this.shockRifleShieldMaterial.mainTextureOffset;
                    double num10 = (double)(mainTextureOffset2.y = num9);
                    Vector2 vector2_2 = this.shockRifleShieldMaterial.mainTextureOffset = mainTextureOffset2;
                }
                float num11 = this.shockRifleShieldMaterial.mainTextureOffset.x + (-0.85f - this.shockRifleShieldMaterial.mainTextureOffset.x) * Mathf.Clamp01(0.1f * this.timescale);
                Vector2 mainTextureOffset3 = this.shockRifleShieldMaterial.mainTextureOffset;
                double num12 = (double)(mainTextureOffset3.x = num11);
                Vector2 vector2_3 = this.shockRifleShieldMaterial.mainTextureOffset = mainTextureOffset3;
            }
            else
            {
                if (!this.kSecondaryAim || this.reloading || this.startedRolling)
                    this.secondaryAmmo[8] = Mathf.Clamp01(this.secondaryAmmo[8] + 0.02f * this.timescale);
                this.shockRifleShieldCollider.enabled = false;
                float num1 = this.shockRifleShield.localPosition.z + (-0.85f - this.shockRifleShield.localPosition.z) * Mathf.Clamp01(0.1f * this.timescale);
                Vector3 localPosition = this.shockRifleShield.localPosition;
                double num2 = (double)(localPosition.z = num1);
                Vector3 vector3_1 = this.shockRifleShield.localPosition = localPosition;
                if ((double)this.shockRifleShieldMaterial.mainTextureOffset.x > -0.0500000007450581)
                {
                    this.shockRifleShield.localRotation = Quaternion.Slerp(this.shockRifleShield.localRotation, Quaternion.Euler(-45f, 180f, 0.0f), Mathf.Clamp01(0.1f * this.timescale));
                    int num3 = 0;
                    Vector2 mainTextureOffset = this.shockRifleShieldMaterial.mainTextureOffset;
                    double num4 = (double)(mainTextureOffset.x = (float)num3);
                    Vector2 vector2 = this.shockRifleShieldMaterial.mainTextureOffset = mainTextureOffset;
                }
                else
                {
                    float num3 = this.shockRifleShield.localRotation.eulerAngles.z + this.shockRifleShieldMaterial.mainTextureOffset.x * 100f * this.timescale;
                    Quaternion localRotation = this.shockRifleShield.localRotation;
                    Vector3 eulerAngles = localRotation.eulerAngles;
                    double num4 = (double)(eulerAngles.z = num3);
                    Vector3 vector3_2 = localRotation.eulerAngles = eulerAngles;
                    Quaternion quaternion = this.shockRifleShield.localRotation = localRotation;
                    float num5 = this.shockRifleShieldMaterial.mainTextureOffset.x + (0.0f - this.shockRifleShieldMaterial.mainTextureOffset.x) * Mathf.Clamp01(0.1f * this.timescale);
                    Vector2 mainTextureOffset = this.shockRifleShieldMaterial.mainTextureOffset;
                    double num6 = (double)(mainTextureOffset.x = num5);
                    Vector2 vector2 = this.shockRifleShieldMaterial.mainTextureOffset = mainTextureOffset;
                }
            }
        }
        else if (this.weapon == 9)
        {
            if (this.kSecondaryAim)
            {
                if (!this.rifleLaser.gameObject.activeInHierarchy)
                    this.rifleLaser.gameObject.SetActive(true);
                if (Physics.Raycast(this.rifleLaser.position, -this.rifleLaser.right, out this.rayHit, 40f, (int)this.layerMaskIncEnemiesAndEnemyGameCollisionWithoutBulletPassthrough))
                {
                    float num1 = this.rayHit.distance / this.transform.localScale.y;
                    Vector3 localScale = this.rifleLaser.localScale;
                    double num2 = (double)(localScale.x = num1);
                    Vector3 vector3 = this.rifleLaser.localScale = localScale;
                }
                else
                {
                    float num1 = 40f / this.transform.localScale.y;
                    Vector3 localScale = this.rifleLaser.localScale;
                    double num2 = (double)(localScale.x = num1);
                    Vector3 vector3 = this.rifleLaser.localScale = localScale;
                }
                if ((double)this.orgFogEndValue <= 45.0)
                    this.fogEndTarget = 45f;
                this.timeSinceSniperAim = 0.0f;
                this.cameraScript.tiltMultiplier = this.root.Damp(1.8f, this.cameraScript.tiltMultiplier, 0.3f);
            }
            else
            {
                if (this.rifleLaser.gameObject.activeInHierarchy)
                    this.rifleLaser.gameObject.SetActive(false);
                this.cameraScript.tiltMultiplier = this.root.Damp(1f, this.cameraScript.tiltMultiplier, 0.3f);
            }
        }
        else if (this.weapon == 10)
        {
            float num1 = Mathf.Clamp(1f - this.fireDelay / 45f, 0.3f, 1f);
            Vector3 localScale = this.crossbowBow1.localScale;
            double num2 = (double)(localScale.z = num1);
            Vector3 vector3 = this.crossbowBow1.localScale = localScale;
            this.crossbowPipe.localRotation = Quaternion.Slerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90f), Mathf.SmoothStep(0.0f, 1f, 1f - this.fireDelay / 45f));
        }
        if (!this.rootShared.modSideOnCamera && (double)this.orgFogEndValue <= 45.0)
            RenderSettings.fogEndDistance = this.root.Damp(this.fogEndTarget, RenderSettings.fogEndDistance, 0.03f);
        this.fireSecondaryWeapon = false;
        if (this.aimWithLeftArm)
        {
            if (!this.kSecondaryAim)
            {
                if ((double)Vector3.Distance(this.mousePos2, this.mousePos) < 1.0 || !this.seperateAim)
                {
                    this.seperateAim = false;
                    this.mousePos2 = this.mousePos;
                }
                else
                    this.mousePos2 = this.root.DampV3(this.mousePos, this.mousePos2, 0.3f);
                if (this.kSecondaryAimDown)
                {
                    this.playerAudioSource.clip = this.splitAimStopSound;
                    this.playerAudioSource.volume = UnityEngine.Random.Range(0.8f, 1f);
                    this.playerAudioSource.pitch = UnityEngine.Random.Range(0.85f, 1.15f);
                    this.playerAudioSource.Play();
                    this.secondaryAimOffset = Vector3.zero;
                    this.secondaryAimTarget = (Transform)null;
                    this.secondaryAimEnemyScript = (EnemyScript)null;
                    this.kSecondaryAimDown = false;
                }
            }
            else
            {
                if (!this.kSecondaryAimDown)
                {
                    if (!this.gamepad)
                    {
                        this.mousePos2 = this.mousePos;
                        int index = 0;
                        AutoAimTargetScript[] allAutoAimTargets = this.root.allAutoAimTargets;
                        for (int length = allAutoAimTargets.Length; index < length; ++index)
                        {
                            if ((double)Vector2.Distance((Vector2)this.mousePos, (Vector2)allAutoAimTargets[index].transform.position) < 3.0)
                            {
                                this.secondaryAimEnemyScript = (EnemyScript)allAutoAimTargets[index].GetComponent(typeof(EnemyScript));
                                if (allAutoAimTargets[index].enabled)
                                {
                                    this.secondaryAimTarget = allAutoAimTargets[index].transform;
                                    this.secondaryAimOffset = allAutoAimTargets[index].posOffset;
                                    this.mousePos2 = this.secondaryAimTarget.position + this.secondaryAimOffset;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        RaycastHit hitInfo = new RaycastHit();
                        this.mousePos2 = !Physics.Raycast(this.gamepadAimingReferencePoint, (Vector3)this.targetGamepadAimOffset.normalized, out hitInfo, 20f, (int)this.layerMaskIncEnemiesAndEnemyGameCollisionWithoutBulletPassthrough) ? this.gamepadAimingReferencePoint + (Vector3)(this.targetGamepadAimOffset.normalized * 19.5f) : this.gamepadAimingReferencePoint + (Vector3)(this.targetGamepadAimOffset.normalized * Mathf.Clamp(hitInfo.distance - 1.5f, 6f, 20f));
                    }
                    this.playerAudioSource.clip = this.splitAimStartSound;
                    this.playerAudioSource.volume = UnityEngine.Random.Range(0.8f, 1f);
                    this.playerAudioSource.pitch = UnityEngine.Random.Range(0.85f, 1.15f);
                    this.playerAudioSource.Play();
                    this.kSecondaryAimDown = true;
                }
                this.seperateAim = true;
                if ((UnityEngine.Object)this.secondaryAimTarget != (UnityEngine.Object)null)
                {
                    if ((UnityEngine.Object)this.secondaryAimEnemyScript != (UnityEngine.Object)null)
                    {
                        this.mousePos2 = this.secondaryAimTarget.position + this.secondaryAimOffset;
                        if (!this.secondaryAimEnemyScript.enabled)
                        {
                            this.secondaryAimTarget = (Transform)null;
                            this.secondaryAimEnemyScript = (EnemyScript)null;
                            this.kSecondaryAim = false;
                        }
                    }
                    else
                        this.mousePos2 = this.secondaryAimTarget.position + this.secondaryAimOffset;
                    this.mousePos2.z = this.transform.position.z;
                }
            }
        }
        else
        {
            this.seperateAim = false;
            this.mousePos2 = this.mousePos;
            if (this.kSecondaryAim)
            {
                if (!this.kSecondaryAimDown)
                {
                    if (this.weapon == 1)
                    {
                        this.kSecondaryAim = this.kSecondaryAimDown = false;
                        this.changeWeapon(2f);
                    }
                    else if (this.weapon == 3)
                    {
                        this.kSecondaryAim = this.kSecondaryAimDown = false;
                        this.changeWeapon(4f);
                    }
                    else if ((double)this.secondaryAmmo[this.weapon] > 0.0 && this.weapon == 5 && (!this.startedRolling && (double)this.secondaryFireDelay <= 0.0))
                    {
                        this.secondaryAmmo[this.weapon] = !this.rootShared.modInfiniteAmmo ? this.secondaryAmmo[this.weapon] - 1f : 5f;
                        this.fireSecondaryWeapon = true;
                        this.updateAmmoHUD();
                    }
                    this.kSecondaryAimDown = true;
                }
            }
            else
                this.kSecondaryAimDown = false;
        }
        if (this.aimWithLeftArm && this.kSecondaryAim)
        {
            this.secondaryCursor.position = this.curCamera.WorldToScreenPoint(this.mousePos2);
            if (this.rootShared.hideHUD)
            {
                float num1 = this.root.Damp(0.5f, this.secondaryCursorImage.color.a, 0.3f);
                Color color1 = this.secondaryCursorImage.color;
                double num2 = (double)(color1.a = num1);
                Color color2 = this.secondaryCursorImage.color = color1;
                this.secondaryCursor.sizeDelta = this.root.DampV2(Vector2.one * 8f, this.secondaryCursor.sizeDelta, 0.3f);
            }
            else
            {
                float num1 = this.root.Damp(1f, this.secondaryCursorImage.color.a, 0.3f);
                Color color1 = this.secondaryCursorImage.color;
                double num2 = (double)(color1.a = num1);
                Color color2 = this.secondaryCursorImage.color = color1;
                this.secondaryCursor.sizeDelta = this.root.DampV2(Vector2.one * 23f, this.secondaryCursor.sizeDelta, 0.3f);
            }
            this.secondaryCursor.rotation = this.secondaryCursor.rotation * Quaternion.Euler(0.0f, 0.0f, 2f);
            this.cameraScript.trackPos2 = this.mousePos2;
        }
        else
        {
            float num1 = this.root.Damp(0.0f, this.secondaryCursorImage.color.a, 0.3f);
            Color color1 = this.secondaryCursorImage.color;
            double num2 = (double)(color1.a = num1);
            Color color2 = this.secondaryCursorImage.color = color1;
            this.secondaryCursor.sizeDelta = this.root.DampV2(Vector2.one * 23f * 3f, this.secondaryCursor.sizeDelta, 0.3f);
        }
        if ((double)this.health <= 0.0)
        {
            int num1 = 0;
            Color color1 = this.secondaryCursorImage.color;
            double num2 = (double)(color1.a = (float)num1);
            Color color2 = this.secondaryCursorImage.color = color1;
        }
        if ((double)this.kXDir != 0.0 && this.onGround && !this.startedRolling)
        {
            this.walkTimer += this.timescale;
            this.runBend = Mathf.Clamp01((this.walkTimer - 30f) / 240f);
        }
        else
        {
            this.walkTimer = 0.0f;
            this.runBend = Mathf.Clamp01(this.runBend - 0.1f * this.timescale);
        }
        this.prevTargetXSpeed = this.targetXSpeed;
        this.targetXSpeed = !this.skyfall ? this.kXDir * (!this.swinging || this.onGround ? (float)(7.0 * (1.0 + (double)this.runBend * 0.200000002980232)) : 20f) : this.kXDir * 12f;
        if (this.root.sbClickCont && !this.root.sbClickContDontFreeze && !this.overrideControls)
        {
            this.speechBubbleCoolDownTimer = 5f;
            this.disableInputTimer = 5f;
            this.mousePos = this.root.DampV3(this.root.sbTransform.position + this.root.sbOffset, this.mousePos, 0.3f);
            this.targetXSpeed = 0.0f;
        }
        if ((double)this.kickFreezeTimer <= 0.0 && (double)this.prevTargetXSpeed == 0.0 && (double)this.targetXSpeed != 0.0 && (this.animator.GetCurrentAnimatorStateInfo(0).IsName("OnGround Blend Tree") && (double)Mathf.Abs(this.xSpeed) < 0.5))
            this.animator.Play("OnGround Blend Tree", 0, 0.2f);
        if (!this.isEnemy && this.overrideControls)
            this.bulletHit = false;
        if (this.bulletHit)
        {
            if (!this.isEnemy && !this.root.dead)
            {
                this.root.rumble((double)this.bulletHitVel.x >= 0.0 ? 0 : 1, Mathf.Clamp01(this.bulletStrength * 1.5f), (float)(0.150000005960464 + (double)this.bulletStrength * 0.200000002980232));
                if (!this.root.trailerMode)
                {
                    PlayerHudBloodScript playerHudBloodScript = this.playerHudBloodScripts[this.curPlayerHudBloodScript];
                    playerHudBloodScript.gameObject.SetActive(true);
                    playerHudBloodScript.transform.up = new Vector3(this.bulletHitVel.x, this.bulletHitVel.y, 0.0f).normalized;
                    playerHudBloodScript.doSetup();
                    this.curPlayerHudBloodScript = (int)Mathf.Repeat((float)(this.curPlayerHudBloodScript + 1), 3f);
                    if ((UnityEngine.Object)this.bulletFromTransform != (UnityEngine.Object)null)
                    {
                        DamageIndicatorScript damageIndicatorScript = this.damageIndicatorScripts[this.curDamageIndicatorScript];
                        damageIndicatorScript.magicNumberTarget = this.bulletStrength;
                        damageIndicatorScript.pointerTarget = this.bulletFromTransform;
                        damageIndicatorScript.doSetup();
                        damageIndicatorScript.gameObject.SetActive(true);
                        this.curDamageIndicatorScript = (int)Mathf.Repeat((float)(this.curDamageIndicatorScript + 1), 3f);
                        this.root.doSubtleHighlight(this.bulletFromTransform);
                    }
                }
                this.root.changeDifficulty((float)(-(double)this.bulletStrength * 0.800000011920929));
                this.root.cCheckMuTi = true;
                this.root.multiplierTimer = Mathf.Clamp01(this.root.multiplierTimer - this.bulletStrength);
                ++this.bulletHitsSinceDodgeUsed;
                ++this.bulletHitsSinceSlowMotionUsed;
                this.timeSinceBulletHit = 0.0f;
                this.weaponPanel.SetAsLastSibling();
                this.healthHud.SetAsLastSibling();
                this.scoreHud.SetAsLastSibling();
                if (this.bulletHitDoSound)
                {
                    this.heartBeatAudioSource.pitch = Mathf.Abs((float)(0.899999976158142 + (1.0 - (double)this.health) * 0.5) + UnityEngine.Random.Range(-0.15f, 0.15f));
                    this.heartBeatAudioSource.volume = UnityEngine.Random.Range(0.3f, 0.6f);
                    this.heartBeatAudioSource.Play();
                    this.playerHitAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                    this.playerHitAudioSource.volume = UnityEngine.Random.Range(0.4f, 1f);
                    this.playerHitAudioSource.Play();
                    this.bulletHitDoSound = false;
                }
                this.root.doScoreCountingSound = false;
                if ((double)this.health > 0.0 && !this.root.trailerMode)
                    this.root.bulletHitFreeze();
            }
            this.recoverTimer = 0.0f;
            if (!this.root.trailerMode && !this.rootShared.godMode)
            {
                this.health -= this.root.calculateBulletHitStrength(this.bulletStrength);
                if (!this.isEnemy && this.rootShared.modOneShotPlayer || this.isEnemy && this.rootShared.modOneShotEnemies)
                    this.health = Mathf.Clamp(this.health, -99999f, 0.0f);
            }
            this.bulletStrength = 0.0f;
            this.updateHealthHUD();
            if (!this.root.trailerMode)
            {
                if (!this.isEnemy && !this.root.showDodgeAlert && ((double)this.health > 0.0 && !this.root.dead) && this.root.finishedPlayingIntro)
                {
                    this.hurtStateAudioSnapshot.TransitionTo(!this.onMotorcycle ? 0.0f : 0.05f);
                    this.root.lastActivatedAudioSnapshot = 2;
                }
                this.heartBeatMultiplier = 1f;
            }
            this.bulletHit = false;
            this.bulletHitDoOnce = true;
        }
        else if (this.bulletHitDoOnce)
        {
            if (!this.isEnemy && !this.root.showDodgeAlert && ((double)this.health > 0.0 && !this.root.dead))
            {
                if (this.root.kAction)
                {
                    this.actionStateAudioSnapshot.TransitionTo(0.4f);
                    this.root.lastActivatedAudioSnapshot = 1;
                }
                else
                {
                    if (!this.onMotorcycle)
                        this.normalStateAudioSnapshot.TransitionTo(1f);
                    else
                        this.motorcycleNormalStateAudioSnapshot.TransitionTo(1f);
                    this.root.lastActivatedAudioSnapshot = 0;
                }
            }
            this.bulletHitDoOnce = false;
        }
        this.heartBeat -= this.timescale / (float)(45.0 - (1.0 - (double)this.health) * 20.0);
        if ((double)this.heartBeat <= (1.0 - (double)this.health) / 2.0 - 1.0)
        {
            this.heartBeat = 1f;
            if ((double)this.heartBeatMultiplier > 0.0 && (double)this.health <= 0.5)
            {
                this.heartBeatAudioSource.pitch = (float)(0.899999976158142 + (1.0 - (double)this.health) * 0.5);
                this.heartBeatAudioSource.volume = Mathf.Clamp01(this.heartBeatMultiplier * 1.5f) * Mathf.Clamp01((float)((1.0 - (double)this.health) * 2.0 - 0.400000005960464));
                this.heartBeatAudioSource.Play();
                this.root.rumble(0, this.heartBeatAudioSource.volume * 0.05f, 0.1f);
                this.root.rumble(1, this.heartBeatAudioSource.volume * 0.1f, 0.05f);
            }
        }
        if (!this.root.enemyEngagedWithPlayer)
            this.heartBeatMultiplier = Mathf.Clamp01(this.heartBeatMultiplier - 3f / 1000f * this.timescale);
        if (!this.isEnemy)
        {
            this.healthPackEffect = Mathf.Clamp01(this.healthPackEffect - 0.02f * this.timescale);
            this.bloodCameraColourCorrection.saturation = Mathf.Clamp01(Mathf.Clamp01(this.recoverTimer * 3f) - (float)((1.0 - (double)this.health) * (double)this.heartBeat * (double)this.heartBeatMultiplier * 0.5)) + this.root.camTransitionValue * 0.05f;
            if (this.root.showDodgeAlert && (double)this.health > 0.0)
            {
                if (this.screenCornerBlood.color != Color.black)
                    this.screenCornerBlood.color = Color.black;
            }
            else
            {
                Color color = !this.root.doGore ? Color.black : Color.white;
                color.a = (float)(((1.0 - (double)this.health) * 0.75 + (1.0 - (double)this.recoverTimer) * 0.25 + (double)Mathf.Clamp01(this.heartBeat * this.heartBeatMultiplier) * ((1.0 - (double)this.health) * (0.100000001490116 + (1.0 - (double)this.recoverTimer) * 0.400000005960464)) * 5.0 - (double)this.healthPackEffect * 0.300000011920929) * 0.5);
                if (this.screenCornerBlood.color != color)
                    this.screenCornerBlood.color = color;
            }
            if (this.root.dead)
            {
                this.heartBeatMultiplier = 1f;
                this.bloodCameraColourCorrection.saturation = 0.0f;
                this.cameraVignetteOverlay.blur = 1f;
                if ((double)this.screenCornerBlood.color.a != 0.0)
                {
                    int num1 = 0;
                    Color color1 = this.screenCornerBlood.color;
                    double num2 = (double)(color1.a = (float)num1);
                    Color color2 = this.screenCornerBlood.color = color1;
                }
            }
            else if (this.overrideControls || this.cutsceneMode || this.root.trailerMode || (this.root.sbClickCont && !this.root.sbClickContDontFreeze || this.blackBarTop.activeInHierarchy))
            {
                this.heartBeatMultiplier = 0.0f;
                this.bloodCameraColourCorrection.saturation = 1f;
                this.cameraVignetteOverlay.blur = 0.0f;
                if ((double)this.screenCornerBlood.color.a != 0.0)
                {
                    int num1 = 0;
                    Color color1 = this.screenCornerBlood.color;
                    double num2 = (double)(color1.a = (float)num1);
                    Color color2 = this.screenCornerBlood.color = color1;
                }
            }
        }
        if ((double)this.health <= 0.0)
        {
            this.recoverTimer = 0.0f;
            if (!this.isEnemy)
            {
                if (!this.root.dead)
                {
                    this.root.changeDifficulty(-0.8f);
                    if (this.bulletHitsSinceDodgeUsed > 5)
                        this.root.pedroDeathHintText = this.root.GetTranslation("pHint7");
                    else if ((double)this.root.timeSinceSlowMotionUsed > 1200.0)
                    {
                        this.root.pedroDeathHintText = this.root.GetTranslation("pHint8");
                    }
                    else
                    {
                        float num = UnityEngine.Random.value;
                        this.root.pedroDeathHintText = (double)num <= 0.75 ? ((double)num <= 0.5 ? ((double)num <= 0.25 ? this.root.GetTranslation("pHint12") : this.root.GetTranslation("pHint11")) : this.root.GetTranslation("pHint10")) : this.root.GetTranslation("pHint9");
                    }
                    this.root.showWeaponHud = false;
                    int num1 = 105;
                    Vector2 anchoredPosition1 = this.ammoHud.anchoredPosition;
                    double num2 = (double)(anchoredPosition1.x = (float)num1);
                    Vector2 vector2_1 = this.ammoHud.anchoredPosition = anchoredPosition1;
                    this.root.showHealthHud = false;
                    int num3 = -130;
                    Vector2 anchoredPosition2 = this.healthHud.anchoredPosition;
                    double num4 = (double)(anchoredPosition2.x = (float)num3);
                    Vector2 vector2_2 = this.healthHud.anchoredPosition = anchoredPosition2;
                    this.root.showScoreHud = false;
                    int num5 = -50;
                    Vector2 anchoredPosition3 = this.scoreHud.anchoredPosition;
                    double num6 = (double)(anchoredPosition3.y = (float)num5);
                    Vector2 vector2_3 = this.scoreHud.anchoredPosition = anchoredPosition3;
                    this.blackBarTop.SetActive(true);
                    this.blackBarBottom.SetActive(true);
                    this.root.rumble(0, 1f, 0.08f);
                    this.root.rumble(1, 1f, 0.08f);
                    PacketSender.SendPlayerLifeState(false);

                    xSpeed = 0;
                    targetXSpeed = 0;

                    this.root.dead = true;
                }
            }
            else if (this.onGround && !this.startedRolling && ((double)this.crouchAmount <= 0.100000001490116 && (double)this.targetRotation == 0.0) && (double)Mathf.Abs(this.rotationSpeed) < 0.200000002980232)
            {
                this.xSpeed = this.ySpeed = 0.0f;
                this.rBody.velocity = Vector3.zero;
                this.rBody.isKinematic = true;
                this.animator.SetLayerWeight(0, 1f);
                this.animator.SetLayerWeight(1, 0.0f);
                this.animator.SetLayerWeight(2, 0.0f);
                this.animator.SetLayerWeight(3, 0.0f);
                this.animator.SetLayerWeight(4, 0.0f);
                this.animator.SetLayerWeight(5, 0.0f);
                this.animator.CrossFadeInFixedTime("Death", 0.3f, 0);
                this.fireLightL.enabled = false;
                this.fireLightR.enabled = false;
                SwitchScript component = (SwitchScript)this.GetComponent(typeof(SwitchScript));
                if ((UnityEngine.Object)component != (UnityEngine.Object)null)
                    component.output = 1f;
                this.root.kAction = false;
                this.actualPlayerScript.overrideControls = true;
                this.actualPlayerScript.dontResetMousePosOnOverrideControls = true;
                this.actualPlayerScript.mousePos = this.transform.position;
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(this.transform.position, Vector3.down, out hitInfo, 4f, (int)this.layerMask))
                {
                    float num1 = hitInfo.point.y + 1.55f;
                    Vector3 position = this.transform.position;
                    double num2 = (double)(position.y = num1);
                    Vector3 vector3 = this.transform.position = position;
                }
                int num3 = 0;
                Vector3 localScale = this.enemyHealthBar.localScale;
                double num4 = (double)(localScale.x = (float)num3);
                Vector3 vector3_1 = this.enemyHealthBar.localScale = localScale;
                this.root.dontAllowReactionPedroTimer = 9999f;
                this.root.cCheckGiSc = true;
                this.root.giveScore(10000f, this.root.GetTranslation("bul12"), true);
                ++this.root.nrOfEnemiesKilled;
                ((Behaviour)this.GetComponent(typeof(PlayerScript))).enabled = false;
            }
        }
        else
            this.recoverTimer = Mathf.Clamp01(this.recoverTimer + 0.01f * this.timescale);
        if (!this.isEnemy)
        {
            if (this.root.difficultyMode == 0 || this.root.difficultyMode == 1)
                this.health = (double)this.health <= 0.660000026226044 ? ((double)this.health <= 0.330000013113022 ? Mathf.Clamp(this.health + 1f / 1000f * this.timescale * this.recoverTimer, 0.0f, 0.33f) : Mathf.Clamp(this.health + 1f / 1000f * this.timescale * this.recoverTimer, 0.0f, 0.66f)) : Mathf.Clamp(this.health + 1f / 1000f * this.timescale * this.recoverTimer, 0.0f, 1f);
            this.updateHealthHUD();
        }
        if (this.root.updateFramesDone > 3 && !this.isEnemy)
        {
            if (!this.rootShared.lowEndHardware)
            {
                if ((double)this.healthPackEffect > 0.0)
                {
                    float num1 = this.healthBarStartSize.y + 20f * this.healthPackEffect;
                    Vector2 sizeDelta = this.healthBar.sizeDelta;
                    double num2 = (double)(sizeDelta.y = num1);
                    Vector2 vector2 = this.healthBar.sizeDelta = sizeDelta;
                    this.healthBarSpeed -= this.healthPackEffect * 0.2f * this.timescale;
                }
                else if ((double)this.heartBeat >= 0.75)
                {
                    float num1 = this.root.Damp(this.healthBarStartSize.y + (float)(10.0 * (1.0 - (double)this.health)), this.healthBar.sizeDelta.y, 0.3f);
                    Vector2 sizeDelta = this.healthBar.sizeDelta;
                    double num2 = (double)(sizeDelta.y = num1);
                    Vector2 vector2 = this.healthBar.sizeDelta = sizeDelta;
                    this.healthBarSpeed += (float)((1.0 - (double)this.heartBeat) * (1.0 - (double)this.health)) * this.timescale;
                }
                else
                {
                    float num1 = this.root.Damp(this.healthBarStartSize.y, this.healthBar.sizeDelta.y, 0.5f);
                    Vector2 sizeDelta = this.healthBar.sizeDelta;
                    double num2 = (double)(sizeDelta.y = num1);
                    Vector2 vector2 = this.healthBar.sizeDelta = sizeDelta;
                }
                this.healthBarSpeed -= 0.2f * this.timescale;
                if ((double)this.healthBar.anchoredPosition.y - (double)this.healthBar.sizeDelta.y / 2.0 < 0.0)
                {
                    this.healthBarSpeed += (float)((0.0 - (double)this.healthBar.anchoredPosition.y + (double)this.healthBar.sizeDelta.y / 2.0) * 0.5) * this.timescale;
                    this.healthBarSpeed *= Mathf.Pow(0.7f, this.timescale);
                }
                float num3 = this.healthBar.anchoredPosition.y + this.healthBarSpeed * this.timescale;
                Vector2 anchoredPosition1 = this.healthBar.anchoredPosition;
                double num4 = (double)(anchoredPosition1.y = num3);
                Vector2 vector2_1 = this.healthBar.anchoredPosition = anchoredPosition1;
                this.healthIcon.localScale = Vector3.one + Vector3.one * this.healthBarSpeed * 0.25f + Vector3.one * this.healthPackEffect * 2f + Vector3.one * Mathf.Clamp01((float)(1.0 - (double)this.recoverTimer - 0.5));
                float num5 = this.healthIconStartPos.y + (float)(((double)this.healthBar.anchoredPosition.y - (double)this.healthIconStartPos.y) * 0.600000023841858);
                Vector2 anchoredPosition2 = this.healthIcon.anchoredPosition;
                double num6 = (double)(anchoredPosition2.y = num5);
                Vector2 vector2_2 = this.healthIcon.anchoredPosition = anchoredPosition2;
                if ((double)this.healthBar.anchoredPosition.y > 50.0)
                {
                    this.healthBarSpeed = 0.0f;
                    int num1 = 50;
                    Vector2 anchoredPosition3 = this.healthBar.anchoredPosition;
                    double num2 = (double)(anchoredPosition3.y = (float)num1);
                    Vector2 vector2_3 = this.healthBar.anchoredPosition = anchoredPosition3;
                }
            }
            this.healthIconImage.color = Color.Lerp(this.healthIconImageStartColour, (double)this.healthPackEffect <= 0.0 ? Color.red : Color.blue, (float)((double)this.healthBarSpeed * 0.800000011920929 * (1.0 - (double)this.health)));
            this.healthIconImage.color = Color.Lerp(this.healthIconImage.color, Color.white, this.healthPackEffect);
        }
        int num13 = (double)this.transform.eulerAngles.z <= 40.0 ? 1 : 0;
        if (num13 == 0)
            num13 = (double)this.transform.eulerAngles.z >= 320.0 ? 1 : 0;
        this.withinOkToStandAngles = num13 != 0;
        if (this.kCrouch && (double)this.targetXSpeed != 0.0 && (this.onGround && this.withinOkToStandAngles) && (!this.swinging && !this.pedroBoss))
        {
            if (!this.rolling && (double)this.crouchAmount < 0.100000001490116 && (!this.wallTouchRight && !this.wallTouchLeft))
                this.animator.CrossFade("DiveInToRoll", 0.1f, 0);
            this.rolling = true;
            this.xSpeed = this.lastXSpeedThatWasntZero = this.targetXSpeed;
            this.framesSinceRollStart = 0.0f;
        }
        else if ((double)this.framesSinceRollStart >= 10.0 && ((double)this.targetXSpeed == 0.0 || !this.kCrouch || !this.onGround))
            this.rolling = false;
        if (this.onGround && Physics.Raycast(this.transform.position, Vector3.up, 1.5f, (int)this.layerMask))
        {
            this.rolling = true;
            this.kJump = false;
            this.kCrouch = false;
            if ((double)this.kXDir == 0.0)
            {
                if (this.wallTouchRight)
                    this.xSpeed = this.targetXSpeed = -Mathf.Abs(this.lastXSpeedThatWasntZero);
                else if (this.wallTouchLeft)
                    this.xSpeed = this.targetXSpeed = Mathf.Abs(this.lastXSpeedThatWasntZero);
            }
        }
        this.framesSinceRollStart += this.timescale;
        this.okToStand = !this.rolling && this.withinOkToStandAngles;
        if (this.startedRolling && (double)this.onGroundTimer > 30.0 && (double)this.targetXSpeed != 0.0)
        {
            this.targetXSpeed = Mathf.Abs(this.targetXSpeed) / this.targetXSpeed * 8f;
            this.xSpeed = this.lastXSpeedThatWasntZero = this.targetXSpeed;
        }
        this.kickTimer = Mathf.Clamp(this.kickTimer - this.timescale, 0.0f, this.kickTimer);
        this.animator.SetLayerWeight(5, 1f - Mathf.SmoothStep(1f, 0.0f, Mathf.Clamp(this.kickTimer * 2.2f, 0.0f, 35f) / 35f));
        this.kickFreezeTimer = this.onGround ? Mathf.Clamp(this.kickFreezeTimer - this.timescale, 0.0f, this.kickFreezeTimer) : 0.0f;
        if (this.onGround)
        {
            if (this.okToStand)
            {
                if ((double)this.kickFreezeTimer > 0.0)
                    this.targetXSpeed = 0.0f;
            }
            else if ((double)Mathf.Abs(this.xSpeed) < 6.0)
                this.xSpeed = Mathf.Abs(this.lastXSpeedThatWasntZero) / this.lastXSpeedThatWasntZero * 6f;
        }
        else if (this.justWallJumped)
            this.wallJumpTimer += this.timescale;
        else if (!this.kAction && !this.swinging && (this.okToStand && !this.startedRolling))
        {
            this.targetRotation = Mathf.Clamp(this.xSpeed * Mathf.Clamp((float)(-((double)this.ySpeed + 1.0) * 0.300000011920929), -2f, 2f), -12f, 12f);
            this.rotationSpeed = 0.0f;
        }
        this.topDuckAmount = this.root.Damp(0.0f, this.topDuckAmount, 0.1f);
        if (!this.swinging)
            this.followSpeed = Vector3.zero;
        this.justLandedTimer = Mathf.Clamp(this.justLandedTimer - this.timescale, 0.0f, this.justLandedTimer);
        if (!this.skyfall && Physics.Raycast(this.transform.position, Vector3.down, out this.legRayHit, this.justJumped || (double)this.ySpeed < -0.600000023841858 * ((double)Time.deltaTime * 60.0) || (double)this.ySpeed > 0.0 ? ((double)this.ySpeed <= 0.0 ? this.legLength : this.legLength / 2f) : this.legLength + (this.swinging ? 0.4f : 1f), (int)this.layerMask))
        {
            this.groundTransform = this.legRayHit.collider.transform;

            if (!MultiplayerManagerTest.singleplayerMode && MultiplayerManagerTest.inst.playerObjects != null)
                if (MultiplayerManagerTest.inst.playerObjects.ContainsKey(MultiplayerManagerTest.inst.playerID))
                    MultiplayerManagerTest.inst.playerObjects[MultiplayerManagerTest.inst.playerID].groundTransform = groundTransform;

            if (!this.onGround)
            {
                this.playerAnimationsScript.footstep(4);
                float strength = Mathf.Clamp01((float)(-(double)this.rBody.velocity.y * 0.0199999995529652)) * 0.5f;
                this.root.rumble(0, strength, (float)(0.0500000007450581 + (double)strength * 0.100000001490116));
                this.root.rumble(1, strength, (float)(0.0500000007450581 + (double)strength * 0.100000001490116));
                if ((double)this.transform.rotation.eulerAngles.z < 18.0 || (double)this.transform.rotation.eulerAngles.z > 342.0)
                {
                    this.crouchAmount = Mathf.Clamp(Mathf.Abs(this.rBody.velocity.y) * 0.05f, 0.0f, 0.75f);
                    this.justLandedTimer = Mathf.Abs(this.rBody.velocity.y) * 0.7f;
                }
                this.justJumped = false;
            }
            this.degreesRotatedInAir = 0.0f;
            this.onGround = true;
            this.onGroundTimer += this.timescale;
            this.inAirTimer = 0.0f;
            this.disableFloatJump = false;
            this.lastPosOnGround = this.transform.position;
            this.justPulseJumped = false;
            this.justWallJumped = false;
            this.jumpedFromSwinging = false;
            this.justJumpedFromSkateboard = false;
            this.justJumpedOffEnemy = false;
            this.wallJumpTimer = 0.0f;
            this.extraJumpPower = 0.0f;
            if (this.okToStand)
            {
                this.targetRotation = 0.0f;
                this.rotationSpeed = 0.0f;
            }
            else
                this.rotationSpeed = (double)Mathf.Abs(this.lastXSpeedThatWasntZero) >= 2.0 ? (float)(-(double)this.lastXSpeedThatWasntZero * 2.0) : (float)(-((double)Mathf.Abs(this.lastXSpeedThatWasntZero) / (double)this.lastXSpeedThatWasntZero) * 4.0);
            float num1 = this.root.Damp(this.legRayHit.point.y + this.legLength, this.transform.position.y, 0.2f);
            Vector3 position = this.transform.position;
            double num2 = (double)(position.y = num1);
            Vector3 vector3 = this.transform.position = position;
            if (this.legRayHit.transform.tag == "ABMoveFollow")
            {
                SwitchABMoveScript component = (SwitchABMoveScript)this.legRayHit.transform.GetComponent(typeof(SwitchABMoveScript));
                this.followSpeed = component.movePos.normalized * (component.moveSpeed * component.movePos.magnitude) * 60f;
                this.root.autoSaveTimer = 0.0f;
            }
            this.visualCrouchAmount = Mathf.Clamp((float)(1.0 - ((double)this.transform.position.y - (double)this.crouchLegLength - (double)this.legRayHit.point.y)), 0.0f, 1f);
            if ((double)this.crouchAmount < (double)this.visualCrouchAmount * 0.899999976158142)
                this.visualCrouchAmount = (double)this.visualCrouchAmount >= (double)this.prevVisualCrouchAmount ? Mathf.Clamp(this.prevVisualCrouchAmount - 0.1f, 0.0f, 1f) : this.visualCrouchAmount;
            this.animator.SetFloat("CrouchAmount", this.visualCrouchAmount);
            this.prevVisualCrouchAmount = (double)this.visualCrouchAmount > 0.00999999977648258 ? this.visualCrouchAmount : 0.0f;
            Debug.DrawLine(this.legRayHit.point, this.transform.position);
        }
        else
        {
            if (this.onGround)
            {
                this.groundTransform = (Transform)null;
                if (!this.overrideControls && !this.cutsceneMode && ((double)this.kXDir == 0.0 && !this.kJump) && (!this.startedRolling && (double)Mathf.Abs(this.xSpeed) < 3.79999995231628 && (double)this.ySpeed <= 0.0))
                {
                    float num1 = this.transform.position.x + (this.lastPosOnGround.x - this.transform.position.x);
                    Vector3 position = this.transform.position;
                    double num2 = (double)(position.x = num1);
                    Vector3 vector3 = this.transform.position = position;
                    this.xSpeed = 0.0f;
                }
            }
            this.onGround = false;
            this.onGroundTimer = 0.0f;
            this.inAirTimer += this.timescale;
            this.topLength = 2.2f;
            if (!this.skyfall)
            {
                if (Physics.Raycast(this.transform.position, this.transform.up, out this.topRayHit, this.topLength, (int)this.layerMask))
                {
                    this.hitDist = Vector3.Distance(this.transform.position, this.topRayHit.point);
                    this.tempTopDuckAmountCheck = (float)(1.0 - (double)this.hitDist / (double)this.topLength) * 90f;
                    this.topDuckAmount = (double)this.tempTopDuckAmountCheck <= (double)this.topDuckAmount ? this.topDuckAmount : this.tempTopDuckAmountCheck;
                    if (this.faceRight && this.wallTouchRight || !this.faceRight && this.wallTouchLeft)
                        this.aimBlendTarget = this.aimBlend = 0.0f;
                    if ((double)Vector3.Distance(this.transform.position, this.topRayHit.point) < 0.600000023841858)
                    {
                        this.ySpeed = (double)this.ySpeed >= -0.300000011920929 ? -0.3f : this.ySpeed;
                        float num1 = this.root.Damp(this.topRayHit.point.y - 0.6f, this.transform.position.y, 0.2f);
                        Vector3 position = this.transform.position;
                        double num2 = (double)(position.y = num1);
                        Vector3 vector3 = this.transform.position = position;
                    }
                    if (this.topRayHit.transform.tag == "Glass")
                    {
                        ((GlassWindowScript)this.topRayHit.transform.GetComponent(typeof(GlassWindowScript))).breakGlass(false);
                        this.root.rumble(0, 0.8f, 0.2f);
                        this.root.rumble(1, 0.2f, 0.4f);
                        ++this.statsTracker.jumpedThroughWindows;
                        this.statsTracker.achievementCheck();
                        this.dramaticEntranceTimer = 120f;
                    }
                    else if (this.topRayHit.transform.tag == "BouncePad")
                        ((BouncePadScript)this.topRayHit.transform.GetComponent(typeof(BouncePadScript))).doThing();
                }
                if (Physics.Raycast(this.transform.position, this.transform.up, out this.topRayHit, this.topLength, (int)this.layerMask))
                {
                    Debug.DrawLine(this.topRayHit.point, this.transform.position);
                    this.hitDist = Vector3.Distance(this.transform.position, this.topRayHit.point);
                    this.tempTopDuckAmountCheck = (1.0 - (double)this.hitDist / (double)this.topLength) * 90.0 <= (double)this.topDuckAmount ? this.topDuckAmount : (float)((1.0 - (double)this.hitDist / (double)this.topLength) * 90.0);
                    this.topDuckAmount = (double)this.tempTopDuckAmountCheck <= (double)this.topDuckAmount ? this.topDuckAmount : this.tempTopDuckAmountCheck;
                }
            }
            if (!this.root.dontAllowAutomaticFallDeath && ((double)this.rBody.velocity.y < -40.0 * (double)this.speedModifier && (double)Vector3.Distance(this.transform.position, this.lastPosOnGround) > 30.0))
                this.health = 0.0f;
        }
        this.rollInMidAir = false;
        if (!this.swinging && !this.onGround && ((double)this.ySpeed < 0.0 && this.startedRolling) && Physics.Raycast(this.transform.position, Vector3.down, out this.rollInMidAirRayHit, 4f, (int)this.layerMask))
        {
            this.rollInMidAir = true;
            Debug.DrawLine(this.rollInMidAirRayHit.point, this.transform.position);
        }
        if (this.okToStand && !this.kCrouch && !this.startedRolling)
            this.crouchAmount = this.root.Damp((float)(0.0 + (!this.gamepad || (double)this.kXDir != 0.0 ? 0.0 : (double)Mathf.Clamp(axisRaw, -1f, 0.0f) * -1.0)), this.crouchAmount, Mathf.Clamp01(0.25f - Mathf.Clamp(this.justLandedTimer / 5f, 0.0f, 0.2f)));
        else if (!this.swinging)
            this.crouchAmount = this.root.Damp(1f, this.crouchAmount, 0.5f);
        this.legLength = Mathf.Lerp(this.standLegLength, this.crouchLegLength, this.crouchAmount) * this.transform.localScale.y;
        this.touchedEnemyTimer = Mathf.Clamp(this.touchedEnemyTimer - this.timescale, 0.0f, this.touchedEnemyTimer);
        if (this.kJump)
            this.kJumpDownTimer = 30f;
        this.floatJump = !this.swinging && this.kJumpHeldDown;
        if (this.floatJump)
        {
            if ((double)this.kJumpDownTimer > 0.0)
                this.kJumpDownTimer = Mathf.Clamp(this.kJumpDownTimer - this.timescale, 0.0f, this.kJumpDownTimer);
        }
        else
            this.kJumpDownTimer = 0.0f;
        this.wallTouchRightTransform = (Transform)null;
        this.wallTouchLeftTransform = (Transform)null;
        if (Physics.Raycast(this.transform.position - new Vector3(0.0f, (double)this.touchedEnemyTimer <= 0.0 ? 0.0f : 0.8f, 0.0f), Vector3.right, out this.sideRayHit, this.raycastWidthRange, (int)this.layerMaskIncEnemiesAndEnemyGameCollision))
        {
            this.tempHittingGlass = this.sideRayHit.transform.tag == "Glass";
            if (!this.tempHittingGlass)
            {
                if ((double)this.targetXSpeed > 0.0 && (!this.justWallJumped || this.swinging))
                    this.xSpeed = 0.0f;
                if (this.sideRayHit.transform.tag == "Enemy")
                {
                    this.touchedEnemyTimer = 40f;
                    if (this.kJump)
                    {
                        this.groundTransform = this.sideRayHit.collider.transform;
                        this.wallJump((EnemyScript)this.sideRayHit.transform.GetComponent(typeof(EnemyScript)));
                    }
                }
                else if (this.sideRayHit.transform.tag == "BouncePad")
                    ((BouncePadScript)this.sideRayHit.transform.GetComponent(typeof(BouncePadScript))).doThing();
                this.wallTouchRight = true;
                this.wallTouchRightTransform = this.sideRayHit.transform;
                if (!this.onGround && !this.kAction && (this.justWallJumped || (double)this.transform.eulerAngles.z <= 40.0 || (double)this.transform.eulerAngles.z >= 320.0))
                {
                    this.targetRotation = 25f;
                    if ((double)this.kJumpDownTimer > 0.0)
                        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.targetRotation);
                }
                else if (!this.onGround && this.kAction && (double)this.kXDir >= 0.0)
                {
                    this.targetRotation = 25f;
                    this.transform.rotation = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, this.targetRotation), this.transform.rotation, 0.5f);
                }
            }
            else
            {
                ((GlassWindowScript)this.sideRayHit.transform.GetComponent(typeof(GlassWindowScript))).breakGlass(!this.onGround);
                this.root.rumble(0, 0.8f, 0.2f);
                this.root.rumble(1, 0.2f, 0.4f);
                ++this.statsTracker.jumpedThroughWindows;
                this.statsTracker.achievementCheck();
                this.dramaticEntranceTimer = 120f;
            }
        }
        else
            this.wallTouchRight = false;
        if (Physics.Raycast(this.transform.position - new Vector3(0.0f, (double)this.touchedEnemyTimer <= 0.0 ? 0.0f : 0.8f, 0.0f), Vector3.left, out this.sideRayHit, this.raycastWidthRange, (int)this.layerMaskIncEnemiesAndEnemyGameCollision))
        {
            this.tempHittingGlassL = this.sideRayHit.transform.tag == "Glass";
            if (!this.tempHittingGlassL)
            {
                if ((double)this.targetXSpeed < 0.0 && (!this.justWallJumped || this.swinging))
                    this.xSpeed = 0.0f;
                if (this.sideRayHit.transform.tag == "Enemy")
                {
                    this.touchedEnemyTimer = 40f;
                    if (this.kJump)
                    {
                        this.groundTransform = this.sideRayHit.collider.transform;
                        this.wallJump((EnemyScript)this.sideRayHit.transform.GetComponent(typeof(EnemyScript)));
                    }
                }
                else if (this.sideRayHit.transform.tag == "BouncePad")
                    ((BouncePadScript)this.sideRayHit.transform.GetComponent(typeof(BouncePadScript))).doThing();
                this.wallTouchLeft = true;
                this.wallTouchLeftTransform = this.sideRayHit.transform;
                if (!this.onGround && !this.kAction && (this.justWallJumped || (double)this.transform.eulerAngles.z <= 40.0 || (double)this.transform.eulerAngles.z >= 320.0))
                {
                    this.targetRotation = 335f;
                    if ((double)this.kJumpDownTimer > 0.0)
                        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.targetRotation);
                }
                else if (!this.onGround && this.kAction && (double)this.kXDir <= 0.0)
                {
                    this.targetRotation = 335f;
                    this.transform.rotation = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, this.targetRotation), this.transform.rotation, 0.5f);
                }
            }
            else
            {
                ((GlassWindowScript)this.sideRayHit.transform.GetComponent(typeof(GlassWindowScript))).breakGlass(!this.onGround);
                this.root.rumble(0, 0.8f, 0.2f);
                this.root.rumble(1, 0.2f, 0.4f);
                ++this.statsTracker.jumpedThroughWindows;
                this.statsTracker.achievementCheck();
                this.dramaticEntranceTimer = 120f;
            }
        }
        else
            this.wallTouchLeft = false;
        if (this.wallTouchLeft || this.wallTouchRight)
        {
            if (!this.onGround && !this.justWallJumped && !this.dontLockTowall)
                this.xSpeed = 0.0f;
            if (!this.okToStand && this.onGround)
            {
                this.rotationSpeed = 0.0f;
                this.targetRotation = 0.0f;
            }
        }
        this.dontAllowWallJumpTimer = Mathf.Clamp(this.dontAllowWallJumpTimer - this.timescale, 0.0f, this.dontAllowWallJumpTimer);
        if (!this.onGround && !this.aboutToHitGround)
        {
            if (Physics.Raycast(this.transform.position + new Vector3(!this.kAction ? 0.0f : Mathf.Clamp(this.targetXSpeed, -0.15f, 0.15f), 1f, 0.0f), -this.transform.up, out this.legRayHit, this.standLegLength + 0.1f, (int)this.layerMaskIncEnemiesAndEnemyGameCollision))
            {
                Debug.DrawLine(this.transform.position + new Vector3(!this.kAction ? 0.0f : Mathf.Clamp(this.targetXSpeed, -0.15f, 0.15f), 0.0f, 0.0f), this.transform.position - this.transform.up * (this.standLegLength + 0.1f), new Color(1f, 1f, 1f, 1f));
                if ((this.kJump || (double)this.kJumpDownTimer > 0.0) && ((double)this.jumpCoolDown <= 0.0 && !this.swinging) && ((double)Mathf.Abs(this.targetRotation) > 12.0 && this.legRayHit.transform.tag != "Glass" && this.legRayHit.transform.tag != "BouncePad") && (double)this.transform.up.y > 0.699999988079071)
                {
                    this.groundTransform = this.legRayHit.collider.transform;
                    this.kJumpDownTimer = 0.0f;
                    this.wallJump((EnemyScript)this.legRayHit.transform.GetComponent(typeof(EnemyScript)));
                }
            }
        }
        else
            this.dontLockTowall = false;
        if ((double)this.jumpCoolDown > 0.0)
            this.jumpCoolDown -= this.timescale;
        if (this.kJump && (double)this.jumpCoolDown <= 0.0 && (this.onGround || (double)this.inAirTimer < 10.0 && !this.justJumped))
        {
            this.kJumpDownTimer = 0.0f;
            this.extraJumpPower = !this.pedroBoss ? 1.75f : 2.1f;
            this.ySpeed = !this.pedroBoss ? 1.2f : 10f;
            this.justJumped = true;
            this.jumpedFromSwinging = this.swinging;
            this.justJumpedAnimationBoolTimer = 20f;
            this.rollInMidAir = false;
            this.xSpeed *= Mathf.Pow(1.4f, this.root.fixedTimescale);
            this.jumpCoolDown = 10f;
            this.root.rumble(0, 0.025f, 0.05f);
            this.root.rumble(1, 0.05f, 0.025f);
            if (this.startedRolling)
            {
                this.animator.CrossFade("JumpFromRoll", 0.05f, 0);
                this.rotationSpeed = Mathf.Clamp(this.rotationSpeed / 1.7f, -10f, 10f);
            }
            else if ((double)this.crouchAmount > 0.100000001490116)
                this.animator.CrossFade("JumpCrouch", 0.05f, 0);
            else if (this.faceRight && (double)this.xSpeed > 2.0 || !this.faceRight && (double)this.xSpeed < -2.0)
                this.animator.CrossFade("JumpForward", 0.05f, 0);
            else if (!this.faceRight && (double)this.xSpeed > 2.0 || this.faceRight && (double)this.xSpeed < -2.0)
                this.animator.CrossFade("JumpBackwards", 0.05f, 0);
            this.doPlayerJumpSound();
        }
        if (this.justJumped && this.kAction)
            this.floatJump = true;
        if (!this.floatJump && !this.justPulseJumped)
            this.extraJumpPower = 0.0f;
        if (this.kAction && !this.onGround && (double)Mathf.Abs(this.kXDir) > 0.0 && ((!this.justWallJumped || (double)this.kXDir != 0.0) && (!this.justWallJumped || (double)this.wallJumpTimer >= 30.0)))
            this.rotationSpeed = this.root.Damp((float)(-(double)this.kXDir * 8.0), this.rotationSpeed, !this.justWallJumped || ((double)this.kXDir <= 0.0 || (double)this.xSpeed >= 0.0) && ((double)this.kXDir >= 0.0 || (double)this.xSpeed <= 0.0) ? 0.3f : Mathf.Clamp01((this.wallJumpTimer - 30f) / 40f) * 0.3f);
        if (this.skyfall)
        {
            this.cameraScript.bigScreenShake = (float)(0.0299999993294477 + (double)Mathf.Abs(this.rBody.velocity.y * 0.01f) + (!this.kCrouch ? 0.0 : 0.0299999993294477));
            this.onGround = false;
            this.crouchAmount = this.visualCrouchAmount = 0.0f;
            this.justWallJumped = false;
            this.justJumpedOffEnemy = false;
            if (!this.kAction)
                this.targetRotation = 180f + this.xSpeed * 4f;
            if (!this.isEnemy)
                this.skyfallYPos = !this.kCrouch ? (!this.dodging ? this.root.Damp(0.0f, this.skyfallYPos, 0.08f) : this.root.Damp(9f, this.skyfallYPos, 0.12f)) : this.root.Damp(-14f, this.skyfallYPos, 0.08f);
            if ((double)this.transform.position.x < -6.0)
                this.xSpeed += this.root.DampAdd(-6f, this.transform.position.x, 0.08f);
            else if ((double)this.transform.position.x > 6.0)
                this.xSpeed += this.root.DampAdd(6f, this.transform.position.x, 0.08f);
        }
        this.targetRotation += this.rotationSpeed * this.root.timescale;
        this.transform.rotation = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, this.targetRotation), this.transform.rotation, !this.swinging ? (!this.skyfall || this.kAction ? 0.2f : 0.3f) : 0.1f);
        this.degreesRotatedInAir += this.rotationSpeed * this.root.timescale;
        if (this.cutsceneMode)
        {
            if (!this.cutsceneModeDoOnce)
            {
                this.playerCollider.enabled = false;
                this.cutsceneModeDoOnce = true;
            }
            this.degreesRotatedInAir = 0.0f;
            this.onGround = true;
            this.ySpeed = 0.0f;
            this.rBody.velocity = Vector3.zero;
        }
        else if (this.cutsceneModeDoOnce)
        {
            this.playerCollider.enabled = true;
            this.cutsceneModeDoOnce = false;
        }
        if (this.onGround)
            this.faceRight = (double)this.mousePos.x > (double)this.transform.position.x;
        else if (!this.dualAimMode)
            this.faceRight = (double)this.transform.InverseTransformPoint(this.mousePos).x > 0.0;
        if ((double)this.zPushBack > 0.0)
        {
            float num1 = this.root.Damp(2f, this.transform.position.z, 0.1f);
            Vector3 position = this.transform.position;
            double num2 = (double)(position.z = num1);
            Vector3 vector3 = this.transform.position = position;
            this.zPushBack -= this.timescale;
        }
        else
        {
            float num1 = this.root.Damp(0.0f, this.transform.position.z, 0.1f);
            Vector3 position = this.transform.position;
            double num2 = (double)(position.z = num1);
            Vector3 vector3 = this.transform.position = position;
        }
        if (this.aimWithLeftArm)
            this.timeSinceSplitAim += this.timescale;
        if (this.weapon == 9 && !this.kSecondaryAim)
            this.timeSinceSniperAim += this.timescale;
        if (this.weapon == 5 && !this.kSecondaryAim && (double)this.secondaryAmmo[5] > 0.0)
            this.timeSinceGrenadeLaunched += this.timescale;
        this.timeSinceShotFired += this.timescale;
        this.fireWeapon = false;
        if ((double)this.fireDelay > 0.0)
            this.fireDelay -= this.timescale;
        if ((double)this.secondaryFireDelay > 0.0)
            this.secondaryFireDelay -= this.timescale;
        if (this.kFire)
        {
            if (!this.reloading && !this.startedRolling && (!this.root.disableShooting && !this.fightMode) && (this.automaticWeapon || !this.kFireDown) && ((this.weapon == 5 && this.shotsInARow < 6 || this.weapon != 5) && (double)this.fireDelay <= 0.0))
            {
                if ((double)this.ammo[this.weapon] > 0.0)
                {
                    this.upperArmAnimationBlendTimer = 120f;
                    this.upperArmAnimationBlend = 0.0f;
                    if ((this.weapon != 7 || !this.kSecondaryAim) && (this.weapon != 10 && this.rootShared.curVisualQualityLevel > 0))
                    {
                        if (this.fireLeftGun)
                        {
                            this.fireLightL.enabled = true;
                            this.fireLightL.intensity = (float)UnityEngine.Random.Range(4, 6);
                        }
                        else
                        {
                            this.fireLightR.enabled = true;
                            this.fireLightR.intensity = (float)UnityEngine.Random.Range(4, 6);
                        }
                    }
                    ++this.shotsInARow;
                    this.timeSinceShotFired = 0.0f;
                    if (this.aimWithLeftArm && this.seperateAim && (double)this.timeSinceSplitAim >= 0.0)
                        this.timeSinceSplitAim = 0.0f;
                    this.fireWeapon = true;
                    this.ammo[this.weapon] = Mathf.Clamp(this.ammo[this.weapon] - 1f, 0.0f, this.ammoFullClip[this.weapon]);
                    this.updateAmmoHUD();
                }
                else if ((double)this.ammoTotal[this.weapon != 4 ? this.weapon : 3] > 0.0)
                {
                    this.reload();
                }
                else
                {
                    this.handRAudio.clip = this.emptyGunSound;
                    this.handRAudio.Play();
                    this.handLAudio.clip = this.emptyGunSound;
                    this.handLAudio.Play();
                    this.fireLeftGun = !this.fireLeftGun;
                    this.fireDelay = 10f;
                }
            }
            if (this.reloading && this.allowFireWhileReloading)
                this.fireWhileReloading = true;
            this.kFireDown = true;
        }
        else
        {
            this.shotsInARow = 0;
            this.kFireDown = false;
        }
        if (this.kReload)
            this.reload();
        if (!this.isEnemy && (double)this.ammo[this.weapon] <= 0.0)
        {
            if ((double)this.ammoTotal[this.weapon != 4 ? this.weapon : 3] <= 0.0)
                this.root.showHintChangeWeapon = true;
            else
                this.root.showHintReload = true;
        }
        if ((double)this.fireLightR.intensity > 0.0)
            this.fireLightR.intensity -= this.timescale * 1.8f;
        else
            this.fireLightR.enabled = false;
        if ((double)this.fireLightL.intensity > 0.0)
            this.fireLightL.intensity -= this.timescale * 1.8f;
        else
            this.fireLightL.enabled = false;
        this.punchTimer = Mathf.Clamp(this.punchTimer - this.timescale, 0.0f, this.punchTimer);
        this.animator.speed = this.root.timescaleRaw * this.speedModifier;
        if (!this.pedroBoss)
            this.animator.SetFloat("xSpeed", this.faceRight && (double)this.xSpeed > 0.0 && !this.dualAimMode || !this.faceRight && (double)this.xSpeed < 0.0 || this.faceRight && (double)this.xSpeed < 0.0 && this.dualAimMode ? Mathf.Abs(this.xSpeed) : -Mathf.Abs(this.xSpeed));
        int num14 = (double)this.xSpeed > 0.0 ? 1 : 0;
        if (num14 != 0)
            num14 = (double)this.transform.eulerAngles.z < 90.0 ? 1 : 0;
        if (num14 == 0)
        {
            int num1 = (double)this.xSpeed < 0.0 ? 1 : 0;
            num14 = num1 == 0 ? num1 : ((double)this.transform.eulerAngles.z > 270.0 ? 1 : 0);
        }
        this.almostOkToStand = num14 != 0;
        if ((double)this.crouchAmount > 0.300000011920929 && !this.startedRolling)
        {
            this.pushedAgainstWall = false;
            if ((double)this.mousePos.x > (double)this.transform.position.x)
            {
                if (this.wallTouchRight)
                    this.pushedAgainstWall = true;
                else if (Physics.Raycast(this.transform.position, Vector3.right, this.raycastWidthRange + 0.8f, (int)this.layerMask))
                    this.pushedAgainstWall = true;
            }
            else if (this.wallTouchLeft)
                this.pushedAgainstWall = true;
            else if (Physics.Raycast(this.transform.position, Vector3.left, this.raycastWidthRange + 0.8f, (int)this.layerMask))
                this.pushedAgainstWall = true;
        }
        else
            this.pushedAgainstWall = this.wallTouchRight && (double)this.mousePos.x > (double)this.transform.position.x || this.wallTouchLeft && (double)this.mousePos.x < (double)this.transform.position.x;
        if (this.pushedAgainstWall)
        {
            if (this.wallTouchRight && !Physics.Raycast(this.transform.position + new Vector3(0.0f, 0.75f, 0.0f), Vector3.right, this.raycastWidthRange + 0.8f, (int)this.layerMask))
                this.pushedAgainstWall = false;
            else if (this.wallTouchLeft && !Physics.Raycast(this.transform.position + new Vector3(0.0f, 0.75f, 0.0f), Vector3.left, this.raycastWidthRange + 0.8f, (int)this.layerMask))
                this.pushedAgainstWall = false;
        }
        int num15 = !this.swinging ? 1 : 0;
        if (num15 != 0)
            num15 = !this.onGround ? 1 : 0;
        if (num15 != 0)
            num15 = (double)this.ySpeed < 0.0 ? 1 : 0;
        if (num15 != 0)
            num15 = Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Vector3.down, !this.okToStand ? this.legLength + 1.5f : this.legLength + 1.1f * Mathf.Abs(this.ySpeed * 0.11f), (int)this.layerMask) ? 1 : 0;
        this.aboutToHitGround = num15 != 0;
        bool flag1 = false;
        if (!this.kCrouch && this.aboutToHitGround && !this.onGround && ((double)this.wallJumpTimer <= 0.0 || (double)this.wallJumpTimer > 60.0))
        {
            this.targetRotation = 0.0f;
            flag1 = true;
        }
        if ((!this.wallTouchRight || (double)this.targetXSpeed <= 0.0) && (!this.wallTouchLeft || (double)this.targetXSpeed >= 0.0) && !this.skyfall && ((this.onGround || this.aboutToHitGround && this.startedRolling || this.aboutToHitGround && !flag1) && (!this.okToStand && (!this.almostOkToStand || this.startedRolling)) || this.rollInMidAir))
        {
            if (!this.startedRolling && (double)this.topDuckAmount > 20.0)
                this.animator.Play("Roll", 0, 0.0f);
            this.kickTimer = this.root.Damp(0.0f, this.kickTimer, 0.3f);
            this.startedRolling = true;
            this.animator.SetBool("Rolling", true);
            this.aimBlendTarget = this.aimBlend = 0.0f;
            this.lookAimBlendTarget = 0.0f;
            this.fightPoseBlendTimer = 0.0f;
        }
        else
        {
            if (this.startedRolling)
                this.aimBlend = 0.5f;
            this.startedRolling = false;
            this.animator.SetBool("Rolling", false);
            this.aimBlendTarget = 1f;
            this.lookAimBlendTarget = 1f;
        }
        Animator animator = this.animator;
        int num16 = this.onGround ? 1 : 0;
        if (num16 == 0)
            num16 = this.aboutToHitGround ? 1 : 0;
        if (num16 == 0)
            num16 = this.rollInMidAir ? 1 : 0;
        int num17 = num16 == 0 ? 1 : 0;
        animator.SetBool("InAir", num17 != 0);
        if ((double)this.pushedAgainstWallShootTimer > 0.0)
            this.pushedAgainstWallShootTimer -= this.timescale;
        if ((this.pushedAgainstWall || this.wallTouchLeft || this.wallTouchRight) && (this.kCrouch && this.onGround) && ((double)this.targetXSpeed == 0.0 || this.wallTouchLeft || this.wallTouchRight))
        {
            this.startedRolling = false;
            this.rolling = false;
            this.animator.SetBool("Rolling", false);
            if (this.pushedAgainstWall)
            {
                if (this.fireWeapon || this.weapon == 9 && this.kSecondaryAim)
                {
                    if (!this.faceRight && !Physics.Raycast(this.transform.position + new Vector3(0.0f, 1.5f, 0.0f), Vector3.left, 1f, (int)this.layerMask) || this.faceRight && !Physics.Raycast(this.transform.position + new Vector3(0.0f, 1.5f, 0.0f), Vector3.right, 1f, (int)this.layerMask))
                    {
                        if (this.fireWeapon && (double)this.shootFromCoverLayerWeight <= 0.0)
                            this.fireWeaponDelay = 2f;
                        this.shootFromCoverLayerWeight = 4f;
                        if (!this.faceRight)
                        {
                            this.animator.Play("ShootFromCoverLeft", 3, 0.0f);
                            this.animator.Play("ShootFromCoverLeft", 4, 0.0f);
                        }
                        else if (this.faceRight)
                        {
                            this.animator.Play("ShootFromCoverRight", 3, 0.0f);
                            this.animator.Play("ShootFromCoverRight", 4, 0.0f);
                        }
                    }
                    else
                    {
                        this.pushedAgainstWallShootTimer = 60f;
                        this.aimBlendTarget = this.aimBlend = 1f;
                        this.lookAimBlendTarget = this.lookAimBlend = 1f;
                    }
                }
                if ((double)this.shootFromCoverLayerWeight > 0.0)
                {
                    this.shootFromCoverLayerWeight -= 0.1f * this.timescale;
                    this.aimBlendTarget = this.aimBlend = 0.3f;
                    this.lookAimBlendTarget = this.lookAimBlend = 0.3f;
                }
                else if ((double)this.pushedAgainstWallShootTimer <= 0.0)
                {
                    this.shootFromCoverLayerWeight = 0.0f;
                    this.aimBlendTarget = 0.0f;
                    this.lookAimBlendTarget = 0.0f;
                }
            }
            else
            {
                this.aimBlendTarget = 1f;
                this.lookAimBlendTarget = 1f;
            }
        }
        if (!this.pushedAgainstWall || this.reloading)
            this.shootFromCoverLayerWeight = 0.0f;
        if (this.aimWithLeftArm || this.twoHandedWeapon)
        {
            this.animator.SetLayerWeight(3, 0.0f);
            this.animator.SetLayerWeight(4, Mathf.Clamp01(this.shootFromCoverLayerWeight));
        }
        else
        {
            this.animator.SetLayerWeight(3, Mathf.Clamp01(this.shootFromCoverLayerWeight));
            this.animator.SetLayerWeight(4, 0.0f);
        }
        if ((this.flippingTable || this.reloading) && !this.startedRolling)
        {
            this.lookAimBlendTarget = 1f;
            this.aimBlendTarget = 0.0f;
            this.fightMode = false;
            this.reloadLayerWeight = this.root.Damp(1f, this.reloadLayerWeight, 0.3f);
        }
        else
            this.reloadLayerWeight = this.root.Damp(0.0f, this.reloadLayerWeight, 0.3f);
        if (this.fightMode)
        {
            this.lookAimBlendTarget = 1f;
            this.aimBlendTarget = 0.0f;
        }
        else if (!this.reloading && !this.flippingTable && !this.startedRolling && ((double)Vector3.Distance(this.mousePos, this.neck.position) < 1.89999997615814 || this.root.sbClickCont && !this.root.sbClickContDontFreeze))
        {
            if ((double)this.upperArmAnimationBlendTimer <= 0.0)
                this.aimBlendTarget = 0.0f;
            else if (!this.dodging)
            {
                this.aimBlend = this.aimBlendTarget = 1f;
                this.mousePos = this.neck.position + (this.mousePos - this.neck.position).normalized * 2.5f;
                if (!this.seperateAim)
                    this.mousePos2 = this.mousePos;
            }
        }
        this.timeSinceDodgeUsed += this.timescale;
        if (this.kDodge && !this.kCrouch && !this.startedRolling)
        {
            if (!this.kDodgeDown)
            {
                if ((double)this.dodgingCoolDown <= 0.0 || this.propellerHat)
                {
                    if (this.onGround && (double)this.targetXSpeed == 0.0)
                        this.animator.CrossFade("Dodge_standing", 0.05f, 0);
                    if (!this.reloading)
                        this.animator.Play("Dodge_standing", 2, 0.0f);
                    if (this.onGround)
                        this.playerAnimationsScript.footstep(3);
                    this.dodgeAudioSource.volume = UnityEngine.Random.Range(0.85f, 1f);
                    this.dodgeAudioSource.pitch = UnityEngine.Random.Range(0.85f, 1.3f);
                    this.dodgeAudioSource.Play();
                    this.animator.SetBool("Dodging", true);
                    this.timeSinceDodgeUsed = 0.0f;
                    this.bulletHitsSinceDodgeUsed = 0;
                    this.dodging = true;
                }
                this.kDodgeDown = true;
            }
        }
        else if (this.kDodgeDown)
            this.kDodgeDown = false;
        if (this.dodging)
        {
            this.dodgingCoolDown = 30f;
            this.dodgeAmount += 0.04f * this.timescale;
            this.dodgeSpinAngle = -this.dodgeAmount * 360f;
            this.sideStepAmount = this.root.Damp(1f, this.sideStepAmount, 0.5f);
            this.aimBlendTarget = 0.0f;
            this.lookAimBlendTarget = 0.3f;
            this.aimSpread2 = this.root.Damp(6f, this.aimSpread2, 0.15f);
            this.flippingTable = false;
            if (!this.startedRolling || !this.onGround)
            {
                this.reloadLayerWeight = 1f;
                if (!this.reloading && (double)this.crouchAmount < 0.600000023841858 && !this.animator.GetCurrentAnimatorStateInfo(2).IsName("Dodge_standing"))
                    this.animator.Play("Dodge_standing", 2, 0.0f);
            }
            if (this.root.cinematicShot)
                this.nrOfDodgeSpins = 0;
            if ((double)this.dodgeAmount > 1.0)
            {
                if (!this.kDodge || this.kCrouch || (this.startedRolling || this.nrOfDodgeSpins >= 2))
                {
                    this.nrOfDodgeSpins = 0;
                    this.dodgeAmount = 0.0f;
                    this.dodgeSpinAngle = 0.0f;
                    this.animator.SetBool("Dodging", false);
                    this.dodging = false;
                    if (this.onGround && !this.startedRolling)
                        this.animator.CrossFade("OnGround Blend Tree", 0.15f, 0);
                }
                else
                {
                    ++this.nrOfDodgeSpins;
                    this.dodgeAmount = 0.0f;
                    this.dodgeSpinAngle = 0.0f;
                }
            }
        }
        this.dodgingCoolDown = Mathf.Clamp(this.dodgingCoolDown - this.timescale, -30f, this.dodgingCoolDown);
        this.itemKickCoolDown = Mathf.Clamp(this.itemKickCoolDown - this.timescale, 0.0f, 1000f);
        this.timeSinceMeleeUsed += this.timescale;
        if (this.kPunch && !this.root.disableShooting && (!this.startedRolling && (double)this.punchTimer <= 15.0))
        {
            this.fightMode = true;
            this.animator.CrossFadeInFixedTime("MeleeKick", 0.05f, 0, 0.15f);
            this.punchTimer = 30f;
            this.kickWooshAudio.clip = this.kickWooshSound;
            this.kickWooshAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            this.kickWooshAudio.volume = UnityEngine.Random.Range(0.9f, 1f);
            this.kickWooshAudio.Play();
            this.root.rumble(0, 0.2f, 0.05f);
            this.root.rumble(1, 0.1f, 0.15f);
            this.meleeKickHit = false;
            this.meleeWallKickDetection = 0.0f;
            this.didShootWhileKicking = false;
            this.timeSinceMeleeUsed = 0.0f;
            this.root.doMeleeHint = false;
            this.nrOfDodgeSpins = 0;
            this.dodgeAmount = 0.0f;
            this.dodgeSpinAngle = 0.0f;
            this.animator.SetBool("Dodging", false);
            this.dodging = false;
        }
        if ((double)this.fightPoseBlendTimer <= 0.0 || this.kFire)
            this.fightMode = false;
        if (this.weapon == 0)
            this.fightMode = true;
        if ((double)this.punchTimer > 0.0)
        {
            this.inverseTransformPointMousePos = this.transform.InverseTransformPoint(this.mousePos);
            this.animator.SetFloat("KickAngle", -(Mathf.Atan2(-Mathf.Abs(this.inverseTransformPointMousePos.x), this.inverseTransformPointMousePos.y) * 57.29578f) / 160f);
            if (this.kFire)
                this.didShootWhileKicking = true;
            if (!this.didShootWhileKicking)
            {
                this.aimBlendTarget = 0.0f;
                this.lookAimBlendTarget = 0.0f;
            }
            this.kickFreezeTimer = 3f;
            if ((double)this.punchTimer > 10.0 && !this.meleeKickHit)
                this.meleeKickDetection();
        }
        if (this.root.disableShooting)
        {
            this.aimBlendTarget = 0.0f;
            if (this.kFire)
                this.aimSpread2 += 0.5f * this.timescale;
            if (this.kPunch)
                this.aimSpread2 += 4f * this.timescale;
            if (!this.disableShootingDoOnce)
            {
                this.mainCursorDisabledImage.gameObject.SetActive(true);
                this.mainCursorImage.color = Color.Lerp(this.mainCursorImageStartColour, Color.gray, 0.75f);
                this.disableShootingDoOnce = true;
            }
        }
        else if (this.disableShootingDoOnce)
        {
            this.mainCursorDisabledImage.gameObject.SetActive(false);
            this.mainCursorImage.color = this.mainCursorImageStartColour;
            this.disableShootingDoOnce = false;
        }
        this.animator.SetLayerWeight(2, this.reloadLayerWeight);
        this.fightPoseBlendTimer = Mathf.Clamp(this.fightPoseBlendTimer - this.timescale, 0.0f, this.fightPoseBlendTimer - this.timescale);
        this.fightPoseBlend = (double)this.fightPoseBlendTimer <= 0.0 ? this.root.Damp(0.0f, this.fightPoseBlend, 0.1f) : this.root.Damp(1f, this.fightPoseBlend, 0.3f);
        this.aimBlend = (double)this.aimBlendTarget != 0.0 || (double)this.aimBlend >= 0.00999999977648258 ? this.root.Damp(this.aimBlendTarget, this.aimBlend, 0.2f) : 0.0f;
        this.lookAimBlend = (double)this.lookAimBlendTarget != 0.0 || (double)this.lookAimBlend >= 0.00999999977648258 ? this.root.Damp(this.lookAimBlendTarget, this.lookAimBlend, 0.2f) : 0.0f;
        this.animator.SetLayerWeight(1, this.fightPoseBlend);
        if (this.kCrouch && !this.swinging && (this.onGround || this.aboutToHitGround) && (this.okToStand && (double)this.targetXSpeed == 0.0))
            this.animator.SetBool("Crouching", true);
        else
            this.animator.SetBool("Crouching", false);
        if ((double)this.justJumpedAnimationBoolTimer > 0.0)
            this.justJumpedAnimationBoolTimer -= this.timescale;
        this.animator.SetBool("JustJumped", (double)this.justJumpedAnimationBoolTimer > 0.0);
        if (this.aboutToHitGround)
        {
            if (!this.kCrouch)
                this.visualCrouchAmount = this.prevVisualCrouchAmount = 0.0f;
            else if ((double)this.crouchAmount > 0.100000001490116)
                this.visualCrouchAmount = this.prevVisualCrouchAmount = 1f;
            this.animator.SetFloat("CrouchAmount", this.visualCrouchAmount);
        }
        this.upperArmAnimationBlendTimer = Mathf.Clamp(this.upperArmAnimationBlendTimer - this.timescale, 0.0f, this.upperArmAnimationBlendTimer);
        this.upperArmAnimationBlend = this.root.Damp((double)this.upperArmAnimationBlendTimer > 0.0 || !this.onGround || (this.startedRolling || !(this.mousePos2 == this.mousePos)) || this.pedroBoss ? 0.0f : 1f, this.upperArmAnimationBlend, 0.2f);
        if ((double)this.dramaticEntranceTimer > 0.0)
            this.dramaticEntranceTimer -= this.timescale;
        if (this.onMotorcycle)
        {
            if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("MotorcycleBlend") && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("MotorcycleJump"))
                this.animator.Play("MotorcycleBlend", 0);
            this.motorcycleAim = this.root.Damp((float)(-(double)this.transform.InverseTransformPoint(this.mousePos).x * 0.200000002980232 + 0.5), this.motorcycleAim, 0.2f);
            this.animator.SetFloat("MotorcycleAim", this.motorcycleAim);
            this.armTwist = 1f - Mathf.Clamp01(this.motorcycleAim);
            this.transform.position = this.motorcyclePlayerPos.position;
            this.transform.rotation = this.motorcyclePlayerPos.rotation;
            this.onGround = true;
            this.ySpeed = this.xSpeed = this.targetXSpeed = 0.0f;
            this.faceRight = true;
            this.aimWithLeftArm = this.kSecondaryAim && (this.weapon == 2 || this.weapon == 4);
            this.fireLeftGun = false;
            this.twoHandedWeapon = false;
            this.lookAimBlend = 0.6f;
        }
        if ((double)this.speedModifier == 1.0)
            return;
        this.root.timescale = timescale;
        this.root.fixedTimescale = fixedTimescale;
        this.root.unscaledTimescale = unscaledTimescale;
    }

    public virtual void FixedUpdate()
    {
        float timescale = this.root.timescale;
        float fixedTimescale1 = this.root.fixedTimescale;
        float unscaledTimescale = this.root.unscaledTimescale;
        if ((double)this.speedModifier != 1.0)
        {
            this.root.timescale *= this.speedModifier;
            this.root.fixedTimescale *= this.speedModifier;
            this.root.unscaledTimescale *= this.speedModifier;
        }
        float fixedTimescale2 = this.root.fixedTimescale;
        if (this.onGround)
        {
            if (this.okToStand)
            {
                this.xSpeed = this.root.DampFixed(this.targetXSpeed, this.xSpeed, (double)this.kXDir != 0.0 ? 0.2f : 0.075f);
                if ((double)this.targetXSpeed == 0.0 && (double)this.xSpeed != 0.0)
                    this.xSpeed -= (float)((double)Mathf.Abs(this.xSpeed) / (double)this.xSpeed * 0.025000000372529) * fixedTimescale2;
            }
        }
        else if (!this.justWallJumped)
            this.xSpeed = this.kAction || this.swinging ? this.root.DampFixed(this.targetXSpeed, this.xSpeed, this.swinging || (double)Mathf.Abs(this.xSpeed) >= 4.0 || ((double)this.xSpeed <= 0.0 || (double)this.targetXSpeed <= 0.0) && ((double)this.xSpeed >= 0.0 || (double)this.targetXSpeed >= 0.0) && !this.skyfall ? 0.015f : 0.08f) : this.root.DampFixed(this.targetXSpeed, this.xSpeed, 0.05f);
        if (!this.onGround)
        {
            this.ySpeed -= 0.6f * fixedTimescale2;
            if ((double)this.rBody.velocity.y > -1.0 && (double)this.ySpeed < -20.0)
                this.ySpeed = -20f;
        }
        else
            this.ySpeed = (double)this.ySpeed >= 0.100000001490116 ? this.ySpeed * Mathf.Pow(0.95f, fixedTimescale2) : 0.0f;
        if (!this.disableFloatJump)
        {
            if (this.justJumped && (this.floatJump || this.justPulseJumped) && (double)this.extraJumpPower > 0.0)
            {
                this.ySpeed += this.extraJumpPower * fixedTimescale2;
                this.extraJumpPower -= 0.17f * fixedTimescale2;
            }
            if (!this.onGround && (this.floatJump || this.justWallJumped || this.justPulseJumped))
                this.ySpeed += 0.15f * fixedTimescale2;
        }
        float num1 = this.xSpeed * this.root.timescaleRaw * this.speedModifier + this.followSpeed.x;
        Vector3 velocity1 = this.rBody.velocity;
        double num2 = (double)(velocity1.x = num1);
        Vector3 vector3_1 = this.rBody.velocity = velocity1;
        if (this.skyfall)
        {
            this.ySpeed = 0.0f;
            float num3 = (float)-((double)this.transform.position.y - (double)this.skyfallYPos + (double)Mathf.Sin(Time.time * 2f) * 2.0 + (double)UnityEngine.Random.Range(-1.6f, 1.6f));
            Vector3 velocity2 = this.rBody.velocity;
            double num4 = (double)(velocity2.y = num3);
            Vector3 vector3_2 = this.rBody.velocity = velocity2;
        }
        else
        {
            float num3 = this.ySpeed * this.root.timescaleRaw * this.speedModifier + this.followSpeed.y;
            Vector3 velocity2 = this.rBody.velocity;
            double num4 = (double)(velocity2.y = num3);
            Vector3 vector3_2 = this.rBody.velocity = velocity2;
        }
        this.prevFixedPos = this.transform.position;
        if ((double)this.speedModifier == 1.0)
            return;
        this.root.timescale = timescale;
        this.root.fixedTimescale = fixedTimescale1;
        this.root.unscaledTimescale = unscaledTimescale;
        if (!this.aboutToHitGround || (double)this.rBody.velocity.y >= -20.0)
            return;
        float num5 = this.root.DampFixed(-20f, this.rBody.velocity.y, 0.5f);
        Vector3 velocity3 = this.rBody.velocity;
        double num6 = (double)(velocity3.y = num5);
        Vector3 vector3_3 = this.rBody.velocity = velocity3;
        if ((double)this.rBody.velocity.y < -30.0)
        {
            int num3 = -30;
            Vector3 velocity2 = this.rBody.velocity;
            double num4 = (double)(velocity2.y = (float)num3);
            Vector3 vector3_2 = this.rBody.velocity = velocity2;
        }
        if ((double)this.ySpeed >= -15.0)
            return;
        this.ySpeed = -15f;
    }

    public virtual void LateUpdate()
    {
        if (this.root.doCheckpointSave)
            this.saveState();
        if (this.root.doCheckpointLoad)
            this.loadState();
        float timescale = this.root.timescale;
        float fixedTimescale = this.root.fixedTimescale;
        float unscaledTimescale = this.root.unscaledTimescale;
        if ((double)this.speedModifier != 1.0)
        {
            this.root.timescale *= this.speedModifier;
            this.root.fixedTimescale *= this.speedModifier;
            this.root.unscaledTimescale *= this.speedModifier;
        }
        this.dualAimMode = false;
        this.tempShoulderRBulletPointR2DistanceMultiplier = 0.0f;
        this.invertedNormalizedTopDuckAmount = 1f - this.topDuckAmount / 90f;
        this.kickBack = this.weapon != 7 || !this.kSecondaryAim ? this.fireDelay : 0.0f;


        if (root.dead)
            return;

        if (this.faceRight)
        {
            if (this.aimWithLeftArm && this.mousePos2 != this.mousePos && ((double)this.mousePos2.x < (double)this.transform.position.x && !this.fightMode && ((double)this.punchTimer <= 0.0 && !this.onMotorcycle) && !this.hipSwing))
            {
                this.dualAimMode = true;
                if (!this.startedRolling)
                {
                    this.lowerBackFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.lowerBack.rotation, this.playerGraphics.rotation * Quaternion.Euler(0.0f, 0.0f, -90f), this.aimBlend * this.invertedNormalizedTopDuckAmount), this.lowerBackFakeRot, 0.25f);
                    this.lowerBack.rotation = this.lowerBackFakeRot;
                    this.upperBack.rotation = Quaternion.Slerp(this.upperBack.rotation, this.lowerBack.rotation, this.aimBlend);
                    this.upperBack.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount, 0.0f, 45f), 0.0f);
                    this.lowerBack.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount, 0.0f, 65f), 0.0f);
                    this.neck.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount, 0.0f, 30f), 0.0f);
                }
                this.shoulderR.localRotation = Quaternion.Slerp(this.shoulderR.localRotation, Quaternion.Euler(0.0f, -90f, -90f), this.aimBlend);
                this.shoulderL.localRotation = Quaternion.Slerp(this.shoulderL.localRotation, Quaternion.Euler(0.0f, -90f, 90f), this.aimBlend);
                this.dualWieldFrontTurnSmoothing = Mathf.Clamp(this.dualWieldFrontTurnSmoothing + this.timescale * 0.3f, -1f, 1f);
                this.dualWieldFrontTurnSmoothing2 = Mathf.Clamp(this.dualWieldFrontTurnSmoothing, 0.0f, 1f);
                this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos2.x, this.mousePos2.y, this.upperArmR.position.z) - this.upperArmR.position, Vector3.forward) * Quaternion.Euler(0.0f, (float)(105.0 - (this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 0.5 : 0.0)), 5f), this.aimBlend);
                this.upperArmR.localRotation *= Quaternion.Euler(0.0f, 0.0f, (float)(90.0 * ((double)Mathf.Abs(this.upperArmR.localRotation.eulerAngles.y - 180f) / 180.0)) * this.dualWieldFrontTurnSmoothing2);
                this.upperArmRTempVarForHandRotation = this.upperArmR.localRotation.eulerAngles.y;
                float num1 = Mathf.Clamp(this.upperArmR.localRotation.eulerAngles.y, 90f, 270f);
                Quaternion localRotation1 = this.upperArmR.localRotation;
                Vector3 eulerAngles1 = localRotation1.eulerAngles;
                double num2 = (double)(eulerAngles1.y = num1);
                Vector3 vector3_1 = localRotation1.eulerAngles = eulerAngles1;
                Quaternion quaternion1 = this.upperArmR.localRotation = localRotation1;
                this.armROffset = !Physics.Raycast(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), -this.upperArmR.right, out this.armRayHit, 2f, (int)this.layerMaskIncEnemies) ? this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset, 0.2f) : Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.armRayHit.point) / 2.0) * 180.0 + (double)this.topDuckAmount * 0.699999988079071) * this.aimBlend, 0.0f);
                this.upperArmR.localRotation *= this.armROffset;
                this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos2.x, this.mousePos2.y, this.upperArmR.position.z) - this.lowerArmR.position, Vector3.forward) * Quaternion.Euler(0.0f, (float)(90.0 - (this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 1.0 : 0.0)), 0.0f), this.aimBlend);
                this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.upperArmL.position, Vector3.forward) * Quaternion.Euler(180f, (float)(75.0 + (!this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 0.5 : 0.0)), 5f), this.aimBlend);
                this.upperArmL.localRotation *= Quaternion.Euler(0.0f, 0.0f, (float)(-90.0 * ((double)Mathf.Abs(this.upperArmL.localRotation.eulerAngles.y - 180f) / 180.0)) * this.dualWieldFrontTurnSmoothing2);
                this.upperArmLTempVarForHandRotation = this.upperArmL.localRotation.eulerAngles.y;
                float num3 = Mathf.Clamp(this.upperArmL.localRotation.eulerAngles.y, 90f, 270f);
                Quaternion localRotation2 = this.upperArmL.localRotation;
                Vector3 eulerAngles2 = localRotation2.eulerAngles;
                double num4 = (double)(eulerAngles2.y = num3);
                Vector3 vector3_2 = localRotation2.eulerAngles = eulerAngles2;
                Quaternion quaternion2 = this.upperArmL.localRotation = localRotation2;
                this.armLOffset = !Physics.Raycast(this.shoulderL.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), -this.upperArmL.right, out this.armRayHit, 2f, (int)this.layerMaskIncEnemies) ? this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armLOffset, 0.2f) : Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderL.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.armRayHit.point) / 2.0) * 180.0 + (double)this.topDuckAmount * 0.699999988079071) * this.aimBlend, 0.0f);
                this.upperArmL.localRotation *= this.armLOffset;
                this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.lowerArmL.position, Vector3.forward) * Quaternion.Euler(180f, (float)(90.0 + (!this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 1.0 : 0.0)), 0.0f), this.aimBlend);
                this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, this.dodgeSpinAngle + 180f, 0.0f), this.playerGraphics.localRotation, 0.25f);
                this.sideStepAmount = this.root.Damp(1f, this.sideStepAmount, 0.25f);
            }
            else
            {
                if (!this.startedRolling)
                {
                    if (!this.onMotorcycle)
                    {
                        this.lowerBackTargetLookRotation = Quaternion.LookRotation(this.mousePos - this.lowerBack.position, Vector3.back);
                        this.lowerBackFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.lowerBack.rotation, !this.onGround ? this.lowerBackTargetLookRotation : Quaternion.Euler(this.lowerBackTargetLookRotation.eulerAngles.x + 15f, this.lowerBack.rotation.eulerAngles.y, this.lowerBackTargetLookRotation.eulerAngles.z), (float)(((double)this.fightPoseBlendTimer <= 0.0 ? (double)this.aimBlend : (double)this.lookAimBlend) * 0.300000011920929) * this.invertedNormalizedTopDuckAmount), this.lowerBackFakeRot, 0.25f);
                        this.lowerBack.rotation = this.lowerBackFakeRot;
                    }
                    this.upperBack.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount + this.runBend * 3f, 0.0f, 45f), 0.0f);
                    this.lowerBack.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount + this.runBend * 8f, 0.0f, 65f), 0.0f);
                    this.neck.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount + this.runBend * -5f, 0.0f, 30f), 0.0f);
                }
                this.armTwist = this.root.Damp(1f, this.armTwist, 0.3f);
                this.twoHandedWeaponDirectionalVector = new Vector3(0.0f, 0.0f, 2f * this.armTwist - 1f);
                if (!this.swinging && this.twoHandedWeapon)
                {
                    this.shoulderR.localRotation = Quaternion.Slerp(this.shoulderR.localRotation, Quaternion.Euler(346.5f, 280.8f, 310f), this.aimBlend);
                    this.shoulderL.localRotation = Quaternion.Slerp(this.shoulderL.localRotation, Quaternion.Euler(13.5f, 280.8f, 110f), this.aimBlend);
                    this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.upperArmR.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(180f, 45f + this.kickBack * 0.5f, 290f), this.aimBlend);
                    this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.lowerArmR.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(180f, 90f + this.kickBack * 1f, 45f), this.aimBlend);
                    this.handR.rotation = Quaternion.Slerp(this.handR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.handR.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(235f, 92f + this.kickBack * 1f, 350f), this.aimBlend);
                    if ((double)this.shootFromCoverLayerWeight <= 0.0)
                    {
                        this.tempShoulderRBulletPointR2Distance = Vector3.Distance(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.bulletPointR2.position);
                        if (Physics.Linecast(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.bulletPointR2.position, out this.armRayHit, (int)this.layerMaskIncEnemiesMinusLvlColIgnoreBullet))
                        {
                            this.tempShoulderRBulletPointR2DistanceMultiplier = 1f - this.armRayHit.distance / this.tempShoulderRBulletPointR2Distance;
                            this.armROffset = Quaternion.Euler(0.0f, 0.0f, 35f * this.tempShoulderRBulletPointR2DistanceMultiplier * this.aimBlend);
                            this.armROffset2 = Quaternion.Euler(0.0f, 160f * this.tempShoulderRBulletPointR2DistanceMultiplier * this.aimBlend, 0.0f);
                            this.armROffset3 = Quaternion.Euler(0.0f, Mathf.Clamp(180f * this.tempShoulderRBulletPointR2DistanceMultiplier * this.aimBlend, 0.0f, 62f), 45f * this.tempShoulderRBulletPointR2DistanceMultiplier * this.aimBlend);
                        }
                        else
                        {
                            this.armROffset = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset, 0.2f);
                            this.armROffset2 = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset2, 0.2f);
                            this.armROffset3 = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset2, 0.2f);
                        }
                        this.lowerArmR.localRotation *= this.armROffset;
                        this.handR.localRotation *= this.armROffset3;
                    }
                    this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.upperArmL.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(this.weapon == 6 || this.weapon == 7 ? new Vector3(177f, (float)(64.1600036621094 + (double)this.kickBack * 0.5), 340.3f) : new Vector3(177f, (float)(75.9000015258789 + (double)this.kickBack * 0.5), 340.3f)), this.aimBlend);
                    this.upperArmL.localRotation *= this.armROffset2;
                    this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(this.grabPoint.position - this.lowerArmL.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(this.weapon == 6 || this.weapon == 7 ? new Vector3(180f, (float)(93.3000030517578 + (double)this.kickBack * 1.0), 0.0f) : new Vector3(180f, (float)(87.4000015258789 + (double)this.kickBack * 1.0), 355.84f)), this.aimBlend);
                    this.handL.localRotation = Quaternion.Slerp(this.handL.localRotation, Quaternion.Euler(192.1f, 0.0f, 39f), this.aimBlend);
                }
                else
                {
                    this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.upperArmR.position, Vector3.forward) * Quaternion.Euler(this.armTwist * 180f, (float)(105.0 - (double)this.armTwist * 30.0 + (this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 0.5 : 0.0)), 355f), this.aimBlend * (float)(1.0 - (double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend));
                    if (this.aimWithLeftArm)
                        this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos2.x, this.mousePos2.y, this.upperArmL.position.z) - this.upperArmL.position, Vector3.forward) * Quaternion.Euler((float)((!this.onMotorcycle ? (double)this.armTwist : 1.0) * 180.0), (float)(105.0 - (!this.onMotorcycle ? (double)this.armTwist : 1.0) * 30.0 + (!this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 0.5 : 0.0)), 15f), this.aimBlend * (float)(1.0 - (double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend));
                    if ((double)this.shootFromCoverLayerWeight <= 0.0)
                    {
                        if (this.onGround && (double)this.lowerArmR.position.x > (double)this.upperArmR.position.x || !this.onGround)
                        {
                            this.armROffset = !Physics.Raycast(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), -this.upperArmR.right, out this.armRayHit, 2f, (int)this.layerMaskIncEnemies) ? this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset, 0.2f) : Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.armRayHit.point) / 2.0) * 140.0 + (double)this.topDuckAmount * 0.699999988079071) * this.aimBlend, (2f - this.armRayHit.distance) * -30f);
                            this.upperArmR.localRotation *= this.armROffset;
                        }
                        if ((this.onGround && (double)this.lowerArmL.position.x > (double)this.upperArmL.position.x || !this.onGround) && this.aimWithLeftArm)
                        {
                            this.armLOffset = !Physics.Raycast(this.shoulderL.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), -this.upperArmL.right, out this.armRayHit, 2f, (int)this.layerMaskIncEnemies) ? this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armLOffset, 0.2f) : Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderL.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.armRayHit.point) / 2.0) * 140.0 + (double)this.topDuckAmount * 0.699999988079071) * this.aimBlend, (2f - this.armRayHit.distance) * 30f);
                            this.upperArmL.localRotation *= this.armLOffset;
                        }
                    }
                    this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.lowerArmR.position, Vector3.forward) * Quaternion.Euler(this.armTwist * 180f, (float)(90.0 + (this.fireLeftGun || this.kSecondaryAim || !this.aimWithLeftArm ? (double)this.kickBack * 1.0 : 0.0)), 0.0f), this.aimBlend * (float)(1.0 - 0.300000011920929 * ((double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend)));
                    if (this.aimWithLeftArm)
                        this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos2.x, this.mousePos2.y, this.upperArmL.position.z) - this.lowerArmL.position, Vector3.forward) * Quaternion.Euler((float)((!this.onMotorcycle ? (double)this.armTwist : 1.0) * 180.0), (float)(90.0 + (!this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 1.0 : 0.0)), 0.0f), this.aimBlend * (float)(1.0 - 0.600000023841858 * ((double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend)));
                }
                this.dualWieldFrontTurnSmoothing = -1f;
                if (this.onGround)
                {
                    this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, this.dodgeSpinAngle + (!this.pushedAgainstWall || !this.kCrouch || (!this.onGround || (double)this.punchTimer > 5.0) ? 90f : 180f), 0.0f), this.playerGraphics.localRotation, 0.25f);
                }
                else
                {
                    this.inverseTransformPointMousePos = this.transform.InverseTransformPoint(this.mousePos);
                    this.mouseAngle = Mathf.Atan2(this.inverseTransformPointMousePos.x, this.inverseTransformPointMousePos.y) * 57.29578f;
                    this.bodyTargetAngle = (double)this.mouseAngle >= 90.0 ? this.mouseAngle : 180f - this.mouseAngle;
                    this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, this.dodgeSpinAngle + this.bodyTargetAngle, 0.0f), this.playerGraphics.localRotation, 0.25f);
                }
                this.sideStepAmount = this.root.Damp(0.0f, this.sideStepAmount, 0.25f);
            }
            this.headFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.head.rotation, Quaternion.LookRotation(new Vector3(Mathf.Clamp(this.mousePos.x, this.head.position.x + 0.1f, this.mousePos.x), this.mousePos.y, this.head.position.z) - this.head.position, Vector3.back), (float)((double)this.lookAimBlend * ((double)this.transform.InverseTransformPoint(this.mousePos).x <= 0.0 ? 0.0 : 0.5) * (1.0 - (double)this.topDuckAmount))), this.headFakeRot, 0.25f);
            this.head.rotation = this.headFakeRot;
        }
        else
        {
            if (this.aimWithLeftArm && this.mousePos2 != this.mousePos && ((double)this.mousePos2.x > (double)this.transform.position.x && !this.fightMode && ((double)this.punchTimer <= 0.0 && !this.hipSwing)))
            {
                this.dualAimMode = true;
                if (!this.startedRolling)
                {
                    this.lowerBackFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.lowerBack.rotation, this.playerGraphics.rotation * Quaternion.Euler(0.0f, 0.0f, -90f), this.aimBlend * this.invertedNormalizedTopDuckAmount), this.lowerBackFakeRot, 0.25f);
                    this.lowerBack.rotation = this.lowerBackFakeRot;
                    this.upperBack.rotation = Quaternion.Slerp(this.upperBack.rotation, this.lowerBack.rotation, this.aimBlend);
                    this.upperBack.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount, 0.0f, 45f), 0.0f);
                    this.lowerBack.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount, 0.0f, 65f), 0.0f);
                    this.neck.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount, 0.0f, 30f), 0.0f);
                }
                this.shoulderR.localRotation = Quaternion.Slerp(this.shoulderR.localRotation, Quaternion.Euler(0.0f, -90f, -90f), this.aimBlend);
                this.shoulderL.localRotation = Quaternion.Slerp(this.shoulderL.localRotation, Quaternion.Euler(0.0f, -90f, 90f), this.aimBlend);
                this.dualWieldFrontTurnSmoothing = Mathf.Clamp(this.dualWieldFrontTurnSmoothing + this.timescale * 0.3f, -1f, 1f);
                this.dualWieldFrontTurnSmoothing2 = Mathf.Clamp(this.dualWieldFrontTurnSmoothing, 0.0f, 1f);
                this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.upperArmR.position, Vector3.forward) * Quaternion.Euler(0.0f, (float)(105.0 - (this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 0.5 : 0.0)), 5f), this.aimBlend);
                this.upperArmR.localRotation *= Quaternion.Euler(0.0f, 0.0f, (float)(90.0 * ((double)Mathf.Abs(this.upperArmR.localRotation.eulerAngles.y - 180f) / 180.0)) * this.dualWieldFrontTurnSmoothing2);
                this.upperArmRTempVarForHandRotation = this.upperArmR.localRotation.eulerAngles.y;
                float num1 = Mathf.Clamp(this.upperArmR.localRotation.eulerAngles.y, 90f, 270f);
                Quaternion localRotation1 = this.upperArmR.localRotation;
                Vector3 eulerAngles1 = localRotation1.eulerAngles;
                double num2 = (double)(eulerAngles1.y = num1);
                Vector3 vector3_1 = localRotation1.eulerAngles = eulerAngles1;
                Quaternion quaternion1 = this.upperArmR.localRotation = localRotation1;
                this.armROffset = !Physics.Raycast(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), -this.upperArmR.right, out this.armRayHit, 2f, (int)this.layerMaskIncEnemies) ? this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset, 0.2f) : Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.armRayHit.point) / 2.0) * 180.0 + (double)this.topDuckAmount * 0.699999988079071) * this.aimBlend, 0.0f);
                this.upperArmR.localRotation *= this.armROffset;
                this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.lowerArmR.position, Vector3.forward) * Quaternion.Euler(0.0f, (float)(90.0 - (this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 1.0 : 0.0)), 0.0f), this.aimBlend);
                this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos2.x, this.mousePos2.y, this.upperArmL.position.z) - this.upperArmL.position, Vector3.forward) * Quaternion.Euler(180f, (float)(75.0 + (!this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 0.5 : 0.0)), 5f), this.aimBlend);
                this.upperArmL.localRotation *= Quaternion.Euler(0.0f, 0.0f, (float)(-90.0 * ((double)Mathf.Abs(this.upperArmL.localRotation.eulerAngles.y - 180f) / 180.0)) * this.dualWieldFrontTurnSmoothing2);
                this.upperArmLTempVarForHandRotation = this.upperArmL.localRotation.eulerAngles.y;
                float num3 = Mathf.Clamp(this.upperArmL.localRotation.eulerAngles.y, 90f, 270f);
                Quaternion localRotation2 = this.upperArmL.localRotation;
                Vector3 eulerAngles2 = localRotation2.eulerAngles;
                double num4 = (double)(eulerAngles2.y = num3);
                Vector3 vector3_2 = localRotation2.eulerAngles = eulerAngles2;
                Quaternion quaternion2 = this.upperArmL.localRotation = localRotation2;
                this.armLOffset = !Physics.Raycast(this.shoulderL.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), -this.upperArmL.right, out this.armRayHit, 2f, (int)this.layerMaskIncEnemies) ? this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armLOffset, 0.2f) : Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderL.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.armRayHit.point) / 2.0) * 180.0 + (double)this.topDuckAmount * 0.699999988079071) * this.aimBlend, 0.0f);
                this.upperArmL.localRotation *= this.armLOffset;
                this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos2.x, this.mousePos2.y, this.upperArmL.position.z) - this.lowerArmL.position, Vector3.forward) * Quaternion.Euler(180f, (float)(90.0 + (!this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 1.0 : 0.0)), 0.0f), this.aimBlend);
                this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, this.dodgeSpinAngle + 180f, 0.0f), this.playerGraphics.localRotation, 0.25f);
                this.sideStepAmount = this.root.Damp(1f, this.sideStepAmount, 0.25f);
            }
            else
            {
                if (!this.startedRolling)
                {
                    this.lowerBackTargetLookRotation = Quaternion.LookRotation(this.mousePos - this.lowerBack.position, Vector3.forward);
                    this.lowerBackFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.lowerBack.rotation, !this.onGround ? this.lowerBackTargetLookRotation : Quaternion.Euler(this.lowerBackTargetLookRotation.eulerAngles.x + 15f, this.lowerBack.rotation.eulerAngles.y, this.lowerBackTargetLookRotation.eulerAngles.z), (float)(((double)this.fightPoseBlendTimer <= 0.0 ? (double)this.aimBlend : (double)this.lookAimBlend) * 0.300000011920929) * this.invertedNormalizedTopDuckAmount), this.lowerBackFakeRot, 0.25f);
                    this.lowerBack.rotation = this.lowerBackFakeRot;
                    this.upperBack.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount + this.runBend * 3f, 0.0f, 45f), 0.0f);
                    this.lowerBack.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount + this.runBend * 8f, 0.0f, 65f), 0.0f);
                    this.neck.localRotation *= Quaternion.Euler(0.0f, Mathf.Clamp(this.topDuckAmount + this.runBend * -5f, 0.0f, 30f), 0.0f);
                }
                this.armTwist = this.root.Damp(0.0f, this.armTwist, 0.3f);
                this.twoHandedWeaponDirectionalVector = new Vector3(0.0f, 0.0f, 2f * this.armTwist - 1f);
                if (!this.swinging && this.twoHandedWeapon)
                {
                    this.shoulderR.localRotation = Quaternion.Slerp(this.shoulderR.localRotation, Quaternion.Euler(346.5f, 280.8f, 310f), this.aimBlend);
                    this.shoulderL.localRotation = Quaternion.Slerp(this.shoulderL.localRotation, Quaternion.Euler(13.5f, 280.8f, 110f), this.aimBlend);
                    this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.upperArmR.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(180f, 45f + this.kickBack * 0.5f, 290f), this.aimBlend);
                    this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.lowerArmR.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(180f, 90f + this.kickBack * 1f, 45f), this.aimBlend);
                    this.handR.rotation = Quaternion.Slerp(this.handR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmR.position.z) - this.handR.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(235f, 92f + this.kickBack * 1f, 350f), this.aimBlend);
                    if ((double)this.shootFromCoverLayerWeight <= 0.0)
                    {
                        this.tempShoulderRBulletPointR2Distance = Vector3.Distance(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.bulletPointR2.position);
                        if (Physics.Linecast(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.bulletPointR2.position, out this.armRayHit, (int)this.layerMaskIncEnemiesMinusLvlColIgnoreBullet))
                        {
                            this.tempShoulderRBulletPointR2DistanceMultiplier = 1f - this.armRayHit.distance / this.tempShoulderRBulletPointR2Distance;
                            this.armROffset = Quaternion.Euler(0.0f, 0.0f, 35f * this.tempShoulderRBulletPointR2DistanceMultiplier * this.aimBlend);
                            this.armROffset2 = Quaternion.Euler(0.0f, 160f * this.tempShoulderRBulletPointR2DistanceMultiplier * this.aimBlend, 0.0f);
                            this.armROffset3 = Quaternion.Euler(0.0f, Mathf.Clamp(180f * this.tempShoulderRBulletPointR2DistanceMultiplier * this.aimBlend, 0.0f, 62f), 45f * this.tempShoulderRBulletPointR2DistanceMultiplier * this.aimBlend);
                        }
                        else
                        {
                            this.armROffset = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset, 0.2f);
                            this.armROffset2 = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset2, 0.2f);
                            this.armROffset3 = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset2, 0.2f);
                        }
                        this.lowerArmR.localRotation *= this.armROffset;
                        this.handR.localRotation *= this.armROffset3;
                    }
                    this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.upperArmL.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(this.weapon == 6 || this.weapon == 7 ? new Vector3(177f, (float)(64.1600036621094 + (double)this.kickBack * 0.5), 340.3f) : new Vector3(177f, (float)(75.9000015258789 + (double)this.kickBack * 0.5), 340.3f)), this.aimBlend);
                    this.upperArmL.localRotation *= this.armROffset2;
                    this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(this.grabPoint.position - this.lowerArmL.position, this.twoHandedWeaponDirectionalVector) * Quaternion.Euler(this.weapon == 6 || this.weapon == 7 ? new Vector3(180f, (float)(93.3000030517578 + (double)this.kickBack * 1.0), 0.0f) : new Vector3(180f, (float)(87.4000015258789 + (double)this.kickBack * 1.0), 355.84f)), this.aimBlend);
                    this.handL.localRotation = Quaternion.Slerp(this.handL.localRotation, Quaternion.Euler(192.1f, 0.0f, 39f), this.aimBlend);
                }
                else
                {
                    this.upperArmR.rotation = Quaternion.Slerp(this.upperArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos2.x, this.mousePos2.y, this.upperArmR.position.z) - this.upperArmR.position, Vector3.forward) * Quaternion.Euler(this.armTwist * 180f, (float)(105.0 - (double)this.armTwist * 30.0 + (this.fireLeftGun || this.kSecondaryAim ? -(double)this.kickBack * 0.5 : 0.0)), 355f), this.aimBlend * (float)(1.0 - (double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend));
                    if (this.aimWithLeftArm)
                        this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.upperArmL.position, Vector3.forward) * Quaternion.Euler(this.armTwist * 180f, (float)(105.0 - (double)this.armTwist * 30.0 + (!this.fireLeftGun || this.kSecondaryAim ? -(double)this.kickBack * 0.5 : 0.0)), 15f), this.aimBlend * (float)(1.0 - (double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend));
                    if ((double)this.shootFromCoverLayerWeight <= 0.0)
                    {
                        if (this.onGround && (double)this.lowerArmR.position.x < (double)this.upperArmR.position.x || !this.onGround)
                        {
                            this.armROffset = !Physics.Raycast(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), -this.upperArmR.right, out this.armRayHit, 2f, (int)this.layerMaskIncEnemies) ? this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armROffset, 0.2f) : Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderR.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.armRayHit.point) / 2.0) * 140.0 + (double)this.topDuckAmount * 0.699999988079071) * this.aimBlend, (2f - this.armRayHit.distance) * -30f);
                            this.upperArmR.localRotation *= this.armROffset;
                        }
                        if ((this.onGround && (double)this.lowerArmL.position.x < (double)this.upperArmL.position.x || !this.onGround) && this.aimWithLeftArm)
                        {
                            this.armLOffset = !Physics.Raycast(this.shoulderL.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), -this.upperArmL.right, out this.armRayHit, 2f, (int)this.layerMaskIncEnemies) ? this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, 0.0f), this.armLOffset, 0.2f) : Quaternion.Euler(0.0f, (float)((1.0 - (double)Vector3.Distance(this.shoulderL.position - new Vector3(0.0f, this.topDuckAmount * 0.01f, 0.0f), this.armRayHit.point) / 2.0) * 140.0 + (double)this.topDuckAmount * 0.699999988079071) * this.aimBlend, (2f - this.armRayHit.distance) * 30f);
                            this.upperArmL.localRotation *= this.armLOffset;
                        }
                    }
                    this.lowerArmR.rotation = Quaternion.Slerp(this.lowerArmR.rotation, Quaternion.LookRotation(new Vector3(this.mousePos2.x, this.mousePos2.y, this.upperArmR.position.z) - this.lowerArmR.position, Vector3.forward) * Quaternion.Euler(this.armTwist * 180f, (float)(90.0 + (this.fireLeftGun || this.kSecondaryAim || !this.aimWithLeftArm ? -(double)this.kickBack * 1.0 : 0.0)), 0.0f), this.aimBlend * (float)(1.0 - 0.300000011920929 * ((double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend)));
                    if (this.aimWithLeftArm)
                        this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(new Vector3(this.mousePos.x, this.mousePos.y, this.upperArmL.position.z) - this.lowerArmL.position, Vector3.forward) * Quaternion.Euler(this.armTwist * 180f, (float)(90.0 + (!this.fireLeftGun || this.kSecondaryAim ? -(double)this.kickBack * 1.0 : 0.0)), 0.0f), this.aimBlend * (float)(1.0 - 0.600000023841858 * ((double)Mathf.Abs(this.xSpeed / 5f) * (double)this.upperArmAnimationBlend)));
                }
                this.dualWieldFrontTurnSmoothing = -1f;
                if (this.onGround)
                {
                    this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, this.dodgeSpinAngle + (!this.pushedAgainstWall || !this.kCrouch || (!this.onGround || (double)this.punchTimer > 5.0) ? 270f : 180f), 0.0f), this.playerGraphics.localRotation, 0.25f);
                }
                else
                {
                    this.inverseTransformPointMousePos = this.transform.InverseTransformPoint(this.mousePos);
                    this.mouseAngle = Mathf.Atan2(this.inverseTransformPointMousePos.x, this.inverseTransformPointMousePos.y) * 57.29578f;
                    this.bodyTargetAngle = (double)this.mouseAngle <= -90.0 ? this.mouseAngle : 180f - this.mouseAngle;
                    this.playerGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(0.0f, this.dodgeSpinAngle + this.bodyTargetAngle, 0.0f), this.playerGraphics.localRotation, 0.25f);
                }
                this.sideStepAmount = this.root.Damp(0.0f, this.sideStepAmount, 0.25f);
            }
            this.headFakeRot = this.root.DampSlerp(Quaternion.Slerp(this.head.rotation, Quaternion.LookRotation(new Vector3(Mathf.Clamp(this.mousePos.x, this.mousePos.x, this.head.position.x - 0.1f), this.mousePos.y, this.head.position.z) - this.head.position, Vector3.forward), (float)((double)this.lookAimBlend * ((double)this.transform.InverseTransformPoint(this.mousePos).x >= 0.0 ? 0.0 : 0.5) * (1.0 - (double)this.topDuckAmount))), this.headFakeRot, 0.25f);
            this.head.rotation = this.headFakeRot;
        }
        if (!this.twoHandedWeapon || this.twoHandedWeapon && this.swinging)
        {
            this.handR.localRotation = Quaternion.Slerp(this.handR.localRotation, Quaternion.Euler(!this.dualAimMode ? 45f : (float)(90.0 - (double)Mathf.Abs(180f - this.upperArmRTempVarForHandRotation) / 180.0 * ((double)this.upperArmRTempVarForHandRotation >= 180.0 ? -180.0 : 180.0)), 0.0f, (float)((this.fireLeftGun || this.kSecondaryAim || !this.aimWithLeftArm ? (double)Mathf.Clamp(this.kickBack, 0.0f, 8f) * 4.0 : 0.0) - 8.0)), this.aimBlend);
            if (this.aimWithLeftArm)
                this.handL.localRotation = Quaternion.Slerp(this.handL.localRotation, Quaternion.Euler(!this.dualAimMode ? -45f : (float)((double)Mathf.Abs(180f - this.upperArmLTempVarForHandRotation) / 180.0 * ((double)this.upperArmLTempVarForHandRotation >= 180.0 ? -180.0 : 180.0) - 90.0), 0.0f, (float)(8.0 - (!this.fireLeftGun || this.kSecondaryAim ? (double)this.kickBack * 4.0 : 0.0))), this.aimBlend);
        }
        if ((double)this.shootFromCoverLayerWeight > 0.0)
        {
            this.handR.rotation = Quaternion.LookRotation(Vector3.Normalize(this.mousePos - this.handR.position), Vector3.up);
            this.handR.rotation *= Quaternion.Euler(90f, 90f, -10f);
            if (this.aimWithLeftArm || this.twoHandedWeapon)
            {
                this.handL.rotation = Quaternion.LookRotation(Vector3.Normalize(this.mousePos - this.handL.position), Vector3.up);
                this.handL.rotation *= Quaternion.Euler(90f, 90f, -10f);
            }
        }
        if (this.swinging)
        {
            this.justWallJumped = false;
            this.justJumpedFromSkateboard = false;
            this.wallJumpTimer = 0.0f;
            if ((double)this.framesInSwingMode == 0.0 && !this.kSecondaryAim)
                this.timeSinceKSecondaryAimForHipSwingPurpose = 999f;
            this.framesInSwingMode += this.timescale;
            StringScript component = (StringScript)this.swingTransform.GetComponent(typeof(StringScript));
            int num1 = this.weapon == 2 ? 1 : 0;
            if (num1 == 0)
                num1 = this.weapon == 4 ? 1 : 0;
            bool flag = num1 != 0;
            if (flag)
            {
                if (!this.hipSwing)
                {
                    if (this.kSecondaryAim || (double)this.timeSinceKSecondaryAimForHipSwingPurpose < 300.0)
                    {
                        float num2 = Vector2.Distance((Vector2)this.transform.position, (Vector2)this.swingTransform.position);
                        if ((UnityEngine.Object)component != (UnityEngine.Object)null && (double)num2 < (double)component.ropeLength)
                        {
                            this.grabLength = num2;
                            this.hipSwing = true;
                        }
                        if ((double)this.transform.up.y < -0.800000011920929)
                            this.hipSwing = true;
                    }
                }
                else if (!this.kSecondaryAim && !this.kFire && ((double)this.timeSinceShotFired > 60.0 && !this.dualAimMode) && (double)this.timeSinceKSecondaryAimForHipSwingPurpose > 300.0)
                    this.hipSwing = false;
            }
            else
                this.hipSwing = false;
            if (!this.hipSwing)
            {
                this.fireLeftGun = true;
                this.upperArmL.rotation = Quaternion.Slerp(this.upperArmL.rotation, Quaternion.LookRotation(new Vector3(this.swingTransform.position.x, this.swingTransform.position.y, this.upperArmL.position.z) - this.upperArmL.position, Vector3.back) * Quaternion.Euler(180f, 75f, 15f), 1f);
                this.lowerArmL.rotation = Quaternion.Slerp(this.lowerArmL.rotation, Quaternion.LookRotation(new Vector3(this.swingTransform.position.x, this.swingTransform.position.y, this.upperArmL.position.z) - this.lowerArmL.position, Vector3.back) * Quaternion.Euler(180f, 90f, 0.0f), 1f);
            }
            if (!this.onGround)
            {
                this.faceRightInvertMultiplier = !this.faceRight ? -1 : 1;
                if (!this.hipSwing)
                {
                    this.upperLegR.localRotation *= Quaternion.Euler(0.0f, (float)(-(double)this.xSpeed * 7.0) * (float)this.faceRightInvertMultiplier, 0.0f);
                    this.lowerLegR.localRotation *= Quaternion.Euler(0.0f, this.xSpeed * 1f * (float)this.faceRightInvertMultiplier, 0.0f);
                    this.upperLegL.localRotation *= Quaternion.Euler(0.0f, (float)(-(double)this.xSpeed * 4.0) * (float)this.faceRightInvertMultiplier, 0.0f);
                    this.lowerLegL.localRotation *= Quaternion.Euler(0.0f, (float)(-(double)this.xSpeed * 7.0) * (float)this.faceRightInvertMultiplier, 0.0f);
                }
                else
                {
                    this.upperLegR.localRotation *= Quaternion.Euler(0.0f, 60f, 0.0f);
                    this.lowerLegR.localRotation *= Quaternion.Euler(0.0f, -40f, 50f);
                    this.upperLegL.localRotation *= Quaternion.Euler(-40f, 30f, -20f);
                    this.lowerLegL.localRotation *= Quaternion.Euler(0.0f, 30f, 25f);
                }
            }
            if (this.kCrouch && !this.onGround && (double)this.swingOnGroundContractTimer <= 0.0)
                this.grabLength = Mathf.Clamp(this.grabLength + 0.12f * this.timescale, 0.0f, this.ropeLength - 0.1f);
            if (this.onGround)
                this.swingOnGroundContractTimer = 10f;
            if ((double)this.swingOnGroundContractTimer > 0.0)
            {
                this.grabLength = Mathf.Clamp(this.grabLength - 0.01f * this.swingOnGroundContractTimer * this.timescale, 1f, this.ropeLength - 0.1f);
                this.swingOnGroundContractTimer -= this.timescale;
            }
            Vector3 b = !this.hipSwing ? this.handL.position : this.transform.position + this.transform.up * -0.75f + this.physicsInterpolationOffset;
            this.dst = Vector3.Distance(this.swingTransform.position, b);
            if ((double)this.dst > (double)this.grabLength)
            {
                this.vect = this.swingTransform.position - b;
                this.vect = this.vect.normalized;
                this.vect *= this.dst - this.grabLength;
                this.transform.position = this.transform.position + this.vect;
                if (!this.onGround)
                    this.ySpeed *= Mathf.Pow(0.6f, this.root.fixedTimescale);
            }
            if (!this.onGround)
            {
                this.xSpeed += this.root.DampAdd(this.swingTransform.position.x, b.x, Mathf.Clamp01((float)(0.200000002980232 * (double)Mathf.Clamp(4.5f - this.dst, 1f, 4.5f) * (!((UnityEngine.Object)component != (UnityEngine.Object)null) || !component.weightedString ? 0.600000023841858 : 2.0))));
                this.xSpeed *= Mathf.Pow((float)(0.986000001430511 * (double)Mathf.Clamp(this.dst / 1.5f, 0.98f, 1f) * (!((UnityEngine.Object)component != (UnityEngine.Object)null) || !component.weightedString ? 1.0 : 0.959999978542328)), this.timescale);
            }
            if (!this.startedRolling)
            {
                this.targetRotation = Mathf.Clamp(this.xSpeed * 6f, -35f, 35f);
                if (flag && (this.kSecondaryAim || (double)this.timeSinceKSecondaryAimForHipSwingPurpose < 300.0 || this.hipSwing))
                {
                    this.targetRotation += 180f;
                    this.transform.rotation = this.root.DampSlerp(Quaternion.Euler(0.0f, 0.0f, this.targetRotation), this.transform.rotation, (float)(0.0500000007450581 + 0.349999994039536 * (1.0 - (double)this.root.unityTimescale)));
                }
            }
            if ((double)this.framesInSwingMode > 2.0 && (!this.root.autoSwing && !this.kJumpHeldDown || this.root.autoSwing && this.kJump))
            {
                if (!this.onGround)
                    this.jumpFromSwinging();
                if (this.hipSwing)
                {
                    this.rotationSpeed -= this.xSpeed * 0.5f;
                    this.hipSwing = false;
                }
                this.swinging = false;
                this.doPlayerJumpSound();
                this.jumpedFromSwinging = true;
            }
        }
        else
        {
            this.hipSwing = false;
            this.framesInSwingMode = 0.0f;
        }
        if (!this.onGround && (double)this.visualCrouchAmount <= 0.400000005960464)
        {
            this.upperLegR.localRotation *= Quaternion.Euler(0.0f, -this.punchTimer * 1f, 0.0f);
            this.lowerLegR.localRotation *= Quaternion.Euler(0.0f, this.punchTimer * 2.5f, this.punchTimer * 3f);
            this.footR.localRotation *= Quaternion.Euler(0.0f, 0.0f, this.punchTimer * 3f);
        }
        if (this.onGround && this.okToStand && (!this.startedRolling && (double)this.visualCrouchAmount <= 0.0))
        {
            this.prevVisualCrouchAmount = this.visualCrouchAmount = 0.0f;
            this.tempLegLength = this.legLength;
            if (!this.pedroBoss)
            {
                if (Physics.Raycast(this.footTipR.position + new Vector3(0.0f, this.tempLegLength, 0.0f), Vector3.down, out this.footRayHit, this.tempLegLength, (int)this.layerMask))
                {
                    this.tempDist = Vector3.Distance(this.footTipR.position + new Vector3(0.0f, this.tempLegLength, 0.0f), this.footRayHit.point);
                    if ((double)this.tempDist > 0.219999998807907)
                    {
                        this.legRotOffset = Mathf.Clamp((float)(1.0 - (double)this.tempDist / (double)this.tempLegLength) * -180f, -80f * Mathf.Clamp01(1f - this.justLandedTimer / 9f), 0.0f);
                        this.upperLegR.localRotation *= Quaternion.Euler(this.legRotOffset * 0.5f, this.legRotOffset, 0.0f);
                        this.lowerLegR.localRotation *= Quaternion.Euler(0.0f, -this.legRotOffset, -this.legRotOffset);
                    }
                }
                if (Physics.Raycast(this.footTipL.position + new Vector3(0.0f, this.tempLegLength, 0.0f), Vector3.down, out this.footRayHit, this.tempLegLength, (int)this.layerMask))
                {
                    this.tempDist = Vector3.Distance(this.footTipL.position + new Vector3(0.0f, this.tempLegLength, 0.0f), this.footRayHit.point);
                    if ((double)this.tempDist > 0.219999998807907)
                    {
                        this.legRotOffset = Mathf.Clamp(Mathf.Clamp((float)(1.0 - (double)this.tempDist / (double)this.tempLegLength) * -180f, -45f * Mathf.Clamp01(1f - this.justLandedTimer / 9f), 0.0f) * 2f, -80f, 0.0f);
                        this.upperLegL.localRotation *= Quaternion.Euler(this.legRotOffset * 0.309f, this.legRotOffset * 0.62f, this.legRotOffset * -0.526f);
                        this.lowerLegL.localRotation *= Quaternion.Euler(this.legRotOffset * -0.309f, this.legRotOffset * -0.62f, this.legRotOffset * 0.426f);
                    }
                }
            }
        }
        this.animator.SetFloat("SideStepAmount", this.sideStepAmount);
        if ((double)this.fireWeaponDelay > 0.0)
        {
            if (!this.fireWeaponDelayDoOnce)
                this.fireWeaponDelayDoOnce = true;
            this.fireWeapon = false;
            this.fireWeaponDelay -= this.timescale;
        }
        else if (this.fireWeaponDelayDoOnce)
        {
            this.fireWeapon = true;
            this.fireWeaponDelayDoOnce = false;
        }
        if (this.fireSecondaryWeapon && this.weapon == 5)
        {

            PacketSender.SendPlayerMachineGunGrenade(this.bulletPointR2.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position), UnityEngine.Random.Range(-30, 30));

            if (MultiplayerManagerTest.singleplayerMode)
            {
                this.tempBulletVar = UnityEngine.Object.Instantiate<GameObject>(this.machineGunGrenade, this.bulletPointR2.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position));
                Rigidbody component = (Rigidbody)this.tempBulletVar.GetComponent(typeof(Rigidbody));
                component.velocity = this.tempBulletVar.transform.forward * 23f;
                this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR2.position, this.tempBulletVar.transform.forward, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);

                int num1 = UnityEngine.Random.Range(-30, 30);
                Vector3 angularVelocity = component.angularVelocity;
                double num2 = (double)(angularVelocity.y = (float)num1);
                Vector3 vector3 = component.angularVelocity = angularVelocity;
            }

            this.timeSinceGrenadeLaunched = 0.0f;
            this.secondaryFireDelay = 30f;
            this.handLAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            this.handLAudio.volume = UnityEngine.Random.Range(0.95f, 1.1f);
            this.handLAudio.Play();
        }
        if (this.fireWeapon)
        {

            //        PacketSender.SendGenericGunfire();

            if (swinging)
                PacketSender.SendPlayerGunSound(true, false);

            Vector3 vector3_1 = new Vector3(!this.wallTouchRight ? (!this.wallTouchLeft ? 0.0f : 0.1f) : -0.1f, (float)(-((double)this.topDuckAmount / 90.0) * 0.400000005960464), 0.0f);
            this.root.musicIntenseFactor = Mathf.Clamp01(this.root.musicIntenseFactor + 0.05f);
            this.root.playerFiredWeapon = true;
            if (this.weapon == 1 || this.weapon == 2 && this.swinging && !this.hipSwing)
            {
                // this.playGunSound(true);
                this.root.rumble(1, 0.25f, 0.1f);
                // this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);

                Vector3 shootVec = this.bulletPointR.position + vector3_1;
                Quaternion shootRot = Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position);

                PacketSender.SendGenericGunfire(shootVec, shootRot, UnityEngine.Random.Range(-1f, 1f), aimSpread);

                /*  this.tempBulletVar = this.root.getBullet(this.bulletPointR.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                  this.tempBulletScript = this.root.getBulletScript();
                  this.tempBulletScript.bulletStrength = 0.35f;
                  this.tempBulletScript.doPostSetup();
                  */
                this.root.getMuzzleFlash(0, this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                // this.tempBulletVar.transform.rotation *= Quaternion.Euler(UnityEngine.Random.Range(-1f, 1f) * this.aimSpread, 0.0f, 0.0f);
                this.cameraScript.screenShake += 0.02f;
                this.cameraScript.kickBack(0.2f);
                this.fireDelay = 10f;
            }
            else if (this.weapon == 2)
            {

                //Vector3 shootVec = (fireLeftGun || this.kSecondaryAim ? this.bulletPointL.position + vector3_1 : this.bulletPointR.position + vector3_1);
                //	Quaternion shootRot = (fireLeftGun || this.kSecondaryAim ? Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position) : Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));

                //PacketSender.SendGenericGunfire(shootVec, shootRot, true);

                Vector3 shootVec;
                Quaternion shootRot;

                if (this.fireLeftGun || this.kSecondaryAim)
                {
                    //  this.playGunSound(false);
                    this.root.rumble(0, 0.25f, 0.1f);
                    // this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointL.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                    this.shellParticle.Emit(this.root.generateEmitParams(this.handL.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);

                    shootVec = this.bulletPointL.position + vector3_1;
                    shootRot = Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position);

                    PacketSender.SendGenericGunfire(shootVec, shootRot, 0, UnityEngine.Random.Range(-4f, 4f), true);

                    //    if (!kSecondaryAim)
                    //   PacketSender.SendPlayerGunSound(false, false);

                    //this.tempBulletVar = this.root.getBullet(this.bulletPointL.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position));
                    //  this.tempBulletScript = this.root.getBulletScript();
                    // this.tempBulletScript.bulletStrength = 0.35f;
                    //  this.setBulletScoreParameters(this.tempBulletScript);
                    // this.tempBulletScript.tailCheck = Vector2.Distance((Vector2)this.tempBulletScript.transform.position, (Vector2)this.neck.position);
                    // this.tempBulletScript.doPostSetup();
                    this.root.getMuzzleFlash(0, this.bulletPointL.position, Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                }
                if ((!this.fireLeftGun || this.kSecondaryAim) && (!this.kSecondaryAim || this.kSecondaryAim && (double)this.ammo[this.weapon] > 0.0))
                {
                    //this.playGunSound(true);
                    this.root.rumble(1, 0.25f, 0.1f);
                    if (this.kSecondaryAim)
                    {
                        this.ammo[this.weapon] = this.ammo[this.weapon] - 1f;
                        this.updateAmmoHUD();
                    }
                    this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                    this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);


                    shootVec = this.bulletPointR.position + vector3_1;
                    shootRot = Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position);

                    PacketSender.SendGenericGunfire(shootVec, shootRot, 0, UnityEngine.Random.Range(-4f, 4f), true);

                    //    if (!kSecondaryAim)
                    //     PacketSender.SendPlayerGunSound(true, false);

                    //   this.tempBulletVar = this.root.getBullet(this.bulletPointR.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                    //   this.tempBulletScript = this.root.getBulletScript();
                    //   this.tempBulletScript.bulletStrength = 0.35f;
                    //   this.setBulletScoreParameters(this.tempBulletScript);
                    //  this.tempBulletScript.tailCheck = Vector2.Distance((Vector2)this.tempBulletScript.transform.position, (Vector2)this.neck.position);
                    // this.tempBulletScript.doPostSetup();
                    this.root.getMuzzleFlash(0, this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                }
                //  this.tempBulletVar.transform.rotation *= Quaternion.Euler(UnityEngine.Random.Range(-4f, 4f) * this.aimSpread, 0.0f, 0.0f);

                if (kSecondaryAim)
                    PacketSender.SendPlayerGunSound(true, true);

                this.cameraScript.screenShake += 0.02f;
                this.cameraScript.kickBack(0.2f);
                this.fireLeftGun = !this.fireLeftGun;
                this.fireDelay = !this.kSecondaryAim ? 6f : 10f;
            }
            else if (this.weapon == 3 || this.weapon == 4 && this.swinging && !this.hipSwing)
            {
                //  this.playGunSound(true);


                Vector3 shootVec = this.bulletPointR.position + vector3_1;
                Quaternion shootRot = Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position);


                PacketSender.SendGenericGunfire(shootVec, shootRot, UnityEngine.Random.Range(-2f, 2f), aimSpread, false);

                //  this.root.rumble(1, 0.25f, 0.1f);
                //  this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                // this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                //     this.tempBulletVar = this.root.getBullet(this.bulletPointR.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                this.root.getMuzzleFlash(1, this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                /*   this.tempBulletScript = this.root.getBulletScript();
                   this.tempBulletScript.bulletStrength = 0.2f;
                   this.tempBulletScript.bulletSpeed = 12f;
                   this.tempBulletScript.doPostSetup();
                   this.tempBulletVar.transform.rotation *= Quaternion.Euler(UnityEngine.Random.Range(-2f, 2f) * this.aimSpread, 0.0f, 0.0f);
                   */
                this.cameraScript.onOffScreenShake = 0.11f;
                this.cameraScript.kickBack(0.2f);
                this.fireDelay = 6f;
                this.aimSpread2 += (float)((4.0 - (double)this.aimSpread) * 0.025000000372529);
            }
            else if (this.weapon == 4)
            {

                Vector3 shootVec = Vector3.zero;
                Quaternion shootRot = Quaternion.identity;

                if (this.fireLeftGun || this.kSecondaryAim)
                {

                    shootVec = this.bulletPointL.position + vector3_1;
                    shootRot = Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position);

                    //  this.playGunSound(false);
                    //  this.root.rumble(0, 0.2f, 0.1f);
                    //   this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointL.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                    //  this.shellParticle.Emit(this.root.generateEmitParams(this.handL.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                    //   this.tempBulletVar = this.root.getBullet(this.bulletPointL.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position));
                    this.root.getMuzzleFlash(1, this.bulletPointL.position, Quaternion.LookRotation(new Vector3(this.bulletPointLAimTarget.transform.position.x, this.bulletPointLAimTarget.transform.position.y, this.bulletPointL.position.z) - this.bulletPointL.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                    /*     this.tempBulletScript = this.root.getBulletScript();
                         this.tempBulletScript.bulletStrength = 0.2f;
                         this.tempBulletScript.bulletSpeed = 12f;
                         this.setBulletScoreParameters(this.tempBulletScript);
                         this.tempBulletScript.tailCheck = Vector2.Distance((Vector2)this.tempBulletScript.transform.position, (Vector2)this.neck.position);
                         this.tempBulletScript.doPostSetup();
                         */

                    PacketSender.SendGenericGunfire(shootVec, shootRot, UnityEngine.Random.Range(-3f, 3f), aimSpread, true);
                }
                if ((!this.fireLeftGun || this.kSecondaryAim) && (!this.kSecondaryAim || this.kSecondaryAim && (double)this.ammo[this.weapon] > 0.0))
                {
                    //   this.playGunSound(true);
                    //  this.root.rumble(1, 0.2f, 0.1f);

                    shootVec = this.bulletPointR.position + vector3_1;
                    shootRot = Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position);

                    if (this.kSecondaryAim)
                    {
                        this.ammo[this.weapon] = this.ammo[this.weapon] - 1f;
                        this.updateAmmoHUD();
                    }
                    //  this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                    this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                    //    this.tempBulletVar = this.root.getBullet(this.bulletPointR.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position));
                    this.root.getMuzzleFlash(1, this.bulletPointR.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                    /*    this.tempBulletScript = this.root.getBulletScript();
                        this.tempBulletScript.bulletStrength = 0.2f;
                        this.tempBulletScript.bulletSpeed = 12f;
                        */
                    //      this.setBulletScoreParameters(this.tempBulletScript);
                    // this.tempBulletScript.tailCheck = Vector2.Distance((Vector2)this.tempBulletScript.transform.position, (Vector2)this.neck.position);
                    //this.tempBulletScript.doPostSetup();

                    PacketSender.SendGenericGunfire(shootVec, shootRot, UnityEngine.Random.Range(-3f, 3f), aimSpread, true);
                }


                //  this.tempBulletVar.transform.rotation *= Quaternion.Euler(UnityEngine.Random.Range(-3f, 3f) * this.aimSpread, 0.0f, 0.0f);
                this.cameraScript.onOffScreenShake = 0.11f;
                this.cameraScript.kickBack(0.2f);
                this.fireLeftGun = !this.fireLeftGun;
                this.fireDelay = !this.kSecondaryAim ? 4f : 6f;
                this.aimSpread2 += (float)((4.0 - (double)this.aimSpread) * 0.0500000007450581);
            }
            else if (this.weapon == 5)
            {


                if (this.isEnemy)
                {
                    this.playGunSound(true);
                    this.root.rumble(0, 0.2f, 0.1f);
                    this.root.rumble(1, 0.35f, 0.1f);
                    this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR2.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                    this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);

                    this.tempBulletVar = this.root.getBullet(this.bulletPointR2.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position));
                    if (!this.dodging && (double)this.tempShoulderRBulletPointR2DistanceMultiplier > 0.300000011920929)
                        this.tempBulletVar.transform.rotation = Quaternion.LookRotation(this.mousePos - new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.mousePos.z));
                    this.tempBulletScript = this.root.getBulletScript();
                    this.tempBulletScript.bulletStrength = 0.45f;
                    this.tempBulletScript.bulletSpeed = 14f;
                    this.tempBulletScript.bulletLength = 2f;
                    this.tempBulletScript.friendly = !this.isEnemy;
                    this.tempBulletScript.doPostSetup();
                    this.tempBulletVar.transform.rotation *= Quaternion.Euler(UnityEngine.Random.Range(-1.5f, 1.5f) * this.aimSpread, 0.0f, 0.0f);
                }
                else
                {
                    PacketSender.SendGenericGunfire(bulletPointR2.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position), UnityEngine.Random.Range(-1.5f, 1.5f), aimSpread, false);
                }

                this.root.getMuzzleFlash(2, this.bulletPointR2.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position) * Quaternion.Euler(0.0f, 270f, 0.0f));

                this.cameraScript.onOffScreenShake = 0.11f;
                this.cameraScript.kickBack(0.25f);
                this.fireDelay = 4f;
            }
            else if (this.weapon == 6)
            {
                //  this.playGunSound(true);
                this.root.rumble(0, 0.25f, 0.15f);
                this.root.rumble(1, 0.45f, 0.2f);
                //   this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR2.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                //   this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);


                float[] bulletRnd = new float[8];

                bulletRnd[0] = -999;

                for (int index = 1; index < 8; ++index)
                {
                    bulletRnd[index] = UnityEngine.Random.Range(-9f, 9f);
                }


                PacketSender.SendShotgunfire(this.bulletPointR2.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position), bulletRnd, (!this.dodging && (double)this.tempShoulderRBulletPointR2DistanceMultiplier > 0.300000011920929), new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position);

                /*    for (int index = 0; index < 8; ++index)
                    {
                        this.tempBulletVar = this.root.getBullet(this.bulletPointR2.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position));
                        if (!this.dodging && (double)this.tempShoulderRBulletPointR2DistanceMultiplier > 0.300000011920929)
                            this.tempBulletVar.transform.rotation = Quaternion.LookRotation(this.mousePos - new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.mousePos.z));
                        this.tempBulletScript = this.root.getBulletScript();
                        this.tempBulletScript.bulletKillOnHeadshot = false;
                        this.tempBulletScript.bulletStrength = 0.2f;
                        this.tempBulletScript.bulletSpeed = 8f + (float)index * 0.5f;
                        this.tempBulletScript.bulletLength = 0.4f;
                        this.tempBulletScript.allowGib = true;
                        if (index > 0)
                            this.tempBulletVar.transform.rotation *= Quaternion.Euler(UnityEngine.Random.Range(-9f, 9f), 0.0f, 0.0f);
                        this.setBulletScoreParameters(this.tempBulletScript);
                        this.tempBulletScript.tailCheck = Vector2.Distance((Vector2)this.tempBulletScript.transform.position, (Vector2)this.neck.position);
                        this.tempBulletScript.doPostSetup();
                    }

                    */
                this.root.getMuzzleFlash(3, this.bulletPointR2.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
                this.cameraScript.screenShake += 0.08f;
                this.cameraScript.kickBack(0.45f);
                this.fireDelay = 20f;
            }
            else if (this.weapon == 7)
                this.turretGunScript.fire = true;
            else if (this.weapon == 8)
            {
                this.playGunSound(true);
                this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR2.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                this.tempBulletVar = UnityEngine.Object.Instantiate<GameObject>(this.shockRifleBullet, this.bulletPointR2.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position));
                this.xSpeed = Mathf.Clamp(this.xSpeed - this.tempBulletVar.transform.forward.x * 8f, -9f, 9f) * 1.5f;
                if (!this.onGround)
                    this.ySpeed = this.tempBulletVar.transform.forward.y * (!this.justPulseJumped ? -10f : -5f);
                this.rotationSpeed += this.tempBulletVar.transform.forward.x * 1.5f;
                this.extraJumpPower = this.justPulseJumped ? 0.0f : 0.5f;
                this.justPulseJumped = true;
                this.cameraScript.onOffScreenShake = 0.11f;
                this.cameraScript.kickBack(0.25f);
                this.fireDelay = 45f;
            }
            else if (this.weapon == 9)
            {
                // this.playGunSound(true);
                this.root.rumble(0, 0.35f, 0.1f);
                this.root.rumble(1, 0.35f, 0.15f);
                //   this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR2.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                //  this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);

                /*     this.tempBulletVar = this.root.getBullet(this.bulletPointR2.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position));
             if (!this.dodging && (double)this.tempShoulderRBulletPointR2DistanceMultiplier > 0.300000011920929)
                 this.tempBulletVar.transform.rotation = Quaternion.LookRotation(this.mousePos - new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.mousePos.z));
             this.tempBulletScript = this.root.getBulletScript();
             this.tempBulletScript.bulletStrength = 1f;
             this.tempBulletScript.bulletSpeed = 17f;
             this.tempBulletScript.bulletLength = 2.5f;
             this.tempBulletScript.knockBack = true;
             this.tempBulletScript.doPostSetup();
             */

                float quaternionRnd = (!kSecondaryAim ? UnityEngine.Random.Range(-1f, 1f) : -999);

                this.root.getMuzzleFlash(4, this.bulletPointR2.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position) * Quaternion.Euler(0.0f, 270f, 0.0f));

                PacketSender.SendGenericGunfire(bulletPointR2.position + vector3_1, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position), quaternionRnd, aimSpread, false);

                /*     if (!this.kSecondaryAim)
                        this.tempBulletVar.transform.rotation *= Quaternion.Euler(UnityEngine.Random.Range(-1f, 1f) * this.aimSpread, 0.0f, 0.0f);
                        */
                this.cameraScript.onOffScreenShake = 0.11f;
                this.cameraScript.kickBack(0.25f);
                this.fireDelay = 6f;
            }
            else if (this.weapon == 10)
            {
                this.playGunSound(true);
                this.smokeParticle.Emit(this.root.generateEmitParams(this.bulletPointR2.position, Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
                this.tempBulletVar = UnityEngine.Object.Instantiate<GameObject>(this.crossbowArrow, this.bulletPointR2.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.bulletPointR2.position.z) - this.bulletPointR2.position));
                if (!this.dodging && (double)this.tempShoulderRBulletPointR2DistanceMultiplier > 0.300000011920929)
                    this.tempBulletVar.transform.rotation = Quaternion.LookRotation(this.mousePos - new Vector3(this.bulletPointRAimTarget2.transform.position.x, this.bulletPointRAimTarget2.transform.position.y, this.mousePos.z));
                this.crossbowArrows[(int)this.ammo[this.weapon]].SetActive(false);
                this.tempBulletVar.transform.rotation *= Quaternion.Euler(UnityEngine.Random.Range(-1f, 1f) * this.aimSpread, 0.0f, 0.0f);
                this.cameraScript.onOffScreenShake = 0.11f;
                this.cameraScript.kickBack(0.25f);
                this.fireDelay = 45f;
            }
            this.setBulletScoreParameters(this.tempBulletScript);
            this.tempBulletScript.tailCheck = Vector2.Distance((Vector2)this.tempBulletScript.transform.position, (Vector2)this.neck.position);
            this.aimSpread2 += (float)((4.0 - (double)this.aimSpread) * 0.100000001490116);
            if (this.isEnemy || this.playerNr > 0)
            {
                BulletScript component = (BulletScript)this.tempBulletVar.GetComponent(typeof(BulletScript));
                component.friendly = false;
                if (this.isEnemy)
                    component.bulletStrength *= 0.5f;
            }
            if (this.pedroBoss)
            {
                float num1 = 2.5f;
                Vector3 localScale = this.tempBulletVar.transform.localScale;
                double num2 = (double)(localScale.y = num1);
                Vector3 vector3_2 = this.tempBulletVar.transform.localScale = localScale;
            }
            if (this.onMotorcycle)
            {
                float num1 = ((BoxCollider)this.tempBulletVar.GetComponent(typeof(BoxCollider))).size.x + 4f;
                Vector3 size = ((BoxCollider)this.tempBulletVar.GetComponent(typeof(BoxCollider))).size;
                double num2 = (double)(size.x = num1);
                Vector3 vector3_2 = ((BoxCollider)this.tempBulletVar.GetComponent(typeof(BoxCollider))).size = size;
            }
        }
        this.aimSpread2 = Mathf.Clamp(this.aimSpread2 - (float)((!this.kAction ? 0.0 : 0.100000001490116) + 0.0599999986588955 + (double)this.crouchAmount * 0.0500000007450581) * this.timescale, (float)(0.400000005960464 - (double)this.crouchAmount * 0.200000002980232), 4f);
        this.aimSpread = this.aimSpread2 + Mathf.Abs(this.xSpeed * 0.02f) + Mathf.Abs(this.ySpeed * 0.01f);
        if (this.rootShared.modIncreaseAccuracy)
            this.aimSpread = this.aimSpread2 = 0.25f;
        if (!this.isEnemy)
        {
            this.mainCursor.sizeDelta = Vector2.one * 15f * (this.aimSpread + 0.5f);
            float num1 = Mathf.Clamp(1f - (float)(((double)this.aimSpread - 0.5) / 2.5), 0.3f, 1f);
            Color color1 = this.mainCursorImage.color;
            double num2 = (double)(color1.a = num1);
            Color color2 = this.mainCursorImage.color = color1;
            if (this.root.multiplayer)
                this.mainCursor.position = Input.mousePosition;
            if (this.gamepad)
            {
                if (this.gamepadMode != 4)
                    this.mainCursor.position = this.curCamera.WorldToScreenPoint(this.mousePos);
                Vector3 normalized1 = (Vector3)this.targetGamepadAimOffset.normalized;
                RaycastHit hitInfo = new RaycastHit();
                float a1 = this.mainCursorImage.color.a;
                if (Physics.Raycast(this.gamepadAimingReferencePoint + ((double)this.shootFromCoverLayerWeight <= 0.0 ? Vector3.zero : normalized1), normalized1, out hitInfo, this.gamepadMode == 4 || this.gamepadMode == 5 || this.gamepadMode == 1 ? 20f : 15f, (int)this.layerMaskIncEnemiesAndEnemyGameCollisionWithoutBulletPassthrough))
                {
                    int layer = hitInfo.transform.gameObject.layer;
                    if (!this.root.disableShooting && (layer == 10 || layer == 13))
                    {
                        this.mainCursor.localScale = Vector3.one;
                        this.mainCursorImage.color = Color.red;
                        this.aimHelperImage.color = Color.red;
                    }
                    else
                    {
                        this.mainCursor.localScale = Vector3.one * 0.6f;
                        this.mainCursorImage.color = this.mainCursorImageStartColour;
                        this.aimHelperImage.color = this.aimHelperImageStartColour;
                    }
                    float num3 = a1;
                    Color color3 = this.mainCursorImage.color;
                    double num4 = (double)(color3.a = num3);
                    Color color4 = this.mainCursorImage.color = color3;
                    if (this.gamepadMode == 5 || this.gamepadMode == 1)
                        this.aimHelperDistance = (double)hitInfo.distance >= (double)this.aimHelperDistance ? this.root.DampUnscaled(hitInfo.distance, this.aimHelperDistance, 0.25f) : this.root.DampUnscaled(hitInfo.distance, this.aimHelperDistance, 0.4f);
                    else if (this.gamepadMode == 4)
                        this.mainCursor.position = this.curCamera.WorldToScreenPoint(hitInfo.point);
                    else
                        this.aimHelperDistance = this.root.DampUnscaled(hitInfo.distance, this.aimHelperDistance, 0.015f);
                }
                else
                {
                    float a2 = this.mainCursorImage.color.a;
                    if (!this.root.disableShooting)
                    {
                        this.mainCursor.localScale = Vector3.one * 0.6f;
                        this.mainCursorImage.color = this.mainCursorImageStartColour;
                        this.aimHelperImage.color = this.aimHelperImageStartColour;
                    }
                    float num3 = a2;
                    Color color3 = this.mainCursorImage.color;
                    double num4 = (double)(color3.a = num3);
                    Color color4 = this.mainCursorImage.color = color3;
                    if (this.gamepadMode == 5 || this.gamepadMode == 1)
                        this.aimHelperDistance = this.root.DampUnscaled(20f, this.aimHelperDistance, 0.25f);
                    else if (this.gamepadMode == 4)
                        this.mainCursor.position = (Vector3)new Vector2(-999f, -999f);
                    else
                        this.aimHelperDistance = this.root.DampUnscaled(15f, this.aimHelperDistance, 0.015f);
                }
                if (this.gamepadMode == 5 || this.gamepadMode == 1)
                {
                    this.fakeGamepadMousePos = this.gamepadAimingReferencePoint + normalized1 * this.aimHelperDistance;
                    this.aimHelper.position = this.mainCursor.position = this.curCamera.WorldToScreenPoint(this.fakeGamepadMousePos);
                    this.mainCursor.anchoredPosition += (Vector2)(normalized1 * -6.5f * this.root.unscaledTimescale);
                    Vector3 viewportPoint = this.curCamera.WorldToViewportPoint(this.gamepadAimingReferencePoint);
                    viewportPoint.x *= this.hudCanvasRect.sizeDelta.x;
                    viewportPoint.y *= this.hudCanvasRect.sizeDelta.y;
                    float num3 = Mathf.Clamp(Vector2.Distance((Vector2)viewportPoint, this.aimHelper.anchoredPosition) - 90f, 9.5f, 9999f);
                    Vector2 sizeDelta = this.aimHelper.sizeDelta;
                    double num4 = (double)(sizeDelta.y = num3);
                    Vector2 vector2 = this.aimHelper.sizeDelta = sizeDelta;
                    float num5 = Mathf.Atan2(normalized1.y, normalized1.x) * 57.29578f - 90f;
                    Quaternion rotation1 = this.mainCursor.rotation;
                    Vector3 eulerAngles1 = rotation1.eulerAngles;
                    double num6 = (double)(eulerAngles1.z = num5);
                    Vector3 vector3_1 = rotation1.eulerAngles = eulerAngles1;
                    Quaternion quaternion1 = this.mainCursor.rotation = rotation1;
                    float num7 = num5;
                    Quaternion rotation2 = this.aimHelper.rotation;
                    Vector3 eulerAngles2 = rotation2.eulerAngles;
                    double num8 = (double)(eulerAngles2.z = num7);
                    Vector3 vector3_2 = rotation2.eulerAngles = eulerAngles2;
                    Quaternion quaternion2 = this.aimHelper.rotation = rotation2;
                }
                else if (this.gamepadMode == 4)
                {
                    this.aimHelper.position = this.curCamera.WorldToScreenPoint(this.mousePos);
                    Vector3 normalized2 = (this.gamepadAimingReferencePoint - this.mousePos).normalized;
                    float num3 = Mathf.Atan2(normalized2.y, normalized2.x) * 57.29578f + 90f;
                    Quaternion rotation1 = this.mainCursor.rotation;
                    Vector3 eulerAngles1 = rotation1.eulerAngles;
                    double num4 = (double)(eulerAngles1.z = num3);
                    Vector3 vector3_1 = rotation1.eulerAngles = eulerAngles1;
                    Quaternion quaternion1 = this.mainCursor.rotation = rotation1;
                    float num5 = num3;
                    Quaternion rotation2 = this.aimHelper.rotation;
                    Vector3 eulerAngles2 = rotation2.eulerAngles;
                    double num6 = (double)(eulerAngles2.z = num5);
                    Vector3 vector3_2 = rotation2.eulerAngles = eulerAngles2;
                    Quaternion quaternion2 = this.aimHelper.rotation = rotation2;
                }
                else
                {
                    this.aimHelperDistance = Mathf.Clamp(this.aimHelperDistance, 10f, 18f);
                    this.aimHelper.position = this.curCamera.WorldToScreenPoint(this.gamepadAimingReferencePoint + normalized1 * this.aimHelperDistance);
                    Vector3 normalized2 = (this.mainCursor.position - this.aimHelper.position).normalized;
                    float num3 = Mathf.Atan2(normalized2.y, normalized2.x) * 57.29578f + 90f;
                    Quaternion rotation1 = this.mainCursor.rotation;
                    Vector3 eulerAngles1 = rotation1.eulerAngles;
                    double num4 = (double)(eulerAngles1.z = num3);
                    Vector3 vector3_1 = rotation1.eulerAngles = eulerAngles1;
                    Quaternion quaternion1 = this.mainCursor.rotation = rotation1;
                    float num5 = num3;
                    Quaternion rotation2 = this.aimHelper.rotation;
                    Vector3 eulerAngles2 = rotation2.eulerAngles;
                    double num6 = (double)(eulerAngles2.z = num5);
                    Vector3 vector3_2 = rotation2.eulerAngles = eulerAngles2;
                    Quaternion quaternion2 = this.aimHelper.rotation = rotation2;
                }
            }
            if (this.root.paused || (double)this.health <= 0.0)
            {
                if (this.gamepad)
                    this.aimHelper.position = (Vector3)(Vector2.one * 9999f);
                if ((double)this.health > 0.0 || this.gamepad)
                {
                    int num3 = 0;
                    Color color3 = this.mainCursorImage.color;
                    double num4 = (double)(color3.a = (float)num3);
                    Color color4 = this.mainCursorImage.color = color3;
                }
            }
            if (this.rootShared.hideHUD && !this.root.paused)
            {
                this.mainCursor.sizeDelta = Vector2.one * 17f;
                int num3 = 0;
                Color color3 = this.aimHelperImage.color;
                double num4 = (double)(color3.a = (float)num3);
                Color color4 = this.aimHelperImage.color = color3;
                this.mainCursor.localScale = Vector3.one * 0.5f;
                float num5 = 0.55f;
                Color color5 = this.mainCursorImage.color;
                double num6 = (double)(color5.a = num5);
                Color color6 = this.mainCursorImage.color = color5;
            }
            if (this.rootShared.modCinematicCamera && this.root.kAction)
            {
                int num3 = 0;
                Color color3 = this.aimHelperImage.color;
                double num4 = (double)(color3.a = (float)num3);
                Color color4 = this.aimHelperImage.color = color3;
            }
        }
        this.movementSound.volume = this.root.Damp((float)(((double)(this.prevFootRPos - this.footR.position).magnitude + (double)Mathf.Clamp((this.prevHandRPos - this.handR.position).magnitude, 0.0f, 0.4f)) * (1.0 + (double)Mathf.Abs(this.rotationSpeed) * 0.0900000035762787) * 1.20000004768372), this.movementSound.volume, 0.35f);
        this.prevFootRPos = this.footR.position;
        this.prevHandRPos = this.handR.position;
        if (this.skyfall && !this.root.levelEnded && (!this.root.paused && !this.root.dead))
        {
            if ((double)UnityEngine.Random.value > 0.600000023841858)
                this.smokeParticle.Emit(this.root.generateEmitParams(this.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.25f, 0.25f), 0.0f), new Vector3((float)(-(double)this.xSpeed * 0.25), 0.0f, 0.0f), 3f, 2f, new Color(1f, 1f, 1f, 0.05f)), 1);
            float num = 0.05f;
            this.lowerBack.localScale = Vector3.one + new Vector3((float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num);
            this.lowerArmR.localScale = Vector3.one + new Vector3((float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num) * 0.5f;
            this.lowerArmL.localScale = Vector3.one + new Vector3((float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num) * 0.5f;
            this.upperLegR.localScale = Vector3.one + new Vector3((float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num);
            this.upperLegL.localScale = Vector3.one + new Vector3((float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num);
            this.lowerLegR.localScale = Vector3.one + new Vector3((float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num);
            this.lowerLegL.localScale = Vector3.one + new Vector3((float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num, (float)UnityEngine.Random.Range(-1, 1) * num);
            if ((double)this.rootShared.modPlayerSize == 100.0)
                this.transform.localScale = new Vector3(UnityEngine.Random.Range(0.97f, 1.03f), UnityEngine.Random.Range(0.97f, 1.03f), UnityEngine.Random.Range(0.97f, 1.03f));
        }
        if (this.overrideControls && !this.isEnemy)
        {
            this.speechBubbleCoolDownTimer = 10f;
            this.disableInputTimer = 5f;
            this.kJump = false;
            this.kXDir = 0.0f;
            this.kJumpHeldDown = false;
            this.kCrouch = false;
            this.kFire = false;
            this.kSecondaryAim = false;
            this.kReload = false;
            this.kUse = false;
            this.kUseHeldDown = false;
            this.kChangeWeapon = false;
            if (!this.dontResetMousePosOnOverrideControls && (double)Mathf.Abs(this.xSpeed) > 0.200000002980232)
                this.mousePos = this.mousePos2 = this.neck.position + new Vector3((double)this.xSpeed <= 0.0 ? -1.5f : 1.5f, 0.0f, 0.0f);
        }
        if (this.kSecondaryAim || this.hipSwing && this.kFire)
            this.timeSinceKSecondaryAimForHipSwingPurpose = 0.0f;
        else
            this.timeSinceKSecondaryAimForHipSwingPurpose += this.timescale;
        if ((double)this.speedModifier == 1.0)
            return;
        this.root.timescale = timescale;
        this.root.fixedTimescale = fixedTimescale;
        this.root.unscaledTimescale = unscaledTimescale;
    }

    private void setBulletScoreParameters(BulletScript bulletScript)
    {

        if (bulletScript == null)
            return;

        bulletScript.midAirShot = !this.onGround;
        bulletScript.wallJumpShot = !this.justJumpedFromSkateboard && this.justWallJumped;
        bulletScript.enemyJumpShot = this.justJumpedOffEnemy;
        bulletScript.playerAngularVelocityShot = this.rotationSpeed;
        bulletScript.splitShot = (double)Vector2.Angle((Vector2)(this.transform.position - this.mousePos), (Vector2)(this.transform.position - this.mousePos2)) > 10.0;
        bulletScript.dodgeShot = this.dodging;
        bulletScript.swingShot = this.swinging;
        BulletScript bulletScript1 = bulletScript;
        int num = this.swinging ? 1 : 0;
        if (num != 0)
            num = (UnityEngine.Object)this.swingTransform.GetComponent(typeof(SlideSwingScript)) != (UnityEngine.Object)null ? 1 : 0;
        bulletScript1.slideShot = num != 0;
        bulletScript.dramaticEntranceShot = (double)this.dramaticEntranceTimer > 0.0;
        bulletScript.slowMoShot = this.kAction;
    }

    public virtual void playGunSound(bool isRight)
    {
        if (isRight)
        {
            this.handRAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            this.handRAudio.volume = UnityEngine.Random.Range(0.95f, 1.1f);
            this.handRAudio.Play();
        }
        else
        {
            this.handLAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            this.handLAudio.volume = UnityEngine.Random.Range(0.95f, 1.1f);
            this.handLAudio.Play();
        }
        float num = 1f - this.ammo[this.weapon] / this.ammoFullClip[this.weapon];
        this.ammoLeftAudio.pitch = (float)(0.200000002980232 + 1.20000004768372 * (double)num) + UnityEngine.Random.Range(-0.05f, 0.05f);
        this.ammoLeftAudio.volume = (float)(1.5 * (double)num - 0.5) + UnityEngine.Random.Range(-0.05f, 0.05f);
        this.ammoLeftAudio.Play();
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != 14 || !(col.tag == "Glass"))
            return;
        ((Rigidbody)col.GetComponent(typeof(Rigidbody))).isKinematic = false;
        this.tempGlassPieceScript = (GlassPieceScript)col.GetComponent(typeof(GlassPieceScript));
        this.tempGlassPieceScript.isTriggered = true;
        this.tempGlassPieceScript.particleVelocity = this.rBody.velocity;
        ((Behaviour)col.GetComponent(typeof(RigidBodySlowMotion))).enabled = true;
    }

    public virtual void OnTriggerStay(Collider col)
    {
        if (!(col.tag == "Z Pusher"))
            return;
        this.tempYPosCheck = (float)((double)this.transform.position.y - (double)this.legLength + 0.5);
        if ((double)this.tempYPosCheck < (double)col.transform.position.y || (double)this.tempYPosCheck > (double)col.bounds.max.y - 1.10000002384186)
            return;
        this.zPushBack = 3f;
    }

    public virtual void jumpFromSwinging()
    {
        this.targetXSpeed = this.xSpeed = Mathf.Clamp(this.xSpeed * Mathf.Pow(1.5f, this.root.fixedTimescale), -12f, 12f);
        if ((double)this.kXDir != 0.0 && (double)Mathf.Abs(this.targetXSpeed) < 10.0)
            this.targetXSpeed = this.xSpeed = this.kXDir * 10f;
        this.rotationSpeed = (float)(-(double)this.xSpeed * 0.100000001490116);
        this.ySpeed = 7f * Mathf.Clamp01(Mathf.Abs(this.xSpeed) * 0.1f);
        if ((double)this.xSpeed > 0.0 && this.faceRight || (double)this.xSpeed < 0.0 && !this.faceRight)
            this.animator.Play("JumpForward", 0, 0.0f);
        else
            this.animator.Play("JumpBackwards", 0, 0.0f);
    }

    public virtual void changeWeapon(float w)
    {
        if (this.kSecondaryAim || (double)w != 0.0 && !this.weaponActive[(int)w])
            return;
        this.pistolL.gameObject.SetActive(false);
        this.pistolR.gameObject.SetActive(false);
        this.uziR.gameObject.SetActive(false);
        this.uziL.gameObject.SetActive(false);
        this.machineGun.gameObject.SetActive(false);
        this.shotgun.gameObject.SetActive(false);
        this.turretGun.gameObject.SetActive(false);
        this.shockRifle.gameObject.SetActive(false);
        this.rifle.gameObject.SetActive(false);
        this.crossbow.gameObject.SetActive(false);
        this.reloading = false;
        this.vHandR = (SkinnedMeshRenderer)this.handR.Find("hand_01").GetComponent(typeof(SkinnedMeshRenderer));
        this.vHandL = (SkinnedMeshRenderer)this.handL.Find("hand_01_L").GetComponent(typeof(SkinnedMeshRenderer));
        this.cameraScript.tiltMultiplier = 1f;
        this.fightMode = false;
        this.fightPoseBlendTimer = 0.0f;



        if ((double)w == 0.0)
        {
            this.fightMode = true;
            this.fireLeftGun = false;
            this.aimWithLeftArm = false;
            this.twoHandedWeapon = false;
            this.automaticWeapon = false;
            this.animator.SetFloat("IdleNr", 1f);
        }
        else if ((double)w == 1.0)
        {
            this.pistolR.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 20f);
            this.fireLeftGun = false;
            this.aimWithLeftArm = false;
            this.twoHandedWeapon = false;
            this.automaticWeapon = false;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        else if ((double)w == 2.0)
        {
            this.pistolR.gameObject.SetActive(true);
            this.pistolL.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 100f);
            this.aimWithLeftArm = true;
            this.twoHandedWeapon = false;
            this.automaticWeapon = false;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        else if ((double)w == 3.0)
        {
            this.uziR.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 20f);
            this.fireLeftGun = false;
            this.aimWithLeftArm = false;
            this.twoHandedWeapon = false;
            this.automaticWeapon = true;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        else if ((double)w == 4.0)
        {
            this.uziR.gameObject.SetActive(true);
            this.uziL.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 100f);
            this.aimWithLeftArm = true;
            this.twoHandedWeapon = false;
            this.automaticWeapon = true;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        else if ((double)w == 5.0)
        {
            this.machineGun.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 50f);
            this.fireLeftGun = false;
            this.aimWithLeftArm = false;
            this.twoHandedWeapon = true;
            this.automaticWeapon = true;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        else if ((double)w == 6.0)
        {
            this.shotgun.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 50f);
            this.fireLeftGun = false;
            this.aimWithLeftArm = false;
            this.twoHandedWeapon = true;
            this.automaticWeapon = false;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        else if ((double)w == 7.0)
        {
            this.turretGun.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 50f);
            this.fireLeftGun = false;
            this.aimWithLeftArm = false;
            this.twoHandedWeapon = true;
            this.automaticWeapon = true;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        else if ((double)w == 8.0)
        {
            this.shockRifle.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 50f);
            this.fireLeftGun = false;
            this.aimWithLeftArm = false;
            this.twoHandedWeapon = true;
            this.automaticWeapon = false;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        else if ((double)w == 9.0)
        {
            this.rifle.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 50f);
            this.fireLeftGun = false;
            this.aimWithLeftArm = false;
            this.twoHandedWeapon = true;
            this.automaticWeapon = false;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        else if ((double)w == 10.0)
        {
            this.crossbow.gameObject.SetActive(true);
            this.vHandR.SetBlendShapeWeight(0, 100f);
            this.vHandL.SetBlendShapeWeight(0, 50f);
            this.fireLeftGun = false;
            this.aimWithLeftArm = false;
            this.twoHandedWeapon = true;
            this.automaticWeapon = false;
            this.animator.SetFloat("IdleNr", 0.0f);
        }
        this.weaponAdjusted = this.root.getAdjustedWeaponNr((int)w);
        if ((double)this.weapon != (double)w)
        {
            this.hudAudioSource.clip = this.splitAimStartSound;
            this.hudAudioSource.loop = false;
            this.hudAudioSource.volume = UnityEngine.Random.Range(0.55f, 0.8f);
            this.hudAudioSource.pitch = 0.85f + Mathf.Clamp((float)(this.weaponAdjusted / 7), 0.0f, 0.35f) + UnityEngine.Random.Range(-0.05f, 0.05f);
            this.hudAudioSource.Play();
        }
        this.fireWhileReloading = false;
        this.weapon = (int)w;
        this.setGunSound();
        if ((double)this.ammo[this.weapon] <= 0.0)
            this.reload();
        if (this.isEnemy)
            return;

        PacketSender.SendPlayerWeapon();

        this.weaponHUDIcon.sprite = this.root.weaponIcons[this.weapon];
        this.updateAmmoHUD();
    }

    public virtual void setGunSound()
    {
        int index = Mathf.Clamp(this.weapon, 0, this.nrOfWeapons);

        if ((UnityEngine.Object)this.weaponSound[index] != (UnityEngine.Object)null)
        {
            this.handRAudio.clip = this.weaponSound[index];
            this.handLAudio.clip = this.weaponSound[index];
        }
        if (index != 5)
            return;
        this.handLAudio.clip = this.grenadeLauncher;
    }

    public virtual void createInitialAmmoHudBullets()
    {
        this.ammoHudBullet.gameObject.SetActive(true);
        this.ammoHudBullets = new Image[this.maxAmountOfBullets];
        for (float num = 0.0f; (double)num < (double)this.maxAmountOfBullets; ++num)
        {
            this.ammoHudBullets[(int)num] = (Image)UnityEngine.Object.Instantiate<RectTransform>(this.ammoHudBullet).GetComponent(typeof(Image));
            RectTransform component = (RectTransform)this.ammoHudBullets[(int)num].GetComponent(typeof(RectTransform));
            component.transform.SetParent(this.ammoHudBullet.transform.parent);
            component.transform.localScale = Vector3.one;
            this.ammoHudBullets[(int)num].gameObject.SetActive(false);
        }
        UnityEngine.Object.Destroy((UnityEngine.Object)this.ammoHudBullet.gameObject);
    }

    public virtual void setUpAmmoHUDBullets()
    {
        float num1 = this.ammoHudAmmoBar.sizeDelta.x / this.ammoFullClip[this.weapon];
        for (float num2 = 0.0f; (double)num2 < (double)this.maxAmountOfBullets; ++num2)
        {
            if ((double)num2 < (double)this.ammoFullClip[this.weapon])
            {
                this.ammoHudBullets[(int)num2].gameObject.SetActive(true);
                RectTransform component = (RectTransform)this.ammoHudBullets[(int)num2].GetComponent(typeof(RectTransform));
                if (this.aimWithLeftArm)
                {
                    if ((double)num2 / 2.0 == (double)Mathf.Floor(num2 / 2f))
                    {
                        float num3 = Mathf.Floor(num2 / 2f) * num1 * 2f;
                        Vector2 anchoredPosition1 = component.anchoredPosition;
                        double num4 = (double)(anchoredPosition1.x = num3);
                        Vector2 vector2_1 = component.anchoredPosition = anchoredPosition1;
                        int num5 = 2;
                        Vector2 anchoredPosition2 = component.anchoredPosition;
                        double num6 = (double)(anchoredPosition2.y = (float)num5);
                        Vector2 vector2_2 = component.anchoredPosition = anchoredPosition2;
                        float num7 = 0.75f * num1 * 2f;
                        Vector2 sizeDelta1 = component.sizeDelta;
                        double num8 = (double)(sizeDelta1.x = num7);
                        Vector2 vector2_3 = component.sizeDelta = sizeDelta1;
                        int num9 = 6;
                        Vector2 sizeDelta2 = component.sizeDelta;
                        double num10 = (double)(sizeDelta2.y = (float)num9);
                        Vector2 vector2_4 = component.sizeDelta = sizeDelta2;
                    }
                    else
                    {
                        float num3 = Mathf.Floor(num2 / 2f) * num1 * 2f;
                        Vector2 anchoredPosition1 = component.anchoredPosition;
                        double num4 = (double)(anchoredPosition1.x = num3);
                        Vector2 vector2_1 = component.anchoredPosition = anchoredPosition1;
                        int num5 = -6;
                        Vector2 anchoredPosition2 = component.anchoredPosition;
                        double num6 = (double)(anchoredPosition2.y = (float)num5);
                        Vector2 vector2_2 = component.anchoredPosition = anchoredPosition2;
                        float num7 = 0.75f * num1 * 2f;
                        Vector2 sizeDelta1 = component.sizeDelta;
                        double num8 = (double)(sizeDelta1.x = num7);
                        Vector2 vector2_3 = component.sizeDelta = sizeDelta1;
                        int num9 = 6;
                        Vector2 sizeDelta2 = component.sizeDelta;
                        double num10 = (double)(sizeDelta2.y = (float)num9);
                        Vector2 vector2_4 = component.sizeDelta = sizeDelta2;
                    }
                }
                else
                {
                    float num3 = num2 * num1;
                    Vector2 anchoredPosition1 = component.anchoredPosition;
                    double num4 = (double)(anchoredPosition1.x = num3);
                    Vector2 vector2_1 = component.anchoredPosition = anchoredPosition1;
                    int num5 = 0;
                    Vector2 anchoredPosition2 = component.anchoredPosition;
                    double num6 = (double)(anchoredPosition2.y = (float)num5);
                    Vector2 vector2_2 = component.anchoredPosition = anchoredPosition2;
                    float num7 = 0.75f * num1;
                    Vector2 sizeDelta1 = component.sizeDelta;
                    double num8 = (double)(sizeDelta1.x = num7);
                    Vector2 vector2_3 = component.sizeDelta = sizeDelta1;
                    int num9 = 10;
                    Vector2 sizeDelta2 = component.sizeDelta;
                    double num10 = (double)(sizeDelta2.y = (float)num9);
                    Vector2 vector2_4 = component.sizeDelta = sizeDelta2;
                }
            }
            else
                this.ammoHudBullets[(int)num2].gameObject.SetActive(false);
        }
        if (this.rootShared.modInfiniteAmmo || this.weapon <= 2)
        {
            this.ammoText.gameObject.SetActive(false);
            this.ammoHudInfiniteSymbol.gameObject.SetActive(true);
        }
        else
        {
            this.ammoText.gameObject.SetActive(true);
            this.ammoHudInfiniteSymbol.gameObject.SetActive(false);
        }
        this.updateAmmoHUDBullets();
    }

    public virtual void updateAmmoHUDBullets()
    {
        for (int index = 0; index < this.ammoHudBullets.Length; ++index)
        {
            float num1 = (double)this.ammo[this.weapon] <= (double)index ? 0.25f : 1f;
            Color color1 = this.ammoHudBullets[index].color;
            double num2 = (double)(color1.a = num1);
            Color color2 = this.ammoHudBullets[index].color = color1;
        }
    }

    public virtual void updateAmmoHUD()
    {
        if (this.isEnemy)
            return;
        this.ammoTotalWeaponToDisplay = this.weapon != 2 ? (this.weapon != 4 ? this.weapon : 3) : 1;
        if (this.weapon == 0 || this.weapon == 1 || this.weapon == 2)
            this.ammoText.text = "Inf";
        else
            this.ammoText.text = (Mathf.Round(this.ammo[this.weapon]) + this.ammoTotal[this.ammoTotalWeaponToDisplay]).ToString();
        if (this.usesSecondaryAmmo[this.weapon])
        {
            this.secondaryAmmoText.gameObject.SetActive(true);
            this.secondaryAmmoText.text = RuntimeServices.op_Addition(string.Empty, (object)this.secondaryAmmo[this.weapon]);
            float num1 = this.weaponPanelStartPos.y + 15f;
            Vector2 anchoredPosition = this.weaponPanel.anchoredPosition;
            double num2 = (double)(anchoredPosition.y = num1);
            Vector2 vector2 = this.weaponPanel.anchoredPosition = anchoredPosition;
        }
        else
        {
            this.secondaryAmmoText.gameObject.SetActive(false);
            float y = this.weaponPanelStartPos.y;
            Vector2 anchoredPosition = this.weaponPanel.anchoredPosition;
            double num = (double)(anchoredPosition.y = y);
            Vector2 vector2 = this.weaponPanel.anchoredPosition = anchoredPosition;
        }
        this.ammoHudAmmoAlert.SetActive((double)this.ammoTotal[this.ammoTotalWeaponToDisplay] <= 0.0);
        if ((double)this.ammoHudBullets.Length != (double)this.ammoFullClip[this.weapon])
            this.setUpAmmoHUDBullets();
        else
            this.updateAmmoHUDBullets();
    }

    public virtual void updateHealthHUD()
    {
        if (this.isEnemy)
            return;
        if (this.root.difficultyMode == 2)
        {
            float num1 = Mathf.Clamp01(this.health);
            Vector3 localScale = this.healthBar2HUDRect.localScale;
            double num2 = (double)(localScale.x = num1);
            Vector3 vector3 = this.healthBar2HUDRect.localScale = localScale;
        }
        else
        {
            float num1 = Mathf.Clamp01(this.health * 3f - 2f);
            Vector3 localScale1 = this.healthBar3HUDRect.localScale;
            double num2 = (double)(localScale1.x = num1);
            Vector3 vector3_1 = this.healthBar3HUDRect.localScale = localScale1;
            float num3 = Mathf.Clamp01(this.health * 3f - 1f);
            Vector3 localScale2 = this.healthBar2HUDRect.localScale;
            double num4 = (double)(localScale2.x = num3);
            Vector3 vector3_2 = this.healthBar2HUDRect.localScale = localScale2;
            float num5 = Mathf.Clamp01(this.health * 3f);
            Vector3 localScale3 = this.healthBar1HUDRect.localScale;
            double num6 = (double)(localScale3.x = num5);
            Vector3 vector3_3 = this.healthBar1HUDRect.localScale = localScale3;
        }
        this.healthBar1HUD.color = this.healthBar2HUD.color = this.healthBar3HUD.color = Color.Lerp(new Color(0.9f, 0.1f, 0.1f, 1f), this.healthBarStartColour, this.health * (2f * this.health));
    }

    public virtual void reload()
    {
        if (this.weapon == 1 || this.weapon == 2)
            this.ammoTotal[1] = this.ammoFullClip[2] * 2f;
        if (this.rootShared.modInfiniteAmmo)
        {
            for (int index = 0; index < this.weaponActive.Length; ++index)
                this.ammoTotal[index] = this.ammoFullClip[index] * 5f;
        }
        if (this.weapon == 7 || this.reloading || ((double)this.ammoTotal[this.weapon == 2 || this.weapon == 4 ? this.weapon - 1 : this.weapon] <= 0.0 || (double)this.ammo[this.weapon] >= (double)this.ammoFullClip[this.weapon]))
            return;
        this.reloading = true;
        this.reloadingSafteyTimer = 120f;
        if (this.weapon == 1)
            this.animator.Play("SinglePistolReload", 2, 0.0f);
        else if (this.weapon == 2 || this.weapon == 4)
            this.animator.Play("DoublePistolReload", 2, 0.0f);
        else if (this.weapon == 6)
        {
            this.allowFireWhileReloading = false;
            this.animator.Play("ShotgunReload", 2, 0.0f);
        }
        else
            this.animator.Play("SinglePistolReload", 2, 0.0f);
        if (this.weapon != 10)
            return;
        for (int index = 0; index < 4; ++index)
            this.crossbowArrows[index].SetActive(true);
    }

    public virtual void finishedReloading()
    {
        if (!this.reloading)
            return;
        this.reloading = false;
        this.flippingTable = false;
        this.reloadingSafteyTimer = 0.0f;
        this.amountToReload = this.ammoFullClip[this.weapon] - this.ammo[this.weapon];
        this.ammoTotalWeaponToUse = this.weapon != 2 ? (this.weapon != 4 ? this.weapon : 3) : 1;
        if ((double)this.amountToReload != 0.0)
        {
            if ((double)this.ammoTotal[this.ammoTotalWeaponToUse] >= (double)this.amountToReload)
            {
                this.ammoTotal[this.ammoTotalWeaponToUse] = this.ammoTotal[this.ammoTotalWeaponToUse] - this.amountToReload;
                this.ammo[this.weapon] = this.ammo[this.weapon] + this.amountToReload;
            }
            else
            {
                this.ammo[this.weapon] = this.ammo[this.weapon] + this.ammoTotal[this.ammoTotalWeaponToUse];
                this.ammoTotal[this.ammoTotalWeaponToUse] = 0.0f;
            }
            this.updateAmmoHUD();
        }
        this.fireWhileReloading = false;
    }

    public virtual void flipTable()
    {
        this.flippingTable = true;
        this.animator.Play("FlipTable", 2, 0.0f);
    }

    public virtual void finishedFlippingTable()
    {
        this.flippingTable = false;
        this.reloading = false;
    }

    public virtual void shotgunReload()
    {
        if (!this.reloading || (double)this.ammoTotal[this.weapon] <= 0.0)
            return;
        this.ammoTotal[this.weapon] = this.ammoTotal[this.weapon] - 1f;
        this.ammo[this.weapon] = this.ammo[this.weapon] + 1f;
        this.allowFireWhileReloading = false;
        this.reloadingSafteyTimer = 60f;
        if ((double)this.ammo[this.weapon] < (double)this.ammoFullClip[this.weapon] && !this.fireWhileReloading)
            this.animator.CrossFade("ShotgunReload", 0.05f, 2, 0.5f);
        this.updateAmmoHUD();
    }

    public virtual void shotgunFinishedReloading()
    {
        this.fireWhileReloading = false;
        this.reloading = false;
    }

    public virtual void emitClip(int h)
    {
        if (this.weapon == 1 || this.weapon == 3)
        {
            this.pistolClipParticle.Emit(this.root.generateEmitParams(this.handR.position, Vector3.zero, 0.2f, 2f, Color.white), 1);
        }
        else
        {
            if (this.weapon != 2 && this.weapon != 4)
                return;
            if (h == 1)
                this.pistolClipParticle.Emit(this.root.generateEmitParams(this.handR.position, Vector3.zero, 0.2f, 2f, Color.white), 1);
            else
                this.pistolClipParticle.Emit(this.root.generateEmitParams(this.handL.position, Vector3.zero, 0.2f, 2.3f, Color.white), 1);
        }
    }

    public virtual void meleeKickDetection()
    {
        if (this.startedRolling)
            return;
        this.aimSpread2 = this.root.Damp(4f, this.aimSpread2, 0.4f);
        Vector3 vector3_1 = new Vector3(this.footL.position.x, this.footL.position.y, this.transform.position.z) - new Vector3(this.transform.position.x, this.upperLegL.position.y, this.transform.position.z);
        vector3_1.z = Mathf.Sin(this.punchTimer) * 1.4f;
        RaycastHit hitInfo = new RaycastHit();
        float maxDistance = vector3_1.magnitude + 0.5f;
        if (Physics.Raycast(new Vector3(this.transform.position.x, this.upperLegL.position.y, this.transform.position.z), vector3_1.normalized, out this.punchRayHit, maxDistance, (int)this.layerMaskIncEnemiesAndEnemyGameCollision) || (UnityEngine.Object)this.punchRayHit.collider == (UnityEngine.Object)null && Physics.Raycast(new Vector3(this.transform.position.x, this.upperLegL.position.y, (double)this.transform.position.z <= 1.0 ? 2f : 0.0f), vector3_1.normalized, out hitInfo, maxDistance, (int)this.layerMaskIncEnemiesAndEnemyGameCollision))
        {
            if ((UnityEngine.Object)this.punchRayHit.collider == (UnityEngine.Object)null && (UnityEngine.Object)hitInfo.collider != (UnityEngine.Object)null)
                this.punchRayHit = hitInfo;
            this.enemyScript = (EnemyScript)this.punchRayHit.collider.GetComponentInParent(typeof(EnemyScript));
            if ((UnityEngine.Object)this.enemyScript != (UnityEngine.Object)null)
            {
                this.enemyScript.bulletHitName = this.punchRayHit.collider.name;
                this.enemyScript.bulletHitPos = this.punchRayHit.point + vector3_1.normalized * 1.75f;
                this.enemyScript.bulletHit = true;
                ++this.enemyScript.bulletStrength;
                this.enemyScript.bulletHitRot = this.transform.rotation;
                this.enemyScript.bulletHitVel = vector3_1.normalized * 20f;
                this.enemyScript.bulletHitVel.y = (double)this.enemyScript.bulletHitVel.y >= 5.0 ? this.enemyScript.bulletHitVel.y : 5f;
                this.enemyScript.allowGib = false;
                this.enemyScript.bulletTimeAlive = 0.0f;
                this.enemyScript.bulletKillOnHeadshot = true;
                this.enemyScript.shootTimer = (float)UnityEngine.Random.Range(70, 100);
                this.enemyScript.bulletHitText = RuntimeServices.op_Addition(this.root.GetTranslation("bul28"), "-");
                this.enemyScript.bulletHitExtraScore = 15f;
                this.enemyScript.knockBack((double)this.transform.position.x < (double)this.punchRayHit.collider.transform.position.x, 20f);
                if ((UnityEngine.Object)this.enemyScript.deathAudioSource != (UnityEngine.Object)null && this.enemyScript.deathAudioSource.gameObject.activeInHierarchy)
                {
                    this.enemyScript.deathAudioSource.clip = this.kickEnemyHitSound;
                    this.enemyScript.deathAudioSource.pitch = UnityEngine.Random.Range(0.975f, 1.025f);
                    this.enemyScript.deathAudioSource.volume = UnityEngine.Random.Range(0.85f, 1f);
                    this.enemyScript.deathAudioSource.Play();
                }
                this.root.rumble((double)vector3_1.x <= 0.0 ? 0 : 1, 0.6f, 0.2f);
                ++this.statsTracker.nrOfTimesEnemiesKicked;
                this.statsTracker.achievementCheck();
                if (!this.root.showNoBlood)
                {
                    this.bloodMistParticle.Emit(this.root.generateEmitParams(this.punchRayHit.point, Vector3.zero, (float)UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(0.3f, 1f), !this.root.doGore ? new Color(0.0f, 0.0f, 0.0f, 0.5f) : new Color(1f, 1f, 1f, 0.5f)), 1);
                    for (this.b = 0; this.b < 10; ++this.b)
                        this.bloodParticle.Emit(this.root.generateEmitParams(this.punchRayHit.point, new Vector3((float)(-(double)this.transform.forward.x * 3.5) + (float)UnityEngine.Random.Range(-4, 4), (float)(-(double)this.transform.forward.y * 3.5) + (float)UnityEngine.Random.Range(1, 6), UnityEngine.Random.Range(-0.5f, 0.5f)), UnityEngine.Random.Range(0.05f, 0.2f), UnityEngine.Random.Range(0.8f, 1.3f), !this.root.doGore ? new Color(0.0f, 0.0f, 0.0f, 1f) : new Color(1f, 1f, 1f, 1f)), 1);
                }
                this.cameraScript.screenShake += 0.05f;
                this.cameraScript.kickBack(0.8f);
                this.cameraScript.fakePos.z -= 0.25f;
                this.root.doMeleeHint = false;
                this.root.meleeHintCoolDown = 7200f;
                this.meleeKickHit = true;
            }
            DestructableObjectScript componentInParent1 = (DestructableObjectScript)this.punchRayHit.collider.GetComponentInParent(typeof(DestructableObjectScript));
            if ((UnityEngine.Object)componentInParent1 != (UnityEngine.Object)null && !componentInParent1.dontAllowKickDestroy)
            {
                componentInParent1.forceDoThing();
                this.cameraScript.screenShake = this.root.Damp(0.08f, this.cameraScript.screenShake, 0.4f);
                this.cameraScript.kickBack(0.2f);
                this.cameraScript.fakePos.z -= 0.04f;
                SoundScript component = (SoundScript)componentInParent1.GetComponent(typeof(SoundScript));
                if ((UnityEngine.Object)component != (UnityEngine.Object)null)
                {
                    this.playerAudioSource.clip = component.bulletImpact[UnityEngine.Random.Range(0, Extensions.get_length((System.Array)component.bulletImpact))];
                    this.playerAudioSource.loop = false;
                    this.playerAudioSource.volume = UnityEngine.Random.Range(0.65f, 0.8f);
                    this.playerAudioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                    this.playerAudioSource.Play();
                }
            }
            GlassWindowScript component1 = (GlassWindowScript)this.punchRayHit.collider.GetComponent(typeof(GlassWindowScript));
            if ((UnityEngine.Object)component1 != (UnityEngine.Object)null)
            {
                component1.breakGlass(false);
                this.root.rumble(0, 0.8f, 0.2f);
                this.root.rumble(1, 0.2f, 0.4f);
            }
            Rigidbody componentInParent2 = (Rigidbody)this.punchRayHit.collider.GetComponentInParent(typeof(Rigidbody));
            if ((UnityEngine.Object)componentInParent2 != (UnityEngine.Object)null)
            {
                float num1 = componentInParent2.velocity.x + ((double)this.transform.position.x >= (double)componentInParent2.transform.position.x ? -5f : 5f);
                Vector3 velocity = componentInParent2.velocity;
                double num2 = (double)(velocity.x = num1);
                Vector3 vector3_2 = componentInParent2.velocity = velocity;
                this.meleeKickHit = true;
            }
            if ((double)this.punchTimer < 29.0 && (UnityEngine.Object)this.punchRayHit.collider != (UnityEngine.Object)null && ((UnityEngine.Object)this.enemyScript == (UnityEngine.Object)null && (UnityEngine.Object)componentInParent1 == (UnityEngine.Object)null) && ((UnityEngine.Object)component1 == (UnityEngine.Object)null && (UnityEngine.Object)componentInParent2 == (UnityEngine.Object)null))
            {
                float num = 1f - this.punchRayHit.distance / maxDistance;
                if (((double)this.crouchAmount > 0.800000011920929 && (double)num > 0.5 || (double)this.crouchAmount <= 0.800000011920929) && (double)num > (double)this.meleeWallKickDetection)
                    this.meleeWallKickDetection = num;
            }
        }
        if ((double)this.meleeWallKickDetection <= 0.0 || (double)this.targetRotation > 25.0 && (double)this.targetRotation < 335.0)
            return;
        this.playerGraphics.localRotation = Quaternion.Slerp(this.playerGraphics.localRotation, Quaternion.Euler(0.0f, !this.faceRight ? 180f : 0.0f, 0.0f), this.meleeWallKickDetection);
    }

    public virtual void punch(int fist)
    {
        if (this.startedRolling)
            return;
        this.zOffset = fist == 1 && this.faceRight || fist == 0 && !this.faceRight ? -0.15f : 0.15f;
        if (!Physics.Raycast(new Vector3(this.neck.position.x, this.neck.position.y, this.zOffset), new Vector3(this.mousePos.x, this.mousePos.y, this.zOffset) - new Vector3(this.neck.position.x, this.neck.position.y, this.zOffset / 2f), out this.punchRayHit, 2.7f, (int)this.layerMaskOnlyEnemies))
            return;
        this.enemyScript = (EnemyScript)this.punchRayHit.collider.GetComponentInParent(typeof(EnemyScript));
        this.enemyScript.bulletHit = true;
        ++this.enemyScript.bulletStrength;
        this.enemyScript.bulletHitName = this.punchRayHit.collider.name;
        this.enemyScript.bulletHitPos = this.punchRayHit.point;
        this.enemyScript.bulletHitRot = this.transform.rotation;
        this.enemyScript.bulletHitVel = new Vector3(!this.faceRight ? -10f : 10f, 1f, 0.0f);
        this.enemyScript.allowGib = false;
        this.enemyScript.bulletTimeAlive = 999f;
        this.enemyScript.shootTimer = (float)UnityEngine.Random.Range(70, 100);
        this.enemyScript.knockBack((double)this.transform.position.x < (double)this.punchRayHit.collider.transform.position.x, 20f);
        this.punchTimer = 2f;
        this.bloodMistParticle.Emit(this.root.generateEmitParams(this.punchRayHit.point, Vector3.zero, (float)UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(0.3f, 1f), !this.root.doGore ? new Color(0.0f, 0.0f, 0.0f, 0.5f) : new Color(1f, 1f, 1f, 0.5f)), 1);
        for (this.b = 0; this.b < 10; ++this.b)
            this.bloodParticle.Emit(this.root.generateEmitParams(this.punchRayHit.point, new Vector3((float)(-(double)this.transform.forward.x * 3.5) + (float)UnityEngine.Random.Range(-4, 4), (float)(-(double)this.transform.forward.y * 3.5) + (float)UnityEngine.Random.Range(1, 6), UnityEngine.Random.Range(-0.5f, 0.5f)), UnityEngine.Random.Range(0.05f, 0.2f), UnityEngine.Random.Range(0.8f, 1.3f), !this.root.doGore ? new Color(0.0f, 0.0f, 0.0f, 1f) : new Color(1f, 1f, 1f, 1f)), 1);
    }

    public virtual void wallJump(EnemyScript eScript)
    {
        if ((double)this.dontAllowWallJumpTimer > 0.0)
            return;
        this.wallJumpTimer = 1f;
        this.animator.Play("InAir", -1, 0.0f);
        this.xSpeed = Mathf.Sin((float)(-(double)this.transform.up.x * 1.57079637050629)) * -9.5f;
        this.ySpeed = (float)(-(double)Mathf.Cos((float)(-(double)this.transform.up.x * 1.57079637050629)) * ((double)this.transform.up.y <= 0.0 ? 13.0 : -13.0));
        this.ySpeed += 1.8f;
        if ((double)this.rootShared.modPlayerSize != 100.0)
            this.ySpeed += (float)((1.0 - (double)this.rootShared.modPlayerSize / 100.0) * 0.800000011920929);
        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(this.transform.up.x * 3f, this.transform.up.y, this.transform.up.z).normalized * 5f);
        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(this.transform.up.x * 3f, this.transform.up.y + 0.3f, this.transform.up.z).normalized * 5f);
        this.rotationSpeed = !Physics.Raycast(this.transform.position, new Vector3(this.transform.up.x * 3f, this.transform.up.y, this.transform.up.z), 5f, (int)this.layerMask) || !this.justWallJumped && !Physics.Raycast(this.transform.position, new Vector3(this.transform.up.x * 3f, this.transform.up.y + 0.3f, this.transform.up.z), 5f, (int)this.layerMaskIncEnemiesAndEnemyGameCollision) ? ((double)this.xSpeed <= 0.0 ? 4.7f : -4.7f) : ((double)this.xSpeed <= 0.0 ? -2f : 2f);
        this.playerJumpAudio.clip = this.playerWallJumpSound[UnityEngine.Random.Range(0, Extensions.get_length((System.Array)this.playerWallJumpSound))];
        this.playerJumpAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        this.playerJumpAudio.volume = UnityEngine.Random.Range(0.9f, 1f);
        this.playerJumpAudio.Play();
        this.root.rumble(0, 0.03f, 0.05f);
        this.root.rumble(1, 0.04f, 0.03f);
        this.jumpedFromSwinging = false;
        this.justWallJumped = true;
        this.disableFloatJump = false;
        if (!((UnityEngine.Object)eScript != (UnityEngine.Object)null))
            return;
        eScript.knockBack((double)this.xSpeed < 0.0, 60f);
        this.root.rumble(0, 0.04f, 0.07f);
        this.root.rumble(1, 0.06f, 0.04f);
        this.justJumpedOffEnemy = true;
    }

    public virtual void giveFullAmmo()
    {
        for (int index = 1; index < this.root.nrOfWeapons; ++index)
        {
            this.weaponActive[index] = true;
            this.ammo[index] = this.root.ammoFullClip[index];
            this.ammoTotal[index] = this.root.ammoMax[index];
        }
        this.weaponActive[0] = this.weaponActive[10] = this.weaponActive[8] = this.weaponActive[7] = false;
        this.updateAmmoHUD();
    }

    private void newWeaponPrompt(float w)
    {
        if (this.rootShared.hideHUD)
            return;
        this.newWeaponPromptScript.gameObject.SetActive(true);
        this.newWeaponPromptScript.weaponNr = (int)w;
        this.newWeaponPromptScript.animTimer = 0.0f;
    }

    public virtual void pickedUpWeapon(float w)
    {
        this.weaponActive[0] = false;
        float num = -999f;
        if (!this.weaponActive[(int)w])
        {
            this.newWeaponPrompt(w);
            this.weaponActive[(int)w] = true;
            this.ammo[(int)w] = this.ammoFullClip[(int)w];
            this.ammoTotal[(int)w] = this.ammoTotal[(int)w] + this.ammoFullClip[(int)w] * 6f;
            this.changeWeapon(w);
            this.uiWeaponSelectorScript.prepareUI();
            if ((double)w == 5.0)
                this.secondaryAmmo[(int)w] = 2f;
            this.playerAudioSource.volume = 1f;
            this.playerAudioSource.pitch = UnityEngine.Random.Range(0.85f, 0.9f);
        }
        else
        {
            this.playerAudioSource.volume = 1f;
            this.playerAudioSource.pitch = UnityEngine.Random.Range(0.85f, 0.9f);
            if ((double)w == 1.0 && !this.weaponActive[2])
            {
                this.newWeaponPrompt(2f);
                this.weaponActive[2] = true;
                this.ammo[2] = this.ammoFullClip[2];
                this.changeWeapon(2f);
                this.uiWeaponSelectorScript.prepareUI();
            }
            else if ((double)w == 3.0 && !this.weaponActive[4])
            {
                this.newWeaponPrompt(4f);
                this.weaponActive[4] = true;
                this.ammo[4] = this.ammoFullClip[4];
                this.changeWeapon(4f);
                this.uiWeaponSelectorScript.prepareUI();
            }
            else
            {
                num = this.ammoTotal[(int)w];
                this.ammoTotal[(int)w] = this.ammoTotal[(int)w] + this.ammoFullClip[(int)w];
                this.playerAudioSource.volume = UnityEngine.Random.Range(0.45f, 0.6f);
                this.playerAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.3f);
            }
            this.setGunSound();
        }
        if ((double)w == 5.0)
            this.secondaryAmmo[(int)w] = Mathf.Clamp(this.secondaryAmmo[(int)w] + 1f, 0.0f, 5f);
        this.ammoTotal[(int)w] = Mathf.Clamp(this.ammoTotal[(int)w], 0.0f, this.root.ammoMax[(int)w]);
        if ((double)num != -999.0)
            this.root.doPickUpNotification((int)w, this.ammoTotal[(int)w] - num, false);
        if (this.playerAudioSource.isPlaying && (UnityEngine.Object)this.playerAudioSource.clip != (UnityEngine.Object)this.healthPickUpSound || !this.playerAudioSource.isPlaying)
        {
            this.playerAudioSource.clip = this.weaponPickUpSound;
            this.playerAudioSource.Play();
        }
        this.updateAmmoHUD();
    }

    public virtual void allowKickItem(Rigidbody kickRBody, float extraGravity, float kickRange, Vector3 centerOffset, PhysicsSoundsScript physicsSoundsScript, ObjectKickScript objectKickScript)
    {
        if (this.kickTimer <= (float)5 && (!this.startedRolling || !this.onGround))
        {
            Vector3 vector = this.transform.InverseTransformPoint(kickRBody.transform.position);
            Vector3 vector2 = this.transform.InverseTransformPoint(this.mousePos);
            Vector3 vector3 = kickRBody.transform.TransformPoint(centerOffset);
            if (Mathf.Abs(vector.x) < kickRange && !Physics.Linecast(this.transform.position, vector3, this.layerMask))
            {
                this.root.highlightObject(kickRBody.transform, true, this.root.GetTranslation("interact1"), 1.3f);
                this.root.showHintKick = true;
                Vector3 normalized = (this.mousePos - vector3).normalized;
                Vector3 vector4 = (float)20 * normalized;

                if (this.outlineEffect.outlineRenderers[0] != null)
                    this.trajectory.position = this.outlineEffect.outlineRenderers[0].transform.TransformPoint(centerOffset);

                Transform transform = null;
                float num = (float)999;
                EnemyScript autoTargetEnemyScript = null;
                int i = 0;
                Transform[] allEnemies = this.root.allEnemies;
                int length = allEnemies.Length;
                while (i < length)
                {
                    Transform transform2 = allEnemies[i].Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Neck/Head");
                    float num2 = Vector2.Distance(vector3, transform2.position);
                    if (num2 < (float)8 && Vector2.Distance(vector3 + normalized * num2, transform2.position) < (float)4)
                    {
                        EnemyScript enemyScript = (EnemyScript)allEnemies[i].GetComponent(typeof(EnemyScript));
                        if (enemyScript.enabled && !enemyScript.doorSpawn && num2 < num && !Physics.Linecast(vector3, transform2.position, this.layerMask))
                        {
                            num = num2;
                            transform = transform2;
                            autoTargetEnemyScript = enemyScript;
                        }
                    }
                    i++;
                }
                if (transform != null)
                {
                    Vector2 a = (transform.position - vector3).normalized;
                    this.trajectory.rotation = Quaternion.Euler(-Mathf.Atan2(a.y, a.x) * 57.29578f + (float)90, (float)90, (float)-90);
                    int num3 = 0;
                    Vector3 localScale = this.trajBone[1].localScale;
                    float num4 = localScale.x = (float)num3;
                    Vector3 vector5 = this.trajBone[1].localScale = localScale;
                    float x = Vector2.Distance(transform.position, vector3);
                    Vector3 localScale2 = this.trajBone[0].localScale;
                    float num5 = localScale2.x = x;
                    Vector3 vector6 = this.trajBone[0].localScale = localScale2;
                    vector4 = a * (float)20;
                }
                else
                {
                    this.trajectory.rotation = Quaternion.Euler(-Mathf.Atan2(vector4.y, vector4.x) * 57.29578f + (float)90, (float)90, (float)-90);
                    bool flag = default(bool);
                    Vector2 v = vector4;
                    int num6 = 1;
                    Vector3 localScale3 = this.trajBone[0].localScale;
                    float num7 = localScale3.x = (float)num6;
                    Vector3 vector7 = this.trajBone[0].localScale = localScale3;
                    for (float num8 = (float)0; num8 < (float)Extensions.get_length(this.trajBone); num8 += (float)1)
                    {
                        if (num8 > (float)0)
                        {
                            this.trajBone[(int)num8].rotation = Quaternion.LookRotation(v, Vector3.forward) * Quaternion.Euler((float)90, (float)0, (float)-90);
                            v.y -= 0.5f - extraGravity * Physics.gravity.normalized.y / (float)8 * 0.8f + kickRBody.drag * (num8 / (float)Extensions.get_length(this.trajBone)) * (float)3;
                        }
                        if (!flag)
                        {
                            int num9 = 1;
                            Vector3 localScale4 = this.trajBone[(int)num8].localScale;
                            float num10 = localScale4.x = (float)num9;
                            Vector3 vector8 = this.trajBone[(int)num8].localScale = localScale4;
                            if (num8 >= (float)7)
                            {
                                float x2 = (float)1 - (num8 - (float)7) / 9.5f * ((float)1 - Mathf.Clamp01(Mathf.Abs(normalized.x) * 1.6f));
                                Vector3 localScale5 = this.trajBone[(int)num8].localScale;
                                float num11 = localScale5.x = x2;
                                Vector3 vector9 = this.trajBone[(int)num8].localScale = localScale5;
                            }
                            RaycastHit raycastHit = default(RaycastHit);
                            if (Physics.Raycast(this.trajBone[(int)num8].position, v, out raycastHit, this.trajBone[(int)num8].localScale.x, this.layerMaskIncEnemiesAndEnemyGameCollision))
                            {
                                flag = true;
                                float distance = raycastHit.distance;
                                Vector3 localScale6 = this.trajBone[(int)num8].localScale;
                                float num12 = localScale6.x = distance;
                                Vector3 vector10 = this.trajBone[(int)num8].localScale = localScale6;
                            }
                        }
                        else
                        {
                            int num13 = 0;
                            Vector3 localScale7 = this.trajBone[(int)num8].localScale;
                            float num14 = localScale7.x = (float)num13;
                            Vector3 vector11 = this.trajBone[(int)num8].localScale = localScale7;
                        }
                    }
                }
                if ((this.kPunch || this.punchTimer > (float)10) && this.itemKickCoolDown <= (float)0 && (this.root.prevOutlinedObject == kickRBody.transform || this.root.showHintPressButton))
                {


                    PacketSender.SendPlayerKickObject(vector4, (this.footL.position - kickRBody.transform.position), objectKickScript.networkHelper.entityIdentifier);

                    MFPEditorUtils.Log("Kick sent");

                    /*    kickRBody.isKinematic = false;
                        kickRBody.velocity = vector4;
                        kickRBody.angularVelocity = kickRBody.velocity;
                        */




                    /*  if (transform != null)
                      {
                          ObjectKickScript objectKickScript2 = (ObjectKickScript)kickRBody.GetComponent(typeof(ObjectKickScript));
                          objectKickScript2.autoTargetTransform = transform;
                          objectKickScript2.autoTargetEnemyScript = autoTargetEnemyScript;
                      }
                      */
                    this.itemKickCoolDown = (float)20;
                    /*    kickRBody.transform.position = kickRBody.transform.position + (this.footL.position - kickRBody.transform.position) * 0.4f;
                        objectKickScript.kickJuggleAmount++;
                        */
                    if (objectKickScript.objType == 4 && objectKickScript.kickJuggleAmount > this.statsTracker.highestNrOfBasketballJuggles)
                    {
                        this.statsTracker.highestNrOfBasketballJuggles = objectKickScript.kickJuggleAmount;
                        this.statsTracker.achievementCheck();
                    }
                    /*     if (physicsSoundsScript != null)
                         {
                             physicsSoundsScript.triggerCollisionSound((float)3, true, 0.3f);
                         }
                         */
                }
            }
        }
    }

    public virtual void enableGamepad()
    {
        this.inputPText = "P1";
        if ((!this.root.sbClickCont || this.root.sbClickContDontFreeze) && (!this.overrideControls && (double)this.disableInputTimer <= 0.0))
            this.aimHelper.gameObject.SetActive(true);
        this.gamepad = true;
        this.root.useGamepadIcons = true;
        this.root.setUpHintText();
        this.root.SetCursorState();
        if (this.gamepadMode == 5)
            this.mainCursor.localScale = Vector3.one * 0.6f;
        this.rStick2TargetAngle = -90f;
        this.rStick2TargetQuaternion = this.rStick2Quaternion = Quaternion.Euler(0.0f, 0.0f, this.rStick2TargetAngle);
        this.gamepadAimOffset = this.targetGamepadAimOffset = (Vector2)(Vector3.right * 3f);
        this.fakeGamepadMousePos = this.transform.position + (Vector3)this.gamepadAimOffset;
        if (this.inputHelperScript.checkForMissingInput(false))
            this.player.controllers.maps.LoadDefaultMaps(ControllerType.Joystick);
        PlatformPlayerPrefs.SetInt("gamepad", 1);
    }

    public virtual void disableGamepad()
    {
        this.inputPText = string.Empty;
        this.aimHelper.gameObject.SetActive(false);
        int num1 = 0;
        Quaternion rotation = this.mainCursor.rotation;
        Vector3 eulerAngles = rotation.eulerAngles;
        double num2 = (double)(eulerAngles.z = (float)num1);
        Vector3 vector3 = rotation.eulerAngles = eulerAngles;
        Quaternion quaternion = this.mainCursor.rotation = rotation;
        this.gamepad = false;
        this.root.useGamepadIcons = false;
        this.mainCursorImage.color = this.mainCursorImageStartColour;
        this.root.setUpHintText();
        this.root.SetCursorState();
        this.mainCursor.localScale = Vector3.one;
        PlatformPlayerPrefs.SetInt("gamepad", 0);
    }

    public virtual Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = this.curCamera.ScreenPointToRay(screenPosition);
        Plane plane = new Plane(Vector3.forward, new Vector3(0.0f, 0.0f, z));
        float enter = new float();
        plane.Raycast(ray, out enter);
        return ray.GetPoint(enter);
    }

    public virtual void doPlayerJumpSound()
    {
        this.playerJumpAudio.clip = this.playerJumpSound[UnityEngine.Random.Range(0, Extensions.get_length((System.Array)this.playerJumpSound))];
        this.playerJumpAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        this.playerJumpAudio.volume = UnityEngine.Random.Range(0.9f, 1f);
        this.playerJumpAudio.Play();
    }
}
