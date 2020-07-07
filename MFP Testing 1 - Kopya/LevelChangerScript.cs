// Decompiled with JetBrains decompiler
// Type: LevelChangerScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

//Not to be confused with the script in Networked Entities folder

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityScript.Lang;
using Steamworks;
using Jhrino.MFPMultiplayer.NetworkEntities;


[Serializable]
public class LevelChangerScript : MonoBehaviour
{
    public SwitchScript[] inputSwitch;
    private float switchInput;
    public int levelNumber;
    public bool relative;
    public bool saveState;
    public bool dontShowEndScreen;
    private RootScript root;

    private List<CSteamID> levelCompletors = new List<CSteamID>();

    public LevelChangerScript()
    {
        levelNumber = 1;
        relative = true;
        saveState = true;
    }

    public void Awake()
    {
        Jhrino.MFPMultiplayer.NetworkEntities.LevelChangerScript networkLevelChng = gameObject.AddComponent<Jhrino.MFPMultiplayer.NetworkEntities.LevelChangerScript>();

        networkLevelChng.inputSwitch = inputSwitch;
        networkLevelChng.levelNumber = levelNumber;
        networkLevelChng.saveState = saveState;
        networkLevelChng.dontShowEndScreen = dontShowEndScreen;
        Destroy(this);
    }
}

