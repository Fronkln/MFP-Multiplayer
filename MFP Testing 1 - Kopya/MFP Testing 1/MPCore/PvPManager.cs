using System.Collections;
using UnityEngine;
using Steamworks;

public class PvPManager : MonoBehaviour
{

    private RootScript root;

    public readonly float countdown = 5;
    public readonly float slowMotionDuration = 6;

    public static PvPManager instance;


    private float lastTimeSinceSlowmo = 0;
    private float slowMotionCountdown = 0;
    private float slowMotionTimeRemaining = 0;

    private bool inSlowmoEvent = false;
    private bool countingDown = false;

    public bool slowMotionActivated = false;


    //will check every X seconds to decide if we want to do slow mo event
    IEnumerator SlowmotionEventRNG()
    {
        yield return new WaitForSeconds(3.5f);

        if (!inSlowmoEvent)
        {
            if (lastTimeSinceSlowmo >= 5) //seconds
            {
                int slowmoRng = UnityEngine.Random.Range(1, 7);

                if (slowmoRng <= 3)
                {
                    P2PMessage packet = new P2PMessage();
                    packet.WriteByte(18);

                    PacketSender.SendPackageToEveryone(packet, EP2PSend.k_EP2PSendReliable);

                   MFPEditorUtils.Log("sent package to start slowmotion event");
                }
            }
        }

        StartCoroutine(SlowmotionEventRNG());
    }


    public void OnSlowmotionEventStart()
    {
        lastTimeSinceSlowmo = 0;
        slowMotionCountdown = countdown;
        inSlowmoEvent = true;
        countingDown = true;
        MFPMPUI.inst.pvpSlowMotionEventText.enabled = true;
    }

    public void OnSlowmotionCountdownOver()
    {
        countingDown = false;
        slowMotionActivated = true;
        slowMotionTimeRemaining = slowMotionDuration;
       // MFPMPUI.inst.pvpSlowMotionEventText.enabled = false;

        MFPEditorUtils.Log("slowmotion begins!");

        RootSharedScript.Instance.modFocusSlowdownScale = 25;
        RootSharedScript.Instance.modInfiniteFocus = true;

        root.actionModeActivated = true;
        root.actionStateAudioSnapshot.TransitionTo(0.2f);
        root.lastActivatedAudioSnapshot = 1;

    }

    public void OnSlowmotionEventOver()
    {
        slowMotionActivated = false;
        inSlowmoEvent = false;
        lastTimeSinceSlowmo = 0;

        root.actionModeActivated = false;

        MFPMPUI.inst.pvpSlowMotionEventText.enabled = false;
        
    }

    void Awake()
    {
        if (MultiplayerManagerTest.playingAsHost)
            StartCoroutine(SlowmotionEventRNG());


        root = RootScript.RootScriptInstance;

        instance = this;
    }


    void Update()
    {
        if (MultiplayerManagerTest.playingAsHost)
        {

            if (!inSlowmoEvent)
                lastTimeSinceSlowmo += Time.deltaTime;
        }

        if (countingDown)
        {
            slowMotionCountdown -= Time.deltaTime;
            int timeInSecond = (int)slowMotionCountdown;

            if (slowMotionCountdown < 0)
                slowMotionCountdown = 0;

            MFPMPUI.inst.pvpSlowMotionEventText.text = "SLOWMOTION \nIN " + timeInSecond.ToString();


            if (slowMotionCountdown == 0)
                OnSlowmotionCountdownOver();

        }

        if (slowMotionActivated)
        {
            slowMotionTimeRemaining -= Time.unscaledDeltaTime;

            int slowMotionTimeAsSec = (int)slowMotionTimeRemaining;

            MFPMPUI.inst.pvpSlowMotionEventText.text = slowMotionTimeAsSec.ToString();

            if (slowMotionTimeRemaining <= 0)
                OnSlowmotionEventOver();
        }
    }
}

