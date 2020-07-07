// Decompiled with JetBrains decompiler
// Type: EnemyChairScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using UnityEngine;

[Serializable]
public class EnemyChairScript : MonoBehaviour
{
    private BaseNetworkEntity networkHelper;

    private RootScript root;
    public bool triggered;
    public EnemyScript targetEnemy;
    public bool fallOver;
    private Quaternion startRot;
    private Vector3 startPos;
    private Vector3 moveDir;
    private float rotSpeed;
    private float rotAmount;

    public void EnemyChairTriggered()
    {
        targetEnemy.alertAmount = 0.100000001490116f;
        targetEnemy.idle = false;
        triggered = true;
    }


    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.startRot = this.transform.rotation;
        this.startPos = this.transform.position;
        this.moveDir = this.transform.up;

        if (this.targetEnemy.idleAnim == (string)null || this.targetEnemy.idleAnim == string.Empty)
            this.targetEnemy.idleAnim = "Idle_Sitting";
        if (this.targetEnemy.neverReturnToIdleAnim)
        {
            this.fallOver = true;
        }
        else
        {
            if (!this.fallOver)
                return;
            this.targetEnemy.neverReturnToIdleAnim = true;
        }
    }

    public virtual void Update()
    {
        if (((double)this.targetEnemy.alertAmount >= 0.100000001490116 || !this.targetEnemy.idle) && !this.triggered)
            //this.triggered = true;
            PacketSender.BaseNetworkedEntityRPC("EnemyChairTriggered", networkHelper.entityIdentifier, sendType: Steamworks.EP2PSend.k_EP2PSendUnreliable);

        if (this.triggered)
        {
            if (this.fallOver)
            {
                this.rotSpeed -= 0.5f * this.root.timescale;
                this.rotAmount += this.rotSpeed * this.root.timescale;
                if ((double)this.rotAmount < -90.0)
                {
                    this.rotAmount = -90f;
                    this.rotSpeed *= -0.5f;
                }
                this.transform.rotation = Quaternion.Euler(this.startRot.eulerAngles.x + this.rotAmount, this.startRot.eulerAngles.y, this.startRot.eulerAngles.z);
                this.transform.position = this.root.DampV3(this.startPos + this.moveDir * 0.5f + Vector3.forward * 1.25f, this.transform.position, 0.4f);
            }
            else
            {
                if ((double)this.targetEnemy.alertAmount < 0.100000001490116)
                    this.triggered = false;
                this.transform.position = this.root.DampV3(this.startPos + Vector3.forward * 1.3f, this.transform.position, 0.4f);
                this.transform.rotation = this.root.DampSlerp(Quaternion.Euler(270f, 180f, 0.0f), this.transform.rotation, 0.3f);
            }
        }
        else
        {
            this.transform.position = this.root.DampV3(this.startPos, this.transform.position, 0.2f);
            this.transform.rotation = this.root.DampSlerp(this.startRot, this.transform.rotation, 0.1f);
        }
    }
}
