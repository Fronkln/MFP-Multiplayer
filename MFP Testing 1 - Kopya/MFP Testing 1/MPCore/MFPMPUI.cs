using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;


public class MFPMPUI : MonoBehaviour
{
    public Button startStopServerButton;
    public InputField chatField;
    public Text debugPlayerCount;
    public Toggle toggleMPGhost;

    public static bool isTyping = false;

    private MultiplayerManagerTest mpManager;
    public static MFPMPUI inst;

    public static GameObject retailHintFocus;

    public bool disableGhostsOnInitDoOnce = true;

    public void Awake()
    {
        inst = this;

        chatField = transform.Find("chatbox").GetComponent<InputField>();
        debugPlayerCount = transform.Find("DebugPlayerCount").GetComponent<Text>();

        startStopServerButton = transform.Find("startServerButton").GetComponent<Button>();
        startStopServerButton.onClick.AddListener(delegate { OnClickStartStopButton(); });
        startStopServerButton.gameObject.SetActive(false);//just add disconnect to menu;

        toggleMPGhost = transform.Find("debugShowGhost").GetComponent<Toggle>();
        toggleMPGhost.onValueChanged.AddListener(delegate { OnToggleDebugGhost(); });

        mpManager = MultiplayerManagerTest.inst;
    }

    public void OnClickStartStopButton()
    {
        if (startStopServerButton.GetComponentInChildren<Text>().text.ToLower().Contains("start"))
            mpManager.StartOrStopServer(true);
        else
            mpManager.StartOrStopServer(false);
    }

    public void OnToggleDebugGhost()
    {
        mpManager.playerObjects[mpManager.playerID].ToggleGhost();
    }

    public void OverrideStandardHud()
    {
        if (retailHintFocus != null && retailHintFocus.activeSelf)
            retailHintFocus.gameObject.SetActive(false);
    }

    public void LateUpdate()
    {
        OverrideStandardHud();
    }

    public void Update()
    {
        if (mpManager.root.levelEnded)
            gameObject.SetActive(false);

        if (chatField.enabled && !MultiplayerManagerTest.connected)
            chatField.enabled = false;
        else if (!chatField.enabled && MultiplayerManagerTest.connected)
            chatField.enabled = true;


        if (toggleMPGhost.enabled && !MultiplayerManagerTest.connected)
        {
            toggleMPGhost.enabled = false;
            toggleMPGhost.isOn = true;
        }
        else if (!toggleMPGhost.enabled && MultiplayerManagerTest.connected)
            toggleMPGhost.enabled = true;

        if (chatField.isFocused && !isTyping)
            isTyping = true;



        if (isTyping && !chatField.isFocused)
        {
            MFPEditorUtils.Log("\nAttempting to send message: " + chatField.text);
            byte[] message = Encoding.UTF8.GetBytes(chatField.text);


            if (!SteamMatchmaking.SendLobbyChatMsg(MultiplayerManagerTest.inst.globalID, message, message.Length))
                MFPEditorUtils.LogError("Failed to send message");
            else
                MFPEditorUtils.Log("Sent the message");


            chatField.text = "";
            isTyping = false;
        }
    }
}

