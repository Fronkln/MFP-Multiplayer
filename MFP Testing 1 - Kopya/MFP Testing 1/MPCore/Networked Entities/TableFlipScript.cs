// Decompiled with JetBrains decompiler
// Type: TableFlipScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using UnityEngine;

[Serializable]
public class TableFlipScript : MonoBehaviour
{

    private BaseNetworkEntity networkHelper;

    public bool flipped;
    public float flipRightMultiplier;
    private bool flippedDoOnce;
    private Quaternion startRot;
    private Vector3 rotPos;
    private Collider objCol;
    private float rotOffset;
    private float rotSpeed;
    private float boundHeight;
    private RootScript root;
    private StatsTrackerScript statsTracker;
    private float deactivateTimer;
    private Transform player;
    private PlayerScript playerScript;
    private float width;
    private OutlineEffect outlineEffect;
    private GameObject directionArrow;
    private AudioSource theAudioSource;

    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    public virtual void Start()
    {
        this.startRot = this.transform.localRotation;
        this.rotPos = this.transform.position;
        this.objCol = (Collider)this.GetComponent(typeof(Collider));
        this.boundHeight = this.objCol.bounds.size.y;
        this.width = this.objCol.bounds.size.x;
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.statsTracker = (StatsTrackerScript)GameObject.Find("RootShared").GetComponent(typeof(StatsTrackerScript));
        this.player = GameObject.Find("Player").transform;
        this.playerScript = (PlayerScript)this.player.GetComponent(typeof(PlayerScript));
        this.outlineEffect = (OutlineEffect)GameObject.Find("Main Camera").GetComponent(typeof(OutlineEffect));
        this.directionArrow = GameObject.Find("HUD/DirectionArrow");
        this.theAudioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
    }


    public virtual void FlipTheTable(bool flipRightMult)
    {
        Vector3 vector3 = this.transform.position - this.player.position;
        flipRightMultiplier = flipRightMult ? -1f : 1f;
        flipped = true;

    }

    public virtual void Update()
    {

        if (this.flipped)
        {
            if (!this.flippedDoOnce)
            {
                this.transform.tag = "Untagged";
                this.flippedDoOnce = true;
                ++this.statsTracker.tablesFlipped;
                this.statsTracker.achievementCheck();
                this.theAudioSource.volume = UnityEngine.Random.Range(0.8f, 1f);
                this.theAudioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                this.theAudioSource.Play();
                this.rotPos.x += this.objCol.bounds.extents.x * 0.9f * this.flipRightMultiplier;
            }
            this.rotOffset = (this.transform.localRotation * Quaternion.Inverse(this.startRot)).eulerAngles.z;
            if ((double)this.rotOffset > 180.0)
                this.rotOffset -= 360f;
            this.rotSpeed += this.root.DampAdd(-87f * this.flipRightMultiplier, this.rotOffset, 0.04f);
            this.rotSpeed = this.root.Damp(0.0f, this.rotSpeed, 0.2f);
            int num = (double)this.transform.localRotation.eulerAngles.x > 180.0 ? 1 : 0;
            if (num != 0)
                num = (double)this.transform.localRotation.eulerAngles.x < 357.0 ? 1 : 0;
            this.transform.RotateAround(num == 0 ? this.rotPos + new Vector3(this.boundHeight * this.flipRightMultiplier, 0.0f, 0.0f) : this.rotPos, Vector3.forward, this.rotSpeed * this.root.timescale);
            this.deactivateTimer += this.root.timescale;
            if ((double)this.deactivateTimer < 40.0)
                return;
            this.flipped = false;
            ((Behaviour)this.GetComponent(typeof(TableFlipScript))).enabled = false;
        }
        else
        {
            Vector3 vector3 = this.transform.position - this.player.position;
            if ((double)vector3.magnitude >= (double)this.width * 1.5 || (double)Mathf.Abs(vector3.x) >= (double)this.width + 2.0 || ((double)vector3.x <= 0.0 || !this.playerScript.faceRight) && ((double)vector3.x >= 0.0 || this.playerScript.faceRight) || (double)this.player.position.x >= (double)this.objCol.bounds.min.x && (double)this.player.position.x <= (double)this.objCol.bounds.max.x)
                return;
            this.root.highlightObject(this.transform, false, this.root.GetTranslation("interact6"), 1.6f);
            this.root.showHintFlipTable = true;
            if ((double)this.playerScript.punchTimer <= 15.0 && !this.playerScript.kUse || !((UnityEngine.Object)this.root.prevOutlinedObject == (UnityEngine.Object)this.transform))
                return;
            if (this.playerScript.kUse)
                this.playerScript.flipTable();

            bool flipRightMult = vector3.x <= 0.0;

            PacketSender.BaseNetworkedEntityRPC("FlipTheTable", networkHelper.entityIdentifier, new object[] { flipRightMult });
            // this.flipRightMultiplier = (double) vector3.x <= 0.0 ? -1f : 1f;
            // this.flipped = true;
        }
    }

    public virtual void OnCollisionStay(Collision col)
    {
        if (!this.flipped || col.gameObject.layer == 13)
            return;
        int num1 = 4;
        Vector3 velocity1 = col.rigidbody.velocity;
        double num2 = (double)(velocity1.y = (float)num1);
        Vector3 vector3_1 = col.rigidbody.velocity = velocity1;
        float num3 = (float)(-(double)this.rotSpeed * 0.300000011920929);
        Vector3 velocity2 = col.rigidbody.velocity;
        double num4 = (double)(velocity2.x = num3);
        Vector3 vector3_2 = col.rigidbody.velocity = velocity2;
        float rotSpeed = this.rotSpeed;
        Vector3 angularVelocity = col.rigidbody.angularVelocity;
        double num5 = (double)(angularVelocity.z = rotSpeed);
        Vector3 vector3_3 = col.rigidbody.angularVelocity = angularVelocity;
    }
}
    