using System;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Lang;

// Token: 0x0200008F RID: 143
[Serializable]
public class PedroBossScript : MonoBehaviour
{

    private bool attackFrame = false; //used to prevent multiple bombs etc being spit at the same time by pedro, probably wont work but eh

    private bool dead = false;
    private Transform targetPlayer;
    private BaseNetworkEntity networkHelper;

    // Token: 0x04000A21 RID: 2593
    private RootScript root;

    // Token: 0x04000A22 RID: 2594
    private Transform thePlayer;

    // Token: 0x04000A23 RID: 2595
    private PlayerScript playerScript;

    // Token: 0x04000A24 RID: 2596
    private GameObject walrus;

    // Token: 0x04000A25 RID: 2597
    private AudioSource theAudioSource;

    // Token: 0x04000A26 RID: 2598
    public AudioClip inhaleSound;

    // Token: 0x04000A27 RID: 2599
    public AudioClip spitSound;

    // Token: 0x04000A28 RID: 2600
    public AudioClip death1Sound;

    // Token: 0x04000A29 RID: 2601
    public AudioClip death2Sound;

    // Token: 0x04000A2A RID: 2602
    public AudioClip deathPopSound;

    // Token: 0x04000A2B RID: 2603
    public AudioClip gagGunSound;

    // Token: 0x04000A2C RID: 2604
    public AudioClip weaponSound;

    // Token: 0x04000A2D RID: 2605
    public SwitchScript startSwitch;

    // Token: 0x04000A2E RID: 2606
    public SwitchScript finishedFirstStateSwitchOutput;

    // Token: 0x04000A2F RID: 2607
    public GameObject projectile;

    // Token: 0x04000A30 RID: 2608
    public GameObject splitProjectile;

    // Token: 0x04000A31 RID: 2609
    public AnimationCurve healthSize;

    // Token: 0x04000A32 RID: 2610
    public AnimationCurve startBossScaleCurve;

    // Token: 0x04000A33 RID: 2611
    private Transform pedroBossIn;

    // Token: 0x04000A34 RID: 2612
    private Transform projectilePoint;

    // Token: 0x04000A35 RID: 2613
    private int attackPattern;

    // Token: 0x04000A36 RID: 2614
    private int attackPatternProjectileCount;

    // Token: 0x04000A37 RID: 2615
    private float shootTimer;

    // Token: 0x04000A38 RID: 2616
    private float shootInterval;

    // Token: 0x04000A39 RID: 2617
    private float moveTimer;

    // Token: 0x04000A3A RID: 2618
    private float moveTimerSpeed;

    // Token: 0x04000A3B RID: 2619
    private int shotsUntilSplitProjectile;

    // Token: 0x04000A3C RID: 2620
    private Vector3 startPos;

    // Token: 0x04000A3D RID: 2621
    private Vector3 startScale;

    // Token: 0x04000A3E RID: 2622
    private Vector3 targetScale;

    // Token: 0x04000A3F RID: 2623
    public int bossState;

    // Token: 0x04000A40 RID: 2624
    private float bossStateTransitionTimer;

    // Token: 0x04000A41 RID: 2625
    public float health;

    // Token: 0x04000A42 RID: 2626
    private GameObject bossHUD;

    // Token: 0x04000A43 RID: 2627
    private RectTransform healthBar;

    // Token: 0x04000A44 RID: 2628
    private Image healthBarImg;

    // Token: 0x04000A45 RID: 2629
    private Color healthBarStartColour;

    // Token: 0x04000A46 RID: 2630
    private Animator theAnimator;

    // Token: 0x04000A47 RID: 2631
    private Transform pistol1;

    // Token: 0x04000A48 RID: 2632
    private Transform pistol2;

    // Token: 0x04000A49 RID: 2633
    private Transform pistol1FirePoint;

    // Token: 0x04000A4A RID: 2634
    private Transform pistol2FirePoint;

    // Token: 0x04000A4B RID: 2635
    private Vector3 pistolStartScale;

    // Token: 0x04000A4C RID: 2636
    private Vector3 pistol1StartPos;

    // Token: 0x04000A4D RID: 2637
    private Quaternion pistol1StartRot;

    // Token: 0x04000A4E RID: 2638
    private Vector3 pistol2StartPos;

    // Token: 0x04000A4F RID: 2639
    private Quaternion pistol2StartRot;

    // Token: 0x04000A50 RID: 2640
    private bool showPistols;

    // Token: 0x04000A51 RID: 2641
    private bool shootLeftGun;

    // Token: 0x04000A52 RID: 2642
    private Material bossInsideMaterial;

    // Token: 0x04000A53 RID: 2643
    private float throb;

    // Token: 0x04000A54 RID: 2644
    private int bossState2AttackPatternCount;

    // Token: 0x04000A55 RID: 2645
    private Material faceMaterial;

    // Token: 0x04000A56 RID: 2646
    private float faceAnimationSpeed;

    // Token: 0x04000A57 RID: 2647
    private float faceAnimation;

    // Token: 0x04000A58 RID: 2648
    private float faceFrameNr;

    // Token: 0x04000A59 RID: 2649
    private float faceFrameNrTimer;

    // Token: 0x04000A5A RID: 2650
    private bool faceAnimationHoldLastFrame;

    // Token: 0x04000A5B RID: 2651
    private Vector3 targetPos;

    // Token: 0x04000A5C RID: 2652
    private float targetRot;

    // Token: 0x04000A5D RID: 2653
    private float xSpeed;

    // Token: 0x04000A5E RID: 2654
    private float ySpeed;

    // Token: 0x04000A5F RID: 2655
    private float rotSpeed;

    // Token: 0x04000A60 RID: 2656
    private float fakeRot;

    // Token: 0x04000A61 RID: 2657
    private float damageWobble;

    // Token: 0x04000A62 RID: 2658
    private float damageWobbleSpeed;

    // Token: 0x04000A63 RID: 2659
    private float damageWobble2;

    // Token: 0x04000A64 RID: 2660
    private float damageWobble2Speed;

    // Token: 0x04000A65 RID: 2661
    private float damageWobble3;

    // Token: 0x04000A66 RID: 2662
    private float damageWobble3Speed;

    // Token: 0x04000A67 RID: 2663
    private Transform wiggleBone;

    // Token: 0x04000A68 RID: 2664
    private Transform[] bananaBones;

    // Token: 0x04000A69 RID: 2665
    private float playerLookRot;

    // Token: 0x04000A6A RID: 2666
    private ParticleSystem fleshParticles;

    // Token: 0x04000A6B RID: 2667
    private ParticleSystem deathParticles;

    // Token: 0x04000A6C RID: 2668
    private Light shootLight;

    // Token: 0x04000A6D RID: 2669
    private bool headPopped;

    // Token: 0x04000A6E RID: 2670
    private SwitchScript switchScript;

    // Token: 0x04000A6F RID: 2671
    private float yMoveSineMultiplier;

    // Token: 0x04000A70 RID: 2672
    private float dynAnimMultiplier;

    // Token: 0x04000A71 RID: 2673
    private Transform startFollowPedro;

    // Token: 0x04000A72 RID: 2674
    private Color startFogColour;

    // Token: 0x04000A73 RID: 2675
    private float timeSinceTakenDamage;

    // Token: 0x04000A74 RID: 2676
    private CutsceneCameraScript bossCam;

    // Token: 0x04000A75 RID: 2677
    private int attackPatternS;

    // Token: 0x04000A76 RID: 2678
    private int attackPatternProjectileCountS;

    // Token: 0x04000A77 RID: 2679
    private float shootTimerS;

    // Token: 0x04000A78 RID: 2680
    private float shootIntervalS;

    // Token: 0x04000A79 RID: 2681
    private float moveTimerS;

    // Token: 0x04000A7A RID: 2682
    private float moveTimerSpeedS;

    // Token: 0x04000A7B RID: 2683
    private int shotsUntilSplitProjectileS;

    // Token: 0x04000A7C RID: 2684
    private Vector3 targetScaleS;

    // Token: 0x04000A7D RID: 2685
    private int bossStateS;

    // Token: 0x04000A7E RID: 2686
    private float bossStateTransitionTimerS;

    // Token: 0x04000A7F RID: 2687
    private float healthS;

    // Token: 0x04000A80 RID: 2688
    private bool showPistolsS;

    // Token: 0x04000A81 RID: 2689
    private bool shootLeftGunS;

    // Token: 0x04000A82 RID: 2690
    private float throbS;

    // Token: 0x04000A83 RID: 2691
    private int bossState2AttackPatternCountS;

    // Token: 0x04000A84 RID: 2692
    private float faceAnimationSpeedS;

    // Token: 0x04000A85 RID: 2693
    private float faceAnimationS;

    // Token: 0x04000A86 RID: 2694
    private float faceFrameNrS;

    // Token: 0x04000A87 RID: 2695
    private float faceFrameNrTimerS;

    // Token: 0x04000A88 RID: 2696
    private bool faceAnimationHoldLastFrameS;

    // Token: 0x04000A89 RID: 2697
    private Vector3 targetPosS;

    // Token: 0x04000A8A RID: 2698
    private float targetRotS;

    // Token: 0x04000A8B RID: 2699
    private float xSpeedS;

    // Token: 0x04000A8C RID: 2700
    private float ySpeedS;

    // Token: 0x04000A8D RID: 2701
    private float rotSpeedS;

    // Token: 0x04000A8E RID: 2702
    private float fakeRotS;

    // Token: 0x04000A8F RID: 2703
    private float damageWobbleS;

    // Token: 0x04000A90 RID: 2704
    private float damageWobbleSpeedS;

    // Token: 0x04000A91 RID: 2705
    private float damageWobble2S;

    // Token: 0x04000A92 RID: 2706
    private float damageWobble2SpeedS;

    // Token: 0x04000A93 RID: 2707
    private float damageWobble3S;

    // Token: 0x04000A94 RID: 2708
    private float damageWobble3SpeedS;

    // Token: 0x04000A95 RID: 2709
    private float playerLookRotS;

    // Token: 0x04000A96 RID: 2710
    private bool headPoppedS;

    // Token: 0x04000A97 RID: 2711
    private float yMoveSineMultiplierS;

    // Token: 0x04000A98 RID: 2712
    private float dynAnimMultiplierS;

    // Token: 0x04000A99 RID: 2713
    private float timeSinceTakenDamageS;

    // Token: 0x04000A9A RID: 2714
    private float healthBarLocalScaleXS;

    // Token: 0x04000A9B RID: 2715
    private bool bossHUDActiveS;

    // Token: 0x04000A9C RID: 2716
    private Vector2 faceMaterialOffsetS;

    // Token: 0x060003A2 RID: 930 RVA: 0x00003940 File Offset: 0x00001B40
    public PedroBossScript()
    {
        this.shootInterval = (float)120;
        this.health = (float)1;
        this.yMoveSineMultiplier = (float)1;
    }

    // Token: 0x060003A3 RID: 931 RVA: 0x000574D8 File Offset: 0x000556D8
    public virtual void saveState()
    {
        this.attackPatternS = this.attackPattern;
        this.attackPatternProjectileCountS = this.attackPatternProjectileCount;
        this.shootTimerS = this.shootTimer;
        this.shootIntervalS = this.shootInterval;
        this.moveTimerS = this.moveTimer;
        this.moveTimerSpeedS = this.moveTimerSpeed;
        this.shotsUntilSplitProjectileS = this.shotsUntilSplitProjectile;
        this.targetScaleS = this.targetScale;
        this.bossStateS = this.bossState;
        this.bossStateTransitionTimerS = this.bossStateTransitionTimer;
        this.healthS = this.health;
        this.showPistolsS = this.showPistols;
        this.shootLeftGunS = this.shootLeftGun;
        this.throbS = this.throb;
        this.bossState2AttackPatternCountS = this.bossState2AttackPatternCount;
        this.faceAnimationSpeedS = this.faceAnimationSpeed;
        this.faceAnimationS = this.faceAnimation;
        this.faceFrameNrS = this.faceFrameNr;
        this.faceFrameNrTimerS = this.faceFrameNrTimer;
        this.faceAnimationHoldLastFrameS = this.faceAnimationHoldLastFrame;
        this.targetPosS = this.targetPos;
        this.targetRotS = this.targetRot;
        this.xSpeedS = this.xSpeed;
        this.ySpeedS = this.ySpeed;
        this.rotSpeedS = this.rotSpeed;
        this.fakeRotS = this.fakeRot;
        this.damageWobbleS = this.damageWobble;
        this.damageWobbleSpeedS = this.damageWobbleSpeed;
        this.damageWobble2S = this.damageWobble2;
        this.damageWobble2SpeedS = this.damageWobble2Speed;
        this.damageWobble3S = this.damageWobble3;
        this.damageWobble3SpeedS = this.damageWobble3Speed;
        this.playerLookRotS = this.playerLookRot;
        this.headPoppedS = this.headPopped;
        this.yMoveSineMultiplierS = this.yMoveSineMultiplier;
        this.dynAnimMultiplierS = this.dynAnimMultiplier;
        this.timeSinceTakenDamageS = this.timeSinceTakenDamage;
        this.healthBarLocalScaleXS = this.healthBar.localScale.x;
        this.bossHUDActiveS = this.bossHUD.activeSelf;
        this.faceMaterialOffsetS = this.faceMaterial.mainTextureOffset;
    }

    // Token: 0x060003A4 RID: 932 RVA: 0x000576DC File Offset: 0x000558DC

    /*
     * 
     * WOULD TAKE 147 BYTES A PACKAGE TO SYNC PEDRO BOSS BASED ON LOADSTATE, TO COMPARE
     * 
     * PLAYER TRANSFORM SYNC IS 244 BYTES
     * PLAYER ANIM SYNC ON A NORMAL LEVEL IS 21 BYTES
     * 
     * 244 = 21 = 265
     * 
     * 
     */

    public virtual void loadState()
    {

        return;

        this.attackPattern = this.attackPatternS;
        this.attackPatternProjectileCount = this.attackPatternProjectileCountS;
        this.shootTimer = this.shootTimerS;
        this.shootInterval = this.shootIntervalS;
        this.moveTimer = this.moveTimerS;
        this.moveTimerSpeed = this.moveTimerSpeedS;
        this.shotsUntilSplitProjectile = this.shotsUntilSplitProjectileS;
        this.targetScale = this.targetScaleS;
        this.bossState = this.bossStateS;
        this.bossStateTransitionTimer = this.bossStateTransitionTimerS;
        this.health = this.healthS;
        this.showPistols = this.showPistolsS;
        this.shootLeftGun = this.shootLeftGunS;
        this.throb = this.throbS;
        this.bossState2AttackPatternCount = this.bossState2AttackPatternCountS;
        this.faceAnimationSpeed = this.faceAnimationSpeedS;
        this.faceAnimation = this.faceAnimationS;
        this.faceFrameNr = this.faceFrameNrS;
        this.faceFrameNrTimer = this.faceFrameNrTimerS;
        this.faceAnimationHoldLastFrame = this.faceAnimationHoldLastFrameS;
        this.targetPos = this.targetPosS;
        this.targetRot = this.targetRotS;
        this.xSpeed = this.xSpeedS;
        this.ySpeed = this.ySpeedS;
        this.rotSpeed = this.rotSpeedS;
        this.fakeRot = this.fakeRotS;
        this.damageWobble = this.damageWobbleS;
        this.damageWobbleSpeed = this.damageWobbleSpeedS;
        this.damageWobble2 = this.damageWobble2S;
        this.damageWobble2Speed = this.damageWobble2SpeedS;
        this.damageWobble3 = this.damageWobble3S;
        this.damageWobble3Speed = this.damageWobble3SpeedS;
        this.playerLookRot = this.playerLookRotS;
        this.headPopped = this.headPoppedS;
        this.yMoveSineMultiplier = this.yMoveSineMultiplierS;
        this.dynAnimMultiplier = this.dynAnimMultiplierS;
        this.timeSinceTakenDamage = this.timeSinceTakenDamageS;
        float x = this.healthBarLocalScaleXS;
        Vector3 localScale = this.healthBar.localScale;
        float num = localScale.x = x;
        Vector3 vector = this.healthBar.localScale = localScale;
        this.bossHUD.SetActive(this.bossHUDActiveS);
        this.faceMaterial.mainTextureOffset = this.faceMaterialOffsetS;
    }

    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
        networkHelper.maxAllowedPackets = 20;
    }

    // Token: 0x060003A5 RID: 933 RVA: 0x000578FC File Offset: 0x00055AFC
    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.thePlayer = GameObject.Find("Player").transform;
        this.pedroBossIn = this.transform.Find("Pedro_boss");
        this.theAudioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.walrus = GameObject.Find("Walrus");
        this.playerScript = (PlayerScript)this.thePlayer.GetComponent(typeof(PlayerScript));
        this.bossCam = (CutsceneCameraScript)GameObject.Find("CutsceneCamera").GetComponent(typeof(CutsceneCameraScript));
        this.startFollowPedro = GameObject.Find("TalkingPedro").transform;
        this.pistol1 = this.pedroBossIn.Find("Armature/Bone/Bone_001/Bone_009/Bone_010/Bone_011/Bone_012/Bone_013/Bone_014/pistol");
        this.pistol2 = this.pedroBossIn.Find("Armature/Bone/Bone_001/Bone_021/Bone_022/Bone_023/Bone_024/Bone_025/Bone_026/pistol");
        this.pistol1FirePoint = this.pistol1.Find("FirePoint");
        this.pistol2FirePoint = this.pistol2.Find("FirePoint");
        this.switchScript = (SwitchScript)this.GetComponent(typeof(SwitchScript));
        this.fleshParticles = (ParticleSystem)this.transform.Find("FleshParticles").GetComponent(typeof(ParticleSystem));
        this.shootLight = (Light)this.transform.Find("ShootLight").GetComponent(typeof(Light));
        this.shootLight.intensity = (float)0;
        this.wiggleBone = this.pedroBossIn.Find("Armature/Bone/Bone_001");
        this.projectilePoint = this.wiggleBone.Find("ProjectilePoint");
        this.startFogColour = RenderSettings.fogColor;
        UnityScript.Lang.Array array = new UnityScript.Lang.Array();
        int i = 0;
        Component[] componentsInChildren = this.transform.GetComponentsInChildren(typeof(Transform));
        int length = componentsInChildren.Length;
        while (i < length)
        {
            if (((Transform)componentsInChildren[i]).name.Contains("Bone"))
            {
                array.Add((Transform)componentsInChildren[i]);
            }
            i++;
        }
        this.bananaBones = (array.ToBuiltin(typeof(Transform)) as Transform[]);
        this.deathParticles = (ParticleSystem)this.bananaBones[27].Find("DeathParticles").GetComponent(typeof(ParticleSystem));
        this.bossInsideMaterial = ((SkinnedMeshRenderer)this.pedroBossIn.Find("Pedro_Boss_Inside").GetComponent(typeof(SkinnedMeshRenderer))).material;
        this.faceMaterial = ((SkinnedMeshRenderer)this.pedroBossIn.Find("Pedro_boss").GetComponent(typeof(SkinnedMeshRenderer))).materials[2];
        this.faceAnimation = (float)-1;
        this.pistolStartScale = this.pistol1.localScale;
        this.pistol1.localScale = Vector3.one * 0.001f;
        this.pistol2.localScale = Vector3.one * 0.001f;
        this.pistol1StartPos = this.pistol1.localPosition;
        this.pistol1StartRot = this.pistol1.localRotation;
        this.pistol2StartPos = this.pistol2.localPosition;
        this.pistol2StartRot = this.pistol2.localRotation;
        this.theAnimator = (Animator)this.pedroBossIn.GetComponent(typeof(Animator));
        this.startPos = this.transform.position;
        this.startScale = this.transform.localScale;
        this.targetPos = this.startPos;
        this.targetScale = this.startScale;
        this.shotsUntilSplitProjectile = 5;
        this.bossHUD = GameObject.Find("HUD/Canvas").transform.Find("BossHealth").gameObject;
        this.healthBar = (RectTransform)this.bossHUD.transform.Find("Outline/Bar").GetComponent(typeof(RectTransform));
        this.healthBarImg = (Image)this.healthBar.GetComponent(typeof(Image));
        this.healthBarStartColour = this.healthBarImg.color;
        ((Text)this.bossHUD.transform.Find("Outline/Text").GetComponent(typeof(Text))).text = this.root.GetTranslation("Pedro");
        this.attackPattern = 0;
        if (this.bossState == -1)
        {
            this.targetScale = (this.transform.localScale = Vector3.one * 0.001f);
        }
        this.root.nrOfEnemiesTotal = this.root.nrOfEnemiesTotal + 1;
    }

    // Token: 0x060003A6 RID: 934 RVA: 0x00057DF4 File Offset: 0x00055FF4
    public virtual void FixedUpdate()
    {
        if (this.bossState == -1 && this.bossStateTransitionTimer < (float)240)
        {
            this.thePlayer.position = new Vector3((float)-21, (float)48, (float)0);
            this.playerScript.xSpeed = (this.playerScript.ySpeed = (float)0);
        }
    }

    // Token: 0x060003A7 RID: 935 RVA: 0x00057E54 File Offset: 0x00056054
    public virtual void Update()
    {
        attackFrame = false;
        targetPlayer = EMFDNS.GetNearestPlayer(transform.position);

        if (MultiplayerManagerTest.playingAsHost)
            PacketSender.BaseNetworkedEntityRPC("SyncPedroBoss", networkHelper.entityIdentifier, new object[] { health, attackPattern, attackPatternProjectileCount, shootTimer, shootInterval, shotsUntilSplitProjectile });
        else
        {
            health = (float)networkHelper.packageVars[0];
            attackPattern = (int)networkHelper.packageVars[1];
            attackPatternProjectileCount = (int)networkHelper.packageVars[2];
            shootTimer = (float)networkHelper.packageVars[3];
            shootInterval = (float)networkHelper.packageVars[4];
            shotsUntilSplitProjectile = (int)networkHelper.packageVars[5];

            updateHealthBar();
        }



        if (this.bossState == 0 && this.health <= (float)0)
        {
            this.theAudioSource.clip = this.death1Sound;
            this.theAudioSource.pitch = (float)1;
            this.theAudioSource.volume = 0.5f;
            this.theAudioSource.PlayDelayed(0.25f);
            this.theAnimator.CrossFadeInFixedTime("Peel_Open", (float)1, 0);
            this.bossState = 1;
            RenderSettings.fogColor = Color.white;
            this.bossStateTransitionTimer = (float)0;
            this.shootTimer = (float)0;
            this.shootInterval = (float)180;
            this.attackPattern = 0;
            this.health = (float)0;
        }

        if (showPistols && !dead)
        {
            if (this.health <= (float)0)
            {
                this.theAudioSource.clip = this.death2Sound;
                this.theAudioSource.pitch = (float)1;
                this.theAudioSource.volume = 0.5f;
                this.theAudioSource.PlayDelayed(0.25f);
                this.bossStateTransitionTimer = (float)0;
                this.theAnimator.CrossFadeInFixedTime("Dying", 0.6f, 0);
                this.switchScript.output = (float)1;
                this.bossState = 2;
                this.root.kAction = false;
                this.root.actionModeActivated = false;
                this.root.dontAllowReactionPedroTimer = (float)9999;
                this.root.cCheckGiSc = true;
                this.root.giveScore((float)10000, this.root.GetTranslation("bul12"), true);
                this.root.nrOfEnemiesKilled = this.root.nrOfEnemiesKilled + 1;

                dead = true;
            }
        }


        this.shootLight.intensity = Mathf.Clamp(this.shootLight.intensity - 0.5f * this.root.timescale, (float)0, (float)10);
        if (this.bossState == -1)
        {
            this.playerScript.overrideControls = true;
            this.playerScript.kCrouch = true;
            this.playerScript.mousePos = this.startFollowPedro.position;
            this.bossStateTransitionTimer += this.root.timescale;
            if (this.bossStateTransitionTimer < (float)240)
            {
                this.thePlayer.position = new Vector3((float)-21, (float)48, (float)0);
                this.playerScript.xSpeed = (this.playerScript.ySpeed = (float)0);
            }
            this.transform.position = (this.targetPos = this.startFollowPedro.position);
            if (this.startSwitch.output >= (float)1)
            {
                this.transform.localScale = Vector3.one * 0.1f;
                this.targetScale = this.startScale;
                this.shootTimer = (float)0;
                this.bossStateTransitionTimer = (float)0;
                this.playerScript.overrideControls = false;
                this.bossHUD.SetActive(true);
                this.bossState = 0;
            }
        }
        else if (this.bossState == 0)
        {
            if (this.bossStateTransitionTimer > (float)300)
            {
                this.shootTimer += this.root.timescale;
            }
            else
            {
                this.bossStateTransitionTimer += this.root.timescale;
                float num = this.bossStateTransitionTimer / (float)300;
                this.transform.localScale = this.startScale * this.startBossScaleCurve.Evaluate(Mathf.Clamp01(num * 1.5f));
                float x = this.transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f) * ((float)1 - num);
                Vector3 position = this.transform.position;
                float num2 = position.x = x;
                Vector3 vector = this.transform.position = position;
                float y = this.transform.position.y + UnityEngine.Random.Range(-0.1f, 0.1f) * ((float)1 - num);
                Vector3 position2 = this.transform.position;
                float num3 = position2.y = y;
                Vector3 vector2 = this.transform.position = position2;
            }
            if (this.shootTimer > this.shootInterval)
            {
                int num4 = (this.shotsUntilSplitProjectile > 0 || this.attackPattern != 0) ? 0 : 1;
                if (num4 == 0 && !attackFrame)
                {

                    attackFrame = true;
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.projectile, new Vector3(this.projectilePoint.position.x, this.projectilePoint.position.y, this.projectilePoint.position.z), Quaternion.Euler((float)0, (float)0, (float)0));
                    PedroBossBulletScript pedroBossBulletScript = (PedroBossBulletScript)gameObject.GetComponent(typeof(PedroBossBulletScript));
                    pedroBossBulletScript.speed = 0.25f + (float)this.attackPattern * UnityEngine.Random.Range(0.15f, 0.25f) + UnityEngine.Random.Range((float)0, 0.05f);
                    pedroBossBulletScript.attackPattern = this.attackPattern;
                    if (this.attackPattern == 1)
                    {
                        pedroBossBulletScript.attackPatternOffset = ((this.health <= 0.5f) ? (this.health * (float)10 + (float)15 - (float)(this.attackPatternProjectileCount * 4) * Mathf.Clamp01(0.5f + this.health)) : (((float)1 - this.health) * (float)5 + (float)(this.attackPatternProjectileCount * 4)));
                    }
                    this.attackPatternProjectileCount++;
                    if (this.health < (float)1)
                    {
                        this.shotsUntilSplitProjectile--;
                    }
                }
                else if (num4 >= 1)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.splitProjectile, new Vector3(this.projectilePoint.position.x, this.projectilePoint.position.y, this.projectilePoint.position.z), Quaternion.Euler((float)0, (float)0, (float)0));
                    PedroBossSplitBulletScript pedroBossSplitBulletScript = (PedroBossSplitBulletScript)gameObject.GetComponent(typeof(PedroBossSplitBulletScript));
                    pedroBossSplitBulletScript.speed = 0.25f;
                    this.shotsUntilSplitProjectile = (int)((float)1 + Mathf.Round(this.health * (float)3 + (float)UnityEngine.Random.Range(0, 2)));
                }
                if (this.attackPattern == 0)
                {
                    if (this.attackPatternProjectileCount < 10)
                    {
                        this.shootInterval = (float)20 + this.health * (float)65 + Mathf.Clamp01((this.health - 0.75f) * (float)4) * (float)30 + UnityEngine.Random.value * (float)30;
                    }
                    else
                    {
                        this.attackPattern = 1;
                        this.attackPatternProjectileCount = 0;
                        this.shootInterval = (float)120;
                    }
                }
                else if (this.attackPattern == 1)
                {
                    if ((float)this.attackPatternProjectileCount < (float)2 + ((float)1 - this.health) * (float)4)
                    {
                        this.shootInterval = (float)20;
                    }
                    else
                    {
                        this.attackPattern = 0;
                        this.attackPatternProjectileCount = 0;
                        this.shootInterval = (float)180;
                    }
                }
                if (this.shootInterval <= (float)20)
                {
                    this.theAudioSource.clip = this.spitSound;
                    this.theAudioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
                    this.theAudioSource.volume = UnityEngine.Random.Range(0.7f, 0.9f);
                    this.theAudioSource.Play();
                    this.playFaceAnimation((float)1, 0.5f, true, (this.faceAnimation == (float)0) || (this.faceAnimation == (float)1));
                }
                else
                {
                    this.theAudioSource.clip = this.spitSound;
                    this.theAudioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
                    this.theAudioSource.volume = UnityEngine.Random.Range(0.7f, 0.9f);
                    this.theAudioSource.Play();
                    this.playFaceAnimation((float)2, 0.5f, false, this.faceAnimation == (float)0);
                }
                this.shootTimer = (float)0;
            }
            if (this.shootTimer > this.shootInterval - (float)30 && this.faceAnimation != (float)0 && this.faceAnimation != (float)1 && this.faceAnimation != (float)2)
            {
                this.theAudioSource.clip = this.inhaleSound;
                this.theAudioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                this.theAudioSource.volume = UnityEngine.Random.Range(0.5f, 0.7f);
                this.theAudioSource.Play();
                this.playFaceAnimation((float)0, 0.5f, true, false);
            }
            this.moveTimerSpeed = this.root.Damp((this.shootTimer <= this.shootInterval - (float)30) ? (1.5f + ((float)1 - this.health) * (float)2) : ((float)0), this.moveTimerSpeed, 0.1f);
        }
        else if (this.bossState == 1)
        {
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, this.startFogColour * 0.8f, 0.005f * this.root.timescale);
            RenderSettings.fogStartDistance = this.root.Damp((float)36, RenderSettings.fogStartDistance, 0.005f);
            RenderSettings.fogEndDistance = this.root.Damp((float)230, RenderSettings.fogEndDistance, 0.001f);
            this.throb -= this.root.timescale * ((float)1 + ((float)1 - this.health) * 0.75f);
            if (this.throb <= (float)-10 - this.health * (float)80)
            {
                this.throb = ((float)1 - this.health) * (float)60;
            }
            this.bossInsideMaterial.SetFloat("_BumpScale", 0.75f + Mathf.Sin(Time.time * (float)2) * 0.1f + Mathf.Clamp01(this.throb * 0.01f));
            if (this.showPistols)
            {
                this.pistol1.localScale = (this.pistol2.localScale = this.pistol2.localScale + (this.pistolStartScale - this.pistol1.localScale) * Mathf.Clamp01(0.1f * this.root.timescale));
                this.dynAnimMultiplier = Mathf.Clamp01(this.dynAnimMultiplier + 0.01f * this.root.timescale);
            }
            if (this.bossStateTransitionTimer < (float)520)
            {
                this.moveTimerSpeed = (float)0;
                if (this.bossStateTransitionTimer > (float)240)
                {
                    this.health = Mathf.Clamp01((this.bossStateTransitionTimer - (float)240) / (float)280);
                    this.targetScale = this.startScale * 1.25f * (0.5f + this.healthSize.Evaluate(this.health) * 0.5f);
                    float x2 = this.startPos.x + UnityEngine.Random.Range(-0.6f, 0.6f) * (1.1f - this.health);
                    Vector3 position3 = this.transform.position;
                    float num5 = position3.x = x2;
                    Vector3 vector3 = this.transform.position = position3;
                }
                if (this.bossStateTransitionTimer > (float)60 && this.faceAnimation != (float)4)
                {
                    this.playFaceAnimation((float)4, 0.125f, true, true);
                }
                this.pedroBossIn.rotation = this.root.DampSlerp(Quaternion.Euler((float)180, (float)90, (float)90), this.pedroBossIn.rotation, 0.01f);
                this.updateHealthBar();
                this.bossStateTransitionTimer += this.root.timescale;
            }
            else
            {
                this.finishedFirstStateSwitchOutput.output = (float)1;
                if (this.attackPattern == 0)
                {
                    this.shootTimer += this.root.timescale;
                    if (this.shootTimer > this.shootInterval)
                    {
                        this.theAudioSource.clip = this.weaponSound;
                        this.theAudioSource.pitch = UnityEngine.Random.Range(0.6f, 0.8f);
                        this.theAudioSource.volume = UnityEngine.Random.Range(0.7f, 0.9f);
                        this.theAudioSource.Play();
                        Vector3 vector4 = (!this.shootLeftGun) ? this.pistol1FirePoint.position : this.pistol2FirePoint.position;
                        GameObject muzzleFlash = this.root.getMuzzleFlash(0, vector4, Quaternion.Euler((float)0, (float)0, (float)0));
                        muzzleFlash.transform.localScale = muzzleFlash.transform.localScale * (float)3;
                        vector4.z *= 0.3f;
                        GameObject bullet = this.root.getBullet(vector4, Quaternion.Euler((float)0, (float)90, (float)0));
                        BulletScript bulletScript = this.root.getBulletScript();
                        float y2 = 3.5f;
                        Vector3 localScale = bullet.transform.localScale;
                        float num6 = localScale.y = y2;
                        Vector3 vector5 = bullet.transform.localScale = localScale;
                        bulletScript.friendly = false;
                        bulletScript.bulletStrength = 0.35f;
                        bulletScript.doPostSetup();
                        if (this.shootLeftGun)
                        {
                            float x3 = this.pistol2.localPosition.x + UnityEngine.Random.Range(0.1f, 0.4f);
                            Vector3 localPosition = this.pistol2.localPosition;
                            float num7 = localPosition.x = x3;
                            Vector3 vector6 = this.pistol2.localPosition = localPosition;
                            this.pistol2.localRotation = this.pistol2.localRotation * Quaternion.Euler(-UnityEngine.Random.Range(0.5f, 2.5f), (float)0, (float)0);
                        }
                        else
                        {
                            float x4 = this.pistol1.localPosition.x + UnityEngine.Random.Range(0.1f, 0.4f);
                            Vector3 localPosition2 = this.pistol1.localPosition;
                            float num8 = localPosition2.x = x4;
                            Vector3 vector7 = this.pistol1.localPosition = localPosition2;
                            this.pistol1.localRotation = this.pistol1.localRotation * Quaternion.Euler(-UnityEngine.Random.Range(0.5f, 2.5f), (float)0, (float)0);
                        }
                        this.shootLeftGun = !this.shootLeftGun;
                        if ((float)this.attackPatternProjectileCount > (float)8 + ((float)1 - this.health) * (float)8)
                        {
                            this.attackPatternProjectileCount = 0;
                            this.bossState2AttackPatternCount++;
                            this.shootInterval = (float)80 + this.health * (float)40;
                        }
                        else
                        {
                            this.attackPatternProjectileCount++;
                            this.shootInterval = UnityEngine.Random.Range(4f, 6f);
                        }
                        if (this.bossState2AttackPatternCount > 3)
                        {
                            this.attackPattern = 1;
                            this.theAnimator.CrossFadeInFixedTime("Open_spit_prepare", 0.3f, 0);
                            this.theAnimator.SetBool("Keep spitting", true);
                            this.shootInterval = (float)30;
                            this.bossState2AttackPatternCount = 0;
                        }
                        this.shootLight.transform.position = vector4;
                        this.shootLight.intensity = UnityEngine.Random.Range(5f, 7f);
                        this.rotSpeed = 0.15f;
                        this.xSpeed -= 0.025f;
                        this.shootTimer = (float)0;
                    }
                    this.moveTimerSpeed = this.root.Damp(1.5f + ((float)1 - this.health) * (float)2, this.moveTimerSpeed, 0.01f);
                }
                else if (this.attackPattern == 1)
                {
                    this.shootTimer += this.root.timescale;
                    if (this.shootTimer > this.shootInterval)
                    {
                        int num4 = 0;
                        if (num4 == 0)
                        {
                            this.theAudioSource.clip = this.spitSound;
                            this.theAudioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
                            this.theAudioSource.volume = UnityEngine.Random.Range(0.7f, 0.9f);
                            this.theAudioSource.Play();
                            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.splitProjectile, this.bananaBones[30].position + Vector3.forward * -0.5f, Quaternion.Euler((float)0, (float)0, (float)0));
                            PedroBossSplitBulletScript pedroBossSplitBulletScript2 = (PedroBossSplitBulletScript)gameObject2.GetComponent(typeof(PedroBossSplitBulletScript));
                            pedroBossSplitBulletScript2.speed = 0.2f;
                            this.attackPatternProjectileCount++;
                            this.theAnimator.CrossFadeInFixedTime("Open_spit", 0.05f, 0);
                            if (this.attackPatternProjectileCount > 6)
                            {
                                this.theAnimator.SetBool("Keep spitting", false);
                                this.attackPattern = 0;
                                this.shootInterval = (float)90;
                                this.attackPatternProjectileCount = 0;
                            }
                            else
                            {
                                this.shootInterval = (float)UnityEngine.Random.Range(50, 70);
                            }
                        }
                        this.shootTimer = (float)0;
                    }
                    this.moveTimerSpeed = this.root.Damp((this.shootTimer <= this.shootInterval - (float)30) ? (1.5f + ((float)1 - this.health) * (float)2) : ((float)0), this.moveTimerSpeed, 0.1f);
                }
                this.pistol1.localPosition = this.root.DampV3(this.pistol1StartPos, this.pistol1.localPosition, 0.1f);
                this.pistol1.localRotation = this.root.DampSlerp(this.pistol1StartRot, this.pistol1.localRotation, 0.1f);
                this.pistol2.localPosition = this.root.DampV3(this.pistol2StartPos, this.pistol2.localPosition, 0.1f);
                this.pistol2.localRotation = this.root.DampSlerp(this.pistol2StartRot, this.pistol2.localRotation, 0.1f);
            }
        }
        else if (this.bossState == 2)
        {
            if (!this.headPopped)
            {
                this.bossCam.blendToSecondCamera = true;
                this.moveTimerSpeed = this.root.Damp((float)2, this.moveTimerSpeed, 0.1f);
                this.yMoveSineMultiplier = this.root.Damp(0.1f, this.yMoveSineMultiplier, 0.01f);
                this.bossStateTransitionTimer = Mathf.Clamp01(this.bossStateTransitionTimer + 0.01f * this.root.timescale);
                float x5 = this.targetPos.x + UnityEngine.Random.Range(-this.bossStateTransitionTimer * 0.4f, this.bossStateTransitionTimer * 0.4f);
                Vector3 position4 = this.transform.position;
                float num9 = position4.x = x5;
                Vector3 vector8 = this.transform.position = position4;
                RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, this.startFogColour * 0.5f, 0.02f * this.root.timescale);
                RenderSettings.fogStartDistance = this.root.Damp((float)-30, RenderSettings.fogStartDistance, 0.01f);
                RenderSettings.fogEndDistance = this.root.Damp(125.23f, RenderSettings.fogEndDistance, 0.01f);
            }
            else
            {
                RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, this.startFogColour, 0.1f * this.root.timescale);
                RenderSettings.fogStartDistance = this.root.Damp(26.1f, RenderSettings.fogStartDistance, 0.01f);
                RenderSettings.fogEndDistance = this.root.Damp(487.5f, RenderSettings.fogEndDistance, 0.01f);
                this.moveTimerSpeed = this.root.Damp(0.05f, this.moveTimerSpeed, 0.1f);
                this.yMoveSineMultiplier = this.root.Damp(0.05f, this.yMoveSineMultiplier, 0.01f);
                this.bossStateTransitionTimer += 0.01f * this.root.timescale;
                this.startPos.y = this.startPos.y - Mathf.Clamp01(-1.8f + this.bossStateTransitionTimer) * 0.05f * this.root.timescale;
                if (this.bossStateTransitionTimer > (float)3)
                {
                    this.startPos.y = this.startPos.y - ((float)-3 + this.bossStateTransitionTimer) * this.root.timescale;
                }
                if (this.bossStateTransitionTimer > 4.3f)
                {
                    this.playerScript.kJump = true;
                    this.playerScript.kJumpHeldDown = true;
                    this.playerScript.kDodge = true;
                    this.playerScript.nrOfDodgeSpins = 0;
                    if (this.bossStateTransitionTimer > 4.8f && this.walrus.activeInHierarchy)
                    {
                        this.root.explode(this.walrus.transform.position, (float)5, 3, -Vector3.right, "Blue", true, true);
                        this.walrus.SetActive(false);
                    }
                }
            }
        }
        this.moveTimer += this.moveTimerSpeed * this.root.timescale;
        this.targetPos.y = this.startPos.y + Mathf.Sin(this.moveTimer / (float)60) * (float)5 * this.yMoveSineMultiplier;
        this.targetPos.x = this.startPos.x + Mathf.Cos(this.moveTimer / (float)90) * this.yMoveSineMultiplier;
        this.xSpeed += this.root.DampAdd(this.targetPos.x, this.transform.position.x, 0.02f);
        this.xSpeed *= Mathf.Pow(0.9f, this.root.timescale);
        float x6 = this.transform.position.x + this.xSpeed * this.root.timescale;
        Vector3 position5 = this.transform.position;
        float num10 = position5.x = x6;
        Vector3 vector9 = this.transform.position = position5;
        this.ySpeed += this.root.DampAdd(this.targetPos.y, this.transform.position.y, 0.02f);
        this.ySpeed *= Mathf.Pow(0.9f, this.root.timescale);
        float y3 = this.transform.position.y + this.ySpeed * this.root.timescale;
        Vector3 position6 = this.transform.position;
        float num11 = position6.y = y3;
        Vector3 vector10 = this.transform.position = position6;
        this.targetRot = Mathf.Cos(this.moveTimer / (float)120 - (float)1) * (float)7 - (float)5;
        this.rotSpeed += this.root.DampAdd(this.targetRot, this.fakeRot, 0.01f);
        this.rotSpeed *= Mathf.Pow(0.9f, this.root.timescale);
        this.fakeRot += this.rotSpeed * this.root.timescale;
        this.damageWobbleSpeed += this.root.DampAdd((float)0, this.damageWobble, 0.01f);
        this.damageWobbleSpeed *= Mathf.Pow(0.97f, this.root.timescale);
        this.damageWobble += this.damageWobbleSpeed * this.root.timescale;
        this.damageWobble = Mathf.Clamp(this.damageWobble, (float)-7, (float)8);
        this.damageWobble2Speed += this.root.DampAdd(this.damageWobble, this.damageWobble2, 0.02f);
        this.damageWobble2Speed *= Mathf.Pow(0.92f, this.root.timescale);
        this.damageWobble2 += this.damageWobble2Speed * this.root.timescale;
        this.damageWobble3Speed += this.root.DampAdd(this.damageWobble2, this.damageWobble3, 0.025f);
        this.damageWobble3Speed *= Mathf.Pow(0.9f, this.root.timescale);
        this.damageWobble3 += this.damageWobble3Speed * this.root.timescale;
        this.transform.rotation = Quaternion.Euler((float)0, (float)0, this.fakeRot);
        this.transform.localScale = this.root.DampV3(this.targetScale, this.transform.localScale, ((this.bossState != 1) ? ((float)1) : Mathf.Clamp01(((float)-240 + this.bossStateTransitionTimer) / (float)280)) * 0.1f);
        this.faceFrameNrTimer += this.faceAnimationSpeed * this.root.timescale;
        if (this.faceFrameNrTimer > (float)1)
        {
            if (this.faceFrameNr >= (float)9)
            {
                if (!this.faceAnimationHoldLastFrame)
                {
                    this.faceAnimation = (float)-1;
                    this.faceFrameNr = (float)0;
                }
                this.faceAnimationSpeed = (float)0;
            }
            else
            {
                this.faceFrameNr = Mathf.Clamp(this.faceFrameNr + (float)1, (float)0, (float)9);
            }
            float x7 = this.faceFrameNr / 10.039216f;
            Vector2 mainTextureOffset = this.faceMaterial.mainTextureOffset;
            float num12 = mainTextureOffset.x = x7;
            Vector2 vector11 = this.faceMaterial.mainTextureOffset = mainTextureOffset;
            float y4 = -Mathf.Clamp01(this.faceAnimation / 10.039216f);
            Vector2 mainTextureOffset2 = this.faceMaterial.mainTextureOffset;
            float num13 = mainTextureOffset2.y = y4;
            Vector2 vector12 = this.faceMaterial.mainTextureOffset = mainTextureOffset2;
            this.faceFrameNrTimer -= (float)1;
        }
        this.timeSinceTakenDamage = Mathf.Clamp(this.timeSinceTakenDamage + this.root.timescale, (float)0, (float)600);
        this.healthBarImg.color = Color.Lerp(this.healthBarImg.color, this.healthBarStartColour, 0.3f * this.root.timescale);
    }

    // Token: 0x060003A8 RID: 936 RVA: 0x000597D8 File Offset: 0x000579D8
    public virtual void LateUpdate()
    {
        if (this.bossState == 0)
        {
            this.wiggleBone.rotation = this.wiggleBone.rotation * Quaternion.Euler((float)0, this.rotSpeed * (float)10 + this.fakeRot, this.damageWobble);
        }
        else
        {
            this.wiggleBone.rotation = this.wiggleBone.rotation * Quaternion.Euler((this.rotSpeed * (float)10 + this.fakeRot) * this.dynAnimMultiplier, (float)0, (float)0);
            if (this.showPistols)
            {
                this.playerLookRot = this.root.Damp(Mathf.Clamp(this.targetPlayer.position.y - this.transform.position.y, (float)-4, (float)4), this.playerLookRot, 0.1f);
            }
            this.bananaBones[10].localRotation = this.bananaBones[10].localRotation * Quaternion.Euler((-this.playerLookRot * (float)2 - (float)15) * this.dynAnimMultiplier, (float)0, (float)0);
            this.bananaBones[22].localRotation = this.bananaBones[22].localRotation * Quaternion.Euler((this.playerLookRot * (float)2 + (float)15) * this.dynAnimMultiplier, (float)0, (float)0);
            this.bananaBones[27].localRotation = this.bananaBones[27].localRotation * Quaternion.Euler((float)0, this.playerLookRot * this.dynAnimMultiplier, (float)0);
            this.bananaBones[34].localRotation = this.bananaBones[34].localRotation * Quaternion.Euler((float)0, (this.playerLookRot + this.rotSpeed + this.damageWobble) * this.dynAnimMultiplier, this.damageWobble * this.dynAnimMultiplier);
            this.bananaBones[35].localRotation = this.bananaBones[35].localRotation * Quaternion.Euler((float)0, (this.playerLookRot + this.rotSpeed * (float)2 + this.damageWobble2 * 1.5f) * this.dynAnimMultiplier, this.damageWobble2 * this.dynAnimMultiplier);
            this.bananaBones[36].localRotation = this.bananaBones[36].localRotation * Quaternion.Euler((float)0, (this.playerLookRot + this.rotSpeed * (float)3 + this.damageWobble3 * 2.5f) * this.dynAnimMultiplier, this.damageWobble3 * this.dynAnimMultiplier);
            this.bananaBones[37].localRotation = this.bananaBones[37].localRotation * Quaternion.Euler((float)0, (float)0, this.playerLookRot * (float)3 * this.dynAnimMultiplier);
            this.bananaBones[38].localRotation = this.bananaBones[38].localRotation * Quaternion.Euler((float)0, (float)0, -this.playerLookRot * (float)3 * this.dynAnimMultiplier);
            this.bananaBones[3].localRotation = this.bananaBones[3].localRotation * Quaternion.Euler((float)0, (float)0, (this.playerLookRot + this.rotSpeed + this.damageWobble) * this.dynAnimMultiplier);
            this.bananaBones[4].localRotation = this.bananaBones[4].localRotation * Quaternion.Euler((float)0, (float)0, this.damageWobble * this.dynAnimMultiplier);
            this.bananaBones[5].localRotation = this.bananaBones[5].localRotation * Quaternion.Euler((float)0, (float)0, this.damageWobble * this.dynAnimMultiplier);
            this.bananaBones[6].localRotation = this.bananaBones[6].localRotation * Quaternion.Euler((float)0, (float)0, this.damageWobble * this.dynAnimMultiplier);
            this.bananaBones[7].localRotation = this.bananaBones[7].localRotation * Quaternion.Euler((float)0, (float)0, this.damageWobble * this.dynAnimMultiplier);
            this.bananaBones[8].localRotation = this.bananaBones[8].localRotation * Quaternion.Euler((float)0, (float)0, this.damageWobble * this.dynAnimMultiplier);
            int i = 0;
            Transform[] array = this.bananaBones;
            int length = array.Length;
            while (i < length)
            {
                array[i].localRotation = array[i].localRotation * Quaternion.Euler(this.damageWobble * 0.25f * this.dynAnimMultiplier, (this.ySpeed * (float)10 + this.xSpeed * (float)5 + this.rotSpeed * (float)2 + this.damageWobble * 0.25f) * this.dynAnimMultiplier, this.damageWobble * 0.25f * this.dynAnimMultiplier);
                i++;
            }
        }
        if (this.root.doCheckpointSave)
        {
            this.saveState();
        }
        if (this.root.doCheckpointLoad)
        {
            this.loadState();
        }
    }

    // Token: 0x060003A9 RID: 937 RVA: 0x00059CFC File Offset: 0x00057EFC
    public virtual void OnCollisionEnter(Collision col)
    {
        if (this.bossStateTransitionTimer > (float)300 && this.bossState == 1 && (col.collider.gameObject.layer == 9 || col.collider.gameObject.layer == 21) && col.contacts[0].point.y > this.transform.position.y)
        {
            BulletScript bulletScript = (BulletScript)col.collider.GetComponent(typeof(BulletScript));
            if (!bulletScript.bulletHasHit)
            {
                bulletScript.bulletHasHit = true;
                bulletScript.emitBlood(false, (float)0);
                float bulletStrength = bulletScript.bulletStrength;
                this.health -= Mathf.Clamp(bulletStrength * 1.1f, 0.3f, (float)1) * (0.0115f + this.timeSinceTakenDamage / (float)600 * 0.05f);
                this.timeSinceTakenDamage = (float)0;
                this.root.slowMotionAmount = Mathf.Clamp01(this.root.slowMotionAmount + 0.05f);
                float x = this.transform.position.x - bulletStrength * 0.6f;
                Vector3 position = this.transform.position;
                float num = position.x = x;
                Vector3 vector = this.transform.position = position;
                if (this.showPistols)
                {
                    this.fleshParticles.transform.position = col.contacts[0].point;
                    this.fleshParticles.Emit(UnityEngine.Random.Range(1, 2));
                    this.targetScale = this.startScale * 1.25f * (0.5f + this.healthSize.Evaluate(this.health) * 0.5f);
                    this.damageWobbleSpeed += bulletStrength * 0.6f;
                }
                Vector3 forward = col.collider.transform.forward;
                this.xSpeed += forward.x * 0.1f;
                this.ySpeed += forward.y * 0.04f;
                this.rotSpeed += Mathf.Clamp(this.transform.position.y - col.collider.transform.position.y, (float)-5, (float)5) * -0.1f;
                this.updateHealthBar();
            }
        }
    }

    // Token: 0x060003AA RID: 938 RVA: 0x0005A084 File Offset: 0x00058284
    public virtual void revealPistols()
    {
        this.theAudioSource.clip = this.gagGunSound;
        this.theAudioSource.pitch = (float)1;
        this.theAudioSource.volume = 0.8f;
        this.theAudioSource.Play();
        this.showPistols = true;
    }

    // Token: 0x060003AB RID: 939 RVA: 0x0005A0D4 File Offset: 0x000582D4
    public virtual void headPop()
    {
        this.theAudioSource.clip = this.deathPopSound;
        this.theAudioSource.pitch = (float)1;
        this.theAudioSource.volume = (float)1;
        this.theAudioSource.Play();
        this.deathParticles.Emit(500);
        this.headPopped = true;
        this.damageWobbleSpeed = (float)2;
        this.bossStateTransitionTimer = (float)0;
        this.bossCam.blendToSecondCamera = false;
    }

    // Token: 0x060003AC RID: 940 RVA: 0x00003961 File Offset: 0x00001B61
    public virtual void playFaceAnimation(float animNr, float playSpeed, bool holdLastFrame, bool doInterupt)
    {
        if (doInterupt || (!doInterupt && this.faceAnimationSpeed == (float)0))
        {
            this.faceAnimation = animNr;
            this.faceFrameNr = (float)0;
            this.faceAnimationSpeed = playSpeed;
            this.faceAnimationHoldLastFrame = holdLastFrame;
        }
    }

    // Token: 0x060003AD RID: 941 RVA: 0x0005A14C File Offset: 0x0005834C
    public virtual void takeDamage(Vector3 dir, Vector3 pos)
    {
        this.xSpeed += dir.x * 0.5f;
        this.ySpeed += dir.y * 0.3f;
        this.rotSpeed += Mathf.Clamp(this.transform.position.y - pos.y, (float)-5, (float)5) * -0.5f;
        this.damageWobbleSpeed += 1.5f;
        this.health -= (0.0125f + 0.0125f * Mathf.Clamp01(this.health - 0.25f) * 1.3f) * (1.15f + this.timeSinceTakenDamage / (float)600 * (float)4);
        this.timeSinceTakenDamage = (float)0;
        this.root.slowMotionAmount = Mathf.Clamp01(this.root.slowMotionAmount + 0.35f);
        this.playFaceAnimation((float)3, 0.5f, false, true);

        this.targetScale = this.startScale * this.healthSize.Evaluate(this.health);
        this.updateHealthBar();
    }

    // Token: 0x060003AE RID: 942 RVA: 0x0005A330 File Offset: 0x00058530
    public virtual void updateHealthBar()
    {
        float x = Mathf.Clamp01(this.health);
        Vector3 localScale = this.healthBar.localScale;
        float num = localScale.x = x;
        Vector3 vector = this.healthBar.localScale = localScale;
        this.healthBarImg.color = Color.white;
    }
}
