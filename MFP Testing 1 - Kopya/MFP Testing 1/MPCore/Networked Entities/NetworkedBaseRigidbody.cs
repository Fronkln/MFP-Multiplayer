using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//this in theory should sync rigidbodies (even if a lil bit choppy)
//all clients can send these for now, but don't forget to move this to server later on!
//when you sync kicking ofcourse
public class NetworkedBaseRigidbody : BaseNetworkEntity
{
    public Rigidbody rBody;

    public Vector3 recievedPos;
    public Quaternion recievedRot;
    public Vector3 recievedVelocity;
    public Vector3 recievedAngularVelocity;

    public float minMagnitudetoSync = 0.2f;

    private bool hostOnlySync = false;

    private bool moving = false;

    public override void Awake()
    {
        base.Awake();
        maxAllowedPackets = 12;

        rBody = GetComponent<Rigidbody>();
    }

    public void ReadPackage(P2PMessage package)
    {
        recievedPos = package.ReadVector3();
        recievedRot = package.ReadCompressedQuaternion();
        recievedVelocity = package.ReadVector3();
        recievedAngularVelocity = package.ReadVector3();
    }

    public override void Update()
    {
        base.Update();

        if (MultiplayerManagerTest.singleplayerMode)
            return;

        if (moving && !interactingPlayer.isLocalUser())
            moving = false;

        if (recievedPos != Vector3.zero && interactingPlayer != null && !interactingPlayer.isLocalUser())
        {
            transform.position = Vector3.Lerp(transform.position, recievedPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, recievedRot, Time.deltaTime * 5);
            rBody.velocity = recievedVelocity;
            rBody.angularVelocity = recievedAngularVelocity;
        }

        float magnitude = rBody.velocity.magnitude;

        //apparently magnitude is expensive... try finding an alternative later
        if (magnitude >= minMagnitudetoSync || magnitude <= -minMagnitudetoSync)
        {
            if (interactingPlayer.isNull() && MultiplayerManagerTest.playingAsHost)
                PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID });

            if (interactingPlayer.isLocalUser())
            {
                if (!moving)
                {
                    moving = true;
                    MFPEditorUtils.Log("Rigidbody start sync");
                }
                PacketSender.BaseNetworkedEntityRPC("ReadPackage", entityIdentifier, new object[] { transform.position, transform.rotation, rBody.velocity, rBody.angularVelocity });
            }

        }

        if (moving && magnitude > -minMagnitudetoSync && magnitude < minMagnitudetoSync)
        {
            moving = false;
            ignoreMaxPacketsDoOnce = true;
            PacketSender.BaseNetworkedEntityRPC("OnPlayerStopInteract", entityIdentifier);
        }

    }
}

