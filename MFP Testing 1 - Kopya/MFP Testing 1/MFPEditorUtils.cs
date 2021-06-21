using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using I2.Loc;
using System.Reflection;


class MFPEditorDebuggerRuntime : MonoBehaviour
{
    public List<string> logs = new List<string>();

    private GUIStyle redStyle;


    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

#if DEBUG
    public void OnGUI()
    {
        if (logs == null) 
            logs = new List<string>();


        if (logs.Count > 10)
            logs.RemoveAt(0);

        foreach (string log in logs)
            if (log != null)
                GUILayout.Label(log);

        if (Camera.main != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform != null)
                {
                    GUILayout.Button(hit.transform.name);

                    if (hit.transform.GetComponent<BaseNetworkEntity>())
                    {
                        BaseNetworkEntity ent = hit.transform.GetComponent<BaseNetworkEntity>();
                        GUILayout.Button("BaseNetworkEntity Properties:");
                        //GUILayout.Button("-----------------------------");
                        GUILayout.Button("Entity ID: " + ent.entityIdentifier.ToString());
                        GUILayout.Button("Current packages: " + ent.curPackets.ToString());
                        GUILayout.Button("Max packages: " + ent.maxAllowedPackets);
                        if (!ent.interactingPlayer.isNull())
                            GUILayout.Button("Interacting Player: " + Steamworks.SteamFriends.GetFriendPersonaName(ent.interactingPlayer));
                    }
                    else if (hit.transform.root.name.StartsWith("PlayerGhost"))
                        GUILayout.Button("GameObject Layer: " + hit.transform.gameObject.layer.ToString());
                            

                    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.U))
                    {
                        MFPEditorUtils.Log("------------");

                        foreach (Component comp in hit.transform.gameObject.GetComponents<Component>())
                        {
                            MFPEditorUtils.Log(comp.ToString() + " " + comp.gameObject.name);
                            MFPEditorUtils.Log("------------");
                        }

                        foreach (Component comp in hit.transform.gameObject.GetComponentsInChildren<Component>())
                        {
                            MFPEditorUtils.Log(comp.ToString() + " " + comp.gameObject.name);
                            MFPEditorUtils.Log("------------");
                        }
                    }
                    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P))
                        Destroy(hit.transform.gameObject);

                }

            }  
        }
        

        if (!MFPMPUI.isTyping && Input.GetKeyDown(KeyCode.Y))
        {
            PlayerScript.PlayerInstance.transform.position = new Vector3(PlayerScript.PlayerInstance.mousePos.x, PlayerScript.PlayerInstance.mousePos.y, 0.0f);
            PlayerScript.PlayerInstance.ySpeed = 1f;
        }
    }

#endif
    public void GUILog(string txt)
    {
        logs.Add(txt);
        if (logs.Count > 10)
            logs.RemoveAt(0);
    }
}


public static class MFPEditorUtils
{
    private static MFPEditorDebuggerRuntime guiInstance;
    public static string modName = "MFPMultiplayer_Jhrino";
    public static string modPath = Application.dataPath + @"/" + modName + @"/";

    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D texture2D = null;
        if (File.Exists(filePath))
        {
            byte[] data = File.ReadAllBytes(filePath);
            texture2D = new Texture2D(2, 2);

            texture2D.LoadImage(data);
        }
        return texture2D;
    }

    public static string LoadFile(string file)
    {
        if (!Directory.Exists(Application.dataPath))
            Directory.CreateDirectory(modPath);

        return modPath + file;
    }

    public static void ClearLog()
    {
        File.WriteAllLines(LoadFile(modName + "_log.txt"), new string[1] { string.Empty });
    }

    public static void InitGUILogging()
    {
        if (guiInstance == null)
            guiInstance = new GameObject().AddComponent<MFPEditorDebuggerRuntime>();
    }

    public static void Log(string st)
    {
        Debug.Log("[MFPEDITORUTILS]:" + st);
        File.AppendAllText(LoadFile(modName + "_log.txt"), Environment.NewLine + st);

        if (guiInstance == null)
            InitGUILogging();

        guiInstance.GUILog(st);
    }
    public static void Log(object text)
    {
        Log(text.ToString());
    }

    public static void LogError(string text)
    {
        Debug.Log("[MFPEDITORUTILS] [ERROR]:" + text);
        File.AppendAllText(LoadFile(modName + "_log.txt"), Environment.NewLine + text);

        if (guiInstance != null)
            guiInstance.GUILog("[ERROR]" + text);
    }

    public static void doPedroHint(string txt, float timer = -99999)
    {
        RootScript root = GameObject.Find("Root").GetComponent<RootScript>();

        if (root != null)
        {
            root.pedroHintTimer = timer;
            root.StartCoroutine(root.doPedroHint(txt));
        }
    }

    public static void DebugJSONObject(object obj)
    {
        string file = LoadFile("JSONOUTPUT/" + obj.ToString() + "_out.txt");

        if (!File.Exists(file))
            File.Create(file).Close();

        File.WriteAllLines(file, new string[1] { JsonUtility.ToJson(obj, true) });
    }

    public static void UnlockWeapons(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            PlayerScript.PlayerInstance.weaponActive[i] = true;
            PlayerScript.PlayerInstance.ammo[i] = PlayerScript.PlayerInstance.ammoFullClip[i];
            PlayerScript.PlayerInstance.ammoTotal[i] = PlayerScript.PlayerInstance.ammoTotal[i] + PlayerScript.PlayerInstance.ammoFullClip[i] * 2f;
        }
    }


    public static bool TranslationExists(string termName)
    {
        string test = "";

        LocalizationManager.TryGetTranslation(termName, out test);

        if (test.ToLower().Contains("missing translation"))
            return false;
        else return true;
    }

    public static void CreateTranslation(string termName, string translation)
    {
        string language = LocalizationManager.CurrentLanguage;

        var i2languagesPrefab = LocalizationManager.Sources[0];
        var termData = i2languagesPrefab.AddTerm(termName, eTermType.Text);

        // Find Language Index (or add the language if its a new one)
        int langIndex = i2languagesPrefab.GetLanguageIndex(language, false, false);
        if (langIndex < 0)
        {
            i2languagesPrefab.AddLanguage(language, GoogleLanguages.GetLanguageCode(language));
            langIndex = i2languagesPrefab.GetLanguageIndex(language, false, false);
        }

        termData.Languages[langIndex] = translation;
    }


    public static void ChangeTranslation(string termName, string translation)
    {
        if (!TranslationExists(termName))
        {
            CreateTranslation(termName, translation);
            return;
        }

        string language = LocalizationManager.CurrentLanguage;
        var i2languagesPrefab = LocalizationManager.Sources[0];

        TermData termdata = i2languagesPrefab.GetTermData(termName);
        int langIndex = i2languagesPrefab.GetLanguageIndex(language, false, false);

        termdata.Languages[langIndex] = translation;

    }

    public static string NormalizeInputFieldIntValue(string value, int min, int max)
    {
        int valConverted = int.Parse(value);

        if (valConverted < min)
            return min.ToString();
        if (valConverted > max)
            return max.ToString();

        return value;
    }
}
