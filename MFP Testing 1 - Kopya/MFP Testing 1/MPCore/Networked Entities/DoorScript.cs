// Decompiled with JetBrains decompiler
// Type: DoorScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using UnityEngine;

[Serializable]
public class DoorScript : MonoBehaviour
{

    private BaseNetworkEntity networkHelper;

    public float openAmountRight;
    public float openAmountLeft;
    public bool closeAndNeverOpenAgain;
    private bool hasClosedAgain;
    private bool hasBeenOpened;
    private Quaternion startRot;
    private bool open;
    private Quaternion targetRot;
    private bool playerOnRight;
    private Collider objCollider;
    // private Transform mainPlayer { get { return MFPPlayerGhost.localInstance; } } using MP Ghost as a way to calculate distance was probably why doors felt delayed.
    private Transform mainPlayer;
    private RootScript root;
    private AudioSource theAudioSource;

    public DoorScript()
    {
        this.openAmountRight = 90f;
        this.openAmountLeft = 90f;
    }


    public void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    public void Start()
    {

        root = RootScript.RootScriptInstance;
        this.startRot = this.transform.rotation;
        this.targetRot = this.startRot;
        this.objCollider = (Collider)this.GetComponent(typeof(Collider));
        this.mainPlayer = GameObject.Find("Player").transform;
        this.theAudioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
    }


    public void OpenDoor()
    {
        objCollider.enabled = false;
        targetRot = this.startRot * Quaternion.Euler(0.0f, 0.0f, !this.playerOnRight ? -this.openAmountRight : this.openAmountLeft);
        theAudioSource.volume = UnityEngine.Random.Range(0.75f, 0.85f);
        theAudioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        theAudioSource.Play();
        open = true;
    }

    public virtual void Update()
    {
        if (hasClosedAgain)
            return;
        if (EMFDNS.isNull(networkHelper.interactingPlayer))
            playerOnRight = (double)this.mainPlayer.position.x > (double)this.transform.position.x;
        else
            playerOnRight = networkHelper.mpManager.playerObjects[networkHelper.interactingPlayer].transform.position.x > transform.position.x;

        if ((double)this.mainPlayer.position.y > (double)this.transform.position.y && (double)Vector2.Distance(new Vector2(this.mainPlayer.position.x, this.mainPlayer.position.y), new Vector2(this.transform.position.x + (!this.playerOnRight ? 2f : -2f), this.transform.position.y + 1.6f)) < 3.0)
        {
            if (!this.open)
                PacketSender.BaseNetworkedEntityRPC("OpenDoor", networkHelper.entityIdentifier, sendType: Steamworks.EP2PSend.k_EP2PSendUnreliableNoDelay);
        }
        else if (this.closeAndNeverOpenAgain && this.hasBeenOpened && (double)Vector2.Distance((Vector2)this.mainPlayer.position, (Vector2)this.transform.position) > 3.0) // dont sync the vectors so players behind dont get locked out?
        {
            this.objCollider.enabled = true;
            this.open = false;
        }
        if (this.open)
        {
            transform.rotation = root.DampSlerp(targetRot, transform.rotation, 0.2f);
            hasBeenOpened = true;
        }
        else
        {
            if (!this.closeAndNeverOpenAgain || !this.hasBeenOpened)
                return;
            transform.rotation = root.DampSlerp(startRot, transform.rotation, 0.2f);
            if ((double)Quaternion.Angle(this.transform.rotation, this.startRot) >= 1.0)
                return;
            hasClosedAgain = true;
        }
    }
}
