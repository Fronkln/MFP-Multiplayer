using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using I2.Loc;

    public static class RuntimeEntities
    {
        public static GameObject pedroSample;
        public static GameObject doorSpawnerSample;

        public static PedroScript SpawnPedro(Vector3 position, string name = "Pedro")
        {
            GameObject pedro = GameObject.Instantiate(pedroSample);
            pedro.transform.position = position + new Vector3(3, 0, 0);
            pedro.SetActive(true);
            pedro.name = name;

            return pedro.GetComponent<PedroScript>();
        }

        public static EnemyChairScript CreateChairEnemy(GameObject chair, EnemyScript enemy, bool faceRight, bool fallOver = true)
        {
            EnemyChairScript chairScript = chair.AddComponent<EnemyChairScript>();
            chairScript.targetEnemy = enemy;

            enemy.standStill = true;
            enemy.faceRight = faceRight;
            enemy.transform.position = chairScript.gameObject.transform.position + new Vector3(-1, 0, 0);

            chairScript.fallOver = fallOver;

            MFPEditorUtils.Log("Created chair enemy from: " + enemy.transform.name);

            return chairScript;
        }

        public static SpawnDoorScript CreateEnemyDoor(Vector3 position, int enemyCount, bool visualDoor = false)
        {
            GameObject newDoor = GameObject.Instantiate(doorSpawnerSample);
            newDoor.GetComponentInChildren<SpawnDoorScript>().nrOfEnemies = enemyCount;
            newDoor.transform.position = position;


            newDoor.SetActive(true);

            if (visualDoor)
                newDoor.GetComponentInChildren<SpawnDoorScript>().enabled = false;

            return newDoor.GetComponentInChildren<SpawnDoorScript>();
        }

        public static InstructionTextScript CreateInstructionText(GameObject target, string[] translationID, bool forceActivate = false, bool reTrigger = false, bool triggerWithoutPlayer = false)
        {
            GameObject insTarget = target;

            insTarget.AddComponent<SwitchScript>();
            InstructionTextScript insScript = insTarget.AddComponent<InstructionTextScript>();


            insScript.inputSwitch = new SwitchScript[0];

            insScript.triggerWithoutPlayer = triggerWithoutPlayer;

            insScript.translationID = translationID;

             insScript.forceActivate = forceActivate;
             insScript.reTrigger = reTrigger;

            return insScript;
        }

        public static SpeechTriggerControllerScript GenerateSpeechScript(GameObject target, Transform followTarget, string speakerName, string locStringID, AudioClip voice = null, bool freezePlayer = false, GameObject forceSpawnPedro = null)
        {

            SwitchScript switchScript = target.AddComponent<SwitchScript>();
            switchScript.output = 1;

            SpeechTriggerScript speechTriggerScript = target.AddComponent<SpeechTriggerScript>();

            if (freezePlayer)
            {
                speechTriggerScript.clickToContinue = true;
                speechTriggerScript.clickToContinueDontFreeze = false;
            }


            speechTriggerScript.followTransform = followTarget;

            speechTriggerScript.speakerName = speakerName;

            if (forceSpawnPedro != null)
                speechTriggerScript.forceSpawnPedro = forceSpawnPedro.GetComponent<PedroScript>();

            string testString = "";

            LocalizationManager.TryGetTranslation(speakerName, out testString);

            if (testString.ToLower().Contains("missing translation"))
                MFPEditorUtils.CreateTranslation(speakerName, speakerName);


            if(voice != null)
            speechTriggerScript.voice = voice;

            speechTriggerScript.locStringId = locStringID;

            SpeechTriggerControllerScript controllerScript = target.AddComponent<SpeechTriggerControllerScript>();;

            controllerScript.inputSwitch = new SwitchScript[0];

            return controllerScript;

        }

        public static AutoControlZoneScript GenerateAutoControlZone(GameObject target, SwitchScript[] inputSwitch = null, bool enableOverrideOnEnter = true, bool disableOverrideWhenLeavingZone = true, bool disableOverrideAfterInteract = false, bool disableOverrideAfterTime = false, float disableOverrideTimer = 0, bool reTrigger = false, float triggerDelay = 0, bool triggerOnlyOnEnter = false, bool setXMoveAmount = false, float xMoveAmount = 0, bool crouch = false, bool jump = false, bool dodge = false, bool interact = false, bool slowMotion = false, bool disableSlowMotionOnExit = false, bool reload = false, bool fire = false, bool secondaryFire = false, bool setAim = false, Vector3 aimPos = default(Vector3), Vector3 secondaryAimPos = default(Vector3), int weapon = -1)
        {
            AutoControlZoneScript script = target.AddComponent<AutoControlZoneScript>();


            if (inputSwitch != null)
                script.inputSwitch = inputSwitch;
            else
                script.inputSwitch = new SwitchScript[] { };

            script.enableOverrideOnEnter = enableOverrideOnEnter;
            script.disableOverrideWhenLeavingZone = disableOverrideWhenLeavingZone;
            script.disableOverrideAfterInteract = disableOverrideAfterInteract;
            script.disableOverrideAfterTime = disableOverrideAfterTime;
            script.disableOverrideTimer = disableOverrideTimer;

            script.reTrigger = reTrigger;
            script.triggerDelay = triggerDelay;
            script.triggerOnlyOnEnter = triggerOnlyOnEnter;

            script.setXMoveAmount = setXMoveAmount;
            script.xMoveAmount = xMoveAmount;

            script.crouch = crouch;
            script.jump = jump;
            script.dodge = dodge;
            script.interact = interact;
            script.slowMotion = slowMotion;
            script.disableSlowMotionOnExit = disableSlowMotionOnExit;
            script.reload = reload;
            script.fire = fire;
            script.secondaryFire = secondaryFire;

            script.setAim = setAim;
            script.aimPos = aimPos;
            script.secondaryAimPos = secondaryAimPos;

            script.weapon = weapon; 

            return script;
        }
    }

