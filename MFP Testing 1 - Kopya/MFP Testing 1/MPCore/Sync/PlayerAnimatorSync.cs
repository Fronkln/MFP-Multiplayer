using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steamworks;
using UnityEngine;


public static class PlayerAnimatorSync
{
    public static void HandleSync(P2PMessage packet, CSteamID remoteID)
    {
        Animator targetAnimator = MultiplayerManagerTest.inst.playerObjects[remoteID].playerAnimator;

        //maybe send the bools as bytes and read them?
        //match the Animator order in Pedro MP test player animator

        targetAnimator.SetFloat("xSpeed", packet.ReadFloat());
        targetAnimator.SetBool("InAir", packet.ReadBool());
        targetAnimator.SetBool("JustJumped", packet.ReadBool());
        targetAnimator.SetFloat("IdleNr", packet.ReadFloat());

        if (!MultiplayerManagerTest.inst.isMotorcycleLevel)
            targetAnimator.SetBool("Dodging", packet.ReadBool());

        if (!MultiplayerManagerTest.inst.isMotorcycleLevel && !MultiplayerManagerTest.inst.isSkyfallLevel)
        {
            targetAnimator.SetBool("Crouching", packet.ReadBool());
            targetAnimator.SetFloat("CrouchAmount", packet.ReadFloat());
            targetAnimator.SetFloat("SideStepAmount", packet.ReadFloat());
            targetAnimator.SetBool("Rolling", packet.ReadBool());
        }

        if (MultiplayerManagerTest.inst.isMotorcycleLevel)
            targetAnimator.SetFloat("MotorcycleAim", packet.ReadFloat());

        //TODO for optimization? oyuncunun kaykayda olup olmadığını syncle, ona göre veri oku ve yolla

        //  MultiplayerManagerTest.inst.playerObjects[remoteID].transform.position = packet.ReadVector3();
        // MultiplayerManagerTest.inst.playerObjects[remoteID].transform.GetChild(0).rotation = packet.ReadQuaternion();
    }
}

