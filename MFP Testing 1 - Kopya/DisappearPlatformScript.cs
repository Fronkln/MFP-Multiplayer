using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
[Serializable]
public class DisappearPlatformScript : MonoBehaviour
{
    // Token: 0x06000137 RID: 311 RVA: 0x000028DD File Offset: 0x00000ADD
    public DisappearPlatformScript()
    {
        this.hideTimer = (float)180;
    }

    // Token: 0x06000138 RID: 312 RVA: 0x00019418 File Offset: 0x00017618
    public virtual void saveState()
    {
        this.timerS = this.timer;
        this.doHideS = this.doHide;
        this.steppedOnTimerS = this.steppedOnTimer;
        this.dipAmountS = this.dipAmount;
        this.theColliderEnabledS = this.theCollider.enabled;
        this.theRendererEnabledS = this.theRenderer.enabled;
    }

    // Token: 0x06000139 RID: 313 RVA: 0x00019478 File Offset: 0x00017678
    public virtual void loadState()
    {
        this.timer = this.timerS;
        this.doHide = this.doHideS;
        this.steppedOnTimer = this.steppedOnTimerS;
        this.dipAmount = this.dipAmountS;
        this.theCollider.enabled = this.theColliderEnabledS;
        this.theRenderer.enabled = this.theRendererEnabledS;
    }

    // Token: 0x0600013A RID: 314 RVA: 0x000028F1 File Offset: 0x00000AF1
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

    // Token: 0x0600013B RID: 315 RVA: 0x000194D8 File Offset: 0x000176D8
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
        this.randomTimeOffset = UnityEngine.Random.value * (float)10;
    }

    // Token: 0x0600013C RID: 316 RVA: 0x000195C0 File Offset: 0x000177C0
    public virtual void Update()
    {
        if (this.playerScript.groundTransform == this.transform)
        {
            this.dipAmount = this.root.Damp(0.2f, this.dipAmount, 0.05f);
            this.steppedOnTimer = (float)3;
            this.transform.rotation = this.root.DampSlerp(this.startRot * Quaternion.Euler(-this.transform.InverseTransformPoint(this.playerScript.transform.position).y * (float)2, (float)0, (float)0), this.transform.rotation, 0.1f);
        }
        else
        {
            this.dipAmount = this.root.Damp(Mathf.Sin(Time.time + this.randomTimeOffset) * 0.1f, this.dipAmount, 0.1f);
            this.transform.rotation = this.root.DampSlerp(this.startRot * Quaternion.Euler(Mathf.Sin((Time.time + this.randomTimeOffset) / (float)2) * (float)2, (float)0, (float)0), this.transform.rotation, 0.1f);
            if (this.steppedOnTimer != (float)0)
            {
                this.steppedOnTimer -= this.root.timescale;
                if (this.steppedOnTimer <= (float)0)
                {
                    this.doHide = true;
                }
            }
        }
        if (this.playerScript.groundTransform == this.transform && this.playerScript.justWallJumped)
        {
            this.doHide = true;
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
            if (this.timer >= this.hideTimer && this.hideTimer != (float)-1)
            {
                this.timer = (float)0;
                this.theRenderer.enabled = true;
                this.theCollider.enabled = true;
                this.doHide = false;
            }
            else
            {
                float y = this.transform.localScale.y - 0.1f * this.root.timescale;
                Vector3 localScale = this.transform.localScale;
                float num = localScale.y = y;
                Vector3 vector = this.transform.localScale = localScale;
                if (this.transform.localScale.y <= (float)0)
                {
                    int num2 = 0;
                    Vector3 localScale2 = this.transform.localScale;
                    float num3 = localScale2.y = (float)num2;
                    Vector3 vector2 = this.transform.localScale = localScale2;
                    this.theRenderer.enabled = false;
                    this.theCollider.enabled = false;
                    this.steppedOnTimer = (float)0;
                }
            }
        }
        if (!this.doHide)
        {
            if (this.doHideDoOnce)
            {
                this.doHideDoOnce = false;
            }
            float y2 = this.transform.localScale.y + 0.1f * this.root.timescale;
            Vector3 localScale3 = this.transform.localScale;
            float num4 = localScale3.y = y2;
            Vector3 vector3 = this.transform.localScale = localScale3;
            if (this.transform.localScale.y > this.startScale.y)
            {
                float y3 = this.startScale.y;
                Vector3 localScale4 = this.transform.localScale;
                float num5 = localScale4.y = y3;
                Vector3 vector4 = this.transform.localScale = localScale4;
            }
            this.transform.position = this.startPos + Vector3.up * -this.dipAmount;
        }
    }

    // Token: 0x0600013D RID: 317 RVA: 0x000020A7 File Offset: 0x000002A7
    public virtual void Main()
    {
    }

    // Token: 0x04000328 RID: 808
    private RootScript root;

    // Token: 0x04000329 RID: 809
    private PlayerScript playerScript;

    // Token: 0x0400032A RID: 810
    private AudioSource theSound;

    // Token: 0x0400032B RID: 811
    private Vector3 startPos;

    // Token: 0x0400032C RID: 812
    private Vector3 startScale;

    // Token: 0x0400032D RID: 813
    private Quaternion startRot;

    // Token: 0x0400032E RID: 814
    private BoxCollider theCollider;

    // Token: 0x0400032F RID: 815
    public float hideTimer;

    // Token: 0x04000330 RID: 816
    private float timer;

    // Token: 0x04000331 RID: 817
    private bool doHide;

    // Token: 0x04000332 RID: 818
    private bool doHideDoOnce;

    // Token: 0x04000333 RID: 819
    private float steppedOnTimer;

    // Token: 0x04000334 RID: 820
    private MeshRenderer theRenderer;

    // Token: 0x04000335 RID: 821
    private float dipAmount;

    // Token: 0x04000336 RID: 822
    private float randomTimeOffset;

    // Token: 0x04000337 RID: 823
    private float timerS;

    // Token: 0x04000338 RID: 824
    private bool doHideS;

    // Token: 0x04000339 RID: 825
    private float steppedOnTimerS;

    // Token: 0x0400033A RID: 826
    private float dipAmountS;

    // Token: 0x0400033B RID: 827
    private bool theColliderEnabledS;

    // Token: 0x0400033C RID: 828
    private bool theRendererEnabledS;
}
