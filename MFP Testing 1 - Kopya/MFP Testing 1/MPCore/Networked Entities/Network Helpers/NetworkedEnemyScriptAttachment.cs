/*
 * PURPOSE: A SCRIPT FILE THAT WILL WORK TOGETHER WITH ENEMYSCRIPT
 * TO HANDLE NPC SYNCHRONIZATION, METHODS WILL PROBABLY BE
 * VERY SIMILIAR TO BASENETWORKEDENTITY
 */

using Steamworks;
using UnityEngine;


public class NetworkedEnemyScriptAttachment : BaseNetworkEntity
{
    private EnemyScript enemyScript;

    public override void Awake()
    {
        base.Awake();

        enemyScript = GetComponent<EnemyScript>();
    }

    public override void OnPlayerStartInteract(ulong activator)
    {
        base.OnPlayerStartInteract(activator);

        enemyScript.idle = false;
        enemyScript.wasActivatedByPlayers = true;
        enemyScript.hasBeenOnScreen = true;
        enemyScript.alertAmount = 0.11f;
       // enemyScript.runLogic = true;
        //enemyScript.playerDetectionRadius = 999;
        //enemyScript.playerShootingDetectionRadius = 999;
        MultiplayerManagerTest.inst.root.enemyEngagedWithPlayer = true;
        MFPEditorUtils.Log("Enemy got activated");
        
    }

    public void OnEnemyDeath()
    {
        /* 
             this causes a crash
        if (enemyScript.specialEnemySyncer != null) 
            enemyScript.specialEnemySyncer.enabled = false;
        */

        if (enemyScript.idle) enemyScript.idle = false;
        if (!enemyScript.runLogic) enemyScript.runLogic = true;
        if (enemyScript.alertAmount <= 0) enemyScript.alertAmount = 1;

        enemyScript.health = 0;
    }
}

