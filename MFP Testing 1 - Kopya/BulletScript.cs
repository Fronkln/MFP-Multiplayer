using System;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x02000018 RID: 24
[Serializable]
public class BulletScript : MonoBehaviour
{

    public Steamworks.CSteamID shooter;


    // Token: 0x06000074 RID: 116 RVA: 0x0000236D File Offset: 0x0000056D
    public BulletScript()
    {
        this.allowScreenShake = true;
        this.bulletSpeed = (float)10;
        this.bulletStrength = 0.35f;
        this.friendly = true;
        this.bulletKillOnHeadshot = true;
        this.bulletLength = (float)1;
    }

    // Token: 0x06000075 RID: 117 RVA: 0x000023A6 File Offset: 0x000005A6
    public virtual void LateUpdate()
    {

        if (MultiplayerManagerTest.inst.gamemode == MPGamemodes.PvP || MultiplayerManagerTest.inst.gamemode == MPGamemodes.Race)
            return;

        if (this.root.doCheckpointLoad)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Token: 0x06000076 RID: 118 RVA: 0x0000BE44 File Offset: 0x0000A044
    public virtual void Start()
    {
        if (this.root == null)
        {
            this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
            this.rootShared = RootSharedScript.Instance;
            if (!this.isExplosion)
            {
                this.theBoxCollider = (BoxCollider)this.GetComponent(typeof(BoxCollider));
                this.theBoxColliderStartSize = this.theBoxCollider.size;
                this.bulletAudioTransform = this.transform.Find("Audio Source");
                this.bulletAudio = (AudioSource)this.bulletAudioTransform.GetComponent(typeof(AudioSource));
                this.bulletAudioStartClip = this.bulletAudio.clip;
                this.bulletAudioStartVol = this.bulletAudio.volume;
                this.bulletAudioStartPitch = this.bulletAudio.pitch;
                this.bulletAudioStartDoppler = this.bulletAudio.dopplerLevel;
            }
            else
            {
                this.theSphereCollider = (SphereCollider)this.GetComponent(typeof(SphereCollider));
                this.thisBulletScript = (BulletScript)this.GetComponent(typeof(BulletScript));
            }
            this.bulletHitParticle = (ParticleSystem)GameObject.Find("Main Camera/BulletHitParticle").GetComponent(typeof(ParticleSystem));
            this.smokeSpriteParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeSpriteParticle").GetComponent(typeof(ParticleSystem));
            this.smokeParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeParticle").GetComponent(typeof(ParticleSystem));
            this.sparkParticle = (ParticleSystem)GameObject.Find("Main Camera/SparksParticle").GetComponent(typeof(ParticleSystem));
            this.destroyParticleGlass = (ParticleSystem)GameObject.Find("Main Camera/DestroyParticleGlass").GetComponent(typeof(ParticleSystem));
            this.bloodParticle = (ParticleSystem)GameObject.Find("Main Camera/BloodDropsParticle").GetComponent(typeof(ParticleSystem));
            this.playerTransform = GameObject.Find("Player").transform;
            this.cameraScript = (CameraScript)GameObject.Find("Main Camera").GetComponent(typeof(CameraScript));
            this.rigidBody = (Rigidbody)this.GetComponent(typeof(Rigidbody));
            this.bloodMistParticle = (ParticleSystem)GameObject.Find("Main Camera/BloodMistParticle").GetComponent(typeof(ParticleSystem));
            this.glassParticle = (ParticleSystem)GameObject.Find("Main Camera/GlassParticle").GetComponent(typeof(ParticleSystem));
            this.layerMask = -2145428224;
            this.layerMaskMinusPlayer = -2145430272;
            this.layerMask2 = 65792;
            this.layerMask3 = 98560;
            this.layerMask4 = 81920;
            this.layerMask5 = 256;
            if (!this.isExplosion)
            {
                if (Time.timeSinceLevelLoad > (float)1)
                {
                    MonoBehaviour.print("!!!!!!! BULLET GOT CREATED - SHOULD PROBABLY NOT HAPPEN !!!!!!!!!!");
                }
                this.gameObject.SetActive(false);
            }
        }
    }

    // Token: 0x06000077 RID: 119 RVA: 0x0000C180 File Offset: 0x0000A380
    public virtual void doSetup()
    {
        if (this.root == null)
        {
            this.Start();
        }
        if (this.lastIgnoredCollider != null)
        {
            Physics.IgnoreCollision(this.lastIgnoredCollider, this.theBoxCollider, false);
            this.lastIgnoredCollider = null;
        }
        if (this.lastIgnoredCollider2 != null)
        {
            Physics.IgnoreCollision(this.lastIgnoredCollider2, this.theBoxCollider, false);
            this.lastIgnoredCollider2 = null;
        }
        if (!this.isExplosion)
        {
            this.theBoxCollider.size = this.theBoxColliderStartSize;
            this.bulletAudio.clip = this.bulletAudioStartClip;
            this.bulletAudio.volume = this.bulletAudioStartVol;
            this.bulletAudio.pitch = this.bulletAudioStartPitch;
            this.bulletAudio.dopplerLevel = this.bulletAudioStartDoppler;
            this.bulletAudio.Play();
            this.bulletAudioTransform.SetParent(this.transform);
            this.bulletAudioTransform.localPosition = Vector3.zero;
            this.bulletAudioTransform.localRotation = Quaternion.Euler((float)0, (float)0, (float)0);
            this.bulletAudioTransform.localScale = Vector3.one;
        }
        else
        {
            this.theSphereCollider.isTrigger = false;
        }
        this.rigidBody.velocity = Vector3.zero;
        this.orgPos = this.transform.position;
        float z = 0.025f;
        Vector3 localScale = this.transform.localScale;
        float num = localScale.z = z;
        Vector3 vector = this.transform.localScale = localScale;
        this.destroyTimer = (float)0;
        this.bulletHasHit = false;
        this.allowScreenShake = true;
        this.playedSound = false;
        this.bulletSpeed = (float)10;
        this.friendly = true;
        this.bulletStrength = ((!this.isExplosion) ? 0.35f : ((float)3));
        this.allowGib = this.isExplosion;
        this.knockBack = this.isExplosion;
        this.bounced = false;
        this.bulletKillOnHeadshot = true;
        this.bulletLength = (float)1;
        this.timeAlive = (float)0;
        this.particleEmitPoint = Vector3.zero;
        this.upcomingBulletHitPos = Vector3.zero;
        this.ignoreTransform = null;
        this.gaveDodgeScore = false;
        this.fromTransform = null;
        this.turretBullet = false;
        this.tailCheck = (float)0;
        this.midAirShot = false;
        this.wallJumpShot = false;
        this.enemyJumpShot = false;
        this.playerAngularVelocityShot = (float)0;
        this.splitShot = false;
        this.dodgeShot = false;
        this.swingShot = false;
        this.slideShot = false;
        this.dramaticEntranceShot = false;
        this.slowMoShot = false;
        this.nrOfBounces = 0;
        this.bouncedOffFryingPan = false;
    }

    // Token: 0x06000078 RID: 120 RVA: 0x0000C420 File Offset: 0x0000A620
    public virtual void doPostSetup()
    {
        this.bulletLength += (float)6;
        this.doDodgeHelperCheck();
        this.destroyTimerMultiplier = (float)1;
        float z = this.theBoxColliderStartSize.z;
        Vector3 size = this.theBoxCollider.size;
        float num = size.z = z;
        Vector3 vector = this.theBoxCollider.size = size;
        float z2 = -0.3404705f;
        Vector3 center = this.theBoxCollider.center;
        float num2 = center.z = z2;
        Vector3 vector2 = this.theBoxCollider.center = center;
        if (!this.friendly)
        {
            if (this.rootShared.modEnemyBulletSpeed != (float)50)
            {
                this.destroyTimerMultiplier = ((this.rootShared.modEnemyBulletSpeed >= (float)50) ? (this.rootShared.modEnemyBulletSpeed / (float)50 * 1.5f) : (0.1f + this.rootShared.modEnemyBulletSpeed / (float)50 * 0.9f));
                this.bulletSpeed = Mathf.Clamp(this.bulletSpeed * ((this.rootShared.modEnemyBulletSpeed >= (float)50) ? (this.rootShared.modEnemyBulletSpeed / (float)50 * 1.5f) : (0.1f + this.rootShared.modEnemyBulletSpeed / (float)50 * 0.9f)), (float)1, (float)30);
                if (this.rootShared.modEnemyBulletSpeed < (float)50)
                {
                    float z3 = this.theBoxColliderStartSize.z * this.destroyTimerMultiplier;
                    Vector3 size2 = this.theBoxCollider.size;
                    float num3 = size2.z = z3;
                    Vector3 vector3 = this.theBoxCollider.size = size2;
                    float z4 = -0.3404705f * this.destroyTimerMultiplier;
                    Vector3 center2 = this.theBoxCollider.center;
                    float num4 = center2.z = z4;
                    Vector3 vector4 = this.theBoxCollider.center = center2;
                }
            }
        }
        else if (this.rootShared.modPlayerBulletSpeed != (float)50)
        {
            this.destroyTimerMultiplier = ((this.rootShared.modPlayerBulletSpeed >= (float)50) ? (this.rootShared.modPlayerBulletSpeed / (float)50 * 1.5f) : (0.1f + this.rootShared.modPlayerBulletSpeed / (float)50 * 0.9f));
            this.bulletSpeed = Mathf.Clamp(this.bulletSpeed * ((this.rootShared.modPlayerBulletSpeed >= (float)50) ? (this.rootShared.modPlayerBulletSpeed / (float)50 * 1.5f) : (0.1f + this.rootShared.modPlayerBulletSpeed / (float)50 * 0.9f)), (float)1, (float)30);
            if (this.rootShared.modPlayerBulletSpeed < (float)50)
            {
                float z5 = this.theBoxColliderStartSize.z * this.destroyTimerMultiplier;
                Vector3 size3 = this.theBoxCollider.size;
                float num5 = size3.z = z5;
                Vector3 vector5 = this.theBoxCollider.size = size3;
                float z6 = -0.3404705f * this.destroyTimerMultiplier;
                Vector3 center3 = this.theBoxCollider.center;
                float num6 = center3.z = z6;
                Vector3 vector6 = this.theBoxCollider.center = center3;
            }
        }
    }

    // Token: 0x06000079 RID: 121 RVA: 0x0000C784 File Offset: 0x0000A984
    public virtual void collisionIgnoreLogic(Collider col)
    {
        if (this.lastIgnoredCollider == null || this.lastIgnoredCollider2 != null)
        {
            if (this.lastIgnoredCollider != null)
            {
                Physics.IgnoreCollision(col, this.lastIgnoredCollider, false);
            }
            if (this.theBoxCollider != null)
            {
                Physics.IgnoreCollision(col, this.theBoxCollider, true);
            }
            this.lastIgnoredCollider = col;
        }
        else
        {
            if (this.lastIgnoredCollider2 != null)
            {
                Physics.IgnoreCollision(col, this.lastIgnoredCollider2, false);
            }
            if (this.theBoxCollider != null)
            {
                Physics.IgnoreCollision(col, this.theBoxCollider, true);
            }
            this.lastIgnoredCollider2 = col;
        }
    }

    // Token: 0x0600007A RID: 122 RVA: 0x0000C840 File Offset: 0x0000AA40
    public virtual void doDodgeHelperCheck()
    {
        if (!this.friendly && !this.root.neverDoDodgeHelper)
        {
            RaycastHit raycastHit = default(RaycastHit);
            if (Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y, this.playerTransform.position.z), this.transform.forward, out raycastHit, this.bulletSpeed * (float)30, this.layerMask) && raycastHit.collider.tag == "Player")
            {
                this.root.dodgeHelper = true;
                this.root.dodgeHelperDamage = this.root.dodgeHelperDamage + this.bulletStrength * (float)((!this.turretBullet) ? 1 : 5);
                if (raycastHit.distance < this.root.dodgeHelperClosestDistance)
                {
                    this.root.dodgeHelperTransform = this.transform;
                    this.root.dodgeHelperClosestDistance = raycastHit.distance;
                }
            }
        }
    }

    // Token: 0x0600007B RID: 123 RVA: 0x0000C96C File Offset: 0x0000AB6C
    public virtual void Update()
    {
        if (!this.isExplosion)
        {
            this.destroyTimer += this.destroyTimerMultiplier * this.root.timescale;
            if (this.destroyTimer >= (float)240)
            {
                this.gameObject.SetActive(false);
            }
            if (this.upcomingBulletHitPos == Vector3.zero)
            {
                this.rigidBody.velocity = this.transform.forward * (this.bulletSpeed * 6.5f * this.root.timescaleRaw);
            }
            if (this.transform.localScale.z < this.bulletLength)
            {
                float z = Mathf.Clamp(Vector3.Distance(this.transform.position, this.orgPos) / 0.728777f, (float)0, this.bulletLength);
                Vector3 localScale = this.transform.localScale;
                float num = localScale.z = z;
                Vector3 vector = this.transform.localScale = localScale;
            }
            this.doDodgeHelperCheck();
        }
        else
        {
            this.destroyTimer += this.root.timescale;
            if (this.destroyTimer > (float)30)
            {
                this.theSphereCollider.enabled = false;
                this.thisBulletScript.enabled = false;
            }
        }
        this.timeAlive += this.root.timescale;
    }

    // Token: 0x0600007C RID: 124 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
    public virtual void FixedUpdate()
    {
        if (!this.isExplosion)
        {
            if (this.upcomingBulletHitPos != Vector3.zero)
            {
                this.rigidBody.velocity = this.transform.forward;
                this.rigidBody.MovePosition(this.upcomingBulletHitPos);
                this.upcomingBulletHitPos = Vector3.zero;
            }
            RaycastHit raycastHit = default(RaycastHit);
            if (Physics.Raycast(this.transform.position + this.transform.forward * -this.tailCheck, this.transform.forward, out raycastHit, this.rigidBody.velocity.magnitude / (float)60 + this.tailCheck, (!this.friendly) ? this.layerMask : this.layerMaskMinusPlayer) && raycastHit.transform != this.ignoreTransform && (!raycastHit.collider.isTrigger || raycastHit.collider.tag == "FriendlyBulletsOnly") && (!this.friendly || (this.friendly && raycastHit.collider.tag != "Player")))
            {
                this.rigidBody.velocity = this.transform.forward;
                this.upcomingBulletHitPos = raycastHit.point;
            }
            this.tailCheck = (float)0;
        }
    }

    // Token: 0x0600007D RID: 125 RVA: 0x0000CC64 File Offset: 0x0000AE64
    public virtual void OnTriggerEnter(Collider col)
    {
        if (!this.bulletHasHit)
        {
            this.particleEmitPoint = col.transform.position;
            if (!this.root.pleaseESRB && col.gameObject.tag == "Enemy" && this.friendly && !this.isExplosion)
            {
                bool flag = true;
                RaycastHit raycastHit = default(RaycastHit);
                Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * (float)8);
                if (Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y, (float)0), this.transform.forward, out raycastHit, (float)8, this.layerMask4))
                {
                    GasCanisterScript gasCanisterScript = (GasCanisterScript)raycastHit.transform.GetComponent(typeof(GasCanisterScript));
                    if (raycastHit.transform.tag == "BounceBullet" || (gasCanisterScript != null && !gasCanisterScript.doEmitSmoke))
                    {
                        flag = false;
                    }
                }
                EnemyScript enemyScript = (EnemyScript)col.gameObject.GetComponentInParent(typeof(EnemyScript));
                if (enemyScript.allowBulletHit /*&& enemyScript.runLogic && flag*/)
                {
                    enemyScript.bulletHit = true;
                    enemyScript.bulletStrength += this.bulletStrength;
                    enemyScript.bulletHitName = col.gameObject.name;
                    enemyScript.bulletHitPos = this.transform.position;
                    enemyScript.bulletHitRot = this.transform.rotation;
                    if (this.rigidBody.velocity.magnitude < 0.5f)
                    {
                        enemyScript.bulletHitVel = this.transform.forward * (float)4;
                    }
                    else
                    {
                        enemyScript.bulletHitVel = this.rigidBody.velocity / 1.5f / (float)4;
                    }
                    enemyScript.allowGib = this.allowGib;
                    enemyScript.bulletTimeAlive = this.timeAlive;
                    enemyScript.bulletKillOnHeadshot = this.bulletKillOnHeadshot;
                    string text = null;
                    float num = 0f;
                    bool flag2 = default(bool);
                    bool flag3 = default(bool);
                    if (this.enemyJumpShot)
                    {
                        text += this.root.GetTranslation("bul1");
                        num = (float)100;
                        flag2 = true;
                        flag3 = true;
                    }
                    else if (this.wallJumpShot)
                    {
                        text += this.root.GetTranslation("bul2");
                        num = (float)50;
                        flag2 = true;
                        flag3 = true;
                    }
                    else if (this.slideShot)
                    {
                        text += this.root.GetTranslation("bul3");
                        num += (float)10;
                        flag2 = true;
                        flag3 = true;
                    }
                    else if (this.swingShot)
                    {
                        text += this.root.GetTranslation("bul4");
                        num += (float)10;
                        flag2 = true;
                        flag3 = true;
                    }
                    else if (this.midAirShot)
                    {
                        text += this.root.GetTranslation("bul5");
                        num = (float)20;
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        text += "-";
                        flag2 = false;
                    }
                    if (Mathf.Abs(this.playerAngularVelocityShot) > (float)7 && !this.swingShot && !this.slideShot)
                    {
                        text += this.root.GetTranslation("bul6");
                        num += (float)20;
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        text += "-";
                        flag2 = false;
                    }
                    if (this.dodgeShot)
                    {
                        text += this.root.GetTranslation("bul7");
                        num += (float)40;
                        flag2 = true;
                        flag3 = true;
                    }
                    else if (this.splitShot)
                    {
                        text += this.root.GetTranslation("bul8");
                        num += (float)30;
                        flag2 = true;
                        flag3 = true;
                    }
                    if (flag2)
                    {
                        text += "-";
                        flag2 = false;
                    }
                    if (this.dramaticEntranceShot)
                    {
                        text += this.root.GetTranslation("bul9");
                        num += (float)100;
                        flag2 = true;
                        flag3 = true;
                    }
                    if (flag2)
                    {
                        text += "-";
                        flag2 = false;
                    }
                    if (this.nrOfBounces > 0)
                    {
                        text += this.root.GetTranslation("bul10");
                        num += (float)(50 * this.nrOfBounces);
                        flag2 = true;
                        flag3 = true;
                    }
                    if (flag2)
                    {
                        text += "-";
                        flag2 = false;
                    }
                    if (this.allowGib && this.timeAlive <= (float)5 && enemyScript.health - enemyScript.bulletStrength <= (float)0)
                    {
                        text += this.root.GetTranslation("bul11");
                        num += (float)100;
                        flag2 = true;
                        flag3 = true;
                    }
                    if (flag2)
                    {
                        text += "-";
                    }
                    if (flag3)
                    {
                        enemyScript.bulletKillText = text + this.root.GetTranslation("bul12");
                    }
                    else if (num >= (float)40)
                    {
                        enemyScript.bulletKillText = this.root.getEncouragingText();
                    }
                    else
                    {
                        enemyScript.bulletKillText = string.Empty;
                    }
                    enemyScript.bulletHitText = text;
                    enemyScript.bulletHitExtraScore = num;
                    if (enemyScript.health - enemyScript.calculateActualBulletHitStrength() <= (float)0 && this.bouncedOffFryingPan)
                    {
                        this.root.trackStat("fryingPanBounceKill");
                    }
                    if (num >= (float)150 && enemyScript.health - enemyScript.bulletStrength <= (float)0)
                    {
                        if (this.root.reactionPedroTimer <= (float)0)
                        {
                            this.root.reactionPedroFace.sprite = this.root.pedroExpressions[UnityEngine.Random.Range(2, Extensions.get_length(this.root.pedroExpressions) - 3)];
                        }
                        this.root.reactionPedroTimer = (float)180;
                    }
                    if (this.knockBack)
                    {
                        enemyScript.knockBack(this.rigidBody.velocity.x > (float)0, (float)20);
                    }
                    if (!this.root.cinematicShot && !this.root.showNoBlood)
                    {
                        this.emitBlood(false, (float)0);
                        if (this.root.doGore && enemyScript.motorcycle == null && !enemyScript.skyfall && this.root.framesSinceBloodSplatter >= (float)10)
                        {
                            RaycastHit raycastHit2 = default(RaycastHit);
                            if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit2, (float)5, this.layerMask5))
                            {
                                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bloodSplatter, new Vector3(raycastHit2.point.x, raycastHit2.point.y, (float)0), Quaternion.FromToRotation(Vector3.right, raycastHit2.normal));
                                gameObject.transform.position = gameObject.transform.position + gameObject.transform.right * 0.1f;
                                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.bloodSplatterStatic, this.transform.position, Quaternion.Euler(this.transform.eulerAngles.x, (float)((this.rigidBody.velocity.x <= (float)0) ? -90 : 90), (float)UnityEngine.Random.Range(0, 360)));
                                gameObject2.transform.position = gameObject2.transform.position + gameObject2.transform.forward * (float)-2;
                                this.emitBlood(true, col.transform.position.z);
                            }
                            this.root.framesSinceBloodSplatter = (float)0;
                        }
                    }
                    this.bulletHasHit = true;
                    this.cameraScript.screenShake = this.cameraScript.screenShake + 0.01f;
                    this.playImpactSound((SoundScript)col.transform.GetComponentInParent(typeof(SoundScript)));
                    this.gameObject.SetActive(false);
                }
                else
                {
                    enemyScript.doorHideTimer = (float)UnityEngine.Random.Range(20, 70);
                }
            }

            else if (MultiplayerManagerTest.inst.gamemode == MPGamemodes.PvP && col.gameObject.transform.root.name.StartsWith("PlayerGhost"))
            {
                Steamworks.CSteamID playerID = (Steamworks.CSteamID)ulong.Parse(col.gameObject.transform.root.name.Split('_')[1]);

                MFPPlayerGhost playerGhost = MultiplayerManagerTest.inst.playerObjects[playerID];

                if (EMFDNS.isLocalUser(playerGhost.owner) || playerGhost.playerAnimator.GetBool("Dodging"))
                    return;

                if (!this.root.pleaseESRB)
                    emitBlood(false, 0);

                gameObject.SetActive(false);

            }

            else if ((!this.root.pleaseESRB && col.gameObject.tag == "Player" && (!this.friendly || (this.isExplosion && this.timeAlive < (float)15))) || (col.gameObject.tag == "EvilPlayer" && this.friendly))
            {
                PlayerScript playerScript = (PlayerScript)col.gameObject.GetComponentInParent(typeof(PlayerScript));
                if (((playerScript.onMotorcycle && !this.isExplosion) || !playerScript.onMotorcycle) && (playerScript.dodgingCoolDown < (float)((!playerScript.isEnemy) ? 15 : 25) || this.isExplosion))
                {
                    playerScript.bulletHit = true;
                    playerScript.bulletHitRotation = this.transform.rotation;
                    playerScript.bulletHitVel = this.rigidBody.velocity;
                    playerScript.bulletStrength = ((!this.isExplosion) ? (playerScript.bulletStrength + this.bulletStrength) : 0.5f);
                    if (this.isExplosion && playerScript.isEnemy)
                    {
                        playerScript.bulletStrength = 2.5f;
                    }
                    playerScript.bulletFromTransform = this.fromTransform;
                    playerScript.bulletHitDoSound = true;
                    this.emitBlood(false, (float)0);
                    this.bulletHasHit = true;
                    this.cameraScript.kickBack(0.4f);
                    this.cameraScript.screenShake = this.cameraScript.screenShake + 0.04f;
                    if (!this.isExplosion)
                    {
                        this.playImpactSound((SoundScript)col.transform.GetComponentInParent(typeof(SoundScript)));
                        this.gameObject.SetActive(false);
                    }
                    else
                    {
                        this.root.earRingingTimer = (float)300;
                    }
                }
                else if (playerScript.dodgingCoolDown >= (float)15 && !this.gaveDodgeScore)
                {
                    this.root.trackStat("dodged");
                    this.root.timeSinceScoreLastGiven = (float)0;
                    this.root.rumble(0, 0.1f, 0.05f);
                    this.root.rumble(1, 0.1f, 0.05f);
                    this.bulletAudio.clip = playerScript.bulletDodgeSound;
                    this.bulletAudio.volume = UnityEngine.Random.Range((float)0, 0.7f);
                    this.bulletAudio.pitch = UnityEngine.Random.Range(0.85f, 1.45f);
                    this.bulletAudio.dopplerLevel = UnityEngine.Random.Range(1.4f, 2.1f);
                    this.bulletAudio.Play();
                    this.gaveDodgeScore = true;
                }
            }
            else if (this.friendly && col.gameObject.tag == "Lever")
            {
                SwitchScript switchScript = (SwitchScript)col.gameObject.GetComponentInParent(typeof(SwitchScript));
                LeverScript leverScript = (LeverScript)col.gameObject.GetComponentInParent(typeof(LeverScript));
                if (leverScript.hasBeenOnScreen)
                {
                    if (col.transform.InverseTransformPoint(col.transform.position + this.rigidBody.velocity).y * (float)((!leverScript.invert) ? 1 : -1) > (float)0)
                    {
                        if (switchScript.output != (float)1)
                        {
                            this.emitBulletHitParticles(Vector3.zero);
                            this.playImpactSound((SoundScript)col.transform.GetComponentInParent(typeof(SoundScript)));
                            switchScript.output = (float)1;
                            this.gameObject.SetActive(false);
                        }
                    }
                    else if (switchScript.output != (float)-1)
                    {
                        this.emitBulletHitParticles(Vector3.zero);
                        this.playImpactSound((SoundScript)col.transform.GetComponentInParent(typeof(SoundScript)));
                        switchScript.output = (float)-1;
                        this.gameObject.SetActive(false);
                    }
                }
            }
            else if (col.gameObject.tag == "BounceBulletReverse" && !this.friendly && !this.isExplosion)
            {
                this.emitSparks();
                this.bounced = true;
                float z = 0.025f;
                Vector3 localScale = this.transform.localScale;
                float num2 = localScale.z = z;
                Vector3 vector = this.transform.localScale = localScale;
                this.orgPos = this.transform.position;
                this.transform.rotation = this.transform.rotation * Quaternion.Euler((float)(180 + UnityEngine.Random.Range(-5, 5)), (float)0, (float)0);
                this.transform.position = this.transform.position + this.transform.forward * (float)2 * this.root.timescale;
                this.friendly = true;
                this.collisionIgnoreLogic(col);
                this.ignoreTransform = col.transform;
            }
            else if (col.gameObject.layer == 17)
            {
                ((TurretGunScript)col.gameObject.GetComponent(typeof(TurretGunScript))).health = ((TurretGunScript)col.gameObject.GetComponent(typeof(TurretGunScript))).health - (float)1;
            }
            else if (col.gameObject.tag == "FriendlyBulletsOnly" && this.friendly && !this.isExplosion)
            {
                this.bulletHasHit = true;
                this.emitSparks();
                this.gameObject.SetActive(false);
            }
        }
    }

    // Token: 0x0600007E RID: 126 RVA: 0x0000DB28 File Offset: 0x0000BD28
    public virtual void playImpactSound(SoundScript sScript)
    {
        if (!this.playedSound)
        {
            if (sScript != null && Extensions.get_length(sScript.bulletImpact) > 0)
            {
                this.bulletAudio.clip = sScript.bulletImpact[UnityEngine.Random.Range(0, Extensions.get_length(sScript.bulletImpact))];
                if (sScript.pitchDependingOnSize)
                {
                    this.bulletAudio.pitch = Mathf.Abs((float)2 - (sScript.transform.localScale.x + sScript.transform.localScale.y + sScript.transform.localScale.z) / (float)3);
                }
                else
                {
                    this.bulletAudio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                }
                this.bulletAudio.volume = UnityEngine.Random.Range(0.9f, 1.1f);
                if (this.bulletAudio.gameObject.activeInHierarchy && this.bulletAudio.enabled)
                {
                    this.bulletAudio.Play();
                }
                this.bulletAudioTransform.parent = null;
            }
            this.playedSound = true;
        }
    }

    // Token: 0x0600007F RID: 127 RVA: 0x0000DC58 File Offset: 0x0000BE58
    public virtual void emitBulletHitParticles(Vector3 normalDir)
    {
        this.bulletHitParticle.Emit(this.root.generateEmitParams(this.particleEmitPoint, Vector3.zero, (float)4, 0.1f, new Color((float)1, (float)1, (float)1, (float)1)), 1);
        ParticleSystem.MainModule mainModule = smokeSpriteParticle.main;


        mainModule.startRotation = Mathf.Atan2(this.transform.forward.y, this.transform.forward.x);
        ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
        emitParams.position = this.particleEmitPoint;
        emitParams.startLifetime = (float)UnityEngine.Random.Range(2, 3);
        emitParams.startSize = UnityEngine.Random.Range(0.7f, (float)1);
        this.smokeSpriteParticle.Emit(emitParams, 1);
        if (normalDir != Vector3.zero)
        {
            float num = UnityEngine.Random.Range(0.6f, (float)1);
            emitParams.startColor = new Color(num, num, num, UnityEngine.Random.Range(0.8f, (float)1));
            emitParams.startLifetime = UnityEngine.Random.Range(0.5f, 0.8f);
            emitParams.startSize = UnityEngine.Random.Range(0.8f, 1.5f);
            mainModule.startRotation = Mathf.Atan2(-normalDir.y, -normalDir.x);
            this.smokeSpriteParticle.Emit(emitParams, 1);
            emitParams.startColor = new Color(num, num, num, UnityEngine.Random.Range(0.2f, 0.4f));
            emitParams.startSize += UnityEngine.Random.Range(0.6f, (float)1);
            emitParams.startLifetime += UnityEngine.Random.Range(0.05f, 0.13f);
            emitParams.velocity = normalDir * (float)3;
            this.smokeParticle.Emit(emitParams, 1);
        }
        this.smokeParticle.Emit(this.root.generateEmitParams(this.particleEmitPoint, -this.transform.forward / (float)2 + this.transform.up * UnityEngine.Random.value / (float)4, (float)UnityEngine.Random.Range(2, 4), (float)UnityEngine.Random.Range(3, 4), new Color((float)1, (float)1, (float)1, UnityEngine.Random.Range(0.08f, 0.2f))), 1);
        this.smokeParticle.Emit(this.root.generateEmitParams(this.particleEmitPoint, -this.transform.forward / (float)2 - this.transform.up * UnityEngine.Random.value / (float)4, (float)UnityEngine.Random.Range(2, 4), (float)UnityEngine.Random.Range(3, 4), new Color((float)1, (float)1, (float)1, UnityEngine.Random.Range(0.08f, 0.2f))), 1);
        this.emitSparks();
    }

    // Token: 0x06000080 RID: 128 RVA: 0x0000DF54 File Offset: 0x0000C154
    private void emitSparks()
    {
        for (int i = 0; i < 5; i++)
        {
            this.sparkParticle.Emit(this.root.generateEmitParams(this.particleEmitPoint, new Vector3(-this.transform.forward.x * 3.5f + (float)UnityEngine.Random.Range(-4, 4), -this.transform.forward.y * 3.5f + (float)UnityEngine.Random.Range(1, 6), UnityEngine.Random.Range(-0.5f, 0.5f)), UnityEngine.Random.Range(0.06f, 0.08f), (float)1, new Color((float)1, (float)1, (float)1, (float)1)), 1);
        }
    }

    // Token: 0x06000081 RID: 129 RVA: 0x0000E00C File Offset: 0x0000C20C
    public virtual void OnCollisionEnter(Collision col)
    {
        this.particleEmitPoint = col.contacts[0].point;
        this.particleEmitPoint -= this.transform.forward * 0.7f;
        this.particleEmitPoint.z = this.transform.position.z;
        if (!this.isExplosion && (col.gameObject.tag == "Untagged" || col.gameObject.tag == "FloorBuilder" || col.gameObject.tag == "Platform Lift" || col.gameObject.tag == "Flippable" || col.gameObject.tag == "ABMoveFollow" || col.gameObject.tag == "Mech" || col.gameObject.tag == "BouncePad" || col.gameObject.tag == "Knife" || col.gameObject.tag == "SkateRamp"))
        {
            float num = (this.transform.position.z <= (float)1) ? (col.collider.bounds.min.z - this.transform.position.z) : (this.transform.position.z - col.collider.bounds.max.z);
            if (num > (float)0)
            {
                float x = 0.6f;
                Vector3 size = this.theBoxCollider.size;
                float num2 = size.x = x;
                Vector3 vector = this.theBoxCollider.size = size;
                this.collisionIgnoreLogic(col.collider);
            }
            else
            {
                this.emitBulletHitParticles(col.contacts[0].normal);
                if (this.allowScreenShake)
                {
                    this.cameraScript.onOffScreenShake = 0.1f;
                    this.allowScreenShake = false;
                }
                this.playImpactSound((SoundScript)col.transform.GetComponentInParent(typeof(SoundScript)));
                this.gameObject.SetActive(false);
            }
        }
        if (col.gameObject.tag == "DestroyParticleGreenGlass")
        {
            this.destroyParticleHit(new Color(0.01f, 0.05f, 0.02f, UnityEngine.Random.Range(0.5f, 1f)), col.gameObject);
        }
        if (col.gameObject.tag == "DestroyParticleWhiteGlass")
        {
            this.destroyParticleHit(new Color(0.6f, 0.6f, 0.6f, UnityEngine.Random.Range(0.8f, 1f)), col.gameObject);
        }
        if (col.gameObject.tag == "DestroyParticleBlueGlass")
        {
            this.destroyParticleHit(new Color(0.02f, 0.01f, 0.05f, UnityEngine.Random.Range(0.8f, 1f)), col.gameObject);
        }
        if (col.gameObject.tag == "Gib")
        {
            this.emitBloodLight((float)0);
            if (this.allowScreenShake)
            {
                this.allowScreenShake = false;
            }
            if (!this.isExplosion)
            {
                this.playImpactSound((SoundScript)col.transform.GetComponentInParent(typeof(SoundScript)));
                this.gameObject.SetActive(false);
            }
        }
        else if (col.gameObject.layer == 12)
        {
            this.emitBlood(false, (float)0);
            if (this.allowScreenShake)
            {
                this.allowScreenShake = false;
            }
            if (!this.isExplosion)
            {
                this.playImpactSound((SoundScript)col.transform.GetComponentInParent(typeof(SoundScript)));
                this.gameObject.SetActive(false);
            }
        }
        if (!this.isExplosion && col.gameObject.layer == 24)
        {
            this.emitBulletHitParticles(col.contacts[0].normal);
            this.collisionIgnoreLogic(col.collider);
            this.ignoreTransform = col.transform;
            this.transform.position = col.transform.position + Vector3.up;
            float z = 0.025f;
            Vector3 localScale = this.transform.localScale;
            float num3 = localScale.z = z;
            Vector3 vector2 = this.transform.localScale = localScale;
            this.orgPos = this.transform.position;
            this.bounced = true;
            this.playImpactSound((SoundScript)col.transform.GetComponentInParent(typeof(SoundScript)));
            this.transform.rotation = Quaternion.Euler((float)UnityEngine.Random.Range(-180, 0), (float)90, (float)0);
        }
        if (!this.isExplosion && col.gameObject.tag == "BounceBullet")
        {
            this.emitBulletHitParticles(col.contacts[0].normal);
            this.collisionIgnoreLogic(col.collider);
            this.ignoreTransform = col.transform;
            if (Physics.Raycast(col.transform.position + Vector3.up * 0.1f, Vector3.down, (float)1, this.layerMask3))
            {
                col.rigidbody.velocity = Vector3.up * (float)18;
                this.gameObject.SetActive(false);
            }
            else
            {
                this.transform.position = col.transform.position + new Vector3(Mathf.Clamp(-this.rigidBody.velocity.x * (float)10, -0.35f, 0.35f), -0.4f, (float)0);
                float z2 = 0.025f;
                Vector3 localScale2 = this.transform.localScale;
                float num4 = localScale2.z = z2;
                Vector3 vector3 = this.transform.localScale = localScale2;
                this.orgPos = col.transform.position;
                this.bounced = true;
                this.nrOfBounces++;
                if (this.friendly)
                {
                    this.bouncedOffFryingPan = true;
                }
                if (col.rigidbody.velocity.y < (float)5)
                {
                    float y = UnityEngine.Random.Range(1f, 3f);
                    Vector3 velocity = col.rigidbody.velocity;
                    float num5 = velocity.y = y;
                    Vector3 vector4 = col.rigidbody.velocity = velocity;
                    float x2 = col.rigidbody.velocity.x * 0.7f + UnityEngine.Random.Range(-1.5f, 1.5f);
                    Vector3 velocity2 = col.rigidbody.velocity;
                    float num6 = velocity2.x = x2;
                    Vector3 vector5 = col.rigidbody.velocity = velocity2;
                }
                this.transform.rotation = Quaternion.Euler((float)UnityEngine.Random.Range(0, 360), (float)90, (float)0);
                int i = 0;
                Transform[] allEnemies = this.root.allEnemies;
                int length = allEnemies.Length;
                while (i < length)
                {
                    Transform transform = allEnemies[i].Find("EnemyGraphics/Armature/Center/LowerBack");
                    Vector3 vector6 = this.transform.position - transform.position;
                    if (transform.gameObject.layer != 12 && vector6.magnitude < (float)20 && !Physics.Linecast(col.transform.position, transform.position, this.layerMask2))
                    {
                        this.transform.rotation = Quaternion.Euler((float)UnityEngine.Random.Range(178, 182) - Mathf.Atan2(vector6.y, vector6.x) * 57.29578f, (float)90, (float)0);
                        this.friendly = true;
                        if (UnityEngine.Random.value > 0.5f)
                        {
                            break;
                        }
                    }
                    i++;
                }
            }
            col.rigidbody.angularVelocity = new Vector3((float)UnityEngine.Random.Range(-10, 10), (float)UnityEngine.Random.Range(-10, 10), (float)UnityEngine.Random.Range(-10, 10));
        }
        if (!this.isExplosion && col.gameObject.tag == "BounceBulletSign")
        {
            this.emitBulletHitParticles(col.contacts[0].normal);
            this.collisionIgnoreLogic(col.collider);
            this.ignoreTransform = col.transform;
            float z3 = 0.025f;
            Vector3 localScale3 = this.transform.localScale;
            float num7 = localScale3.z = z3;
            Vector3 vector7 = this.transform.localScale = localScale3;
            this.orgPos = this.transform.position;
            this.orgPos.x = col.transform.position.x;
            this.bounced = true;
            this.nrOfBounces++;
            bool flag = this.transform.forward.x > (float)0;
            SignBulletBounceScript signBulletBounceScript = (SignBulletBounceScript)col.gameObject.GetComponent(typeof(SignBulletBounceScript));
            signBulletBounceScript.rotSpeed += ((!flag) ? -2.5f : 2.5f);
            signBulletBounceScript.doSound();
            if (!flag && signBulletBounceScript.rightTargetTransform != null)
            {
                this.transform.forward = (signBulletBounceScript.rightTargetTransform.position - this.transform.position).normalized;
            }
            else if (flag && signBulletBounceScript.leftTargetTransform != null)
            {
                this.transform.forward = (signBulletBounceScript.leftTargetTransform.position - this.transform.position).normalized;
            }
            else if (!flag && signBulletBounceScript.rightTarget.x > (float)0)
            {
                this.transform.rotation = Quaternion.Euler(-Mathf.Atan2(col.transform.position.y + signBulletBounceScript.rightTarget.y - this.transform.position.y, col.transform.position.x + signBulletBounceScript.rightTarget.x - this.transform.position.x) * 57.29578f, (float)90, (float)0);
            }
            else if (flag && signBulletBounceScript.leftTarget.x < (float)0)
            {
                this.transform.rotation = Quaternion.Euler(-Mathf.Atan2(col.transform.position.y + signBulletBounceScript.leftTarget.y - this.transform.position.y, col.transform.position.x + signBulletBounceScript.leftTarget.x - this.transform.position.x) * 57.29578f, (float)90, (float)0);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler((float)((!flag) ? UnityEngine.Random.Range(-5, 5) : UnityEngine.Random.Range(175, 185)), (float)90, (float)0);
            }
            this.friendly = true;
            int j = 0;
            Transform[] allEnemies2 = this.root.allEnemies;
            int length2 = allEnemies2.Length;
            while (j < length2)
            {
                EnemyScript enemyScript = (EnemyScript)allEnemies2[j].GetComponent(typeof(EnemyScript));
                if (!enemyScript.doorSpawn)
                {
                    Transform transform2 = allEnemies2[j].Find("EnemyGraphics/Armature/Center/LowerBack");
                    Vector3 vector8 = this.transform.position - transform2.position;
                    if (transform2.gameObject.layer != 12 && vector8.magnitude < (float)25 && ((flag && col.transform.position.x > allEnemies2[j].position.x) || (!flag && col.transform.position.x < allEnemies2[j].position.x)) && !Physics.Linecast(col.transform.position, allEnemies2[j].position, this.layerMask5))
                    {
                        this.transform.rotation = Quaternion.Euler((float)UnityEngine.Random.Range(178, 182) - Mathf.Atan2(vector8.y, vector8.x) * 57.29578f, (float)90, (float)0);
                        if (UnityEngine.Random.value > 0.5f)
                        {
                            break;
                        }
                    }
                }
                j++;
            }
        }
        if (col.gameObject.tag == "GunTurret")
        {
            this.emitBulletHitParticles(col.contacts[0].normal);
            if (this.allowScreenShake)
            {
                this.cameraScript.onOffScreenShake = 0.1f;
                this.allowScreenShake = false;
            }
            if (!this.isExplosion)
            {
                this.playImpactSound((SoundScript)col.transform.GetComponent(typeof(SoundScript)));
                this.gameObject.SetActive(false);
            }
        }
    }

    // Token: 0x06000082 RID: 130 RVA: 0x0000EE48 File Offset: 0x0000D048
    public virtual void destroyParticleHit(Color colour, GameObject obj)
    {
        this.bulletHitParticle.Emit(this.root.generateEmitParams(obj.transform.position, Vector3.zero, (float)3, 0.1f, new Color((float)1, (float)1, (float)1, (float)1)), 1);
        for (int i = 0; i < 4; i++)
        {
            this.glassParticle.Emit(this.root.generateEmitParams(this.particleEmitPoint + this.transform.forward * UnityEngine.Random.Range(-0.1f, 0.1f), new Vector3(Mathf.Clamp(this.rigidBody.velocity.x / this.root.timescaleRaw, (float)-5, (float)5), this.rigidBody.velocity.y / this.root.timescaleRaw * 0.6f, UnityEngine.Random.Range(-0.2f, 0.2f)) * UnityEngine.Random.Range(0.2f, 0.8f), UnityEngine.Random.Range(0.2f, 0.5f), UnityEngine.Random.Range(0.7f, 1.5f), new Color((float)1, (float)1, (float)1, UnityEngine.Random.Range(0.2f, 1f))), 1);
            this.destroyParticleGlass.Emit(this.root.generateEmitParams(obj.transform.position, new Vector3((float)UnityEngine.Random.Range(-4, 4), (float)UnityEngine.Random.Range(3, 9), UnityEngine.Random.Range(-0.2f, 0.2f)) * UnityEngine.Random.Range(0.7f, 0.9f), UnityEngine.Random.Range(0.3f, 0.5f), UnityEngine.Random.Range(0.6f, (float)1), colour), 1);
        }
        obj.SetActive(false);
    }

    // Token: 0x06000083 RID: 131 RVA: 0x0000F010 File Offset: 0x0000D210
    public virtual void emitBlood(bool inverted, float colZPos)
    {
        if (!this.root.cinematicShot && !this.root.showNoBlood)
        {
            ParticleSystem.MainModule mainModule = default(ParticleSystem.MainModule);
            mainModule = this.smokeSpriteParticle.main;
            mainModule.startRotation = Mathf.Atan2(this.transform.forward.y, this.transform.forward.x) + ((!inverted) ? ((float)0) : 3.14159274f);
            ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
            emitParams.position = this.particleEmitPoint;
            emitParams.startLifetime = (float)UnityEngine.Random.Range(2, 3);
            emitParams.startColor = ((!this.root.doGore) ? new Color((float)0, (float)0, (float)0, UnityEngine.Random.Range(0.1f, 0.3f)) : new Color(0.9f, 0.3f, 0.3f, UnityEngine.Random.Range(0.1f, 0.3f)));
            emitParams.startSize = UnityEngine.Random.Range(0.7f, (float)1);
            this.smokeSpriteParticle.Emit(emitParams, 1);
            if (!inverted)
            {
                this.bloodMistParticle.Emit(this.root.generateEmitParams(this.particleEmitPoint, new Vector3(-this.transform.forward.x * 1.5f, -this.transform.forward.y * 1.5f, UnityEngine.Random.Range(-0.5f, 0.5f)), (float)UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(0.3f, 1f), (!this.root.doGore) ? new Color((float)0, (float)0, (float)0, 0.5f) : new Color((float)1, (float)1, (float)1, 0.5f)), 1);
                if (this.root.doGore)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bloodSplatterAnimated[UnityEngine.Random.Range(0, this.bloodSplatterAnimated.Length)], this.particleEmitPoint, this.transform.rotation * Quaternion.Euler((float)0, (float)90, (float)0));
                    gameObject.transform.localScale = gameObject.transform.localScale * UnityEngine.Random.Range(0.9f, 1.2f);
                }
                for (int i = 0; i < 3; i++)
                {
                    this.bloodParticle.Emit(this.root.generateEmitParams(this.particleEmitPoint, new Vector3(-this.transform.forward.x * 3.5f + (float)UnityEngine.Random.Range(-4, 4), -this.transform.forward.y * 3.5f + (float)UnityEngine.Random.Range(1, 6), UnityEngine.Random.Range(-0.5f, 0.5f)), UnityEngine.Random.Range(0.05f, 0.2f), UnityEngine.Random.Range(0.8f, 1.3f), (!this.root.doGore) ? new Color((float)0, (float)0, (float)0, (float)1) : new Color((float)1, (float)1, (float)1, (float)1)), 1);
                }
            }
            else if (this.root.doGore)
            {
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.bloodSplatterAnimated[1], new Vector3(this.particleEmitPoint.x, this.particleEmitPoint.y, colZPos), this.transform.rotation * Quaternion.Euler((float)0, (float)90, (float)180));
                gameObject2.transform.position = gameObject2.transform.position + gameObject2.transform.right * 0.6f;
                gameObject2.transform.localScale = gameObject2.transform.localScale * UnityEngine.Random.Range(1.8f, 2.2f);
            }
        }
    }

    // Token: 0x06000084 RID: 132 RVA: 0x0000F40C File Offset: 0x0000D60C
    public virtual void emitBloodLight(float colZPos)
    {
        if (!this.root.cinematicShot && !this.root.showNoBlood)
        {
            this.bloodMistParticle.Emit(this.root.generateEmitParams(this.particleEmitPoint, new Vector3(-this.transform.forward.x * 1.5f, -this.transform.forward.y * 1.5f, UnityEngine.Random.Range(-0.5f, 0.5f)), (float)UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(0.3f, 1f), (!this.root.doGore) ? new Color((float)0, (float)0, (float)0, 0.5f) : new Color((float)1, (float)1, (float)1, 0.5f)), 1);
            for (int i = 0; i < 5; i++)
            {
                this.bloodParticle.Emit(this.root.generateEmitParams(this.particleEmitPoint, new Vector3(-this.transform.forward.x * 3.5f + (float)UnityEngine.Random.Range(-4, 4), -this.transform.forward.y * 3.5f + (float)UnityEngine.Random.Range(1, 6), UnityEngine.Random.Range(-0.5f, 0.5f)), UnityEngine.Random.Range(0.05f, 0.2f), UnityEngine.Random.Range(0.8f, 1.3f), (!this.root.doGore) ? new Color((float)0, (float)0, (float)0, (float)1) : new Color((float)1, (float)1, (float)1, (float)1)), 1);
            }
        }
    }

    // Token: 0x06000085 RID: 133 RVA: 0x000020A7 File Offset: 0x000002A7
    public virtual void Main()
    {
    }

    // Token: 0x04000154 RID: 340
    private RootScript root;

    // Token: 0x04000155 RID: 341
    private RootSharedScript rootShared;

    // Token: 0x04000156 RID: 342
    private BoxCollider theBoxCollider;

    // Token: 0x04000157 RID: 343
    private Vector3 theBoxColliderStartSize;

    // Token: 0x04000158 RID: 344
    private SphereCollider theSphereCollider;

    // Token: 0x04000159 RID: 345
    public Collider lastIgnoredCollider;

    // Token: 0x0400015A RID: 346
    public Collider lastIgnoredCollider2;

    // Token: 0x0400015B RID: 347
    private BulletScript thisBulletScript;

    // Token: 0x0400015C RID: 348
    private Transform bulletAudioTransform;

    // Token: 0x0400015D RID: 349
    private AudioSource bulletAudio;

    // Token: 0x0400015E RID: 350
    private AudioClip bulletAudioStartClip;

    // Token: 0x0400015F RID: 351
    private float bulletAudioStartVol;

    // Token: 0x04000160 RID: 352
    private float bulletAudioStartPitch;

    // Token: 0x04000161 RID: 353
    private float bulletAudioStartDoppler;

    // Token: 0x04000162 RID: 354
    private ParticleSystem bulletHitParticle;

    // Token: 0x04000163 RID: 355
    private ParticleSystem smokeSpriteParticle;

    // Token: 0x04000164 RID: 356
    private ParticleSystem smokeParticle;

    // Token: 0x04000165 RID: 357
    private ParticleSystem sparkParticle;

    // Token: 0x04000166 RID: 358
    private ParticleSystem destroyParticleGlass;

    // Token: 0x04000167 RID: 359
    private ParticleSystem bloodParticle;

    // Token: 0x04000168 RID: 360
    private Vector3 orgPos;

    // Token: 0x04000169 RID: 361
    private Transform playerTransform;

    // Token: 0x0400016A RID: 362
    private float destroyTimer;

    // Token: 0x0400016B RID: 363
    private Rigidbody rigidBody;

    // Token: 0x0400016C RID: 364
    public bool bulletHasHit;

    // Token: 0x0400016D RID: 365
    private CameraScript cameraScript;

    // Token: 0x0400016E RID: 366
    private bool allowScreenShake;

    // Token: 0x0400016F RID: 367
    private ParticleSystem glassParticle;

    // Token: 0x04000170 RID: 368
    private bool playedSound;

    // Token: 0x04000171 RID: 369
    public float bulletSpeed;

    // Token: 0x04000172 RID: 370
    public float bulletStrength;

    // Token: 0x04000173 RID: 371
    public bool friendly;

    // Token: 0x04000174 RID: 372
    public bool allowGib;

    // Token: 0x04000175 RID: 373
    public bool knockBack;

    // Token: 0x04000176 RID: 374
    public bool bounced;

    // Token: 0x04000177 RID: 375
    public bool bulletKillOnHeadshot;

    // Token: 0x04000178 RID: 376
    public float bulletLength;

    // Token: 0x04000179 RID: 377
    public float timeAlive;

    // Token: 0x0400017A RID: 378
    public GameObject bloodSplatter;

    // Token: 0x0400017B RID: 379
    public GameObject bloodSplatterStatic;

    // Token: 0x0400017C RID: 380
    public GameObject[] bloodSplatterAnimated;

    // Token: 0x0400017D RID: 381
    private ParticleSystem bloodMistParticle;

    // Token: 0x0400017E RID: 382
    public Vector3 particleEmitPoint;

    // Token: 0x0400017F RID: 383
    public bool isExplosion;

    // Token: 0x04000180 RID: 384
    private LayerMask layerMask;

    // Token: 0x04000181 RID: 385
    private LayerMask layerMaskMinusPlayer;

    // Token: 0x04000182 RID: 386
    private LayerMask layerMask2;

    // Token: 0x04000183 RID: 387
    private LayerMask layerMask3;

    // Token: 0x04000184 RID: 388
    private LayerMask layerMask4;

    // Token: 0x04000185 RID: 389
    private LayerMask layerMask5;

    // Token: 0x04000186 RID: 390
    private Vector3 upcomingBulletHitPos;

    // Token: 0x04000187 RID: 391
    private Transform ignoreTransform;

    // Token: 0x04000188 RID: 392
    private bool gaveDodgeScore;

    // Token: 0x04000189 RID: 393
    public Transform fromTransform;

    // Token: 0x0400018A RID: 394
    public bool turretBullet;

    // Token: 0x0400018B RID: 395
    public float tailCheck;

    // Token: 0x0400018C RID: 396
    public bool midAirShot;

    // Token: 0x0400018D RID: 397
    public bool wallJumpShot;

    // Token: 0x0400018E RID: 398
    public bool enemyJumpShot;

    // Token: 0x0400018F RID: 399
    public float playerAngularVelocityShot;

    // Token: 0x04000190 RID: 400
    public bool splitShot;

    // Token: 0x04000191 RID: 401
    public bool dodgeShot;

    // Token: 0x04000192 RID: 402
    public bool swingShot;

    // Token: 0x04000193 RID: 403
    public bool slideShot;

    // Token: 0x04000194 RID: 404
    public bool dramaticEntranceShot;

    // Token: 0x04000195 RID: 405
    public bool slowMoShot;

    // Token: 0x04000196 RID: 406
    public int nrOfBounces;

    // Token: 0x04000197 RID: 407
    private bool bouncedOffFryingPan;

    // Token: 0x04000198 RID: 408
    private float destroyTimerMultiplier;
}
