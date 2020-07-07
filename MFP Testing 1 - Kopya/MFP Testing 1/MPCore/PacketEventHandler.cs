using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steamworks;
using UnityEngine;

public static class PacketEventHandler
{

    /// <summary>
    /// The first byte of a package always specifies the event!
    /// </summary>
    public static void HandleEvent(P2PMessage packet, CSteamID remoteID)
    {
        if (!MultiplayerManagerTest.inGameplayLevel && !MultiplayerManagerTest.inMenu)
            return;

        byte p2pEvent = packet.ReadByte();

        switch (p2pEvent)
        {
            //REGISTER PLAYER PREFAB ON JOIN
            case 0:
                MFPEditorUtils.Log("Registration package from " + SteamFriends.GetFriendPersonaName(remoteID) + " recieved");
                MultiplayerManagerTest.inst.RegisterPlayer(remoteID);
                break;
            
            //TRANSFORM & ROTATION SYNC
            case 1:
                PlayerTransformSync.HandleSync(packet, remoteID);
                break;
            //PLAYER ANIMATOR SYNC
            case 2:
                PlayerAnimatorSync.HandleSync(packet, remoteID);
                break;
            //PLAYER WEAPON SYNC, packet structure: byte, byte (weapon)
            case 3:
                PlayerGunSync.HandleGunSwitch(packet, remoteID);
                break;
            //PLAYER SHOOTING SYNC, not properly implemented in PlayerScript.cs
            case 4:
                BulletSync.HandleSync(packet, remoteID);
                break;
            //PLAYER GUN SOUND SYNC, packet structure: bool, bool
            case 5:
                PlayerGunSync.HandleGunSoundSync(packet, remoteID);
                break;

            //PLAYER DEATH EVENT
            case 6:
                MultiplayerManagerTest.inst.playerObjects[remoteID].GhostRespawn();
                break;
            //PLAYER RESPAWN EVENT
            case 7:
                MultiplayerManagerTest.inst.playerObjects[remoteID].GhostDeath();
                break;
            case 8:

                Vector3 pos = packet.ReadVector3();
                float size = packet.ReadFloat();
                short amount = packet.ReadShort();
                Vector3 vel = packet.ReadVector3();
                string explosionColor = packet.ReadUnicodeString();
                bool turnOffCollision = packet.ReadBool();
                bool doSound = packet.ReadBool();



                MultiplayerManagerTest.inst.root.explode(pos, size, amount, vel, explosionColor, turnOffCollision, doSound);
                MFPEditorUtils.Log("Boom!");
                break;

            case 9: //Determining if players loaded the next level
                if (!MultiplayerManagerTest.inst.levelTransitionReady.Contains(remoteID))
                {
                    MultiplayerManagerTest.inst.levelTransitionReady.Add(remoteID);
                    MFPEditorUtils.Log(SteamFriends.GetFriendPersonaName(remoteID) + " loaded the next scene.");
                }
                break;

            case 10: //Next Level Transition
                MFPEditorUtils.Log("Transitioning clients to next level.");
                MultiplayerManagerTest.inst.levelTransitionReady.Clear();

                if (MultiplayerManagerTest.inGameplayLevel)
                {

                    MFPEditorUtils.Log("not pog!");

                    if (!MultiplayerManagerTest.inst.forceNextLevelDoOnce)
                    {
                        MFPEditorUtils.Log("A");
                        GameObject.FindObjectOfType<LevelCompleteScreenScript>().MultiplayerGoNextLevel();
                        MFPEditorUtils.Log("A2");
                    }
                    else
                    {
                        MFPEditorUtils.Log("this part executes hhh");
                        GameObject.FindObjectOfType<LoadNextLevelAllClientsAsync>().asyncLoad.allowSceneActivation = true;
                        MultiplayerManagerTest.transitioningToNextLevel = true;
                    }
                }


                if (MultiplayerManagerTest.inMenu)
                {
                    MultiplayerManagerTest.transitioningToNextLevel = true;
                    MultiplayerManagerTest.inst.loadLobbyLevelOperation.allowSceneActivation = true;
                }

                break;

            case 11:
                BulletSync.HandleMachineGunGrenadeSync(packet, remoteID);
                break;

            case 12: //Player kick objects

                MFPEditorUtils.Log("kick request recieved");

                Vector3 vector4 = packet.ReadVector3();
                Vector3 footkickCalculation = packet.ReadVector3();
                int kickedRBodyID = packet.ReadInteger();
                int autoTargetTransformID = packet.ReadInteger();

                BaseNetworkEntity kickedOBJ = MultiplayerManagerTest.inst.networkedBaseEntities[kickedRBodyID];
                BaseNetworkEntity autoTargetTransform = null;

                if(autoTargetTransformID != -1)
                    autoTargetTransform = MultiplayerManagerTest.inst.networkedBaseEntities[autoTargetTransformID];

                Rigidbody kickRBody = kickedOBJ.GetComponent<Rigidbody>();
                ObjectKickScript kickScript = kickedOBJ.GetComponent<ObjectKickScript>();

                kickRBody.isKinematic = false;
                kickRBody.velocity = vector4;
                kickRBody.angularVelocity = kickRBody.velocity;

                if (autoTargetTransform != null)
                { 
                    kickScript.autoTargetTransform = autoTargetTransform.transform;
                    //kickScript.autoTargetEnemyScript = autoTargetEnemyScript.transform;
                }

                kickRBody.transform.position = kickRBody.transform.position + footkickCalculation * 0.4f;
                kickScript.kickJuggleAmount++;

                PhysicsSoundsScript physSoundScript = kickRBody.GetComponent<PhysicsSoundsScript>();

               /* if (objectKickScript.objType == 4 && kickScript.kickJuggleAmount > this.statsTracker.highestNrOfBasketballJuggles)
                {
                    this.statsTracker.highestNrOfBasketballJuggles = objectKickScript.kickJuggleAmount;
                    this.statsTracker.achievementCheck();
                }
                */
                if (physSoundScript != null)
                    physSoundScript.triggerCollisionSound((float)3, true, 0.3f);

                kickedOBJ.interactingPlayer = remoteID;

                break;

            case 13: //Host pressed E on speechscript
                RootScript.RootScriptInstance.doMultiplayerClickToContinueOnce = true;
                MFPEditorUtils.Log("progressing speechscript");
                break;


            case 14: //clients will load lobby level
                MFPEditorUtils.Log("GOT EVENT");
                MultiplayerManagerTest.inst.StartCoroutine(MultiplayerManagerTest.inst.LoadLobbyLevel());
                break;
            case 15:
                MultiplayerManagerTest.inst.hostIsOnSpeechScript = packet.ReadBool();
                break;
            case 16:
                new GameObject().AddComponent<LoadNextLevelAllClientsAsync>();
                break;


            //HostActivateEntityRequest
            case 252:

                MFPEditorUtils.Log("Got request to activate entity");

                int entityActivationID = packet.ReadInteger();

                PacketSender.BaseNetworkedEntityRPC("OnHostActivatedEntity", entityActivationID);
                break;

            //BaseNetworkEntityRPC possible improvement for OnStart and OnStop, use remoteID instead of sending ulong parameter.
            case 253:

                int entityID = packet.ReadInteger();
                short rpcID = packet.ReadShort();

                if (!MultiplayerManagerTest.inst.networkedBaseEntities.ContainsKey(entityID)) //this shouldnt happen at all
                    return;


                if (MultiplayerManagerTest.extraDebug && !MultiplayerManagerTest.inst.networkedBaseEntities[entityID].dontDoDebug)
                    MultiplayerManagerTest.inst.networkedBaseEntities[entityID].debugHelper.OnRPC(NetworkedBaseRPCFunctions.ReturnRPCName(rpcID), RPCSendMode.EVERYONE);

                NetworkedBaseRPCFunctions.DoRPC(MultiplayerManagerTest.inst.networkedBaseEntities[entityID], remoteID, rpcID, packet);

    
                break;

            //PLAYERMODEL CHANGE
            case 254:
                GameObject playerObj = MultiplayerManagerTest.inst.playerObjects[remoteID].gameObject;

                if (playerObj.transform.Find("PlayerGraphics/Head01").GetComponent<SkinnedMeshRenderer>().enabled == true)
                    return;

                playerObj.transform.Find("PlayerGraphics/Head01").GetComponent<SkinnedMeshRenderer>().enabled = true;
                playerObj.transform.Find("PlayerGraphics/Head01").GetComponent<SkinnedMeshRenderer>().enabled = true;
                playerObj.transform.Find("PlayerGraphics/Legs01").GetComponent<SkinnedMeshRenderer>().enabled = true;
                playerObj.transform.Find("PlayerGraphics/Hair").GetComponent<SkinnedMeshRenderer>().enabled = true;



                if (remoteID.isLocalUser())
                {
                    if (Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault((GameObject g) => g.name == "Head01" && g.transform.root.name == "Player") != null)
                    {
                        GameObject betaHead = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault((GameObject g) => g.name == "Head01" && g.transform.root.GetComponent<PlayerScript>());
                        betaHead.SetActive(true);

                        betaHead.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                    }
                    else
                        MFPEditorUtils.Log("Head01 not present!");

                    GameObject betaLegs = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault((GameObject g) => g.name == "Legs01" && g.transform.root.GetComponent<PlayerScript>());
                    betaLegs.SetActive(true);

                    GameObject betaHair = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault((GameObject g) => g.name == "Hair" && g.transform.root.GetComponent<PlayerScript>());
                    betaHair.SetActive(true);


                    MFPPlayerGhost ghost = playerObj.GetComponent<MFPPlayerGhost>();



                    GameObject defaultRenderer = GameObject.Find("Player/PlayerGraphics/TorsorBlackLongSleeve");

                    GameObject torsor = new GameObject();
                    SkinnedMeshRenderer torsorRenderer = torsor.AddComponent<SkinnedMeshRenderer>();
                    torsorRenderer.sharedMesh = DiscordCT.multiplayerBundle.LoadAsset("TorsoLongCoatAndHoodie") as Mesh;
                    torsorRenderer.sharedMaterial = DiscordCT.multiplayerBundle.LoadAsset("torsor_long_coat_and_hoodie") as Material;

                    torsorRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                    torsorRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;

                    torsor.transform.parent = defaultRenderer.transform;
                    torsor.transform.position = defaultRenderer.transform.position;

                    SkinnedMeshRenderer originalRenderer = defaultRenderer.GetComponent<SkinnedMeshRenderer>();


                    Transform[] replacementBones = new Transform[25];

                    for (int i = 0; i < 23; i++)
                        replacementBones[i] = originalRenderer.bones[i];

                    replacementBones[23] = replacementBones[0];
                    replacementBones[24] = replacementBones[0];

                    torsorRenderer.bones = replacementBones;
                    torsorRenderer.rootBone = originalRenderer.rootBone;
                    torsorRenderer.probeAnchor = originalRenderer.probeAnchor;

                    torsorRenderer.updateWhenOffscreen = true;
                }

                break;
        }
    }
}

