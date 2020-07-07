// Decompiled with JetBrains decompiler
// Type: LevelChangerScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityScript.Lang;
using Steamworks;

[Serializable]
public class LevelChangerScript : BaseNetworkEntity
{
    public SwitchScript[] inputSwitch;
    private float switchInput;
    public int levelNumber;
    public bool relative;
    public bool saveState;
    public bool dontShowEndScreen;
    private RootScript root;

    private int nrOfFinishedPlayers = 0;
    private bool waitingPlayers = false;
    private float waitTime = 15;


    private bool fading = false;

    private List<CSteamID> levelCompletors = new List<CSteamID>();

    public LevelChangerScript()
    {
        levelNumber = 1;
        relative = true;
        saveState = true;
    }


    public override void OnPlayerStartInteract(ulong activator)
    {
        base.OnPlayerStartInteract(activator);

        CSteamID activatorID = (CSteamID)activator;

        if (!EMFDNS.isLocalUser(activatorID))
            MFPEditorUtils.Log("Got LevelChangerScript event"); //this line executed

        // if (levelCompletors.Contains(activatorID)) //this line might be problematic
        //   return;

        if (!levelCompletors.Contains(activatorID))
        {
            levelCompletors.Add(activatorID);
            nrOfFinishedPlayers++;
        }

        if (!waitingPlayers)
        {
            waitingPlayers = true;

            if (nrOfFinishedPlayers == MultiplayerManagerTest.inst.playerObjects.Count)
                return;

            Invoke("ForceCompleteLevel", waitTime);
            MFPEditorUtils.doPedroHint("A player has reached the end\nLevel will finish in " + waitTime.ToString() + " seconds.");

            if (EMFDNS.isLocalUser(activatorID))
            {
                PlayerScript.PlayerInstance.enabled = false;

                // CameraScript camScript = GameObject.FindObjectOfType<CameraScript>();
                // camScript.GetComponent<Camera>().enabled = false;

                //   camScript.playerPublic = mpManager.playerObjects.ElementAt(UnityEngine.Random.Range(1, mpManager.playerObjects.Count)).Value.transform;
                /* while (true)
                 {
                     int rnd = UnityEngine.Random.Range(1, mpManager.playerObjects.Count);

                     if (mpManager.playerObjects.ElementAt(rnd).Key.isLocalUser())
                         continue;

                     SpectateMode.InitCam(mpManager.playerObjects.ElementAt(rnd).Key);

                     break;
                 }
                 */
            }
        }

    }

    public void ForceCompleteLevel() => doTheThing(true);


    public virtual void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Player")
            return;

        if (MultiplayerManagerTest.singleplayerMode)
            doTheThing();
        else
            PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID }, EP2PSend.k_EP2PSendUnreliable);
    }

    public virtual void doTheThing(bool mpForce = false)
    {
        if (MultiplayerManagerTest.singleplayerMode || mpForce)
        {
            if (dontShowEndScreen)
            {
                fading = true;
                //root.fadeToBlackAndChangeLevel();
            }
            else
                root.levelEnded = true;
            root.levelToLoad = !relative ? levelNumber : SceneManager.GetActiveScene().buildIndex + levelNumber;
        }
    }


    public override void Awake()
    {
        base.Awake();
        maxAllowedPackets = 3;
    }

    public override void Start()
    {
        base.Start();

        root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        new GameObject().AddComponent<MultiplayerManagerTest>();
    }

    public override void Update()
    {
        base.Update();

        if (!root.levelEnded && MultiplayerManagerTest.inst.initComplete && waitingPlayers)
            if (nrOfFinishedPlayers == MultiplayerManagerTest.inst.playerObjects.Count)
            {
                CancelInvoke();
                doTheThing(true);
            }


        if (this.inputSwitch.Length <= 0)
            return;

        int num = -1;
        int index = 0;
        SwitchScript[] inputSwitch = this.inputSwitch;
        for (int length = inputSwitch.Length; index < length; ++index)
        {
            if (inputSwitch[index].output > num)
                num = (int)inputSwitch[index].output;
        }
        if (num >= 1)
        {
            //doTheThing();
            PacketSender.BaseNetworkedEntityRPC("OnPlayerStartInteract", entityIdentifier, new object[] { MultiplayerManagerTest.inst.playerID.m_SteamID }, EP2PSend.k_EP2PSendUnreliable);
        }
    }

}
