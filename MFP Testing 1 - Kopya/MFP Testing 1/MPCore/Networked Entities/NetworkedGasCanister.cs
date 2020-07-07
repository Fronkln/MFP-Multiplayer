// Decompiled with JetBrains decompiler
// Type: GasCanisterScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using System.Collections;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class NetworkedGasCanisterScript : BaseNetworkEntity
{
    private RootScript root;
    private AudioSource theSound;
    private float ranPitchOffset;
    public bool allowEnemyBulletHit;
    public bool allowTriggerByExplosion;
    public float topMargin;
    private bool topBeenHit;
    public float explosionSize;
    public float explosionYOffset;
    public bool containsBits;
    private Transform objGraphics;
    private bool doDestroy;
    public bool emitSmokeOnHit;
    [HideInInspector]
    public bool doEmitSmoke;
    private float smokeTimer;
    private float smokeEmitTimer;
    private Vector3[] smokePositions;
    private Vector3[] smokeDirections;
    private ParticleSystem smokeParticle;
    private Rigidbody rBody;
    private PlayerScript playerScript;
    private Rigidbody playerRBody;
    private bool playerFollow;
    public bool dontAllowPlayerFollow;
    private Vector3 prevPos;
    public bool lockMovement;
    public float lockedMoveAmount;
    private float lockedSpeed;
    private float lockedPos;
    private Vector3 startPos;
    private Vector3 startDir;
    private CameraScript mainCameraScript;
    private bool topBeenHitS;
    private bool doDestroyS;
    private bool doEmitSmokeS;
    private float smokeTimerS;
    private float smokeEmitTimerS;
    private Vector3[] smokePositionsS;
    private Vector3[] smokeDirectionsS;
    private bool playerFollowS;
    private Vector3 prevPosS;
    private float lockedSpeedS;
    private float lockedPosS;
    private bool theSoundIsPlayingS;

    public NetworkedGasCanisterScript()
    {
        this.allowEnemyBulletHit = true;
        this.allowTriggerByExplosion = true;
        this.explosionSize = 2.5f;
        this.explosionYOffset = 0.2f;
    }

    public virtual void saveState()
    {
        this.topBeenHitS = this.topBeenHit;
        this.doDestroyS = this.doDestroy;
        this.doEmitSmokeS = this.doEmitSmoke;
        this.smokeTimerS = this.smokeTimer;
        this.smokeEmitTimerS = this.smokeEmitTimer;
        this.smokePositionsS = this.smokePositions;
        this.smokeDirectionsS = this.smokeDirections;
        this.playerFollowS = this.playerFollow;
        this.prevPosS = this.prevPos;
        this.lockedSpeedS = this.lockedSpeed;
        this.lockedPosS = this.lockedPos;
        this.theSoundIsPlayingS = this.theSound.isPlaying;
    }

    public virtual void loadState()
    {
        this.topBeenHit = this.topBeenHitS;
        this.doDestroy = this.doDestroyS;
        this.doEmitSmoke = this.doEmitSmokeS;
        this.smokeTimer = this.smokeTimerS;
        this.smokeEmitTimer = this.smokeEmitTimerS;
        this.smokePositions = this.smokePositionsS;
        this.smokeDirections = this.smokeDirectionsS;
        this.playerFollow = this.playerFollowS;
        this.prevPos = this.prevPosS;
        this.lockedSpeed = this.lockedSpeedS;
        this.lockedPos = this.lockedPosS;
        if (this.theSoundIsPlayingS)
            this.theSound.Play();
        else
            this.theSound.Stop();
    }

    public virtual void LateUpdate()
    {
        if (this.root.doCheckpointSave)
            this.saveState();
        if (!this.root.doCheckpointLoad)
            return;
        this.loadState();
    }


    public void GasCanisterExplode()
    {
       // PacketSender.SendRootExplosion(this.transform.position + this.transform.forward * this.explosionYOffset, this.explosionSize, 3, Vector3.ClampMagnitude(this.rBody.velocity, 2f), "Yellow", false, true);
         this.root.explode(this.transform.position + this.transform.forward * this.explosionYOffset, this.explosionSize, 3, Vector3.ClampMagnitude(this.rBody.velocity, 2f), "Yellow", false, true);
        if (this.containsBits)
        {
            Component[] componentsInChildren = this.gameObject.GetComponentsInChildren(typeof(Transform), true);
            int index = 0;
            Component[] componentArray = componentsInChildren;
            for (int length = componentArray.Length; index < length; ++index)
            {
                if ((UnityEngine.Object)((Transform)componentArray[index]).parent == (UnityEngine.Object)this.transform && (UnityEngine.Object)componentArray[index] != (UnityEngine.Object)this.objGraphics)
                {
                    componentArray[index].gameObject.SetActive(true);
                    ((Transform)componentArray[index]).parent = this.transform.parent;
                    Rigidbody component = (Rigidbody)componentArray[index].GetComponent(typeof(Rigidbody));
                    if ((UnityEngine.Object)component != (UnityEngine.Object)null)
                    {
                        component.isKinematic = false;
                        component.velocity = this.rBody.velocity * 0.5f + (((Transform)componentArray[index]).position - this.transform.position).normalized * 2f + new Vector3((float)UnityEngine.Random.Range(-3, 3), (float)UnityEngine.Random.Range(2, 5), 0.0f);
                        component.angularVelocity = component.velocity + new Vector3((float)UnityEngine.Random.Range(-10, 10), (float)UnityEngine.Random.Range(-10, 10), (float)UnityEngine.Random.Range(-10, 10));
                    }
                }
            }
        }
        this.gameObject.SetActive(false);
    }

    public override void Start()
    {
        base.Start();

        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.theSound = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.ranPitchOffset = UnityEngine.Random.Range(0.0f, 0.1f);
        UnityScript.Lang.Array array = new UnityScript.Lang.Array();
        array.Add((object)Vector3.zero);
        this.smokePositions = array.ToBuiltin(typeof(Vector3)) as Vector3[];
        this.smokeDirections = array.ToBuiltin(typeof(Vector3)) as Vector3[];
        this.smokeParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeParticle").GetComponent(typeof(ParticleSystem));
        this.rBody = (Rigidbody)this.GetComponent(typeof(Rigidbody));
        if (this.emitSmokeOnHit)
            this.objGraphics = this.transform.Find("Graphics");
        this.playerScript = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
        this.playerRBody = (Rigidbody)this.playerScript.transform.GetComponent(typeof(Rigidbody));
        if (!this.lockMovement)
            return;
        this.startDir = -this.transform.forward;
        this.startPos = this.transform.position;
        this.rBody.isKinematic = true;
        this.mainCameraScript = (CameraScript)GameObject.Find("Main Camera").GetComponent(typeof(CameraScript));
    }

    public override void Update()
    {
        base.Update();

        if (this.doEmitSmoke)
        {
            if (!this.theSound.isPlaying)
                this.theSound.Play();
            this.theSound.pitch = Mathf.Clamp((float)((double)this.smokeTimer * (1.0 / 1000.0) + 0.300000011920929) + this.ranPitchOffset, 0.1f, 2f);
            this.theSound.volume = Mathf.Clamp((float)((double)this.smokeTimer * 0.00999999977648258 + 0.600000023841858 + (double)this.ranPitchOffset * 0.5), 0.1f, 2f);
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            for (int index = 1; index < Extensions.get_length((System.Array)this.smokePositions); ++index)
            {
                if (!this.lockMovement)
                    this.smokeTimer += this.root.timescale;
                if ((double)UnityEngine.Random.value > 0.25 && (double)this.smokeEmitTimer == 0.0)
                {
                    emitParams.position = this.transform.TransformPoint(this.smokePositions[index]);
                    Vector3 vector3 = -this.transform.TransformDirection(this.smokeDirections[index]);
                    emitParams.velocity = vector3 * 3.5f + new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f));
                    emitParams.startSize = UnityEngine.Random.Range(0.4f, 0.55f);
                    emitParams.startLifetime = UnityEngine.Random.Range(0.4f, 0.7f);
                    emitParams.startColor = (Color32)(new Color(1f, 1f, 1f, UnityEngine.Random.Range(0.1f, 0.25f)) * UnityEngine.Random.Range(0.75f, 1f));
                    if ((double)Mathf.Abs(vector3.x) > 0.800000011920929)
                    {
                        emitParams.startSize += 1f + UnityEngine.Random.Range(0.4f, 0.55f);
                        emitParams.startLifetime += 1f + UnityEngine.Random.Range(0.8f, 1.2f);
                        emitParams.velocity *= 0.3f;
                    }
                    this.smokeParticle.Emit(emitParams, 1);
                    this.rBody.AddTorque(Vector3.one * ((double)this.smokePositions[index].y >= 0.0 ? 5f : -5f), ForceMode.Impulse);
                }
            }
            if (this.topBeenHit)
            {
                if (this.lockMovement)
                {
                    this.smokeTimer += this.root.timescale * 2f;
                    this.lockedSpeed = this.root.Damp(0.75f, this.lockedSpeed, 0.05f);
                    this.lockedPos += this.lockedSpeed * this.root.timescale;
                    if ((double)this.lockedPos > (double)this.lockedMoveAmount)
                    {
                        this.lockedPos = this.lockedMoveAmount;
                        this.lockedSpeed *= -0.3f;
                    }
                    this.transform.position = this.startPos + this.startDir * this.lockedPos;
                }
                else
                    this.rBody.velocity += -this.transform.forward * 2f * this.root.timescale;
                if (!this.dontAllowPlayerFollow)
                    this.playerFollow = (UnityEngine.Object)this.playerScript.groundTransform == (UnityEngine.Object)this.transform;
            }
            this.smokeEmitTimer += this.root.timescale;
            if ((double)this.smokeEmitTimer >= 1.0)
            {
                this.transform.position = this.transform.position + new Vector3(UnityEngine.Random.Range(-this.smokeTimer, this.smokeTimer), UnityEngine.Random.Range(-this.smokeTimer, this.smokeTimer), 0.0f) * 0.00025f;
                this.rBody.AddTorque(Vector3.one * UnityEngine.Random.Range(-this.smokeTimer, this.smokeTimer) * 3f, ForceMode.Impulse);
                this.objGraphics.RotateAround(this.objGraphics.position, this.objGraphics.forward, UnityEngine.Random.Range(-this.smokeTimer, this.smokeTimer) * 0.1f);
                this.smokeEmitTimer = 0.0f;
            }
            if (this.playerFollow)
            {
                this.root.autoSaveTimer = 0.0f;
                this.smokeTimer = 60f;
            }
            if ((double)this.smokeTimer > 180.0)
            {
                this.doEmitSmoke = false;
                this.doDestroy = true;
            }
        }
        if (!this.doDestroy)
            return;

        PacketSender.BaseNetworkedEntityRPC("GasCanisterExplode", entityIdentifier, sendType: Steamworks.EP2PSend.k_EP2PSendUnreliableNoDelay);
    }

    public virtual void FixedUpdate()
    {
        if (this.playerFollow)
        {
            Vector3 vector3_1 = this.transform.position - this.prevPos;
            this.playerRBody.transform.position = this.playerRBody.transform.position + vector3_1;
            float num1 = this.root.DampFixed(this.transform.position.x, this.playerRBody.transform.position.x, 0.02f);
            Vector3 position = this.playerRBody.transform.position;
            double num2 = (double)(position.x = num1);
            Vector3 vector3_2 = this.playerRBody.transform.position = position;
            this.mainCameraScript.camPos += vector3_1 * 0.4f * this.root.fixedTimescale;
        }
        this.prevPos = this.transform.position;
    }

    public virtual void OnCollisionEnter(Collision col)
    {
        if (this.doDestroy || !(col.transform.tag == "Bullet") && col.gameObject.layer != 21)
            return;
        BulletScript component = (BulletScript)col.transform.GetComponent(typeof(BulletScript));
        if (!this.allowEnemyBulletHit && !component.friendly)
            return;
        if (this.emitSmokeOnHit && !component.isExplosion)
        {
            this.doEmitSmoke = true;
            UnityScript.Lang.Array array1 = new UnityScript.Lang.Array((IEnumerable)this.smokePositions);
            UnityScript.Lang.Array array2 = new UnityScript.Lang.Array((IEnumerable)this.smokeDirections);
            Vector3 vector3 = this.transform.InverseTransformPoint(col.contacts[0].point);
            vector3.x = 0.0f;
            if (!this.topBeenHit)
            {
                array2.Add((object)this.transform.InverseTransformDirection(-this.transform.forward));
                array1.Add((object)new Vector3(0.0f, 0.0f, this.topMargin + 1.5f));
                this.topBeenHit = true;
            }
            array1.Add((object)vector3);
            this.smokePositions = array1.ToBuiltin(typeof(Vector3)) as Vector3[];
            array2.Add((object)this.transform.InverseTransformDirection((double)vector3.z <= (double)this.topMargin ? col.contacts[0].normal : -this.transform.forward));
            this.smokeDirections = array2.ToBuiltin(typeof(Vector3)) as Vector3[];
            this.rBody.AddTorque(Vector3.one * ((double)col.contacts[0].point.x >= (double)this.transform.position.x ? 50f : -50f), ForceMode.Impulse);
        }
        else
        {
            if ((!component.isExplosion || !this.allowTriggerByExplosion) && component.isExplosion)
                return;
            this.doDestroy = true;
        }
    }

    public virtual void Main()
    {
    }
}
