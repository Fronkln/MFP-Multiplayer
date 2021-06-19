using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using I2.Loc;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityScript.Lang;
using UnityStandardAssets.ImageEffects;

// Token: 0x020000A8 RID: 168
[Serializable]
public class RootScript : MonoBehaviour
{

    public BaseNetworkEntity networkHelper;
    public bool doMultiplayerClickToContinueOnce = false;

    private RootSharedScript rootShared;
    private StatsTrackerScript statsTracker;
    private PostProcessingProfile defaultPostProcessing;
    public PostProcessingProfile lowPostProcessing;
    [HideInInspector]
    public bool playerFiredWeapon;
    public bool cinematicShot;
    public bool trailerMode;
    public float blackFadeExtraTimer;
    public float blackFadeSpeed;
    public bool dontAllowCheckpointSave;
    public bool dontShowPedroAtEndScreen;
    public int weaponToUseWhenLoadingFromLvlSelectScreen;
    public int theme;
    public float maxScoreReference;
    public bool disablePedroHintsForThisLevel;
    public float difficulty;
    public int difficultyMode;
    public bool isTutorialLevel;
    public bool dontAllowActionMode;
    public bool dontAllowAutomaticFallDeath;

    // Token: 0x04000DF9 RID: 3577
    public bool isAlarmLevel;

    // Token: 0x04000DFA RID: 3578
    [HideInInspector]
    public bool hasTriggeredAlarm;

    // Token: 0x04000DFB RID: 3579
    public bool disableShooting;

    // Token: 0x04000DFC RID: 3580
    public bool multiplayer;

    // Token: 0x04000DFD RID: 3581
    public string[] inputTextExtension;

    // Token: 0x04000DFE RID: 3582
    public bool[] inputIsGamepad;

    // Token: 0x04000DFF RID: 3583
    [HideInInspector]
    public bool useGamepadIcons;

    // Token: 0x04000E00 RID: 3584
    [HideInInspector]
    public bool updateInputIcons;

    // Token: 0x04000E01 RID: 3585
    [HideInInspector]
    public int collectablesCollected;

    // Token: 0x04000E02 RID: 3586
    [HideInInspector]
    public int collectablesTotal;

    // Token: 0x04000E03 RID: 3587
    public Vector3 setGravity;

    // Token: 0x04000E04 RID: 3588
    public bool allowDebug;

    // Token: 0x04000E05 RID: 3589
    public bool demoLevel;

    // Token: 0x04000E06 RID: 3590
    public bool fadeToBlack;

    // Token: 0x04000E07 RID: 3591
    private bool levelEndedVar;

    // Token: 0x04000E08 RID: 3592
    private bool levelEndedDoOnce;

    // Token: 0x04000E09 RID: 3593
    public int levelToLoad;

    // Token: 0x04000E0A RID: 3594
    private bool fadeToBlackDoOnce;

    // Token: 0x04000E0B RID: 3595
    private Image blackFade;

    // Token: 0x04000E0C RID: 3596
    [Header("XB Gamepad Icons")]
    public GameObject xbA;

    // Token: 0x04000E0D RID: 3597
    public GameObject xbB;

    // Token: 0x04000E0E RID: 3598
    public GameObject xbBack;

    // Token: 0x04000E0F RID: 3599
    public GameObject xbDPad;

    // Token: 0x04000E10 RID: 3600
    public GameObject xbDPadDown;

    // Token: 0x04000E11 RID: 3601
    public GameObject xbDPadLeft;

    // Token: 0x04000E12 RID: 3602
    public GameObject xbDPadRight;

    // Token: 0x04000E13 RID: 3603
    public GameObject xbDPadUp;

    // Token: 0x04000E14 RID: 3604
    public GameObject xbLB;

    // Token: 0x04000E15 RID: 3605
    public GameObject xbLStick;

    // Token: 0x04000E16 RID: 3606
    public GameObject xbLStickClick;

    // Token: 0x04000E17 RID: 3607
    public GameObject xbLStickUp;

    // Token: 0x04000E18 RID: 3608
    public GameObject xbLStickDown;

    // Token: 0x04000E19 RID: 3609
    public GameObject xbLStickLeft;

    // Token: 0x04000E1A RID: 3610
    public GameObject xbLStickRight;

    // Token: 0x04000E1B RID: 3611
    public GameObject xbLT;

    // Token: 0x04000E1C RID: 3612
    public GameObject xbRB;

    // Token: 0x04000E1D RID: 3613
    public GameObject xbRStick;

    // Token: 0x04000E1E RID: 3614
    public GameObject xbRStickClick;

    // Token: 0x04000E1F RID: 3615
    public GameObject xbRT;

    // Token: 0x04000E20 RID: 3616
    public GameObject xbStart;

    // Token: 0x04000E21 RID: 3617
    public GameObject xbX;

    // Token: 0x04000E22 RID: 3618
    public GameObject xbY;

    // Token: 0x04000E23 RID: 3619
    [Header("Prefabs")]
    public GameObject bullet;

    // Token: 0x04000E24 RID: 3620
    private int curBullet;

    // Token: 0x04000E25 RID: 3621
    private GameObject[] bullets;

    // Token: 0x04000E26 RID: 3622
    private BulletScript[] bulletScripts;

    // Token: 0x04000E27 RID: 3623
    public GameObject explosion;

    // Token: 0x04000E28 RID: 3624
    private int curExplosion;

    // Token: 0x04000E29 RID: 3625
    private GameObject[] explosions;

    // Token: 0x04000E2A RID: 3626
    private ExplosionVisualScript[] explosionScripts;

    // Token: 0x04000E2B RID: 3627
    private Rigidbody[] explosionRigidbodies;

    // Token: 0x04000E2C RID: 3628
    private SphereCollider[] explosionSphereColliders;

    // Token: 0x04000E2D RID: 3629
    public GameObject hintTextPrefab;

    // Token: 0x04000E2E RID: 3630
    public GameObject keyHintPrefab;

    // Token: 0x04000E2F RID: 3631
    public GameObject mouseSymbolPrefab;

    // Token: 0x04000E30 RID: 3632
    public GameObject mouseSymbolLeftClickPrefab;

    // Token: 0x04000E31 RID: 3633
    public GameObject mouseSymbolRightClickPrefab;

    // Token: 0x04000E32 RID: 3634
    private GameObject hudCanvas;

    // Token: 0x04000E33 RID: 3635
    private RectTransform hudCanvasRect;

    // Token: 0x04000E34 RID: 3636
    private GameObject pickupNotificationHolder;

    // Token: 0x04000E35 RID: 3637
    public Sprite[] pedroExpressions;

    // Token: 0x04000E36 RID: 3638
    public GameObject[] muzzleFlashes;

    // Token: 0x04000E37 RID: 3639
    private MuzzleFlashMainScript[] mfPistol;

    // Token: 0x04000E38 RID: 3640
    private MuzzleFlashMainScript[] mfUzi;

    // Token: 0x04000E39 RID: 3641
    private MuzzleFlashMainScript[] mfShotgun;

    // Token: 0x04000E3A RID: 3642
    private MuzzleFlashMainScript[] mfAssaultRifle;

    // Token: 0x04000E3B RID: 3643
    private MuzzleFlashMainScript[] mfSniper;

    // Token: 0x04000E3C RID: 3644
    private int curMfPistol;

    // Token: 0x04000E3D RID: 3645
    private int curMfUzi;

    // Token: 0x04000E3E RID: 3646
    private int curMfShotgun;

    // Token: 0x04000E3F RID: 3647
    private int curMfAssaultRifle;

    // Token: 0x04000E40 RID: 3648
    private int curMfSniper;

    // Token: 0x04000E41 RID: 3649
    [Header("Audio stuff")]
    public bool dontInitializeMusicAtStart;

    // Token: 0x04000E42 RID: 3650
    public bool loopMusicIntro;

    // Token: 0x04000E43 RID: 3651
    public AudioClip musicIntro;

    // Token: 0x04000E44 RID: 3652
    public AudioClip musicLoop;

    // Token: 0x04000E45 RID: 3653
    private AudioSource musicAudioSource;

    // Token: 0x04000E46 RID: 3654
    private AudioSource musicAudioSource2;

    // Token: 0x04000E47 RID: 3655
    public AudioMixer audioMixer;

    // Token: 0x04000E48 RID: 3656
    public AudioMixerSnapshot normalStateAudioSnapshot;

    // Token: 0x04000E49 RID: 3657
    public AudioMixerSnapshot motorcycleNormalStateAudioSnapshot;

    // Token: 0x04000E4A RID: 3658
    public AudioMixerSnapshot actionStateAudioSnapshot;

    // Token: 0x04000E4B RID: 3659
    public AudioMixerSnapshot dodgeStateAudioSnapshot;

    // Token: 0x04000E4C RID: 3660
    public AudioMixerSnapshot deadStateAudioSnapshot;

    // Token: 0x04000E4D RID: 3661
    public AudioMixerSnapshot levelCompleteStateAudioSnapshot;

    // Token: 0x04000E4E RID: 3662
    public AudioMixerSnapshot weaponSelectStateAudioSnapshot;

    // Token: 0x04000E4F RID: 3663
    public AudioMixerSnapshot introMusicLoopAudioSnapshot;

    // Token: 0x04000E50 RID: 3664
    public AudioMixerSnapshot pauseAudioSnapshot;

    // Token: 0x04000E51 RID: 3665
    public AudioMixerSnapshot pauseAudioSnapshotNoPitchChange;

    // Token: 0x04000E52 RID: 3666
    public int lastActivatedAudioSnapshot;

    // Token: 0x04000E53 RID: 3667
    private bool slowMoAudioEffectDoOnce;

    // Token: 0x04000E54 RID: 3668
    public bool fadeInSound;

    // Token: 0x04000E55 RID: 3669
    public bool fadeOutSound;

    // Token: 0x04000E56 RID: 3670
    public bool finishedPlayingIntro;

    // Token: 0x04000E57 RID: 3671
    public float earRingingTimer;

    // Token: 0x04000E58 RID: 3672
    private AudioSource earRingingSound;

    // Token: 0x04000E59 RID: 3673
    public AudioClip pedroHintSound;

    // Token: 0x04000E5A RID: 3674
    public AudioClip scoreGetSound;

    // Token: 0x04000E5B RID: 3675
    public AudioClip scoreCountingSound;

    // Token: 0x04000E5C RID: 3676
    public AudioClip multiplierFinishedSound;

    // Token: 0x04000E5D RID: 3677
    public AudioClip weaponSelectAppearSound;

    // Token: 0x04000E5E RID: 3678
    private AudioSource hudAudio;

    // Token: 0x04000E5F RID: 3679
    private AudioSource hudAudio2;

    // Token: 0x04000E60 RID: 3680
    private AudioSource multiplierAudio;

    // Token: 0x04000E61 RID: 3681
    public bool doScoreCountingSound;

    // Token: 0x04000E62 RID: 3682
    [Header("Level specific tweaks")]
    public bool loadStateOnStart;

    // Token: 0x04000E63 RID: 3683
    public int[] startingWeapons;

    // Token: 0x04000E64 RID: 3684
    public bool neverDoDodgeHelper;

    // Token: 0x04000E65 RID: 3685
    public bool alwaysDoDodgeHelper;

    // Token: 0x04000E66 RID: 3686
    [Header("Mini level goal")]
    public bool killEveryone;

    // Token: 0x04000E67 RID: 3687
    [HideInInspector]
    public int nrOfEnemiesTotal;

    // Token: 0x04000E68 RID: 3688
    [HideInInspector]
    public int potentialMultipliersFromEnemies;

    // Token: 0x04000E69 RID: 3689
    [HideInInspector]
    public int nrOfEnemiesKilled;

    // Token: 0x04000E6A RID: 3690
    private int prevNrOfEnemiesKilled;

    // Token: 0x04000E6B RID: 3691
    [HideInInspector]
    public bool isMiniLevel;

    // Token: 0x04000E6C RID: 3692
    private Text miniLevelGoalText;

    // Token: 0x04000E6D RID: 3693
    private bool miniLevelSuccess;

    // Token: 0x04000E6E RID: 3694
    private Text miniLevelCurTimeUIText;

    // Token: 0x04000E6F RID: 3695
    private Text miniLevelBestTimeUIText;

    // Token: 0x04000E70 RID: 3696
    [HideInInspector]
    public int nrOfDeaths;

    // Token: 0x04000E71 RID: 3697
    [Header("This stuff probably shouldnt be changed manually...")]
    public Sprite[] weaponIcons;

    // Token: 0x04000E72 RID: 3698
    public Sprite healthIcon;

    // Token: 0x04000E73 RID: 3699
    public bool doCheckpointSaveExternalTrigger;

    // Token: 0x04000E74 RID: 3700
    public bool doCheckpointSave;

    // Token: 0x04000E75 RID: 3701
    public bool forceCheckpointSave;

    // Token: 0x04000E76 RID: 3702
    public bool doCheckpointLoad;

    // Token: 0x04000E77 RID: 3703
    private int extraCheckpointLoad;

    // Token: 0x04000E78 RID: 3704
    public float unityTimescale;

    // Token: 0x04000E79 RID: 3705
    public float targetUnityTimescale;

    // Token: 0x04000E7A RID: 3706
    public float prevUnityTimescale;

    // Token: 0x04000E7B RID: 3707
    public float deltaUnityTimescale;

    // Token: 0x04000E7C RID: 3708
    private float startUnityTimeScale;

    // Token: 0x04000E7D RID: 3709
    private float startUnityFixedDeltaTime;

    // Token: 0x04000E7E RID: 3710
    private float startUnityMaximumDeltaTime;

    // Token: 0x04000E7F RID: 3711
    private float bulletHitFreezeTimer;

    // Token: 0x04000E80 RID: 3712
    public float timescale;

    // Token: 0x04000E81 RID: 3713
    public float fixedTimescale;

    // Token: 0x04000E82 RID: 3714
    public float timescaleRaw;

    // Token: 0x04000E83 RID: 3715
    public float unscaledTimescale;

    // Token: 0x04000E84 RID: 3716
    public bool kAction;

    // Token: 0x04000E85 RID: 3717
    private bool kActionDoOnce;

    // Token: 0x04000E86 RID: 3718
    public float framesSinceBloodSplatter;

    // Token: 0x04000E87 RID: 3719
    public bool dead;

    // Token: 0x04000E88 RID: 3720
    private bool deadDoOnce;

    // Token: 0x04000E89 RID: 3721
    public bool paused;

    // Token: 0x04000E8A RID: 3722
    private bool pausedDoOnce;

    // Token: 0x04000E8B RID: 3723
    private RectTransform optionsMenu;

    // Token: 0x04000E8C RID: 3724
    public OptionsMenuScript optionsMenuScript;

    // Token: 0x04000E8D RID: 3725
    private RectTransform levelEndScreen;

    // Token: 0x04000E8E RID: 3726
    private ParticleSystem bulletHitParticle;

    // Token: 0x04000E8F RID: 3727
    private ParticleSystem sparksParticle;

    // Token: 0x04000E90 RID: 3728
    private ParticleSystem bloodMistParticle;

    // Token: 0x04000E91 RID: 3729
    private ParticleSystem bloodDropsParticle;

    // Token: 0x04000E92 RID: 3730
    private ParticleSystem pistolClipParticle;

    // Token: 0x04000E93 RID: 3731
    private ParticleSystem glassParticle;

    // Token: 0x04000E94 RID: 3732
    private ParticleSystem destroyParticleGlass;

    // Token: 0x04000E95 RID: 3733
    private ParticleSystem debrisParticle;

    // Token: 0x04000E96 RID: 3734
    private ParticleSystem shellParticle;

    // Token: 0x04000E97 RID: 3735
    private ParticleSystem smokeParticle;

    // Token: 0x04000E98 RID: 3736
    private ParticleSystem smokeSpriteParticle;

    // Token: 0x04000E99 RID: 3737
    private ParticleSystem rockParticle;

    // Token: 0x04000E9A RID: 3738
    public bool actionModeActivated;

    // Token: 0x04000E9B RID: 3739
    private bool actionModeActivatedDoOnce;

    // Token: 0x04000E9C RID: 3740
    private bool actionModeActivatedDoOnce2;

    // Token: 0x04000E9D RID: 3741
    private float timeInActionMode;

    // Token: 0x04000E9E RID: 3742
    private Camera mainCamera;

    // Token: 0x04000E9F RID: 3743
    private PostProcessingBehaviour postProcessingBehaviour;

    // Token: 0x04000EA0 RID: 3744
    private CameraScript cameraScript;

    // Token: 0x04000EA1 RID: 3745
    private Record record;

    // Token: 0x04000EA2 RID: 3746
    private ColorCorrectionCurves camColourCorrection;

    // Token: 0x04000EA3 RID: 3747
    private VignetteAndChromaticAberration camVignette;

    // Token: 0x04000EA4 RID: 3748
    public float camTransitionValue;

    // Token: 0x04000EA5 RID: 3749
    private bool camTransitionDoOnce;

    // Token: 0x04000EA6 RID: 3750
    private OutlineEffect outlineEffect;

    // Token: 0x04000EA7 RID: 3751
    private bool disableOutlineEffect;

    // Token: 0x04000EA8 RID: 3752
    public Transform prevOutlinedObject;

    // Token: 0x04000EA9 RID: 3753
    private GameObject trajectory;

    // Token: 0x04000EAA RID: 3754
    public Transform[] allEnemies;

    // Token: 0x04000EAB RID: 3755
    public AutoAimTargetScript[] allAutoAimTargets;

    // Token: 0x04000EAC RID: 3756
    private PlayerScript playerScript;

    // Token: 0x04000EAD RID: 3757
    public bool autoSwing;

    // Token: 0x04000EAE RID: 3758
    private RectTransform sbBubble;

    // Token: 0x04000EAF RID: 3759
    private RectTransform sbTail;

    // Token: 0x04000EB0 RID: 3760
    private Text sbText;

    // Token: 0x04000EB1 RID: 3761
    private Text sbName;

    // Token: 0x04000EB2 RID: 3762
    private RectTransform sbNameHolder;

    // Token: 0x04000EB3 RID: 3763
    private RectTransform sbClickIndicator;

    // Token: 0x04000EB4 RID: 3764
    private RectTransform sbClickIndicatorKeyboard;

    // Token: 0x04000EB5 RID: 3765
    private RectTransform sbClickIndicatorGamepad;

    // Token: 0x04000EB6 RID: 3766
    public Transform sbTransform;

    // Token: 0x04000EB7 RID: 3767
    public Vector3 sbOffset;

    // Token: 0x04000EB8 RID: 3768
    public float sbTimer;

    // Token: 0x04000EB9 RID: 3769
    private float sbTimerMultiplier;

    // Token: 0x04000EBA RID: 3770
    public bool sbClickCont;

    // Token: 0x04000EBB RID: 3771
    public bool sbClickContDontFreeze;

    // Token: 0x04000EBC RID: 3772
    public float sbAppearDelay;

    // Token: 0x04000EBD RID: 3773
    private bool sbDoOnce;

    // Token: 0x04000EBE RID: 3774
    private string[] sbStringArray;

    // Token: 0x04000EBF RID: 3775
    private int sbCurStringInArray;

    // Token: 0x04000EC0 RID: 3776
    public Transform sbTriggerTransform;

    // Token: 0x04000EC1 RID: 3777
    private Animator sbAnimator;

    // Token: 0x04000EC2 RID: 3778
    private float sbShake;

    // Token: 0x04000EC3 RID: 3779
    private VoiceControllerScript voiceController;

    // Token: 0x04000EC4 RID: 3780
    private AudioSource voice;

    // Token: 0x04000EC5 RID: 3781
    private float voiceTimer;

    // Token: 0x04000EC6 RID: 3782
    private float voiceTargetPitch;

    // Token: 0x04000EC7 RID: 3783
    private float voiceTargetVolume;

    // Token: 0x04000EC8 RID: 3784
    private float hintTextTimer;

    // Token: 0x04000EC9 RID: 3785
    public int hintTextID;

    // Token: 0x04000ECA RID: 3786
    public float musicIntenseFactor;

    // Token: 0x04000ECB RID: 3787
    [HideInInspector]
    public float[] ammoFullClip;

    // Token: 0x04000ECC RID: 3788
    [HideInInspector]
    public float[] ammoMax;

    // Token: 0x04000ECD RID: 3789
    [HideInInspector]
    public int nrOfWeapons;

    // Token: 0x04000ECE RID: 3790
    public float startTime;

    // Token: 0x04000ECF RID: 3791
    public float finishTime;

    // Token: 0x04000ED0 RID: 3792
    public float timePaused;

    // Token: 0x04000ED1 RID: 3793
    private float timePausedTemp;

    // Token: 0x04000ED2 RID: 3794
    [HideInInspector]
    public bool enemyEngagedWithPlayer;

    // Token: 0x04000ED3 RID: 3795
    private bool enemyEngagedWithPlayerDoOnce;

    // Token: 0x04000ED4 RID: 3796
    public float autoSaveTimer;

    // Token: 0x04000ED5 RID: 3797
    [HideInInspector]
    public int killsSinceMedkitDrop;

    // Token: 0x04000ED6 RID: 3798
    public GameObject scoreBlood;

    // Token: 0x04000ED7 RID: 3799
    private Transform scoreBloodHolder;

    // Token: 0x04000ED8 RID: 3800
    private ScoreBloodScript[] scoreBloodScripts;

    // Token: 0x04000ED9 RID: 3801
    private int curScoreBloodScript;

    // Token: 0x04000EDA RID: 3802
    public float score;

    // Token: 0x04000EDB RID: 3803
    private float scoreVisual;

    // Token: 0x04000EDC RID: 3804
    private float scoreVisualStringCheck;

    // Token: 0x04000EDD RID: 3805
    public float tempScore;

    // Token: 0x04000EDE RID: 3806
    private float tempScoreStringCheck;

    // Token: 0x04000EDF RID: 3807
    public float multiplier;

    // Token: 0x04000EE0 RID: 3808
    private float multiplierStringCheck;

    // Token: 0x04000EE1 RID: 3809
    public float multiplierTimer;

    // Token: 0x04000EE2 RID: 3810
    public float timeSinceScoreLastGiven;

    // Token: 0x04000EE3 RID: 3811
    private float inAirScore;

    // Token: 0x04000EE4 RID: 3812
    public float lastEnemyKillScore;

    // Token: 0x04000EE5 RID: 3813
    private float prevScore;

    // Token: 0x04000EE6 RID: 3814
    private float prevTempScore;

    // Token: 0x04000EE7 RID: 3815
    private float prevMultiplier;

    // Token: 0x04000EE8 RID: 3816
    private float prevMultiplierTimer;

    // Token: 0x04000EE9 RID: 3817
    private bool cCheckSc;

    // Token: 0x04000EEA RID: 3818
    private bool cCheckMu;

    // Token: 0x04000EEB RID: 3819
    public bool cCheckMuTi;

    // Token: 0x04000EEC RID: 3820
    private bool cCheckTSc;

    // Token: 0x04000EED RID: 3821
    public bool cCheckGiSc;

    // Token: 0x04000EEE RID: 3822
    private bool cCheck;

    // Token: 0x04000EEF RID: 3823
    public bool hasCapturedMoment;

    // Token: 0x04000EF0 RID: 3824
    private Image screenCornerSlowMotionEffect;

    // Token: 0x04000EF1 RID: 3825
    private Color startFogColor;

    // Token: 0x04000EF2 RID: 3826
    private Color slowMoFogColor;

    // Token: 0x04000EF3 RID: 3827
    private CanvasGroup scoreHudCanvasGroup;

    // Token: 0x04000EF4 RID: 3828
    private CanvasGroup healthAndSlowMoCanvasGroup;

    // Token: 0x04000EF5 RID: 3829
    private CanvasGroup weaponPanelCanvasGroup;

    // Token: 0x04000EF6 RID: 3830
    private RectTransform scoreHud;

    // Token: 0x04000EF7 RID: 3831
    private RectTransform scoreHudTextCanvas;

    // Token: 0x04000EF8 RID: 3832
    private RectTransform scoreHudMainGraphic;

    // Token: 0x04000EF9 RID: 3833
    private RectTransform scoreHudBall;

    // Token: 0x04000EFA RID: 3834
    private Image scoreHudBallImage;

    // Token: 0x04000EFB RID: 3835
    private Color scoreHudBallImageStartColour;

    // Token: 0x04000EFC RID: 3836
    private RectTransform scoreHudLine1;

    // Token: 0x04000EFD RID: 3837
    private RectTransform scoreHudLine2;

    // Token: 0x04000EFE RID: 3838
    private TextMeshProUGUI scoreHudMultiplierText;

    // Token: 0x04000EFF RID: 3839
    private TextMeshProUGUI scoreHudMultiplierScoreText;

    // Token: 0x04000F00 RID: 3840
    private Vector3 scoreHudScoreNameTextStartPos;

    // Token: 0x04000F01 RID: 3841
    private ScoreTextScript[] scoreHudScoreNameTextScript;

    // Token: 0x04000F02 RID: 3842
    private int scoreHudScoreNameTextCurScript;

    // Token: 0x04000F03 RID: 3843
    private ScoreTextScript prevScoreHudScoreNameText;

    // Token: 0x04000F04 RID: 3844
    private int curPickupNotification;

    // Token: 0x04000F05 RID: 3845
    private RectTransform[] pickupNotificationRectTransforms;

    // Token: 0x04000F06 RID: 3846
    private PickUpNotificationScript[] pickupNotificationScripts;

    // Token: 0x04000F07 RID: 3847
    private Vector3 scoreHudScoreAmountTextStartPos;

    // Token: 0x04000F08 RID: 3848
    private ScoreTextScript[] scoreHudScoreAmountTextScript;

    // Token: 0x04000F09 RID: 3849
    private int scoreHudScoreAmountTextCurScript;

    // Token: 0x04000F0A RID: 3850
    private ScoreTextScript prevScoreHudScoreAmountText;

    // Token: 0x04000F0B RID: 3851
    private TextMeshProUGUI scoreHudTotalScoreText;

    // Token: 0x04000F0C RID: 3852
    private Vector3 scoreHudLine1StartPos;

    // Token: 0x04000F0D RID: 3853
    private Vector3 scoreHudLine2StartPos;

    // Token: 0x04000F0E RID: 3854
    private string scoreHudPreviousScoreName;

    // Token: 0x04000F0F RID: 3855
    private float scoreHudPreviousScore;

    // Token: 0x04000F10 RID: 3856
    private int scoreHudPreviousScoreMultiplier;

    // Token: 0x04000F11 RID: 3857
    private int scoreHudPreviousScoreMultiplierScore;

    // Token: 0x04000F12 RID: 3858
    private RectTransform subtleHighlighter;

    // Token: 0x04000F13 RID: 3859
    private Image subtleHighlighterImg;

    // Token: 0x04000F14 RID: 3860
    private Transform subtleHighlighterFollowTransform;

    // Token: 0x04000F15 RID: 3861
    private Canvas bigScreenReactionCanvas;

    // Token: 0x04000F16 RID: 3862
    private Text bigText;

    // Token: 0x04000F17 RID: 3863
    private Outline bigTextOutline;

    // Token: 0x04000F18 RID: 3864
    private Color bigTextOutlineStartColour;

    // Token: 0x04000F19 RID: 3865
    private Image bigFace;

    // Token: 0x04000F1A RID: 3866
    private Text bigMultiplier;

    // Token: 0x04000F1B RID: 3867
    private bool bigMultiplierDoOnce;

    // Token: 0x04000F1C RID: 3868
    private Outline bigMultiplierOutline;

    // Token: 0x04000F1D RID: 3869
    private Color bigMultiplierStartColour;

    // Token: 0x04000F1E RID: 3870
    private RectTransform slowMotionBarRect;

    // Token: 0x04000F1F RID: 3871
    private Image slowMotionBarHolder;

    // Token: 0x04000F20 RID: 3872
    private RectTransform slowMoIcon;

    // Token: 0x04000F21 RID: 3873
    private Image slowMoIconImage;

    // Token: 0x04000F22 RID: 3874
    private Color slowMoIconImageStartColour;

    // Token: 0x04000F23 RID: 3875
    private Vector2 slowMoIconStartPos;

    // Token: 0x04000F24 RID: 3876
    private float slowMoIconShakeTimer;

    // Token: 0x04000F25 RID: 3877
    public bool showPedroHint;

    // Token: 0x04000F26 RID: 3878
    private Canvas pedroHintCanvas;

    // Token: 0x04000F27 RID: 3879
    private bool enablePedroHintCanvasDoOnce;

    // Token: 0x04000F28 RID: 3880
    private RectTransform pedroHint;

    // Token: 0x04000F29 RID: 3881
    private RectTransform pedroHintSplat1;

    // Token: 0x04000F2A RID: 3882
    private Image pedroHintSplatImg1;

    // Token: 0x04000F2B RID: 3883
    private RectTransform pedroHintSplat2;

    // Token: 0x04000F2C RID: 3884
    private Image pedroHintSplatImg2;

    // Token: 0x04000F2D RID: 3885
    private RectTransform pedroHintSplat3;

    // Token: 0x04000F2E RID: 3886
    private Image pedroHintSplatImg3;

    // Token: 0x04000F2F RID: 3887
    private RectTransform pedroHintSplat4;

    // Token: 0x04000F30 RID: 3888
    private Image pedroHintSplatImg4;

    // Token: 0x04000F31 RID: 3889
    private RectTransform pedroHintPedro;

    // Token: 0x04000F32 RID: 3890
    private Text pedroHintTopText;

    // Token: 0x04000F33 RID: 3891
    private RectTransform pedroHintText;

    // Token: 0x04000F34 RID: 3892
    public float pedroHintTimer;

    // Token: 0x04000F35 RID: 3893
    private float pedroHintPedroXSpeed;

    // Token: 0x04000F36 RID: 3894
    private RectTransform reactionPedro;

    // Token: 0x04000F37 RID: 3895
    public Image reactionPedroFace;

    // Token: 0x04000F38 RID: 3896
    public float reactionPedroTimer;

    // Token: 0x04000F39 RID: 3897
    private float reactionPedroRot;

    // Token: 0x04000F3A RID: 3898
    public float dontAllowReactionPedroTimer;

    // Token: 0x04000F3B RID: 3899
    private float reactionPedroSpeed;

    // Token: 0x04000F3C RID: 3900
    public bool doReactionPedro;

    // Token: 0x04000F3D RID: 3901
    public float slowMotionAmount;

    // Token: 0x04000F3E RID: 3902
    public float timeSinceSlowMotionUsed;

    // Token: 0x04000F3F RID: 3903
    public bool unlimitedSlowMotion;

    // Token: 0x04000F40 RID: 3904
    public bool doMeleeHint;

    // Token: 0x04000F41 RID: 3905
    public float meleeHintCoolDown;

    // Token: 0x04000F42 RID: 3906
    public bool dodgeHelper;

    // Token: 0x04000F43 RID: 3907
    private bool prevDodgeHelper;

    // Token: 0x04000F44 RID: 3908
    public bool showDodgeAlert;

    // Token: 0x04000F45 RID: 3909
    private bool showDodgeAlertDoOnce;

    // Token: 0x04000F46 RID: 3910
    public float dodgeHelperClosestDistance;

    // Token: 0x04000F47 RID: 3911
    public Transform dodgeHelperTransform;

    // Token: 0x04000F48 RID: 3912
    private float dodgeHelperCoolDown;

    // Token: 0x04000F49 RID: 3913
    public float dodgeHelperDamage;

    // Token: 0x04000F4A RID: 3914
    public int updateFramesDone;

    // Token: 0x04000F4B RID: 3915
    public float timeSinceEnemySpokeOnDetection;

    // Token: 0x04000F4C RID: 3916
    public int lastUsedEnemyDetectSpeech;

    // Token: 0x04000F4D RID: 3917
    public float timeSinceEnemySpokeOnHearing;

    // Token: 0x04000F4E RID: 3918
    public Transform currentForceSpawnedPedro;

    // Token: 0x04000F4F RID: 3919
    private RectTransform instructionBackground;

    // Token: 0x04000F50 RID: 3920
    public bool showScoreHud;

    // Token: 0x04000F51 RID: 3921
    public bool showHealthHud;

    // Token: 0x04000F52 RID: 3922
    public bool showWeaponHud;

    // Token: 0x04000F53 RID: 3923
    public Material slowScrollMaterial;

    // Token: 0x04000F54 RID: 3924
    public int[] adjustedWeaponOrder;

    // Token: 0x04000F55 RID: 3925
    public Transform lastTriggeredInstructionText;

    // Token: 0x04000F56 RID: 3926
    private bool playerScriptKChangeWeaponDoOnce;

    // Token: 0x04000F57 RID: 3927
    private float pauseMenuHoldTimer;

    // Token: 0x04000F58 RID: 3928
    private Player player;

    // Token: 0x04000F59 RID: 3929
    private InputHelperScript inputHelperScript;

    // Token: 0x04000F5A RID: 3930
    private float deathTimer;

    // Token: 0x04000F5B RID: 3931
    [HideInInspector]
    public string pedroDeathHintText;

    // Token: 0x04000F5C RID: 3932
    public int[] allowedWeaponsForEnemies;

    // Token: 0x04000F5D RID: 3933
    public float optionsScreenShakeMultiplier;

    // Token: 0x04000F5E RID: 3934
    public bool doGore;

    // Token: 0x04000F5F RID: 3935
    public bool pleaseESRB;

    // Token: 0x04000F60 RID: 3936
    public bool showNoBlood;

    // Token: 0x04000F61 RID: 3937
    private GameObject hintDodge;

    // Token: 0x04000F62 RID: 3938
    private GameObject hintDied;

    // Token: 0x04000F63 RID: 3939
    private GameObject hintFocus;

    // Token: 0x04000F64 RID: 3940
    private GameObject hintChangeWeapon;

    // Token: 0x04000F65 RID: 3941
    private GameObject hintReload;

    // Token: 0x04000F66 RID: 3942
    private GameObject hintHealthFull;

    // Token: 0x04000F67 RID: 3943
    private GameObject hintAmmoFull;

    // Token: 0x04000F68 RID: 3944
    [HideInInspector]
    public GameObject hintKick;

    // Token: 0x04000F69 RID: 3945
    private GameObject hintGrab;

    // Token: 0x04000F6A RID: 3946
    private GameObject hintFlipSkateboard;

    // Token: 0x04000F6B RID: 3947
    private GameObject hintPressButton;

    // Token: 0x04000F6C RID: 3948
    private GameObject hintFlipLever;

    // Token: 0x04000F6D RID: 3949
    private GameObject hintFlipTable;

    // Token: 0x04000F6E RID: 3950
    private GameObject hintOpen;

    // Token: 0x04000F6F RID: 3951
    private GameObject hintSwing;

    // Token: 0x04000F70 RID: 3952
    private bool showHintDodge;

    // Token: 0x04000F71 RID: 3953
    private bool showHintDied;

    // Token: 0x04000F72 RID: 3954
    private bool showHintFocus;

    // Token: 0x04000F73 RID: 3955
    public bool showHintChangeWeapon;

    // Token: 0x04000F74 RID: 3956
    public bool showHintReload;

    // Token: 0x04000F75 RID: 3957
    public float showHintHealthFull;

    // Token: 0x04000F76 RID: 3958
    public float showHintAmmoFull;

    // Token: 0x04000F77 RID: 3959
    public bool showHintKick;

    // Token: 0x04000F78 RID: 3960
    public bool showHintGrab;

    // Token: 0x04000F79 RID: 3961
    public bool showHintFlipSkateboard;

    // Token: 0x04000F7A RID: 3962
    public bool showHintPressButton;

    // Token: 0x04000F7B RID: 3963
    public bool showHintFlipLever;

    // Token: 0x04000F7C RID: 3964
    public bool showHintFlipTable;

    // Token: 0x04000F7D RID: 3965
    public bool showHintOpen;

    // Token: 0x04000F7E RID: 3966
    public bool showHintSwing;

    // Token: 0x04000F7F RID: 3967
    public bool hasInitializedMusic;

    // Token: 0x04000F80 RID: 3968
    private Canvas canvasWeaponPanel;

    // Token: 0x04000F81 RID: 3969
    private Canvas canvasBigMultiplier;

    // Token: 0x04000F82 RID: 3970
    private Canvas canvasBigScreenReaction;

    // Token: 0x04000F83 RID: 3971
    private Canvas canvasBossHealth;

    // Token: 0x04000F84 RID: 3972
    private Canvas canvasDamageCanvas;

    // Token: 0x04000F85 RID: 3973
    private Canvas canvasEnemySpeechHolder;

    // Token: 0x04000F86 RID: 3974
    private Canvas canvasHealthAndSlowMo;

    // Token: 0x04000F87 RID: 3975
    private Canvas canvasHintsHolder;

    // Token: 0x04000F88 RID: 3976
    private Canvas canvasInstructionBackground;

    // Token: 0x04000F89 RID: 3977
    private Canvas canvasPedroHint;

    // Token: 0x04000F8A RID: 3978
    private Canvas canvasPickupNotificationHolder;

    // Token: 0x04000F8B RID: 3979
    private Canvas canvasScoreHud;

    // Token: 0x04000F8C RID: 3980
    private Canvas canvasWeaponHighlightHolder;

    // Token: 0x04000F8D RID: 3981
    private CanvasGroup secretUnlockedCanvasGroup;

    // Token: 0x04000F8E RID: 3982
    private Text secretUnlockedText;

    // Token: 0x04000F8F RID: 3983
    private float secretUnlockedTimer;

    // Token: 0x04000F90 RID: 3984
    private bool secretUnlockedDoOnce;

    // Token: 0x04000F91 RID: 3985
    private bool haveSaved;

    // Token: 0x04000F92 RID: 3986
    private bool fadeToBlackS;

    // Token: 0x04000F93 RID: 3987
    private int levelToLoadS;

    // Token: 0x04000F94 RID: 3988
    private bool slowMoAudioEffectDoOnceS;

    // Token: 0x04000F95 RID: 3989
    private bool fadeInSoundS;

    // Token: 0x04000F96 RID: 3990
    private bool fadeOutSoundS;

    // Token: 0x04000F97 RID: 3991
    private float unityTimescaleS;

    // Token: 0x04000F98 RID: 3992
    private float startUnityTimeScaleS;

    // Token: 0x04000F99 RID: 3993
    private float startUnityFixedDeltaTimeS;

    // Token: 0x04000F9A RID: 3994
    private float startUnityMaximumDeltaTimeS;

    // Token: 0x04000F9B RID: 3995
    private float timescaleS;

    // Token: 0x04000F9C RID: 3996
    private float timescaleRawS;

    // Token: 0x04000F9D RID: 3997
    private bool kActionS;

    // Token: 0x04000F9E RID: 3998
    private bool kActionDoOnceS;

    // Token: 0x04000F9F RID: 3999
    private float framesSinceBloodSplatterS;

    // Token: 0x04000FA0 RID: 4000
    private bool deadS;

    // Token: 0x04000FA1 RID: 4001
    private bool actionModeActivatedS;

    // Token: 0x04000FA2 RID: 4002
    private bool actionModeActivatedDoOnceS;

    // Token: 0x04000FA3 RID: 4003
    private bool actionModeActivatedDoOnce2S;

    // Token: 0x04000FA4 RID: 4004
    private float timeInActionModeS;

    // Token: 0x04000FA5 RID: 4005
    private float camTransitionValueS;

    // Token: 0x04000FA6 RID: 4006
    private bool camTransitionDoOnceS;

    // Token: 0x04000FA7 RID: 4007
    private bool disableOutlineEffectS;

    // Token: 0x04000FA8 RID: 4008
    private Transform prevOutlinedObjectS;

    // Token: 0x04000FA9 RID: 4009
    private Transform[] allEnemiesS;

    // Token: 0x04000FAA RID: 4010
    private AutoAimTargetScript[] allAutoAimTargetsS;

    // Token: 0x04000FAB RID: 4011
    private Transform sbTransformS;

    // Token: 0x04000FAC RID: 4012
    private Vector3 sbOffsetS;

    // Token: 0x04000FAD RID: 4013
    private float sbTimerS;

    // Token: 0x04000FAE RID: 4014
    private bool sbClickContS;

    // Token: 0x04000FAF RID: 4015
    private bool sbClickContDontFreezeS;

    // Token: 0x04000FB0 RID: 4016
    private float sbAppearDelayS;

    // Token: 0x04000FB1 RID: 4017
    private bool sbDoOnceS;

    // Token: 0x04000FB2 RID: 4018
    private string[] sbStringArrayS;

    // Token: 0x04000FB3 RID: 4019
    private int sbCurStringInArrayS;

    // Token: 0x04000FB4 RID: 4020
    private Transform sbTriggerTransformS;

    // Token: 0x04000FB5 RID: 4021
    private float voiceTimerS;

    // Token: 0x04000FB6 RID: 4022
    private float voiceTargetPitchS;

    // Token: 0x04000FB7 RID: 4023
    private float voiceTargetVolumeS;

    // Token: 0x04000FB8 RID: 4024
    private float hintTextTimerS;

    // Token: 0x04000FB9 RID: 4025
    private int hintTextIDS;

    // Token: 0x04000FBA RID: 4026
    private float musicIntenseFactorS;

    // Token: 0x04000FBB RID: 4027
    private int killsSinceMedkitDropS;

    // Token: 0x04000FBC RID: 4028
    private int nrOfEnemiesKilledS;

    // Token: 0x04000FBD RID: 4029
    private float slowMotionAmountS;

    // Token: 0x04000FBE RID: 4030
    private int potentialMultipliersFromEnemiesS;

    // Token: 0x04000FBF RID: 4031
    private bool hasTriggeredAlarmS;

    // Token: 0x04000FC0 RID: 4032
    private int nrOfEnemiesTotalS;

    // Token: 0x04000FC1 RID: 4033
    private bool hasCapturedMomentS;

    // Token: 0x04000FC2 RID: 4034
    private Transform lastTriggeredInstructionTextS;

    // Token: 0x04000FC3 RID: 4035
    private bool showScoreHudS;

    // Token: 0x04000FC4 RID: 4036
    private bool showHealthHudS;

    // Token: 0x04000FC5 RID: 4037
    private bool showWeaponHudS;

    // Token: 0x04000FC6 RID: 4038
    private float scoreS;

    // Token: 0x04000FC7 RID: 4039
    private float scoreVisualS;

    // Token: 0x04000FC8 RID: 4040
    private float tempScoreS;

    // Token: 0x04000FC9 RID: 4041
    private float multiplierS;

    // Token: 0x04000FCA RID: 4042
    private float multiplierTimerS;

    // Token: 0x04000FCB RID: 4043
    private float timeSinceScoreLastGivenS;

    // Token: 0x04000FCC RID: 4044
    private bool bigMultiplierDoOnceS;

    // Token: 0x04000FCD RID: 4045
    private bool outlineEffectEnabledS;

    // Token: 0x04000FCE RID: 4046
    private float outlineEffectLineThicknessS;

    // Token: 0x04000FCF RID: 4047
    private Renderer outlineEffectOutlineRenderers0S;

    // Token: 0x04000FD0 RID: 4048
    private Renderer outlineEffectOutlineRenderers1S;

    // Token: 0x04000FD1 RID: 4049
    [NonSerialized]
    public static RootScript RootScriptInstance;

    // Token: 0x06000468 RID: 1128 RVA: 0x00078BC8 File Offset: 0x00076DC8
    public RootScript()
    {
        this.blackFadeSpeed = (float)1;
        this.weaponToUseWhenLoadingFromLvlSelectScreen = 2;
        this.theme = 1;
        this.difficulty = (float)-1;
        this.setGravity = new Vector3((float)0, -9.81f, (float)0);
        this.loadStateOnStart = true;
        this.unityTimescale = (float)1;
        this.targetUnityTimescale = (float)1;
        this.prevUnityTimescale = (float)1;
        this.timescale = (float)1;
        this.fixedTimescale = (float)1;
        this.timescaleRaw = (float)1;
        this.disableOutlineEffect = true;
        this.slowMotionAmount = (float)1;
        this.showScoreHud = true;
        this.showHealthHud = true;
        this.showWeaponHud = true;
        this.optionsScreenShakeMultiplier = (float)1;
        this.doGore = true;
    }

    // Token: 0x17000024 RID: 36
    // (get) Token: 0x06000469 RID: 1129 RVA: 0x00003D6C File Offset: 0x00001F6C
    // (set) Token: 0x0600046A RID: 1130 RVA: 0x00003D74 File Offset: 0x00001F74
    public virtual bool levelEnded
    {
        get
        {
            return this.levelEndedVar;
        }
        set
        {
            this.levelEndedVar = value;
        }
    }

    // Token: 0x0600046B RID: 1131 RVA: 0x00078C78 File Offset: 0x00076E78
    public virtual void saveState()
    {
        this.haveSaved = true;
        this.fadeToBlackS = this.fadeToBlack;
        this.levelToLoadS = this.levelToLoad;
        this.slowMoAudioEffectDoOnceS = this.slowMoAudioEffectDoOnce;
        this.fadeInSoundS = this.fadeInSound;
        this.fadeOutSoundS = this.fadeOutSound;
        this.unityTimescaleS = this.unityTimescale;
        this.startUnityTimeScaleS = this.startUnityTimeScale;
        this.startUnityFixedDeltaTimeS = this.startUnityFixedDeltaTime;
        this.startUnityMaximumDeltaTimeS = this.startUnityMaximumDeltaTime;
        this.timescaleS = this.timescale;
        this.timescaleRawS = this.timescaleRaw;
        this.kActionS = this.kAction;
        this.kActionDoOnceS = this.kActionDoOnce;
        this.framesSinceBloodSplatterS = this.framesSinceBloodSplatter;
        this.deadS = this.dead;
        this.actionModeActivatedS = this.actionModeActivated;
        this.actionModeActivatedDoOnceS = this.actionModeActivatedDoOnce;
        this.actionModeActivatedDoOnce2S = this.actionModeActivatedDoOnce2;
        this.timeInActionModeS = this.timeInActionMode;
        this.camTransitionValueS = this.camTransitionValue;
        this.camTransitionDoOnceS = this.camTransitionDoOnce;
        this.disableOutlineEffectS = this.disableOutlineEffect;
        this.prevOutlinedObjectS = this.prevOutlinedObject;
        this.allEnemiesS = this.allEnemies;
        this.allAutoAimTargetsS = this.allAutoAimTargets;
        this.sbTransformS = this.sbTransform;
        this.sbOffsetS = this.sbOffset;
        this.sbTimerS = this.sbTimer;
        this.sbClickContS = this.sbClickCont;
        this.sbClickContDontFreezeS = this.sbClickContDontFreeze;
        this.sbAppearDelayS = this.sbAppearDelay;
        this.sbDoOnceS = this.sbDoOnce;
        this.sbStringArrayS = this.sbStringArray;
        this.sbCurStringInArrayS = this.sbCurStringInArray;
        this.sbTriggerTransformS = this.sbTriggerTransform;
        this.voiceTimerS = this.voiceTimer;
        this.voiceTargetPitchS = this.voiceTargetPitch;
        this.voiceTargetVolumeS = this.voiceTargetVolume;
        this.hintTextTimerS = this.hintTextTimer;
        this.hintTextIDS = this.hintTextID;
        this.musicIntenseFactorS = this.musicIntenseFactor;
        this.killsSinceMedkitDropS = this.killsSinceMedkitDrop;
        this.nrOfEnemiesKilledS = this.nrOfEnemiesKilled;
        this.slowMotionAmountS = this.slowMotionAmount;
        this.potentialMultipliersFromEnemiesS = this.potentialMultipliersFromEnemies;
        this.hasTriggeredAlarmS = this.hasTriggeredAlarm;
        this.nrOfEnemiesTotalS = this.nrOfEnemiesTotal;
        this.hasCapturedMomentS = this.hasCapturedMoment;
        this.lastTriggeredInstructionTextS = this.lastTriggeredInstructionText;
        this.showScoreHudS = this.showScoreHud;
        this.showHealthHudS = this.showHealthHud;
        this.showWeaponHudS = this.showWeaponHud;
        this.bigMultiplierDoOnceS = this.bigMultiplierDoOnce;
        this.scoreS = this.score;
        this.scoreVisualS = this.scoreVisual;
        this.tempScoreS = this.tempScore;
        this.multiplierS = this.multiplier;
        this.multiplierTimerS = this.multiplierTimer;
        this.timeSinceScoreLastGivenS = this.timeSinceScoreLastGiven;
        this.outlineEffectEnabledS = this.outlineEffect.enabled;
        this.outlineEffectLineThicknessS = this.outlineEffect.lineThickness;
        this.outlineEffectOutlineRenderers0S = this.outlineEffect.outlineRenderers[0];
        this.outlineEffectOutlineRenderers1S = this.outlineEffect.outlineRenderers[1];
    }

    // Token: 0x0600046C RID: 1132 RVA: 0x00078FA0 File Offset: 0x000771A0
    public virtual void loadState()
    {
        this.enemyEngagedWithPlayerDoOnce = false;
        this.autoSaveTimer = (float)0;
        this.fadeToBlack = this.fadeToBlackS;
        this.levelToLoad = this.levelToLoadS;
        this.slowMoAudioEffectDoOnce = this.slowMoAudioEffectDoOnceS;
        this.fadeInSound = this.fadeInSoundS;
        this.fadeOutSound = this.fadeOutSoundS;
        this.unityTimescale = this.unityTimescaleS;
        this.startUnityTimeScale = this.startUnityTimeScaleS;
        this.startUnityFixedDeltaTime = this.startUnityFixedDeltaTimeS;
        this.startUnityMaximumDeltaTime = this.startUnityMaximumDeltaTimeS;
        this.timescale = this.timescaleS;
        this.timescaleRaw = this.timescaleRawS;
        this.kAction = this.kActionS;
        this.kActionDoOnce = this.kActionDoOnceS;
        this.framesSinceBloodSplatter = this.framesSinceBloodSplatterS;
        this.dead = this.deadS;

        if (MultiplayerManagerTest.inst.gamemode != MPGamemodes.PvP)
        {
            this.actionModeActivated = this.actionModeActivatedS;
            this.actionModeActivatedDoOnce = this.actionModeActivatedDoOnceS;
            this.actionModeActivatedDoOnce2 = this.actionModeActivatedDoOnce2S;
        }

        this.timeInActionMode = this.timeInActionModeS;
        this.camTransitionValue = this.camTransitionValueS;
        this.camTransitionDoOnce = this.camTransitionDoOnceS;
        this.disableOutlineEffect = this.disableOutlineEffectS;
        this.prevOutlinedObject = this.prevOutlinedObjectS;
        this.allEnemies = this.allEnemiesS;
        this.allAutoAimTargets = this.allAutoAimTargetsS;
        this.sbTransform = this.sbTransformS;
        this.sbOffset = this.sbOffsetS;
        this.sbTimer = this.sbTimerS;
        this.sbClickCont = this.sbClickContS;
        this.sbClickContDontFreeze = this.sbClickContDontFreezeS;
        this.sbAppearDelay = this.sbAppearDelayS;
        this.sbDoOnce = this.sbDoOnceS;
        this.sbStringArray = this.sbStringArrayS;
        this.sbCurStringInArray = this.sbCurStringInArrayS;
        this.sbTriggerTransform = this.sbTriggerTransformS;
        this.voiceTimer = this.voiceTimerS;
        this.voiceTargetPitch = this.voiceTargetPitchS;
        this.voiceTargetVolume = this.voiceTargetVolumeS;
        this.hintTextTimer = this.hintTextTimerS;
        this.hintTextID = this.hintTextIDS;
        this.musicIntenseFactor = this.musicIntenseFactorS;
        this.killsSinceMedkitDrop = this.killsSinceMedkitDropS;
        this.nrOfEnemiesKilled = this.nrOfEnemiesKilledS;
        this.slowMotionAmount = this.slowMotionAmountS;
        this.potentialMultipliersFromEnemies = this.potentialMultipliersFromEnemiesS;
        this.hasTriggeredAlarm = this.hasTriggeredAlarmS;
        this.nrOfEnemiesTotal = this.nrOfEnemiesTotalS;
        this.hasCapturedMoment = this.hasCapturedMomentS;
        this.lastTriggeredInstructionText = this.lastTriggeredInstructionTextS;
        this.showScoreHud = this.showScoreHudS;
        this.showHealthHud = this.showHealthHudS;
        this.showWeaponHud = this.showWeaponHudS;
        this.bigMultiplierDoOnce = this.bigMultiplierDoOnceS;
        this.score = this.scoreS;
        this.scoreVisual = this.scoreVisualS;
        this.tempScore = this.tempScoreS;
        this.multiplier = this.multiplierS;
        this.score += this.tempScore * this.multiplier;
        this.tempScore = (float)0;
        this.multiplier = (float)0;
        this.multiplierTimer = (float)0;
        this.prevScore = this.score;
        this.prevMultiplier = this.multiplier;
        this.prevTempScore = this.tempScore;
        this.prevMultiplierTimer = this.multiplierTimer;
        this.cCheckMu = (this.cCheckSc = (this.cCheckTSc = (this.cCheckMuTi = (this.cCheckGiSc = true))));
        this.outlineEffect.enabled = this.outlineEffectEnabledS;
        this.outlineEffect.lineThickness = this.outlineEffectLineThicknessS;
        this.outlineEffect.outlineRenderers[0] = this.outlineEffectOutlineRenderers0S;
        this.outlineEffect.outlineRenderers[1] = this.outlineEffectOutlineRenderers1S;
        this.timeSinceScoreLastGiven = this.timeSinceScoreLastGivenS;
        this.lastEnemyKillScore = (float)0;
        if (this.kAction)
        {
            this.actionStateAudioSnapshot.TransitionTo(0.2f);
            this.lastActivatedAudioSnapshot = 1;
        }
        else
        {
            if (!this.playerScript.onMotorcycle)
            {
                this.normalStateAudioSnapshot.TransitionTo(0.2f);
            }
            else
            {
                this.motorcycleNormalStateAudioSnapshot.TransitionTo(0.2f);
            }
            this.lastActivatedAudioSnapshot = 0;
        }
        int i = 0;
        ParticleSystem[] componentsInChildren = this.mainCamera.GetComponentsInChildren<ParticleSystem>();
        int length = componentsInChildren.Length;
        while (i < length)
        {
            componentsInChildren[i].Clear(true);
            i++;
        }
        this.voice.Stop();
        this.sbBubble.gameObject.SetActive(false);
        this.sbTail.gameObject.SetActive(false);
        this.sbClickCont = false;
        this.sbTimer = (float)0;
        this.sbDoOnce = false;
        this.clearHintText();
        if (this.pedroHintTimer > (float)0)
        {
            this.pedroHintTimer = (float)60;
        }
    }

    // Token: 0x0600046D RID: 1133 RVA: 0x00079454 File Offset: 0x00077654
    public virtual void LateUpdate()
    {
        if (this.doCheckpointSave)
        {
            this.saveState();
        }
        if (this.doCheckpointLoad)
        {
            this.loadState();
        }
        if (this.prevScore != this.score)
        {
            if (!this.cCheckSc)
            {
                this.cCheck = true;
            }
            this.prevScore = this.score;
        }
        if (this.prevMultiplier != this.multiplier)
        {
            if (!this.cCheckMu)
            {
                this.cCheck = true;
            }
            this.prevMultiplier = this.multiplier;
        }
        if (this.prevTempScore != this.tempScore)
        {
            if (!this.cCheckTSc)
            {
                this.cCheck = true;
            }
            this.prevTempScore = this.tempScore;
        }
        if (this.prevMultiplierTimer != this.multiplierTimer)
        {
            if (!this.cCheckMuTi)
            {
                this.cCheck = true;
            }
            this.prevMultiplierTimer = this.multiplierTimer;
        }
        this.cCheckSc = false;
        this.cCheckMu = false;
        this.cCheckTSc = false;
        this.cCheckMuTi = false;
        this.cCheckGiSc = false;
    }

    // Token: 0x0600046E RID: 1134 RVA: 0x00003D7D File Offset: 0x00001F7D
    public virtual bool GetCCheck()
    {
        return this.cCheck;
    }

    // Token: 0x0600046F RID: 1135 RVA: 0x00079564 File Offset: 0x00077764
    public virtual void setupGraphicsSettings()
    {
        if (this.rootShared.curVisualQualityLevel == 0)
        {
            if (this.lowPostProcessing != null)
            {
                this.postProcessingBehaviour.profile = this.lowPostProcessing;
            }
            this.camVignette.enabled = false;
            this.camColourCorrection.enabled = false;
            if (this.rootShared.modPlayerSize == (float)100 && !this.rootShared.modBigHead)
            {
                GameObject.Find("Player/PlayerGraphics/TorsorBlackLongSleeve").GetComponent<Cloth>().enabled = false;
            }
        }
        else
        {
            this.postProcessingBehaviour.profile = this.defaultPostProcessing;
            this.camVignette.enabled = true;
            this.camColourCorrection.enabled = true;
            if (this.rootShared.modPlayerSize == (float)100 && !this.rootShared.modBigHead)
            {
                GameObject.Find("Player/PlayerGraphics/TorsorBlackLongSleeve").GetComponent<Cloth>().enabled = true;
            }
        }
        if (this.rootShared.modSideOnCamera && SceneManager.GetActiveScene().buildIndex == 52)
        {
            this.postProcessingBehaviour.profile = this.lowPostProcessing;
        }
    }

    // Token: 0x06000470 RID: 1136 RVA: 0x00079690 File Offset: 0x00077890
    public virtual void Awake()
    {
        RootScript.RootScriptInstance = this;
        Debug.Log("Level start at: " + DateTime.Now);
        if (GameObject.Find("Rewired Input Manager") == null)
        {
            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Rewired Input Manager"));
            gameObject.name = "Rewired Input Manager";
        }
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
        this.player = ReInput.players.GetPlayer(0);
    }

    // Token: 0x06000471 RID: 1137 RVA: 0x00079704 File Offset: 0x00077904
    public virtual void Start()
    {
        GameObject gameObject = GameObject.Find("RootShared");
        if (gameObject == null)
        {
            gameObject = new GameObject();
            gameObject.gameObject.name = "RootShared";
            gameObject.AddComponent(typeof(RootSharedScript));
        }
        this.rootShared = (RootSharedScript)gameObject.GetComponent(typeof(RootSharedScript));
        this.statsTracker = (StatsTrackerScript)gameObject.GetComponent(typeof(StatsTrackerScript));
        this.playerScript = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
        if (!this.rootShared.neverChangeMouseCursor)
        {
            Cursor.SetCursor(Resources.Load("HUD/ingame_cursor") as Texture2D, new Vector2((float)16, (float)16), CursorMode.Auto);
        }
        this.rootShared.adjustUIForAspectRatio();
        this.SetCursorState();
        Physics.gravity = this.setGravity;
        this.startUnityTimeScale = Time.timeScale;
        this.startUnityFixedDeltaTime = Time.fixedDeltaTime;
        this.startUnityMaximumDeltaTime = Time.maximumDeltaTime;
        this.blackFade = (Image)GameObject.Find("HUD/Canvas/BlackFade").GetComponent(typeof(Image));
        this.blackFade.enabled = true;
        float a = this.blackFade.color.a + this.blackFadeExtraTimer / ((float)60 / this.blackFadeSpeed);
        Color color = this.blackFade.color;
        float num = color.a = a;
        Color color2 = this.blackFade.color = color;
        this.inputHelperScript = (InputHelperScript)GameObject.Find("Rewired Input Manager").GetComponent(typeof(InputHelperScript));
        this.screenCornerSlowMotionEffect = (Image)GameObject.Find("HUD/Canvas/DamageCanvas/ScreenCornerSlowMotionEffect").GetComponent(typeof(Image));
        this.screenCornerSlowMotionEffect.enabled = false;
        int num2 = 0;
        Color color3 = this.screenCornerSlowMotionEffect.color;
        float num3 = color3.a = (float)num2;
        Color color4 = this.screenCornerSlowMotionEffect.color = color3;
        this.startFogColor = RenderSettings.fogColor;
        this.slowMoFogColor = Color.Lerp(this.startFogColor, Color.white, (this.theme != 5) ? ((this.theme != 4) ? 0.32f : 0.5f) : 0.1f);
        this.bulletHitParticle = (ParticleSystem)GameObject.Find("Main Camera/BulletHitParticle").GetComponent(typeof(ParticleSystem));
        this.sparksParticle = (ParticleSystem)GameObject.Find("Main Camera/SparksParticle").GetComponent(typeof(ParticleSystem));
        this.bloodMistParticle = (ParticleSystem)GameObject.Find("Main Camera/BloodMistParticle").GetComponent(typeof(ParticleSystem));
        this.bloodDropsParticle = (ParticleSystem)GameObject.Find("Main Camera/BloodDropsParticle").GetComponent(typeof(ParticleSystem));
        this.pistolClipParticle = (ParticleSystem)GameObject.Find("Main Camera/PistolClipParticle").GetComponent(typeof(ParticleSystem));
        this.glassParticle = (ParticleSystem)GameObject.Find("Main Camera/GlassParticle").GetComponent(typeof(ParticleSystem));
        this.destroyParticleGlass = (ParticleSystem)GameObject.Find("Main Camera/DestroyParticleGlass").GetComponent(typeof(ParticleSystem));
        this.debrisParticle = (ParticleSystem)GameObject.Find("Main Camera/DebrisParticle").GetComponent(typeof(ParticleSystem));
        this.shellParticle = (ParticleSystem)GameObject.Find("Main Camera/ShellParticle").GetComponent(typeof(ParticleSystem));
        this.smokeParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeParticle").GetComponent(typeof(ParticleSystem));
        this.smokeSpriteParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeSpriteParticle").GetComponent(typeof(ParticleSystem));
        this.rockParticle = (ParticleSystem)GameObject.Find("Main Camera/RockParticles").GetComponent(typeof(ParticleSystem));
        this.mainCamera = (Camera)GameObject.Find("Main Camera").GetComponent(typeof(Camera));
        this.postProcessingBehaviour = (PostProcessingBehaviour)this.mainCamera.GetComponent(typeof(PostProcessingBehaviour));
        this.cameraScript = (CameraScript)GameObject.Find("Main Camera").GetComponent(typeof(CameraScript));
        this.record = (Record)this.mainCamera.GetComponent(typeof(Record));
        this.camColourCorrection = (ColorCorrectionCurves)this.mainCamera.gameObject.GetComponent(typeof(ColorCorrectionCurves));
        this.camVignette = (VignetteAndChromaticAberration)this.mainCamera.gameObject.GetComponent(typeof(VignetteAndChromaticAberration));
        this.camVignette.blurDistance = 1.5f;
        this.outlineEffect = (OutlineEffect)GameObject.Find("Main Camera").GetComponent(typeof(OutlineEffect));
        this.trajectory = GameObject.Find("HUD/Trajectory");
        this.defaultPostProcessing = this.postProcessingBehaviour.profile;
        this.setupGraphicsSettings();
        this.sbBubble = (RectTransform)GameObject.Find("HUD/Canvas/SpeechBubble_Bubble").GetComponent(typeof(RectTransform));
        this.sbTail = (RectTransform)GameObject.Find("HUD/Canvas/SpeechBubble_Tail").GetComponent(typeof(RectTransform));
        this.sbText = (Text)this.sbBubble.Find("SpeechBubble_Text").GetComponent(typeof(Text));
        this.sbName = (Text)this.sbBubble.Find("SpeechBubble_SpeakerNameBackground/SpeechBubble_SpeakerName").GetComponent(typeof(Text));
        this.sbNameHolder = (RectTransform)this.sbBubble.Find("SpeechBubble_SpeakerNameBackground").GetComponent(typeof(RectTransform));
        this.sbClickIndicator = (RectTransform)this.sbBubble.Find("SpeechBubble_ClickIndicator").GetComponent(typeof(RectTransform));
        this.setUpSpeechBubbleButtonPrompt();
        this.sbBubble.gameObject.SetActive(false);
        this.sbTail.gameObject.SetActive(false);
        this.hudCanvas = GameObject.Find("HUD/Canvas");
        this.hudCanvasRect = (RectTransform)this.hudCanvas.GetComponent(typeof(RectTransform));
        this.pickupNotificationHolder = this.hudCanvas.transform.Find("PickupNotificationHolder").gameObject;
        this.scoreHud = (RectTransform)this.hudCanvas.transform.Find("ScoreHud").GetComponent(typeof(RectTransform));
        this.scoreHudTextCanvas = (RectTransform)this.scoreHud.Find("TextCanvas").GetComponent(typeof(RectTransform));
        this.scoreHudMainGraphic = (RectTransform)this.scoreHud.Find("MainGraphic").GetComponent(typeof(RectTransform));
        this.scoreHudBall = (RectTransform)this.scoreHud.Find("Ball").GetComponent(typeof(RectTransform));
        this.scoreHudBallImage = (Image)this.scoreHudBall.GetComponent(typeof(Image));
        this.scoreHudBallImageStartColour = this.scoreHudBallImage.color;
        this.scoreHudLine1 = (RectTransform)this.scoreHud.Find("Line1").GetComponent(typeof(RectTransform));
        this.scoreHudLine2 = (RectTransform)this.scoreHud.Find("Line2").GetComponent(typeof(RectTransform));
        this.scoreHudMultiplierText = (TextMeshProUGUI)this.scoreHud.Find("MultiplierText").GetComponent(typeof(TextMeshProUGUI));
        this.scoreHudMultiplierScoreText = (TextMeshProUGUI)this.scoreHud.Find("TextCanvas/MultiplierScoreText").GetComponent(typeof(TextMeshProUGUI));
        this.scoreHudTotalScoreText = (TextMeshProUGUI)this.scoreHud.Find("TotalScoreText").GetComponent(typeof(TextMeshProUGUI));
        if (LocalizationManager.CurrentLanguageCode == "fr" || LocalizationManager.CurrentLanguageCode == "de")
        {
            this.scoreHudMultiplierText.text = "x " + this.multiplier;
        }
        else
        {
            this.scoreHudMultiplierText.text = "x" + this.multiplier;
        }
        int i = 0;
        this.scoreHudScoreNameTextScript = new ScoreTextScript[10];
        this.scoreHudScoreNameTextScript[0] = (ScoreTextScript)this.scoreHud.Find("TextCanvas/ScoreNameText").GetComponent(typeof(ScoreTextScript));
        this.scoreHudScoreNameTextStartPos = ((RectTransform)this.scoreHudScoreNameTextScript[0].GetComponent(typeof(RectTransform))).anchoredPosition;
        for (i = 1; i < Extensions.get_length(this.scoreHudScoreNameTextScript); i++)
        {
            this.scoreHudScoreNameTextScript[i] = (ScoreTextScript)UnityEngine.Object.Instantiate<GameObject>(this.scoreHudScoreNameTextScript[0].gameObject).GetComponent(typeof(ScoreTextScript));
            this.scoreHudScoreNameTextScript[i].transform.SetParent(this.scoreHudTextCanvas);
            ((RectTransform)this.scoreHudScoreNameTextScript[i].GetComponent(typeof(RectTransform))).anchoredPosition = this.scoreHudScoreNameTextStartPos;
        }
        this.scoreHudScoreAmountTextScript = new ScoreTextScript[10];
        this.scoreHudScoreAmountTextScript[0] = (ScoreTextScript)this.scoreHud.Find("TextCanvas/ScoreAmountText").GetComponent(typeof(ScoreTextScript));
        this.scoreHudScoreAmountTextStartPos = ((RectTransform)this.scoreHudScoreAmountTextScript[0].GetComponent(typeof(RectTransform))).anchoredPosition;
        for (i = 1; i < Extensions.get_length(this.scoreHudScoreAmountTextScript); i++)
        {
            this.scoreHudScoreAmountTextScript[i] = (ScoreTextScript)UnityEngine.Object.Instantiate<GameObject>(this.scoreHudScoreAmountTextScript[0].gameObject).GetComponent(typeof(ScoreTextScript));
            this.scoreHudScoreAmountTextScript[i].transform.SetParent(this.scoreHudTextCanvas);
            ((RectTransform)this.scoreHudScoreAmountTextScript[i].GetComponent(typeof(RectTransform))).anchoredPosition = this.scoreHudScoreAmountTextStartPos;
        }
        this.pickupNotificationRectTransforms = new RectTransform[5];
        this.pickupNotificationScripts = new PickUpNotificationScript[5];
        for (int j = 0; j < Extensions.get_length(this.pickupNotificationRectTransforms); j++)
        {
            GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("HUD/PickupNotification"), this.pickupNotificationHolder.transform);
            this.pickupNotificationRectTransforms[j] = (RectTransform)gameObject2.GetComponent(typeof(RectTransform));
            this.pickupNotificationScripts[j] = (PickUpNotificationScript)gameObject2.GetComponent(typeof(PickUpNotificationScript));
        }
        this.scoreHudLine1StartPos = this.scoreHudLine1.anchoredPosition;
        this.scoreHudLine2StartPos = this.scoreHudLine2.anchoredPosition;
        if (this.rootShared.lowEndHardware)
        {
            this.scoreHudLine1.gameObject.SetActive(false);
            this.scoreHudLine2.gameObject.SetActive(false);
        }
        int num4 = -50;
        Vector2 anchoredPosition = this.scoreHud.anchoredPosition;
        float num5 = anchoredPosition.y = (float)num4;
        Vector2 vector = this.scoreHud.anchoredPosition = anchoredPosition;
        this.multiplier = (float)0;
        this.scoreHudCanvasGroup = (CanvasGroup)this.scoreHud.gameObject.AddComponent(typeof(CanvasGroup));
        this.healthAndSlowMoCanvasGroup = (CanvasGroup)this.hudCanvas.transform.Find("HealthAndSlowMo").gameObject.AddComponent(typeof(CanvasGroup));
        this.weaponPanelCanvasGroup = (CanvasGroup)this.hudCanvas.transform.Find("WeaponPanel").gameObject.AddComponent(typeof(CanvasGroup));
        this.slowMotionBarRect = (RectTransform)GameObject.Find("HealthAndSlowMo/SlowMoIcon/SlowMoBar/Bar").GetComponent(typeof(RectTransform));
        this.slowMotionBarHolder = (Image)GameObject.Find("HealthAndSlowMo/SlowMoIcon/SlowMoBar").GetComponent(typeof(Image));
        this.slowMoIcon = (RectTransform)GameObject.Find("HealthAndSlowMo/SlowMoIcon").GetComponent(typeof(RectTransform));
        this.slowMoIconImage = (Image)this.slowMoIcon.GetComponent(typeof(Image));
        this.slowMoIconImageStartColour = this.slowMoIconImage.color;
        this.slowMoIconStartPos = this.slowMoIcon.anchoredPosition;
        this.scoreBloodHolder = this.hudCanvas.transform.Find("ScoreBloodHolder");
        this.scoreBloodScripts = new ScoreBloodScript[3];
        this.scoreBloodScripts[0] = (ScoreBloodScript)UnityEngine.Object.Instantiate<GameObject>(this.scoreBlood).GetComponent(typeof(ScoreBloodScript));
        this.scoreBloodScripts[1] = (ScoreBloodScript)UnityEngine.Object.Instantiate<GameObject>(this.scoreBlood).GetComponent(typeof(ScoreBloodScript));
        this.scoreBloodScripts[2] = (ScoreBloodScript)UnityEngine.Object.Instantiate<GameObject>(this.scoreBlood).GetComponent(typeof(ScoreBloodScript));
        this.pedroHint = (RectTransform)this.hudCanvas.transform.Find("PedroHint").GetComponent(typeof(RectTransform));
        this.pedroHintCanvas = (Canvas)this.pedroHint.GetComponent(typeof(Canvas));
        this.pedroHintSplat1 = (RectTransform)this.pedroHint.Find("Splat1").GetComponent(typeof(RectTransform));
        this.pedroHintSplatImg1 = (Image)this.pedroHintSplat1.GetComponent(typeof(Image));
        this.pedroHintSplat2 = (RectTransform)this.pedroHint.Find("Splat2").GetComponent(typeof(RectTransform));
        this.pedroHintSplatImg2 = (Image)this.pedroHintSplat2.GetComponent(typeof(Image));
        this.pedroHintSplat3 = (RectTransform)this.pedroHint.Find("Splat3").GetComponent(typeof(RectTransform));
        this.pedroHintSplatImg3 = (Image)this.pedroHintSplat3.GetComponent(typeof(Image));
        this.pedroHintSplat4 = (RectTransform)this.pedroHint.Find("Splat4").GetComponent(typeof(RectTransform));
        this.pedroHintSplatImg4 = (Image)this.pedroHintSplat4.GetComponent(typeof(Image));
        this.pedroHintPedro = (RectTransform)this.pedroHint.Find("Pedro").GetComponent(typeof(RectTransform));
        this.pedroHintTopText = (Text)this.pedroHint.Find("TopText").GetComponent(typeof(Text));
        this.pedroHintText = (RectTransform)this.pedroHint.Find("MainText").GetComponent(typeof(RectTransform));
        int num6 = 0;
        Vector3 localScale = this.pedroHintSplat4.localScale;
        float num7 = localScale.x = (float)num6;
        Vector3 vector2 = this.pedroHintSplat4.localScale = localScale;
        int num8 = num6;
        Vector3 localScale2 = this.pedroHintSplat3.localScale;
        float num9 = localScale2.x = (float)num8;
        Vector3 vector3 = this.pedroHintSplat3.localScale = localScale2;
        int num10 = num8;
        Vector3 localScale3 = this.pedroHintSplat2.localScale;
        float num11 = localScale3.x = (float)num10;
        Vector3 vector4 = this.pedroHintSplat2.localScale = localScale3;
        int num12 = num10;
        Vector3 localScale4 = this.pedroHintSplat1.localScale;
        float num13 = localScale4.y = (float)num12;
        Vector3 vector5 = this.pedroHintSplat1.localScale = localScale4;
        int num14 = -100;
        Vector2 anchoredPosition2 = this.pedroHintPedro.anchoredPosition;
        float num15 = anchoredPosition2.x = (float)num14;
        Vector2 vector6 = this.pedroHintPedro.anchoredPosition = anchoredPosition2;
        int num16 = -1000;
        Vector2 anchoredPosition3 = this.pedroHintText.anchoredPosition;
        float num17 = anchoredPosition3.x = (float)num16;
        Vector2 vector7 = this.pedroHintText.anchoredPosition = anchoredPosition3;
        int num18 = 0;
        Color color5 = this.pedroHintTopText.color;
        float num19 = color5.a = (float)num18;
        Color color6 = this.pedroHintTopText.color = color5;
        int num20 = num18;
        Color color7 = this.pedroHintSplatImg4.color;
        float num21 = color7.a = (float)num20;
        Color color8 = this.pedroHintSplatImg4.color = color7;
        int num22 = num20;
        Color color9 = this.pedroHintSplatImg3.color;
        float num23 = color9.a = (float)num22;
        Color color10 = this.pedroHintSplatImg3.color = color9;
        int num24 = num22;
        Color color11 = this.pedroHintSplatImg2.color;
        float num25 = color11.a = (float)num24;
        Color color12 = this.pedroHintSplatImg2.color = color11;
        this.reactionPedro = (RectTransform)this.hudCanvas.transform.Find("ReactionPedro").GetComponent(typeof(RectTransform));
        this.reactionPedroFace = (Image)this.reactionPedro.Find("Face").GetComponent(typeof(Image));
        this.bigScreenReactionCanvas = (Canvas)this.hudCanvas.transform.Find("BigScreenReaction").GetComponent(typeof(Canvas));
        this.bigText = (Text)this.hudCanvas.transform.Find("BigScreenReaction/BigText").GetComponent(typeof(Text));
        this.bigTextOutline = (Outline)this.bigText.GetComponent(typeof(Outline));
        this.bigTextOutlineStartColour = this.bigTextOutline.effectColor;
        this.bigFace = (Image)this.hudCanvas.transform.Find("BigScreenReaction/BigFace").GetComponent(typeof(Image));
        this.bigMultiplier = (Text)this.hudCanvas.transform.Find("BigMultiplier").GetComponent(typeof(Text));
        this.bigMultiplierOutline = (Outline)this.bigMultiplier.GetComponent(typeof(Outline));
        this.bigMultiplierStartColour = this.bigMultiplier.color;
        this.bigMultiplier.text = string.Empty;
        int num26 = 0;
        Color color13 = this.bigMultiplier.color;
        float num27 = color13.a = (float)num26;
        Color color14 = this.bigMultiplier.color = color13;
        this.bigMultiplier.gameObject.SetActive(false);
        this.subtleHighlighter = (RectTransform)this.hudCanvas.transform.Find("DamageCanvas/SubtleHighlighter").GetComponent(typeof(RectTransform));
        this.subtleHighlighterImg = (Image)this.subtleHighlighter.GetComponent(typeof(Image));
        int num28 = 0;
        Color color15 = this.subtleHighlighterImg.color;
        float num29 = color15.a = (float)num28;
        Color color16 = this.subtleHighlighterImg.color = color15;
        this.subtleHighlighter.gameObject.SetActive(false);
        this.levelEndScreen = (RectTransform)this.hudCanvas.transform.Find("EndScreen").GetComponent(typeof(RectTransform));
        this.levelEndScreen.gameObject.SetActive(false);
        GameObject gameObject3 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("HUD/OptionsMenu"), this.hudCanvasRect);
        this.optionsMenu = (RectTransform)gameObject3.GetComponent(typeof(RectTransform));
        this.optionsMenuScript = (OptionsMenuScript)this.optionsMenu.GetComponent(typeof(OptionsMenuScript));
        this.optionsMenuScript.inGame = true;
        this.instructionBackground = (RectTransform)GameObject.Find("HUD/Canvas/").transform.Find("InstructionBackground").GetComponent(typeof(RectTransform));
        this.voiceController = (VoiceControllerScript)this.gameObject.AddComponent(typeof(VoiceControllerScript));
        this.voice = (AudioSource)this.transform.Find("Voice").GetComponent(typeof(AudioSource));
        this.voiceController.voice = this.voice;
        this.musicAudioSource = (AudioSource)this.transform.Find("Music/NormalMusic/Calm").GetComponent(typeof(AudioSource));
        this.audioMixer.updateMode = AudioMixerUpdateMode.Normal;
        if (this.transform.Find("Music/NormalMusic").gameObject.activeSelf)
        {
            this.audioMixer.SetFloat("NormalTimeMusicHighPass", (float)0);
        }
        if (this.loopMusicIntro)
        {
            this.introMusicLoopAudioSnapshot.TransitionTo((float)0);
            this.lastActivatedAudioSnapshot = 7;
        }
        else
        {
            if (!this.playerScript.onMotorcycle)
            {
                this.normalStateAudioSnapshot.TransitionTo((float)0);
            }
            else
            {
                this.motorcycleNormalStateAudioSnapshot.TransitionTo((float)0);
            }
            this.lastActivatedAudioSnapshot = 0;
        }
        if (this.fadeInSound)
        {
            this.audioMixer.SetFloat("MasterVolume", (float)-80);
        }
        else
        {
            this.audioMixer.SetFloat("MasterVolume", (float)0);
        }
        this.earRingingSound = (AudioSource)this.transform.Find("GeneralSounds/EarRinging").GetComponent(typeof(AudioSource));
        this.hudAudio = (AudioSource)GameObject.Find("HUD").GetComponent(typeof(AudioSource));
        this.hudAudio2 = (AudioSource)GameObject.Find("HUD/Canvas").GetComponent(typeof(AudioSource));
        this.multiplierAudio = (AudioSource)this.transform.Find("GeneralSounds/ScoreMultiplierSound").GetComponent(typeof(AudioSource));
        this.bullets = new GameObject[50];
        this.bulletScripts = new BulletScript[Extensions.get_length(this.bullets)];
        for (int k = 0; k < Extensions.get_length(this.bullets); k++)
        {
            this.bullets[k] = UnityEngine.Object.Instantiate<GameObject>(this.bullet);
            this.bulletScripts[k] = (BulletScript)this.bullets[k].GetComponent(typeof(BulletScript));
        }
        this.explosions = new GameObject[20];
        this.explosionScripts = new ExplosionVisualScript[Extensions.get_length(this.explosions)];
        this.explosionRigidbodies = new Rigidbody[Extensions.get_length(this.explosions)];
        this.explosionSphereColliders = new SphereCollider[Extensions.get_length(this.explosions)];
        for (int l = 0; l < Extensions.get_length(this.explosions); l++)
        {
            this.explosions[l] = UnityEngine.Object.Instantiate<GameObject>(this.explosion);
            this.explosionScripts[l] = (ExplosionVisualScript)this.explosions[l].GetComponent(typeof(ExplosionVisualScript));
            this.explosionRigidbodies[l] = (Rigidbody)this.explosions[l].GetComponent(typeof(Rigidbody));
            this.explosionSphereColliders[l] = (SphereCollider)this.explosions[l].GetComponent(typeof(SphereCollider));
        }
        int m = 0;
        this.mfPistol = new MuzzleFlashMainScript[10];
        for (m = 0; m < Extensions.get_length(this.mfPistol); m++)
        {
            this.mfPistol[m] = (MuzzleFlashMainScript)UnityEngine.Object.Instantiate<GameObject>(this.muzzleFlashes[0]).GetComponent(typeof(MuzzleFlashMainScript));
        }
        this.mfUzi = new MuzzleFlashMainScript[10];
        for (m = 0; m < Extensions.get_length(this.mfUzi); m++)
        {
            this.mfUzi[m] = (MuzzleFlashMainScript)UnityEngine.Object.Instantiate<GameObject>(this.muzzleFlashes[1]).GetComponent(typeof(MuzzleFlashMainScript));
        }
        this.mfShotgun = new MuzzleFlashMainScript[10];
        for (m = 0; m < Extensions.get_length(this.mfShotgun); m++)
        {
            this.mfShotgun[m] = (MuzzleFlashMainScript)UnityEngine.Object.Instantiate<GameObject>(this.muzzleFlashes[3]).GetComponent(typeof(MuzzleFlashMainScript));
        }
        this.mfAssaultRifle = new MuzzleFlashMainScript[10];
        for (m = 0; m < Extensions.get_length(this.mfAssaultRifle); m++)
        {
            this.mfAssaultRifle[m] = (MuzzleFlashMainScript)UnityEngine.Object.Instantiate<GameObject>(this.muzzleFlashes[2]).GetComponent(typeof(MuzzleFlashMainScript));
        }
        this.mfSniper = new MuzzleFlashMainScript[10];
        for (m = 0; m < Extensions.get_length(this.mfSniper); m++)
        {
            this.mfSniper[m] = (MuzzleFlashMainScript)UnityEngine.Object.Instantiate<GameObject>(this.muzzleFlashes[4]).GetComponent(typeof(MuzzleFlashMainScript));
        }
        this.nrOfWeapons = 11;
        this.ammoFullClip = new float[this.nrOfWeapons];
        this.ammoFullClip[0] = (float)1;
        this.ammoFullClip[1] = (float)8;
        this.ammoFullClip[2] = (float)16;
        this.ammoFullClip[3] = (float)24;
        this.ammoFullClip[4] = (float)48;
        this.ammoFullClip[5] = (float)32;
        this.ammoFullClip[6] = (float)8;
        this.ammoFullClip[7] = (float)50;
        this.ammoFullClip[8] = (float)6;
        this.ammoFullClip[9] = (float)16;
        this.ammoFullClip[10] = (float)4;
        this.ammoMax = new float[this.nrOfWeapons];
        this.ammoMax[0] = (float)1;
        this.ammoMax[1] = (float)999;
        this.ammoMax[2] = (float)999;
        this.ammoMax[3] = (float)720;
        this.ammoMax[4] = (float)720;
        this.ammoMax[5] = (float)480;
        this.ammoMax[6] = (float)120;
        this.ammoMax[7] = (float)600;
        this.ammoMax[8] = (float)90;
        this.ammoMax[9] = (float)240;
        this.ammoMax[10] = (float)30;
        this.adjustedWeaponOrder = new int[this.nrOfWeapons];
        for (int n = 0; n < Extensions.get_length(this.adjustedWeaponOrder); n++)
        {
            this.adjustedWeaponOrder[n] = this.getAdjustedWeaponNr(n);
        }
        if (this.rootShared.isDemo && this.rootShared.isNintendoDemo)
        {
            this.rootShared.levelLoadedFromLevelSelectScreen = true;
        }
        UnityScript.Lang.Array array = new UnityScript.Lang.Array();
        if (this.rootShared.levelLoadedFromLevelSelectScreen)
        {
            if (this.rootShared.modAllWeapons || this.weaponToUseWhenLoadingFromLvlSelectScreen >= 1)
            {
                array.Push(1);
            }
            if (this.rootShared.modAllWeapons || this.weaponToUseWhenLoadingFromLvlSelectScreen >= 3)
            {
                array.Push(3);
            }
            if (this.rootShared.modAllWeapons || this.weaponToUseWhenLoadingFromLvlSelectScreen >= 5)
            {
                array.Push(6);
            }
            if (this.rootShared.modAllWeapons || this.weaponToUseWhenLoadingFromLvlSelectScreen >= 6)
            {
                array.Push(5);
            }
        }
        else if (SavedData.HasKey("weapon"))
        {
            for (int num30 = 1; num30 < 7; num30++)
            {
                if (num30 != 2 && num30 != 4 && SavedData.GetInt("weaponActive" + num30) == 1)
                {
                    array.Push(num30);
                }
            }
        }
        this.allowedWeaponsForEnemies = (array.ToBuiltin(typeof(int)) as int[]);
        GameObject gameObject4 = GameObject.Find("Root/TempStartPos");
        if (gameObject4 != null && gameObject4.activeInHierarchy)
        {
            this.playerScript.transform.position = GameObject.Find("TempStartPos").transform.position;
            float x = gameObject4.transform.position.x;
            Vector3 position = this.mainCamera.transform.position;
            float num31 = position.x = x;
            Vector3 vector8 = this.mainCamera.transform.position = position;
            float y = gameObject4.transform.position.y;
            Vector3 position2 = this.mainCamera.transform.position;
            float num32 = position2.y = y;
            Vector3 vector9 = this.mainCamera.transform.position = position2;
            this.rootShared.allowDebugMenu = true;
            this.allowDebug = true;
            gameObject4.SetActive(false);
        }
        this.autoSwing = true;
        if (this.demoLevel)
        {
            this.playerScript.demoLevel = true;
        }
        this.startTime = Time.unscaledTime;
        int num33 = SavedData.GetInt("difficultyMode");
        if (RuntimeServices.EqualityOperator(num33, null))
        {
            num33 = 0;
        }
        this.difficultyMode = num33;
        if (!this.isTutorialLevel && this.difficultyMode == 2)
        {
            this.neverDoDodgeHelper = true;
        }
        if (this.playerScript.onMotorcycle || this.isTutorialLevel)
        {
            this.difficulty = (float)1;
        }
        if (this.killEveryone)
        {
            this.isMiniLevel = true;
            this.prevNrOfEnemiesKilled = -1;
        }
        if (this.isMiniLevel)
        {
            this.miniLevelGoalText = (Text)GameObject.Find("HUD/Canvas").transform.Find("MiniLevelText/Text").GetComponent(typeof(Text));
            this.miniLevelGoalText.transform.parent.gameObject.SetActive(true);
            if (this.killEveryone)
            {
                this.miniLevelGoalText.text = string.Empty;
            }
            this.miniLevelCurTimeUIText = (Text)this.miniLevelGoalText.transform.parent.Find("Timer/CurrentTime").GetComponent(typeof(Text));
            this.miniLevelBestTimeUIText = (Text)this.miniLevelGoalText.transform.parent.Find("Timer/BestTime").GetComponent(typeof(Text));
            int num34 = int.Parse(SceneManager.GetActiveScene().name.Replace("mini_", string.Empty));
            float @float = SavedData.GetFloat("miniTime" + num34);
            if (@float > (float)0)
            {
                this.miniLevelBestTimeUIText.text = "Previous best: " + @float;
            }
            else
            {
                this.miniLevelBestTimeUIText.text = "Previous best: -";
            }
        }
        this.timeSinceEnemySpokeOnDetection = (float)UnityEngine.Random.Range(0, 200);
        this.lastUsedEnemyDetectSpeech = UnityEngine.Random.Range(0, 10);
        this.timeSinceEnemySpokeOnHearing = (float)UnityEngine.Random.Range(0, 200);
        if (SavedData.GetInt("haveSavedGameOptions") == 1)
        {
            float num35 = (float)SavedData.GetInt("screenshake");
            this.optionsScreenShakeMultiplier = num35 / (float)100;
            this.doGore = (SavedData.GetInt("bloodAndGore") == 1);
        }
        if (this.rootShared.chineseBuild)
        {
            this.doGore = false;
        }
        this.setUpHintText();
        this.canvasWeaponPanel = (Canvas)GameObject.Find("HUD/Canvas/WeaponPanel").GetComponent(typeof(Canvas));
        this.canvasBigMultiplier = (Canvas)GameObject.Find("HUD/Canvas/BigMultiplier").GetComponent(typeof(Canvas));
        this.canvasBigScreenReaction = (Canvas)GameObject.Find("HUD/Canvas/BigScreenReaction").GetComponent(typeof(Canvas));
        this.canvasBossHealth = (Canvas)GameObject.Find("HUD/Canvas/BossHealth").GetComponent(typeof(Canvas));
        this.canvasDamageCanvas = (Canvas)GameObject.Find("HUD/Canvas/DamageCanvas").GetComponent(typeof(Canvas));
        this.canvasEnemySpeechHolder = (Canvas)GameObject.Find("HUD/Canvas/EnemySpeechHolder").GetComponent(typeof(Canvas));
        this.canvasHealthAndSlowMo = (Canvas)GameObject.Find("HUD/Canvas/HealthAndSlowMo").GetComponent(typeof(Canvas));
        this.canvasHintsHolder = (Canvas)GameObject.Find("HUD/Canvas/HintsHolder").GetComponent(typeof(Canvas));
        this.canvasInstructionBackground = (Canvas)GameObject.Find("HUD/Canvas/InstructionBackground").GetComponent(typeof(Canvas));
        this.canvasPedroHint = (Canvas)GameObject.Find("HUD/Canvas/PedroHint").GetComponent(typeof(Canvas));
        this.canvasPickupNotificationHolder = (Canvas)GameObject.Find("HUD/Canvas/PickupNotificationHolder").GetComponent(typeof(Canvas));
        this.canvasScoreHud = (Canvas)GameObject.Find("HUD/Canvas/ScoreHud").GetComponent(typeof(Canvas));
        this.canvasWeaponHighlightHolder = (Canvas)GameObject.Find("HUD/Canvas/WeaponHighlightHolder").GetComponent(typeof(Canvas));
        if (this.rootShared.hideHUD)
        {
            int num36 = -10;
            Quaternion localRotation = this.reactionPedro.localRotation;
            Vector3 eulerAngles = localRotation.eulerAngles;
            float num37 = eulerAngles.z = (float)num36;
            Vector3 vector10 = localRotation.eulerAngles = eulerAngles;
            Quaternion quaternion = this.reactionPedro.localRotation = localRotation;
            this.doHideHUD(true);
        }
        this.secretUnlockedCanvasGroup = (CanvasGroup)GameObject.Find("HUD/Canvas/SecretUnlocked").GetComponent(typeof(CanvasGroup));
        this.secretUnlockedText = (Text)this.secretUnlockedCanvasGroup.transform.Find("Outline/Text2").GetComponent(typeof(Text));
        if (this.trailerMode)
        {
            this.scoreHud.gameObject.SetActive(false);
            GameObject.Find("HUD/Canvas/HealthAndSlowMo").SetActive(false);
            GameObject.Find("HUD/Canvas/WeaponPanel").SetActive(false);
            GameObject.Find("HUD/Canvas/Cursors/MainCursor").SetActive(false);
            GameObject.Find("HUD/Canvas/Cursors/SecondaryCursor").SetActive(false);
            GameObject.Find("HUD/Canvas/Cursors/AimHelper").SetActive(false);
            this.allowDebug = true;
            this.playerScript.unlockAllWeapons();
            Cursor.SetCursor(Resources.Load("HUD/trailer_cursor") as Texture2D, new Vector2((float)16, (float)16), CursorMode.Auto);
            this.audioMixer.SetFloat("MusicMasterVolume", (float)-80);
        }
    }

    // Token: 0x06000472 RID: 1138 RVA: 0x0007BB1C File Offset: 0x00079D1C
    public virtual void Update()
    {
        this.unscaledTimescale = Time.unscaledDeltaTime * (float)60;
        this.updateFramesDone = Mathf.Clamp(this.updateFramesDone + 1, 0, 1000);
        if (Time.timeSinceLevelLoad < 0.09f)
        {
            this.startTime = Time.unscaledTime;
        }
        if (!this.dontInitializeMusicAtStart && !this.hasInitializedMusic && this.updateFramesDone > 30)
        {
            this.initializeMusic(this.musicIntro, this.musicLoop);
        }
        this.doCheckpointSave = false;
        this.doCheckpointLoad = false;
        if (this.rootShared.allowDebugMenu)
        {
            if (Input.GetKeyDown("f11"))
            {
                if (this.showNoBlood)
                {
                    this.pleaseESRB = true;
                    this.doGore = false;
                }
                this.showNoBlood = true;
            }
            if (Input.GetKeyDown("f12"))
            {
                Cursor.SetCursor(Resources.Load("HUD/trailer_cursor") as Texture2D, new Vector2((float)16, (float)16), CursorMode.Auto);
                this.scoreHud.gameObject.SetActive(this.trailerMode);
                GameObject.Find("HUD/Canvas/HealthAndSlowMo").SetActive(this.trailerMode);
                GameObject.Find("HUD/Canvas/WeaponPanel").SetActive(this.trailerMode);
                GameObject.Find("HUD/Canvas/Cursors/MainCursor").SetActive(this.trailerMode);
                GameObject.Find("HUD/Canvas/Cursors/SecondaryCursor").SetActive(this.trailerMode);
                GameObject.Find("HUD/Canvas/Cursors/AimHelper").SetActive(this.trailerMode);
                this.trailerMode = !this.trailerMode;
                if (!Input.GetKey(KeyCode.RightShift))
                {
                    this.audioMixer.SetFloat("MusicMasterVolume", (float)-80);
                }
                this.playerScript.unlockAllWeapons();
            }
            if (this.trailerMode)
            {
                this.dontAllowReactionPedroTimer = (float)600;
            }
            if ((this.allowDebug || this.trailerMode) && Input.GetKeyDown("y"))
            {
                this.doCheckpointSave = true;
                this.doCheckpointSaveExternalTrigger = false;
            }
            if (this.haveSaved && (this.allowDebug || this.trailerMode) && Input.GetKeyDown("u"))
            {
                this.doCheckpointLoad = true;
                this.extraCheckpointLoad = 10;
            }
        }
        if (this.extraCheckpointLoad > 0)
        {
            this.doCheckpointLoad = true;
            this.blackFade.color = Color.black;
            float a = 1.1f;
            Color color = this.blackFade.color;
            float num = color.a = a;
            Color color2 = this.blackFade.color = color;
            this.extraCheckpointLoad--;
        }
        if (!this.haveSaved && !this.playerScript.overrideControls && Time.timeSinceLevelLoad > (float)1)
        {
            this.doCheckpointSave = true;
        }
        if (this.hasInitializedMusic && !this.finishedPlayingIntro)
        {
            if (this.musicIntro == null || this.musicLoop == null)
            {
                this.finishedPlayingIntro = true;
            }
            else if (this.musicAudioSource2.time > 0.01f)
            {
                this.finishedPlayingIntro = true;
            }
        }
        if (this.rootShared.allowDebugMenu && !this.allowDebug && ((Input.GetKey("q") && Input.GetKey("m")) || (Input.GetAxis("DPadYP1") >= 0.9f && Input.GetButton("ActionModeP1"))))
        {
            this.allowDebug = true;
        }
        if (!this.dead)
        {
            if (this.deadDoOnce)
            {
                if (this.rootShared.curVisualQualityLevel == 0)
                {
                    this.camVignette.enabled = false;
                    this.camColourCorrection.enabled = false;
                }
                this.doHideHUD(this.rootShared.hideHUD);
                this.deadDoOnce = false;
            }
            if (this.forceCheckpointSave && !this.trailerMode)
            {
                this.doCheckpointSave = true;
                this.forceCheckpointSave = false;
            }
            if (this.doCheckpointSaveExternalTrigger && !this.trailerMode)
            {
                if (this.autoSaveTimer < (float)10)
                {
                    this.autoSaveTimer = (float)10;
                }
                this.enemyEngagedWithPlayerDoOnce = true;
            }
            if (this.meleeHintCoolDown > (float)0)
            {
                this.meleeHintCoolDown -= this.timescale;
            }
            if (this.enemyEngagedWithPlayer)
            {
                this.autoSaveTimer = (float)0;
                if (!this.enemyEngagedWithPlayerDoOnce)
                {
                    this.enemyEngagedWithPlayerDoOnce = true;
                }
            }
            else
            {
                if (this.playerScript.bulletHit)
                {
                    this.autoSaveTimer = (float)0;
                }
                if (this.enemyEngagedWithPlayerDoOnce && (this.playerScript.onGround || this.playerScript.swinging) && !this.playerScript.startedRolling && !this.playerScript.kJump && this.playerScript.kJumpDownTimer <= (float)0)
                {
                    this.autoSaveTimer += this.timescale;
                }
                if (this.autoSaveTimer > (float)20 && this.enemyEngagedWithPlayerDoOnce)
                {
                    if (!this.isMiniLevel && !this.playerScript.onMotorcycle)
                    {
                        if (this.playerScript.bulletHitsSinceDodgeUsed > 5 && this.playerScript.timeSinceDodgeUsed > (float)1200)
                        {
                            this.playerScript.timeSinceDodgeUsed = (float)0;
                            this.StartCoroutine(this.doPedroHint(this.GetTranslation("pHint1")));
                        }
                        else if (this.playerScript.bulletHitsSinceSlowMotionUsed > 10 && this.timeSinceSlowMotionUsed > (float)3600 && this.slowMotionAmount >= (float)1)
                        {
                            this.timeSinceSlowMotionUsed = (float)0;
                            this.playerScript.bulletHitsSinceSlowMotionUsed = 0;
                            this.StartCoroutine(this.doPedroHint(this.GetTranslation("pHint2")));
                        }
                        else if (this.playerScript.aimWithLeftArm && this.playerScript.timeSinceSplitAim > (float)2100)
                        {
                            this.playerScript.timeSinceSplitAim = (float)-1000;
                            this.StartCoroutine(this.doPedroHint(this.GetTranslation("pHint3")));
                        }
                        else if (this.playerScript.weapon == 9 && this.playerScript.timeSinceSniperAim > (float)1500)
                        {
                            this.playerScript.timeSinceSniperAim = (float)0;
                            this.StartCoroutine(this.doPedroHint(this.GetTranslation("pHint4")));
                        }
                        else if (this.playerScript.weapon == 5 && this.playerScript.timeSinceGrenadeLaunched > (float)1500)
                        {
                            this.playerScript.timeSinceGrenadeLaunched = (float)0;
                            this.StartCoroutine(this.doPedroHint(this.GetTranslation("pHint5")));
                        }
                        else if (this.doMeleeHint && this.meleeHintCoolDown <= (float)0 && this.playerScript.timeSinceMeleeUsed > (float)1000)
                        {
                            this.doMeleeHint = false;
                            this.StartCoroutine(this.doPedroHint(this.GetTranslation("pHint6")));
                            if (this.pedroHintTimer > (float)0)
                            {
                                this.meleeHintCoolDown = (float)3600;
                            }
                        }
                        if (!this.trailerMode)
                        {
                            this.doCheckpointSave = true;
                            this.doCheckpointSaveExternalTrigger = false;
                            this.autoSaveTimer = (float)0;
                        }
                    }
                    this.enemyEngagedWithPlayerDoOnce = false;
                }
            }
            this.enemyEngagedWithPlayer = false;
            if (!this.playerScript.overrideControls && (!this.sbClickCont || this.sbClickContDontFreeze) && !this.levelEnded && !this.paused && Time.timeSinceLevelLoad > (float)1)
            {
                if (this.player.GetButtonDown("Focus"))
                {
                    if (MultiplayerManagerTest.inst.gamemode == MPGamemodes.PvP)
                        return;

                    if (this.slowMotionAmount > (float)0 && !this.dontAllowActionMode)
                    {
                        this.actionModeActivated = !this.actionModeActivated;
                    }
                    else
                    {
                        this.actionModeActivated = false;
                    }
                }
                this.kAction = this.actionModeActivated;
            }
            if (this.playerScript.overrideControls)
            {
                this.dodgeHelper = false;
            }
            if (this.neverDoDodgeHelper)
            {
                this.dodgeHelper = false;
            }
            else
            {
                this.dodgeHelperCoolDown = Mathf.Clamp(this.dodgeHelperCoolDown - this.timescale, (float)0, this.dodgeHelperCoolDown);
                if (!this.prevDodgeHelper && this.dodgeHelper)
                {
                    if (this.playerScript.dodging || this.playerScript.onMotorcycle || this.dodgeHelperClosestDistance < (float)2 || (!this.alwaysDoDodgeHelper && (this.playerScript.health - this.calculateBulletHitStrength(this.dodgeHelperDamage) > (float)0 || this.dodgeHelperCoolDown > (float)0)))
                    {
                        this.dodgeHelper = false;
                    }
                    else
                    {
                        this.dodgeStateAudioSnapshot.TransitionTo(0.05f);
                        this.lastActivatedAudioSnapshot = 3;
                    }
                }
                if (this.playerScript.dodging || this.playerScript.startedRolling || this.playerScript.dodgingCoolDown > (float)0 || ((this.playerScript.onGround || this.playerScript.swinging) && this.playerScript.kCrouch))
                {
                    this.dodgeHelper = false;
                }
            }
            if (!this.dodgeHelper && !this.paused && (this.lastActivatedAudioSnapshot == 3 || this.lastActivatedAudioSnapshot == 8))
            {
                if (this.kAction)
                {
                    this.actionStateAudioSnapshot.TransitionTo(0.1f);
                    this.lastActivatedAudioSnapshot = 1;
                }
                else
                {
                    if (!this.playerScript.onMotorcycle)
                    {
                        this.normalStateAudioSnapshot.TransitionTo(0.1f);
                    }
                    else
                    {
                        this.motorcycleNormalStateAudioSnapshot.TransitionTo(0.1f);
                    }
                    this.lastActivatedAudioSnapshot = 0;
                }
            }
            this.prevDodgeHelper = this.dodgeHelper;
            this.showDodgeAlert = this.dodgeHelper;
            if (!this.dodgeHelper || this.dodgeHelperTransform == null)
            {
                this.dodgeHelperClosestDistance = (float)999999;
                this.dodgeHelperDamage = (float)0;
                this.dodgeHelperTransform = null;
            }
            this.dodgeHelperDamage = (float)0;
            if (this.showDodgeAlert)
            {
                if (!this.showDodgeAlertDoOnce)
                {
                    this.camColourCorrection.enabled = true;
                    this.showDodgeAlertDoOnce = true;
                }
                this.showHintDodge = true;
                if (this.scoreHudCanvasGroup.alpha != 0.15f || this.healthAndSlowMoCanvasGroup.alpha != 0.15f || this.weaponPanelCanvasGroup.alpha != 0.15f)
                {
                    this.scoreHudCanvasGroup.alpha = (this.healthAndSlowMoCanvasGroup.alpha = (this.weaponPanelCanvasGroup.alpha = 0.15f));
                }
            }
            else
            {
                if (this.showDodgeAlertDoOnce)
                {
                    if (this.rootShared.curVisualQualityLevel == 0)
                    {
                        this.camColourCorrection.enabled = false;
                    }
                    this.showDodgeAlertDoOnce = false;
                }
                if (this.scoreHudCanvasGroup.alpha != (float)1 || this.healthAndSlowMoCanvasGroup.alpha != (float)1 || this.weaponPanelCanvasGroup.alpha != (float)1)
                {
                    this.scoreHudCanvasGroup.alpha = (this.healthAndSlowMoCanvasGroup.alpha = (this.weaponPanelCanvasGroup.alpha = (float)1));
                }
            }
            this.prevUnityTimescale = this.unityTimescale;
            if (this.playerScript.kChangeWeapon)
            {
                this.unityTimescale = this.DampUnscaled(0.03f, this.unityTimescale, 0.2f);
                this.targetUnityTimescale = 0.03f;
            }
            else if (this.dodgeHelper)
            {
                this.playerScript.recoverTimer = (float)0;
                this.dodgeHelperCoolDown = Mathf.Clamp(this.dodgeHelperCoolDown + Time.unscaledDeltaTime * (float)300, (float)0, (float)180);
                this.unityTimescale = this.DampUnscaled(0.03f, this.unityTimescale, 0.5f);
                this.targetUnityTimescale = 0.03f;
                this.dodgeHelper = false;
            }
            else
            {
                this.bulletHitFreezeTimer = Mathf.Clamp(this.bulletHitFreezeTimer - (float)1, (float)0, (float)60);
                if (this.bulletHitFreezeTimer > (float)0)
                {
                    this.unityTimescale = this.DampUnscaled((!this.kAction) ? ((float)1) : (Mathf.Clamp(this.rootShared.modFocusSlowdownScale, (float)3, (float)100) * 0.01f), this.unityTimescale, 0.05f);
                    this.targetUnityTimescale = ((!this.kAction) ? ((float)1) : (Mathf.Clamp(this.rootShared.modFocusSlowdownScale, (float)3, (float)100) * 0.01f));

                }
                else
                {
                    this.unityTimescale = this.DampUnscaled((!this.kAction) ? ((float)1) : (Mathf.Clamp(this.rootShared.modFocusSlowdownScale, (float)3, (float)100) * 0.01f), this.unityTimescale, 0.3f);
                    this.targetUnityTimescale = ((!this.kAction) ? ((float)1) : (Mathf.Clamp(this.rootShared.modFocusSlowdownScale, (float)3, (float)100) * 0.01f));

                }
            }
            if (this.instructionBackground.gameObject.activeInHierarchy)
            {
                float x = this.Damp((float)0, this.instructionBackground.anchoredPosition.x, 0.2f);
                Vector2 anchoredPosition = this.instructionBackground.anchoredPosition;
                float num2 = anchoredPosition.x = x;
                Vector2 vector = this.instructionBackground.anchoredPosition = anchoredPosition;
            }
            if (this.rootShared.allowDebugMenu && this.allowDebug)
            {
                if (Input.GetKey("x"))
                {
                    this.unityTimescale = (float)6;
                }
                if (Input.GetKeyDown("f3") || (Input.GetButton("RightStickClickP1") && Input.GetButton("ReloadP1")))
                {
                    this.kAction = false;
                    this.unityTimescale = (float)1;
                    Time.timeScale = this.startUnityTimeScale;
                    Time.fixedDeltaTime = this.startUnityFixedDeltaTime;
                    Time.maximumDeltaTime = this.startUnityMaximumDeltaTime;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
                if (Input.GetKeyDown("f4") || (Input.GetButton("RightStickClickP1") && Input.GetButton("UseP1")))
                {
                    this.kAction = false;
                    this.unityTimescale = (float)1;
                    Time.timeScale = this.startUnityTimeScale;
                    Time.fixedDeltaTime = this.startUnityFixedDeltaTime;
                    Time.maximumDeltaTime = this.startUnityMaximumDeltaTime;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if (Input.GetKeyDown("f5"))
                {
                    this.kAction = false;
                    this.unityTimescale = (float)1;
                    Time.timeScale = this.startUnityTimeScale;
                    Time.fixedDeltaTime = this.startUnityFixedDeltaTime;
                    Time.maximumDeltaTime = this.startUnityMaximumDeltaTime;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                if (Input.GetKeyDown("f8"))
                {
                    this.pedroHintTimer = (float)-99999;
                    this.StartCoroutine(this.doPedroHint("This is a test."));
                }
                if (Input.GetKeyDown("f9"))
                {
                    this.rootShared.godMode = !this.rootShared.godMode;
                    this.pedroHintTimer = (float)-99999;
                    this.StartCoroutine(this.doPedroHint("DEBUG GOD MODE: " + this.rootShared.godMode));
                }
            }
            this.timescale = Time.deltaTime * (float)60;
        }
        else
        {
            if (!this.deadDoOnce)
            {
                this.record.cancelCaptureMoment();
                this.camVignette.enabled = true;
                this.camColourCorrection.enabled = true;
                this.bigText.transform.localScale = Vector3.one * (float)50;
                this.blackFade.enabled = true;
                this.fadeToBlackDoOnce = false;
                this.blackFade.color = Color.white;
                float a2 = 2.5f;
                Color color3 = this.blackFade.color;
                float num3 = color3.a = a2;
                Color color4 = this.blackFade.color = color3;
                int num4 = 0;
                Color color5 = this.bigFace.color;
                float num5 = color5.a = (float)num4;
                Color color6 = this.bigFace.color = color5;
                this.deathTimer = (float)0;
                this.deadStateAudioSnapshot.TransitionTo(0.5f);
                this.lastActivatedAudioSnapshot = 4;
                this.bigText.text = this.GetTranslation("dead");
                this.bigText.color = Color.black;
                this.bigTextOutline.effectColor = this.bigTextOutlineStartColour;
                float num6 = 0.25f;
                Vector2 effectDistance = this.bigTextOutline.effectDistance;
                float num7 = effectDistance.y = num6;
                Vector2 vector2 = this.bigTextOutline.effectDistance = effectDistance;
                float x2 = num6;
                Vector2 effectDistance2 = this.bigTextOutline.effectDistance;
                float num8 = effectDistance2.x = x2;
                Vector2 vector3 = this.bigTextOutline.effectDistance = effectDistance2;
                this.bigTextOutline.enabled = true;
                this.bigScreenReactionCanvas.enabled = true;
                this.secretUnlockedCanvasGroup.gameObject.SetActive(false);
                this.bigMultiplier.text = string.Empty;
                this.canvasHintsHolder.enabled = true;
                this.deadDoOnce = true;
            }
            this.showHintDied = true;
            // this.timescaleRaw *= 0.9f;
            // this.timescale = Time.deltaTime * (float)60 * this.timescaleRaw;
            this.dodgeHelper = (this.showDodgeAlert = false);
            this.bigText.transform.localScale = this.bigText.transform.localScale + (Vector3.one - this.bigText.transform.localScale) * Mathf.Clamp01(0.4f * this.unscaledTimescale);
            float a3 = Mathf.Clamp01(this.blackFade.color.a - 0.16f * this.unscaledTimescale);
            Color color7 = this.blackFade.color;
            float num9 = color7.a = a3;
            Color color8 = this.blackFade.color = color7;
            this.blackFade.transform.SetAsLastSibling();
            this.deathTimer += Time.unscaledDeltaTime * (float)60;
            if (this.deathTimer > (float)150 && this.pedroDeathHintText != string.Empty)
            {
                this.pedroHintTimer = (float)-9999;
                this.StartCoroutine(this.doPedroHint(this.pedroDeathHintText));
                this.pedroDeathHintText = string.Empty;
            }
            if (this.player.GetButtonDown("Interact") || this.player.GetButtonDown("UIStart"))
            {
                this.blackFade.enabled = true;
                this.fadeToBlackDoOnce = false;
                this.blackFade.color = Color.black;
                int num10 = 2;
                Color color9 = this.blackFade.color;
                float num11 = color9.a = (float)num10;
                Color color10 = this.blackFade.color = color9;
                int num12 = 0;
                Color color11 = this.bigText.color;
                float num13 = color11.a = (float)num12;
                Color color12 = this.bigText.color = color11;
                this.pedroHintTimer = (float)0;
                this.nrOfDeaths++;
                this.kAction = false;
                this.unityTimescale = (float)1;
                this.targetUnityTimescale = (float)1;
                Time.timeScale = this.startUnityTimeScale;
                Time.fixedDeltaTime = this.startUnityFixedDeltaTime;
                Time.maximumDeltaTime = this.startUnityMaximumDeltaTime;
                if (this.haveSaved && !this.dontAllowCheckpointSave)
                {
                    this.doCheckpointLoad = true;
                    this.extraCheckpointLoad = 10;
                }
                else
                {
                    this.restartLevel();
                }
            }
        }
        if (this.kAction)
        {
            if (!this.kActionDoOnce)
            {
                this.screenCornerSlowMotionEffect.enabled = true;
                int num14 = 0;
                Color color13 = this.screenCornerSlowMotionEffect.color;
                float num15 = color13.a = (float)num14;
                Color color14 = this.screenCornerSlowMotionEffect.color = color13;
                this.kActionDoOnce = true;
            }
            if (this.screenCornerSlowMotionEffect.color.a < (float)1)
            {
                float a4 = Mathf.Clamp01(this.screenCornerSlowMotionEffect.color.a + 0.07f * this.timescale);
                Color color15 = this.screenCornerSlowMotionEffect.color;
                float num16 = color15.a = a4;
                Color color16 = this.screenCornerSlowMotionEffect.color = color15;
                RenderSettings.fogColor = Color.Lerp(this.startFogColor, this.slowMoFogColor, this.screenCornerSlowMotionEffect.color.a);
            }
            if (!this.playerScript.kChangeWeapon)
            {
                this.slowMotionAmount -= this.timescale / (float)200;
                this.timeSinceSlowMotionUsed = (float)0;
                this.playerScript.bulletHitsSinceSlowMotionUsed = 0;
            }
            if (this.slowMotionAmount < (float)0)
            {
                int num17 = 50;
                Vector2 sizeDelta = this.slowMoIcon.sizeDelta;
                float num18 = sizeDelta.y = (float)num17;
                Vector2 vector4 = this.slowMoIcon.sizeDelta = sizeDelta;
                int num19 = num17;
                Vector2 sizeDelta2 = this.slowMoIcon.sizeDelta;
                float num20 = sizeDelta2.x = (float)num19;
                Vector2 vector5 = this.slowMoIcon.sizeDelta = sizeDelta2;
                this.slowMoIconImage.color = Color.red;
                this.slowMoIconShakeTimer = (float)100;
                this.actionModeActivated = false;
                this.kAction = false;
                this.slowMotionAmount = (float)0;
            }
            if (this.unlimitedSlowMotion || this.rootShared.modInfiniteFocus)
            {
                this.slowMotionAmount = (float)1;
            }
            if (!this.slowMoAudioEffectDoOnce)
            {
                if (!this.showDodgeAlert)
                {
                    this.actionStateAudioSnapshot.TransitionTo(0.2f);
                    this.lastActivatedAudioSnapshot = 1;
                }
                this.slowMoAudioEffectDoOnce = true;
            }
            if (!this.camTransitionDoOnce)
            {
                this.camTransitionDoOnce = true;
            }
            if (this.camTransitionValue < (float)1)
            {
                this.camTransitionValue += 0.1f * this.timescale;
            }
            else
            {
                this.camTransitionValue = (float)1;
            }
            float num21 = this.Damp((float)35, this.slowMoIcon.sizeDelta.x, 0.3f);
            Vector2 sizeDelta3 = this.slowMoIcon.sizeDelta;
            float num22 = sizeDelta3.y = num21;
            Vector2 vector6 = this.slowMoIcon.sizeDelta = sizeDelta3;
            float x3 = num21;
            Vector2 sizeDelta4 = this.slowMoIcon.sizeDelta;
            float num23 = sizeDelta4.x = x3;
            Vector2 vector7 = this.slowMoIcon.sizeDelta = sizeDelta4;
            this.slowMoIconImage.color = this.slowMoIconImage.color + (Color.Lerp(this.slowMoIconImageStartColour, Color.white, 0.5f) - this.slowMoIconImage.color) * Mathf.Clamp01(0.3f * this.timescale);
        }
        else
        {
            if (this.kActionDoOnce)
            {
                this.kActionDoOnce = false;
            }
            if (this.screenCornerSlowMotionEffect.color.a > (float)0)
            {
                float a5 = Mathf.Clamp01(this.screenCornerSlowMotionEffect.color.a - 0.07f * this.timescale);
                Color color17 = this.screenCornerSlowMotionEffect.color;
                float num24 = color17.a = a5;
                Color color18 = this.screenCornerSlowMotionEffect.color = color17;
                RenderSettings.fogColor = Color.Lerp(this.startFogColor, this.slowMoFogColor, this.screenCornerSlowMotionEffect.color.a);
                if (this.screenCornerSlowMotionEffect.color.a <= (float)0)
                {
                    this.screenCornerSlowMotionEffect.enabled = false;
                }
            }
            this.timeSinceSlowMotionUsed += this.timescale;
            this.slowMotionAmount = Mathf.Clamp01(this.slowMotionAmount + Mathf.Clamp01((this.timeSinceSlowMotionUsed - (float)60) * 0.0005f) * 0.015f * this.timescale);
            if (this.slowMoAudioEffectDoOnce)
            {
                if (!this.showDodgeAlert)
                {
                    if (!this.playerScript.onMotorcycle)
                    {
                        this.normalStateAudioSnapshot.TransitionTo(0.4f);
                    }
                    else
                    {
                        this.motorcycleNormalStateAudioSnapshot.TransitionTo(0.4f);
                    }
                    this.lastActivatedAudioSnapshot = 0;
                }
                this.slowMoAudioEffectDoOnce = false;
            }
            if (this.camTransitionValue > (float)0)
            {
                this.camTransitionValue -= 0.1f * this.timescale;
            }
            else
            {
                if (this.camTransitionDoOnce)
                {
                    this.camTransitionDoOnce = false;
                }
                this.camTransitionValue = (float)0;
            }
            float num25 = this.Damp((float)22, this.slowMoIcon.sizeDelta.x, 0.1f);
            Vector2 sizeDelta5 = this.slowMoIcon.sizeDelta;
            float num26 = sizeDelta5.y = num25;
            Vector2 vector8 = this.slowMoIcon.sizeDelta = sizeDelta5;
            float x4 = num25;
            Vector2 sizeDelta6 = this.slowMoIcon.sizeDelta;
            float num27 = sizeDelta6.x = x4;
            Vector2 vector9 = this.slowMoIcon.sizeDelta = sizeDelta6;
            this.slowMoIconImage.color = this.slowMoIconImage.color + (this.slowMoIconImageStartColour - this.slowMoIconImage.color) * Mathf.Clamp01(0.1f * this.timescale);
            if (this.slowMoIconShakeTimer > (float)0)
            {
                this.slowMoIconShakeTimer -= this.timescale;
                float x5 = this.slowMoIconStartPos.x + Mathf.Sin(this.slowMoIconShakeTimer * 0.6f) * (float)5 * (this.slowMoIconShakeTimer / (float)100);
                Vector2 anchoredPosition2 = this.slowMoIcon.anchoredPosition;
                float num28 = anchoredPosition2.x = x5;
                Vector2 vector10 = this.slowMoIcon.anchoredPosition = anchoredPosition2;
                this.slowMoIconImage.color = Color.Lerp(this.slowMoIconImage.color, Color.red, this.slowMoIconShakeTimer / (float)100);
                this.slowMotionBarHolder.color = this.slowMoIconImage.color;
                if (this.slowMoIconShakeTimer <= (float)0)
                {
                    this.slowMotionBarHolder.color = this.slowMoIconImageStartColour;
                }
            }
        }
        float x6 = this.slowMotionAmount;
        Vector3 localScale = this.slowMotionBarRect.localScale;
        float num29 = localScale.x = x6;
        Vector3 vector11 = this.slowMotionBarRect.localScale = localScale;
        if (this.playerScript.kChangeWeapon)
        {
            this.camTransitionDoOnce = true;
            this.camVignette.intensity = this.Damp(0.4f, this.camVignette.intensity, 0.6f);
            this.camVignette.blur = this.Damp(0.6f, this.camVignette.blur, 0.5f);
            this.camVignette.chromaticAberration = this.Damp((float)15, this.camVignette.chromaticAberration, 0.5f);
            if (!this.playerScriptKChangeWeaponDoOnce)
            {
                this.camVignette.enabled = true;
                this.hudAudio2.clip = this.weaponSelectAppearSound;
                this.hudAudio2.loop = false;
                this.hudAudio2.volume = UnityEngine.Random.Range(0.5f, 0.6f);
                this.hudAudio2.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                this.hudAudio2.Play();
                this.playerScriptKChangeWeaponDoOnce = true;
            }
            if (this.lastActivatedAudioSnapshot != 6)
            {
                this.weaponSelectStateAudioSnapshot.TransitionTo(0.08f);
                this.lastActivatedAudioSnapshot = 6;
            }
        }
        else
        {
            this.camVignette.intensity = Mathf.Lerp(0.1f, -0.25f, this.camTransitionValue);
            this.camVignette.blur = Mathf.Clamp01(Mathf.Clamp(((float)1 - this.playerScript.recoverTimer + ((float)1 - this.playerScript.health) * 1.5f) / 3.5f, (float)0, 0.7f) + this.playerScript.healthPackEffect * 0.7f + 0.1f + this.camTransitionValue * 0.4f);
            this.camVignette.chromaticAberration = Mathf.Lerp((float)0, (float)-3, this.camTransitionValue);
            if (this.playerScriptKChangeWeaponDoOnce)
            {
                if (this.rootShared.curVisualQualityLevel == 0)
                {
                    this.camVignette.enabled = false;
                }
                this.hudAudio2.clip = this.weaponSelectAppearSound;
                this.hudAudio2.loop = false;
                this.hudAudio2.volume = UnityEngine.Random.Range(0.4f, 0.5f);
                this.hudAudio2.pitch = UnityEngine.Random.Range(0.45f, 0.55f);
                this.hudAudio2.Play();
                this.playerScriptKChangeWeaponDoOnce = false;
            }
            if (this.lastActivatedAudioSnapshot == 6)
            {
                if (this.kAction)
                {
                    this.actionStateAudioSnapshot.TransitionTo(0.2f);
                    this.lastActivatedAudioSnapshot = 1;
                }
                else
                {
                    if (!this.playerScript.onMotorcycle)
                    {
                        this.normalStateAudioSnapshot.TransitionTo(0.2f);
                    }
                    else
                    {
                        this.motorcycleNormalStateAudioSnapshot.TransitionTo(0.2f);
                    }
                    this.lastActivatedAudioSnapshot = 0;
                }
            }
        }
        if (!this.rootShared.modSideOnCamera)
        {
            this.mainCamera.fieldOfView = Mathf.Lerp((float)60, (float)59, this.camTransitionValue);
        }
        if (!this.levelEnded)
        {
            if (this.updateFramesDone > 30 && !this.dead && (this.player.GetButtonDown("UIStart") || PlatformManager.overlayActive) && !this.paused)
            {
                this.doPause();
            }
            if (this.levelEndedDoOnce)
            {
                this.levelEndedDoOnce = false;
            }
        }
        else if (!this.levelEndedDoOnce)
        {
            this.cCheckSc = true;
            this.score += this.tempScore * this.multiplier;
            this.cCheckTSc = true;
            this.tempScore = (float)0;
            this.cCheckMu = true;
            this.multiplier = (float)0;
            this.bigMultiplier.text = string.Empty;
            this.paused = true;
            this.finishTime = Time.unscaledTime - this.timePaused;
            this.pickupNotificationHolder.gameObject.SetActive(false);
            this.levelEndScreen.gameObject.SetActive(true);
            this.levelEndScreen.transform.SetAsLastSibling();
            this.audioMixer.updateMode = AudioMixerUpdateMode.UnscaledTime;
            this.levelCompleteStateAudioSnapshot.TransitionTo(1.2f);
            this.lastActivatedAudioSnapshot = 5;
            if (!this.playerFiredWeapon)
            {
                this.statsTracker.lvlsCompletedWithoutFiringWeapon = this.statsTracker.lvlsCompletedWithoutFiringWeapon + 1;
                this.statsTracker.achievementCheck();
            }
            this.levelEndedDoOnce = true;
        }
        if (this.paused || this.extraCheckpointLoad > 0)
        {
            if (!this.pausedDoOnce)
            {
                this.timePausedTemp = Time.unscaledTime;
                this.record.doPause();
                this.audioMixer.updateMode = AudioMixerUpdateMode.UnscaledTime;
                this.pausedDoOnce = true;
            }
            if (!this.levelEnded && this.lastActivatedAudioSnapshot != 8)
            {
                if (!this.finishedPlayingIntro)
                {
                    this.pauseAudioSnapshotNoPitchChange.TransitionTo(0.3f);
                }
                else
                {
                    this.pauseAudioSnapshot.TransitionTo(0.3f);
                }
                this.lastActivatedAudioSnapshot = 8;
            }
            this.showDodgeAlert = false;
            this.showHintDodge = false;
            // Time.timeScale = (float)0;
        }
        else
        {
            if (this.player.GetButtonDown("Restart Level"))
            {
                this.unityTimescale = (float)1;
                this.targetUnityTimescale = (float)1;
                Time.timeScale = this.startUnityTimeScale;
                Time.fixedDeltaTime = this.startUnityFixedDeltaTime;
                Time.maximumDeltaTime = this.startUnityMaximumDeltaTime;
                this.restartLevel();
            }
            if (this.pausedDoOnce)
            {
                this.timePaused += Time.unscaledTime - this.timePausedTemp;
                if (!this.levelEnded)
                {
                    this.record.doRecord();
                }
                this.audioMixer.updateMode = AudioMixerUpdateMode.Normal;
                this.pausedDoOnce = false;
            }
            Time.timeScale = this.startUnityTimeScale * this.unityTimescale;
            Time.fixedDeltaTime = this.startUnityFixedDeltaTime * this.unityTimescale;
            Time.maximumDeltaTime = this.startUnityMaximumDeltaTime / this.unityTimescale;
        }
        ParticleSystem.MainModule mainModule = default(ParticleSystem.MainModule);

        ParticleSystem.MainModule bullethitParticleMainModule = bulletHitParticle.main;
        bullethitParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule bloodMistParticleMainModule = bloodMistParticle.main;
        bloodMistParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule sparksParticleMainModule = sparksParticle.main;
        sparksParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule bloodDropsParticleMainModule = bloodDropsParticle.main;
        bloodDropsParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule pistolClipParticleMainModule = pistolClipParticle.main;
        pistolClipParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule glassParticleMainModule = glassParticle.main;
        glassParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule debrisParticleMainModule = debrisParticle.main;
        debrisParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule shellParticleMainModule = shellParticle.main;
        shellParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule smokeParticleMainModule = smokeParticle.main;
        smokeParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule smokeSpriteParticleMainModule = smokeSpriteParticle.main;
        smokeSpriteParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule rockParticleMainModule = rockParticle.main;
        rockParticleMainModule.simulationSpeed = timescaleRaw;

        ParticleSystem.MainModule destroyParticleGlassMainModule = destroyParticleGlass.main;
        destroyParticleGlassMainModule.simulationSpeed = timescaleRaw;


        //  this.bulletHitParticle.main.simulationSpeed = this.timescaleRaw;
        //  this.sparksParticle.main.simulationSpeed = this.timescaleRaw;
        //  this.bloodMistParticle.main.simulationSpeed = this.timescaleRaw;
        //  this.bloodDropsParticle.main.simulationSpeed = this.timescaleRaw;
        // this.pistolClipParticle.main.simulationSpeed = this.timescaleRaw;
        //   this.glassParticle.main.simulationSpeed = this.timescaleRaw;
        //   this.destroyParticleGlass.main.simulationSpeed = this.timescaleRaw;
        //   this.debrisParticle.main.simulationSpeed = this.timescaleRaw;
        //   this.shellParticle.main.simulationSpeed = this.timescaleRaw;
        //   this.smokeParticle.main.simulationSpeed = this.timescaleRaw;
        //   this.smokeSpriteParticle.main.simulationSpeed = this.timescaleRaw;
        //   this.rockParticle.main.simulationSpeed = this.timescaleRaw;
        if (this.actionModeActivated)
        {
            if (this.playerScript.fireWeapon)
            {
                this.timeInActionMode = (float)0;
            }
            if (this.playerScript.reloading && this.timeInActionMode > (float)60)
            {
                this.actionModeActivatedDoOnce2 = false;
                this.timeInActionMode = (float)60;
            }
            if (this.timeInActionMode < (float)90)
            {
                this.timeInActionMode += this.timescale;
            }
            else
            {
                this.showHintFocus = true;
                if (!this.actionModeActivatedDoOnce || !this.actionModeActivatedDoOnce2)
                {
                    this.actionModeActivatedDoOnce2 = true;
                    this.actionModeActivatedDoOnce = true;
                }
            }
        }
        else
        {
            if (this.actionModeActivatedDoOnce)
            {
                this.actionModeActivatedDoOnce2 = false;
                this.actionModeActivatedDoOnce = false;
            }
            this.timeInActionMode = (float)0;
        }
        if (this.fadeToBlack)
        {
            if (this.fadeToBlackDoOnce)
            {
                int num30 = 0;
                Color color19 = this.blackFade.color;
                float num31 = color19.a = (float)num30;
                Color color20 = this.blackFade.color = color19;
                this.blackFade.enabled = true;
                this.fadeToBlackDoOnce = false;
            }
            if (this.blackFade.color.a < (float)1)
            {
                float a6 = this.blackFade.color.a + 0.03f * (Time.unscaledDeltaTime * (float)60);
                Color color21 = this.blackFade.color;
                float num32 = color21.a = a6;
                Color color22 = this.blackFade.color = color21;
                if (this.fadeOutSound)
                {
                    this.audioMixer.SetFloat("MasterVolume", Mathf.Clamp(this.blackFade.color.a * (float)-80, (float)-80, (float)0));
                }
            }
            else if (this.levelToLoad != 0)
            {
                this.saveProgress();
                this.unityTimescale = (float)1;
                this.targetUnityTimescale = (float)1;
                Time.timeScale = this.startUnityTimeScale;
                Time.fixedDeltaTime = this.startUnityFixedDeltaTime;
                Time.maximumDeltaTime = this.startUnityMaximumDeltaTime;
                MonoBehaviour.print("-------------------- LEVEL FINISHED ----------------------- \n Name: " + SceneManager.GetActiveScene().name + "\n Completion time: " + (this.finishTime - this.startTime) + "\n Kills: " + this.nrOfEnemiesKilled + "/" + this.nrOfEnemiesTotal + "\n Score: " + this.score + "\n Player health: " + this.playerScript.health + "\n Difficulty: " + this.difficulty + "\n Active weapon: " + this.playerScript.weapon + "\n------------------------ END OF PRINT --------------------");
                this.rootShared.loadingScreenLevelToLoad = this.levelToLoad;
                SceneManager.LoadScene("_Loading_Screen");
            }
        }
        else if (this.updateFramesDone > 10 && !this.doCheckpointLoad)
        {
            if (this.blackFade.color.a > (float)0)
            {
                float a7 = this.blackFade.color.a - 0.022f * this.blackFadeSpeed * this.unscaledTimescale;
                Color color23 = this.blackFade.color;
                float num33 = color23.a = a7;
                Color color24 = this.blackFade.color = color23;
                if (this.fadeInSound)
                {
                    this.audioMixer.SetFloat("MasterVolume", Mathf.Clamp(this.blackFade.color.a * (float)-15, (float)-80, (float)0));
                }
            }
            else if (!this.fadeToBlackDoOnce)
            {
                this.blackFade.enabled = false;
                this.fadeToBlackDoOnce = true;
            }
        }
        this.framesSinceBloodSplatter = Mathf.Clamp(this.framesSinceBloodSplatter + (float)1, (float)0, (float)60);
        this.cameraScript.ResetTrackPos();
        if (this.disableOutlineEffect)
        {
            this.doDisableOutlineEffect();
        }
        this.disableOutlineEffect = true;
        if (this.sbClickCont || this.sbTimer > (float)0)
        {
            if (this.sbAppearDelay > (float)0)
            {
                this.sbAppearDelay -= this.timescale;
            }
            else
            {
                if (!this.sbDoOnce)
                {
                    this.voiceController.voiceTimer = (float)0;
                    this.voice.Play();
                    this.voice.transform.position = this.sbTransform.position;
                    this.sbBubble.gameObject.SetActive(true);
                    this.sbTail.gameObject.SetActive(true);
                    this.setSpeechBubbleStartPos();
                    this.setSpeechBubbleSize();
                    this.sbDoOnce = true;
                }
                if (!RuntimeServices.EqualityOperator(this.sbTransform.position, null))
                {
                    this.voice.transform.position = this.sbTransform.position;
                }
                this.sbTimer -= this.timescale * this.sbTimerMultiplier;
                if (this.rootShared.allowDebugMenu && this.allowDebug && Input.GetKey("z") && this.sbClickCont)
                {
                    this.sbTimer = (float)-999;
                }
                if (this.sbClickCont)
                {
                    if (!this.sbClickContDontFreeze)
                    {
                        this.actionModeActivated = false;
                        this.kAction = false;
                    }
                    if (this.sbTimer < (float)-10)
                    {
                        if (this.sbTimer > (float)-240)
                        {
                            this.sbClickIndicator.transform.localScale = this.DampV3(Vector3.one * 0.75f, this.sbClickIndicator.transform.localScale, 0.05f);
                        }
                        else
                        {
                            this.sbClickIndicator.transform.localScale = this.DampV3(Vector3.one * ((float)1 + Mathf.Sin(Time.time * (float)5) * 0.07f), this.sbClickIndicator.transform.localScale, 0.05f);
                        }
                        if (!this.paused)
                        {

                            if (this.player.GetButtonDown("Interact"))
                            {
                                if (MultiplayerManagerTest.singleplayerMode || sbClickContDontFreeze || !MultiplayerManagerTest.playingAsHost && !MultiplayerManagerTest.inst.hostIsOnSpeechScript)
                                    doMultiplayerClickToContinueOnce = true;
                                else
                                    if (MultiplayerManagerTest.playingAsHost)
                                    PacketSender.SendHostSpeechScriptClick();
                            }


                            if (doMultiplayerClickToContinueOnce)
                            {
                                doMultiplayerClickToContinueOnce = false;

                                this.voiceController.voiceTimer = (float)0;
                                this.sbTimer = (float)0;
                                this.sbClickIndicator.transform.localScale = Vector3.one * 0.5f;
                                if (this.sbCurStringInArray >= Extensions.get_length(this.sbStringArray) - 1)
                                {
                                    this.voice.Stop();
                                    this.sbClickCont = false;
                                }
                                else
                                {
                                    this.sbCurStringInArray++;
                                    this.setSpeechBubbleText();
                                }
                            }
                        }
                    }
                    else
                    {
                        this.sbClickIndicator.transform.localScale = Vector3.one * 0.5f;
                    }
                }
                else if (this.sbTimer <= (float)0)
                {
                    if (this.sbCurStringInArray >= Extensions.get_length(this.sbStringArray) - 1)
                    {
                        this.sbTimer = (float)0;
                    }
                    else
                    {
                        this.sbCurStringInArray++;
                        this.sbTimer = (float)(60 + this.sbStringArray[this.sbCurStringInArray].Length * 5);
                        this.sbText.text = this.sbStringArray[this.sbCurStringInArray];
                        this.setSpeechBubbleSize();
                        this.voiceController.voiceTimer = (float)0;
                        this.voice.Play();
                        this.voice.transform.position = this.sbTransform.position;
                    }
                }


                if (!sbClickCont && MultiplayerManagerTest.inst.hostIsOnSpeechScript && MultiplayerManagerTest.playingAsHost)
                    PacketSender.SendHostSpeechScriptState(false);

                Vector3 v = this.mainCamera.WorldToViewportPoint(this.sbTransform.position + this.sbOffset);
                v.x *= this.hudCanvasRect.sizeDelta.x;
                v.y *= this.hudCanvasRect.sizeDelta.y;
                float x7 = this.Damp(v.x, this.sbBubble.anchoredPosition.x, 0.005f);
                Vector2 anchoredPosition3 = this.sbBubble.anchoredPosition;
                float num34 = anchoredPosition3.x = x7;
                Vector2 vector12 = this.sbBubble.anchoredPosition = anchoredPosition3;
                if (this.sbBubble.anchoredPosition.y > v.y + (float)75)
                {
                    float y = this.Damp(v.y + (float)75, this.sbBubble.anchoredPosition.y, 0.001f);
                    Vector2 anchoredPosition4 = this.sbBubble.anchoredPosition;
                    float num35 = anchoredPosition4.y = y;
                    Vector2 vector13 = this.sbBubble.anchoredPosition = anchoredPosition4;
                }
                else
                {
                    float y2 = this.Damp(v.y + (float)75, this.sbBubble.anchoredPosition.y, 0.1f);
                    Vector2 anchoredPosition5 = this.sbBubble.anchoredPosition;
                    float num36 = anchoredPosition5.y = y2;
                    Vector2 vector14 = this.sbBubble.anchoredPosition = anchoredPosition5;
                }
                float y3 = Mathf.Clamp(this.sbBubble.anchoredPosition.y, this.hudCanvasRect.sizeDelta.y - (float)170, this.hudCanvasRect.sizeDelta.y - (float)50);
                Vector2 anchoredPosition6 = this.sbBubble.anchoredPosition;
                float num37 = anchoredPosition6.y = y3;
                Vector2 vector15 = this.sbBubble.anchoredPosition = anchoredPosition6;
                Vector3[] array = new Vector3[4];
                this.sbBubble.GetWorldCorners(array);
                Vector2 vector16 = default(Vector2);
                for (int i = 0; i < 4; i++)
                {
                    if (this.sbBubble.position.x - array[i].x > vector16.x)
                    {
                        vector16.x = this.sbBubble.position.x - array[i].x;
                    }
                    if (this.sbBubble.position.y - array[i].y > vector16.y)
                    {
                        vector16.y = this.sbBubble.position.y - array[i].y;
                    }
                }
                if (this.sbBubble.position.x - vector16.x < (float)this.mainCamera.pixelWidth * 0.05f)
                {
                    float x8 = vector16.x + (float)this.mainCamera.pixelWidth * 0.05f;
                    Vector3 position = this.sbBubble.position;
                    float num38 = position.x = x8;
                    Vector3 vector17 = this.sbBubble.position = position;
                }
                else if (this.sbBubble.position.x + vector16.x > (float)this.mainCamera.pixelWidth * 0.95f)
                {
                    float x9 = (float)this.mainCamera.pixelWidth * 0.95f - vector16.x;
                    Vector3 position2 = this.sbBubble.position;
                    float num39 = position2.x = x9;
                    Vector3 vector18 = this.sbBubble.position = position2;
                }
                if (this.sbShake > (float)0)
                {
                    this.sbBubble.anchoredPosition = this.sbBubble.anchoredPosition + new Vector2(UnityEngine.Random.Range(-this.sbShake, this.sbShake), UnityEngine.Random.Range(-this.sbShake, this.sbShake)) * 0.02f;
                    this.sbShake -= this.timescale;
                }
                this.sbTail.anchoredPosition = this.sbBubble.anchoredPosition + new Vector2(Mathf.Clamp((v.x - this.sbBubble.anchoredPosition.x) * 0.35f, -this.sbBubble.sizeDelta.x * 0.4f, this.sbBubble.sizeDelta.x * 0.4f), (float)0);
                float z = Mathf.Atan2(this.sbTail.anchoredPosition.y - v.y, this.sbTail.anchoredPosition.x - v.x) * 57.29578f - (float)90;
                Vector3 eulerAngles = this.sbTail.eulerAngles;
                float num40 = eulerAngles.z = z;
                Vector3 vector19 = this.sbTail.eulerAngles = eulerAngles;
                float y4 = Vector2.Distance(this.sbTail.anchoredPosition, v) * 0.6f;
                Vector2 sizeDelta7 = this.sbTail.sizeDelta;
                float num41 = sizeDelta7.y = y4;
                Vector2 vector20 = this.sbTail.sizeDelta = sizeDelta7;
                this.doVoice();
            }
        }
        else if (this.sbDoOnce)
        {
            this.voice.Stop();
            this.sbBubble.gameObject.SetActive(false);
            this.sbTail.gameObject.SetActive(false);
            this.sbDoOnce = false;
        }
        this.musicIntenseFactor = Mathf.Clamp01(this.musicIntenseFactor - 0.0015f * this.timescale);
        this.audioMixer.SetFloat("CalmVolume", (float)-80 * this.musicIntenseFactor * this.musicIntenseFactor * this.musicIntenseFactor * this.musicIntenseFactor);
        this.audioMixer.SetFloat("IntenseVolume", (float)-80 * ((float)1 - this.musicIntenseFactor) * ((float)1 - this.musicIntenseFactor) * ((float)1 - this.musicIntenseFactor) * ((float)1 - this.musicIntenseFactor));
        if (this.earRingingTimer > (float)0)
        {
            this.earRingingTimer -= this.timescale;
        }
        this.earRingingSound.volume = Mathf.Clamp01(this.earRingingTimer / (float)180);
        if (this.playerScript.onGround || this.playerScript.swinging)
        {
            if (this.inAirScore > (float)1600)
            {
                if (this.reactionPedroTimer <= (float)0)
                {
                    this.reactionPedroFace.sprite = this.pedroExpressions[UnityEngine.Random.Range(2, 10)];
                }
                this.reactionPedroTimer = (float)180;
            }
            this.inAirScore = (float)0;
        }
        if (this.dontAllowReactionPedroTimer > (float)0)
        {
            this.dontAllowReactionPedroTimer -= Time.unscaledDeltaTime * (float)60;
            this.reactionPedroTimer = (float)0;
        }
        float num42 = (float)-10;
        if (this.reactionPedroTimer > (float)0)
        {
            this.reactionPedroTimer -= this.timescale;
            num42 = (float)37;
            if (this.reactionPedroTimer <= (float)0)
            {
                this.dontAllowReactionPedroTimer = (float)600;
            }
        }
        if (!this.rootShared.hideHUD && (this.reactionPedroTimer > (float)0 || Mathf.Abs(num42 - this.reactionPedroRot) > (float)5))
        {
            this.reactionPedroSpeed += this.DampAdd(num42, this.reactionPedroRot, 0.06f);
            this.reactionPedroSpeed *= Mathf.Pow(0.75f, this.timescale);
            this.reactionPedroRot += this.reactionPedroSpeed * this.timescale;
            float z2 = this.reactionPedroRot - Mathf.Sin(Time.time + (float)10) * 1.5f;
            Quaternion localRotation = this.reactionPedro.localRotation;
            Vector3 eulerAngles2 = localRotation.eulerAngles;
            float num43 = eulerAngles2.z = z2;
            Vector3 vector21 = localRotation.eulerAngles = eulerAngles2;
            Quaternion quaternion = this.reactionPedro.localRotation = localRotation;
            float x10 = this.Damp((float)22 + ((float)1 - Mathf.Clamp(this.reactionPedroRot, (float)0, (float)37) / (float)37) * (float)30, this.reactionPedro.anchoredPosition.x, 0.2f);
            Vector2 anchoredPosition7 = this.reactionPedro.anchoredPosition;
            float num44 = anchoredPosition7.x = x10;
            Vector2 vector22 = this.reactionPedro.anchoredPosition = anchoredPosition7;
            float y5 = this.Damp((float)-226 + this.reactionPedroSpeed + Mathf.Sin(Time.time) * 2.5f, this.reactionPedro.anchoredPosition.y, 0.2f);
            Vector2 anchoredPosition8 = this.reactionPedro.anchoredPosition;
            float num45 = anchoredPosition8.y = y5;
            Vector2 vector23 = this.reactionPedro.anchoredPosition = anchoredPosition8;
        }
        else if (this.reactionPedro.anchoredPosition.x != (float)50)
        {
            int num46 = 50;
            Vector2 anchoredPosition9 = this.reactionPedro.anchoredPosition;
            float num47 = anchoredPosition9.x = (float)num46;
            Vector2 vector24 = this.reactionPedro.anchoredPosition = anchoredPosition9;
        }
        this.timeSinceScoreLastGiven = Mathf.Clamp(this.timeSinceScoreLastGiven + this.timescale, (float)0, (float)60);
        if (this.timeSinceScoreLastGiven >= (float)60)
        {
            if (this.multiplierTimer > (float)0)
            {
                if (this.prevMultiplierTimer == this.multiplierTimer)
                {
                    this.cCheckMuTi = true;
                }
                this.multiplierTimer = Mathf.Clamp01(this.multiplierTimer - this.timescale / (float)300);
            }
            if (this.multiplierTimer <= (float)0 && this.tempScore != (float)0)
            {
                this.doScoreCountingSound = false;
                if (!this.isTutorialLevel)
                {
                    if (this.multiplier >= (float)3 || this.doReactionPedro)
                    {
                        if (this.playerScript.timeSinceBulletHit > (float)((!this.enemyEngagedWithPlayer) ? 10 : 120))
                        {
                            if (this.multiplier >= (float)4 || this.doReactionPedro)
                            {
                                this.bigText.text = this.getEncouragingText().ToUpper();
                                this.bigText.color = this.bigMultiplierStartColour;
                                float a8 = 0.7f;
                                Color color25 = this.bigText.color;
                                float num48 = color25.a = a8;
                                Color color26 = this.bigText.color = color25;
                                this.bigTextOutline.enabled = false;
                                this.bigText.transform.localScale = Vector3.one * 0.1f;
                            }
                            else
                            {
                                this.bigFace.color = this.bigMultiplierStartColour;
                                float a9 = 0.7f;
                                Color color27 = this.bigFace.color;
                                float num49 = color27.a = a9;
                                Color color28 = this.bigFace.color = color27;
                                this.bigFace.transform.localScale = Vector3.one * 0.1f;
                            }
                            if (!this.rootShared.hideHUD)
                            {
                                this.bigScreenReactionCanvas.enabled = true;
                            }
                        }
                        if (this.multiplier >= (float)3)
                        {
                            if (LocalizationManager.CurrentLanguageCode == "fr" || LocalizationManager.CurrentLanguageCode == "de")
                            {
                                this.bigMultiplier.text = "x " + this.multiplier;
                            }
                            else
                            {
                                this.bigMultiplier.text = "x" + this.multiplier;
                            }
                            this.bigMultiplier.color = Color.white;
                            int num50 = 1;
                            Color color29 = this.bigMultiplier.color;
                            float num51 = color29.a = (float)num50;
                            Color color30 = this.bigMultiplier.color = color29;
                            int num52 = 0;
                            Vector2 effectDistance3 = this.bigMultiplierOutline.effectDistance;
                            float num53 = effectDistance3.x = (float)num52;
                            Vector2 vector25 = this.bigMultiplierOutline.effectDistance = effectDistance3;
                            float a10 = 0.5f;
                            Color effectColor = this.bigMultiplierOutline.effectColor;
                            float num54 = effectColor.a = a10;
                            Color color31 = this.bigMultiplierOutline.effectColor = effectColor;
                            this.bigMultiplier.transform.localScale = Vector3.zero;
                            this.bigMultiplier.transform.SetAsLastSibling();
                        }
                        if (this.multiplier >= (float)3 && !this.trailerMode)
                        {
                            this.hudAudio2.clip = this.multiplierFinishedSound;
                            this.hudAudio2.loop = false;
                            this.hudAudio2.pitch = Mathf.Clamp(0.7f + this.multiplier / (float)10 + UnityEngine.Random.Range(-0.1f, 0.1f), 0.9f, 1.5f);
                            this.hudAudio2.volume = 0.2f + Mathf.Clamp01(this.multiplier / (float)6) * 0.5f;
                            this.hudAudio2.Play();
                            this.multiplierAudio.pitch = Mathf.Clamp01(this.multiplier / (float)4);
                            this.multiplierAudio.volume = Mathf.Clamp01(this.multiplier / (float)4 - (float)3);
                            this.doScoreCountingSound = true;
                        }
                    }
                    if (this.doReactionPedro)
                    {
                        this.reactionPedroTimer = (float)180;
                        this.doReactionPedro = false;
                    }
                    if (this.multiplier >= (float)2 && !this.trailerMode)
                    {
                        this.hudAudio.clip = this.scoreCountingSound;
                        this.hudAudio.loop = false;
                        this.hudAudio.pitch = (float)1;
                        this.hudAudio.volume = 0.3f + Mathf.Clamp01(this.multiplier / (float)4) * 0.5f;
                        this.hudAudio.Play();
                    }
                }
                this.cCheckSc = true;
                this.score += this.tempScore * this.multiplier;
                this.cCheckTSc = true;
                this.tempScore = (float)0;
                this.cCheckMu = true;
                this.multiplier = (float)0;
            }
            else if (!this.isTutorialLevel && !this.trailerMode)
            {
                this.multiplierAudio.volume = this.Damp((-2.5f + ((float)1 - this.multiplierTimer) * (float)4) * Mathf.Clamp01(this.multiplier / (float)2 - (float)1), this.multiplierAudio.volume, 0.1f);
                this.multiplierAudio.pitch = this.Damp(0.8f + this.multiplierTimer * 1.5f + Mathf.Clamp01(this.multiplier / (float)10) * 0.1f, this.multiplierAudio.pitch, 0.1f);
            }
        }
        else
        {
            this.multiplierAudio.volume = this.Damp((float)0, this.multiplierAudio.volume, 0.05f);
            this.multiplierAudio.pitch = this.Damp(this.multiplierTimer * (float)3, this.multiplierAudio.pitch, 0.5f);
        }
        if (this.scoreVisual < this.score)
        {
            this.slowMotionAmount = Mathf.Clamp01(this.slowMotionAmount + 0.005f);
            if (this.score - this.scoreVisual > (float)10000)
            {
                this.scoreVisual += (float)3333;
            }
            else if (this.score - this.scoreVisual > (float)1000)
            {
                this.scoreVisual += (float)333;
            }
            else
            {
                this.scoreVisual += (float)33;
            }
            if (this.scoreVisual > this.score)
            {
                this.scoreVisual = this.score;
            }
            if (this.doScoreCountingSound && !this.hudAudio.isPlaying)
            {
                this.hudAudio.clip = this.scoreCountingSound;
                this.hudAudio.loop = false;
                this.hudAudio.pitch = 0.6f + this.scoreVisual / this.score * (float)2;
                this.hudAudio.volume = 0.2f - this.scoreVisual / this.score * 0.05f;
                this.hudAudio.Play();
            }
        }
        if (this.tempScoreStringCheck != this.tempScore)
        {
            this.scoreHudMultiplierScoreText.text = this.addCommasToNumber(this.tempScore);
            this.tempScoreStringCheck = this.tempScore;
        }
        if (this.scoreVisualStringCheck != this.scoreVisual)
        {
            this.scoreHudTotalScoreText.text = this.addCommasToNumber(this.scoreVisual);
            this.scoreVisualStringCheck = this.scoreVisual;
        }
        if (this.scoreVisual <= (float)0)
        {
            if (this.scoreHudTotalScoreText.color.a != 0.5f)
            {
                this.scoreHudTotalScoreText.fontSize = (float)20;
                float a11 = 0.5f;
                Color color32 = this.scoreHudTotalScoreText.color;
                float num55 = color32.a = a11;
                Color color33 = this.scoreHudTotalScoreText.color = color32;
            }
        }
        else if (this.scoreHudTotalScoreText.color.a != (float)1)
        {
            this.scoreHudTotalScoreText.fontSize = (float)38;
            int num56 = 1;
            Color color34 = this.scoreHudTotalScoreText.color;
            float num57 = color34.a = (float)num56;
            Color color35 = this.scoreHudTotalScoreText.color = color34;
        }
        this.scoreHudBallImage.color = this.DampColor(this.scoreHudBallImageStartColour, this.scoreHudBallImage.color, 0.1f);
        this.scoreHudBall.localScale = this.DampV3(Vector3.one * this.multiplierTimer, this.scoreHudBall.localScale, 0.1f);
        if (this.multiplierStringCheck != this.multiplier)
        {
            if (LocalizationManager.CurrentLanguageCode == "fr" || LocalizationManager.CurrentLanguageCode == "de")
            {
                this.scoreHudMultiplierText.text = "x " + this.multiplier;
            }
            else
            {
                this.scoreHudMultiplierText.text = "x" + this.multiplier;
            }
            this.multiplierStringCheck = this.multiplier;
        }
        if (this.tempScore <= (float)0 && this.multiplier <= (float)0)
        {
            this.scoreHudMainGraphic.localScale = (this.scoreHudMultiplierScoreText.transform.localScale = (this.scoreHudMultiplierText.transform.localScale = this.DampV3(Vector3.one * 0.85f, this.scoreHudMainGraphic.localScale, 0.1f)));
            if (!this.rootShared.lowEndHardware)
            {
                float y6 = this.Damp((float)0, this.scoreHudLine1.anchoredPosition.y, 0.3f);
                Vector2 anchoredPosition10 = this.scoreHudLine1.anchoredPosition;
                float num58 = anchoredPosition10.y = y6;
                Vector2 vector26 = this.scoreHudLine1.anchoredPosition = anchoredPosition10;
                float y7 = this.Damp((float)0, this.scoreHudLine2.anchoredPosition.y, 0.3f);
                Vector2 anchoredPosition11 = this.scoreHudLine2.anchoredPosition;
                float num59 = anchoredPosition11.y = y7;
                Vector2 vector27 = this.scoreHudLine2.anchoredPosition = anchoredPosition11;
                float x11 = this.Damp((float)0, this.scoreHudLine1.localScale.x, 0.3f);
                Vector3 localScale2 = this.scoreHudLine1.localScale;
                float num60 = localScale2.x = x11;
                Vector3 vector28 = this.scoreHudLine1.localScale = localScale2;
                float x12 = this.Damp((float)0, this.scoreHudLine2.localScale.x, 0.3f);
                Vector3 localScale3 = this.scoreHudLine2.localScale;
                float num61 = localScale3.x = x12;
                Vector3 vector29 = this.scoreHudLine2.localScale = localScale3;
            }
            float a12 = this.Damp(0.3f, this.scoreHudMultiplierScoreText.color.a, 0.3f);
            Color color36 = this.scoreHudMultiplierScoreText.color;
            float num62 = color36.a = a12;
            Color color37 = this.scoreHudMultiplierScoreText.color = color36;
            float a13 = this.Damp(0.3f, this.scoreHudMultiplierText.color.a, 0.3f);
            Color color38 = this.scoreHudMultiplierText.color;
            float num63 = color38.a = a13;
            Color color39 = this.scoreHudMultiplierText.color = color38;
        }
        else
        {
            this.scoreHudMainGraphic.localScale = (this.scoreHudMultiplierScoreText.transform.localScale = (this.scoreHudMultiplierText.transform.localScale = this.DampV3(Vector3.one, this.scoreHudMainGraphic.localScale, 0.3f)));
            if (!this.rootShared.lowEndHardware)
            {
                float y8 = this.Damp(this.scoreHudLine1StartPos.y, this.scoreHudLine1.anchoredPosition.y, 0.3f);
                Vector2 anchoredPosition12 = this.scoreHudLine1.anchoredPosition;
                float num64 = anchoredPosition12.y = y8;
                Vector2 vector30 = this.scoreHudLine1.anchoredPosition = anchoredPosition12;
                float y9 = this.Damp(this.scoreHudLine2StartPos.y, this.scoreHudLine2.anchoredPosition.y, 0.3f);
                Vector2 anchoredPosition13 = this.scoreHudLine2.anchoredPosition;
                float num65 = anchoredPosition13.y = y9;
                Vector2 vector31 = this.scoreHudLine2.anchoredPosition = anchoredPosition13;
                float x13 = this.Damp((float)1, this.scoreHudLine1.localScale.x, 0.3f);
                Vector3 localScale4 = this.scoreHudLine1.localScale;
                float num66 = localScale4.x = x13;
                Vector3 vector32 = this.scoreHudLine1.localScale = localScale4;
                float x14 = this.Damp((float)1, this.scoreHudLine2.localScale.x, 0.3f);
                Vector3 localScale5 = this.scoreHudLine2.localScale;
                float num67 = localScale5.x = x14;
                Vector3 vector33 = this.scoreHudLine2.localScale = localScale5;
            }
            float a14 = this.Damp((float)1, this.scoreHudMultiplierScoreText.color.a, 0.3f);
            Color color40 = this.scoreHudMultiplierScoreText.color;
            float num68 = color40.a = a14;
            Color color41 = this.scoreHudMultiplierScoreText.color = color40;
            float a15 = this.Damp((float)1, this.scoreHudMultiplierText.color.a, 0.3f);
            Color color42 = this.scoreHudMultiplierText.color;
            float num69 = color42.a = a15;
            Color color43 = this.scoreHudMultiplierText.color = color42;
        }
        if (this.subtleHighlighterFollowTransform != null && !this.prevDodgeHelper)
        {
            this.subtleHighlighter.position = this.mainCamera.WorldToScreenPoint(this.subtleHighlighterFollowTransform.position);
            float a16 = this.subtleHighlighterImg.color.a - 0.1f * this.timescale;
            Color color44 = this.subtleHighlighterImg.color;
            float num70 = color44.a = a16;
            Color color45 = this.subtleHighlighterImg.color = color44;
            if (this.subtleHighlighterImg.color.a <= (float)0)
            {
                this.subtleHighlighterFollowTransform = null;
            }
        }
        else if (this.subtleHighlighterImg.color.a > (float)0)
        {
            int num71 = 0;
            Color color46 = this.subtleHighlighterImg.color;
            float num72 = color46.a = (float)num71;
            Color color47 = this.subtleHighlighterImg.color = color46;
        }
        bool flag;
        if (flag = !this.paused)
        {
            flag = (this.pedroHintTimer > (float)0);
        }
        bool flag2;
        if (flag2 = flag)
        {
            flag2 = !this.sbClickCont;
        }
        bool flag3;
        if (flag3 = flag2)
        {
            flag3 = !this.playerScript.overrideControls;
        }
        this.showPedroHint = flag3;
        float num73 = Time.unscaledDeltaTime * (float)60;
        float num74 = 0f;
        if (this.pedroHintTimer > (float)-3600)
        {
            if (this.pedroHintTimer < (float)0)
            {
                this.pedroHintTimer -= (0.5f + ((float)1 - Mathf.Clamp01((float)(SceneManager.GetActiveScene().buildIndex / 40))) * 0.5f) * this.timescale;
            }
            else
            {
                this.pedroHintTimer -= this.timescale;
            }
        }
        if (this.showPedroHint)
        {
            if (this.pedroHintSplatImg4.color.a <= (float)0)
            {
                int num75 = 0;
                Vector3 localScale6 = this.pedroHintSplat4.localScale;
                float num76 = localScale6.x = (float)num75;
                Vector3 vector34 = this.pedroHintSplat4.localScale = localScale6;
                int num77 = num75;
                Vector3 localScale7 = this.pedroHintSplat3.localScale;
                float num78 = localScale7.x = (float)num77;
                Vector3 vector35 = this.pedroHintSplat3.localScale = localScale7;
                int num79 = num77;
                Vector3 localScale8 = this.pedroHintSplat2.localScale;
                float num80 = localScale8.x = (float)num79;
                Vector3 vector36 = this.pedroHintSplat2.localScale = localScale8;
                int num81 = num79;
                Vector3 localScale9 = this.pedroHintSplat1.localScale;
                float num82 = localScale9.y = (float)num81;
                Vector3 vector37 = this.pedroHintSplat1.localScale = localScale9;
            }
            num74 = (float)41;
            float y10 = this.pedroHintSplat1.localScale.y + ((float)1 - this.pedroHintSplat1.localScale.y) * Mathf.Clamp01(0.3f * num73);
            Vector3 localScale10 = this.pedroHintSplat1.localScale;
            float num83 = localScale10.y = y10;
            Vector3 vector38 = this.pedroHintSplat1.localScale = localScale10;
            float x15 = this.pedroHintSplat2.localScale.x + ((float)1 - this.pedroHintSplat2.localScale.x) * Mathf.Clamp01(0.1f * num73);
            Vector3 localScale11 = this.pedroHintSplat2.localScale;
            float num84 = localScale11.x = x15;
            Vector3 vector39 = this.pedroHintSplat2.localScale = localScale11;
            float x16 = this.pedroHintSplat3.localScale.x + ((float)1 - this.pedroHintSplat3.localScale.x) * Mathf.Clamp01(0.2f * num73);
            Vector3 localScale12 = this.pedroHintSplat3.localScale;
            float num85 = localScale12.x = x16;
            Vector3 vector40 = this.pedroHintSplat3.localScale = localScale12;
            float x17 = this.pedroHintSplat4.localScale.x + ((float)1 - this.pedroHintSplat4.localScale.x) * Mathf.Clamp01(0.4f * num73);
            Vector3 localScale13 = this.pedroHintSplat4.localScale;
            float num86 = localScale13.x = x17;
            Vector3 vector41 = this.pedroHintSplat4.localScale = localScale13;
            float x18 = this.pedroHintSplat4.localScale.x;
            Vector3 localScale14 = this.pedroHintSplat4.localScale;
            float num87 = localScale14.y = x18;
            Vector3 vector42 = this.pedroHintSplat4.localScale = localScale14;
            float num88 = x18;
            Vector3 localScale15 = this.pedroHintSplat3.localScale;
            float num89 = localScale15.y = num88;
            Vector3 vector43 = this.pedroHintSplat3.localScale = localScale15;
            float y11 = num88;
            Vector3 localScale16 = this.pedroHintSplat2.localScale;
            float num90 = localScale16.y = y11;
            Vector3 vector44 = this.pedroHintSplat2.localScale = localScale16;
            float x19 = this.pedroHintSplat4.localScale.x;
            Color color48 = this.pedroHintTopText.color;
            float num91 = color48.a = x19;
            Color color49 = this.pedroHintTopText.color = color48;
            float num92 = x19;
            Color color50 = this.pedroHintSplatImg4.color;
            float num93 = color50.a = num92;
            Color color51 = this.pedroHintSplatImg4.color = color50;
            float num94 = num92;
            Color color52 = this.pedroHintSplatImg3.color;
            float num95 = color52.a = num94;
            Color color53 = this.pedroHintSplatImg3.color = color52;
            float a17 = num94;
            Color color54 = this.pedroHintSplatImg2.color;
            float num96 = color54.a = a17;
            Color color55 = this.pedroHintSplatImg2.color = color54;
            float x20 = this.pedroHintText.anchoredPosition.x + ((float)0 - this.pedroHintText.anchoredPosition.x) * Mathf.Clamp01(0.3f * num73);
            Vector2 anchoredPosition14 = this.pedroHintText.anchoredPosition;
            float num97 = anchoredPosition14.x = x20;
            Vector2 vector45 = this.pedroHintText.anchoredPosition = anchoredPosition14;
        }
        else
        {
            num74 = (float)-100;
            float x21 = this.pedroHintText.anchoredPosition.x + ((float)-1000 - this.pedroHintText.anchoredPosition.x) * Mathf.Clamp01(0.1f * num73);
            Vector2 anchoredPosition15 = this.pedroHintText.anchoredPosition;
            float num98 = anchoredPosition15.x = x21;
            Vector2 vector46 = this.pedroHintText.anchoredPosition = anchoredPosition15;
            float num99 = Mathf.Clamp01(this.pedroHintSplatImg4.color.a - 0.05f * num73);
            Color color56 = this.pedroHintTopText.color;
            float num100 = color56.a = num99;
            Color color57 = this.pedroHintTopText.color = color56;
            float num101 = num99;
            Color color58 = this.pedroHintSplatImg4.color;
            float num102 = color58.a = num101;
            Color color59 = this.pedroHintSplatImg4.color = color58;
            float num103 = num101;
            Color color60 = this.pedroHintSplatImg3.color;
            float num104 = color60.a = num103;
            Color color61 = this.pedroHintSplatImg3.color = color60;
            float a18 = num103;
            Color color62 = this.pedroHintSplatImg2.color;
            float num105 = color62.a = a18;
            Color color63 = this.pedroHintSplatImg2.color = color62;
            float y12 = Mathf.Clamp01(this.pedroHintSplat1.localScale.y - 0.05f * num73);
            Vector3 localScale17 = this.pedroHintSplat1.localScale;
            float num106 = localScale17.y = y12;
            Vector3 vector47 = this.pedroHintSplat1.localScale = localScale17;
        }
        if (this.showPedroHint || Mathf.Abs(num74 - this.pedroHintPedro.anchoredPosition.x) > 0.5f)
        {
            if (this.enablePedroHintCanvasDoOnce)
            {
                if (!this.rootShared.hideHUD)
                {
                    this.pedroHintCanvas.enabled = true;
                }
                this.enablePedroHintCanvasDoOnce = false;
            }
            this.pedroHintPedroXSpeed += this.DampAddUnscaled(num74, this.pedroHintPedro.anchoredPosition.x, 0.06f);
            this.pedroHintPedroXSpeed *= Mathf.Pow(0.85f, num73);
            float x22 = this.pedroHintPedro.anchoredPosition.x + this.pedroHintPedroXSpeed * num73;
            Vector2 anchoredPosition16 = this.pedroHintPedro.anchoredPosition;
            float num107 = anchoredPosition16.x = x22;
            Vector2 vector48 = this.pedroHintPedro.anchoredPosition = anchoredPosition16;
            float y13 = Mathf.Sin(Time.unscaledTime) * (float)5;
            Vector2 anchoredPosition17 = this.pedroHintPedro.anchoredPosition;
            float num108 = anchoredPosition17.y = y13;
            Vector2 vector49 = this.pedroHintPedro.anchoredPosition = anchoredPosition17;
            float z3 = (float)5 + Mathf.Sin(Time.unscaledTime + (float)10) * (float)5 + this.pedroHintPedroXSpeed * 0.25f;
            Quaternion rotation = this.pedroHintPedro.rotation;
            Vector3 eulerAngles3 = rotation.eulerAngles;
            float num109 = eulerAngles3.z = z3;
            Vector3 vector50 = rotation.eulerAngles = eulerAngles3;
            Quaternion quaternion2 = this.pedroHintPedro.rotation = rotation;
        }
        else if (!this.enablePedroHintCanvasDoOnce)
        {
            this.pedroHintCanvas.enabled = false;
            this.enablePedroHintCanvasDoOnce = true;
        }
        if (this.bigText.color.a >= (float)0 && !this.dead)
        {
            this.bigText.transform.localScale = this.DampV3Unscaled(Vector3.one, this.bigText.transform.localScale, 0.3f);
            float a19 = this.bigText.color.a - 0.0125f * this.unscaledTimescale;
            Color color64 = this.bigText.color;
            float num110 = color64.a = a19;
            Color color65 = this.bigText.color = color64;
        }
        if (this.bigFace.color.a >= (float)0)
        {
            this.bigFace.transform.localScale = this.DampV3Unscaled(Vector3.one, this.bigFace.transform.localScale, 0.3f);
            float a20 = this.bigFace.color.a - 0.0215f * this.unscaledTimescale;
            Color color66 = this.bigFace.color;
            float num111 = color66.a = a20;
            Color color67 = this.bigFace.color = color66;
        }
        if (!this.dead && this.bigFace.color.a <= (float)0 && this.bigText.color.a <= (float)0 && this.bigScreenReactionCanvas.enabled)
        {
            this.bigScreenReactionCanvas.enabled = false;
        }
        if (this.bigMultiplier.color.a >= (float)0)
        {
            this.bigMultiplier.color = Color.Lerp(this.bigMultiplier.color, new Color(this.bigMultiplierStartColour.r, this.bigMultiplierStartColour.g, this.bigMultiplierStartColour.b, this.bigMultiplier.color.a), Mathf.Clamp01(0.3f * this.timescale));
            float a21 = this.bigMultiplier.color.a + Mathf.Clamp(-this.bigMultiplier.color.a * Mathf.Clamp01(0.15f * this.timescale), -0.01f * this.timescale, (float)1);
            Color color68 = this.bigMultiplier.color;
            float num112 = color68.a = a21;
            Color color69 = this.bigMultiplier.color = color68;
            float x23 = this.bigMultiplierOutline.effectDistance.x - (float)2 * this.timescale;
            Vector2 effectDistance4 = this.bigMultiplierOutline.effectDistance;
            float num113 = effectDistance4.x = x23;
            Vector2 vector51 = this.bigMultiplierOutline.effectDistance = effectDistance4;
            float a22 = this.bigMultiplierOutline.effectColor.a - 0.02f * this.timescale;
            Color effectColor2 = this.bigMultiplierOutline.effectColor;
            float num114 = effectColor2.a = a22;
            Color color70 = this.bigMultiplierOutline.effectColor = effectColor2;
            float x24 = this.Damp(Mathf.Clamp01(this.bigMultiplier.color.a * (float)30), this.bigMultiplier.transform.localScale.x, 0.4f);
            Vector3 localScale18 = this.bigMultiplier.transform.localScale;
            float num115 = localScale18.x = x24;
            Vector3 vector52 = this.bigMultiplier.transform.localScale = localScale18;
            if (this.bigMultiplier.color.a > 0.5f)
            {
                float y14 = (float)1 + Mathf.Sin(this.bigMultiplier.transform.localScale.x * (float)3) * Mathf.Clamp01(Mathf.Abs((float)1 - this.bigMultiplier.transform.localScale.x));
                Vector3 localScale19 = this.bigMultiplier.transform.localScale;
                float num116 = localScale19.y = y14;
                Vector3 vector53 = this.bigMultiplier.transform.localScale = localScale19;
            }
            else
            {
                float y15 = (float)1 - Mathf.Clamp01(((float)1 - this.bigMultiplier.transform.localScale.x) * (float)2 - (float)1) * (float)5;
                Vector3 localScale20 = this.bigMultiplier.transform.localScale;
                float num117 = localScale20.y = y15;
                Vector3 vector54 = this.bigMultiplier.transform.localScale = localScale20;
            }
        }
        if (this.bigMultiplier.color.a > 0.1f)
        {
            if (!this.bigMultiplierDoOnce)
            {
                this.bigMultiplier.gameObject.SetActive(true);
                this.bigMultiplierDoOnce = true;
            }
        }
        else if (this.bigMultiplierDoOnce)
        {
            this.bigMultiplier.gameObject.SetActive(false);
            this.bigMultiplierDoOnce = false;
        }
        this.timeSinceEnemySpokeOnDetection = Mathf.Clamp(this.timeSinceEnemySpokeOnDetection + this.timescale, (float)0, (float)1000);
        this.timeSinceEnemySpokeOnHearing = Mathf.Clamp(this.timeSinceEnemySpokeOnHearing + this.timescale, (float)0, (float)1000);
        if (this.slowScrollMaterial != null)
        {
            float x25 = this.slowScrollMaterial.mainTextureOffset.x - 0.005f * this.timescale;
            Vector2 mainTextureOffset = this.slowScrollMaterial.mainTextureOffset;
            float num118 = mainTextureOffset.x = x25;
            Vector2 vector55 = this.slowScrollMaterial.mainTextureOffset = mainTextureOffset;
            if (this.slowScrollMaterial.mainTextureOffset.x < (float)0)
            {
                float x26 = this.slowScrollMaterial.mainTextureOffset.x + (float)1;
                Vector2 mainTextureOffset2 = this.slowScrollMaterial.mainTextureOffset;
                float num119 = mainTextureOffset2.x = x26;
                Vector2 vector56 = this.slowScrollMaterial.mainTextureOffset = mainTextureOffset2;
            }
        }
        if (this.updateInputIcons)
        {
            this.setUpSpeechBubbleButtonPrompt();
            this.setUpHintText();
            this.updateInputIcons = false;
        }
        if (this.killEveryone && this.prevNrOfEnemiesKilled != this.nrOfEnemiesKilled)
        {
            if (this.nrOfEnemiesKilled == this.nrOfEnemiesTotal)
            {
                this.miniLevelGoalText.text = "Success!";
                this.markMiniLevelTimeComplete();
            }
            else
            {
                this.miniLevelGoalText.text = "Kill everyone: " + this.nrOfEnemiesKilled + "/" + this.nrOfEnemiesTotal;
            }
            this.prevNrOfEnemiesKilled = this.nrOfEnemiesKilled;
        }
        if (this.isMiniLevel && !this.miniLevelSuccess)
        {
            this.miniLevelCurTimeUIText.text = "Current time: " + (Time.unscaledTime - this.startTime);
        }
        if (this.secretUnlockedTimer > (float)0)
        {
            this.secretUnlockedTimer -= this.timescale;
            if (this.secretUnlockedCanvasGroup.alpha < (float)1)
            {
                this.secretUnlockedCanvasGroup.alpha = this.secretUnlockedCanvasGroup.alpha + 0.1f * this.timescale;
            }
        }
        else if (this.secretUnlockedCanvasGroup.alpha > (float)0)
        {
            this.secretUnlockedCanvasGroup.alpha = this.secretUnlockedCanvasGroup.alpha - 0.05f * this.timescale;
        }
        else if (this.secretUnlockedDoOnce)
        {
            this.secretUnlockedCanvasGroup.gameObject.SetActive(false);
            this.secretUnlockedDoOnce = false;
        }
        if (this.playerScript.overrideControls || this.sbClickCont || this.trailerMode)
        {
            this.showHintDodge = false;
            this.showHintChangeWeapon = false;
            this.showHintReload = false;
            this.showHintHealthFull = (float)0;
            this.showHintAmmoFull = (float)0;
            this.showHintFocus = false;
            this.showHintKick = false;
            this.showHintGrab = false;
            this.showHintFlipSkateboard = false;
            this.showHintPressButton = false;
            this.showHintFlipLever = false;
            this.showHintFlipTable = false;
            this.showHintOpen = false;
            this.showHintSwing = false;
        }
        if (this.showHintDied)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(true);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintDodge)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(true);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintPressButton)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(true);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintFlipLever)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(true);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintKick)
        {
            this.hintKick.SetActive(true);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintGrab)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(true);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintFlipSkateboard)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(true);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintFlipTable)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(true);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintOpen)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(true);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintSwing)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(true);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintChangeWeapon)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(true);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintReload)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(true);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        else if (this.showHintHealthFull > (float)0)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(true);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
            this.showHintHealthFull -= this.timescale;
        }
        else if (this.showHintAmmoFull > (float)0)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(true);
            this.hintFocus.SetActive(false);
            this.showHintAmmoFull -= this.timescale;
        }
        else if (this.showHintFocus)
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(true);
        }
        else
        {
            this.hintKick.SetActive(false);
            this.hintGrab.SetActive(false);
            this.hintFlipSkateboard.SetActive(false);
            this.hintPressButton.SetActive(false);
            this.hintFlipLever.SetActive(false);
            this.hintFlipTable.SetActive(false);
            this.hintOpen.SetActive(false);
            this.hintSwing.SetActive(false);
            this.hintDied.SetActive(false);
            this.hintDodge.SetActive(false);
            this.hintChangeWeapon.SetActive(false);
            this.hintReload.SetActive(false);
            this.hintHealthFull.SetActive(false);
            this.hintAmmoFull.SetActive(false);
            this.hintFocus.SetActive(false);
        }
        this.showHintDied = false;
        this.showHintDodge = false;
        this.showHintChangeWeapon = false;
        this.showHintReload = false;
        this.showHintFocus = false;
        this.showHintKick = false;
        this.showHintGrab = false;
        this.showHintFlipSkateboard = false;
        this.showHintPressButton = false;
        this.showHintFlipLever = false;
        this.showHintFlipTable = false;
        this.showHintOpen = false;
        this.showHintSwing = false;
        this.deltaUnityTimescale = Mathf.Abs(this.prevUnityTimescale - this.unityTimescale);
        if (this.rootShared.modDisableCheckpoints)
        {
            this.doCheckpointSave = false;
        }
        if (this.rootShared.allowDebugMenu && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Game end at: " + DateTime.Now);
        }
    }

    // Token: 0x06000473 RID: 1139 RVA: 0x00003D85 File Offset: 0x00001F85
    public virtual void FixedUpdate()
    {
        this.fixedTimescale = Time.fixedDeltaTime * (float)50;
    }

    // Token: 0x06000474 RID: 1140 RVA: 0x00003D96 File Offset: 0x00001F96
    public virtual void OnApplicationFocus(bool hasFocus)
    {
        if (this.updateFramesDone > 5)
        {
            if (hasFocus)
            {
                this.SetCursorState();
            }
        }
    }

    // Token: 0x06000475 RID: 1141 RVA: 0x00082180 File Offset: 0x00080380
    public virtual void SetCursorState()
    {
        if (this.rootShared.runningOnConsole)
        {
            Cursor.visible = false;
        }
        else if (this.rootShared.simulateMousePos && !this.paused && !this.levelEnded)
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (Cursor.visible)
            {
                this.rootShared.fakeMousePos = Input.mousePosition;
                Cursor.visible = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = !this.playerScript.gamepad;
        }
    }

    // Token: 0x06000476 RID: 1142 RVA: 0x00003DB5 File Offset: 0x00001FB5
    public virtual string addCommasToNumber(float nr)
    {
        return this.rootShared.addCommasToNumber(nr);
    }

    // Token: 0x06000477 RID: 1143 RVA: 0x00082220 File Offset: 0x00080420
    public virtual string convertToTimeFormat(float timer)
    {
        int num = Mathf.FloorToInt(timer / 60f);
        int num2 = Mathf.FloorToInt(timer - (float)(num * 60));
        int num3 = Mathf.FloorToInt((float)100 * (timer - (float)(num * 60) - (float)num2));
        return string.Format("{0:0}:{1:00}.<size=9>{2:00}</size>", num, num2, num3);
    }

    // Token: 0x06000478 RID: 1144 RVA: 0x00082278 File Offset: 0x00080478
    public virtual float calculateBulletHitStrength(float bulletStrength)
    {
        return bulletStrength / (float)3 * ((this.difficultyMode != 2) ? ((float)1 + (float)this.difficultyMode * 0.25f) : ((!this.playerScript.onMotorcycle) ? ((float)((!this.isTutorialLevel) ? 2 : 1)) : ((this.difficultyMode != 2) ? 0.18f : 0.25f)));
    }

    // Token: 0x06000479 RID: 1145 RVA: 0x00003DC3 File Offset: 0x00001FC3
    public virtual void quitGame()
    {
        this.statsTracker.saveStats();
        Application.Quit();
    }

    // Token: 0x0600047A RID: 1146 RVA: 0x000822F0 File Offset: 0x000804F0
    public virtual void quitToMainMenu()
    {
        this.rootShared.levelLoadedFromLevelSelectScreen = false;
        Time.timeScale = this.startUnityTimeScale;
        Time.fixedDeltaTime = this.startUnityFixedDeltaTime;
        Time.maximumDeltaTime = this.startUnityMaximumDeltaTime;
        this.statsTracker.saveStats();
        this.rootShared.loadingScreenLevelToLoad = 1;
        SceneManager.LoadScene("_Loading_Screen");
        ((RootScript)this.GetComponent(typeof(RootScript))).enabled = false;
    }

    // Token: 0x0600047B RID: 1147 RVA: 0x00003DD5 File Offset: 0x00001FD5
    public virtual void resumeGame()
    {
        this.playerScript.speechBubbleCoolDownTimer = (float)5;
        if (this.inputHelperScript.checkForMissingInput(false))
        {
            this.player.controllers.maps.LoadDefaultMaps(ControllerType.Joystick);
        }
        this.paused = false;
    }

    // Token: 0x0600047C RID: 1148 RVA: 0x00082368 File Offset: 0x00080568
    public virtual void doPause()
    {
        this.paused = true;
        this.optionsMenuScript.menuEnabled = true;
        this.optionsMenuScript.curOption = 0;
        this.optionsMenuScript.curOptionSortedNr = (float)0;
        this.optionsMenu.gameObject.SetActive(true);
        this.optionsMenu.SetAsLastSibling();
    }

    // Token: 0x0600047D RID: 1149 RVA: 0x000823C0 File Offset: 0x000805C0
    public virtual void restartLevel()
    {
        Time.timeScale = this.startUnityTimeScale;
        Time.fixedDeltaTime = this.startUnityFixedDeltaTime;
        Time.maximumDeltaTime = this.startUnityMaximumDeltaTime;
        this.statsTracker.saveStats();
        this.rootShared.loadingScreenLevelToLoad = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("_Loading_Screen");
        ((RootScript)this.GetComponent(typeof(RootScript))).enabled = false;
    }

    // Token: 0x0600047E RID: 1150 RVA: 0x00003E12 File Offset: 0x00002012
    public virtual void fadeToBlackAndChangeLevel()
    {
        this.blackFade.transform.SetAsLastSibling();
        this.fadeToBlack = true;
    }

    // Token: 0x0600047F RID: 1151 RVA: 0x00003E2B File Offset: 0x0000202B
    public virtual IEnumerator doPedroHint(string txt)
    {
        return new RootScript.doPedroHint2581(txt, this).GetEnumerator();
    }

    // Token: 0x06000480 RID: 1152 RVA: 0x00082438 File Offset: 0x00080638
    public virtual void doSubtleHighlight(Transform followTransform)
    {
        this.subtleHighlighter.gameObject.SetActive(true);
        int num = 1;
        Color color = this.subtleHighlighterImg.color;
        float num2 = color.a = (float)num;
        Color color2 = this.subtleHighlighterImg.color = color;
        this.subtleHighlighterFollowTransform = followTransform;
        this.subtleHighlighter.position = this.mainCamera.WorldToScreenPoint(this.subtleHighlighterFollowTransform.position);
    }

    // Token: 0x06000481 RID: 1153 RVA: 0x000824B0 File Offset: 0x000806B0
    public virtual void bulletHitFreeze()
    {
        if (!this.cinematicShot)
        {
            this.bulletHitFreezeTimer = (float)2 + ((float)1 - this.playerScript.health) * (float)3;
            this.unityTimescale = Mathf.Clamp01(this.playerScript.health - 0.3f) + 0.2f;
        }
    }

    // Token: 0x06000482 RID: 1154 RVA: 0x00082504 File Offset: 0x00080704
    public virtual void giveScore(float amount, string name, bool increaseMultiplier)
    {
        if (!this.cCheckGiSc)
        {
            this.cCheck = true;
        }
        if (this.trailerMode)
        {
            this.cCheckMu = true;
            this.multiplier = (float)0;
        }
        this.cCheckTSc = true;
        this.tempScore += amount;
        this.inAirScore += amount;
        this.timeSinceScoreLastGiven = (float)0;
        if (increaseMultiplier)
        {
            this.cCheckMu = true;
            this.multiplier += (float)1;
            if (this.multiplier > (float)this.statsTracker.highestCombo)
            {
                this.statsTracker.highestCombo = (int)this.multiplier;
                this.statsTracker.achievementCheck();
            }
            if (!this.trailerMode)
            {
                this.hudAudio.clip = this.scoreGetSound;
                this.hudAudio.loop = false;
                this.hudAudio.volume = UnityEngine.Random.Range(0.25f, 0.35f) * Mathf.Clamp01(0.4f + this.timescale);
                this.hudAudio.pitch = UnityEngine.Random.Range(0.65f, 1.35f) * Mathf.Clamp01(0.4f + this.timescale);
                this.hudAudio.Play();
            }
            this.cCheckMuTi = true;
            this.multiplierTimer = (float)1;
        }
        else
        {
            this.cCheckMuTi = true;
            this.multiplierTimer = Mathf.Clamp01(this.multiplierTimer + amount * 0.001f);
        }
        this.scoreHudBallImage.color = Color.white;
        this.slowMotionAmount = Mathf.Clamp01(this.slowMotionAmount + amount * 0.0001f);
        if (this.multiplierTimer >= (float)1)
        {
            this.slowMotionAmount = Mathf.Clamp01(this.slowMotionAmount + 0.2f);
            this.scoreHudBall.localScale = Vector3.one;
            this.scoreHudBallImage.color = Color.white * (float)5;
            this.cCheckMuTi = true;
            this.multiplierTimer = (float)1;
        }
        if (!this.rootShared.lowEndHardware)
        {
            string rhs;
            if (LocalizationManager.CurrentLanguageCode == "fr" || LocalizationManager.CurrentLanguageCode == "de")
            {
                rhs = " ";
            }
            else
            {
                rhs = string.Empty;
            }
            if (this.prevScoreHudScoreNameText != null && this.prevScoreHudScoreNameText.gameObject.activeSelf && this.scoreHudPreviousScoreName == name)
            {
                Text text = (Text)this.prevScoreHudScoreNameText.GetComponent(typeof(Text));
                if (this.scoreHudPreviousScoreMultiplier == 0)
                {
                    text.text += "<b> x" + rhs + "2</b>";
                }
                else
                {
                    text.text = text.text.Replace(" x" + rhs + (this.scoreHudPreviousScoreMultiplier + 1), " x" + rhs + (this.scoreHudPreviousScoreMultiplier + 2));
                }
                this.scoreHudScoreNameTextScript[(int)Mathf.Repeat((float)(this.scoreHudScoreNameTextCurScript - 1), (float)Extensions.get_length(this.scoreHudScoreNameTextScript))].prepareFollowingPreferredWidth();
                this.scoreHudPreviousScoreMultiplier++;
            }
            else
            {
                this.scoreHudPreviousScoreMultiplier = 0;
                this.scoreHudScoreNameTextScript[this.scoreHudScoreNameTextCurScript].gameObject.SetActive(true);
                this.scoreHudScoreNameTextScript[this.scoreHudScoreNameTextCurScript].doSetup(name, this.prevScoreHudScoreNameText, this.scoreHudScoreNameTextStartPos);
                this.prevScoreHudScoreNameText = this.scoreHudScoreNameTextScript[this.scoreHudScoreNameTextCurScript];
                this.scoreHudScoreNameTextCurScript = (int)Mathf.Repeat((float)(this.scoreHudScoreNameTextCurScript + 1), (float)Extensions.get_length(this.scoreHudScoreNameTextScript));
            }
            if (this.prevScoreHudScoreAmountText != null && this.prevScoreHudScoreAmountText.gameObject.activeSelf && this.scoreHudPreviousScore == amount)
            {
                Text text2 = (Text)this.prevScoreHudScoreAmountText.GetComponent(typeof(Text));
                if (this.scoreHudPreviousScoreMultiplierScore == 0)
                {
                    text2.text += "<b> x" + rhs + "2</b>";
                }
                else
                {
                    text2.text = text2.text.Replace(" x" + rhs + (this.scoreHudPreviousScoreMultiplierScore + 1), " x" + rhs + (this.scoreHudPreviousScoreMultiplierScore + 2));
                }
                this.scoreHudScoreAmountTextScript[(int)Mathf.Repeat((float)(this.scoreHudScoreAmountTextCurScript - 1), (float)Extensions.get_length(this.scoreHudScoreAmountTextScript))].prepareFollowingPreferredWidth();
                this.scoreHudPreviousScoreMultiplierScore++;
            }
            else
            {
                this.scoreHudPreviousScoreMultiplierScore = 0;
                this.scoreHudScoreAmountTextScript[this.scoreHudScoreAmountTextCurScript].gameObject.SetActive(true);
                this.scoreHudScoreAmountTextScript[this.scoreHudScoreAmountTextCurScript].doSetup("+" + rhs + amount, this.prevScoreHudScoreAmountText, this.scoreHudScoreAmountTextStartPos);
                this.prevScoreHudScoreAmountText = this.scoreHudScoreAmountTextScript[this.scoreHudScoreAmountTextCurScript];
                this.scoreHudScoreAmountTextCurScript = (int)Mathf.Repeat((float)(this.scoreHudScoreAmountTextCurScript + 1), (float)Extensions.get_length(this.scoreHudScoreAmountTextScript));
            }
            this.scoreHudPreviousScoreName = name;
            this.scoreHudPreviousScore = amount;
        }
    }

    // Token: 0x06000483 RID: 1155 RVA: 0x00082A64 File Offset: 0x00080C64
    public virtual void markMiniLevelTimeComplete()
    {
        this.miniLevelSuccess = true;
        float num = Time.unscaledTime - this.startTime;
        this.miniLevelCurTimeUIText.text = num.ToString();
        int num2 = int.Parse(SceneManager.GetActiveScene().name.Replace("mini_", string.Empty));
        float @float = SavedData.GetFloat("miniTime" + num2);
        if (@float <= (float)0 || num < @float)
        {
            this.miniLevelBestTimeUIText.text = "NEW BEST TIME!";
            SavedData.SetFloat("miniTime" + num2, num);
        }
    }

    // Token: 0x06000484 RID: 1156 RVA: 0x00082B04 File Offset: 0x00080D04
    public virtual void saveProgress()
    {
        this.rootShared.levelLoadedFromLevelSelectScreen = false;
        SavedData.SetInt("level", SceneManager.GetActiveScene().buildIndex);
        this.saveLevelSelectProgress();
        SavedData.SetInt("hasUsedGameModifier", (!this.rootShared.gameModifiersCheck()) ? 0 : 1);
        SavedData.SetInt("weapon", this.playerScript.weapon);
        SavedData.SetFloat("health", this.playerScript.health);
        SavedData.SetFloat("difficulty", this.difficulty);
        for (int i = 0; i < this.playerScript.nrOfWeapons; i++)
        {
            SavedData.SetFloat("ammo" + i, this.playerScript.ammo[i]);
            SavedData.SetFloat("ammoTotal" + i, this.playerScript.ammoTotal[i]);
            SavedData.SetInt("weaponActive" + i, (!this.playerScript.weaponActive[i]) ? 0 : 1);
            SavedData.SetFloat("secondaryAmmo" + i, this.playerScript.secondaryAmmo[i]);
        }
        PlatformPlayerPrefs.SetInt("gamepad", (!this.playerScript.gamepad) ? 0 : 1);
        this.statsTracker.saveStats();
        PlatformPlayerPrefs.Save();
        SavedData.Save();
    }

    // Token: 0x06000485 RID: 1157 RVA: 0x00082C8C File Offset: 0x00080E8C
    public virtual void saveLevelSelectProgress()
    {
        int num = SceneManager.GetActiveScene().buildIndex + 1;
        if (num == 13)
        {
            num = 14;
        }
        else if (num == 15)
        {
            num = 16;
        }
        else if (num == 29)
        {
            num = 31;
        }
        else if (num == 40)
        {
            num = 41;
        }
        else if (num == 42)
        {
            num = 43;
        }
        else if (num == 51)
        {
            num = 52;
        }
        if (num > SavedData.GetInt("levelSelectMaxNr"))
        {
            SavedData.SetInt("levelSelectMaxNr", num);
        }
    }

    // Token: 0x06000486 RID: 1158 RVA: 0x00082D20 File Offset: 0x00080F20
    public virtual void loadProgress()
    {
        if (this.difficulty == (float)-1)
        {
            this.difficulty = Mathf.Clamp(SavedData.GetFloat("difficulty"), (float)1, (float)10);
        }
        if (SavedData.GetInt("hasUsedGameModifier") == 1 && !this.rootShared.gameModifiersCheck())
        {
            this.rootShared.levelLoadedFromLevelSelectScreen = true;
        }
        if (this.rootShared.levelLoadedFromLevelSelectScreen)
        {
            for (int i = 1; i <= 4; i++)
            {
                if (this.weaponToUseWhenLoadingFromLvlSelectScreen >= i)
                {
                    this.playerScript.weaponActive[i] = true;
                    this.playerScript.ammo[i] = this.ammoFullClip[i];
                    this.playerScript.ammoTotal[i] = this.ammoFullClip[i] * (float)5;
                    this.playerScript.weapon = i;
                }
            }
            if (this.weaponToUseWhenLoadingFromLvlSelectScreen >= 5)
            {
                this.playerScript.weaponActive[6] = true;
                this.playerScript.ammo[6] = this.ammoFullClip[6];
                this.playerScript.ammoTotal[6] = this.ammoFullClip[6] * (float)5;
                this.playerScript.weapon = 6;
            }
            if (this.weaponToUseWhenLoadingFromLvlSelectScreen >= 6)
            {
                this.playerScript.weaponActive[5] = true;
                this.playerScript.ammo[5] = this.ammoFullClip[5];
                this.playerScript.ammoTotal[5] = this.ammoFullClip[5] * (float)5;
                this.playerScript.secondaryAmmo[5] = (float)((!this.rootShared.modInfiniteAmmo) ? 3 : 5);
                this.playerScript.weapon = 5;
            }
            if (this.weaponToUseWhenLoadingFromLvlSelectScreen >= 7)
            {
                this.playerScript.weaponActive[9] = true;
                this.playerScript.ammo[9] = this.ammoFullClip[9];
                this.playerScript.ammoTotal[9] = this.ammoFullClip[9] * (float)5;
                this.playerScript.weapon = 9;
            }
            this.playerScript.health = (float)1;
            this.playerScript.weaponActive[0] = false;
        }
        else if (SavedData.HasKey("weapon"))
        {
            this.playerScript.weapon = SavedData.GetInt("weapon");
            this.playerScript.health = Mathf.Clamp01(SavedData.GetFloat("health") + 0.3f);
            for (int j = 0; j < this.playerScript.nrOfWeapons; j++)
            {
                this.playerScript.ammo[j] = SavedData.GetFloat("ammo" + j);
                this.playerScript.ammoTotal[j] = SavedData.GetFloat("ammoTotal" + j);
                this.playerScript.weaponActive[j] = (SavedData.GetInt("weaponActive" + j) == 1);
                this.playerScript.secondaryAmmo[j] = SavedData.GetFloat("secondaryAmmo" + j);
            }
        }
    }

    // Token: 0x06000487 RID: 1159 RVA: 0x00003E39 File Offset: 0x00002039
    public virtual void clearInstructionText()
    {
        this.instructionBackground.gameObject.SetActive(false);
    }

    // Token: 0x06000488 RID: 1160 RVA: 0x00083060 File Offset: 0x00081260
    public virtual void setUpHintText()
    {
        if (this.hintDodge != null)
        {
            UnityEngine.Object.Destroy(this.hintDodge);
            UnityEngine.Object.Destroy(this.hintDied);
            UnityEngine.Object.Destroy(this.hintFocus);
            UnityEngine.Object.Destroy(this.hintChangeWeapon);
            UnityEngine.Object.Destroy(this.hintReload);
            UnityEngine.Object.Destroy(this.hintHealthFull);
            UnityEngine.Object.Destroy(this.hintAmmoFull);
            UnityEngine.Object.Destroy(this.hintKick);
            UnityEngine.Object.Destroy(this.hintGrab);
            UnityEngine.Object.Destroy(this.hintFlipSkateboard);
            UnityEngine.Object.Destroy(this.hintPressButton);
            UnityEngine.Object.Destroy(this.hintFlipLever);
            UnityEngine.Object.Destroy(this.hintFlipTable);
            UnityEngine.Object.Destroy(this.hintOpen);
            UnityEngine.Object.Destroy(this.hintSwing);
        }
        Transform transform = GameObject.Find("HUD/Canvas/HintsHolder").transform;
        this.hintDodge = this.createHintTextParent(this.GetTranslation("hint1"), "hintDodge", transform);
        this.hintDied = this.createHintTextParent(this.GetTranslation("hint2"), "hintDied", transform);
        this.hintFocus = this.createHintTextParent(this.GetTranslation("hint3"), "hintFocus", transform);
        this.hintChangeWeapon = this.createHintTextParent(this.GetTranslation("hint4"), "hintChangeWeapon", transform);
        this.hintReload = this.createHintTextParent(this.GetTranslation("hint5"), "hintReload", transform);
        this.hintHealthFull = this.createHintTextParent(this.GetTranslation("hint6"), "hintHealthFull", transform);
        this.hintAmmoFull = this.createHintTextParent(this.GetTranslation("hint7"), "hintAmmoFull", transform);
        this.hintKick = this.createHintTextParent(this.GetTranslation("interact1"), "hintKick", transform);
        this.hintGrab = this.createHintTextParent(this.GetTranslation("interact2"), "hintGrab", transform);
        this.hintFlipSkateboard = this.createHintTextParent(this.GetTranslation("interact3"), "hintFlipSkateboard", transform);
        this.hintPressButton = this.createHintTextParent(this.GetTranslation("interact4"), "hintPressButton", transform);
        this.hintFlipLever = this.createHintTextParent(this.GetTranslation("interact5"), "hintFlipLever", transform);
        this.hintFlipTable = this.createHintTextParent(this.GetTranslation("interact6"), "hintFlipTable", transform);
        this.hintOpen = this.createHintTextParent(this.GetTranslation("interact7"), "hintOpen", transform);
        this.hintSwing = this.createHintTextParent(this.GetTranslation("interact8"), "hintSwing", transform);
        this.hintDodge.AddComponent(typeof(SinewaveScaleScript));
        int i = 0;
        Text[] componentsInChildren = this.hintDodge.transform.GetComponentsInChildren<Text>();
        int length = componentsInChildren.Length;
        while (i < length)
        {
            if (componentsInChildren[i].transform.parent == this.hintDodge.transform)
            {
                componentsInChildren[i].color = new Color((float)255, (float)192, (float)135, (float)255) / (float)255;
            }
            i++;
        }
        this.hintDodge.SetActive(false);
        this.hintDied.SetActive(false);
        this.hintFocus.SetActive(false);
        this.hintChangeWeapon.SetActive(false);
        this.hintReload.SetActive(false);
        this.hintHealthFull.SetActive(false);
        this.hintAmmoFull.SetActive(false);
        this.hintKick.SetActive(false);
        this.hintGrab.SetActive(false);
        this.hintFlipSkateboard.SetActive(false);
        this.hintPressButton.SetActive(false);
        this.hintFlipLever.SetActive(false);
        this.hintFlipTable.SetActive(false);
        this.hintOpen.SetActive(false);
        this.hintSwing.SetActive(false);
    }

    // Token: 0x06000489 RID: 1161 RVA: 0x00083430 File Offset: 0x00081630
    public virtual void clearHintText()
    {
        GameObject gameObject = GameObject.Find("HUD/Canvas/HintHolder");
        if (gameObject != null)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }

    // Token: 0x0600048A RID: 1162 RVA: 0x0008345C File Offset: 0x0008165C
    public virtual void hintText(string txt, float timer, bool overrideText, int hintID)
    {
        bool flag = default(bool);
        if (txt == string.Empty)
        {
            this.clearHintText();
        }
        else if (overrideText)
        {
            flag = true;
        }
        else if (!overrideText && GameObject.Find("HUD/Canvas/HintHolder") == null)
        {
            flag = true;
        }
        if (flag && !this.trailerMode)
        {
            this.clearHintText();
            this.createHintText(txt, "HintHolder");
        }
        this.hintTextID = hintID;
        this.hintTextTimer = timer;
        MonoBehaviour.print("---- CREATED NEW HINT TEXT - THIS SHOULD NOT HAPPEN ----------------------------------");
    }

    // Token: 0x0600048B RID: 1163 RVA: 0x00003E4C File Offset: 0x0000204C
    public virtual GameObject createHintTextParent(string txt, string holderName, Transform theParent)
    {
        return this.rootShared.createHintText(txt, holderName, theParent, this.useGamepadIcons, false);
    }

    // Token: 0x0600048C RID: 1164 RVA: 0x000834F8 File Offset: 0x000816F8
    public virtual GameObject createHintText(string txt, string holderName)
    {
        return this.rootShared.createHintText(txt, holderName, this.hudCanvas.transform, this.useGamepadIcons, false);
    }

    // Token: 0x0600048D RID: 1165 RVA: 0x00083524 File Offset: 0x00081724
    public virtual void speechBubble(Transform trnsfrm, Vector3 offSet, string txt, string speakerName, bool clickToContinue, bool clickToContinueDontFreeze, float speechBubbleTimerMultiplier, float appearDelay, Transform triggerTransform, Animator targetAnimator)
    {
        this.sbTriggerTransform = triggerTransform;
        this.sbTimerMultiplier = speechBubbleTimerMultiplier;
        this.sbAnimator = targetAnimator;
        if (appearDelay <= (float)0)
        {
            this.sbBubble.gameObject.SetActive(true);
            this.sbTail.gameObject.SetActive(true);
            this.setSpeechBubbleSize();
            this.voiceController.voiceTimer = (float)0;
            this.voice.Play();
            this.sbDoOnce = true;
        }
        else
        {
            this.voice.Stop();
            this.sbBubble.gameObject.SetActive(false);
            this.sbTail.gameObject.SetActive(false);
            this.sbDoOnce = false;
        }
        this.sbAppearDelay = appearDelay;
        this.sbCurStringInArray = 0;
        if (txt.EndsWith("|"))
        {
            txt = txt.Remove(txt.Length - 1);
        }
        this.sbStringArray = txt.Split(new char[]
        {
            "|"[0]
        });
        if (speakerName != string.Empty || speakerName != null)
        {
            if (speakerName == "-")
            {
                this.sbNameHolder.gameObject.SetActive(false);
            }
            else
            {
                this.sbNameHolder.gameObject.SetActive(true);
                this.sbName.text = speakerName;
                float x = this.sbName.preferredWidth + (float)8;
                Vector2 sizeDelta = this.sbNameHolder.sizeDelta;
                float num = sizeDelta.x = x;
                Vector2 vector = this.sbNameHolder.sizeDelta = sizeDelta;
            }
        }
        this.sbTransform = trnsfrm;
        this.sbOffset = offSet;
        this.setSpeechBubbleText();
        this.sbClickCont = clickToContinue;
        this.sbClickContDontFreeze = clickToContinueDontFreeze;
        if (!this.sbClickCont)
        {
            this.sbTimer = (float)(60 + this.sbStringArray[this.sbCurStringInArray].Length * 5);
        }
        else
        {
            this.sbTimer = (float)0;
        }
        if (this.sbClickCont)
        {
            if (this.useGamepadIcons)
            {
                this.sbClickIndicatorKeyboard.gameObject.SetActive(false);
                this.sbClickIndicatorGamepad.gameObject.SetActive(true);
            }
            else
            {
                this.sbClickIndicatorKeyboard.gameObject.SetActive(true);
                this.sbClickIndicatorGamepad.gameObject.SetActive(false);
            }
        }
        this.sbClickIndicator.gameObject.SetActive(this.sbClickCont);
    }

    // Token: 0x0600048E RID: 1166 RVA: 0x000837C0 File Offset: 0x000819C0
    private void setSpeechBubbleText()
    {
        int num = this.sbStringArray[this.sbCurStringInArray].IndexOf("[");
        if (num != -1)
        {
            int num2 = this.sbStringArray[this.sbCurStringInArray].IndexOf("]", num);
            string text = this.sbStringArray[this.sbCurStringInArray].Substring(num + 1, num2 - num - 1);
            string text2 = null;
            int num3 = 0;
            if (text[text.Length - 2] == '#')
            {
                num3 = UnityBuiltins.parseInt((int)text[text.Length - 1]) - 48;
                text = text.Remove(text.Length - 2, 2);
            }
            if (text.Contains("?"))
            {
                int num4 = text.IndexOf("?");
                text2 = text.Substring(num4 + 1);
                text = text.Remove(num4, text.Length - num4);
            }
            this.sbStringArray[this.sbCurStringInArray] = this.sbStringArray[this.sbCurStringInArray].Remove(num, num2 - num + 1);
            if (this.sbAnimator != null)
            {
                if (text2 != null && text2 != string.Empty)
                {
                    if (this.sbAnimator.GetCurrentAnimatorStateInfo(0).IsName(text2))
                    {
                        this.sbAnimator.CrossFadeInFixedTime(text, (float)((num3 != 0) ? 0 : 1), num3);
                    }
                }
                else
                {
                    this.sbAnimator.CrossFadeInFixedTime(text, (float)((num3 != 0) ? 0 : 1), num3);
                }
            }
            else
            {
                MonoBehaviour.print("Text suggest to play animation, but there is no linked Animator");
            }
        }
        if (this.sbStringArray[this.sbCurStringInArray].IndexOf("<shake>") != -1)
        {
            this.sbStringArray[this.sbCurStringInArray] = this.sbStringArray[this.sbCurStringInArray].Replace("<shake>", string.Empty);
            this.sbShake = (float)120;
        }
        else
        {
            this.sbShake = (float)0;
        }
        this.sbText.text = this.sbStringArray[this.sbCurStringInArray];
        this.setSpeechBubbleSize();
        this.setSpeechBubbleStartPos();
    }

    // Token: 0x0600048F RID: 1167 RVA: 0x000839E0 File Offset: 0x00081BE0
    private void setSpeechBubbleSize()
    {
        float x = (float)20 + Mathf.Clamp(this.sbText.preferredWidth, (float)100, (float)630);
        Vector2 sizeDelta = this.sbBubble.sizeDelta;
        float num = sizeDelta.x = x;
        Vector2 vector = this.sbBubble.sizeDelta = sizeDelta;
        float y = Mathf.Clamp(LayoutUtility.GetPreferredHeight(this.sbText.rectTransform) + (float)20, (float)50, (float)999);
        Vector2 sizeDelta2 = this.sbBubble.sizeDelta;
        float num2 = sizeDelta2.y = y;
        Vector2 vector2 = this.sbBubble.sizeDelta = sizeDelta2;
    }

    // Token: 0x06000490 RID: 1168 RVA: 0x00083A90 File Offset: 0x00081C90
    private void setSpeechBubbleStartPos()
    {
        Vector3 vector = this.mainCamera.WorldToViewportPoint(this.sbTransform.position + this.sbOffset);
        vector.x *= this.hudCanvasRect.sizeDelta.x;
        vector.y *= this.hudCanvasRect.sizeDelta.y;
        float x = vector.x;
        Vector2 anchoredPosition = this.sbBubble.anchoredPosition;
        float num = anchoredPosition.x = x;
        Vector2 vector2 = this.sbBubble.anchoredPosition = anchoredPosition;
        float y = Mathf.Clamp(vector.y + (float)75, this.hudCanvasRect.sizeDelta.y - (float)170, this.hudCanvasRect.sizeDelta.y - (float)50);
        Vector2 anchoredPosition2 = this.sbBubble.anchoredPosition;
        float num2 = anchoredPosition2.y = y;
        Vector2 vector3 = this.sbBubble.anchoredPosition = anchoredPosition2;
    }

    // Token: 0x06000491 RID: 1169 RVA: 0x00083BB4 File Offset: 0x00081DB4
    public virtual void stopSpeechBubble()
    {
        this.sbTimer = (float)0;
        this.sbClickCont = false;
        this.voice.Stop();
        this.sbBubble.gameObject.SetActive(false);
        this.sbTail.gameObject.SetActive(false);
        this.sbDoOnce = false;
    }

    // Token: 0x06000492 RID: 1170 RVA: 0x00083C04 File Offset: 0x00081E04
    public virtual void setUpSpeechBubbleButtonPrompt()
    {
        if (this.sbClickIndicatorKeyboard != null)
        {
            UnityEngine.Object.Destroy(this.sbClickIndicatorKeyboard.gameObject);
        }
        this.sbClickIndicatorKeyboard = (RectTransform)this.inputHelperScript.GetInputSymbol("INTERACT", true).GetComponent(typeof(RectTransform));
        this.sbClickIndicatorKeyboard.SetParent(this.sbClickIndicator);
        this.sbClickIndicatorKeyboard.localScale = Vector3.one;
        this.sbClickIndicatorKeyboard.anchorMax = (this.sbClickIndicatorKeyboard.anchorMin = new Vector2(0.5f, 0.5f));
        this.sbClickIndicatorKeyboard.anchoredPosition = new Vector3(-0.3f, 3.9f, (float)0);
        this.sbClickIndicatorKeyboard.localRotation = Quaternion.Euler((float)0, (float)0, (float)0);
        if (this.useGamepadIcons)
        {
            this.sbClickIndicatorKeyboard.gameObject.SetActive(false);
        }
        if (this.sbClickIndicatorGamepad != null)
        {
            UnityEngine.Object.Destroy(this.sbClickIndicatorGamepad.gameObject);
        }
        this.sbClickIndicatorGamepad = (RectTransform)this.inputHelperScript.GetInputSymbol("INTERACT", false).GetComponent(typeof(RectTransform));
        this.sbClickIndicatorGamepad.SetParent(this.sbClickIndicator);
        this.sbClickIndicatorGamepad.localScale = Vector3.one;
        this.sbClickIndicatorGamepad.anchorMax = (this.sbClickIndicatorGamepad.anchorMin = new Vector2(0.5f, 0.5f));
        this.sbClickIndicatorGamepad.anchoredPosition = new Vector3(-0.3f, 3.9f, (float)0);
        this.sbClickIndicatorGamepad.localRotation = Quaternion.Euler((float)0, (float)0, (float)0);
        if (!this.useGamepadIcons)
        {
            this.sbClickIndicatorGamepad.gameObject.SetActive(false);
        }
    }

    // Token: 0x06000493 RID: 1171 RVA: 0x00003E63 File Offset: 0x00002063
    public virtual void setVoice(AudioClip aClip)
    {
        this.voice.clip = aClip;
    }

    // Token: 0x06000494 RID: 1172 RVA: 0x00083DE4 File Offset: 0x00081FE4
    private void doVoice()
    {
        this.voiceController.voiceLength = (float)this.sbStringArray[this.sbCurStringInArray].Length;
        this.voiceController.curVoiceChar = this.sbStringArray[this.sbCurStringInArray][(int)Mathf.Round(this.voiceController.voiceTimer)];
        this.voiceController.isQuestion = (this.sbStringArray[this.sbCurStringInArray][this.sbStringArray[this.sbCurStringInArray].Length - 1] == '?');
        this.voiceController.doVoice();
    }

    // Token: 0x06000495 RID: 1173 RVA: 0x00083E84 File Offset: 0x00082084
    public virtual void explode(Vector3 pos, float size, int amount, Vector3 vel, string explColor, bool turnOffCollision, bool doSound)
    {
        this.cameraScript.bigScreenShake = ((this.cameraScript.bigScreenShake >= 0.3f) ? this.cameraScript.bigScreenShake : 0.3f);
        for (int i = 0; i < amount; i++)
        {
            this.cameraScript.onOffScreenShake = this.cameraScript.onOffScreenShake + (0.1f + 0.05f * size);
            this.cameraScript.screenShake = this.cameraScript.screenShake + ((float)1 + 0.1f * size);
            this.curExplosion = (int)Mathf.Repeat((float)(this.curExplosion + 1), (float)Extensions.get_length(this.explosions));
            GameObject gameObject = this.explosions[this.curExplosion];
            gameObject.SetActive(true);
            ExplosionVisualScript explosionVisualScript = this.explosionScripts[this.curExplosion];
            explosionVisualScript.doSetup();
            gameObject.transform.position = pos;
            gameObject.transform.rotation = Quaternion.Euler((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));
            Rigidbody rigidbody = this.explosionRigidbodies[this.curExplosion];
            this.explosionSphereColliders[this.curExplosion].isTrigger = turnOffCollision;
            explosionVisualScript.explColor = explColor;
            if (amount == 1)
            {
                gameObject.transform.localScale = Vector3.one * size;
                rigidbody.velocity = vel;
            }
            else if (i == 0)
            {
                gameObject.transform.localScale = Vector3.one * size;
                rigidbody.velocity = vel;
            }
            else
            {
                gameObject.transform.localScale = Vector3.one * UnityEngine.Random.Range(Mathf.Clamp01(size / (float)2), Mathf.Clamp(size, (float)1, size));
                Vector3 normalized = new Vector3((float)UnityEngine.Random.Range(-1, 1), (float)UnityEngine.Random.Range(-1, 1), (float)0).normalized;
                gameObject.transform.position = pos + normalized * (size * 0.15f);
                gameObject.transform.localScale = Vector3.one * (size * UnityEngine.Random.Range(0.3f, 0.6f));
                rigidbody.velocity = normalized * 7.5f;
                explosionVisualScript.dontPlaySound = true;
            }
            explosionVisualScript.dontPlaySound = (i != 0 || !doSound);
            explosionVisualScript.doPostSetup();
        }
        if (doSound)
        {
            for (int j = 0; j < 3 * amount; j++)
            {
                this.debrisParticle.Emit(this.generateEmitParams(pos, -vel.normalized * (float)10 + new Vector3(UnityEngine.Random.Range(-8.5f, 8.5f), (j <= 3 * amount / 2) ? UnityEngine.Random.Range(-12f, (float)0) : UnityEngine.Random.Range((float)0, 12f), UnityEngine.Random.Range((float)0, 0.2f)), UnityEngine.Random.Range(0.22f, 0.52f), UnityEngine.Random.Range(3.5f, (float)6), Color.white), 1);
            }
        }
    }

    // Token: 0x06000496 RID: 1174 RVA: 0x000841B8 File Offset: 0x000823B8
    public virtual void doDisableOutlineEffect()
    {
        if (this.outlineEffect.enabled)
        {
            if (this.hintTextID == 111)
            {
                this.clearHintText();
            }
            this.outlineEffect.enabled = false;
        }
        else
        {
            this.trajectory.SetActive(false);
        }
    }

    // Token: 0x06000497 RID: 1175 RVA: 0x00084208 File Offset: 0x00082408
    public virtual void highlightObjectArray(Transform[] obj, bool showArrow, string hintTxt)
    {
        if (!this.trailerMode)
        {
            this.trajectory.SetActive(this.hintKick.activeSelf);
        }
        if (!this.outlineEffect.enabled)
        {
            if (!this.trailerMode)
            {
                this.outlineEffect.enabled = true;
                this.outlineEffect.lineThickness = 1.6f;
            }
            for (int i = 0; i < Extensions.get_length(obj); i++)
            {
                this.outlineEffect.outlineRenderers[i] = (Renderer)obj[i].GetComponent(typeof(Renderer));
            }
            this.prevOutlinedObject = obj[0];
        }
        if (obj[0] == this.prevOutlinedObject)
        {
            this.disableOutlineEffect = false;
        }
    }

    // Token: 0x06000498 RID: 1176 RVA: 0x000842D8 File Offset: 0x000824D8
    public virtual void highlightObject(Transform obj, bool showArrow, string hintTxt, float thickness)
    {
        if (!this.trailerMode)
        {
            this.trajectory.SetActive(this.hintKick.activeSelf);
        }
        if (!this.outlineEffect.enabled)
        {
            if (!this.trailerMode)
            {
                this.outlineEffect.enabled = true;
                this.outlineEffect.lineThickness = thickness;
            }
            this.outlineEffect.outlineRenderers[0] = (Renderer)obj.GetComponentInChildren(typeof(Renderer));
            this.outlineEffect.outlineRenderers[1] = null;
            this.prevOutlinedObject = obj;
        }
        if (obj == this.prevOutlinedObject)
        {
            this.disableOutlineEffect = false;
        }
    }

    // Token: 0x06000499 RID: 1177 RVA: 0x00084394 File Offset: 0x00082594
    public virtual void changeDifficulty(float amount)
    {
        if (!this.playerScript.onMotorcycle && !this.isTutorialLevel)
        {
            int num = (!this.isMiniLevel) ? SceneManager.GetActiveScene().buildIndex : 1;
            this.difficulty = Mathf.Clamp(this.difficulty + amount * 0.7f, ((this.difficultyMode != 2) ? ((this.difficultyMode != 1) ? 0.8f : 1.8f) : 3.8f) + (float)num * 0.05f, (float)10);
        }
    }

    // Token: 0x0600049A RID: 1178 RVA: 0x00084434 File Offset: 0x00082634
    public virtual void doBloodScoreSplat(Vector3 pos, Vector3 dir, string textOverride)
    {
        if (!this.showNoBlood)
        {
            this.cameraScript.kickBack(Mathf.Clamp01(0.1f * this.multiplier));
            this.cameraScript.screenShake = this.cameraScript.screenShake + 0.01f * this.multiplier;
            string rhs;
            if (LocalizationManager.CurrentLanguageCode == "fr" || LocalizationManager.CurrentLanguageCode == "de")
            {
                rhs = " ";
            }
            else
            {
                rhs = string.Empty;
            }
            string theScoreBloodText;
            string theScoreBloodMultiplierText;
            if (this.rootShared.hideHUD)
            {
                theScoreBloodText = string.Empty;
                theScoreBloodMultiplierText = string.Empty;
            }
            else if (textOverride == string.Empty || this.trailerMode)
            {
                if (this.multiplier >= (float)3)
                {
                    theScoreBloodMultiplierText = ((!this.isTutorialLevel) ? ("x" + rhs + (this.multiplier + (float)1)) : string.Empty);
                    theScoreBloodText = this.getEncouragingText();
                }
                else if (this.multiplier >= (float)1)
                {
                    theScoreBloodText = ((!this.isTutorialLevel) ? ("x" + rhs + (this.multiplier + (float)1)) : string.Empty);
                    theScoreBloodMultiplierText = string.Empty;
                }
                else
                {
                    theScoreBloodText = string.Empty;
                    theScoreBloodMultiplierText = string.Empty;
                }
            }
            else
            {
                if (this.multiplier > (float)1)
                {
                    theScoreBloodMultiplierText = ((!this.isTutorialLevel) ? ("x" + rhs + (this.multiplier + (float)1)) : string.Empty);
                }
                else
                {
                    theScoreBloodMultiplierText = string.Empty;
                }
                theScoreBloodText = textOverride;
            }
            float num = Mathf.Clamp((float)14 + this.multiplier * 0.4f, (float)14, (float)20);
            this.scoreBloodScripts[this.curScoreBloodScript].doSetup(this.multiplier, pos, dir, theScoreBloodText, theScoreBloodMultiplierText, num, num + (float)10);
            this.scoreBloodScripts[this.curScoreBloodScript].gameObject.SetActive(true);
            this.curScoreBloodScript = (int)Mathf.Repeat((float)(this.curScoreBloodScript + 1), (float)3);
        }
    }

    // Token: 0x0600049B RID: 1179 RVA: 0x0008466C File Offset: 0x0008286C
    public virtual string getEncouragingText()
    {
        float num = UnityEngine.Random.value * (float)2;
        return (num <= 1.9f) ? ((num <= 1.8f) ? ((num <= 1.7f) ? ((num <= 1.6f) ? ((num <= 1.5f) ? ((num <= 1.4f) ? ((num <= 1.3f) ? ((num <= 1.2f) ? ((num <= 1.1f) ? ((num <= 1f) ? ((num <= 0.9f) ? ((num <= 0.8f) ? ((num <= 0.7f) ? ((num <= 0.6f) ? ((num <= 0.5f) ? ((num <= 0.4f) ? ((num <= 0.3f) ? ((num <= 0.2f) ? ((num <= 0.1f) ? this.GetTranslation("pep20") : this.GetTranslation("pep19")) : this.GetTranslation("pep18")) : this.GetTranslation("pep17")) : this.GetTranslation("pep16")) : this.GetTranslation("pep15")) : this.GetTranslation("pep14")) : this.GetTranslation("pep13")) : this.GetTranslation("pep12")) : this.GetTranslation("pep11")) : this.GetTranslation("pep10")) : this.GetTranslation("pep9")) : this.GetTranslation("pep8")) : this.GetTranslation("pep7")) : this.GetTranslation("pep6")) : this.GetTranslation("pep5")) : this.GetTranslation("pep4")) : this.GetTranslation("pep3")) : this.GetTranslation("pep2")) : this.GetTranslation("pep1");
    }

    // Token: 0x0600049C RID: 1180 RVA: 0x00003E71 File Offset: 0x00002071
    public virtual int getAdjustedWeaponNr(int i)
    {
        return (i != 5) ? ((i != 6) ? ((i != 7) ? ((i != 9) ? i : 7) : 9) : 5) : 6;
    }

    // Token: 0x0600049D RID: 1181 RVA: 0x00084894 File Offset: 0x00082A94
    public virtual void doPickUpNotification(int weaponNr, float amount, bool doPercentage)
    {
        if (!this.trailerMode)
        {
            this.pickupNotificationHolder.gameObject.SetActive(true);
            int i = 0;
            PickUpNotificationScript[] array = this.pickupNotificationScripts;
            int length = array.Length;
            while (i < length)
            {
                array[i].yPosOffset = array[i].yPosOffset + (float)30;
                i++;
            }
            this.pickupNotificationRectTransforms[this.curPickupNotification].gameObject.SetActive(true);
            this.pickupNotificationRectTransforms[this.curPickupNotification].anchoredPosition = new Vector3((float)-50, (float)180, (float)0);
            this.pickupNotificationScripts[this.curPickupNotification].doSetup(weaponNr != 999, (weaponNr != 999) ? this.weaponIcons[weaponNr] : this.healthIcon);
            this.curPickupNotification = (int)Mathf.Repeat((float)(this.curPickupNotification + 1), (float)Extensions.get_length(this.pickupNotificationScripts));
        }
    }

    // Token: 0x0600049E RID: 1182 RVA: 0x0008498C File Offset: 0x00082B8C
    public virtual void unlockSecret(string gameModifierString)
    {
        this.secretUnlockedDoOnce = true;
        this.secretUnlockedTimer = (float)360;
        this.secretUnlockedCanvasGroup.alpha = (float)0;
        this.secretUnlockedText.text = this.GetTranslation(gameModifierString);
        SavedData.SetInt(gameModifierString + "Unlocked", 1);
        this.hudAudio.clip = this.pedroHintSound;
        this.hudAudio.loop = false;
        this.hudAudio.volume = (float)1;
        this.hudAudio.pitch = (float)2;
        this.hudAudio.Play();
        this.secretUnlockedCanvasGroup.gameObject.SetActive(true);
    }

    // Token: 0x0600049F RID: 1183 RVA: 0x00084A30 File Offset: 0x00082C30
    public virtual ParticleSystem.EmitParams generateEmitParams(Vector3 pos, Vector3 vel, float size, float lifetime, Color color)
    {
        return new ParticleSystem.EmitParams
        {
            position = pos,
            velocity = vel,
            startSize = size,
            startLifetime = lifetime,
            startColor = color
        };
    }

    // Token: 0x060004A0 RID: 1184 RVA: 0x00084A7C File Offset: 0x00082C7C
    public virtual void trackStat(string statName)
    {
        if (statName == "dodged")
        {
            this.statsTracker.bulletsDodged = this.statsTracker.bulletsDodged + 1;
            this.statsTracker.achievementCheck();
        }
        else if (statName == "fryingPanBounceKill")
        {
            this.statsTracker.fryingPanBounceKills = this.statsTracker.fryingPanBounceKills + 1;
            this.statsTracker.achievementCheck();
        }
    }

    // Token: 0x060004A1 RID: 1185 RVA: 0x00003EAF File Offset: 0x000020AF
    public virtual string GetTranslation(string id)
    {
        return this.rootShared.GetTranslation(id);
    }

    // Token: 0x060004A2 RID: 1186 RVA: 0x00084AF4 File Offset: 0x00082CF4
    public virtual void initializeMusic(AudioClip introClip, AudioClip loopClip)
    {
        if (introClip != null)
        {
            this.musicAudioSource.clip = introClip;
            this.musicAudioSource.loop = false;
            this.musicAudioSource.Play();
            if (loopClip != null)
            {
                this.musicAudioSource2 = (AudioSource)this.musicAudioSource.gameObject.AddComponent(typeof(AudioSource));
                this.musicAudioSource2.outputAudioMixerGroup = this.musicAudioSource.outputAudioMixerGroup;
                this.musicAudioSource2.clip = loopClip;
                this.musicAudioSource2.playOnAwake = false;
                this.musicAudioSource2.loop = true;
                if (!this.loopMusicIntro)
                {
                    this.musicAudioSource2.PlayDelayed(introClip.length);
                }
                else
                {
                    this.musicAudioSource.loop = true;
                }
            }
        }
        else if (loopClip != null)
        {
            this.musicAudioSource.clip = loopClip;
            this.musicAudioSource.loop = true;
            this.musicAudioSource.Play();
        }
        this.hasInitializedMusic = true;
    }

    // Token: 0x060004A3 RID: 1187 RVA: 0x00084C04 File Offset: 0x00082E04
    public virtual void StopIntroMusicLoop()
    {
        this.musicAudioSource.loop = false;
        this.loopMusicIntro = false;
        this.musicAudioSource2.PlayDelayed(this.musicIntro.length - this.musicAudioSource.time);
        this.normalStateAudioSnapshot.TransitionTo(this.musicIntro.length - this.musicAudioSource.time + 0.5f);
        this.lastActivatedAudioSnapshot = 0;
    }

    // Token: 0x060004A4 RID: 1188 RVA: 0x00003EBD File Offset: 0x000020BD
    public virtual void rumble(int motorNr, float strength, float duration)
    {
        if (this.playerScript.gamepad && !this.rootShared.disableRumble)
        {
            this.player.SetVibration(motorNr, strength, duration, false);
        }
    }

    // Token: 0x060004A5 RID: 1189 RVA: 0x00084C78 File Offset: 0x00082E78
    public virtual void resetTimeStuff()
    {
        this.kAction = false;
        this.paused = false;
        this.unityTimescale = (float)1;
        this.targetUnityTimescale = (float)1;
        Time.timeScale = this.startUnityTimeScale;
        Time.fixedDeltaTime = this.startUnityFixedDeltaTime;
        Time.maximumDeltaTime = this.startUnityMaximumDeltaTime;
    }

    // Token: 0x060004A6 RID: 1190 RVA: 0x00084CC4 File Offset: 0x00082EC4
    public virtual GameObject getBullet(Vector3 pos, Quaternion rot)
    {
        this.bullets[this.curBullet].SetActive(true);
        this.bullets[this.curBullet].transform.position = pos;
        this.bullets[this.curBullet].transform.rotation = rot;
        this.bulletScripts[this.curBullet].doSetup();
        return this.bullets[this.curBullet];
    }

    // Token: 0x060004A7 RID: 1191 RVA: 0x00084D34 File Offset: 0x00082F34
    public virtual BulletScript getBulletScript()
    {
        BulletScript result = this.bulletScripts[this.curBullet];
        this.curBullet = (int)Mathf.Repeat((float)(this.curBullet + 1), (float)Extensions.get_length(this.bullets));
        return result;
    }

    // Token: 0x060004A8 RID: 1192 RVA: 0x00084D74 File Offset: 0x00082F74
    public virtual GameObject getMuzzleFlash(int nr, Vector3 pos, Quaternion rot)
    {
        GameObject result;
        if (nr == 0)
        {
            this.curMfPistol = (int)Mathf.Repeat((float)(this.curMfPistol + 1), (float)Extensions.get_length(this.mfPistol));
            this.mfPistol[this.curMfPistol].transform.position = pos;
            this.mfPistol[this.curMfPistol].transform.rotation = rot;
            this.mfPistol[this.curMfPistol].transform.localScale = Vector3.one;
            this.mfPistol[this.curMfPistol].gameObject.SetActive(true);
            this.mfPistol[this.curMfPistol].doSetup();
            result = this.mfPistol[this.curMfPistol].gameObject;
        }
        else if (nr == 1)
        {
            this.curMfUzi = (int)Mathf.Repeat((float)(this.curMfUzi + 1), (float)Extensions.get_length(this.mfUzi));
            this.mfUzi[this.curMfUzi].transform.position = pos;
            this.mfUzi[this.curMfUzi].transform.rotation = rot;
            this.mfUzi[this.curMfUzi].transform.localScale = Vector3.one;
            this.mfUzi[this.curMfUzi].gameObject.SetActive(true);
            this.mfUzi[this.curMfUzi].doSetup();
            result = this.mfUzi[this.curMfUzi].gameObject;
        }
        else if (nr == 2)
        {
            this.curMfAssaultRifle = (int)Mathf.Repeat((float)(this.curMfAssaultRifle + 1), (float)Extensions.get_length(this.mfAssaultRifle));
            this.mfAssaultRifle[this.curMfAssaultRifle].transform.position = pos;
            this.mfAssaultRifle[this.curMfAssaultRifle].transform.rotation = rot;
            this.mfAssaultRifle[this.curMfAssaultRifle].transform.localScale = Vector3.one;
            this.mfAssaultRifle[this.curMfAssaultRifle].gameObject.SetActive(true);
            this.mfAssaultRifle[this.curMfAssaultRifle].doSetup();
            result = this.mfAssaultRifle[this.curMfAssaultRifle].gameObject;
        }
        else if (nr == 3)
        {
            this.curMfShotgun = (int)Mathf.Repeat((float)(this.curMfShotgun + 1), (float)Extensions.get_length(this.mfShotgun));
            this.mfShotgun[this.curMfShotgun].transform.position = pos;
            this.mfShotgun[this.curMfShotgun].transform.rotation = rot;
            this.mfShotgun[this.curMfShotgun].transform.localScale = Vector3.one;
            this.mfShotgun[this.curMfShotgun].gameObject.SetActive(true);
            this.mfShotgun[this.curMfShotgun].doSetup();
            result = this.mfShotgun[this.curMfShotgun].gameObject;
        }
        else if (nr == 4)
        {
            this.curMfSniper = (int)Mathf.Repeat((float)(this.curMfSniper + 1), (float)Extensions.get_length(this.mfSniper));
            this.mfSniper[this.curMfSniper].transform.position = pos;
            this.mfSniper[this.curMfSniper].transform.rotation = rot;
            this.mfSniper[this.curMfSniper].transform.localScale = Vector3.one;
            this.mfSniper[this.curMfSniper].gameObject.SetActive(true);
            this.mfSniper[this.curMfSniper].doSetup();
            result = this.mfSniper[this.curMfSniper].gameObject;
        }
        else
        {
            result = null;
        }
        return result;
    }

    // Token: 0x060004A9 RID: 1193 RVA: 0x0008510C File Offset: 0x0008330C
    public virtual void doHideHUD(bool doHide)
    {
        if (doHide)
        {
            this.canvasWeaponPanel.enabled = (this.canvasBigMultiplier.enabled = (this.canvasBigScreenReaction.enabled = (this.canvasBossHealth.enabled = (this.canvasDamageCanvas.enabled = (this.canvasEnemySpeechHolder.enabled = (this.canvasHealthAndSlowMo.enabled = (this.canvasHintsHolder.enabled = (this.canvasInstructionBackground.enabled = (this.canvasPedroHint.enabled = (this.canvasPickupNotificationHolder.enabled = (this.canvasScoreHud.enabled = (this.canvasWeaponHighlightHolder.enabled = false))))))))))));
        }
        else
        {
            this.canvasWeaponPanel.enabled = (this.canvasBigMultiplier.enabled = (this.canvasBigScreenReaction.enabled = (this.canvasBossHealth.enabled = (this.canvasDamageCanvas.enabled = (this.canvasEnemySpeechHolder.enabled = (this.canvasHealthAndSlowMo.enabled = (this.canvasHintsHolder.enabled = (this.canvasInstructionBackground.enabled = (this.canvasPedroHint.enabled = (this.canvasPickupNotificationHolder.enabled = (this.canvasScoreHud.enabled = (this.canvasWeaponHighlightHolder.enabled = true))))))))))));
        }
    }

    // Token: 0x060004AA RID: 1194 RVA: 0x00003EEE File Offset: 0x000020EE
    public virtual void OnApplicationQuit()
    {
        if (this.levelEnded)
        {
            MonoBehaviour.print("Saving progress on Quit");
            this.saveProgress();
        }
    }

    // Token: 0x060004AB RID: 1195 RVA: 0x000852B4 File Offset: 0x000834B4
    public virtual float Damp(float target, float source, float smoothing)
    {
        float num = Mathf.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.timescale));
        return float.IsNaN(num) ? source : num;
    }

    // Token: 0x060004AC RID: 1196 RVA: 0x000852F4 File Offset: 0x000834F4
    public virtual float DampAdd(float target, float source, float smoothing)
    {
        float num = Mathf.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.timescale)) - source;
        return float.IsNaN(num) ? source : num;
    }

    // Token: 0x060004AD RID: 1197 RVA: 0x00085334 File Offset: 0x00083534
    public virtual float DampUnscaled(float target, float source, float smoothing)
    {
        float num = Mathf.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * (Time.unscaledDeltaTime * (float)60)));
        return float.IsNaN(num) ? source : num;
    }

    // Token: 0x060004AE RID: 1198 RVA: 0x00085374 File Offset: 0x00083574
    public virtual float DampAddUnscaled(float target, float source, float smoothing)
    {
        float num = Mathf.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * (Time.unscaledDeltaTime * (float)60))) - source;
        return float.IsNaN(num) ? source : num;
    }

    // Token: 0x060004AF RID: 1199 RVA: 0x000853B8 File Offset: 0x000835B8
    public virtual Vector2 DampV2(Vector2 target, Vector2 source, float smoothing)
    {
        Vector2 vector = Vector2.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.timescale));
        return (float.IsNaN(vector.x) || float.IsNaN(vector.y)) ? source : vector;
    }

    // Token: 0x060004B0 RID: 1200 RVA: 0x0008540C File Offset: 0x0008360C
    public virtual Vector2 DampV2Unscaled(Vector2 target, Vector2 source, float smoothing)
    {
        Vector2 vector = Vector2.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * (Time.unscaledDeltaTime * (float)60)));
        return (float.IsNaN(vector.x) || float.IsNaN(vector.y)) ? source : vector;
    }

    // Token: 0x060004B1 RID: 1201 RVA: 0x00085464 File Offset: 0x00083664
    public virtual Vector2 DampAddV2Unscaled(Vector2 target, Vector2 source, float smoothing)
    {
        Vector2 vector = Vector2.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * (Time.unscaledDeltaTime * (float)60))) - source;
        return (float.IsNaN(vector.x) || float.IsNaN(vector.y)) ? source : vector;
    }

    // Token: 0x060004B2 RID: 1202 RVA: 0x000854C4 File Offset: 0x000836C4
    public virtual Vector3 DampV3(Vector3 target, Vector3 source, float smoothing)
    {
        Vector3 vector = Vector3.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.timescale));
        return (float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z)) ? source : vector;
    }

    // Token: 0x060004B3 RID: 1203 RVA: 0x0008552C File Offset: 0x0008372C
    public virtual Vector3 DampV3Unscaled(Vector3 target, Vector3 source, float smoothing)
    {
        Vector3 vector = Vector3.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * (Time.unscaledDeltaTime * (float)60)));
        return (float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z)) ? source : vector;
    }

    // Token: 0x060004B4 RID: 1204 RVA: 0x00085594 File Offset: 0x00083794
    public virtual Vector3 DampAddV3(Vector3 target, Vector3 source, float smoothing)
    {
        Vector3 vector = Vector3.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.timescale)) - source;
        return (float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z)) ? source : vector;
    }

    // Token: 0x060004B5 RID: 1205 RVA: 0x00085600 File Offset: 0x00083800
    public virtual Color DampColor(Color target, Color source, float smoothing)
    {
        Color color = Color.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.timescale));
        return (float.IsNaN(color.r) || float.IsNaN(color.g) || float.IsNaN(color.b) || float.IsNaN(color.a)) ? source : color;
    }

    // Token: 0x060004B6 RID: 1206 RVA: 0x00085678 File Offset: 0x00083878
    public virtual Color DampColorUnscaled(Color target, Color source, float smoothing)
    {
        Color color = Color.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * (Time.unscaledDeltaTime * (float)60)));
        return (float.IsNaN(color.r) || float.IsNaN(color.g) || float.IsNaN(color.b) || float.IsNaN(color.a)) ? source : color;
    }

    // Token: 0x060004B7 RID: 1207 RVA: 0x000856F4 File Offset: 0x000838F4
    public virtual Quaternion DampSlerp(Quaternion target, Quaternion source, float smoothing)
    {
        Quaternion quaternion = Quaternion.Slerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.timescale));
        return (float.IsNaN(quaternion.x) || float.IsNaN(quaternion.y) || float.IsNaN(quaternion.z) || float.IsNaN(quaternion.w)) ? source : quaternion;
    }

    // Token: 0x060004B8 RID: 1208 RVA: 0x0008576C File Offset: 0x0008396C
    public virtual Quaternion DampSlerpUnscaled(Quaternion target, Quaternion source, float smoothing)
    {
        Quaternion quaternion = Quaternion.Slerp(source, target, (float)1 - Mathf.Exp(-smoothing * (Time.unscaledDeltaTime * (float)60)));
        return (float.IsNaN(quaternion.x) || float.IsNaN(quaternion.y) || float.IsNaN(quaternion.z) || float.IsNaN(quaternion.w)) ? source : quaternion;
    }

    // Token: 0x060004B9 RID: 1209 RVA: 0x000857E8 File Offset: 0x000839E8
    public virtual Quaternion DampSlerpFixed(Quaternion target, Quaternion source, float smoothing)
    {
        Quaternion quaternion = Quaternion.Slerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.fixedTimescale));
        return (float.IsNaN(quaternion.x) || float.IsNaN(quaternion.y) || float.IsNaN(quaternion.z) || float.IsNaN(quaternion.w)) ? source : quaternion;
    }

    // Token: 0x060004BA RID: 1210 RVA: 0x00085860 File Offset: 0x00083A60
    public virtual float DampFixed(float target, float source, float smoothing)
    {
        float num = Mathf.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.fixedTimescale));
        return float.IsNaN(num) ? source : num;
    }

    // Token: 0x060004BB RID: 1211 RVA: 0x000858A0 File Offset: 0x00083AA0
    public virtual float DampAddFixed(float target, float source, float smoothing)
    {
        float num = Mathf.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.fixedTimescale)) - source;
        return float.IsNaN(num) ? source : num;
    }

    // Token: 0x060004BC RID: 1212 RVA: 0x000858E0 File Offset: 0x00083AE0
    public virtual float DampUnscaledFixed(float target, float source, float smoothing)
    {
        float num = Mathf.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * (Time.fixedUnscaledDeltaTime * (float)50)));
        return float.IsNaN(num) ? source : num;
    }

    // Token: 0x060004BD RID: 1213 RVA: 0x00085920 File Offset: 0x00083B20
    public virtual Vector2 DampV2Fixed(Vector2 target, Vector2 source, float smoothing)
    {
        Vector2 vector = Vector2.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.fixedTimescale));
        return (float.IsNaN(vector.x) || float.IsNaN(vector.y)) ? source : vector;
    }

    // Token: 0x060004BE RID: 1214 RVA: 0x00085974 File Offset: 0x00083B74
    public virtual Vector3 DampV3Fixed(Vector3 target, Vector3 source, float smoothing)
    {
        Vector3 vector = Vector3.Lerp(source, target, (float)1 - Mathf.Exp(-smoothing * this.fixedTimescale));
        return (float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z)) ? source : vector;
    }

    // Token: 0x060004BF RID: 1215 RVA: 0x000020A7 File Offset: 0x000002A7
    public virtual void Main()
    {
    }

    // Token: 0x020000A9 RID: 169
    [CompilerGenerated]
    [Serializable]
    internal sealed class doPedroHint2581 : GenericGenerator<object>
    {
        // Token: 0x04000FD2 RID: 4050
        internal string txt2588;

        // Token: 0x04000FD3 RID: 4051
        internal RootScript self_2589;

        // Token: 0x060004C0 RID: 1216 RVA: 0x00003F0B File Offset: 0x0000210B
        public doPedroHint2581(string txt, RootScript self_)
        {
            this.txt2588 = txt;
            this.self_2589 = self_;
        }

        // Token: 0x060004C1 RID: 1217 RVA: 0x00003F21 File Offset: 0x00002121
        public override IEnumerator<object> GetEnumerator()
        {
            return new RootScript.doPedroHint2581.sealedClass(this.txt2588, this.self_2589);
        }

        // Token: 0x020000AA RID: 170
        [CompilerGenerated]
        [Serializable]
        internal sealed class sealedClass : GenericGeneratorEnumerator<object>, IEnumerator
        {
            // Token: 0x04000FD4 RID: 4052
            internal float ranNr2582;

            // Token: 0x04000FD5 RID: 4053
            internal Transform oldPedroHint2583;

            // Token: 0x04000FD6 RID: 4054
            internal GameObject tempPedroHintText2584;

            // Token: 0x04000FD7 RID: 4055
            internal RectTransform tempPedroHintTextRectTransform2585;

            // Token: 0x04000FD8 RID: 4056
            internal string txt2586;

            // Token: 0x04000FD9 RID: 4057
            internal RootScript self_2587;

            // Token: 0x060004C2 RID: 1218 RVA: 0x00003F34 File Offset: 0x00002134
            public sealedClass(string txt, RootScript self_)
            {

                this.txt2586 = txt;

                this.self_2587 = self_;

            }

            // Token: 0x060004C3 RID: 1219 RVA: 0x000859DC File Offset: 0x00083BDC
            public override bool MoveNext()
            {
                switch (this._state)
                {
                    default:
                        if (this.self_2587.pedroHintTimer <= (float)-3600 && !this.self_2587.disablePedroHintsForThisLevel && !this.self_2587.trailerMode && !this.self_2587.rootShared.disablePedroHints)
                        {
                            this.ranNr2582 = UnityEngine.Random.value;
                            if (this.ranNr2582 > 0.9f)
                            {
                                this.self_2587.pedroHintTopText.text = this.self_2587.GetTranslation("pHintTop1");
                            }
                            else if (this.ranNr2582 > 0.8f)
                            {
                                this.self_2587.pedroHintTopText.text = this.self_2587.GetTranslation("pHintTop2");
                            }
                            else if (this.ranNr2582 > 0.7f)
                            {
                                this.self_2587.pedroHintTopText.text = this.self_2587.GetTranslation("pHintTop3");
                            }
                            else if (this.ranNr2582 > 0.6f)
                            {
                                this.self_2587.pedroHintTopText.text = this.self_2587.GetTranslation("pHintTop4");
                            }
                            else if (this.ranNr2582 > 0.5f)
                            {
                                this.self_2587.pedroHintTopText.text = this.self_2587.GetTranslation("pHintTop5");
                            }
                            else
                            {
                                this.self_2587.pedroHintTopText.text = this.self_2587.GetTranslation("pHintTop6");
                            }
                            this.self_2587.hudAudio.clip = this.self_2587.pedroHintSound;
                            this.self_2587.hudAudio.loop = false;
                            this.self_2587.hudAudio.volume = UnityEngine.Random.Range(0.9f, (float)1);
                            this.self_2587.hudAudio.pitch = UnityEngine.Random.Range(0.95f, 1.1f);
                            this.self_2587.hudAudio.Play();
                            this.oldPedroHint2583 = this.self_2587.pedroHintText.Find("PedroHintHolder");
                            if (this.oldPedroHint2583 != null)
                            {
                                UnityEngine.Object.Destroy(this.oldPedroHint2583.gameObject);
                            }
                            this.tempPedroHintText2584 = this.self_2587.createHintText(this.txt2586, "PedroHintHolder");
                            this.tempPedroHintTextRectTransform2585 = (RectTransform)this.tempPedroHintText2584.GetComponent(typeof(RectTransform));
                            this.tempPedroHintTextRectTransform2585.SetParent(this.self_2587.pedroHintText, false);
                            this.tempPedroHintTextRectTransform2585.anchorMax = (this.tempPedroHintTextRectTransform2585.anchorMin = new Vector2((float)0, (float)0));
                            this.tempPedroHintTextRectTransform2585.anchoredPosition = new Vector2((float)0, (float)0);
                            this.self_2587.pedroHintTimer = (float)360;
                            return this.YieldDefault(2);
                        }
                        break;
                    case 1:
                    IL_327:
                        return false;
                    case 2:
                        return this.YieldDefault(3);
                    case 3:
                        return this.YieldDefault(4);
                    case 4:
                        this.self_2587.pedroHint.SetAsLastSibling();
                        break;
                }
                this.YieldDefault(1);
                return false;
            }
        }
    }
}
