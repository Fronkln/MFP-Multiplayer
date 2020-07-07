using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steamworks;
using UnityEngine;

public static class PacketSender
{
    private static MultiplayerManagerTest mpManager { get { return MultiplayerManagerTest.inst; } }


    public static void SendToServer(P2PMessage msg, EP2PSend sendType)
    {
        byte[] bytes = msg.GetBytes();
        SteamNetworking.SendP2PPacket(MultiplayerManagerTest.lobbyOwner, bytes, (uint)bytes.Length, sendType);
    }


    public static void SendToSelf(P2PMessage packet, EP2PSend sendMode = EP2PSend.k_EP2PSendReliable) //Used for race mode
    {
        byte[] bytes = packet.GetBytes();
        SteamNetworking.SendP2PPacket(MultiplayerManagerTest.inst.playerID, bytes, (uint)bytes.Length, sendMode);
    }

    public static void SendPackageToEveryone(P2PMessage packet, EP2PSend sendMode = EP2PSend.k_EP2PSendReliable)
    {
        byte[] bytes = packet.GetBytes();

        for (int i = 0; i < SteamMatchmaking.GetNumLobbyMembers(mpManager.globalID); i++)
        {
            CSteamID reciever = SteamMatchmaking.GetLobbyMemberByIndex(mpManager.globalID, i);
            if (SteamNetworking.SendP2PPacket(reciever, bytes, (uint)bytes.Length, sendMode))
            { }
            else
                MFPEditorUtils.LogError("Packet sending failed!");
        }
    }

    public static void SendToClients(P2PMessage packet, EP2PSend sendMode = EP2PSend.k_EP2PSendReliable)
    {
        byte[] bytes = packet.GetBytes();

        for (int i = 0; i < SteamMatchmaking.GetNumLobbyMembers(MultiplayerManagerTest.lobbyID); i++)
        {
            CSteamID reciever = SteamMatchmaking.GetLobbyMemberByIndex(mpManager.globalID, i);

            if (reciever != MultiplayerManagerTest.lobbyOwner)
            {

                if (SteamNetworking.SendP2PPacket(reciever, bytes, (uint)bytes.Length, sendMode))
                { }
                else
                    MFPEditorUtils.LogError("Packet sending failed!");
            }
        }
    }

    public static void SendPlayerTransform() //65 bytes, very expensive. is there any way we can optimize this?
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(1);

        MFPPlayerGhost player = MultiplayerManagerTest.inst.playerObjects[MultiplayerManagerTest.inst.playerID];
        PlayerScript playerScript = PlayerScript.PlayerInstance;

        packet.WriteVector3(playerScript.transform.position);
        packet.WriteVector3(playerScript.transform.GetChild(0).transform.position);
        packet.WriteQuaternion(playerScript.transform.GetChild(0).rotation);

        packet.WriteVector3(playerScript.center.transform.position);

        //Player IK
        packet.WriteCompressedQuaternion(playerScript.headPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.lowerBackPublic.transform.rotation);

        packet.WriteCompressedQuaternion(playerScript.shoulderRPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.upperArmRPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.lowerArmRPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.handRPublic.transform.rotation);

        packet.WriteCompressedQuaternion(playerScript.shoulderLPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.upperArmLPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.lowerArmLPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.handLPublic.transform.rotation);

        packet.WriteCompressedQuaternion(playerScript.upperLegLPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.lowerLegLPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.footLPublic.transform.rotation);

        packet.WriteCompressedQuaternion(playerScript.upperLegRPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.lowerLegRPublic.transform.rotation);
        packet.WriteCompressedQuaternion(playerScript.footRPublic.transform.rotation);
 




        //      if(MultiplayerManagerTest.inst.isMotorcycleLevel)
        //      packet

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendUnreliable);
    }


    public static void SendHostSpeechScriptClick()
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(13);
        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendUnreliable);
    }

    public static void SendHostSpeechScriptState(bool state)
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(15);
        packet.WriteBool(state);
        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendUnreliable);
    }

    public static void SendTransitionReadyMessage() //can also be used for lobby level load
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(9);

        packet.WriteBool(true);
        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendReliable);
    }

    public static void ForceClientsNextLevel()
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(16);

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendReliable);
    }

    public static void TransitionClientsToNextLevel()
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(10);

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendReliable);
    }

    public static void AllClientsLoadLobbyLevel()
    {
        if (!MultiplayerManagerTest.playingAsHost)
            return;

        P2PMessage packet = new P2PMessage();
        packet.WriteByte(14);

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendReliable); 
    }


    public static void SendPlayerAnimator() //Normal level: 21 bytes Skyfall: 10 bytes Motorcycle: 14 bytes
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(2);

        packet.WriteFloat(PlayerScript.PlayerInstance.playerAnimator.GetFloat("xSpeed"));
        packet.WriteBool(PlayerScript.PlayerInstance.playerAnimator.GetBool("InAir"));
        packet.WriteBool(PlayerScript.PlayerInstance.playerAnimator.GetBool("JustJumped"));
        packet.WriteFloat(PlayerScript.PlayerInstance.playerAnimator.GetFloat("IdleNr"));


        if (!MultiplayerManagerTest.inst.isMotorcycleLevel)
            packet.WriteBool(PlayerScript.PlayerInstance.playerAnimator.GetBool("Dodging"));

        if(!MultiplayerManagerTest.inst.isMotorcycleLevel && !MultiplayerManagerTest.inst.isSkyfallLevel)
        {
            packet.WriteBool(PlayerScript.PlayerInstance.playerAnimator.GetBool("Crouching"));
            packet.WriteFloat(PlayerScript.PlayerInstance.playerAnimator.GetFloat("CrouchAmount"));
            packet.WriteFloat(PlayerScript.PlayerInstance.playerAnimator.GetFloat("SideStepAmount"));
            packet.WriteBool(PlayerScript.PlayerInstance.playerAnimator.GetBool("Rolling"));
        }

        if (MultiplayerManagerTest.inst.isMotorcycleLevel)
            packet.WriteFloat(PlayerScript.PlayerInstance.playerAnimator.GetFloat("MotorcycleAim"));

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendUnreliable);
    }


    public static void SendPlayerMachineGunGrenade(Vector3 position, Quaternion rotation, int num1Rnd)
    {

        P2PMessage packet = new P2PMessage();
        packet.WriteByte(11);

        PlayerScript player = PlayerScript.PlayerInstance;

        packet.WriteVector3(position);
        packet.WriteCompressedQuaternion(rotation);
        packet.WriteInteger(num1Rnd);

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendUnreliable);


    }

    public static void SendGenericGunfire(Vector3 shootVec, Quaternion shootRot, float quaternionRnd, float spread, bool dual = false)
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(4);
        packet.WriteVector3(shootVec);
        packet.WriteQuaternion(shootRot);
        packet.WriteFloat(quaternionRnd);
        packet.WriteFloat(spread);

        if (dual)
            packet.WriteByte(Convert.ToByte(PlayerScript.PlayerInstance.fireLeftGunPublic));

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendUnreliableNoDelay);
    }

    public static void SendPlayerWeapon()
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(3);
        packet.WriteByte((byte)PlayerScript.PlayerInstance.weapon);

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendUnreliable);
    }

    public static void SendPlayerGunSound(bool isRight, bool dualAim)
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(5);
        packet.WriteByte(Convert.ToByte(isRight));
        packet.WriteByte(Convert.ToByte(dualAim));

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendUnreliable);
    }

    public static void SendPlayerLifeState(bool alive)
    {
        P2PMessage packet = new P2PMessage();

        if (alive)
            packet.WriteByte(6);
        else
            packet.WriteByte(7);

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendUnreliable);
    }


    public static void SendPlayerKickObject(Vector3 vector4, Vector3 footKickCalculation, int kickedRBodyID, int autoTargetTransformID = -1)
    {

        P2PMessage packet = new P2PMessage();

        packet.WriteByte(12);

        packet.WriteVector3(vector4);
        packet.WriteVector3(footKickCalculation);
        packet.WriteInteger(kickedRBodyID);
        packet.WriteInteger(autoTargetTransformID);

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendReliable);
    }

    //ROOTSCRIPT SYNC
    public static void SendRootExplosion(Vector3 pos, float size, int amount, Vector3 vel, string explColor, bool turnOffCollision, bool doSound) //ONLY USE IN FEWER CASES! NOT VERY OPTIMIZED! 39-45 bytes
    {
        P2PMessage packet = new P2PMessage();

        packet.WriteByte(8);

        packet.WriteVector3(pos);
        packet.WriteFloat(size);
        packet.WriteShort(Convert.ToInt16(amount));
        packet.WriteVector3(vel);
        packet.WriteUnicodeString(explColor);
        packet.WriteBool(turnOffCollision);
        packet.WriteBool(doSound);

        SendPackageToEveryone(packet, EP2PSend.k_EP2PSendReliable);
    }

    //NETWORKED OBJECTS


    public static void RequestBaseNetworkedEntityActivation(int entityID, object[] parameters = null, EP2PSend sendType = EP2PSend.k_EP2PSendReliable)
    {
        P2PMessage packet = new P2PMessage();
        packet.WriteByte(252);

        packet.WriteInteger(entityID);

        if (parameters != null)
            for (int i = 0; i < parameters.Length; i++)
                packet.WriteObject(parameters[i]);

        SendToServer(packet, sendType);
    }

    //THIS DOESNT WORK BECAUSE OF INCONSISTENCY BETWEEN TWO CLIENTS, TO PINPOINT THE ISSUE, LOG THE BASENETWORKENTITIES OF ALL CLIENTS
    public static void BaseNetworkedEntityRPC(string rpcFunction, int entityID, object[] parameters = null, EP2PSend sendType = EP2PSend.k_EP2PSendReliable)
    {
        if (MultiplayerManagerTest.inst.networkedBaseEntities[entityID].PacketsExceeded())
        {
            MFPEditorUtils.Log("Packet limit exceeded " + MultiplayerManagerTest.inst.networkedBaseEntities[entityID].transform.name);
            return;
        }

     //   if(MultiplayerManagerTest.inst.networkedBaseEntities[entityID].ignoreMaxPacketsDoOnce)
         //   MultiplayerManagerTest.inst.networkedBaseEntities[entityID].ignoreMaxPacketsDoOnce = false;

        short rpcNumber = -1;
        rpcNumber = NetworkedBaseRPCFunctions.ReturnRPCID(rpcFunction);

        if(rpcNumber == -1)
        {
            MFPEditorUtils.Log("Function " + rpcFunction + " does not exist in RPC list!");
            return;
        }

        P2PMessage packet = new P2PMessage();
        packet.WriteByte(253);

        packet.WriteInteger(entityID);
        packet.WriteShort(rpcNumber);

        if (parameters != null)
            for (int i = 0; i < parameters.Length; i++)
                packet.WriteObject(parameters[i]);

        BaseNetworkEntity baseNetEnt = MultiplayerManagerTest.inst.networkedBaseEntities[entityID];

        baseNetEnt.curPackets++;


        if (!baseNetEnt.onlyHostWillSync)
            SendPackageToEveryone(packet, sendType);
        else
            SendToClients(packet, sendType);
    }


    //ANY CHANGES U MAKE TO BASENETWORKEDNPC RPC YOU PROBABLY WANNA DO ON THIS AS WELL
    public static void NetworkedEnemyRPC(string rpcFunction, int npcID, object[] parameters = null, EP2PSend sendType = EP2PSend.k_EP2PSendReliable, bool sendToServer = false)
    {
        if (MultiplayerManagerTest.inst.networkedEnemies[npcID].PacketsExceeded())
        {
            MFPEditorUtils.Log("Packet limit exceeded " + MultiplayerManagerTest.inst.networkedBaseEntities[npcID].transform.name);
            return;
        }

        short rpcNumber = -1;
        rpcNumber = NetworkedBaseRPCFunctions.ReturnRPCID(rpcFunction);

        if (rpcNumber == -1)
        {
            MFPEditorUtils.Log("Function " + rpcFunction + " does not exist in RPC list!");
            return;
        }

        P2PMessage packet = new P2PMessage();
        packet.WriteByte(253);

        packet.WriteInteger(npcID);
        packet.WriteShort(rpcNumber);

        if (parameters != null)
            for (int i = 0; i < parameters.Length; i++)
                packet.WriteObject(parameters[i]);

        MultiplayerManagerTest.inst.networkedBaseEntities[npcID].curPackets++;

        if (!sendToServer)
            SendPackageToEveryone(packet, sendType);
        else
            SendToServer(packet, sendType);
    }

}

