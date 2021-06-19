using System;
using Rewired;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x0200007B RID: 123
[Serializable]
public class MotorcycleScript : MonoBehaviour
{

    private NetworkedBaseTransform networkHelper;

    // Token: 0x04000886 RID: 2182
    [Header("Requires all on to spawn enemy in vehicle")]
    public SwitchScript[] inputSwitch;

    // Token: 0x04000887 RID: 2183
    private float switchInput;

    // Token: 0x04000888 RID: 2184
    public int enemiesToSpawn;

    // Token: 0x04000889 RID: 2185
    public GameObject enemy;

    // Token: 0x0400088A RID: 2186
    public bool isCar;

    // Token: 0x0400088B RID: 2187
    private int spawnedEnemies;

    // Token: 0x0400088C RID: 2188
    private float enemySpawnTimer;

    // Token: 0x0400088D RID: 2189
    private MeshCollider ramp;

    // Token: 0x0400088E RID: 2190
    private bool okToRamp;

    // Token: 0x0400088F RID: 2191
    [HideInInspector]
    public EnemyScript linkedEnemy;

    // Token: 0x04000890 RID: 2192
    public bool isEnemy;

    // Token: 0x04000891 RID: 2193
    public bool restrictMovement;

    // Token: 0x04000892 RID: 2194
    public Vector2 movementZone;

    // Token: 0x04000893 RID: 2195
    public Vector2 movementZoneSize;

    // Token: 0x04000894 RID: 2196
    public float speedMultiplier;

    // Token: 0x04000895 RID: 2197
    public bool dontGiveScore;

    // Token: 0x04000896 RID: 2198
    [Header("Enemy movement type")]
    public bool followX;

    // Token: 0x04000897 RID: 2199
    public bool followZ;

    // Token: 0x04000898 RID: 2200
    public bool pingPongX;

    // Token: 0x04000899 RID: 2201
    public bool pingPongZ;

    // Token: 0x0400089A RID: 2202
    private float pingPongDirectionX;

    // Token: 0x0400089B RID: 2203
    private float pingPongDirectionZ;

    // Token: 0x0400089C RID: 2204
    private Transform mainPlayer;

    // Token: 0x0400089D RID: 2205
    public bool alwaysStickBehindPlayer;

    // Token: 0x0400089E RID: 2206
    public float alwaysStickBehindPlayerDistance;

    // Token: 0x0400089F RID: 2207
    private RootScript root;

    // Token: 0x040008A0 RID: 2208
    [HideInInspector]
    public float xDir;

    // Token: 0x040008A1 RID: 2209
    private float yDir;

    // Token: 0x040008A2 RID: 2210
    private bool kJump;

    // Token: 0x040008A3 RID: 2211
    private bool kJumpHold;

    // Token: 0x040008A4 RID: 2212
    private bool kWheelie;

    // Token: 0x040008A5 RID: 2213
    private float xSpeed;

    // Token: 0x040008A6 RID: 2214
    private float targetXSpeed;

    // Token: 0x040008A7 RID: 2215
    private Rigidbody rBody;

    // Token: 0x040008A8 RID: 2216
    private float groundDistance;

    // Token: 0x040008A9 RID: 2217
    private LayerMask layerMask;

    // Token: 0x040008AA RID: 2218
    private Transform playerPos;

    // Token: 0x040008AB RID: 2219
    private Quaternion playerPosStartRot;

    // Token: 0x040008AC RID: 2220
    private Transform motorcycleGraphics;

    // Token: 0x040008AD RID: 2221
    [HideInInspector]
    public bool dead;

    // Token: 0x040008AE RID: 2222
    private int nrOfExplosions;

    // Token: 0x040008AF RID: 2223
    private bool enteredRestrictedZone;

    // Token: 0x040008B0 RID: 2224
    private PlayerScript playerScript;

    // Token: 0x040008B1 RID: 2225
    private Animator playerAnimator;

    // Token: 0x040008B2 RID: 2226
    private float jumpTimer;

    // Token: 0x040008B3 RID: 2227
    private ParticleSystem smokeParticle;

    // Token: 0x040008B4 RID: 2228
    private float particleEmitTimer;

    // Token: 0x040008B5 RID: 2229
    private ParticleSystem sparkParticle;

    // Token: 0x040008B6 RID: 2230
    private CameraScript cameraScript;

    // Token: 0x040008B7 RID: 2231
    private Transform backWheelHolder;

    // Token: 0x040008B8 RID: 2232
    private Vector3 backWheelHolderStartPos;

    // Token: 0x040008B9 RID: 2233
    private Transform frontWheelHolder;

    // Token: 0x040008BA RID: 2234
    private Vector3 frontWheelHolderStartPos;

    // Token: 0x040008BB RID: 2235
    private Transform backWheels;

    // Token: 0x040008BC RID: 2236
    private Transform frontWheels;

    // Token: 0x040008BD RID: 2237
    private Vector3 backWheelsStartPos;

    // Token: 0x040008BE RID: 2238
    private Vector3 frontWheelsStartPos;

    // Token: 0x040008BF RID: 2239
    private AudioSource motorAudioSource;

    // Token: 0x040008C0 RID: 2240
    private float audioMultiplier;

    // Token: 0x040008C1 RID: 2241
    private AudioSource breakAudioSource;

    // Token: 0x040008C2 RID: 2242
    private AudioSource jumpAudioSource;

    // Token: 0x040008C3 RID: 2243
    public AudioClip[] jumpSounds;

    // Token: 0x040008C4 RID: 2244
    private Player player;

    // Token: 0x040008C5 RID: 2245
    private Vector3 prevFixedPos;

    // Token: 0x040008C6 RID: 2246
    private Vector3 prevFixedPosS;

    // Token: 0x040008C7 RID: 2247
    private float switchInputS;

    // Token: 0x040008C8 RID: 2248
    private int spawnedEnemiesS;

    // Token: 0x040008C9 RID: 2249
    private float enemySpawnTimerS;

    // Token: 0x040008CA RID: 2250
    private bool okToRampS;

    // Token: 0x040008CB RID: 2251
    private EnemyScript linkedEnemyS;

    // Token: 0x040008CC RID: 2252
    private float pingPongDirectionXS;

    // Token: 0x040008CD RID: 2253
    private float pingPongDirectionZS;

    // Token: 0x040008CE RID: 2254
    private float xDirS;

    // Token: 0x040008CF RID: 2255
    private float yDirS;

    // Token: 0x040008D0 RID: 2256
    private bool kJumpS;

    // Token: 0x040008D1 RID: 2257
    private float xSpeedS;

    // Token: 0x040008D2 RID: 2258
    private float targetXSpeedS;

    // Token: 0x040008D3 RID: 2259
    private float groundDistanceS;

    // Token: 0x040008D4 RID: 2260
    private bool deadS;

    // Token: 0x040008D5 RID: 2261
    private int nrOfExplosionsS;

    // Token: 0x040008D6 RID: 2262
    private bool enteredRestrictedZoneS;

    // Token: 0x060002EC RID: 748 RVA: 0x00045F64 File Offset: 0x00044164
    public MotorcycleScript()
    {
        this.switchInput = (float)1;
        this.enemiesToSpawn = 2;
        this.okToRamp = true;
        this.isEnemy = true;
        this.speedMultiplier = 0.5f;
        this.followX = true;
        this.followZ = true;
        this.pingPongDirectionX = (float)1;
        this.pingPongDirectionZ = (float)1;
        this.alwaysStickBehindPlayerDistance = (float)10;
    }

    // Token: 0x060002ED RID: 749 RVA: 0x00045FC8 File Offset: 0x000441C8
    public virtual void saveState()
    {
        this.prevFixedPosS = this.prevFixedPos;
        this.switchInputS = this.switchInput;
        this.spawnedEnemiesS = this.spawnedEnemies;
        this.enemySpawnTimerS = this.enemySpawnTimer;
        this.okToRampS = this.okToRamp;
        this.linkedEnemyS = this.linkedEnemy;
        this.pingPongDirectionXS = this.pingPongDirectionX;
        this.pingPongDirectionZS = this.pingPongDirectionZ;
        this.xDirS = this.xDir;
        this.yDirS = this.yDir;
        this.kJumpS = this.kJump;
        this.xSpeedS = this.xSpeed;
        this.targetXSpeedS = this.targetXSpeed;
        this.groundDistanceS = this.groundDistance;
        this.deadS = this.dead;
        this.nrOfExplosionsS = this.nrOfExplosions;
        this.enteredRestrictedZoneS = this.enteredRestrictedZone;
    }

    // Token: 0x060002EE RID: 750 RVA: 0x000460A4 File Offset: 0x000442A4
    public virtual void loadState()
    {
        this.prevFixedPos = this.prevFixedPosS;
        this.switchInput = this.switchInputS;
        this.spawnedEnemies = this.spawnedEnemiesS;
        this.enemySpawnTimer = this.enemySpawnTimerS;
        this.okToRamp = this.okToRampS;
        this.linkedEnemy = this.linkedEnemyS;
        this.pingPongDirectionX = this.pingPongDirectionXS;
        this.pingPongDirectionZ = this.pingPongDirectionZS;
        this.xDir = this.xDirS;
        this.yDir = this.yDirS;
        this.kJump = this.kJumpS;
        this.xSpeed = this.xSpeedS;
        this.targetXSpeed = this.targetXSpeedS;
        this.groundDistance = this.groundDistanceS;
        this.dead = this.deadS;
        this.nrOfExplosions = this.nrOfExplosionsS;
        this.enteredRestrictedZone = this.enteredRestrictedZoneS;
    }

    // Token: 0x060002EF RID: 751 RVA: 0x000035AA File Offset: 0x000017AA
    public virtual void LateUpdate()
    {
        /*	if (this.root.doCheckpointSave)
            {
                this.saveState();
            }
            if (this.root.doCheckpointLoad)
            {
                this.loadState();
            }
            */
    }

    // Token: 0x060002F0 RID: 752 RVA: 0x000035D8 File Offset: 0x000017D8
    public virtual void Awake()
    {
        this.player = ReInput.players.GetPlayer(0);

        if (isEnemy || isCar)
        {
            networkHelper = gameObject.AddComponent<NetworkedBaseTransform>();
            networkHelper.dontDoDebug = true;
        }
    }

    // Token: 0x060002F1 RID: 753 RVA: 0x00046180 File Offset: 0x00044380
    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        if (this.enemy != null)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.enemy, this.transform.position, Quaternion.Euler((float)0, (float)0, (float)0));
            gameObject.transform.parent = this.transform;
            this.linkedEnemy = (EnemyScript)gameObject.GetComponent(typeof(EnemyScript));
            this.linkedEnemy.faceRight = true;
            this.linkedEnemy.motorcycle = this.transform;
            this.linkedEnemy.standStillInHuntMode = true;
            this.linkedEnemy.standStill = true;
            this.linkedEnemy.dontGiveScore = this.dontGiveScore;
        }
        this.mainPlayer = GameObject.Find("Player").transform;
        this.playerScript = (PlayerScript)this.mainPlayer.GetComponent(typeof(PlayerScript));
        this.cameraScript = (CameraScript)GameObject.Find("Main Camera").GetComponent(typeof(CameraScript));
        this.rBody = (Rigidbody)this.GetComponent(typeof(Rigidbody));
        this.motorcycleGraphics = this.transform.Find("PlayerMotorcycleGraphics");
        this.playerPos = this.transform.Find("PlayerMotorcycleGraphics/PlayerPos");
        this.playerPosStartRot = this.playerPos.localRotation;
        this.layerMask = 98560;
        if (this.isCar)
        {
            this.backWheels = this.transform.Find("BackWheels");
            this.frontWheels = this.transform.Find("FrontWheels");
            Transform transform = this.transform.Find("Ramp");
            if (transform != null)
            {
                this.ramp = (MeshCollider)transform.GetComponent(typeof(MeshCollider));
                this.ramp.isTrigger = true;
            }
        }
        else
        {
            this.backWheelHolder = this.motorcycleGraphics.Find("BackWheelHolder");
            this.backWheelHolderStartPos = this.backWheelHolder.localPosition;
            this.frontWheelHolder = this.motorcycleGraphics.Find("FrontWheelHolder");
            this.frontWheelHolderStartPos = this.frontWheelHolder.localPosition;
            this.backWheels = this.backWheelHolder.Find("BackWheel");
            this.frontWheels = this.frontWheelHolder.Find("FrontWheel");
        }
        this.backWheelsStartPos = this.backWheels.localPosition;
        this.frontWheelsStartPos = this.frontWheels.localPosition;
        if (this.linkedEnemy != null || Extensions.get_length(this.inputSwitch) > 0)
        {
            this.isEnemy = true;
            if (this.linkedEnemy == null)
            {
                this.enemySpawnTimer = (float)80;
            }
        }
        this.smokeParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeParticle").GetComponent(typeof(ParticleSystem));
        this.sparkParticle = (ParticleSystem)GameObject.Find("Main Camera/SparksParticle").GetComponent(typeof(ParticleSystem));
        if (!this.isEnemy)
        {
            this.playerAnimator = (Animator)this.mainPlayer.Find("PlayerGraphics").GetComponent(typeof(Animator));
        }
        this.motorAudioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.breakAudioSource = (AudioSource)this.backWheels.GetComponent(typeof(AudioSource));
        this.jumpAudioSource = (AudioSource)this.frontWheels.GetComponent(typeof(AudioSource));
    }

    // Token: 0x060002F2 RID: 754 RVA: 0x00046550 File Offset: 0x00044750
    public virtual void Update()
    {
        this.motorcycleGraphics.position = Vector3.Lerp(this.prevFixedPos, this.transform.position, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
        if (this.isCar)
        {
            Vector3 vector = Vector3.Lerp(this.prevFixedPos - this.transform.position, Vector3.zero, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
            this.backWheels.localPosition = this.backWheelsStartPos + this.motorcycleGraphics.localPosition;
            this.frontWheels.localPosition = this.frontWheelsStartPos + this.motorcycleGraphics.localPosition;
        }
        if (!this.isEnemy)
        {
            this.xDir = this.player.GetAxisRaw("Move");
            this.yDir = this.player.GetAxisRaw("MotorcycleY");
            this.kJump = this.player.GetButtonDown("Jump");
            this.kJumpHold = this.player.GetButton("Jump");
            this.kWheelie = this.player.GetButton("MotorcycleWheelie");
            this.jumpTimer -= this.root.timescale;
            if (this.groundDistance < (float)3)
            {
                this.cameraScript.screenShake = 0.015f + Mathf.Clamp01(this.rBody.velocity.magnitude * 0.1f) * 0.015f;
                if (this.kJump && this.jumpTimer <= (float)-30)
                {
                    int num = 10;
                    Vector3 velocity = this.rBody.velocity;
                    float num2 = velocity.y = (float)num;
                    Vector3 vector2 = this.rBody.velocity = velocity;
                    this.rBody.AddTorque(this.transform.right * (float)((this.root.kAction && (!this.root.kAction || this.xDir > (float)0)) ? 2 : -2), ForceMode.VelocityChange);
                    this.rBody.AddTorque(this.transform.right * (float)-20, ForceMode.Acceleration);
                    this.jumpTimer = (float)25;
                    this.playerAnimator.Play("MotorcycleJump", 0);
                    if (this.jumpAudioSource != null)
                    {
                        this.jumpAudioSource.clip = this.jumpSounds[UnityEngine.Random.Range(0, Extensions.get_length(this.jumpSounds))];
                        this.jumpAudioSource.volume = UnityEngine.Random.Range(0.8f, (float)1);
                        this.jumpAudioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                        this.jumpAudioSource.Play();
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        this.smokeParticle.Emit(this.root.generateEmitParams(this.transform.position + this.transform.up * UnityEngine.Random.Range(-2f, -1f) + this.transform.forward * -0.5f + this.transform.right * UnityEngine.Random.Range(-1f, 1f), new Vector3((float)UnityEngine.Random.Range(-10, -3), (float)UnityEngine.Random.Range(3, 10), UnityEngine.Random.Range(-2f, -0.3f)), (float)UnityEngine.Random.Range(13, 18), 0.4f, new Color((float)1, (float)1, (float)1, 0.1f)), 1);
                    }
                    this.emitSparks(this.transform.position + this.transform.up * (float)2 + this.transform.forward * -0.5f, 6);
                }
            }
            if ((this.kJumpHold || this.root.kAction) && this.rBody.velocity.y > (float)0 && this.jumpTimer > (float)10)
            {
                float y = this.rBody.velocity.y + this.jumpTimer / (float)25 * 0.8f * this.root.timescale;
                Vector3 velocity2 = this.rBody.velocity;
                float num3 = velocity2.y = y;
                Vector3 vector3 = this.rBody.velocity = velocity2;
                this.rBody.AddTorque(this.transform.right * (float)5 * ((float)1 - this.jumpTimer / (float)25), ForceMode.Acceleration);
            }
            if (!this.kJumpHold && !this.root.kAction && this.jumpTimer > (float)0)
            {
                this.jumpTimer = (float)0;
            }
        }
        else
        {
            if (this.isCar)
            {
                float x = this.motorcycleGraphics.up.z * (float)2;
                Vector3 localPosition = this.frontWheels.localPosition;
                float num4 = localPosition.x = x;
                Vector3 vector4 = this.frontWheels.localPosition = localPosition;
            }
            if (this.isCar && this.ramp != null)
            {
                if (this.player.GetButton("MotorcycleWheelie"))
                {
                    if (this.okToRamp)
                    {
                        this.ramp.isTrigger = false;
                    }
                }
                else
                {
                    this.ramp.isTrigger = true;
                }
            }
            if (this.followX)
            {
                this.xDir = Mathf.Clamp(this.mainPlayer.position.x + (float)((this.mainPlayer.position.x <= this.transform.position.x) ? 4 : -4) - this.transform.position.x, -this.speedMultiplier, this.speedMultiplier);
            }
            if (this.followZ)
            {
                this.yDir = Mathf.Clamp(this.mainPlayer.position.z - this.transform.position.z, -this.speedMultiplier, this.speedMultiplier);
            }
            if (this.pingPongX)
            {
                this.xDir = this.pingPongDirectionX * this.speedMultiplier;
            }
            if (this.pingPongZ)
            {
                this.yDir = this.pingPongDirectionZ * this.speedMultiplier;
            }
            if (this.alwaysStickBehindPlayer)
            {
                this.xDir = Mathf.Clamp(this.mainPlayer.position.x - this.alwaysStickBehindPlayerDistance - this.transform.position.x, -1.5f, (float)1);
                this.xSpeed = (this.targetXSpeed = this.xDir * (float)17);
                if (this.mainPlayer.position.x < this.transform.position.x + 6.5f)
                {
                    float x2 = this.mainPlayer.position.x - 6.5f;
                    Vector3 position = this.transform.position;
                    float num5 = position.x = x2;
                    Vector3 vector5 = this.transform.position = position;
                }
            }
            if (this.dead)
            {
                if (this.gameObject.layer != 22)
                {
                    this.gameObject.layer = 22;
                }
                if (this.nrOfExplosions < 4 && UnityEngine.Random.value > 0.8f)
                {
                    this.root.explode(this.transform.position, UnityEngine.Random.Range((float)1, 2.3f), 1, new Vector3((float)-1, (float)2, (float)0), "Yellow", true, false);
                    this.nrOfExplosions++;
                }
            }
            if (Extensions.get_length(this.inputSwitch) > 0)
            {
                this.switchInput = (float)-1;
                int num6 = 0;
                int j = 0;
                SwitchScript[] array = this.inputSwitch;
                int length = array.Length;
                while (j < length)
                {
                    if (array[j].output > (float)0)
                    {
                        num6++;
                    }
                    j++;
                }
                if (num6 >= Extensions.get_length(this.inputSwitch))
                {
                    this.switchInput = (float)1;
                }
            }
            if (this.switchInput > (float)0 && (this.linkedEnemy == null || this.linkedEnemy.health <= (float)0))
            {
                if (this.isCar)
                {
                    this.enemySpawnTimer += this.root.timescale;
                    if (this.spawnedEnemies >= this.enemiesToSpawn)
                    {
                        this.kill();
                    }
                    else if (this.enemySpawnTimer > (float)120)
                    {
                        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.enemy, this.playerPos.position, this.playerPos.rotation);
                        this.linkedEnemy = (EnemyScript)gameObject.GetComponent(typeof(EnemyScript));
                        this.linkedEnemy.weapon = (float)3;
                        this.linkedEnemy.motorcycle = this.transform;
                        this.linkedEnemy.dontGiveScore = this.dontGiveScore;
                        this.spawnedEnemies++;
                        this.enemySpawnTimer = (float)0;
                    }
                }
                else
                {
                    this.kill();
                }
            }
        }
        this.backWheels.localRotation = this.backWheels.localRotation * Quaternion.Euler((float)33 * this.root.timescale, (float)0, (float)0);
        this.frontWheels.localRotation = this.backWheels.localRotation;
        if (!this.dead)
        {
            if (!this.isCar)
            {
                this.frontWheelHolder.localRotation = Quaternion.Euler((float)0, (float)0, -this.rBody.velocity.z);
                float num7 = Mathf.Clamp(this.transform.up.y * 0.7f - this.rBody.velocity.y * 0.02f, -0.3f, 0.025f);
                this.frontWheelHolder.localPosition = this.frontWheelHolderStartPos + new Vector3((float)0, num7 * 0.2f, num7);
                float num8 = Mathf.Clamp(this.transform.up.y * (float)-20 + this.rBody.velocity.y, (float)-9, (float)9);
                this.backWheelHolder.localRotation = Quaternion.Euler(num8, (float)0, (float)0);
                this.backWheelHolder.localPosition = this.backWheelHolderStartPos + new Vector3((float)0, num8 * 0.01f, (float)0);
            }
            if (this.transform.position.z < -0.5f)
            {
                int num9 = 0;
                Vector3 velocity3 = this.rBody.velocity;
                float num10 = velocity3.z = (float)num9;
                Vector3 vector6 = this.rBody.velocity = velocity3;
                float z = -0.5f;
                Vector3 position2 = this.transform.position;
                float num11 = position2.z = z;
                Vector3 vector7 = this.transform.position = position2;
            }
            else if (this.transform.position.z > (float)16)
            {
                int num12 = 0;
                Vector3 velocity4 = this.rBody.velocity;
                float num13 = velocity4.z = (float)num12;
                Vector3 vector8 = this.rBody.velocity = velocity4;
                int num14 = 16;
                Vector3 position3 = this.transform.position;
                float num15 = position3.z = (float)num14;
                Vector3 vector9 = this.transform.position = position3;
            }
            this.particleEmitTimer -= this.root.timescale;
            if (this.particleEmitTimer <= (float)0)
            {
                this.smokeParticle.Emit(this.root.generateEmitParams(this.transform.position + this.transform.up * (float)2 + this.transform.forward * -0.5f, Vector3.right * (float)-20, (float)13, 0.65f, new Color((float)1, (float)1, (float)1, 0.075f)), 1);
                this.particleEmitTimer = UnityEngine.Random.Range(4f, 7f) / Mathf.Clamp(Mathf.Abs(this.rBody.velocity.z) * 0.25f, (float)1, (float)3);
            }
            this.motorcycleGraphics.localRotation = this.root.DampSlerp(Quaternion.Euler(new Vector3(Mathf.Clamp(-this.rBody.velocity.x * 0.5f, (float)-4, (float)4), this.rBody.velocity.z * -2.5f, (!this.isCar) ? ((float)0) : Mathf.Clamp(this.rBody.velocity.z * (float)-5, (float)-5, (float)5)) * ((!this.isCar) ? ((float)1) : 0.25f)), this.motorcycleGraphics.localRotation, 0.1f);
            float z2 = this.root.Damp(Mathf.Clamp(Mathf.Abs(this.rBody.velocity.x) * 0.5f, (float)-6, (float)6) * 0.02f - 0.05f, this.motorcycleGraphics.localPosition.z, 0.1f);
            Vector3 localPosition2 = this.motorcycleGraphics.localPosition;
            float num16 = localPosition2.z = z2;
            Vector3 vector10 = this.motorcycleGraphics.localPosition = localPosition2;
            this.targetXSpeed = this.xDir * (float)17;
            this.xSpeed = this.root.Damp(this.targetXSpeed, this.xSpeed, 0.15f);
            RaycastHit raycastHit = default(RaycastHit);
            if (Physics.Raycast(this.transform.position, Vector3.down, out raycastHit, (float)20, this.layerMask))
            {
                this.groundDistance = raycastHit.distance;
            }
            else
            {
                this.groundDistance = (float)20;
            }
            this.playerPos.localRotation = this.root.DampSlerp(this.playerPosStartRot * Quaternion.Euler((float)0, (float)0, this.rBody.angularVelocity.z * (float)3), this.playerPos.localRotation, 0.1f);
            if (this.motorAudioSource != null)
            {
                if (!this.motorAudioSource.isPlaying)
                {
                    this.motorAudioSource.Play();
                }
                float num17 = Mathf.Clamp(this.groundDistance - (float)1, (float)0, (float)3);
                this.audioMultiplier = this.root.Damp(Mathf.Clamp(this.xSpeed / (float)10, -0.25f, (float)999) + (float)((!this.kWheelie) ? 0 : 1) + Mathf.Clamp(this.rBody.velocity.x, -0.1f, (float)1) + num17 * (float)2, this.audioMultiplier, 0.06f);
                this.motorAudioSource.pitch = Mathf.Clamp(1.6f + this.audioMultiplier * 0.2f, (float)1, (float)3);
                this.motorAudioSource.volume = 0.8f + this.audioMultiplier * 0.2f;
                if (this.breakAudioSource != null)
                {
                    if (!this.breakAudioSource.isPlaying)
                    {
                        this.breakAudioSource.Play();
                    }
                    float num18 = Mathf.Abs(this.rBody.velocity.z / (float)90);
                    if (num18 < 0.05f)
                    {
                        num18 = (float)0;
                    }
                    float num19 = Mathf.Clamp01(-this.xSpeed / (float)10) * ((float)1 - Mathf.Clamp01(-this.rBody.velocity.x / (float)10));
                    float num20 = Mathf.Clamp01(-this.xSpeed / (float)20 + this.transform.position.x / (float)40);
                    this.breakAudioSource.volume = ((0.4f + num20) * num19 + num18) * ((float)3 - num17);
                    this.breakAudioSource.pitch = Mathf.Clamp(0.2f + num19 * this.transform.position.x / (float)80 + num18 * 0.5f, 0.6f, (float)1);
                }
            }
        }
    }

    // Token: 0x060002F3 RID: 755 RVA: 0x000476A4 File Offset: 0x000458A4
    public virtual void kill()
    {
        if (!this.dead)
        {
            this.root.explode(this.transform.position, (float)3, 3, new Vector3((float)-5, 2.5f, (float)0), "Yellow", true, true);
            this.rBody.AddForce(new Vector3((float)((!this.isCar) ? -10 : -35), (float)20, (float)0), ForceMode.VelocityChange);
            this.rBody.AddTorque(this.transform.right * (float)((!this.isCar) ? -50 : -10) + this.transform.forward * (float)((!this.isCar) ? 35 : 0) + this.transform.up * (float)((!this.isCar) ? 5 : -1), ForceMode.VelocityChange);
            this.rBody.constraints = RigidbodyConstraints.None;
            if (this.motorAudioSource != null)
            {
                this.motorAudioSource.Stop();
            }
            if (this.breakAudioSource != null)
            {
                this.breakAudioSource.Stop();
            }
            this.dead = true;

            networkHelper.enabled = false;
        }
    }

    // Token: 0x060002F4 RID: 756 RVA: 0x000477E4 File Offset: 0x000459E4
    public virtual void FixedUpdate()
    {
        if (this.isEnemy)
        {
            this.rBody.AddForce(Vector3.down * (float)600, ForceMode.Force);
        }
        else
        {
            this.rBody.AddForce(Vector3.down * (float)1000, ForceMode.Force);
        }
        float z = Mathf.Clamp(this.transform.position.z, (float)-1, (float)20);
        Vector3 position = this.transform.position;
        float num = position.z = z;
        Vector3 vector = this.transform.position = position;
        if (!this.dead)
        {
            this.rBody.AddForce(Vector3.right * this.xSpeed * ((!this.isEnemy) ? ((!this.kWheelie || this.xDir <= (float)0) ? 1.25f : 1.5f) : ((float)1)), ForceMode.Acceleration);
            this.rBody.AddForce(Vector3.forward * this.yDir * (float)45 * ((!this.isEnemy) ? ((!this.kWheelie) ? 1.35f : 1.6f) : ((float)1)), ForceMode.Acceleration);
            float z2 = this.rBody.velocity.z * Mathf.Pow(0.95f, this.root.fixedTimescale);
            Vector3 velocity = this.rBody.velocity;
            float num2 = velocity.z = z2;
            Vector3 vector2 = this.rBody.velocity = velocity;
            if ((!this.root.kAction && !this.kWheelie) || (this.groundDistance < (float)8 && this.rBody.velocity.y <= (float)1) || this.isEnemy)
            {
                this.rBody.angularDrag = Mathf.Abs(this.transform.forward.x) * (float)5;
                this.rBody.AddTorque(this.transform.right * ((this.groundDistance >= (float)5) ? 2.5f : ((float)15)) * -this.transform.forward.x, ForceMode.Acceleration);
                if (this.transform.forward.y < 0.2f)
                {
                    this.rBody.AddTorque(this.transform.right * (float)30 * -this.transform.forward.x, ForceMode.Acceleration);
                    if (this.transform.forward.y < -0.7f)
                    {
                        this.rBody.AddTorque(this.transform.right * (float)((this.rBody.angularVelocity.z >= (float)0) ? -10 : 10), ForceMode.Acceleration);
                    }
                }
            }
            else if (this.root.kAction)
            {
                this.rBody.AddTorque(this.transform.right * (this.xDir * (float)50), ForceMode.Acceleration);
            }
            if (this.kWheelie && this.groundDistance < (float)3)
            {
                float d = 0.4f - this.transform.forward.y;
                this.rBody.AddTorque(this.transform.right * (float)85 * d, ForceMode.Acceleration);
            }
            if (this.restrictMovement)
            {
                float num3 = this.movementZone.x + this.movementZoneSize.x / (float)2;
                float num4 = this.movementZone.x - this.movementZoneSize.x / (float)2;
                if (this.transform.position.x > num3)
                {
                    if (this.enteredRestrictedZone && this.rBody.velocity.x > (float)-1)
                    {
                        this.rBody.AddForce(Vector3.right * (num3 - this.transform.position.x) * (float)45, ForceMode.Acceleration);
                        this.pingPongDirectionX = (float)-1;
                    }
                }
                else if (this.transform.position.x < num4)
                {
                    if (this.enteredRestrictedZone && this.rBody.velocity.x < (float)1)
                    {
                        this.rBody.AddForce(Vector3.right * (num4 - this.transform.position.x) * (float)45, ForceMode.Acceleration);
                        this.pingPongDirectionX = (float)1;
                    }
                }
                else
                {
                    this.enteredRestrictedZone = true;
                }
                if (Mathf.Abs(this.transform.position.x) < (float)16)
                {
                    this.enteredRestrictedZone = true;
                }
                if (!this.enteredRestrictedZone)
                {
                    if (this.transform.position.x > (float)0)
                    {
                        float x = Mathf.Clamp(-this.transform.position.x, (float)-50, (float)-10);
                        Vector3 velocity2 = this.rBody.velocity;
                        float num5 = velocity2.x = x;
                        Vector3 vector3 = this.rBody.velocity = velocity2;
                    }
                    else
                    {
                        float x2 = Mathf.Clamp(-this.transform.position.x, (float)10, (float)30);
                        Vector3 velocity3 = this.rBody.velocity;
                        float num6 = velocity3.x = x2;
                        Vector3 vector4 = this.rBody.velocity = velocity3;
                    }
                }
                num3 = this.movementZone.y + this.movementZoneSize.y / (float)2;
                num4 = this.movementZone.y - this.movementZoneSize.y / (float)2;
                if (this.transform.position.z > num3 && this.rBody.velocity.z > (float)-1)
                {
                    this.rBody.AddForce(Vector3.forward * (num3 - this.transform.position.z) * (float)100, ForceMode.Acceleration);
                    this.pingPongDirectionZ = (float)-1;
                }
                else if (this.transform.position.z < num4 && this.rBody.velocity.z < (float)1)
                {
                    this.rBody.AddForce(Vector3.forward * (num4 - this.transform.position.z) * (float)100, ForceMode.Acceleration);
                    this.pingPongDirectionZ = (float)1;
                }
            }
            else if (!this.alwaysStickBehindPlayer && this.transform.position.x > (float)16 && this.rBody.velocity.x > (float)-3)
            {
                this.rBody.AddForce(Vector3.right * ((float)16 - this.transform.position.x) * (float)4, ForceMode.Acceleration);
            }
            else if (this.transform.position.x < (float)-16 && this.rBody.velocity.x < (float)3)
            {
                this.rBody.AddForce(Vector3.right * ((float)-16 - this.transform.position.x) * (float)4, ForceMode.Acceleration);
            }
            if (this.alwaysStickBehindPlayer && this.mainPlayer.position.x < this.transform.position.x + 6.5f)
            {
                float x3 = this.mainPlayer.position.x - 6.5f;
                Vector3 position2 = this.transform.position;
                float num7 = position2.x = x3;
                Vector3 vector5 = this.transform.position = position2;
            }
        }
        this.prevFixedPos = this.transform.position;
    }

    // Token: 0x060002F5 RID: 757 RVA: 0x000480A4 File Offset: 0x000462A4
    private void emitSparks(Vector3 pos, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            this.sparkParticle.Emit(this.root.generateEmitParams(pos, new Vector3((float)(-10 + UnityEngine.Random.Range(-4, 4)), (float)5 + UnityEngine.Random.Range(1f, 6f), UnityEngine.Random.Range(-0.5f, 0.5f)), UnityEngine.Random.Range(0.06f, 0.08f), UnityEngine.Random.Range(0.9f, 1.7f), new Color((float)1, (float)1, (float)1, (float)1)), 1);
        }
    }

    // Token: 0x060002F6 RID: 758 RVA: 0x000035EB File Offset: 0x000017EB
    public virtual void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            this.okToRamp = false;
        }
    }

    // Token: 0x060002F7 RID: 759 RVA: 0x00003609 File Offset: 0x00001809
    public virtual void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            this.okToRamp = true;
        }
    }

    // Token: 0x060002F8 RID: 760 RVA: 0x00048138 File Offset: 0x00046338
    public virtual void OnCollisionStay(Collision col)
    {
        if (this.dead)
        {
            if (this.rBody.velocity.y < (float)0)
            {
                this.rBody.AddForce(new Vector3((float)-35, (float)15, (float)0), ForceMode.VelocityChange);
                this.rBody.AddTorque(this.transform.right * (float)-100 + this.transform.forward * (float)35, ForceMode.VelocityChange);
            }
        }
        else
        {
            if (col.collider.tag == "MotorcycleRamp" || (!this.isEnemy && col.collider.gameObject.layer == 13))
            {
                this.rBody.AddForce(Vector3.right * (float)20, ForceMode.Acceleration);
            }
            if (!this.isEnemy && this.rBody.velocity.y < (float)-1 && col.collider.gameObject.layer == 13)
            {
                MotorcycleBossScript x = (MotorcycleBossScript)col.collider.gameObject.GetComponentInParent(typeof(MotorcycleBossScript));
                EnemyScript enemyScript = (EnemyScript)col.collider.gameObject.GetComponent(typeof(EnemyScript));
                if (enemyScript != null && x == null)
                {
                    enemyScript.bulletHit = true;
                    enemyScript.bulletStrength += (float)1;
                    enemyScript.bulletHitName = "Head";
                    enemyScript.bulletHitPos = col.contacts[0].point;
                    enemyScript.bulletHitRot = this.transform.rotation;
                    enemyScript.bulletHitVel = Vector3.up;
                    enemyScript.allowGib = false;
                    enemyScript.bulletTimeAlive = (float)999;
                    enemyScript.shootTimer = (float)UnityEngine.Random.Range(70, 100);
                    enemyScript.health = (float)0;
                }
            }
        }
    }

    // Token: 0x060002F9 RID: 761 RVA: 0x00048328 File Offset: 0x00046528
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3((float)-16, (float)0, (float)16), new Vector3((float)16, (float)0, (float)16));
        Gizmos.DrawLine(new Vector3((float)-16, (float)0, -0.5f), new Vector3((float)16, (float)0, -0.5f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector3(this.movementZone.x, (float)0, this.movementZone.y), new Vector3(this.movementZoneSize.x, (float)0, this.movementZoneSize.y));
    }
}
