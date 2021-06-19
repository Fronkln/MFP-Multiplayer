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

    public Text pvpSlowMotionEventText;

    public static bool isTyping = false;

    private MultiplayerManagerTest mpManager;
    public static MFPMPUI inst;

    public static GameObject retailHintFocus;

    public bool disableGhostsOnInitDoOnce = true;

    public static GameObject playerAvatarTemplate = null;

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

        pvpSlowMotionEventText = transform.Find("pvpSlowmotionEventText").GetComponent<Text>();
        pvpSlowMotionEventText.enabled = false;

        chatField.gameObject.SetActive(false);

#if !DEBUG
        toggleMPGhost.gameObject.SetActive(false);
#endif

        mpManager = MultiplayerManagerTest.inst;
    }

    public void Start()
    {
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


        if (Input.GetKeyDown(KeyCode.Escape))
            if (chatField.gameObject.activeSelf)
                CloseChat();


        //Pressed enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (chatField.gameObject.activeSelf)
            {
                SendChatMessage(chatField.text);
                CloseChat();
                isTyping = false;
            }
            else
            {
                OpenChat();
                isTyping = true;
            }
        }
    }

    private void CloseChat(bool clean = true)
    {
        if (clean)
            chatField.text = "";

        chatField.DeactivateInputField();
        chatField.gameObject.SetActive(false);
    }
    private void OpenChat()
    {
        chatField.gameObject.SetActive(true);
        chatField.Select();
        chatField.ActivateInputField();
    }


    private void SendChatMessage(string message)
    {
        MFPEditorUtils.Log("\nAttempting to send message: " + message);
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);


        if (!SteamMatchmaking.SendLobbyChatMsg(MultiplayerManagerTest.inst.globalID, messageBytes, message.Length))
            MFPEditorUtils.LogError("Failed to send message");
        else
            MFPEditorUtils.Log("Sent the message");
    }
}

