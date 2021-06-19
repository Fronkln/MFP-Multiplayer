using Steamworks;
using System.Linq;
using UnityEngine;

/// <summary>
/// Extension Methods For DnSpy
/// </summary>
public static class EMFDNS
{
    public static Transform GetNearestPlayer(Vector3 startPos)
    {
        if (MultiplayerManagerTest.singleplayerMode || MultiplayerManagerTest.inst.playerObjects == null || MultiplayerManagerTest.inst.playerObjects.Count == 0)
            return PlayerScript.PlayerInstance.transform;

        float curDist = Mathf.Infinity;
        Transform closest = null;

        for(int i = 0; i < MultiplayerManagerTest.inst.playerObjects.Count; i++)
        {
            MFPPlayerGhost player = MultiplayerManagerTest.inst.playerObjects.ElementAt(i).Value;

            float dist = Vector3.Distance(startPos, player.transform.position);

            if(dist < curDist)
            {
                closest = player.transform;
                curDist = dist;
            }

        }

#if DEBUG
        if (MultiplayerManagerTest.extraDebug)
            Debugging.CreateDisappearingCube(closest.transform.position, Quaternion.identity, new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
#endif

        return closest;
    }

    public static bool isLocalUser(CSteamID user)
    {
        if (user.m_SteamID == MultiplayerManagerTest.inst.playerID.m_SteamID)
            return true;
        else
            return false;
    }

    public static bool isNull(CSteamID user)
    {
        if (user.m_SteamID == 0)
            return true;
        else
            return false;
    }
}

