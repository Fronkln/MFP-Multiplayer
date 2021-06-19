using System;
using Steamworks;
using UnityEngine;



//used for very specific cases like
public class NetworkedBaseTransform : BaseNetworkEntity
{
    public bool syncToggle = true;
    public bool doLerpAuto = true;

    public Vector3 realPos = Vector3.zero;


    private EnemyScript attachedEnemy;
    //we need this so that when enemy dies, we can disable it
    //doing it on EnemyScript.cs and OnEnemyDeath causes a crash somehow

    public override void Awake()
    {
        if (MultiplayerManagerTest.inst.gamemode == MPGamemodes.Race)
        {
            DestroyImmediate(this);
            return;
        }

        attachedEnemy = transform.GetComponent<EnemyScript>();

        base.Awake();
     //   onlyHostWillSync = true;
        maxAllowedPackets = 20;

        MFPEditorUtils.Log("NetworkedBaseTransform awake on" + transform.name);
    }

    public override void Update()
    {
        base.Update();

        if (attachedEnemy != null)
            if (attachedEnemy.health <= 0)
                enabled = false;
            

        if (MultiplayerManagerTest.playingAsHost)
        {
            if (syncToggle)
                PacketSender.BaseNetworkedEntityRPC("SyncBaseTransform", entityIdentifier, new object[] { transform.position });
        }
        else
        {
            if (packageVars != null && packageVars.Length > 0)
                realPos = (Vector3)packageVars[0];

            if (doLerpAuto)
                DoLerp();
        }

    }

    public void DoLerp()
    {
        if (MultiplayerManagerTest.playingAsHost)
            return;

        if (realPos != Vector3.zero)
                transform.position = Vector3.Lerp(transform.position, realPos, 0.1f);
            else
                MFPEditorUtils.Log("recieved pos is vector3 zero!");
    }
}

