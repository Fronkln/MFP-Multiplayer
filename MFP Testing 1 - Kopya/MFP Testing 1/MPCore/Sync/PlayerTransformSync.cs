using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steamworks;
using UnityEngine;


public static class PlayerTransformSync
{
    public static void HandleSync(P2PMessage packet, CSteamID remoteID)
    {

        MFPPlayerGhost target = MultiplayerManagerTest.inst.playerObjects[remoteID];

        target.realRootPosition = packet.ReadVector3();
        target.realGraphicsPosition = packet.ReadVector3();
        target.realGraphicsRotation = packet.ReadQuaternion();
        target.realCenterPosition = packet.ReadVector3();

        //Player IK
        target.headIKRotation = packet.ReadCompressedQuaternion();
        target.lowerBackIKRotation = packet.ReadCompressedQuaternion();

        target.shoulderRIKRotation = packet.ReadCompressedQuaternion();
        target.upperArmRIKRotation = packet.ReadCompressedQuaternion();
        target.lowerArmRIKRotation = packet.ReadCompressedQuaternion();
        target.lowerHandRIKRotation = packet.ReadCompressedQuaternion();

        target.shoulderLIKRotation = packet.ReadCompressedQuaternion();
        target.upperArmLIKRotation = packet.ReadCompressedQuaternion();
        target.lowerArmLIKRotation = packet.ReadCompressedQuaternion();
        target.lowerHandLIKRotation = packet.ReadCompressedQuaternion();

        target.upperLegLIKRotation = packet.ReadCompressedQuaternion();
        target.lowerLegLIKRotation = packet.ReadCompressedQuaternion();
        target.footLIKRotation = packet.ReadCompressedQuaternion();

        target.upperLegRIKRotation = packet.ReadCompressedQuaternion();
        target.lowerLegRIKRotation = packet.ReadCompressedQuaternion();
        target.footRIKRotation = packet.ReadCompressedQuaternion();



    }
}

