// Decompiled with JetBrains decompiler
// Type: RootSharedScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using Boo.Lang.Runtime;
using ConfigurationLibrary;
using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class RootSharedScript : MonoBehaviour
{
    [Header("Build version number")]
    public float currentBuildVersionNumber;
    [Header("Tick if build is showfloor demo - Don't forget to change Build Settings to include/exclude the appropriate levels")]
    public bool isDemo;
    public bool isNintendoDemo;
    [Header("Other options")]
    public bool allowDebugMenu;
    public bool chineseBuild;
    [Header("Below variables are set automatically - Please don't change anything")]
    public bool runningOnConsole;
    public bool doMainMenuIntro;
    public bool disableFriendsLeaderboardFilter;
    public bool disablePedroHints;
    public bool disableRumble;
    public bool godMode;
    public float gamepadAimSens;
    public int aimAssistMode;
    public bool simulateMousePos;
    public float simulateMousePosSensitivity;
    public Vector2 fakeMousePos;
    private Vector2 fakeMousePos2;
    public int loadingScreenLevelToLoad;
    public bool levelLoadedFromLevelSelectScreen;
    private SteamLeaderboardsScript steamLeaderboardsScript;
    private StatsTrackerScript statsTracker;
    private cSharpSharedScript cSharpShared;
    private InputHelperScript inputHelperScript;
    public int curVisualQualityLevel;
    public bool lowEndHardware;
    private bool uploadAllScores;
    private int curUploadAllScores;
    public string userID;
    public bool neverChangeMouseCursor;
    public bool speedrunnerLeaderboard;
    public bool showUITimer;
    private GameObject uiTimer;
    public bool hideHUD;
    [NonSerialized]
    public static RootSharedScript Instance;
    [Header("Game Modifiers")]
    public bool modAllWeapons;
    public bool modInfiniteAmmo;
    public bool modOneShotEnemies;
    public bool modOneShotPlayer;
    public bool modDisableCheckpoints;
    public bool modIncreaseAccuracy;
    public float modFocusSlowdownScale;
    public float modPlayerSpeed;
    public bool modInfiniteFocus;
    public bool modSideOnCamera;
    public bool modBigHead;
    public float modPlayerSize;
    public float modEnemyBulletSpeed;
    public float modPlayerBulletSpeed;
    public bool modCinematicCamera;

    public RootSharedScript()
    {
        this.gamepadAimSens = 1f;
        this.simulateMousePosSensitivity = 0.25f;
        this.modFocusSlowdownScale = 25f;
        this.modPlayerSpeed = 30f;
        this.modPlayerSize = 100f;
        this.modEnemyBulletSpeed = 50f;
        this.modPlayerBulletSpeed = 50f;
    }

    public virtual bool allowLeaderboard
    {
        get
        {
            return (UnityEngine.Object)this.steamLeaderboardsScript != (UnityEngine.Object)null && this.steamLeaderboardsScript.AreLeaderboardsAllowed();
        }
    }

    public virtual bool lostInternetConnection
    {
        get
        {
            return !((UnityEngine.Object)this.steamLeaderboardsScript != (UnityEngine.Object)null) || this.steamLeaderboardsScript.LostInternetConnection();
        }
    }

    public virtual void OnEnable()
    {
        this.steamLeaderboardsScript = (SteamLeaderboardsScript)this.GetComponent(typeof(SteamLeaderboardsScript));
        if ((UnityEngine.Object)this.steamLeaderboardsScript == (UnityEngine.Object)null)
            this.steamLeaderboardsScript = (SteamLeaderboardsScript)this.gameObject.AddComponent(typeof(SteamLeaderboardsScript));
        this.statsTracker = (StatsTrackerScript)this.GetComponent(typeof(StatsTrackerScript));
        if ((UnityEngine.Object)this.statsTracker == (UnityEngine.Object)null)
            this.statsTracker = (StatsTrackerScript)this.gameObject.AddComponent(typeof(StatsTrackerScript));
        this.cSharpShared = (cSharpSharedScript)this.GetComponent(typeof(cSharpSharedScript));
        if ((UnityEngine.Object)this.cSharpShared == (UnityEngine.Object)null)
            this.cSharpShared = (cSharpSharedScript)this.gameObject.AddComponent(typeof(cSharpSharedScript));
        this.userID = this.steamLeaderboardsScript.GetUserID();
        if (!this.allowLeaderboard)
            return;
        this.uploadAllScores = true;
    }

    public virtual void Awake()
    {
        RootSharedScript.Instance = this;
        int num = PlatformPlayerPrefs.GetInt("QualitySetting");
        if (num != 0)
        {
            this.curVisualQualityLevel = num - 1;
            QualitySettings.SetQualityLevel(num - 1, true);
            QualitySettings.vSyncCount = PlatformPlayerPrefs.GetInt("VSync");
        }
        else
        {
            this.curVisualQualityLevel = 1;
            QualitySettings.SetQualityLevel(1, true);
            QualitySettings.vSyncCount = PlatformPlayerPrefs.GetInt("VSync");
        }
        if (!PlatformPlayerPrefs.HasKey("TargetFPS"))
            PlatformPlayerPrefs.SetInt("TargetFPS", 2);
        switch (PlatformPlayerPrefs.GetInt("TargetFPS"))
        {
            case 1:
                Application.targetFrameRate = 30;
                break;
            case 2:
                Application.targetFrameRate = 60;
                break;
            case 3:
                Application.targetFrameRate = 75;
                break;
            case 4:
                Application.targetFrameRate = 100;
                break;
            case 5:
                Application.targetFrameRate = 120;
                break;
            case 6:
                Application.targetFrameRate = 144;
                break;
            case 7:
                Application.targetFrameRate = 165;
                break;
            case 8:
                Application.targetFrameRate = 240;
                break;
            default:
                Application.targetFrameRate = 0;
                break;
        }
        GameObject gameObject = new GameObject();
        gameObject.name = "NvidiaHighlights";
        gameObject.AddComponent(typeof(NvidiaHighlightsScript));
        if (this.GetArg("-neverChangeMouseCursor") == "1")
            this.neverChangeMouseCursor = true;
        if (this.GetArg("-SpeedrunnerLeaderboard") == "1")
            this.speedrunnerLeaderboard = true;
        if (!this.speedrunnerLeaderboard)
            return;
        PlatformManager.speedrunnerLeaderboard = true;
    }

    public virtual void Start()
    {
        if (Application.platform == RuntimePlatform.Switch)
            this.runningOnConsole = true;
        if (this.runningOnConsole)
            Cursor.visible = false;
        this.disablePedroHints = SavedData.GetInt("DisablePedroHints") == 1;
        this.showUITimer = SavedData.GetInt("ShowUITimer") == 1;
        this.disableRumble = SavedData.GetInt("DisableRumble") == 1;
        if (SavedData.HasKey("gamepadAimSens"))
            this.gamepadAimSens = SavedData.GetFloat("gamepadAimSens");
        this.aimAssistMode = !SavedData.HasKey("AimAssistMode") ? 1 : SavedData.GetInt("AimAssistMode");
        if (SavedData.HasKey("cursorMode"))
            this.simulateMousePos = SavedData.GetInt("cursorMode") == 1;
        if (SavedData.HasKey("mouseAimSens"))
            this.simulateMousePosSensitivity = SavedData.GetFloat("mouseAimSens");
        if (SavedData.HasKey("language"))
            LocalizationManager.CurrentLanguageCode = SavedData.GetString("language");
        this.modAllWeapons = SavedData.GetInt("modAllWeapons") == 1;
        this.modInfiniteAmmo = SavedData.GetInt("modInfiniteAmmo") == 1;
        this.modOneShotEnemies = SavedData.GetInt("modOneShotEnemies") == 1;
        this.modOneShotPlayer = SavedData.GetInt("modOneShotPlayer") == 1;
        this.modDisableCheckpoints = SavedData.GetInt("modDisableCheckpoints") == 1;
        this.modIncreaseAccuracy = SavedData.GetInt("modIncreaseAccuracy") == 1;
        this.modFocusSlowdownScale = !SavedData.HasKey("modFocusSlowdownScale") ? 25f : (float)SavedData.GetInt("modFocusSlowdownScale");
        this.modPlayerSpeed = !SavedData.HasKey("modPlayerSpeed") ? 30f : (float)SavedData.GetInt("modPlayerSpeed");
        this.modInfiniteFocus = SavedData.GetInt("modInfiniteFocus") == 1;
        this.modSideOnCamera = SavedData.GetInt("modSideOnCamera") == 1;
        this.modBigHead = SavedData.GetInt("modBigHead") == 1;
        this.modPlayerSize = !SavedData.HasKey("modPlayerSize") ? 100f : (float)SavedData.GetInt("modPlayerSize");
        this.modEnemyBulletSpeed = !SavedData.HasKey("modEnemyBulletSpeed") ? 50f : (float)SavedData.GetInt("modEnemyBulletSpeed");
        this.modPlayerBulletSpeed = !SavedData.HasKey("modPlayerBulletSpeed") ? 50f : (float)SavedData.GetInt("modPlayerBulletSpeed");
        this.modCinematicCamera = SavedData.GetInt("modCinematicCamera") == 1;
        this.hideHUD = SavedData.HasKey("ShowHUD") && SavedData.GetInt("ShowHUD") == 0;
        this.CheckForBrokenScores();
        this.DoVersionCheck();
        GameObject gameObject = GameObject.Find("Rewired Input Manager");
        if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
            this.inputHelperScript = (InputHelperScript)gameObject.GetComponent(typeof(InputHelperScript));
        this.uiTimer = UnityEngine.Object.Instantiate<GameObject>(UnityEngine.Resources.Load("HUD/UITimer") as GameObject);
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object)this.gameObject);
    }

    public virtual void Update()
    {
        if (this.simulateMousePos)
        {
            this.fakeMousePos += new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * (0.005f + this.simulateMousePosSensitivity) * 68f * (Mathf.Round((float)(Screen.width + Screen.height)) / 2f / 1500f);
            this.fakeMousePos.x = Mathf.Clamp(this.fakeMousePos.x, 0.0f, (float)Screen.width);
            this.fakeMousePos.y = Mathf.Clamp(this.fakeMousePos.y, 0.0f, (float)Screen.height);
        }
        if (!this.allowLeaderboard || !this.uploadAllScores)
            return;
        if (this.curUploadAllScores <= 52)
        {
            if (!this.OkToUploadScore())
                return;
            string empty = string.Empty;
            for (float num1 = new float(); (double)num1 <= 2.0; ++num1)
            {
                if (SavedData.GetString(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)this.curUploadAllScores), "diff"), (object)num1), "ID"))) == "-999")
                {
                    int num2 = empty == "-999" ? 1 : 0;
                }
            }
            string leaderboardName = this.GetLeaderboardName(this.curUploadAllScores, string.Empty, false);
            if (leaderboardName != string.Empty)
            {
                float num1 = new float();
                float num2 = !this.speedrunnerLeaderboard ? this.GetBestScoreForLevel(this.curUploadAllScores, true) : this.GetBestTimeForLevel(this.curUploadAllScores) * 1000f;
                if ((double)num2 > 0.0 && (double)num2 < 9999999.0)
                    this.steamLeaderboardsScript.DoUploadScore((int)num2, leaderboardName);
            }
            ++this.curUploadAllScores;
        }
        else
            this.uploadAllScores = false;
    }

    public virtual bool gameModifiersCheck()
    {
        int num = this.modAllWeapons ? 1 : 0;
        if (num == 0)
            num = this.modInfiniteAmmo ? 1 : 0;
        if (num == 0)
            num = this.modOneShotEnemies ? 1 : 0;
        if (num == 0)
            num = this.modOneShotPlayer ? 1 : 0;
        if (num == 0)
            num = this.modIncreaseAccuracy ? 1 : 0;
        if (num == 0)
            num = (double)this.modFocusSlowdownScale != 25.0 ? 1 : 0;
        if (num == 0)
            num = (double)this.modPlayerSpeed != 30.0 ? 1 : 0;
        if (num == 0)
            num = this.modInfiniteFocus ? 1 : 0;
        if (num == 0)
            num = this.modSideOnCamera ? 1 : 0;
        if (num == 0)
            num = this.modBigHead ? 1 : 0;
        if (num == 0)
            num = (double)this.modPlayerSize != 100.0 ? 1 : 0;
        if (num == 0)
            num = (double)this.modEnemyBulletSpeed != 50.0 ? 1 : 0;
        if (num == 0)
            num = (double)this.modPlayerBulletSpeed != 50.0 ? 1 : 0;
        return num != 0 ? num != 0 : this.modCinematicCamera;
    }

    public virtual void restoreDefaultGameModifiers()
    {
        this.modAllWeapons = false;
        this.modInfiniteAmmo = false;
        this.modOneShotEnemies = false;
        this.modOneShotPlayer = false;
        this.modIncreaseAccuracy = false;
        this.modFocusSlowdownScale = 25f;
        this.modPlayerSpeed = 30f;
        this.modInfiniteFocus = false;
        this.modSideOnCamera = false;
        this.modBigHead = false;
        this.modPlayerSize = 100f;
        this.modEnemyBulletSpeed = 50f;
        this.modPlayerBulletSpeed = 50f;
        this.modCinematicCamera = false;
    }

    public virtual bool OkToUploadScore()
    {
        return this.steamLeaderboardsScript.OkToUploadScore();
    }

    public virtual float GetBestScoreForLevel(int lvlNr, bool checkUserID)
    {
        float num1 = 0.0f;
        for (float num2 = new float(); (double)num2 <= 2.0; ++num2)
        {
            int num3 = SavedData.GetInt(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)lvlNr), "diff"), (object)num2)));
            if ((double)num3 > (double)num1)
            {
                if (checkUserID)
                {
                    string str = SavedData.GetString(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)lvlNr), "diff"), (object)num2), "ID")));
                    if (this.userID == "0" && str != "-1" && str != "-999" || str == this.userID)
                        num1 = (float)num3;
                }
                else
                    num1 = (float)num3;
            }
        }
        return num1;
    }

    public virtual float GetBestTimeForLevel(int lvlNr)
    {
        float num1 = 9999999f;
        for (float num2 = new float(); (double)num2 <= 2.0; ++num2)
        {
            float num3 = SavedData.GetFloat(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlTime", (object)lvlNr), "diff"), (object)num2)));
            if ((double)num3 != 0.0 && (double)num3 < (double)num1)
                num1 = num3;
        }
        return num1;
    }

    public virtual void CheckForBrokenScores()
    {
        for (int index = new int(); index <= 52; ++index)
        {
            for (float num1 = new float(); (double)num1 <= 2.0; ++num1)
            {
                int num2 = SavedData.GetInt(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)index), "diff"), (object)num1)));
                if (num2 >= 9999999 || num2 < 0)
                {
                    MonoBehaviour.print((object)RuntimeServices.op_Addition("BROKEN SCORE DETECTED: ", (object)num2));
                    SavedData.SetInt(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)index), "diff"), (object)num1)), 0);
                }
            }
        }
    }

    public virtual void DoVersionCheck()
    {
        MonoBehaviour.print((object)"Checking build number...");
        float num1 = SavedData.GetFloat(CryptoString.Encrypt("BuildVersionNumber"));
        if ((double)this.currentBuildVersionNumber == 0.0 || (double)num1 == (double)this.currentBuildVersionNumber || ((double)num1 == 1.01999998092651 || (double)this.currentBuildVersionNumber != 1.02999997138977))
            return;
        for (int index = new int(); index <= 52; ++index)
        {
            for (float num2 = new float(); (double)num2 <= 2.0; ++num2)
            {
                if (SavedData.HasKey(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)index), "diff"), (object)num2), "ID"))))
                    SavedData.SetString(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)index), "diff"), (object)num2), "ID")), "-1");
            }
        }
        SavedData.SetFloat(CryptoString.Encrypt("BuildVersionNumber"), this.currentBuildVersionNumber);
        MonoBehaviour.print((object)"Save from older build. Disabling auto-score-upload for saved scores on boot.");
    }

    public virtual string addCommasToNumber(float nr)
    {
        return this.cSharpShared.addCommasToNumber(nr);
    }

    public virtual string GetTranslation(string id)
    {
        string str = LocalizationManager.GetTranslation(id, true, 0, true, false, (GameObject)null, (string)null);
        if (str == (string)null)
        {
            str = RuntimeServices.op_Addition("LOCALIZATION ID NOT FOUND: ", id);
            Debug.Log((object)str);
        }
        char ch = Convert.ToChar(160);
        return str.Replace(" !", RuntimeServices.op_Addition((object)ch, "!")).Replace(" ?", RuntimeServices.op_Addition((object)ch, "?")).Replace(" :", RuntimeServices.op_Addition((object)ch, ":")).Replace(" ;", RuntimeServices.op_Addition((object)ch, ";"));
    }

    public virtual GameObject createHintText(
      string txt,
      string holderName,
      Transform theParent,
      bool useGamepadIcons,
      bool setWidth)
    {
        if (!(txt != string.Empty))
            return (GameObject)null;
        GameObject gameObject1 = new GameObject();
        RectTransform rectTransform = (RectTransform)gameObject1.AddComponent(typeof(RectTransform));
        rectTransform.sizeDelta = new Vector2(0.0f, 0.0f);
        rectTransform.SetParent(theParent, false);
        gameObject1.name = holderName;
        string[] strArray1 = txt.Split(">"[0]);
        float num1 = new float();
        for (int index = new int(); index < strArray1.Length; ++index)
        {
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(UnityEngine.Resources.Load("HUD/HintText") as GameObject);
            Text component1 = (Text)gameObject2.GetComponent(typeof(Text));
            string[] strArray2 = strArray1[index].Split("<"[0]);
            component1.text = strArray2.Length <= 1 ? strArray1[index] : strArray2[0];
            gameObject2.transform.SetParent((Transform)rectTransform, false);
            RectTransform component2 = (RectTransform)gameObject2.GetComponent(typeof(RectTransform));
            if (index > 0)
            {
                if (setWidth)
                    component2.anchorMin = component2.anchorMax = new Vector2(0.5f, 0.5f);
                float num2 = num1;
                Vector2 anchoredPosition = component2.anchoredPosition;
                double num3 = (double)(anchoredPosition.x = num2);
                Vector2 vector2 = component2.anchoredPosition = anchoredPosition;
            }
            num1 += component1.preferredWidth;
            if (strArray2.Length > 1)
            {
                if ((UnityEngine.Object)this.inputHelperScript == (UnityEngine.Object)null)
                    this.inputHelperScript = (InputHelperScript)GameObject.Find("Rewired Input Manager").GetComponent(typeof(InputHelperScript));
                GameObject inputSymbol = this.inputHelperScript.GetInputSymbol(strArray2[1], !useGamepadIcons);
                if ((UnityEngine.Object)inputSymbol != (UnityEngine.Object)null)
                {
                    RectTransform component3 = (RectTransform)inputSymbol.GetComponent(typeof(RectTransform));
                    component3.SetParent((Transform)rectTransform, false);
                    if (setWidth)
                        component3.anchorMin = component3.anchorMax = new Vector2(0.5f, 0.5f);
                    float num2 = num1 + component3.sizeDelta.x / 2f - 5f;
                    Vector2 anchoredPosition = component3.anchoredPosition;
                    double num3 = (double)(anchoredPosition.x = num2);
                    Vector2 vector2 = component3.anchoredPosition = anchoredPosition;
                    num1 += component3.sizeDelta.x;
                }
            }
        }
        float num4 = rectTransform.anchoredPosition.x - num1 / 2f;
        Vector2 anchoredPosition1 = rectTransform.anchoredPosition;
        double num5 = (double)(anchoredPosition1.x = num4);
        Vector2 vector2_1 = rectTransform.anchoredPosition = anchoredPosition1;
        int num6 = -80;
        Vector2 anchoredPosition2 = rectTransform.anchoredPosition;
        double num7 = (double)(anchoredPosition2.y = (float)num6);
        Vector2 vector2_2 = rectTransform.anchoredPosition = anchoredPosition2;
        if (setWidth)
        {
            float num2 = num1;
            Vector2 sizeDelta = rectTransform.sizeDelta;
            double num3 = (double)(sizeDelta.x = num2);
            Vector2 vector2_3 = rectTransform.sizeDelta = sizeDelta;
        }

        if (txt == LocalizationManager.GetTranslation("hint3"))
            MFPMPUI.retailHintFocus = gameObject1;

        return gameObject1;
    }

    public virtual string GetMultiplayerLevelName(int lvlNr)
    {
        string category = "UNDEFINED";
        int level = 1;


        if (lvlNr == 3)
            return "New Game";

        if (lvlNr > 3 && lvlNr < 6)
            return "Tutorial Level" + " " + (lvlNr - 2).ToString();

        if (lvlNr > 5 && lvlNr <= 14)
            category = "Old Town";
        if (lvlNr > 14 && lvlNr <= 24)
            category = "District Null";
        if (lvlNr > 24 && lvlNr <= 28)
            category = "Pedro's World";
        if (lvlNr > 28 && lvlNr <= 41)
            category = "The Sewer";
        if (lvlNr > 41 && lvlNr <= 50)
            category = "The Internet";
        if (lvlNr >= 51)
            category = "The End";

        switch (category)
        {
            case "Old Town":
                level = lvlNr - 6;
                break;
            case "District Null":
                level = lvlNr - 15;
                break;
            case "Pedro's World":
                level = lvlNr - 24;
                break;
            case "The Sewer":
                level = lvlNr - 30;
                break;
            case "The Internet":
                level = lvlNr - 41;
                break;
            case "The End":
                level = 1;
                break;
        }


        if (lvlNr == 13)
            return "Old Town Motorcycle Cutscene";


        return category + " " + level.ToString();
    }

    public virtual string GetLeaderboardName(int lvlNr, string idCheck, bool givePlain) //you probably want to make a seperate method for level names   
    {
        string lhs;
        switch (lvlNr)
        {
            case 3:
                lhs = "New Game";
                break;
            case 4:
                lhs = "Tutorial Level 1";
                break;
            case 5:
                lhs = "Tutorial Level 2";
                break;
            case 6:
                lhs = "w1-1";
                break;
            case 7:
                lhs = "w1-2";
                break;
            case 8:
                lhs = "w1-3";
                break;
            case 9:
                lhs = "w1-4";
                break;
            case 10:
                lhs = "w1-5";
                break;
            case 11:
                lhs = "w1-6";
                break;
            case 12:
                lhs = "w1-7";
                break;
            case 14:
                lhs = "w1-8";
                break;
            case 16:
                lhs = "w2-1";
                break;
            case 17:
                lhs = "w2-2";
                break;
            case 18:
                lhs = "w2-3";
                break;
            case 19:
                lhs = "w2-4";
                break;
            case 20:
                lhs = "w2-5";
                break;
            case 21:
                lhs = "w2-6";
                break;
            case 22:
                lhs = "w2-7";
                break;
            case 23:
                lhs = "w2-8";
                break;
            case 24:
                lhs = "w2-9";
                break;
            case 25:
                lhs = "w3-1";
                break;
            case 26:
                lhs = "w3-2";
                break;
            case 27:
                lhs = "w3-3";
                break;
            case 28:
                lhs = "w3-4";
                break;
            case 31:
                lhs = "w4-1";
                break;
            case 32:
                lhs = "w4-2";
                break;
            case 33:
                lhs = "w4-3";
                break;
            case 34:
                lhs = "w4-4";
                break;
            case 35:
                lhs = "w4-5";
                break;
            case 36:
                lhs = "w4-6";
                break;
            case 37:
                lhs = "w4-7";
                break;
            case 38:
                lhs = "w4-8";
                break;
            case 39:
                lhs = "w4-9";
                break;
            case 41:
                lhs = "w4-10";
                break;
            case 43:
                lhs = "w5-1";
                break;
            case 44:
                lhs = "w5-2";
                break;
            case 45:
                lhs = "w5-3";
                break;
            case 46:
                lhs = "w5-4";
                break;
            case 47:
                lhs = "w5-5";
                break;
            case 48:
                lhs = "w5-6";
                break;
            case 49:
                lhs = "w5-7";
                break;
            case 50:
                lhs = "w5-8";
                break;
            case 52:
                lhs = "w6-1";
                break;
            default:
                lhs = string.Empty;
                break;
        }
        if (givePlain)
        {
            switch (lvlNr)
            {
                case 3:
                    lhs = "w1-0.1";
                    break;
                case 4:
                    lhs = "w1-0.2";
                    break;
                case 5:
                    lhs = "w1-0.3";
                    break;
                case 13:
                    lhs = "w1-7.1";
                    break;
                case 15:
                    lhs = "w2-0.1";
                    break;
                case 29:
                    lhs = "w4-0.1";
                    break;
                case 30:
                    lhs = "w4-0.2";
                    break;
                case 40:
                    lhs = "w4-9.1";
                    break;
                case 42:
                    lhs = "w5-0.1";
                    break;
                case 51:
                    lhs = "w5-8.1";
                    break;
                case 53:
                    lhs = "w6-1.1";
                    break;
            }
        }
        if (!givePlain)
        {
            if (this.speedrunnerLeaderboard)
                lhs = RuntimeServices.op_Addition(lhs, "t");
            else if (idCheck != "-999")
                lhs = RuntimeServices.op_Addition(lhs, "n");
        }
        if (givePlain && lhs != string.Empty)
            lhs = lhs.Remove(0, 1);
        return lhs;
    }

    public virtual void AttemptToReconnect(string leaderboardName)
    {
        if (!this.allowLeaderboard)
            return;
        this.steamLeaderboardsScript.AttemptToReconnect(leaderboardName);
    }

    public virtual void PrepareLeaderboardDisplayEntries()
    {
        if (!this.allowLeaderboard)
            return;
        this.steamLeaderboardsScript.PrepareDisplayEntries();
    }

    public virtual void DoEndOfLevelShowTopLeaderboardScore(string leaderboardName)
    {
        if (!this.allowLeaderboard)
            return;
        this.steamLeaderboardsScript.DoEndOfLevelShowTopLeaderboardScore(leaderboardName);
    }

    public virtual void DoShowLeaderboard(string leaderboardName, int filterMode)
    {
        if (!this.allowLeaderboard)
            return;
        this.steamLeaderboardsScript.DoShowLeaderboard(leaderboardName, filterMode);
    }

    public virtual void DoUploadScore(int theScore, string leaderboardName)
    {
        if (!this.allowLeaderboard)
            return;
        this.steamLeaderboardsScript.DoUploadScore(theScore, leaderboardName);
    }

    public virtual void ResetLeaderboardPage()
    {
        if (!this.allowLeaderboard)
            return;
        PlatformManager.pageOffset = 0;
    }

    public virtual void NextLeaderboardPage()
    {
        if (!this.allowLeaderboard || !this.steamLeaderboardsScript.leaderboardAllowChangePage || this.steamLeaderboardsScript.leaderboardLastEntryDisplayedNr == this.steamLeaderboardsScript.leaderboardLength)
            return;
        PlatformManager.pageOffset += 10;
        this.steamLeaderboardsScript.leaderboardAllowChangePage = false;
    }

    public virtual void PreviousLeaderboardPage()
    {
        if (!this.allowLeaderboard || !this.steamLeaderboardsScript.leaderboardAllowChangePage || this.steamLeaderboardsScript.leaderboardFirstEntryDisplayedNr == 1)
            return;
        PlatformManager.pageOffset -= 10;
        this.steamLeaderboardsScript.leaderboardAllowChangePage = false;
    }

    public virtual void adjustUIForAspectRatio()
    {
        GameObject gameObject = GameObject.Find("MainMenuCanvas");
        if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
            gameObject = GameObject.Find("HUD/Canvas");
        if ((double)Camera.main.aspect > 2.29999995231628)
        {
            if (!((UnityEngine.Object)gameObject != (UnityEngine.Object)null))
                return;
            ((CanvasScaler)gameObject.GetComponent(typeof(CanvasScaler))).matchWidthOrHeight = 1f;
        }
        else
        {
            if (!((UnityEngine.Object)gameObject != (UnityEngine.Object)null))
                return;
            ((CanvasScaler)gameObject.GetComponent(typeof(CanvasScaler))).matchWidthOrHeight = 0.5f;
        }
    }

    public virtual string GetArg(string name)
    {
        string[] commandLineArgs = Environment.GetCommandLineArgs();
        for (int index = 0; index < commandLineArgs.Length; ++index)
        {
            if (commandLineArgs[index] == name && commandLineArgs.Length > index + 1)
                return commandLineArgs[index + 1];
        }
        return (string)null;
    }
}
