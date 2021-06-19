using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[Serializable]
public class SkateboardScript : MonoBehaviour
{

    private BaseNetworkEntity networkHelper;

    // Token: 0x04001099 RID: 4249
    private RootScript root;

    // Token: 0x0400109A RID: 4250
    private PlayerScript playerScript;

    // Token: 0x0400109B RID: 4251
    private Rigidbody playerRBody;

    // Token: 0x0400109C RID: 4252
    private Rigidbody rBody;

    // Token: 0x0400109D RID: 4253
    private Vector3 prevPos;

    // Token: 0x0400109E RID: 4254
    private float extraGravityCyclesCounter;

    // Token: 0x0400109F RID: 4255
    private bool playerOn;

    // Token: 0x040010A0 RID: 4256
    private bool playerOnDoOnce;

    // Token: 0x040010A1 RID: 4257
    private bool isJumping;

    // Token: 0x040010A2 RID: 4258
    private bool onGround;

    // Token: 0x040010A3 RID: 4259
    private bool hasJumpedButNotTouchedGround;

    // Token: 0x040010A4 RID: 4260
    private LayerMask layerMask;

    // Token: 0x040010A5 RID: 4261
    private Animator playerAnimator;

    // Token: 0x040010A6 RID: 4262
    private ObjectKickScript kickScript;

    // Token: 0x040010A7 RID: 4263
    private float pushTimer;

    // Token: 0x040010A8 RID: 4264
    private bool boardFacingUp;

    // Token: 0x040010A9 RID: 4265
    private GameObject shine;

    // Token: 0x040010AA RID: 4266
    private bool shineDoOnce;

    // Token: 0x040010AB RID: 4267
    private AudioSource rollAudioSource;

    // Token: 0x040010AC RID: 4268
    private AudioSource theAudioSource;

    // Token: 0x040010AD RID: 4269
    public AudioClip ollieSound;

    // Token: 0x040010AE RID: 4270
    public AudioClip backWheelLandSound;

    // Token: 0x040010AF RID: 4271
    public AudioClip frontWheelLandSound;

    // Token: 0x040010B0 RID: 4272
    public AudioClip spinSound;

    // Token: 0x040010B1 RID: 4273
    private bool wheel1OnGroundDoOnce;

    // Token: 0x040010B2 RID: 4274
    private bool wheel2OnGroundDoOnce;

    // Token: 0x040010B3 RID: 4275
    private bool playerScriptDodgingDoOnce;

    // Token: 0x040010B4 RID: 4276
    private CameraScript cameraScript;

    // Token: 0x040010B5 RID: 4277
    private bool fireWeapon;

    // Token: 0x040010B6 RID: 4278
    private bool kJump;

    // Token: 0x040010B7 RID: 4279
    private bool kJumpHeldDown;

    // Token: 0x040010B8 RID: 4280
    private float extraJumpPower;

    // Token: 0x040010B9 RID: 4281
    private bool doShowHintFlipSkateboard;

    // Token: 0x040010BA RID: 4282
    private Vector3 prevPosS;

    // Token: 0x040010BB RID: 4283
    private float extraGravityCyclesCounterS;

    // Token: 0x040010BC RID: 4284
    private bool playerOnS;

    // Token: 0x040010BD RID: 4285
    private bool playerOnDoOnceS;

    // Token: 0x040010BE RID: 4286
    private bool isJumpingS;

    // Token: 0x040010BF RID: 4287
    private bool onGroundS;

    // Token: 0x040010C0 RID: 4288
    private bool hasJumpedButNotTouchedGroundS;

    // Token: 0x040010C1 RID: 4289
    private float pushTimerS;

    // Token: 0x040010C2 RID: 4290
    private bool boardFacingUpS;

    // Token: 0x040010C3 RID: 4291
    private bool shineDoOnceS;

    // Token: 0x040010C4 RID: 4292
    private bool wheel1OnGroundDoOnceS;

    // Token: 0x040010C5 RID: 4293
    private bool wheel2OnGroundDoOnceS;

    // Token: 0x040010C6 RID: 4294
    private bool playerScriptDodgingDoOnceS;

    // Token: 0x040010C7 RID: 4295
    private bool fireWeaponS;

    // Token: 0x040010C8 RID: 4296
    private bool kJumpS;

    // Token: 0x040010C9 RID: 4297
    private bool kJumpHeldDownS;

    // Token: 0x040010CA RID: 4298
    private float extraJumpPowerS;

    // Token: 0x040010CB RID: 4299
    private bool doShowHintFlipSkateboardS;

    // Token: 0x0600052A RID: 1322 RVA: 0x000020A9 File Offset: 0x000002A9
    public SkateboardScript()
    {
    }

    // Token: 0x0600052B RID: 1323 RVA: 0x0008E3C0 File Offset: 0x0008C5C0
    public virtual void saveState()
    {
        this.prevPosS = this.prevPos;
        this.extraGravityCyclesCounterS = this.extraGravityCyclesCounter;
        this.playerOnS = this.playerOn;
        this.playerOnDoOnceS = this.playerOnDoOnce;
        this.isJumpingS = this.isJumping;
        this.onGroundS = this.onGround;
        this.hasJumpedButNotTouchedGroundS = this.hasJumpedButNotTouchedGround;
        this.pushTimerS = this.pushTimer;
        this.boardFacingUpS = this.boardFacingUp;
        this.shineDoOnceS = this.shineDoOnce;
        this.wheel1OnGroundDoOnceS = this.wheel1OnGroundDoOnce;
        this.wheel2OnGroundDoOnceS = this.wheel2OnGroundDoOnce;
        this.playerScriptDodgingDoOnceS = this.playerScriptDodgingDoOnce;
        this.fireWeaponS = this.fireWeapon;
        this.kJumpS = this.kJump;
        this.kJumpHeldDownS = this.kJumpHeldDown;
        this.extraJumpPowerS = this.extraJumpPower;
        this.doShowHintFlipSkateboardS = this.doShowHintFlipSkateboard;
    }

    // Token: 0x0600052C RID: 1324 RVA: 0x0008E4A8 File Offset: 0x0008C6A8
    public virtual void loadState()
    {
        this.prevPos = this.prevPosS;
        this.extraGravityCyclesCounter = this.extraGravityCyclesCounterS;
        this.playerOn = this.playerOnS;
        this.playerOnDoOnce = this.playerOnDoOnceS;
        this.isJumping = this.isJumpingS;
        this.onGround = this.onGroundS;
        this.hasJumpedButNotTouchedGround = this.hasJumpedButNotTouchedGroundS;
        this.pushTimer = this.pushTimerS;
        this.boardFacingUp = this.boardFacingUpS;
        this.shineDoOnce = this.shineDoOnceS;
        this.wheel1OnGroundDoOnce = this.wheel1OnGroundDoOnceS;
        this.wheel2OnGroundDoOnce = this.wheel2OnGroundDoOnceS;
        this.playerScriptDodgingDoOnce = this.playerScriptDodgingDoOnceS;
        this.fireWeapon = this.fireWeaponS;
        this.kJump = this.kJumpS;
        this.kJumpHeldDown = this.kJumpHeldDownS;
        this.extraJumpPower = this.extraJumpPowerS;
        this.doShowHintFlipSkateboard = this.doShowHintFlipSkateboardS;
    }

    // Token: 0x0600052D RID: 1325 RVA: 0x00003E73 File Offset: 0x00002073
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

    // Token: 0x0600052E RID: 1326 RVA: 0x0008E590 File Offset: 0x0008C790
    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.playerScript = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
        this.playerRBody = (Rigidbody)this.playerScript.transform.GetComponent(typeof(Rigidbody));
        this.rBody = (Rigidbody)this.GetComponent(typeof(Rigidbody));
        this.rBody.maxAngularVelocity = (float)9;
        this.layerMask = 33024;
        this.playerAnimator = (Animator)this.playerScript.transform.Find("PlayerGraphics").GetComponent(typeof(Animator));
        this.kickScript = (ObjectKickScript)this.GetComponent(typeof(ObjectKickScript));
        this.shine = this.transform.Find("Skateboard_Shine").gameObject;
        this.rollAudioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.rollAudioSource.volume = (float)0;
        this.theAudioSource = (AudioSource)this.transform.Find("Sounds").GetComponent(typeof(AudioSource));
        this.cameraScript = (CameraScript)GameObject.Find("Main Camera").GetComponent(typeof(CameraScript));
    }

    // Token: 0x0600052F RID: 1327 RVA: 0x0008E720 File Offset: 0x0008C920
    public virtual void Update()
    {
        if (networkHelper == null)
            if (GetComponent<BaseNetworkEntity>())
                networkHelper = gameObject.AddComponent<BaseNetworkEntity>();

        if (!this.fireWeapon && this.playerScript.fireWeapon)
        {
            this.fireWeapon = true;
        }
        if (!this.kJump && this.playerScript.kJump)
        {
            this.kJump = true;
        }
        if (!this.kJumpHeldDown && (this.playerScript.kJumpHeldDown || this.playerScript.floatJump))
        {
            this.kJumpHeldDown = true;
        }
        if (!this.playerOn && this.playerScript.onGround && this.playerScript.groundTransform == this.transform)
        {
            networkHelper.ignoreMaxPacketsDoOnce = true;
            PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });
            this.playerOn = true;
        }
        if (!this.isJumping && this.onGround && !this.boardFacingUp && Vector3.Distance(this.playerScript.transform.position + Vector3.up * -((float)2 - this.playerScript.crouchAmount * 1.5f), this.transform.position) < 1.5f)
        {
            this.root.highlightObject(this.transform, false, this.root.GetTranslation("interact3"), 0.8f);
            this.root.showHintFlipSkateboard = true;
            if (!this.isJumping && this.kJump && this.playerScript.onGround && !this.playerScript.kCrouch)
            {
                this.doJump();
            }
        }
        if (!this.root.doCheckpointSave && this.playerOn && this.boardFacingUp && !this.isJumping)
        {
            this.playerScript.transform.position = this.playerScript.transform.position + (this.transform.position - this.prevPos);
        }
        this.prevPos = this.transform.position;
    }

    // Token: 0x06000530 RID: 1328 RVA: 0x0008E93C File Offset: 0x0008CB3C
    public virtual void doJump()
    {
        if (this.onGround)
        {
            int num = 2;
            Vector3 velocity = this.rBody.velocity;
            float num2 = velocity.y = (float)num;
            Vector3 vector = this.rBody.velocity = velocity;
            this.theAudioSource.clip = this.ollieSound;
            this.theAudioSource.volume = UnityEngine.Random.Range(0.8f, (float)1);
            this.theAudioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            this.theAudioSource.Play();
        }
        else
        {
            this.playerScript.extraJumpPower = (float)0;
        }
        float z = this.rBody.angularVelocity.z + (this.transform.position.x - this.playerScript.transform.position.x) * (float)-10 * this.root.fixedTimescale;
        Vector3 angularVelocity = this.rBody.angularVelocity;
        float num3 = angularVelocity.z = z;
        Vector3 vector2 = this.rBody.angularVelocity = angularVelocity;
        if (Mathf.Abs(this.rBody.velocity.x) > 1.5f)
        {
            this.playerScript.xSpeed = this.rBody.velocity.x * 0.7f;
            this.playerScript.justWallJumped = true;
            this.playerScript.justJumpedFromSkateboard = true;
            this.playerScript.wallJumpTimer = (float)60;
        }
        this.extraJumpPower = 1.75f;
        if (this.boardFacingUp)
        {
            this.isJumping = true;
        }
        else
        {
            float z2 = this.rBody.angularVelocity.z + (float)10;
            Vector3 angularVelocity2 = this.rBody.angularVelocity;
            float num4 = angularVelocity2.z = z2;
            Vector3 vector3 = this.rBody.angularVelocity = angularVelocity2;
            int num5 = 16;
            Vector3 velocity2 = this.rBody.velocity;
            float num6 = velocity2.y = (float)num5;
            Vector3 vector4 = this.rBody.velocity = velocity2;
            this.isJumping = false;
        }
        this.hasJumpedButNotTouchedGround = true;
    }

    // Token: 0x06000531 RID: 1329 RVA: 0x0008EB90 File Offset: 0x0008CD90
    public virtual void skatePush()
    {
        float num = Mathf.Abs(this.rBody.velocity.x);
        if (num < (float)24)
        {
            float num2 = ((float)24 - num) * 0.4f;
            float x = this.rBody.velocity.x + ((this.playerScript.kXDir <= (float)0) ? (-num2) : num2) / (float)2;
            Vector3 velocity = this.rBody.velocity;
            float num3 = velocity.x = x;
            Vector3 vector = this.rBody.velocity = velocity;
            this.rBody.velocity = this.rBody.velocity + this.transform.right * ((this.playerScript.kXDir <= (float)0) ? (-num2) : num2) / (float)2 * (float)((this.transform.right.x <= (float)0) ? -1 : 1);
        }
        float num4 = Mathf.Clamp01(Mathf.Abs(this.rBody.velocity.x / (float)30));
        this.playerAnimator.SetFloat("SkateSpeed", 0.5f + num4 * 1.5f);
        this.playerAnimator.CrossFadeInFixedTime(((!this.playerScript.faceRight || this.playerScript.kXDir < (float)0) && (this.playerScript.faceRight || this.playerScript.kXDir > (float)0)) ? "SkateBackwards" : "Skate", 0.05f - num4 * 0.04f, 0, 0.2f * num4);
        if (this.playerOn)
        {
            this.root.rumble((this.rBody.velocity.x <= (float)0) ? 0 : 1, 0.2f, 0.1f);
        }
        this.pushTimer = (float)16 + num4 * (float)22;
    }

    // Token: 0x06000532 RID: 1330 RVA: 0x0008EDAC File Offset: 0x0008CFAC
    public virtual void FixedUpdate()
    {
        if (!this.root.doCheckpointSave)
        {
            bool flag = default(bool);
            RaycastHit raycastHit = default(RaycastHit);
            bool flag2 = Physics.Raycast(this.transform.position - this.transform.right * 0.8f + this.transform.forward * 0.15f, Vector3.down, out raycastHit, 0.7f, this.layerMask);
            if (raycastHit.collider != null && raycastHit.collider.tag == "SkateRamp")
            {
                flag = true;
            }
            bool flag3 = Physics.Raycast(this.transform.position + this.transform.right * 0.8f + this.transform.forward * 0.15f, Vector3.down, out raycastHit, 0.7f, this.layerMask);
            if (!flag && raycastHit.collider != null && raycastHit.collider.tag == "SkateRamp")
            {
                flag = true;
            }
            this.onGround = (flag2 || flag3);
            if (flag2)
            {
                if (!this.wheel1OnGroundDoOnce)
                {
                    this.theAudioSource.clip = this.backWheelLandSound;
                    this.theAudioSource.volume = -this.rBody.velocity.y / (float)20 + UnityEngine.Random.Range((float)0, 0.2f);
                    this.theAudioSource.pitch = Mathf.Clamp(-this.rBody.velocity.y / (float)10 + 0.2f + UnityEngine.Random.Range(-0.1f, 0.1f), 0.7f, 1.5f);
                    this.theAudioSource.Play();
                    this.wheel1OnGroundDoOnce = true;
                }
            }
            else if (this.wheel1OnGroundDoOnce)
            {
                this.wheel1OnGroundDoOnce = false;
            }
            if (flag3)
            {
                if (!this.wheel2OnGroundDoOnce)
                {
                    this.theAudioSource.clip = this.frontWheelLandSound;
                    this.theAudioSource.volume = -this.rBody.velocity.y / (float)20 + UnityEngine.Random.Range((float)0, 0.2f);
                    this.theAudioSource.pitch = Mathf.Clamp(-this.rBody.velocity.y / (float)10 + 0.2f + UnityEngine.Random.Range(-0.1f, 0.1f), 0.7f, 1.5f);
                    this.theAudioSource.Play();
                    this.wheel2OnGroundDoOnce = true;
                }
            }
            else if (this.wheel2OnGroundDoOnce)
            {
                this.wheel2OnGroundDoOnce = false;
            }
            float magnitude = this.rBody.velocity.magnitude;
            if (this.onGround)
            {
                this.gameObject.layer = 25;
                this.kickScript.enabled = false;
                this.rollAudioSource.volume = this.root.DampFixed(magnitude / (float)20, this.rollAudioSource.volume, 0.2f);
                this.rollAudioSource.pitch = this.root.DampFixed(0.8f + magnitude / (float)25, this.rollAudioSource.pitch, 0.3f);
            }
            else
            {
                this.gameObject.layer = 14;
                this.kickScript.enabled = true;
                this.rollAudioSource.volume = this.root.DampFixed((float)0, this.rollAudioSource.volume, 0.2f);
                this.rollAudioSource.pitch = this.root.DampFixed(1.5f + this.rBody.velocity.x / (float)30, this.rollAudioSource.pitch, 0.5f);
            }
            this.boardFacingUp = (this.transform.forward.y > (float)0);
            if (!this.playerOn && this.onGround && this.rBody.velocity.magnitude < (float)1)
            {
                if (!this.shineDoOnce && !this.root.trailerMode)
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
            if (this.playerOn)
            {
                if (!this.playerOnDoOnce)
                {
                    if (this.boardFacingUp && this.onGround)
                    {
                        float x = Mathf.Clamp(this.rBody.velocity.x + this.playerRBody.velocity.x, (float)-10, (float)10);
                        Vector3 velocity = this.rBody.velocity;
                        float num = velocity.x = x;
                        Vector3 vector = this.rBody.velocity = velocity;
                        float x2 = this.playerRBody.velocity.x * 0.25f;
                        Vector3 velocity2 = this.playerRBody.velocity;
                        float num2 = velocity2.x = x2;
                        Vector3 vector2 = this.playerRBody.velocity = velocity2;
                        this.playerScript.xSpeed = (float)0;
                    }
                    this.pushTimer = (float)0;
                    this.playerOnDoOnce = true;
                }
                if (this.boardFacingUp)
                {
                    if (this.playerScript.crouchAmount < 0.4f && this.playerScript.kXDir != (float)0)
                    {
                        this.pushTimer -= this.root.fixedTimescale;
                        this.playerScript.xSpeed = (float)0;
                        if ((this.playerScript.kXDir < (float)0 && this.rBody.velocity.x < (float)0) || (this.playerScript.kXDir > (float)0 && this.rBody.velocity.x > (float)0))
                        {
                            if (this.pushTimer <= (float)0)
                            {
                                this.skatePush();
                            }
                            if (Mathf.Abs(this.rBody.velocity.x) < (float)8)
                            {
                                float x3 = this.rBody.velocity.x + ((this.playerScript.kXDir <= (float)0) ? -0.4f : 0.4f) * this.root.fixedTimescale;
                                Vector3 velocity3 = this.rBody.velocity;
                                float num3 = velocity3.x = x3;
                                Vector3 vector3 = this.rBody.velocity = velocity3;
                            }
                        }
                        else
                        {
                            float x4 = this.rBody.velocity.x + this.playerScript.kXDir * 0.4f * this.root.fixedTimescale;
                            Vector3 velocity4 = this.rBody.velocity;
                            float num4 = velocity4.x = x4;
                            Vector3 vector4 = this.rBody.velocity = velocity4;
                        }
                    }
                    this.playerScript.xSpeed = this.playerScript.xSpeed * Mathf.Pow((float)1 - 0.3f * Mathf.Clamp01(Mathf.Abs(this.rBody.velocity.x) / (float)3), this.root.fixedTimescale);
                    if (this.fireWeapon)
                    {
                        float x5 = (this.playerScript.transform.position - this.playerScript.mousePos).normalized.x;
                        if (Mathf.Abs(this.rBody.velocity.x) < (float)1 || (this.rBody.velocity.x > (float)0 && x5 > (float)0) || (this.rBody.velocity.x < (float)0 && x5 < (float)0))
                        {
                            float x6 = this.rBody.velocity.x + x5 * ((this.playerScript.weapon != 6) ? ((this.playerScript.weapon != 5) ? 0.5f : ((float)1)) : ((float)3));
                            Vector3 velocity5 = this.rBody.velocity;
                            float num5 = velocity5.x = x6;
                            Vector3 vector5 = this.rBody.velocity = velocity5;
                        }
                    }
                    if ((flag2 || flag3) && !this.isJumping)
                    {
                        this.rBody.velocity = this.rBody.velocity + -this.transform.forward * Mathf.Abs(this.rBody.velocity.x * 0.05f) * this.root.fixedTimescale;
                    }
                    if (this.onGround && flag && Mathf.Abs(this.rBody.velocity.x) > (float)8)
                    {
                        this.rBody.velocity = this.rBody.velocity + this.transform.right * ((this.rBody.velocity.x <= (float)0) ? -0.15f : 0.15f) * (float)((this.transform.right.x <= (float)0) ? -1 : 1) * this.root.fixedTimescale;
                    }
                }
                else
                {
                    this.root.highlightObject(this.transform, false, this.root.GetTranslation("interact3"), 0.8f);
                    this.root.showHintFlipSkateboard = true;
                }
                if (!this.isJumping)
                {
                    this.playerScript.transform.position = this.playerScript.transform.position + (this.transform.position - this.prevPos);
                }
                if (this.playerScript.dodging)
                {
                    float y = this.root.DampFixed((float)-8, this.rBody.angularVelocity.y, 0.2f);
                    Vector3 angularVelocity = this.rBody.angularVelocity;
                    float num6 = angularVelocity.y = y;
                    Vector3 vector6 = this.rBody.angularVelocity = angularVelocity;
                    float x7 = this.root.DampFixed(this.transform.position.x, this.playerScript.transform.position.x, 0.1f);
                    Vector3 position = this.playerScript.transform.position;
                    float num7 = position.x = x7;
                    Vector3 vector7 = this.playerScript.transform.position = position;
                    if (!this.playerScriptDodgingDoOnce)
                    {
                        this.theAudioSource.clip = this.spinSound;
                        this.theAudioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                        this.theAudioSource.volume = this.rollAudioSource.volume;
                        this.theAudioSource.Play();
                        this.playerScriptDodgingDoOnce = true;
                    }
                }
                else
                {
                    if (!this.isJumping && this.boardFacingUp)
                    {
                        float num8 = this.transform.position.x - this.playerScript.transform.position.x;
                        if (Mathf.Abs(num8) > 0.7f || !flag2 || !flag3)
                        {
                            float z = this.rBody.angularVelocity.z + num8 * 1.1f * this.root.fixedTimescale;
                            Vector3 angularVelocity2 = this.rBody.angularVelocity;
                            float num9 = angularVelocity2.z = z;
                            Vector3 vector8 = this.rBody.angularVelocity = angularVelocity2;
                        }
                    }
                    if (this.playerScriptDodgingDoOnce)
                    {
                        this.playerScriptDodgingDoOnce = false;
                    }
                }
                if (!this.isJumping && this.kJump && !this.playerScript.kCrouch)
                {
                    this.doJump();
                }
                this.cameraScript.externalCameraOffset.x = this.rBody.velocity.x;
            }
            else
            {
                if (this.rBody.velocity.y <= (float)0)
                {
                    float y2 = this.rBody.angularVelocity.y * Mathf.Pow(0.95f, this.root.fixedTimescale);
                    Vector3 angularVelocity3 = this.rBody.angularVelocity;
                    float num10 = angularVelocity3.y = y2;
                    Vector3 vector9 = this.rBody.angularVelocity = angularVelocity3;
                }
                if (!this.isJumping)
                {
                    if (this.onGround)
                    {
                        if (Mathf.Abs(this.transform.up.y) >= 0.85f)
                        {
                            this.rBody.angularVelocity = this.rBody.angularVelocity + this.transform.right * (float)5 * this.root.fixedTimescale;
                        }
                        if (this.boardFacingUp)
                        {
                            float x8 = this.rBody.velocity.x * Mathf.Pow(Mathf.Clamp01(0.9f + Mathf.Abs(this.transform.right.x) * 0.1f), this.root.fixedTimescale);
                            Vector3 velocity6 = this.rBody.velocity;
                            float num11 = velocity6.x = x8;
                            Vector3 vector10 = this.rBody.velocity = velocity6;
                        }
                    }
                    else
                    {
                        this.rBody.angularVelocity = this.rBody.angularVelocity + this.transform.right * -this.transform.up.y * (float)10 * Mathf.Clamp01(-this.rBody.velocity.y) * this.root.fixedTimescale;
                    }
                }
                if (this.playerOnDoOnce)
                {
                    if (!this.isJumping)
                    {
                        this.playerScript.xSpeed = this.rBody.velocity.x * 1.2f;
                        if (flag2 && flag3)
                        {
                            float x9 = Mathf.Clamp(this.rBody.velocity.x * 0.75f, (float)-5, (float)5);
                            Vector3 velocity7 = this.rBody.velocity;
                            float num12 = velocity7.x = x9;
                            Vector3 vector11 = this.rBody.velocity = velocity7;
                        }
                    }
                    this.playerOnDoOnce = false;
                }
            }
            if ((this.playerOn || flag) && !this.isJumping && !this.playerScript.dodging)
            {
                float num13 = Mathf.Clamp(Mathf.Floor(this.transform.right.x * (float)100), (float)-1, (float)1);
                if (num13 <= 0.1f && num13 >= (float)0)
                {
                    num13 = (float)1;
                }
                else if (num13 >= -0.1f && num13 <= (float)0)
                {
                    num13 = (float)-1;
                }
                this.rBody.angularVelocity = this.rBody.angularVelocity + this.transform.forward * ((float)0 - this.transform.right.z) * Mathf.Clamp01(Mathf.Abs(this.rBody.velocity.x) * 0.1f * this.root.fixedTimescale) * (float)-10 * num13;
                float y3 = this.rBody.angularVelocity.y * Mathf.Pow(0.8f, this.root.fixedTimescale);
                Vector3 angularVelocity4 = this.rBody.angularVelocity;
                float num14 = angularVelocity4.y = y3;
                Vector3 vector12 = this.rBody.angularVelocity = angularVelocity4;
            }
            if (this.isJumping)
            {
                float y4 = this.rBody.velocity.y + 1.35f * this.extraJumpPower * this.root.fixedTimescale;
                Vector3 velocity8 = this.rBody.velocity;
                float num15 = velocity8.y = y4;
                Vector3 vector13 = this.rBody.velocity = velocity8;
                float x10 = this.rBody.angularVelocity.x + ((float)5 + (float)7 * this.extraJumpPower) * this.root.fixedTimescale;
                Vector3 angularVelocity5 = this.rBody.angularVelocity;
                float num16 = angularVelocity5.x = x10;
                Vector3 vector14 = this.rBody.angularVelocity = angularVelocity5;
                float x11 = this.transform.localRotation.eulerAngles.x + (float)4 * this.extraJumpPower * this.root.fixedTimescale;
                Quaternion localRotation = this.transform.localRotation;
                Vector3 eulerAngles = localRotation.eulerAngles;
                float num17 = eulerAngles.x = x11;
                Vector3 vector15 = localRotation.eulerAngles = eulerAngles;
                Quaternion quaternion = this.transform.localRotation = localRotation;
                this.extraJumpPower -= 0.17f * this.root.fixedTimescale;
                if (this.extraJumpPower <= (float)0 || !this.kJumpHeldDown)
                {
                    this.isJumping = false;
                }
            }
            int num18 = 0;
            Vector3 position2 = this.transform.position;
            float num19 = position2.z = (float)num18;
            Vector3 vector16 = this.transform.position = position2;
            this.prevPos = this.transform.position;
            if (this.rBody.velocity.sqrMagnitude > 0.1f)
            {
                this.extraGravityCyclesCounter = (float)10;
            }
            if (this.extraGravityCyclesCounter > (float)0)
            {
                this.extraGravityCyclesCounter -= (float)1;
                this.rBody.AddForce((float)25 * Physics.gravity);
            }
            if (this.playerOn && this.boardFacingUp && !this.playerScript.startedRolling)
            {
                float x12 = this.root.DampFixed(this.transform.position.x, this.playerScript.transform.position.x, 0.3f);
                Vector3 position3 = this.playerScript.transform.position;
                float num20 = position3.x = x12;
                Vector3 vector17 = this.playerScript.transform.position = position3;
            }
            if (this.fireWeapon && !this.playerScript.fireWeapon)
            {
                this.fireWeapon = false;
            }
            if (this.kJump && !this.playerScript.kJump)
            {
                this.kJump = false;
            }
            if (this.kJumpHeldDown && !this.playerScript.kJumpHeldDown && !this.playerScript.floatJump)
            {
                this.kJumpHeldDown = false;
            }
            if (this.playerOn && this.playerScript.groundTransform != this.transform)
            {
                networkHelper.ignoreMaxPacketsDoOnce = true;
                PacketSender.BaseNetworkedEntityRPC("OnPlayerStopInteract", networkHelper.entityIdentifier);
                this.playerOn = false;
            }
        }
    }

    // Token: 0x06000533 RID: 1331 RVA: 0x00090240 File Offset: 0x0008E440
    public virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 9 && !this.playerOn)
        {
            BulletScript bulletScript = (BulletScript)col.gameObject.GetComponent(typeof(BulletScript));
            if (bulletScript.friendly)
            {
                RaycastHit raycastHit = default(RaycastHit);
                if (Physics.Raycast(this.transform.position, Vector3.down, out raycastHit, (float)10, this.layerMask))
                {
                    float y = (float)10 - raycastHit.distance;
                    Vector3 velocity = this.rBody.velocity;
                    float num = velocity.y = y;
                    Vector3 vector = this.rBody.velocity = velocity;
                }
                float z = this.rBody.angularVelocity.z + (this.transform.position.x - col.transform.position.x) * (float)-8 * this.root.fixedTimescale;
                Vector3 angularVelocity = this.rBody.angularVelocity;
                float num2 = angularVelocity.z = z;
                Vector3 vector2 = this.rBody.angularVelocity = angularVelocity;
                this.rBody.angularVelocity = this.rBody.angularVelocity + this.transform.right * (float)15 * this.root.fixedTimescale;
            }
        }
    }

    // Token: 0x06000534 RID: 1332 RVA: 0x000903C0 File Offset: 0x0008E5C0
    public virtual void OnTriggerEnter(Collider col)
    {
        EnemyScript enemyScript = null;
        float magnitude = this.rBody.velocity.magnitude;
        if (magnitude > (float)11 && col.transform.name == "Head" && col.gameObject.layer == 10)
        {
            enemyScript = (EnemyScript)col.transform.GetComponentInParent(typeof(EnemyScript));
            enemyScript.bulletHit = true;
            enemyScript.bulletStrength = magnitude;
            enemyScript.bulletHitName = "Head";
            enemyScript.bulletHitPos = this.transform.position;
            enemyScript.bulletHitRot = this.transform.rotation;
            enemyScript.bulletHitVel = this.rBody.velocity / 1.5f / (float)4;
            enemyScript.allowGib = false;
        }
        if (col.gameObject.tag == "Enemy")
        {
            if (enemyScript == null)
            {
                enemyScript = (EnemyScript)col.transform.GetComponentInParent(typeof(EnemyScript));
            }
            if (magnitude > (float)3)
            {
                enemyScript.knockBack(this.rBody.velocity.x > (float)0, (float)30);
            }
            enemyScript.idle = false;
        }
    }
}
