using System;
using UnityEngine;

[Serializable]
public class PlatformLiftScript : MonoBehaviour
{

    private BaseNetworkEntity networkHandler;

    public bool usePhysics;
    [HideInInspector]
    public float ySpeed;
    public float yMoveAmount;
    public float maxUpSpeed;
    public float maxDownSpeed;
    private Vector3 startPos;
    private Rigidbody rBody;
    private RootScript root;
    private Transform rope;
    private PlayerScript playerScript;

    public PlatformLiftScript()
    {
        yMoveAmount = 4.5f;
        maxUpSpeed = 5f;
        maxDownSpeed = 2.5f;
    }

    public virtual void Awake()
    {
        networkHandler = gameObject.AddComponent<BaseNetworkEntity>();
        networkHandler.maxAllowedPackets = 3;
    }

    public virtual void Start()
    {
        startPos = transform.position;
        rBody = (Rigidbody)this.GetComponent(typeof(Rigidbody));
        root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        rope = this.transform.parent.Find("rope");
        playerScript = (PlayerScript)GameObject.Find("Player").GetComponent(typeof(PlayerScript));
        if (usePhysics)
            return;
        DestroyImmediate(rBody);
    }

    public virtual void Update()
    {
        this.ySpeed += 0.05f * this.root.timescale;
        if (networkHandler.interactingPlayer.m_SteamID != 0 || this.playerScript.onGround && (UnityEngine.Object)this.playerScript.groundTransform == (UnityEngine.Object)this.transform)
        {
            this.ySpeed -= 0.1f * this.root.timescale;
            this.playerScript.followSpeed.y = this.ySpeed;
            if (this.usePhysics && this.rBody.IsSleeping())
                this.rBody.WakeUp();

            if (networkHandler.interactingPlayer.m_SteamID == 0)
                PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHandler.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID });
            else
            {
                if (/*networkHandler.interactingPlayer.isLocalUser()*/EMFDNS.isLocalUser(networkHandler.interactingPlayer))
                {
                    if (!playerScript.onGround || playerScript.groundTransform != transform)
                        PacketSender.BaseNetworkedEntityRPC("OnPlayerStopInteract", networkHandler.entityIdentifier);
                }
            }
        }
        if ((double)this.transform.position.y > (double)this.startPos.y)
        {
            this.ySpeed = (double)this.ySpeed >= 1.0 ? Mathf.Abs(this.ySpeed) * -0.4f : 0.0f;
            float y = this.startPos.y;
            Vector3 position = this.transform.position;
            double num = (double)(position.y = y);
            Vector3 vector3 = this.transform.position = position;
        }
        if ((double)this.ySpeed < -(double)this.maxDownSpeed)
            this.ySpeed = -this.maxDownSpeed;
        else if ((double)this.ySpeed > (double)this.maxUpSpeed)
            this.ySpeed = this.maxUpSpeed;
        if (this.usePhysics)
        {
            float num1 = this.ySpeed * this.root.timescaleRaw;
            Vector3 velocity = this.rBody.velocity;
            double num2 = (double)(velocity.y = num1);
            Vector3 vector3 = this.rBody.velocity = velocity;
        }
        else
        {
            float num1 = this.transform.position.y + this.ySpeed / 60f * this.root.timescale;
            Vector3 position = this.transform.position;
            double num2 = (double)(position.y = num1);
            Vector3 vector3 = this.transform.position = position;
        }
        if ((double)this.transform.position.y < (double)this.startPos.y - (double)this.yMoveAmount)
        {
            this.ySpeed = 0.0f;
            float num1 = this.startPos.y - this.yMoveAmount;
            Vector3 position = this.transform.position;
            double num2 = (double)(position.y = num1);
            Vector3 vector3 = this.transform.position = position;
        }
        float num3 = this.rope.position.y - this.transform.position.y - 5f;
        Vector3 localScale = this.rope.localScale;
        double num4 = (double)(localScale.z = num3);
        Vector3 vector3_1 = this.rope.localScale = localScale;
    }

    public virtual void FixedUpdate()
    {
        if (!this.usePhysics || !this.playerScript.onGround || (!((UnityEngine.Object)this.playerScript.groundTransform == (UnityEngine.Object)this.transform) || !this.rBody.IsSleeping()))
            return;
        this.rBody.WakeUp();
    }


    public virtual void OnDrawGizmosSelected()
    {
        if ((double)Time.time != 0.0)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - this.yMoveAmount, this.transform.position.z));
    }
}
