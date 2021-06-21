using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x020000C5 RID: 197
[Serializable]
public class SpinScrewScript : MonoBehaviour
{
	private BaseNetworkEntity networkHelper;

	// Token: 0x0600056C RID: 1388 RVA: 0x00004317 File Offset: 0x00002517
	public SpinScrewScript()
	{
		this.speed = 0.04f;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0000432A File Offset: 0x0000252A
	public virtual void saveState()
	{
		this.moveSpeedS = this.moveSpeed;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00004338 File Offset: 0x00002538
	public virtual void loadState()
	{
		this.moveSpeed = this.moveSpeedS;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00004346 File Offset: 0x00002546
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


	public virtual void Awake()
	{
		networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0008E100 File Offset: 0x0008C300
	public virtual void Start()
	{
		this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
		this.playerScript = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
		this.objectMovementSoundScript = (ObjectMovementSoundScript)this.GetComponent(typeof(ObjectMovementSoundScript));
		this.startPos = this.transform.position;
		if (this.linkScrew != null && !this.dontDoLinkFiddle)
		{
			this.linkScrew.linkScrew = (SpinScrewScript)this.GetComponent(typeof(SpinScrewScript));
			this.linkScrew.dontDoLinkFiddle = true;
			if (this.moveInX)
			{
				float x = this.transform.position.x + this.moveAmount;
				Vector3 position = this.transform.position;
				float num = position.x = x;
				Vector3 vector = this.transform.position = position;
			}
			else
			{
				float y = this.transform.position.y + this.moveAmount;
				Vector3 position2 = this.transform.position;
				float num2 = position2.y = y;
				Vector3 vector2 = this.transform.position = position2;
			}
		}
		this.startRot = this.transform.localRotation;
		this.stablePlatform = this.transform.Find("StablePlatform");
		this.screwCollision = this.transform.Find("ScrewCollision");
		this.screwCollision.parent = null;
		if (this.moveAmount < (float)0)
		{
			IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(this.transform);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				object obj3;
				object obj2 = obj3 = obj;
				if (!(obj2 is Transform))
				{
					obj3 = RuntimeServices.Coerce(obj2, typeof(Transform));
				}
				Transform transform = (Transform)obj3;
				if (transform != this.stablePlatform)
				{
					float x2 = transform.localScale.x * (float)-1;
					Vector3 localScale = transform.localScale;
					float num3 = localScale.x = x2;
					Vector3 vector3 = transform.localScale = localScale;
					UnityRuntimeServices.Update(enumerator, transform);
				}
			}
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0008E364 File Offset: 0x0008C564
	public virtual void Update()
	{
		if ((!EMFDNS.isLocalUser(networkHelper.interactingPlayer) && networkHelper.playerIsInteracting) || !this.moveInX && this.playerScript.groundTransform == this.transform && this.playerScript.dodging)
		{
			this.moveSpeed = this.root.Damp(this.speed, this.moveSpeed, 0.1f);

			if (!networkHelper.playerIsInteracting)
				PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });

			if (EMFDNS.isLocalUser(networkHelper.interactingPlayer))
			{
				float x = this.root.Damp(this.transform.position.x, this.playerScript.transform.position.x, 0.1f);
				Vector3 position = this.playerScript.transform.position;
				float num = position.x = x;
				Vector3 vector = this.playerScript.transform.position = position;
			}
		}
		else
		{
			if (networkHelper.playerIsInteracting)
				if (EMFDNS.isLocalUser(networkHelper.interactingPlayer))
					if (playerScript.groundTransform != this || !playerScript.dodging)
						PacketSender.BaseNetworkedEntityRPC("OnPlayerStopInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });


			if (this.linkScrew != null && (this.linkScrew.moveSpeed > this.moveSpeed || this.alwaysCopyLinkedSpeed))
			{
				this.moveSpeed = -this.linkScrew.moveSpeed;
			}
			else
			{
				this.moveSpeed = this.root.Damp(-this.returnSpeed, this.moveSpeed, 0.1f);
			}
			this.stablePlatform.rotation = this.startRot;
		}
		this.transform.localRotation = this.startRot * Quaternion.Euler((float)0, (float)0, Vector3.Distance(this.startPos, this.transform.position) * (float)-360);
		if (this.moveInX)
		{
			if (this.moveAmount < (float)0)
			{
				float x2 = Mathf.Clamp(this.transform.position.x - this.moveSpeed * this.root.timescale * this.playerScript.speedModifier, this.startPos.x + this.moveAmount, this.startPos.x);
				Vector3 position2 = this.transform.position;
				float num2 = position2.x = x2;
				Vector3 vector2 = this.transform.position = position2;
			}
			else
			{
				float x3 = Mathf.Clamp(this.transform.position.x + this.moveSpeed * this.root.timescale * this.playerScript.speedModifier, this.startPos.x, this.startPos.x + this.moveAmount);
				Vector3 position3 = this.transform.position;
				float num3 = position3.x = x3;
				Vector3 vector3 = this.transform.position = position3;
			}
			this.objectMovementSoundScript.pitchMultiplier = 1.5f + Mathf.Abs(this.startPos.x - this.transform.position.x) / this.moveAmount * (float)-1;
		}
		else
		{
			if (this.moveAmount < (float)0)
			{
				float y = Mathf.Clamp(this.transform.position.y - this.moveSpeed * this.root.timescale * this.playerScript.speedModifier, this.startPos.y + this.moveAmount, this.startPos.y);
				Vector3 position4 = this.transform.position;
				float num4 = position4.y = y;
				Vector3 vector4 = this.transform.position = position4;
			}
			else
			{
				float y2 = Mathf.Clamp(this.transform.position.y + this.moveSpeed * this.root.timescale * this.playerScript.speedModifier, this.startPos.y, this.startPos.y + this.moveAmount);
				Vector3 position5 = this.transform.position;
				float num5 = position5.y = y2;
				Vector3 vector5 = this.transform.position = position5;
			}
			this.objectMovementSoundScript.pitchMultiplier = 1.5f + Mathf.Abs(this.startPos.y - this.transform.position.y) / this.moveAmount * (float)-1;
		}
		this.screwCollision.position = this.transform.position - this.transform.forward;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0008E800 File Offset: 0x0008CA00
	public virtual void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(this.transform.position, this.transform.position + (this.moveInX ? (Vector3.right * this.moveAmount) : (Vector3.up * this.moveAmount)));
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x000020A7 File Offset: 0x000002A7
	public virtual void Main()
	{
	}

	// Token: 0x0400116D RID: 4461
	private RootScript root;

	// Token: 0x0400116E RID: 4462
	private PlayerScript playerScript;

	// Token: 0x0400116F RID: 4463
	private ObjectMovementSoundScript objectMovementSoundScript;

	// Token: 0x04001170 RID: 4464
	public float moveAmount;

	// Token: 0x04001171 RID: 4465
	public bool moveInX;

	// Token: 0x04001172 RID: 4466
	public bool alwaysCopyLinkedSpeed;

	// Token: 0x04001173 RID: 4467
	public float speed;

	// Token: 0x04001174 RID: 4468
	public float returnSpeed;

	// Token: 0x04001175 RID: 4469
	public SpinScrewScript linkScrew;

	// Token: 0x04001176 RID: 4470
	[HideInInspector]
	public bool dontDoLinkFiddle;

	// Token: 0x04001177 RID: 4471
	[HideInInspector]
	public float moveSpeed;

	// Token: 0x04001178 RID: 4472
	private Vector3 startPos;

	// Token: 0x04001179 RID: 4473
	private Quaternion startRot;

	// Token: 0x0400117A RID: 4474
	private Transform stablePlatform;

	// Token: 0x0400117B RID: 4475
	private Transform screwCollision;

	// Token: 0x0400117C RID: 4476
	private float moveSpeedS;
}
