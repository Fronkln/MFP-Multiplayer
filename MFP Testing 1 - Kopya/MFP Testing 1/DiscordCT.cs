using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class DiscordCT
{

    //dont let the class name fool you, use this class for mod initialization

    public static DiscordController discordController;
    public static AssetBundle multiplayerBundle;
    public static AssetBundle debugBundle;

    public static GameObject multiplayerUI;

    public static bool alreadyLoaded = false;

    public static void Init()
    {
        if (discordController != null) return;

        if (!alreadyLoaded)
        {
            multiplayerBundle = AssetBundle.LoadFromFile(MFPEditorUtils.LoadFile("mfpmultiplayer"));
            debugBundle = AssetBundle.LoadFromFile(MFPEditorUtils.LoadFile("mfpmultiplayer_debug"));

            multiplayerUI = multiplayerBundle.LoadAsset("MFPMultiplayerCanvas") as GameObject;
            multiplayerUI.AddComponent<MFPMPUI>();
            alreadyLoaded = true;

            HarmonyPatches.InitPatches();
            new GameObject().AddComponent<DiscordController>();
            new GameObject().AddComponent<MultiplayerManagerTest>().transform.name = "MultiplayerManager";
        }

        MFPEditorUtils.ClearLog();
        MFPEditorUtils.InitGUILogging();
    }
}
