using System;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x02000057 RID: 87
[Serializable]
public class GunTurretScript : MonoBehaviour
{

    private bool overrideSwitchInput = false;
    private BaseNetworkEntity networkHelper;


    // Token: 0x04000671 RID: 1649
    private float health;

    // Token: 0x04000672 RID: 1650
    public GameObject bullet;

    // Token: 0x04000673 RID: 1651
    public SwitchScript[] inputSwitch;

    // Token: 0x04000674 RID: 1652
    private float switchInput;

    // Token: 0x04000675 RID: 1653
    public bool noShields;

    // Token: 0x04000676 RID: 1654
    public bool raycastCheckForPlayer;

    // Token: 0x04000677 RID: 1655
    public bool tutorialTurret;

    // Token: 0x04000678 RID: 1656
    private bool tutorialTurretInPosition;

    // Token: 0x04000679 RID: 1657
    public bool fireEvenIfPlayerIsNotWithinAimAngles;

    // Token: 0x0400067A RID: 1658
    private SwitchScript localSwitchScript;

    // Token: 0x0400067B RID: 1659
    private RootScript root;

    // Token: 0x0400067C RID: 1660
    private Transform mainPlayer;

    // Token: 0x0400067D RID: 1661
    private float animTimer;

    // Token: 0x0400067E RID: 1662
    private AutoAimTargetScript autoAimScript;

    // Token: 0x0400067F RID: 1663
    private Transform door;

    // Token: 0x04000680 RID: 1664
    private Transform gunHolder;

    // Token: 0x04000681 RID: 1665
    private Transform turret;

    // Token: 0x04000682 RID: 1666
    private Transform shield1;

    // Token: 0x04000683 RID: 1667
    private Transform shield2;

    // Token: 0x04000684 RID: 1668
    private Transform pipe;

    // Token: 0x04000685 RID: 1669
    private GameObject electric;

    // Token: 0x04000686 RID: 1670
    private BoxCollider shield1Collider;

    // Token: 0x04000687 RID: 1671
    private BoxCollider shield2Collider;

    // Token: 0x04000688 RID: 1672
    private Transform aimFromPoint;

    // Token: 0x04000689 RID: 1673
    private ParticleSystem smokeParticle;

    // Token: 0x0400068A RID: 1674
    private ParticleSystem shellParticle;

    // Token: 0x0400068B RID: 1675
    private float gunHolderStartZPos;

    // Token: 0x0400068C RID: 1676
    private bool fireWeapon;

    // Token: 0x0400068D RID: 1677
    private float fireDelay;

    // Token: 0x0400068E RID: 1678
    private bool readyFire;

    // Token: 0x0400068F RID: 1679
    [HideInInspector]
    public float fireTimer;

    // Token: 0x04000690 RID: 1680
    private float aimSpeed;

    // Token: 0x04000691 RID: 1681
    private float raycastCheckTimer;

    // Token: 0x04000692 RID: 1682
    private bool canSeePlayer;

    // Token: 0x04000693 RID: 1683
    private LayerMask layerMask;

    // Token: 0x04000694 RID: 1684
    private float playerAngle;

    // Token: 0x04000695 RID: 1685
    private Renderer gunHolderRend;

    // Token: 0x04000696 RID: 1686
    private Vector2 hitColorOffset;

    // Token: 0x04000697 RID: 1687
    private Vector2 hitColorOffsetTarget;

    // Token: 0x04000698 RID: 1688
    private float hitTimer;

    // Token: 0x04000699 RID: 1689
    private float electricAnimTimer;

    // Token: 0x0400069A RID: 1690
    private Vector3 electricStartPos;

    // Token: 0x0400069B RID: 1691
    private Transform healthBar;

    // Token: 0x0400069C RID: 1692
    private AudioSource shootAudioSource;

    // Token: 0x0400069D RID: 1693
    private AudioSource windUpAudioSource;

    // Token: 0x0400069E RID: 1694
    private AudioSource appearAudioSource;

    // Token: 0x0400069F RID: 1695
    private bool doorMovedDoOnce;

    // Token: 0x040006A0 RID: 1696
    private float healthS;

    // Token: 0x040006A1 RID: 1697
    private float switchInputS;

    // Token: 0x040006A2 RID: 1698
    private float animTimerS;

    // Token: 0x040006A3 RID: 1699
    private bool fireWeaponS;

    // Token: 0x040006A4 RID: 1700
    private float fireDelayS;

    // Token: 0x040006A5 RID: 1701
    private bool readyFireS;

    // Token: 0x040006A6 RID: 1702
    private float fireTimerS;

    // Token: 0x040006A7 RID: 1703
    private float aimSpeedS;

    // Token: 0x040006A8 RID: 1704
    private float raycastCheckTimerS;

    // Token: 0x040006A9 RID: 1705
    private bool canSeePlayerS;

    // Token: 0x040006AA RID: 1706
    private float playerAngleS;

    // Token: 0x040006AB RID: 1707
    private Vector2 hitColorOffsetS;

    // Token: 0x040006AC RID: 1708
    private Vector2 hitColorOffsetTargetS;

    // Token: 0x040006AD RID: 1709
    private float hitTimerS;

    // Token: 0x040006AE RID: 1710
    private float electricAnimTimerS;

    // Token: 0x040006AF RID: 1711
    private bool doorMovedDoOnceS;

    // Token: 0x040006B0 RID: 1712
    private bool tutorialTurretS;

    // Token: 0x0600020E RID: 526 RVA: 0x00002F9C File Offset: 0x0000119C
    public GunTurretScript()
    {
        this.health = 4.5f;
        this.raycastCheckForPlayer = true;
    }

    // Token: 0x0600020F RID: 527 RVA: 0x00031070 File Offset: 0x0002F270
    public virtual void saveState()
    {
        this.healthS = this.health;
        this.switchInputS = this.switchInput;
        this.animTimerS = this.animTimer;
        this.fireWeaponS = this.fireWeapon;
        this.fireDelayS = this.fireDelay;
        this.readyFireS = this.readyFire;
        this.fireTimerS = this.fireTimer;
        this.aimSpeedS = this.aimSpeed;
        this.raycastCheckTimerS = this.raycastCheckTimer;
        this.canSeePlayerS = this.canSeePlayer;
        this.playerAngleS = this.playerAngle;
        this.hitColorOffsetS = this.hitColorOffset;
        this.hitColorOffsetTargetS = this.hitColorOffsetTarget;
        this.hitTimerS = this.hitTimer;
        this.electricAnimTimerS = this.electricAnimTimer;
        this.doorMovedDoOnceS = this.doorMovedDoOnce;
        this.tutorialTurretS = this.tutorialTurret;
    }

    // Token: 0x06000210 RID: 528 RVA: 0x0003114C File Offset: 0x0002F34C
    public virtual void loadState()
    {
        this.health = this.healthS;
        this.switchInput = this.switchInputS;
        this.animTimer = this.animTimerS;
        this.fireWeapon = this.fireWeaponS;
        this.fireDelay = this.fireDelayS;
        this.readyFire = this.readyFireS;
        this.fireTimer = this.fireTimerS;
        this.aimSpeed = this.aimSpeedS;
        this.raycastCheckTimer = this.raycastCheckTimerS;
        this.canSeePlayer = this.canSeePlayerS;
        this.playerAngle = this.playerAngleS;
        this.hitColorOffset = this.hitColorOffsetS;
        this.hitColorOffsetTarget = this.hitColorOffsetTargetS;
        this.hitTimer = this.hitTimerS;
        this.electricAnimTimer = this.electricAnimTimerS;
        this.doorMovedDoOnce = this.doorMovedDoOnceS;
        this.tutorialTurret = this.tutorialTurretS;
    }

    // Token: 0x06000211 RID: 529 RVA: 0x00002FB6 File Offset: 0x000011B6
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

    // Token: 0x06000212 RID: 530 RVA: 0x00031228 File Offset: 0x0002F428
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
        this.windUpAudioSource.volume = (float)0;
        this.windUpAudioSource.Stop();
        this.gunHolderStartZPos = this.gunHolder.localPosition.z;
        this.layerMask = 65792;
        this.gunHolderRend = (Renderer)this.gunHolder.GetComponent(typeof(Renderer));
        this.electric.SetActive(false);
        this.electricStartPos = this.electric.transform.localPosition;
        this.localSwitchScript = (SwitchScript)this.transform.GetComponent(typeof(SwitchScript));
        this.localSwitchScript.output = (float)-1;
        this.healthBar = this.transform.Find("GunTurretHealthBar");
        if (this.noShields)
        {
            this.shield1.gameObject.SetActive(false);
            this.shield2.gameObject.SetActive(false);
        }
        if (this.fireEvenIfPlayerIsNotWithinAimAngles)
        {
            this.fireTimer = (float)-150;
        }
        if (this.tutorialTurret)
        {
            this.health = (float)2;
            float y = this.health / 4.5f;
            Vector3 localScale = this.healthBar.localScale;
            float num = localScale.y = y;
            Vector3 vector = this.healthBar.localScale = localScale;
        }
    }

    // Token: 0x06000213 RID: 531 RVA: 0x00031588 File Offset: 0x0002F788
    public virtual void Update()
    {

        this.switchInput = (float)-1;
        if (this.health > (float)0)
        {
            if (Extensions.get_length(this.inputSwitch) == 0)
            {
                this.switchInput = (float)1;
            }
            else
            {
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
            }
        }


        if (this.raycastCheckForPlayer)
        {
            this.raycastCheckTimer += this.root.timescale;
            if (this.raycastCheckTimer > (float)3)
            {
                RaycastHit raycastHit = default(RaycastHit);
                if (Physics.Linecast(this.mainPlayer.position, this.pipe.position, out raycastHit, this.layerMask))
                {
                    if (raycastHit.transform.tag == "GunTurret")
                    {

                        if (!overrideSwitchInput)
                        {
                            networkHelper.ignoreMaxPacketsDoOnce = true;
                            PacketSender.BaseNetworkedEntityRPC("OnTurretDetectPlayer", networkHelper.entityIdentifier);

                            this.canSeePlayer = true;
                        }
                    }
                    else
                    {
                        this.canSeePlayer = false;
                    }
                }
                else
                {
                    if (!overrideSwitchInput)
                    {
                        networkHelper.ignoreMaxPacketsDoOnce = true;
                        PacketSender.BaseNetworkedEntityRPC("OnTurretDetectPlayer", networkHelper.entityIdentifier);

                        this.canSeePlayer = true;
                    }
                }
                this.raycastCheckTimer = (float)0;
            }
        }
        else
        {
            this.canSeePlayer = true;
        }
        this.animTimer = Mathf.Clamp(this.animTimer + this.switchInput * this.root.timescale, (float)0, (float)80);
        this.hitTimer = Mathf.Clamp(this.hitTimer - this.root.timescale, (float)0, this.hitTimer);
        if (this.hitTimer <= (float)0)
        {
            this.electric.SetActive(false);
        }
        else
        {
            this.electricAnimTimer += this.root.timescale;
            if (this.electricAnimTimer > (float)2)
            {
                this.electricAnimTimer = (float)0;
                int num = UnityEngine.Random.Range(0, 360);
                Quaternion localRotation = this.electric.transform.localRotation;
                Vector3 eulerAngles = localRotation.eulerAngles;
                float num2 = eulerAngles.z = (float)num;
                Vector3 vector = localRotation.eulerAngles = eulerAngles;
                Quaternion quaternion = this.electric.transform.localRotation = localRotation;
                this.electric.transform.localScale = new Vector3(UnityEngine.Random.Range(0.05f, 0.2f), UnityEngine.Random.Range(0.1f, 0.45f), (float)1);
                this.electric.transform.localPosition = this.electricStartPos + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f), (float)0);
            }
        }
        if (this.switchInput >= (float)1)
        {
            this.localSwitchScript.output = (float)1;
            if (this.autoAimScript != null)
            {
                this.autoAimScript.enabled = true;
            }
            this.tutorialTurretInPosition = (Quaternion.Angle(this.turret.localRotation, Quaternion.Euler((float)0, this.playerAngle, (float)0)) < (float)5);
            if (this.tutorialTurret)
            {
                this.root.neverDoDodgeHelper = false;
                this.root.alwaysDoDodgeHelper = true;
                this.fireWeapon = true;
                this.fireTimer = (float)-100;
                this.animTimer = (float)100;
            }
            if (this.animTimer > (float)40)
            {
                if (this.canSeePlayer)
                {
                    this.playerAngle = (float)180 + Mathf.Atan2(this.aimFromPoint.InverseTransformPoint(this.mainPlayer.position).x, this.aimFromPoint.InverseTransformPoint(this.mainPlayer.position).z) * 57.29578f;
                    if (this.fireEvenIfPlayerIsNotWithinAimAngles || Quaternion.Angle(this.turret.localRotation, Quaternion.Euler((float)0, this.playerAngle, (float)0)) < (float)5)
                    {
                        this.readyFire = true;
                        if (Vector2.Distance(this.transform.position, this.mainPlayer.position) < (float)40)
                        {
                            this.root.autoSaveTimer = (float)0;
                        }
                    }
                }
                else
                {
                    this.playerAngle = Mathf.Clamp(Mathf.Sin(Time.time * 0.5f) * (float)89, (float)-60, (float)60);
                }
                if (this.playerAngle < (float)180 && this.playerAngle > (float)89)
                {
                    this.playerAngle = (float)89;
                }
                else if (this.playerAngle > (float)180 && this.playerAngle < (float)271)
                {
                    this.playerAngle = (float)271;
                }
                if (this.tutorialTurret)
                {
                    this.playerAngle = (float)271;
                    this.readyFire = true;
                    this.aimSpeed = 0.6f;
                }
                this.turret.localRotation = Quaternion.RotateTowards(this.turret.localRotation, Quaternion.Euler((float)0, this.playerAngle, (float)0), this.aimSpeed * this.root.timescale);
            }
            if (this.readyFire && this.fireTimer >= (float)0)
            {
                float z = this.pipe.localRotation.eulerAngles.z + (float)3 * this.root.timescale;
                Quaternion localRotation2 = this.pipe.localRotation;
                Vector3 eulerAngles2 = localRotation2.eulerAngles;
                float num3 = eulerAngles2.z = z;
                Vector3 vector2 = localRotation2.eulerAngles = eulerAngles2;
                Quaternion quaternion2 = this.pipe.localRotation = localRotation2;
                this.fireTimer += this.root.timescale;
                if (this.fireTimer > (float)50)
                {
                    this.aimSpeed = 0.2f;
                    this.fireWeapon = true;
                }
                else
                {
                    this.aimSpeed = (float)0;
                    if (!this.windUpAudioSource.isPlaying)
                    {
                        this.windUpAudioSource.Play();
                    }
                    this.windUpAudioSource.volume = Mathf.Clamp01(this.fireTimer / (float)35);
                    this.windUpAudioSource.pitch = Mathf.Clamp01(this.fireTimer / (float)45) * 1.25f;
                }
                if (this.fireTimer > (float)220)
                {
                    this.fireWeapon = false;
                    this.aimSpeed = (float)0;
                }
                if (this.fireTimer > (float)260)
                {
                    this.fireTimer = (float)UnityEngine.Random.Range(-180, -140);
                    this.readyFire = false;
                }
            }
            else
            {
                this.fireTimer = Mathf.Clamp(this.fireTimer + this.root.timescale, this.fireTimer, (float)0);
                this.aimSpeed = this.root.Damp((float)2, this.aimSpeed, 0.05f);
                this.windUpAudioSource.volume = Mathf.Clamp01(this.windUpAudioSource.volume - 0.025f * this.root.timescale);
                this.windUpAudioSource.pitch = Mathf.Clamp(this.windUpAudioSource.pitch - 0.05f * this.root.timescale, (float)0, (float)10);
            }
            this.fireDelay -= this.root.timescale;
            if (this.animTimer >= (float)80 && this.fireWeapon && this.fireDelay <= (float)0)
            {
                this.smokeParticle.Emit(this.root.generateEmitParams(this.pipe.position, Vector3.zero, (float)3, (float)2, new Color((float)1, (float)1, (float)1, 0.1f)), 1);
                this.shellParticle.Emit(this.root.generateEmitParams(this.pipe.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), (float)0), 0.15f, (float)3, Color.white), 1);
                GameObject gameObject = this.root.getBullet(this.pipe.position - this.pipe.forward, Quaternion.LookRotation(-this.pipe.forward));
                BulletScript bulletScript = this.root.getBulletScript();
                bulletScript.bulletStrength = ((!this.tutorialTurret) ? 0.1f : ((float)2));
                bulletScript.bulletSpeed = (float)20;
                bulletScript.bulletLength = (float)2;
                bulletScript.fromTransform = this.transform;
                bulletScript.turretBullet = true;
                if (this.tutorialTurret)
                {
                    bulletScript.friendly = !this.tutorialTurretInPosition;
                }
                else
                {
                    bulletScript.friendly = false;
                }
                bulletScript.doPostSetup();
                this.root.getMuzzleFlash(2, this.pipe.position, Quaternion.LookRotation(-this.pipe.forward) * Quaternion.Euler((float)0, (float)270, (float)0));
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(UnityEngine.Random.Range(-1f, 1f), (float)0, (float)0);
                BoxCollider collider = (BoxCollider)gameObject.GetComponent(typeof(BoxCollider));
                Physics.IgnoreCollision(collider, this.shield1Collider, true);
                Physics.IgnoreCollision(collider, this.shield2Collider, true);
                bulletScript.collisionIgnoreLogic(this.shield1Collider);
                bulletScript.collisionIgnoreLogic(this.shield2Collider);
                this.shootAudioSource.volume = UnityEngine.Random.Range(0.9f, (float)1);
                this.shootAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                this.shootAudioSource.Play();
                this.fireDelay = (float)4;
            }
            this.hitColorOffset += (this.hitColorOffsetTarget - this.hitColorOffset) * Mathf.Clamp01(this.root.timescale * 0.05f);
            this.gunHolderRend.materials[1].SetTextureOffset("_MainTex", this.hitColorOffset);
        }
        else
        {
            if (this.autoAimScript != null)
            {
                this.autoAimScript.enabled = false;
            }
            if (this.tutorialTurret && this.root.alwaysDoDodgeHelper)
            {
                this.tutorialTurret = false;
            }
            this.turret.localRotation = Quaternion.RotateTowards(this.turret.localRotation, Quaternion.Euler((float)0, (float)0, (float)0), (float)3 * this.root.timescale);
            this.windUpAudioSource.volume = Mathf.Clamp01(this.windUpAudioSource.volume - 0.025f * this.root.timescale);
            this.windUpAudioSource.pitch = Mathf.Clamp(this.windUpAudioSource.pitch - 0.05f * this.root.timescale, (float)0, (float)10);
            if (this.animTimer <= (float)0 && this.health <= (float)0)
            {
                this.gunHolder.gameObject.SetActive(false);
                this.aimFromPoint.gameObject.SetActive(false);
                this.root.enemyEngagedWithPlayer = true;
                ((GunTurretScript)this.transform.GetComponent(typeof(GunTurretScript))).enabled = false;
            }
        }
        float z2 = Mathf.Lerp((float)0, 0.25f, Mathf.Clamp01(this.animTimer / (float)15));
        Vector3 localPosition = this.door.localPosition;
        float num4 = localPosition.z = z2;
        Vector3 vector3 = this.door.localPosition = localPosition;
        if (this.door.localPosition.z <= 0.1f)
        {
            if (!this.doorMovedDoOnce)
            {
                this.appearAudioSource.pitch = 0.5f;
                this.appearAudioSource.volume = (float)1;
                this.appearAudioSource.Play();
                this.doorMovedDoOnce = true;
            }
        }
        else if (this.doorMovedDoOnce)
        {
            this.appearAudioSource.pitch = (float)1;
            this.appearAudioSource.volume = (float)1;
            this.appearAudioSource.Play();
            this.doorMovedDoOnce = false;
        }
        float z3 = Mathf.Lerp(this.gunHolderStartZPos, (float)0, Mathf.Clamp01((this.animTimer - (float)20) / (float)20));
        Vector3 localPosition2 = this.gunHolder.localPosition;
        float num5 = localPosition2.z = z3;
        Vector3 vector4 = this.gunHolder.localPosition = localPosition2;
        float z4 = Mathf.Lerp((float)180, (float)0, Mathf.Clamp01((this.animTimer - (float)30) / (float)40));
        Quaternion localRotation3 = this.gunHolder.localRotation;
        Vector3 eulerAngles3 = localRotation3.eulerAngles;
        float num6 = eulerAngles3.z = z4;
        Vector3 vector5 = localRotation3.eulerAngles = eulerAngles3;
        Quaternion quaternion3 = this.gunHolder.localRotation = localRotation3;
        if (!this.noShields)
        {
            float x = Mathf.Lerp((float)0, (float)-75, Mathf.Clamp01((this.animTimer - (float)40) / (float)20));
            Quaternion localRotation4 = this.shield1.localRotation;
            Vector3 eulerAngles4 = localRotation4.eulerAngles;
            float num7 = eulerAngles4.x = x;
            Vector3 vector6 = localRotation4.eulerAngles = eulerAngles4;
            Quaternion quaternion4 = this.shield1.localRotation = localRotation4;
            float x2 = Mathf.Lerp((float)0, (float)75, Mathf.Clamp01((this.animTimer - (float)40) / (float)20));
            Quaternion localRotation5 = this.shield2.localRotation;
            Vector3 eulerAngles5 = localRotation5.eulerAngles;
            float num8 = eulerAngles5.x = x2;
            Vector3 vector7 = localRotation5.eulerAngles = eulerAngles5;
            Quaternion quaternion5 = this.shield2.localRotation = localRotation5;
        }
    }

    // Token: 0x06000214 RID: 532 RVA: 0x00032364 File Offset: 0x00030564
    public virtual void bulletHit(float strength)
    {
        if ((!this.tutorialTurret || (this.tutorialTurret && this.tutorialTurretInPosition)) && this.health > (float)0)
        {
            this.health -= strength;
            this.root.cCheckGiSc = true;
            this.root.giveScore((float)20, this.root.GetTranslation("bul14"), false);
            if (this.health <= (float)0)
            {
                this.root.cCheckGiSc = true;
                this.root.giveScore((float)3000, this.root.GetTranslation("bul18"), true);
                this.localSwitchScript.output = (float)-1;
                this.root.explode(this.turret.position, 1.7f, 2, -this.transform.forward * (float)3, "Yellow", true, true);
                this.health = (float)0;
            }
            float y = this.health / 4.5f;
            Vector3 localScale = this.healthBar.localScale;
            float num = localScale.y = y;
            Vector3 vector = this.healthBar.localScale = localScale;
            this.hitColorOffsetTarget = new Vector2(((float)1 - this.health / (float)10) * -0.15f, ((float)1 - this.health / (float)10) * -0.09f);
            this.hitColorOffset = new Vector2((float)0, 0.07f);
            this.hitTimer = (float)3 + ((float)10 - this.health) * (float)2;
            this.electric.SetActive(true);
        }
    }
}
