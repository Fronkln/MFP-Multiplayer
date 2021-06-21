using System;
using System.Linq;
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

    //doesnt exist in .net 3.5
    public static T GetCustomAttribute<T>(this Type type) where T : Attribute
    {
        // Send inherit as false if you want the attribute to be searched only on the type. If you want to search the complete inheritance hierarchy, set the parameter to true.
        object[] attributes = type.GetCustomAttributes(false);
        return attributes.OfType<T>().FirstOrDefault();
    }

    public static T GetCustomAttribute<T>(this Type type, bool inherit) where T : Attribute
    {
        object[] attributes = type.GetCustomAttributes(inherit);
        return attributes.OfType<T>().FirstOrDefault();
    }
}

