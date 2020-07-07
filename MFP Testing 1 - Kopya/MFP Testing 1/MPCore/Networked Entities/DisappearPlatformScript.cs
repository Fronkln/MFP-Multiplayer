// Decompiled with JetBrains decompiler
// Type: DisappearPlatformScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using UnityEngine;

[Serializable]
public class DisappearPlatformScript : MonoBehaviour
{

    private BaseNetworkEntity networkHelper;

    private RootScript root;
    private PlayerScript playerScript;
    private AudioSource theSound;
    private Vector3 startPos;
    private Vector3 startScale;
    private Quaternion startRot;
    private BoxCollider theCollider;
    public float hideTimer;
    private float timer;
    private bool doHide;
    private bool doHideDoOnce;
    private float steppedOnTimer;
    private MeshRenderer theRenderer;
    private float dipAmount;
    private float randomTimeOffset;

    public DisappearPlatformScript()
    {
        this.hideTimer = 180f;
    }


    public void DisappearPlatform()
    {
        this.doHide = true;
    }

    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
        networkHelper.maxAllowedPackets = 2;
    }

    public virtual void Start()
    {

        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.playerScript = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
        this.theSound = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.startPos = this.transform.position;
        this.startScale = this.transform.localScale;
        this.startRot = this.transform.rotation;
        this.theCollider = (BoxCollider)this.GetComponent(typeof(BoxCollider));
        this.theRenderer = (MeshRenderer)this.GetComponent(typeof(MeshRenderer));
        this.randomTimeOffset = UnityEngine.Random.value * 10f;

    }



    public virtual void Update()
    {
        if ((UnityEngine.Object)this.playerScript.groundTransform == (UnityEngine.Object)this.transform)
        {
            this.dipAmount = this.root.Damp(0.2f, this.dipAmount, 0.05f);
            this.steppedOnTimer = 3f;
            this.transform.rotation = this.root.DampSlerp(this.startRot * Quaternion.Euler((float)(-(double)this.transform.InverseTransformPoint(this.playerScript.transform.position).y * 2.0), 0.0f, 0.0f), this.transform.rotation, 0.1f);
        }
        else
        {
            this.dipAmount = this.root.Damp(Mathf.Sin(Time.time + this.randomTimeOffset) * 0.1f, this.dipAmount, 0.1f);
            this.transform.rotation = this.root.DampSlerp(this.startRot * Quaternion.Euler(Mathf.Sin((float)(((double)Time.time + (double)this.randomTimeOffset) / 2.0)) * 2f, 0.0f, 0.0f), this.transform.rotation, 0.1f);
            if ((double)this.steppedOnTimer != 0.0)
            {
                this.steppedOnTimer -= this.root.timescale;
                if ((double)this.steppedOnTimer <= 0.0)
                    //  this.doHide = true;
                    if (!doHide)
                    {
                        if (!MultiplayerManagerTest.singleplayerMode)
                            PacketSender.BaseNetworkedEntityRPC("DisappearPlatform", networkHelper.entityIdentifier, sendType: Steamworks.EP2PSend.k_EP2PSendUnreliable);
                        else
                            doHide = true;
                    }

            }
        }
        if ((UnityEngine.Object)this.playerScript.groundTransform == (UnityEngine.Object)this.transform && this.playerScript.justWallJumped)
            if (!doHide)
            {
                if (!MultiplayerManagerTest.singleplayerMode)
                    PacketSender.BaseNetworkedEntityRPC("DisappearPlatform", networkHelper.entityIdentifier, sendType: Steamworks.EP2PSend.k_EP2PSendUnreliable);
                else
                    doHide = true;
            }
        if (this.doHide)
        {
            if (!this.doHideDoOnce)
            {
                this.theSound.pitch = UnityEngine.Random.Range(0.4f, 0.9f);
                this.theSound.volume = UnityEngine.Random.Range(0.3f, 0.8f);
                this.theSound.Play();
                this.doHideDoOnce = true;
            }
            this.timer += this.root.timescale;
            if ((double)this.timer >= (double)this.hideTimer && (double)this.hideTimer != -1.0)
            {
                this.timer = 0.0f;
                this.theRenderer.enabled = true;
                this.theCollider.enabled = true;
                this.doHide = false;
            }
            else
            {
                float num1 = this.transform.localScale.y - 0.1f * this.root.timescale;
                Vector3 localScale1 = this.transform.localScale;
                float num2 = localScale1.y = num1;
                Vector3 vector3_1 = this.transform.localScale = localScale1;
                if ((double)this.transform.localScale.y <= 0.0)
                {
                    int num3 = 0;
                    Vector3 localScale2 = this.transform.localScale;
                    float num4 = localScale2.y = (float)num3;
                    Vector3 vector3_2 = this.transform.localScale = localScale2;
                    this.theRenderer.enabled = false;
                    this.theCollider.enabled = false;
                    this.steppedOnTimer = 0.0f;
                }
            }
        }
        if (this.doHide)
            return;
        if (this.doHideDoOnce)
            this.doHideDoOnce = false;
        float num5 = this.transform.localScale.y + 0.1f * this.root.timescale;
        Vector3 localScale3 = this.transform.localScale;
        float num6 = localScale3.y = num5;
        Vector3 vector3_3 = this.transform.localScale = localScale3;
        if ((double)this.transform.localScale.y > (double)this.startScale.y)
        {
            float y = this.startScale.y;
            Vector3 localScale1 = this.transform.localScale;
            float num1 = localScale1.y = y;
            Vector3 vector3_1 = this.transform.localScale = localScale1;
        }
        this.transform.position = this.startPos + Vector3.up * -this.dipAmount;
    }
}
