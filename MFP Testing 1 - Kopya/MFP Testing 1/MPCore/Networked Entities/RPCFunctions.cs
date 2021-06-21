using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class NetworkedBaseRPCFunctions
{


    /*
     
        MAYBE TODO??

       AN OBJECT ARRAY FOR RPCS THAT EXECUTE PER FRAME (LIKE RIGIDBODY SYNC) COULD BE ADDED
       INSTEAD OF GETCOMPONENTING THE SPECIFIC CLASS, WE UPDATE THE OBJECT ARRAY AND THE CLASS READS IT

    */

    public static void DoRPC(BaseNetworkEntity entity, Steamworks.CSteamID sender, short functionID, P2PMessage packet = null)
    {

        //Happens in skyfall for some reason!
        if (entity == null)
            return;

        try
        {


            switch (functionID)
            {
                //PEDRO'S WORLD TEST RPC
                case 0:
                    entity.GetComponent<DisappearPlatformScript>().DisappearPlatform();
                    break;
                //PLATFORM LIFT SPEED SYNC ---------------UNUSED
                case 1:
                    //    entity.GetComponent<NetworkedPlatformLiftScript>().SyncLift(packet);
                    break;
                case 2:
                    entity.OnPlayerStartInteract(packet.ReadUlong());
                    break;
                case 3:
                    entity.OnPlayerStopInteract();
                    break;
                case 4: //----------------------------------------------------UNUSED
                        //entity.GetComponent<NetworkedPlatformLiftScript>().MoveLift();
                    break;
                case 5:
                    entity.GetComponent<TableFlipScript>().FlipTheTable(packet.ReadBool()); //if true its -1 if false its 1
                    break;
                case 6:
                    entity.GetComponent<DoorScript>().OpenDoor();
                    break;
                //NetworkedBaseRigidbody Sync
                case 7:
                    entity.GetComponent<NetworkedBaseRigidbody>().ReadPackage(packet);
                    break;
                case 8:
                    entity.GetComponent<LaserDetectorScript>().OnLaserTriggered();
                    break;
                case 9:
                    entity.GetComponent<EnemyChairScript>().EnemyChairTriggered();
                    break;
                case 10:
                    entity.GetComponent<NetworkedGasCanisterScript>().GasCanisterExplode();
                    break;
                case 11:
                    entity.OnHostActivatedEntity();
                    break;
                case 12:
                    entity.TestRPC(packet.ReadUlong());
                    break;
                case 13:
                    entity.GetComponent<NetworkedEnemyScriptAttachment>().OnEnemyDeath();
                    break;
                case 14:
                    entity.GetComponent<GunTurretScript>().OnTurretDetectPlayer();
                    break;
                case 15:
                    entity.GetComponent<SpeechTriggerControllerScript>().TriggerTheSpeech(sender);
                    break;
                case 16:
                    entity.packageVars = new object[1];
                    entity.packageVars[0] = packet.ReadVector3();
                    break;

                case 17:

                    entity.packageVars = new object[6];

                    entity.packageVars[0] = packet.ReadFloat();
                    entity.packageVars[1] = packet.ReadInteger();
                    entity.packageVars[2] = packet.ReadInteger();
                    entity.packageVars[3] = packet.ReadFloat();
                    entity.packageVars[4] = packet.ReadFloat();
                    entity.packageVars[5] = packet.ReadInteger();

                    MFPEditorUtils.Log("Pedro boss sync");

                    break;

                case 18: //execute state
                    entity.ExecuteState(packet.ReadByte());
                    break;

                case 19:
                    entity.GetComponent<LevelChangerScript>().OnClientReachLevelEnd((Steamworks.CSteamID)packet.ReadUlong());
                    break;
                case 20:
                    entity.GetComponent<BouncePadScript>().doVisualThing();
                    break;

            }
        }
        catch(Exception ex)
        {
            MFPEditorUtils.LogError("RPC " + ReturnRPCName(functionID) + " failed!: " + ex.Message);
        }
    }

    public static short ReturnRPCID(string function)
    {
        switch (function)
        {
            default:
                return -1;

            case "DisappearPlatform":
                return 0;
            case "SyncLift":
                return 1;
            case "OnPlayerStartInteract":
                return 2;
            case "OnPlayerStopInteract":
                return 3;
            case "MoveLift":
                MFPEditorUtils.Log("TRIED TO CALL UNUSED RPC??!?!?");
                return 4;
            case "FlipTheTable":
                return 5;
            case "OpenDoor":
                return 6;
            case "ReadPackage":
                return 7;
            case "OnLaserTriggered":
                return 8;
            case "EnemyChairTriggered":
                return 9;
            case "GasCanisterExplode":
                return 10;
            case "OnHostActivatedEntity":
                return 11;
            case "TestRPC": //used for testing package sending
                return 12;
            case "OnEnemyDeath":
                return 13;
            case "OnTurretDetectPlayer":
                return 14;
            case "TriggerTheSpeech":
                return 15;
            case "SyncBaseTransform":
                return 16;
            case "SyncPedroBoss":
                return 17;
            case "ExecuteState":
                return 18;
            case "OnClientReachLevelEnd":
                return 19;
            case "TrampolineBounce":
                return 20;
        }
    }


    public static string ReturnRPCName(short rpcID)
    {
        switch (rpcID)
        {
            default:
                return "none";
            case 0:
                return "DisappearPlatform";
            case 1:
                return "SyncLift";
            case 2:
                return "OnPlayerStartInteract";
            case 3:
                return "OnPlayerStopInteract";
            case 4:
                return "MoveLift";
            case 5:
                return "FlipTheTable";
            case 6:
                return "OpenDoor";
            case 7:
                return "ReadPackage";
            case 8:
                return "OnLaserTriggered";
            case 9:
                return "EnemyChairTriggered";
            case 10:
                return "GasCanisterExplode";
            case 11:
                return "OnHostActivatedEntity";
            case 12:
                return "TestRPC";
            case 13:
                return "OnEnemyDeath";
            case 14:
                return "OnTurretDetectPlayer";
            case 15:
                return "TriggerTheSpeech";
            case 16:
                return "SyncBaseTransform";
            case 17:
                return "SyncPedroBoss";
            case 18://welp this idea didnt last long
                return "ExecuteState";
            case 19:
                return "OnClientReachLevelEnd";
            case 20:
                return "TrampolineBounce";
        }

    }
}

