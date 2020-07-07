using System;
using UnityEngine;

[Serializable]
public class VentShaftScript : MonoBehaviour
{
    private BaseNetworkEntity networkHelper;

    private RootScript root;
    private float fakeRot;
    private float rotSpeed;
    private bool activated;
    private AudioSource theAudioSource;

    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.theAudioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
    }

    public virtual void Update()
    {
        if (!EMFDNS.isNull(networkHelper.interactingPlayer) && !activated)
            activated = true;

        if (!this.activated)
            return;
        this.rotSpeed += this.root.DampAdd(-108f, this.fakeRot, 0.015f);
        this.rotSpeed *= Mathf.Pow(0.97f, this.root.timescale);
        this.fakeRot += this.rotSpeed * this.root.timescale;
        this.transform.rotation = Quaternion.Euler(this.fakeRot, 0.0f, 0.0f);
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        if (this.activated)
            return;
        ((Collider)this.GetComponent(typeof(BoxCollider))).enabled = false;
        ((Collider)this.transform.Find("VentGraphics").GetComponent(typeof(BoxCollider))).enabled = false;
        this.theAudioSource.Play();
        PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", networkHelper.entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID });
    //    this.activated = true;
    }
}
