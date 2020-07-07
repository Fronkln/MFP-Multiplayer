using System;
using Steamworks;
using UnityEngine;



//used for very specific cases like
public class NetworkedBaseTransform : BaseNetworkEntity
{
    public bool syncToggle = true;
    public bool doLerpAuto = true;

    public Vector3 realPos = Vector3.zero;

    public override void Awake()
    {
        if (MultiplayerManagerTest.inst.gamemode == MPGamemodes.Race)
        {
            DestroyImmediate(this);
            return;
        }

        base.Awake();
     //   onlyHostWillSync = true;
        maxAllowedPackets = 20;

        MFPEditorUtils.Log("NetworkedBaseTransform awake on" + transform.name);
    }

    public void LateUpdate()
    {
        if (MultiplayerManagerTest.playingAsHost)
        {
            if (syncToggle)
                PacketSender.BaseNetworkedEntityRPC("SyncBaseTransform", entityIdentifier, new object[] { transform.position });
        }
        else
        {
            if (packageVars != null && packageVars.Length > 0)
                realPos = (Vector3)packageVars[0];

            if (realPos != Vector3.zero)
                if(doLerpAuto)
                transform.position = Vector3.Lerp(transform.position, realPos, 0.1f);
            else
                MFPEditorUtils.Log("recieved pos is vector3 zero!");

        }

    }
}

