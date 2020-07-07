using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steamworks;
using UnityEngine;

public static class BulletSync
{
    public static RootScript root { get { return MultiplayerManagerTest.inst.root; } }


  /*  public static Vector3 GetPlayerMuzzle(MFPPlayerGhost ghost, bool isLocal, bool rightHand)
    {
        PlayerScript player = PlayerScript.PlayerInstance;

        if(isLocal)
           return new Vector3(player.bulletPointRAimTargetPublic.transform.position.x, player.bulletPointRAimTargetPublic.transform.position.y, player.bulletPointRPublic.position.z) - player.bulletPointRPublic.position; 
        else
            return new Vector3(ghost.bulletPointRAimTarget.transform.position.x, ghost.bulletPointRAimTargetPublic.transform.position.y, player.bulletPointRPublic.position.z) - player.bulletPointRPublic.position;

    }*/

    public static void HandleMachineGunGrenadeSync(P2PMessage packet, CSteamID remoteID)
    {

        MFPEditorUtils.Log("Machinegun grenade received by " + SteamFriends.GetFriendPersonaName(remoteID));

        PlayerScript player = PlayerScript.PlayerInstance;

        Vector3 grenadePos = packet.ReadVector3();
        Quaternion grenadeRot = packet.ReadCompressedQuaternion();
        int num1Rnd = packet.ReadInteger();

        GameObject tempBulletVar = UnityEngine.Object.Instantiate<GameObject>(player.machineGunGrenade, grenadePos, grenadeRot);

        Rigidbody component = tempBulletVar.GetComponent<Rigidbody>();
        component.velocity = tempBulletVar.transform.forward * 23f;

        if (remoteID.isLocalUser())
            player.smokeParticlePublic.Emit(root.generateEmitParams(player.bulletPointR2Public.position, tempBulletVar.transform.forward, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);

        Vector3 angularVelocity = component.angularVelocity;
        double num2 = (double)(angularVelocity.y = num1Rnd);
        Vector3 vector3 = component.angularVelocity = angularVelocity;

    }

    public static void HandleSync(P2PMessage packet, CSteamID remoteID) // WHAT THE FUCK?? WHY ARE HALF THE VARIABLES DEPEND ON LOCAL PLAYER? FIX FIX FIX FIX
    {

        PlayerScript player = PlayerScript.PlayerInstance;
        MFPPlayerGhost playerGhost = MultiplayerManagerTest.inst.playerObjects[remoteID];

        Vector3 shootLoc = packet.ReadVector3();
        Quaternion shootRot = packet.ReadQuaternion();

        float quaternionEulerRnd = packet.ReadFloat();
        float aimSpread = packet.ReadFloat();

        bool shooterisLocalPlayer = remoteID.isLocalUser();

        if (MultiplayerManagerTest.inst.playerObjects[remoteID].weapon == 1)
        {
            playerGhost.playGunSound(true);
            root.rumble(1, 0.25f, 0.1f);


            player.shellParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.handRPublic.position : playerGhost.handR.position), new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
            player.smokeParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.bulletPointRPublic.position : playerGhost.bulletPointR.position), Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);

            GameObject tempBulletVar = root.getBullet(shootLoc, shootRot);
            BulletScript tempBulletScript = root.getBulletScript();
            tempBulletScript.bulletStrength = 0.35f;
            tempBulletScript.doPostSetup();

            tempBulletVar.transform.rotation *= Quaternion.Euler(quaternionEulerRnd * aimSpread, 0.0f, 0.0f);
        }
        if (MultiplayerManagerTest.inst.playerObjects[remoteID].weapon == 2)
        {
            GameObject tempBulletVar = player.gameObject; //assigning random transform so it dont bitch about unassigned variable
            BulletScript tempBulletScript;

            tempBulletVar = root.getBullet(shootLoc, shootRot);
            tempBulletScript = root.getBulletScript();
            tempBulletScript.bulletStrength = 0.35f;

            //   tempBulletScript.tailCheck = Vector2.Distance((Vector2)tempBulletScript.transform.position, (Vector2)player.neckPublic.position);
            if (playerGhost.owner.isLocalUser())
                tempBulletScript.tailCheck = Vector2.Distance((Vector2)tempBulletScript.transform.position, (Vector2)player.neckPublic.position);
            else
                tempBulletScript.tailCheck = Vector2.Distance((Vector2)tempBulletScript.transform.position, (Vector2)playerGhost.neck.position);

            tempBulletScript.doPostSetup();

            bool shootLeft = packet.ReadByte().Equals(1);

            if (shootLeft)
            {
                player.shellParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.handLPublic.position : playerGhost.handL.position), new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                player.smokeParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.bulletPointLPublic.position : playerGhost.bulletPointL.position), Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);

                if (remoteID.isLocalUser())
                    root.rumble(0, 0.25f, 0.1f);
                playerGhost.playGunSound(false);
            }
            else
            {
                player.shellParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.handRPublic.position : playerGhost.handR.position), new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                player.smokeParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.bulletPointRPublic.position : playerGhost.bulletPointR.position), Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);

                if (remoteID.isLocalUser())
                    root.rumble(1, 0.25f, 1);
                playerGhost.playGunSound(true);
            }

            tempBulletVar.transform.rotation *= Quaternion.Euler(quaternionEulerRnd * aimSpread, 0.0f, 0.0f);

        }

        if (MultiplayerManagerTest.inst.playerObjects[remoteID].weapon == 3)
        {
            playerGhost.playGunSound(true);
            if (remoteID.isLocalUser())
                root.rumble(1, 0.25f, 0.1f);

            player.shellParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.handRPublic.position : playerGhost.handR.position), new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
            player.smokeParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.bulletPointRPublic.position : playerGhost.bulletPointR.position), Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);

            //  this.shellParticle.Emit(this.root.generateEmitParams(this.handR.position, new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
            GameObject tempBulletVar = root.getBullet(shootLoc, shootRot);
            BulletScript tempBulletScript = root.getBulletScript();
            tempBulletScript.bulletStrength = 0.2f;
            tempBulletScript.bulletSpeed = 12;
            tempBulletScript.doPostSetup();

            //  root.getMuzzleFlash(0, PlayerScript.PlayerInstance.bulletPointRPublic.position, Quaternion.LookRotation(new Vector3(this.bulletPointRAimTarget.transform.position.x, this.bulletPointRAimTarget.transform.position.y, this.bulletPointR.position.z) - this.bulletPointR.position) * Quaternion.Euler(0.0f, 270f, 0.0f));
            tempBulletVar.transform.rotation *= Quaternion.Euler(quaternionEulerRnd * aimSpread, 0.0f, 0.0f);
        }

        if (MultiplayerManagerTest.inst.playerObjects[remoteID].weapon == 4)
        {
            GameObject tempBulletVar = player.gameObject; //assigning random transform so it dont bitch about unassigned variable
            BulletScript tempBulletScript;

            tempBulletVar = root.getBullet(shootLoc, shootRot);
            tempBulletScript = root.getBulletScript();
            tempBulletScript.bulletStrength = 0.2f;
            tempBulletScript.bulletSpeed = 12;

            //   tempBulletScript.tailCheck = Vector2.Distance((Vector2)tempBulletScript.transform.position, (Vector2)player.neckPublic.position);
            if (playerGhost.owner.isLocalUser())
                tempBulletScript.tailCheck = Vector2.Distance((Vector2)tempBulletScript.transform.position, (Vector2)player.neckPublic.position);
            else
                tempBulletScript.tailCheck = Vector2.Distance((Vector2)tempBulletScript.transform.position, (Vector2)playerGhost.neck.position);

            tempBulletScript.doPostSetup();

            bool shootLeft = packet.ReadByte().Equals(1);

            if (shootLeft)
            {
                player.shellParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.handLPublic.position : playerGhost.handL.position), new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                player.smokeParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.bulletPointLPublic.position : playerGhost.bulletPointL.position), Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);

                if (remoteID.isLocalUser())
                    root.rumble(0, 0.2f, 0.1f);
                playerGhost.playGunSound(false);
            }
            else
            {
                player.shellParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.handRPublic.position : playerGhost.handR.position), new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);
                player.smokeParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.bulletPointRPublic.position : playerGhost.bulletPointR.position), Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);

                if (remoteID.isLocalUser())
                    root.rumble(1, 0.2f, 1);
                playerGhost.playGunSound(true);
            }
            tempBulletVar.transform.rotation *= Quaternion.Euler(quaternionEulerRnd * aimSpread, 0.0f, 0.0f);
        }


        if (MultiplayerManagerTest.inst.playerObjects[remoteID].weapon == 5)
        {
            playerGhost.playGunSound(true);

            player.smokeParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.bulletPointRPublic.position : playerGhost.bulletPointR.position), Vector3.zero, 3f, 2f, new Color(1f, 1f, 1f, 0.1f)), 1);
            player.shellParticlePublic.Emit(root.generateEmitParams((shooterisLocalPlayer ? player.handRPublic.position : playerGhost.handR.position), new Vector3(UnityEngine.Random.Range(-0.8f, 0.8f), (float)UnityEngine.Random.Range(3, 6), 0.0f), 0.15f, 3f, Color.white), 1);

            GameObject tempBulletVar = player.gameObject; //assigning random transform so it dont bitch about unassigned variable
            BulletScript tempBulletScript;

            tempBulletVar = root.getBullet(shootLoc, shootRot);
            tempBulletScript = root.getBulletScript();
            tempBulletScript.bulletStrength = 0.45f;
            tempBulletScript.bulletSpeed = 14f;
            tempBulletScript.bulletLength = 2f;

            tempBulletScript.doPostSetup();
            tempBulletVar.transform.rotation *= Quaternion.Euler(quaternionEulerRnd * aimSpread, 0.0f, 0.0f);
        }


        if (MultiplayerManagerTest.extraDebug)
            Debugging.CreateDisappearingCube(shootLoc, shootRot, new Vector3(0.3f, 0.5f, 0.7f), 0.25f);

    }
}

