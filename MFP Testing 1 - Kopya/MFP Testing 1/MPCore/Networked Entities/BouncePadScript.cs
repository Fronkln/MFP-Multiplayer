using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
[Serializable]
public class BouncePadScript : MonoBehaviour
{

	private BaseNetworkEntity networkHelper;
	private bool padWorking { get { return moveSpeed > 0; } }

	// Token: 0x06000068 RID: 104 RVA: 0x000020A9 File Offset: 0x000002A9
	public BouncePadScript()
	{
	}


	public virtual void Awake()
	{
		networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
	}

	// Token: 0x06000069 RID: 105 RVA: 0x0000B43C File Offset: 0x0000963C
	public virtual void Start()
	{
		this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
		this.playerScript = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
		this.top = this.transform.Find("Bouncepad_Top");
		this.spring = this.transform.Find("Bouncepad_Spring");
		this.theSound = (AudioSource)this.GetComponent(typeof(AudioSource));
		this.topStartPos = this.top.localPosition.z;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x0000B4F4 File Offset: 0x000096F4
	public virtual void Update()
	{
		if (this.playerScript.groundTransform == this.transform)
		{
			//PacketSender.BaseNetworkedEntityRPC("TrampolineBounce", networkHelper.entityIdentifier, sendType: Steamworks.EP2PSend.k_EP2PSendUnreliableNoDelay);
			if (!padWorking)
			{
				doThing();
				PacketSender.BaseNetworkedEntityRPC("TrampolineBounce", networkHelper.entityIdentifier, sendType: Steamworks.EP2PSend.k_EP2PSendUnreliableNoDelay);
			}
		}
		this.moveSpeed += this.root.DampAdd(this.topStartPos, this.top.localPosition.z, 0.1f);
		this.moveSpeed *= Mathf.Pow(0.9f, this.root.timescale);
		float z = this.top.localPosition.z + this.moveSpeed * this.root.timescale;
		Vector3 localPosition = this.top.localPosition;
		float num = localPosition.z = z;
		Vector3 vector = this.top.localPosition = localPosition;
		if (this.top.localPosition.z < this.topStartPos)
		{
			float z2 = this.topStartPos;
			Vector3 localPosition2 = this.top.localPosition;
			float num2 = localPosition2.z = z2;
			Vector3 vector2 = this.top.localPosition = localPosition2;
			this.moveSpeed *= -0.6f;
		}
		float z3 = this.top.localPosition.z - this.spring.localPosition.z;
		Vector3 localScale = this.spring.localScale;
		float num3 = localScale.z = z3;
		Vector3 vector3 = this.spring.localScale = localScale;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00002323 File Offset: 0x00000523
	public virtual void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Player")
		{
			this.doThing();
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000B69C File Offset: 0x0000989C
	public virtual void doThing()
	{
		if (this.transform.InverseTransformPoint(this.playerScript.transform.position).z > (float)0)
		{
			this.playerScript.justWallJumped = false;
			this.playerScript.wallJumpTimer = (float)0;
			this.playerScript.dontAllowWallJumpTimer = (float)30;
			this.playerScript.kJumpDownTimer = (float)0;
			this.playerScript.inAirTimer = (float)999;
			this.playerScript.disableFloatJump = true;
			this.playerScript.dontLockTowall = true;
			this.playerScript.ySpeed = this.transform.forward.y * this.bounceStrength;
			if (Mathf.Abs(this.transform.forward.x) < 0.1f)
			{
				this.playerScript.xSpeed = this.playerScript.xSpeed + this.transform.forward.x * this.bounceStrength;
			}
			else
			{
				this.playerScript.xSpeed = this.transform.forward.x * this.bounceStrength;
			}
			this.playerScript.groundTransform = null;
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00002345 File Offset: 0x00000545
	public virtual void doVisualThing()
	{
		this.PlaySound();
		this.moveSpeed = 0.5f * (this.bounceStrength / (float)20);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000B7FC File Offset: 0x000099FC
	public virtual void PlaySound()
	{
		this.theSound.volume = this.bounceStrength / (float)21 - 0.3f + UnityEngine.Random.Range(-0.1f, 0.3f);
		this.theSound.pitch = Mathf.Clamp(this.bounceStrength / (float)21 + UnityEngine.Random.Range(-0.15f, 0.15f), 0.75f, 1.4f);
		this.theSound.Play();
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000020A7 File Offset: 0x000002A7
	public virtual void Main()
	{
	}

	// Token: 0x04000141 RID: 321
	private RootScript root;

	// Token: 0x04000142 RID: 322
	private PlayerScript playerScript;

	// Token: 0x04000143 RID: 323
	private Transform top;

	// Token: 0x04000144 RID: 324
	private Transform spring;

	// Token: 0x04000145 RID: 325
	public float bounceStrength;

	// Token: 0x04000146 RID: 326
	private float topStartPos;

	// Token: 0x04000147 RID: 327
	private float moveSpeed;

	// Token: 0x04000148 RID: 328
	private AudioSource theSound;
}
