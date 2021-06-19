using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steamworks;
using UnityEngine;

public static class PlayerGunSync
{
    public static void HandleGunSoundSync(P2PMessage packet, CSteamID remoteID)
    {
        bool isRight = packet.ReadBool();
        bool dualAiming = packet.ReadBool();

        MultiplayerManagerTest.inst.playerObjects[remoteID].playGunSound(isRight, dualAiming);
    }

    public static void HandleGunSwitch(P2PMessage packet, CSteamID remoteID)
    {
        if (!MultiplayerManagerTest.inst.playerObjects.ContainsKey(remoteID))
            return;

        byte weapon = packet.ReadByte();
        MultiplayerManagerTest.inst.playerObjects[remoteID].ChangeWeapon(weapon);
    }
}

