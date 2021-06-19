using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class DiscordCT
{

    //dont let the class name fool you, use this class for mod initialization


    public static AssetBundle multiplayerBundle;
    public static AssetBundle debugBundle;

    public static GameObject multiplayerUI;

    public static bool alreadyLoaded = false;

    public static void Init()
    {
        if (!alreadyLoaded)
        {
            multiplayerBundle = AssetBundle.LoadFromFile(MFPEditorUtils.LoadFile("mfpmultiplayer"));
            debugBundle = AssetBundle.LoadFromFile(MFPEditorUtils.LoadFile("mfpmultiplayer_debug"));

            multiplayerUI = multiplayerBundle.LoadAsset("MFPMultiplayerCanvas") as GameObject;
            multiplayerUI.AddComponent<MFPMPUI>();
            alreadyLoaded = true;

            MFPMPUI.playerAvatarTemplate = GameObject.Instantiate(multiplayerBundle.LoadAsset<GameObject>("playerAvatar"));
            MFPMPUI.playerAvatarTemplate.AddComponent<WorldSpaceUI>();
            GameObject.DontDestroyOnLoad(MFPMPUI.playerAvatarTemplate);
            MFPEditorUtils.Log((MFPMPUI.playerAvatarTemplate != null).ToString());

            HarmonyPatches.InitPatches();
            CustomizationAssets.Load();

            //harddisk gelene kadar yok maalesef, çok istiyorsan yeni bir package yaparsın
            Debugging.debugText = DiscordCT.multiplayerBundle.LoadAsset("DebugText") as GameObject;
            //  debugText = DiscordCT.debugBundle.LoadAsset("DebugText") as GameObject;
            Debugging.debugCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(Debugging.debugCube.GetComponent<BoxCollider>());
            Debugging.debugCube.AddComponent<DebugDisappearObject>();
            Debugging.debugCube.SetActive(false);

            new GameObject("MultiplayerManager").AddComponent<MultiplayerManagerTest>();
        }

        MFPEditorUtils.ClearLog();
        MFPEditorUtils.InitGUILogging();
    }
}
