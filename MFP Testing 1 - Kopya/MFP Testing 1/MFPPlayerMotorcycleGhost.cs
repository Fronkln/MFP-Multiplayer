using System;
using UnityEngine;

public class MFPPlayerMotorcycleGhost : BaseNetworkEntity
{
    public MFPPlayerGhost targetGhost;

    private Quaternion startRot;
    private bool fixRotationDoOnce = true;

    public override void Awake()
    {
        MotorcycleScript motoScript = GetComponent<MotorcycleScript>();

        if (motoScript != null) DestroyImmediate(motoScript);

        foreach (SphereCollider sphereColl in GetComponents<SphereCollider>())
            DestroyImmediate(sphereColl);

        foreach (SphereCollider sphereColl in transform.GetChild(0).GetComponents<SphereCollider>())
            DestroyImmediate(sphereColl);

        foreach (CapsuleCollider capsuleColl in GetComponents<CapsuleCollider>())
            DestroyImmediate(capsuleColl);

        foreach (CapsuleCollider capsuleColl in transform.GetChild(0).GetComponents<CapsuleCollider>())
            DestroyImmediate(capsuleColl);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) DestroyImmediate(rb);

        base.Awake();
    }

    public override void Start()
    {
        AudioSource motorcycleSound = GetComponent<AudioSource>();

        if(targetGhost == null)
        {
            MFPEditorUtils.Log("TargetGhost for motorcycle is null");
            enabled = false;
        }

        if (targetGhost.owner.isLocalUser())
            motorcycleSound.enabled = false;

        startRot = transform.rotation;

        transform.position = new Vector3(targetGhost.transform.position.x + 0.3f, targetGhost.transform.position.y- 0.8f, targetGhost.transform.position.z - 0.6f);
        transform.SetParent(targetGhost.transform.GetChild(0), true);

        transform.Rotate(0, 0, 90);

    }

    public override void Update()
    {
        base.Update();

        //if (targetGhost != null)
          //  transform.position = new Vector3(targetGhost.transform.position.x - 0.25f, targetGhost.transform.position.y - 2, targetGhost.transform.position.z);
    }
}

