using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;
using I2.Loc;
using UnityEngine.UI;


//How to add kill count etc on PvP theory:
/*
 * 
 * 1: PlayerGhostta lastBulletThatHit değişkeni
 * 2: client kurşunu yiyor, lastBulletThatHit atanıyor (vuran kişide)
 * 3: eğer lastHitByBullet null değilse ve oyuncu öldü ise kill verilir
 * 4: lasthitByBullet sonraki karede veya 0.1 snde temizlenir.
 * 
 * 
 */

public enum MPGamemodes
{
    Normal = 0,
    Race = 1,
    PvP = 2
}

public class MultiplayerManagerTest : MonoBehaviour //Initializes on LevelChangerScript, and if not, PlayerScript
{

    //lobby specific shit

    public bool hostIsOnSpeechScript = false;

    public AsyncOperation loadLobbyLevelOperation;
    private bool asyncLoadDone = false;
    public int lobbyLevelToLoadHost = 3; //host only


#if DEBUG
    public static bool extraDebug = true;
#endif

    public MPGamemodes gamemode = MPGamemodes.Normal;
    public PlayerSkins selectedSkin = PlayerSkins.Default;

    public static string partyID = "";
    public bool forceNextLevelDoOnce = false;

    public CSteamID playerID;
    public static CSteamID lobbyOwner;
    public static CSteamID lobbyID;
    public CSteamID joinID; //only the joining people have this
    public CSteamID globalID;

    public List<CSteamID> levelTransitionReady = new List<CSteamID>();


    public bool isRace { get { return gamemode == MPGamemodes.Race; } }

    public static bool playingAsHost = false;
    public static bool singleplayerMode { get { return inst == null || connected == false; } }
    public static bool inGameplayLevel { get { return PlayerScript.PlayerInstance != null; } }
    public static bool inMenu { get { return SceneManager.GetActiveScene().buildIndex == 1; } }
    public static bool everyoneLoaded { get { return SteamMatchmaking.GetNumLobbyMembers(MultiplayerManagerTest.inst.globalID) == MultiplayerManagerTest.inst.levelTransitionReady.Count; } }
    public static bool clearDoOnce = false;

    public static bool transitioningToNextLevel = false;

    public static bool connected = false;
    public bool initComplete = false;

    protected Callback<LobbyCreated_t> Callback_lobbyCreated;
    protected Callback<LobbyEnter_t> Callback_lobbyEntered;
    protected Callback<LobbyInvite_t> Callback_lobbyInvite;
    protected Callback<LobbyChatUpdate_t> Callback_lobbyChatUpdate;
    protected Callback<LobbyChatMsg_t> Callback_lobbyChatMessage;
    protected Callback<P2PSessionRequest_t> Callback_p2pSessionRequest;
    protected Callback<LobbyMatchList_t> Callback_recieveLobbies;
    protected Callback<LobbyDataUpdate_t> Callback_lobbyDataUpdate;

    public Dictionary<CSteamID, MFPPlayerGhost> playerObjects = new Dictionary<CSteamID, MFPPlayerGhost>();

    public static List<BaseNetworkEntity> entitiesToRegister = new List<BaseNetworkEntity>();
    public static List<BaseNetworkEntity> entitiesToRegisterRUNTIME = new List<BaseNetworkEntity>();
    public Dictionary<int, BaseNetworkEntity> networkedBaseEntities = new Dictionary<int, BaseNetworkEntity>();
    public List<NetworkedEnemyScriptAttachment> networkedEnemies = new List<NetworkedEnemyScriptAttachment>();

    // public List<CSteamID> otherPlayers = new List<CSteamID>();

    public GameObject playerPrefab;

    public static MultiplayerManagerTest inst;
    public MFPMPUI multiplayerUI;

    public RootScript root;
    public RootSharedScript rootShared;

    public bool isMotorcycleLevel = false;
    public bool isSkyfallLevel = false;

    private OptionsMenuScript menuScript;

    IEnumerator TransitionWait()
    {
        yield return new WaitForEndOfFrame();
        PrepareMPCore();
    }

    public IEnumerator LoadLobbyLevel()
    {

        asyncLoadDone = false;
        loadLobbyLevelOperation = null;

        int lobbyLevelToLoad = Convert.ToInt32(SteamMatchmaking.GetLobbyData(lobbyID, "lobbySelectedLevel"));

        loadLobbyLevelOperation = SceneManager.LoadSceneAsync(lobbyLevelToLoad);
        loadLobbyLevelOperation.allowSceneActivation = false;

        while (!loadLobbyLevelOperation.isDone)
        {
            // Check if the load has finished
            if (loadLobbyLevelOperation.progress >= 0.9f && !asyncLoadDone)
            {
                asyncLoadDone = true;
                PacketSender.SendTransitionReadyMessage(); //can also be used for async loads
                MFPEditorUtils.Log("Lobby scene is loaded.");
            }

            yield return null;
        }
    }

    public void OnLocalClientDisconnect() //only used for when local user disconnects
    {
        connected = false;
        initComplete = false;
        CleanManager();
        SceneManager.LoadScene(1);
    }

    public void Awake()
    {
        if (inst != null && this != inst)
        {
            DestroyImmediate(gameObject);
            return;
        }

        string[] commandLineArgs = Environment.GetCommandLineArgs();

        for (int i = 0; i < commandLineArgs.Length; i++)
            MFPEditorUtils.Log(commandLineArgs[i]);

        inst = this;
        DontDestroyOnLoad(gameObject);

        MFPEditorUtils.Log("Initialized MultiplayerManagerTest");

        if (inGameplayLevel)
        {
            

            root = GameObject.FindObjectOfType<RootScript>();


            isMotorcycleLevel = PlayerScript.PlayerInstance.onMotorcycle;
            isSkyfallLevel = PlayerScript.PlayerInstance.skyfall;

            if (isMotorcycleLevel)
                MFPEditorUtils.Log("The map is a motorcycle map.");

            if (isSkyfallLevel)
                MFPEditorUtils.Log("The map is a skyfall map.");

            //   root.dontAllowActionMode = true;

            //PrepareFaultySinglePlayerObjects();

            rootShared = GameObject.FindObjectOfType<RootSharedScript>();
            rootShared.modFocusSlowdownScale = 100;

            playerPrefab = DiscordCT.multiplayerBundle.LoadAsset("Player") as GameObject;
        }
        playerID = SteamUser.GetSteamID();

        MFPEditorUtils.CreateTranslation("testingString010", "Jhrino said...|Cock and ball torture!"); // TEST!!!!!!!!!!!!!!!!!!    


        Callback_recieveLobbies = Callback<LobbyMatchList_t>.Create(OnRecieveLobbyList);
        Callback_lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        Callback_lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        Callback_lobbyInvite = Callback<LobbyInvite_t>.Create(OnLobbyInvite);
        Callback_lobbyChatUpdate = Callback<LobbyChatUpdate_t>.Create(OnLobbyChatUpdate);
        Callback_lobbyChatMessage = Callback<LobbyChatMsg_t>.Create(OnLobbyChatMessage);
        Callback_lobbyDataUpdate = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdate);
        Callback_p2pSessionRequest = Callback<P2PSessionRequest_t>.Create(OnP2PSessionRequest);


        if (inGameplayLevel)
        {
            partyID = SteamUser.GetSteamID().ToString() + "P" + DateTimeOffset.UtcNow.ToString();

            multiplayerUI = Instantiate(DiscordCT.multiplayerUI).GetComponent<MFPMPUI>();

            /*
            Activity discordActivity = new Activity()
            {
                Details = "Testing da MP",
                Secrets = new ActivitySecrets()
                {
                    Join = SteamUser.GetSteamID().ToString()
                },
                Party = new ActivityParty()
                {
                    Id = partyID,
                    Size = new PartySize()
                    {
                        CurrentSize = 1,
                        MaxSize = 4
                    }
                }
            };
            */

            SteamFriends.SetRichPresence("steam_display", "test");
        }

    }

    public void LateUpdate()
    {
        if (root != null)
        {
            if (root.levelEnded)
                return;

            if (root.dead)
            {
                if (PvPManager.instance != null && PvPManager.instance.slowMotionActivated)
                    Time.timeScale = ((!root.kAction) ? 1 : (Mathf.Clamp(rootShared.modFocusSlowdownScale, 3, 100) * 0.01f));
                else if (gamemode != MPGamemodes.Race)
                    Time.timeScale = 1;
            }
        }
    }

    public void Update()
    {
        if (inst == null)
            inst = this;


        if (!inGameplayLevel || clearDoOnce)
        {
            if (inMenu && menuScript == null)
                menuScript = Resources.FindObjectsOfTypeAll<OptionsMenuScript>()[0];

            initComplete = false;

            if (entitiesToRegister.Count > 0)
            {
                entitiesToRegister.Clear();
                MFPEditorUtils.Log("Cleaned up entitiestoRegister");
            }

            networkedBaseEntities.Clear();
            networkedEnemies.Clear();

            if (clearDoOnce)
                clearDoOnce = false;


        }
        else
            if (root == null)
        {

            int gamemodeID = int.Parse(SteamMatchmaking.GetLobbyData(lobbyID, "gamemode"));
            MultiplayerManagerTest.inst.gamemode = (MPGamemodes)gamemodeID;

            if (gamemode == MPGamemodes.PvP)
                new GameObject().AddComponent<PvPManager>();

            //  MFPEditorUtils.Log(SceneManager.GetActiveScene().buildIndex.ToString());
            MFPEditorUtils.Log(GameObject.FindObjectOfType<RootSharedScript>().GetMultiplayerLevelName(SceneManager.GetActiveScene().buildIndex));
            MFPEditorUtils.Log("Refreshing Manager");
            SoftRefreshManager();


            MFPEditorUtils.Log(SceneManager.GetActiveScene().buildIndex.ToString());

            if (gamemode == MPGamemodes.PvP)
                if (SceneManager.GetActiveScene().buildIndex == 50) //Ophelia bossfight
                {
                    foreach (SpeechTriggerControllerScript script in GameObject.FindObjectsOfType<SpeechTriggerControllerScript>())
                        DestroyImmediate(script);

                    foreach (SpeechTriggerScript script in GameObject.FindObjectsOfType<SpeechTriggerScript>())
                        DestroyImmediate(script);

                    PlayerScript.PlayerInstance.overrideControls = false;

                    foreach (PlayerScript script in GameObject.FindObjectsOfType<PlayerScript>())
                        if (script.isEnemy) script.gameObject.SetActive(false);


                }


            if (transitioningToNextLevel || forceNextLevelDoOnce)
            {
                playerObjects.Clear();
                networkedBaseEntities.Clear(); //just to be sure
                MFPEditorUtils.Log("Transition detected");
                StartCoroutine(TransitionWait());
                transitioningToNextLevel = false;
                forceNextLevelDoOnce = false;
            }
        }
        else
        {
            if (entitiesToRegisterRUNTIME.Count != 0)
            {
                int startIndex = networkedBaseEntities.Count;

                for (int i = 0; i < entitiesToRegisterRUNTIME.Count; i++)
                {
                    int newID = startIndex + 1;

                    while (networkedBaseEntities.ContainsKey(newID))
                        newID++;

                    entitiesToRegisterRUNTIME[i].entityIdentifier = newID;
                    MFPEditorUtils.Log(entitiesToRegisterRUNTIME[i].transform.name + " = " + entitiesToRegisterRUNTIME[i].entityIdentifier.ToString());

                    networkedBaseEntities.Add(newID, entitiesToRegisterRUNTIME[i]);

                    startIndex++;
                }

                entitiesToRegisterRUNTIME.Clear();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                MFPEditorUtils.Log(RootScript.RootScriptInstance.sbTransform.ToString());
                MFPEditorUtils.Log(RootScript.RootScriptInstance.sbTriggerTransform.ToString());
            }
        }


        Application.runInBackground = true;

#if DEBUG

        if (Input.GetKey(KeyCode.LeftShift)) //Create debug player at mouse position
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                for (int i = 0; i < SteamMatchmaking.GetNumLobbyMembers(globalID); i++)
                {
                    CSteamID reciever = SteamMatchmaking.GetLobbyMemberByIndex(globalID, i);
                    MFPEditorUtils.Log(SteamFriends.GetFriendPersonaName(reciever));
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                MFPPlayerGhost debugPlayer = MFPPlayerGhost.NewPlayer(playerID, true);
                playerObjects.Add((CSteamID)UnityEngine.Random.Range(1000000000000000, 99999999999999999), debugPlayer);
                debugPlayer.transform.position = PlayerScript.PlayerInstance.mousePos;
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
                playerObjects[playerID].debugFreezeGhost = (playerObjects[playerID].debugFreezeGhost ? false : true);

            if (Input.GetKeyDown(KeyCode.Alpha8))
                SpectateMode.InitCam(playerObjects.ElementAt(UnityEngine.Random.Range(0, playerObjects.Count)).Key);

            if (Input.GetKeyDown(KeyCode.Alpha9))
                GameObject.FindObjectOfType<OptionsMenuScript>().buildDebugMenu();

        }
#endif

        if (connected)
        {
            uint packageSize;

            if (root != null)
                multiplayerUI.debugPlayerCount.text = "Connected Players: " + SteamMatchmaking.GetNumLobbyMembers(globalID);
            while (SteamNetworking.IsP2PPacketAvailable(out packageSize))
            {
                var buffer = new byte[packageSize];
                uint bytesRead;
                CSteamID remoteId;

                P2PMessage msg = new P2PMessage((buffer));

                // read the message into the buffer
                if (SteamNetworking.ReadP2PPacket(buffer, packageSize, out bytesRead, out remoteId))
                    PacketEventHandler.HandleEvent(msg, remoteId);
                else
                    MFPEditorUtils.LogError("Couldn't read package!");
            }
        }

        if (inMenu)
            if (!globalID.isNull())
                if (everyoneLoaded)
                    if (playingAsHost)
                        PacketSender.TransitionClientsToNextLevel();
    }

    public void ChangeToBetaCharacter()
    {

        P2PMessage packet = new P2PMessage();
        packet.WriteByte(254);

        byte[] bytes = packet.GetBytes();


        for (int i = 0; i < SteamMatchmaking.GetNumLobbyMembers(globalID); i++)
        {
            CSteamID reciever = SteamMatchmaking.GetLobbyMemberByIndex(globalID, i);
            if (SteamNetworking.SendP2PPacket(reciever, bytes, (uint)bytes.Length, EP2PSend.k_EP2PSendReliable))
            { }
            else
                MFPEditorUtils.LogError("Packet sending failed!");
        }
    }


    private void PrepareMPCore()
    {
        P2PMessage joinPacket = new P2PMessage();
        joinPacket.WriteInteger(0);
        joinPacket.WriteByte(0);

        PacketSender.SendPackageToEveryone(joinPacket);

        for (int i = 0; i < SteamMatchmaking.GetNumLobbyMembers(globalID); i++)
        {
            CSteamID player = SteamMatchmaking.GetLobbyMemberByIndex(globalID, i);

            MFPEditorUtils.Log("About to start registering player for " + SteamFriends.GetFriendPersonaName(player));

            if (!player.isLocalUser())
            {
                if (!playerObjects.ContainsKey(player))
                    RegisterPlayer(player);
            }
        }


        multiplayerUI.startStopServerButton.buttonText().text = "Disconnect";

        ConvertObjectsToNetwork();
        initComplete = true;
    }


    private void OnApplicationQuit()
    {
        if (connected)
            SteamMatchmaking.LeaveLobby(globalID);
    }

    #region Callbacks


    private void OnLobbyDataUpdate(LobbyDataUpdate_t dataupdate)
    {
        if (inMenu)
        {
            if (menuScript.curActiveMenuPublic != 0.2f)
                menuScript.buildMultiplayerLobbyMenu();
        }
    }

    private void OnRecieveLobbyList(LobbyMatchList_t lobbies)
    {
        if (inMenu) menuScript.buildMultiplayerLobbiesList(lobbies);
    }

    private void OnLobbyCreated(LobbyCreated_t lobby)
    {
        if (lobby.m_eResult == EResult.k_EResultOK)
        {
            playingAsHost = true;
            //    multiplayerUI.startStopServerButton.buttonText().text = "Disconnect";
            lobbyID = (CSteamID)lobby.m_ulSteamIDLobby;
            MFPEditorUtils.Log("\nLobby is set up!: " + lobby.m_ulSteamIDLobby);

            int gamemodeint = (int)gamemode;

            string username = SteamFriends.GetFriendPersonaName(MultiplayerManagerTest.inst.playerID);
            SteamMatchmaking.SetLobbyData(lobbyID, "lobbyOwner", username);
            SteamMatchmaking.SetLobbyData(lobbyID, "lobbySelectedLevel", lobbyLevelToLoadHost.ToString());
            SteamMatchmaking.SetLobbyData(lobbyID, "gamemode", gamemodeint.ToString());
        }
        else
            MFPEditorUtils.LogError("\nLobby creation failed!");
    }

    private void OnLobbyEntered(LobbyEnter_t entrance)
    {

        globalID = (CSteamID)entrance.m_ulSteamIDLobby;
        lobbyID = (CSteamID)entrance.m_ulSteamIDLobby; //big brain moment but i cant be bothered to change code

        connected = true;
        MFPEditorUtils.Log("Joined " + entrance.m_ulSteamIDLobby + " as " + (playingAsHost ? "host" : "client"));

        SteamFriends.SetRichPresence("status", "BLARGG");
        SteamFriends.SetRichPresence("connect", "please");

        int playerSelSkin = (int)selectedSkin;

        lobbyOwner = SteamMatchmaking.GetLobbyOwner(lobbyID);
        SteamMatchmaking.SetLobbyMemberData(lobbyID, "playerSkin", playerSelSkin.ToString());

        if (!inMenu)
            PrepareMPCore();
        else
        {
            MFPEditorUtils.Log("Building lobby menu");
            menuScript.buildMultiplayerLobbyMenu();
        }

    }

    private void OnLobbyInvite(LobbyInvite_t invitation)
    {
        MFPEditorUtils.Log(SteamFriends.GetFriendPersonaName((CSteamID)invitation.m_ulSteamIDUser) + " has invited you, trying auto join!");

        string IDLobby = invitation.m_ulSteamIDLobby.ToString();

        joinID = (CSteamID)ulong.Parse(IDLobby, System.Globalization.NumberStyles.None);
        MFPEditorUtils.Log("ID Thingy " + invitation.m_ulSteamIDLobby.ToString());

        SteamNetworking.AcceptP2PSessionWithUser((CSteamID)invitation.m_ulSteamIDUser);

        SteamMatchmaking.JoinLobby(joinID);
    }

    private void OnLobbyChatUpdate(LobbyChatUpdate_t chatUpdate)
    {
        switch ((EChatMemberStateChange)chatUpdate.m_rgfChatMemberStateChange)
        {
            case EChatMemberStateChange.k_EChatMemberStateChangeEntered:
                if (inMenu)
                    if (menuScript.curActiveMenuPublic != 0.2f)
                        menuScript.buildMultiplayerLobbyMenu();
                MFPEditorUtils.Log(SteamFriends.GetFriendPersonaName((CSteamID)chatUpdate.m_ulSteamIDUserChanged) + " entered the server!");
                break;
            case EChatMemberStateChange.k_EChatMemberStateChangeLeft:
                if (inGameplayLevel)
                    playerObjects[(CSteamID)chatUpdate.m_ulSteamIDUserChanged].DisposePlayer();
                if (inMenu)
                    menuScript.buildMultiplayerLobbyMenu();
                break;
            case EChatMemberStateChange.k_EChatMemberStateChangeDisconnected:
                goto case EChatMemberStateChange.k_EChatMemberStateChangeLeft;
            case EChatMemberStateChange.k_EChatMemberStateChangeBanned:
                goto case EChatMemberStateChange.k_EChatMemberStateChangeLeft;
            case EChatMemberStateChange.k_EChatMemberStateChangeKicked:
                goto case EChatMemberStateChange.k_EChatMemberStateChangeLeft;
        }

        lobbyOwner = SteamMatchmaking.GetLobbyOwner(lobbyID);

        //  otherPlayers.Add((CSteamID)chatUpdate.m_ulSteamIDMakingChange);
    }

    private void OnLobbyChatMessage(LobbyChatMsg_t chatMessage)
    {
        byte[] messageData = new byte[32];

        CSteamID sender;
        SteamMatchmaking.GetLobbyChatEntry(globalID, (int)chatMessage.m_iChatID, out sender, messageData, messageData.Length, out EChatEntryType type);

        string messageString = Encoding.UTF8.GetString(messageData);

        MFPEditorUtils.Log("MessageString: " + messageString);

        if (inGameplayLevel)
            playerObjects[sender].playerSpeechController.speak(messageString, 1, false);

    }

    private void OnP2PSessionRequest(P2PSessionRequest_t p2pRequest)
    {
        MFPEditorUtils.Log("We recieved P2P session request?? OMGOMG lets aCECPT!!!!!!1111");
        SteamNetworking.AcceptP2PSessionWithUser(p2pRequest.m_steamIDRemote);
    }

    #endregion


    public void RegisterPlayer(CSteamID player)
    {
        if (playerObjects.ContainsKey(player))
        {
            MFPEditorUtils.Log(SteamFriends.GetFriendPersonaName(player) + " already exists in playerObjects");
            return;
        }
        else
        {
            GameObject playerObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            MFPEditorUtils.Log("player creation start");
            try
            {


                //  playerObjects.Add(player, GameObject.CreatePrimitive(PrimitiveType.Cube));
                playerObjects.Add(player, MFPPlayerGhost.NewPlayer(player));

                DestroyImmediate(playerObj);
                playerObj = playerObjects[player].gameObject;

                if (isMotorcycleLevel && !player.isLocalUser())
                {
                    GameObject motorcycleGhost = Instantiate(GameObject.Find("PlayerMotorcycle"));
                    motorcycleGhost.AddComponent<MFPPlayerMotorcycleGhost>().targetGhost = playerObjects[player];
                }

                playerObj.transform.Find("PlayerGraphics/Hands01").GetComponent<SkinnedMeshRenderer>().enabled = false;
                playerObj.transform.Find("PlayerGraphics/Head01").GetComponent<SkinnedMeshRenderer>().enabled = false;
                playerObj.transform.Find("PlayerGraphics/Legs01").GetComponent<SkinnedMeshRenderer>().enabled = false;
                playerObj.transform.Find("PlayerGraphics/Hair").GetComponent<SkinnedMeshRenderer>().enabled = false;


                MFPEditorUtils.Log("player creation end");
            }
            catch
            {
                MFPEditorUtils.Log("PLAYER CREATION ERROR\n----------------------\n");
                MFPEditorUtils.Log(player.m_SteamID.ToString());
                MFPEditorUtils.Log((playerObj == null ? "PLAYER WAS NULL" : "PLAYER WAS NOT NULL"));
            }
        }


    }

    /// <summary>
    /// Registers multiplayer entities.
    /// </summary>
    public void ConvertObjectsToNetwork()
    {
        if (entitiesToRegister == null)
            MFPEditorUtils.LogError("EntitiesToRegister is null!");

        for (int i = 0; i < entitiesToRegister.Count; i++)
        {
            BaseNetworkEntity networkedEnt = entitiesToRegister[i];
            networkedEnt.entityIdentifier = i;
            networkedBaseEntities.Add(i, networkedEnt);

            MFPEditorUtils.Log(networkedEnt.transform.name + " = " + networkedEnt.entityIdentifier.ToString() + " added.");
        }

        entitiesToRegister.Clear();
    }


    public void Disconnect()
    {
        SteamMatchmaking.LeaveLobby(globalID);
        OnLocalClientDisconnect();
    }

    public void StartOrStopServer(bool start)
    {
        if (start)
        {
            if (connected)
            {
                MFPEditorUtils.Log("Already on a server");
                return;
            }

            SteamAPICall_t newLobby = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, 4);
        }
        else
        {
            if (!connected)
            {
                MFPEditorUtils.Log("Not connected to any server...");
                return;
            }

            Disconnect();
            //  multiplayerUI.startStopServerButton.buttonText().text = "Start Server";
        }
    }

    public void CleanManager()
    {
        if (!transitioningToNextLevel)
        {
            connected = false;
            playingAsHost = false;
        }
        initComplete = false;

        isMotorcycleLevel = false;
        isSkyfallLevel = false;

        if (!transitioningToNextLevel)
        {
            partyID = "";
            globalID = (CSteamID)0;
            joinID = (CSteamID)0;
            lobbyID = (CSteamID)0;
            lobbyOwner = (CSteamID)0;
        }

        playerObjects.Clear();
        networkedBaseEntities.Clear();
        networkedEnemies.Clear();


    }

    public void SoftRefreshManager() //shit code fitting for a shit programmer, maybe add this to awake
    {
        root = GameObject.FindObjectOfType<RootScript>();
        rootShared = GameObject.FindObjectOfType<RootSharedScript>();


        if (gamemode == MPGamemodes.PvP)
            if (SceneManager.GetActiveScene().buildIndex == 50)
                root.Invoke("StopIntroMusicLoop", 2);

        rootShared.modFocusSlowdownScale = 100;


        if (PlayerScript.PlayerInstance.onMotorcycle)
            isMotorcycleLevel = true;

        if (PlayerScript.PlayerInstance.skyfall)
            isSkyfallLevel = true;

        if (isMotorcycleLevel)
            MFPEditorUtils.Log("The map is a motorcycle map.");
        if (isSkyfallLevel)
            MFPEditorUtils.Log("The map is a skyfall map.");


        if (gamemode == MPGamemodes.Race)
            rootShared.modFocusSlowdownScale = 25;


        playerPrefab = DiscordCT.multiplayerBundle.LoadAsset("Player") as GameObject;
        playerID = SteamUser.GetSteamID();

        partyID = SteamUser.GetSteamID().ToString() + "P" + DateTimeOffset.UtcNow.ToString();

        multiplayerUI = Instantiate(DiscordCT.multiplayerUI).GetComponent<MFPMPUI>();

        /*
        Activity discordActivity = new Activity()
        {
            Details = "Testing da MP",
            Secrets = new ActivitySecrets()
            {
                Join = SteamUser.GetSteamID().ToString()
            },
            Party = new ActivityParty()
            {
                Id = partyID,
                Size = new PartySize()
                {
                    CurrentSize = 1,
                    MaxSize = 4
                }
            }
        };
        

        SteamFriends.SetRichPresence("steam_display", "test");
        DiscordController.inst.discord.GetActivityManager().UpdateActivity(discordActivity, new ActivityManager.UpdateActivityHandler(ActivityUpdateHandler));
        */
    }


    private static void ActivityUpdateHandler(Result res)
    {
        Debug.Log("Got result " + res.ToString() + " when updating activity");
    }
}


