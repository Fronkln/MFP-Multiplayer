using System;
using UnityEngine.UI;
using Steamworks;


static class MPExtensions
{
    public static Text buttonText(this Button button)
    {
        return button.GetComponentInChildren<Text>();
    }

    public static bool isLocalUser(this CSteamID user)
    {
        if (user.m_SteamID == MultiplayerManagerTest.inst.playerID.m_SteamID)
            return true;
        else
            return false;
    }

    public static bool singleplayer()
    {
        if (MultiplayerManagerTest.inst != null)
            return !MultiplayerManagerTest.connected;
        else
            return true;
    }

    public static bool isNull(this CSteamID user)
    {
        if (user.m_SteamID == 0)
            return true;
        else
            return false;
    }
}

