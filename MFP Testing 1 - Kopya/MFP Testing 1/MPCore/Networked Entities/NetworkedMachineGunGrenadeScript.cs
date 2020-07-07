// Decompiled with JetBrains decompiler
// Type: MachineGunGrenadeScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using UnityEngine;

[Serializable]
public class NetworkedMachineGunGrenadeScript : BaseNetworkEntity
{
    private RootScript root;
    private Rigidbody rBody;
    private AudioSource whistleSound;
    private float whistleVolumeThingie;

    public override void Start()
    {
        base.Start();

        root = mpManager.root;
        this.rBody = (Rigidbody)this.transform.GetComponent(typeof(Rigidbody));
        Physics.IgnoreCollision((Collider)this.transform.GetComponent(typeof(BoxCollider)), (Collider)GameObject.Find("Player").GetComponent(typeof(SphereCollider)));
        this.whistleSound = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.whistleSound.volume = 0.3f;
    }

    public override void Update()
    {
        base.Update();

        if ((double)this.rBody.velocity.y < 0.0)
        {
            this.whistleSound.pitch = Mathf.Clamp(this.whistleSound.pitch - 0.015f * this.root.timescale, 0.01f, 10f);
            this.whistleVolumeThingie += 0.005f * this.root.timescale;
        }
        this.whistleVolumeThingie = Mathf.Clamp(this.whistleVolumeThingie + 0.0005f * this.root.timescale, 0.0f, 2f) * 3f;
        this.whistleSound.volume = (float)((double)Mathf.Clamp01((float)((double)Mathf.Clamp01(2f - this.whistleVolumeThingie * 8f) + 0.100000001490116 * (double)this.whistleVolumeThingie + (double)this.rBody.velocity.magnitude / 5.0)) * (double)this.whistleVolumeThingie * 15.0);
    }

    public virtual void FixedUpdate()
    {
        this.rBody.AddForce(2.5f * Physics.gravity);
    }

    public virtual void OnCollisionEnter(Collision col)
    {
        PacketSender.SendRootExplosion(this.transform.position - this.rBody.velocity.normalized * 0.75f, 1.7f, 3, Vector3.zero, "Yellow", false, true);
    //   ((RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript))).explode(this.transform.position - this.rBody.velocity.normalized * 0.75f, 1.7f, 3, Vector3.zero, "Yellow", false, true);
        UnityEngine.Object.Destroy((UnityEngine.Object)this.gameObject);
    }

    public virtual void Main()
    {
    }
}
