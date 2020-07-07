// Decompiled with JetBrains decompiler
// Type: LevelCompleteScreenScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using Boo.Lang.Runtime;
using ConfigurationLibrary;
using Rewired;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityScript.Lang;

[Serializable]
public class LevelCompleteScreenScript : MonoBehaviour
{
    private bool asyncLoadDone = false;
    public AsyncOperation asyncLoad;

    IEnumerator LoadMPMapAsync()
    {
        asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // Check if the load has finished
            if (asyncLoad.progress >= 0.9f && !asyncLoadDone)
            {
                asyncLoadDone = true;
                PacketSender.SendTransitionReadyMessage();
                MFPEditorUtils.Log("Next scene is ready.");
            }

            yield return null;
        }
    }

    private RootScript root;
    private RootSharedScript rootShared;
    private StatsTrackerScript statsTracker;
    private RectTransform theRect;
    private Vector2 theRectStartPos;
    private Vector2 theRectStartScale;
    private Image yellowTint;
    private Color yellowTintStartColour;
    private RectTransform levelCompleteText;
    private Vector2 levelCompleteTextStartPos;
    private float levelCompleteTextXSpeed;
    private RectTransform pedro;
    private Vector2 pedroStartPos;
    private float pedroXSpeed;
    private RectTransform pedroFace;
    private Image pedroFaceImage;
    private Text gameScoreText;
    private Text gameScoreTextScore;
    private Text timeBonusText;
    private Text timeBonusTextScore;
    private Text killsText;
    private Text killsTextScore;
    private Text noDeathBonusText;
    private Text noDeathBonusTextScore;
    private Text difficultyBonusText;
    private Text difficultyBonusTextScore;
    private Text finalRatingText;
    private Text finalRatingTextScore;
    private Image finalRatingBackground;
    private Text finalRatingLetterText;
    private Text finalRatingForText;
    private Image finalRatingBar1;
    private Image finalRatingBar1Bar;
    private Image finalRatingBar2;
    private Image finalRatingBar2Bar;
    private Image finalRatingBar3;
    private Image finalRatingBar3Bar;
    private RectTransform nextLevelButton;
    private RectTransform restartLevelButton;
    private RectTransform exitButton;
    private RectTransform weaponPanel;
    private RectTransform healthAndSlowMoPanel;
    private RectTransform scoreHud;
    private RectTransform bigFace;
    private RectTransform bigText;
    private RectTransform pedroHint;
    private RectTransform reactionPedro;
    private RectTransform gameHighlight;
    private Vector2 gameHighlightStartPos;
    private GameObject gameHighlightTweetPrompt;
    private GameObject gameHighlightPinPrompt;
    private RawImage gameHighlightImage;
    private RectTransform mainCursor;
    private AudioSource theAudioSource;
    public AudioClip appearSound;
    public AudioClip countSound;
    public AudioClip ratingSound;
    private float prevTotalScore;
    private float countedKills;
    private float prevCountedKills;
    private float timer;
    private bool enabledDoOnce;
    private bool ratingTextDoOnce;
    private RectTransform previousScoreDisplay;
    private Vector2 previousScoreDisplayStartPos;
    private Text personalBestScoreHeader;
    private Text personalBestScore;
    private Text leaderboardBestScoreHeader;
    private Text leaderboardBestScore;
    private bool newRecord;
    private int optionSelected;
    private bool optionNavDoOnce;
    private UnityEngine.UI.Button nextLevelUIButton;
    private UnityEngine.UI.Button restartLevelUIButton;
    private UnityEngine.UI.Button exitUIButton;
    private UnityEngine.UI.Button gifSaveButton;
    private UIButtonScript nextLevelButtonScript;
    private UIButtonScript restartLevelButtonScript;
    private UIButtonScript exitButtonScript;
    private UIButtonScript gifSaveButtonScript;
    private UIButtonScript tweeetButtonScript;
    private GameObject gifSaveButtonPrompt;
    private bool hasExportedGifFromGamepad;
    private bool tweetUIStuffDoOnce;
    private GameObject uiConfirmHint;
    private CanvasGroup uiConfirmHintCanvasGroup;
    private bool useGamepadIcons;
    private GameObject gameModifiersNotice;
    private Player player;

    public virtual void Awake()
    {
        this.player = ReInput.players.GetPlayer(0);
    }

    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.rootShared = (RootSharedScript)GameObject.Find("RootShared").GetComponent(typeof(RootSharedScript));
        this.statsTracker = (StatsTrackerScript)GameObject.Find("RootShared").GetComponent(typeof(StatsTrackerScript));
        this.theRect = (RectTransform)this.GetComponent(typeof(RectTransform));
        this.theRectStartPos = this.theRect.anchoredPosition;
        this.theRectStartScale = this.theRect.sizeDelta;
        this.yellowTint = (Image)this.transform.parent.GetComponent(typeof(Image));
        this.yellowTintStartColour = this.yellowTint.color;
        this.levelCompleteText = (RectTransform)this.transform.Find("LevelCompleteText").GetComponent(typeof(RectTransform));
        this.levelCompleteTextStartPos = this.levelCompleteText.anchoredPosition;
        this.pedro = (RectTransform)this.transform.Find("Pedro").GetComponent(typeof(RectTransform));
        this.pedroStartPos = this.pedro.anchoredPosition;
        this.pedroFace = (RectTransform)this.pedro.Find("Face").GetComponent(typeof(RectTransform));
        this.pedroFaceImage = (Image)this.pedroFace.GetComponent(typeof(Image));
        this.gameScoreText = (Text)this.transform.Find("GameScoreText").GetComponent(typeof(Text));
        this.gameScoreTextScore = (Text)this.gameScoreText.transform.Find("ScoreText").GetComponent(typeof(Text));
        this.timeBonusText = (Text)this.transform.Find("TimeBonusText").GetComponent(typeof(Text));
        this.timeBonusTextScore = (Text)this.timeBonusText.transform.Find("ScoreText").GetComponent(typeof(Text));
        this.killsText = (Text)this.transform.Find("KillsText").GetComponent(typeof(Text));
        this.killsTextScore = (Text)this.killsText.transform.Find("ScoreText").GetComponent(typeof(Text));
        this.noDeathBonusText = (Text)this.transform.Find("NoDeathBonusText").GetComponent(typeof(Text));
        this.noDeathBonusTextScore = (Text)this.noDeathBonusText.transform.Find("ScoreText").GetComponent(typeof(Text));
        this.difficultyBonusText = (Text)this.transform.Find("DifficultyBonusText").GetComponent(typeof(Text));
        this.difficultyBonusTextScore = (Text)this.difficultyBonusText.transform.Find("ScoreText").GetComponent(typeof(Text));
        this.finalRatingText = (Text)this.transform.Find("FinalRatingText").GetComponent(typeof(Text));
        this.finalRatingTextScore = (Text)this.finalRatingText.transform.Find("ScoreText").GetComponent(typeof(Text));
        this.finalRatingBackground = (Image)this.transform.Find("FinalRatingBackground").GetComponent(typeof(Image));
        this.finalRatingLetterText = (Text)this.transform.Find("FinalRatingLetterText").GetComponent(typeof(Text));
        this.finalRatingForText = (Text)this.transform.Find("FinalRatingForText").GetComponent(typeof(Text));
        this.finalRatingBar1 = (Image)this.transform.Find("FinalRatingBar1").GetComponent(typeof(Image));
        this.finalRatingBar1Bar = (Image)this.finalRatingBar1.transform.Find("Bar").GetComponent(typeof(Image));
        this.finalRatingBar2 = (Image)this.transform.Find("FinalRatingBar2").GetComponent(typeof(Image));
        this.finalRatingBar2Bar = (Image)this.finalRatingBar2.transform.Find("Bar").GetComponent(typeof(Image));
        this.finalRatingBar3 = (Image)this.transform.Find("FinalRatingBar3").GetComponent(typeof(Image));
        this.finalRatingBar3Bar = (Image)this.finalRatingBar3.transform.Find("Bar").GetComponent(typeof(Image));
        this.nextLevelButton = (RectTransform)this.transform.Find("NextLevelButton").GetComponent(typeof(RectTransform));
        this.nextLevelUIButton = (UnityEngine.UI.Button)this.nextLevelButton.GetComponent(typeof(UnityEngine.UI.Button));
        this.nextLevelButtonScript = (UIButtonScript)this.nextLevelButton.GetComponent(typeof(UIButtonScript));
        this.restartLevelButton = (RectTransform)this.transform.Find("RestartLevelButton").GetComponent(typeof(RectTransform));
        this.restartLevelUIButton = (UnityEngine.UI.Button)this.restartLevelButton.GetComponent(typeof(UnityEngine.UI.Button));
        this.restartLevelButtonScript = (UIButtonScript)this.restartLevelButton.GetComponent(typeof(UIButtonScript));
        this.exitButton = (RectTransform)this.transform.Find("ExitButton").GetComponent(typeof(RectTransform));
        this.exitUIButton = (UnityEngine.UI.Button)this.exitButton.GetComponent(typeof(UnityEngine.UI.Button));
        this.exitButtonScript = (UIButtonScript)this.exitButton.GetComponent(typeof(UIButtonScript));
        this.weaponPanel = (RectTransform)this.transform.parent.parent.Find("WeaponPanel").GetComponent(typeof(RectTransform));
        this.healthAndSlowMoPanel = (RectTransform)this.transform.parent.parent.Find("HealthAndSlowMo").GetComponent(typeof(RectTransform));
        this.scoreHud = (RectTransform)this.transform.parent.parent.Find("ScoreHud").GetComponent(typeof(RectTransform));
        this.bigFace = (RectTransform)this.transform.parent.parent.Find("BigScreenReaction/BigFace").GetComponent(typeof(RectTransform));
        this.bigText = (RectTransform)this.transform.parent.parent.Find("BigScreenReaction/BigText").GetComponent(typeof(RectTransform));
        this.pedroHint = (RectTransform)this.transform.parent.parent.Find("PedroHint").GetComponent(typeof(RectTransform));
        this.reactionPedro = (RectTransform)this.transform.parent.parent.Find("ReactionPedro").GetComponent(typeof(RectTransform));
        this.gameHighlight = (RectTransform)this.transform.Find("GameHighlight").GetComponent(typeof(RectTransform));
        this.gameHighlightStartPos = this.gameHighlight.anchoredPosition;
        this.gameHighlightTweetPrompt = this.gameHighlight.transform.Find("TweetPrompt").gameObject;
        this.gameHighlightPinPrompt = this.gameHighlight.transform.Find("PinPrompt").gameObject;
        this.gameHighlightImage = (RawImage)this.gameHighlight.GetComponent(typeof(RawImage));
        this.mainCursor = (RectTransform)GameObject.Find("HUD/Canvas/Cursors/MainCursor").GetComponent(typeof(RectTransform));
        this.previousScoreDisplay = (RectTransform)this.transform.Find("PreviousScoreDisplay").GetComponent(typeof(RectTransform));
        this.previousScoreDisplayStartPos = this.previousScoreDisplay.anchoredPosition;
        float num1 = this.previousScoreDisplay.anchoredPosition.x - this.previousScoreDisplay.sizeDelta.x * 1.5f;
        Vector2 anchoredPosition1 = this.previousScoreDisplay.anchoredPosition;
        double num2 = (double)(anchoredPosition1.x = num1);
        Vector2 vector2_1 = this.previousScoreDisplay.anchoredPosition = anchoredPosition1;
        this.personalBestScoreHeader = (Text)this.previousScoreDisplay.Find("PersonalBest").GetComponent(typeof(Text));
        this.personalBestScore = (Text)this.personalBestScoreHeader.transform.Find("Score").GetComponent(typeof(Text));
        this.leaderboardBestScoreHeader = (Text)this.previousScoreDisplay.Find("LeaderboardBest").GetComponent(typeof(Text));
        this.leaderboardBestScore = (Text)this.leaderboardBestScoreHeader.transform.Find("Score").GetComponent(typeof(Text));
        this.personalBestScore.text = "-";
        int num3 = 0;
        Color color1 = this.leaderboardBestScore.color;
        double num4 = (double)(color1.a = (float)num3);
        Color color2 = this.leaderboardBestScore.color = color1;
        int num5 = num3;
        Color color3 = this.leaderboardBestScoreHeader.color;
        double num6 = (double)(color3.a = (float)num5);
        Color color4 = this.leaderboardBestScoreHeader.color = color3;
        this.leaderboardBestScore.text = "-";
        this.theAudioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
        this.theAudioSource.clip = this.appearSound;
        this.theAudioSource.volume = 0.6f;
        this.theAudioSource.pitch = 1f;
        this.theAudioSource.Play();
        this.gameModifiersNotice = this.transform.parent.Find("ModifiersDisclaimer").gameObject;
        int num7 = 0;
        Color color5 = this.gameScoreTextScore.color;
        double num8 = (double)(color5.a = (float)num7);
        Color color6 = this.gameScoreTextScore.color = color5;
        int num9 = num7;
        Color color7 = this.gameScoreText.color;
        double num10 = (double)(color7.a = (float)num9);
        Color color8 = this.gameScoreText.color = color7;
        int num11 = 0;
        Color color9 = this.timeBonusTextScore.color;
        double num12 = (double)(color9.a = (float)num11);
        Color color10 = this.timeBonusTextScore.color = color9;
        int num13 = num11;
        Color color11 = this.timeBonusText.color;
        double num14 = (double)(color11.a = (float)num13);
        Color color12 = this.timeBonusText.color = color11;
        int num15 = 0;
        Color color13 = this.killsTextScore.color;
        double num16 = (double)(color13.a = (float)num15);
        Color color14 = this.killsTextScore.color = color13;
        int num17 = num15;
        Color color15 = this.killsText.color;
        double num18 = (double)(color15.a = (float)num17);
        Color color16 = this.killsText.color = color15;
        int num19 = 0;
        Color color17 = this.noDeathBonusTextScore.color;
        double num20 = (double)(color17.a = (float)num19);
        Color color18 = this.noDeathBonusTextScore.color = color17;
        int num21 = num19;
        Color color19 = this.noDeathBonusText.color;
        double num22 = (double)(color19.a = (float)num21);
        Color color20 = this.noDeathBonusText.color = color19;
        int num23 = 0;
        Color color21 = this.difficultyBonusTextScore.color;
        double num24 = (double)(color21.a = (float)num23);
        Color color22 = this.difficultyBonusTextScore.color = color21;
        int num25 = num23;
        Color color23 = this.difficultyBonusText.color;
        double num26 = (double)(color23.a = (float)num25);
        Color color24 = this.difficultyBonusText.color = color23;
        int num27 = 0;
        Color color25 = this.finalRatingTextScore.color;
        double num28 = (double)(color25.a = (float)num27);
        Color color26 = this.finalRatingTextScore.color = color25;
        int num29 = num27;
        Color color27 = this.finalRatingText.color;
        double num30 = (double)(color27.a = (float)num29);
        Color color28 = this.finalRatingText.color = color27;
        int num31 = 0;
        Color color29 = this.finalRatingBackground.color;
        double num32 = (double)(color29.a = (float)num31);
        Color color30 = this.finalRatingBackground.color = color29;
        int num33 = 0;
        Color color31 = this.finalRatingLetterText.color;
        double num34 = (double)(color31.a = (float)num33);
        Color color32 = this.finalRatingLetterText.color = color31;
        int num35 = 0;
        Color color33 = this.finalRatingForText.color;
        double num36 = (double)(color33.a = (float)num35);
        Color color34 = this.finalRatingForText.color = color33;
        int num37 = 0;
        Color color35 = this.finalRatingBar1Bar.color;
        double num38 = (double)(color35.a = (float)num37);
        Color color36 = this.finalRatingBar1Bar.color = color35;
        int num39 = num37;
        Color color37 = this.finalRatingBar1.color;
        double num40 = (double)(color37.a = (float)num39);
        Color color38 = this.finalRatingBar1.color = color37;
        int num41 = 0;
        Color color39 = this.finalRatingBar2Bar.color;
        double num42 = (double)(color39.a = (float)num41);
        Color color40 = this.finalRatingBar2Bar.color = color39;
        int num43 = num41;
        Color color41 = this.finalRatingBar2.color;
        double num44 = (double)(color41.a = (float)num43);
        Color color42 = this.finalRatingBar2.color = color41;
        int num45 = 0;
        Color color43 = this.finalRatingBar3Bar.color;
        double num46 = (double)(color43.a = (float)num45);
        Color color44 = this.finalRatingBar3Bar.color = color43;
        int num47 = num45;
        Color color45 = this.finalRatingBar3.color;
        double num48 = (double)(color45.a = (float)num47);
        Color color46 = this.finalRatingBar3.color = color45;
        this.nextLevelButton.gameObject.SetActive(false);
        this.restartLevelButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);
        this.gameHighlight.gameObject.SetActive(false);
        int num49 = -Screen.width;
        Vector2 anchoredPosition2 = this.theRect.anchoredPosition;
        double num50 = (double)(anchoredPosition2.x = (float)num49);
        Vector2 vector2_2 = this.theRect.anchoredPosition = anchoredPosition2;
        int num51 = 0;
        Vector2 sizeDelta = this.theRect.sizeDelta;
        double num52 = (double)(sizeDelta.y = (float)num51);
        Vector2 vector2_3 = this.theRect.sizeDelta = sizeDelta;
        int num53 = -10;
        Vector2 anchoredPosition3 = this.levelCompleteText.anchoredPosition;
        double num54 = (double)(anchoredPosition3.y = (float)num53);
        Vector2 vector2_4 = this.levelCompleteText.anchoredPosition = anchoredPosition3;
        this.levelCompleteTextXSpeed = 10f;
        this.yellowTint.color = new Color(0.95f, 0.95f, 0.85f, 0.6f);
        float num55 = this.pedro.anchoredPosition.x - 400f;
        Vector2 anchoredPosition4 = this.pedro.anchoredPosition;
        double num56 = (double)(anchoredPosition4.x = num55);
        Vector2 vector2_5 = this.pedro.anchoredPosition = anchoredPosition4;
        int num57 = 0;
        Vector3 localScale1 = this.finalRatingBar1Bar.transform.localScale;
        double num58 = (double)(localScale1.x = (float)num57);
        Vector3 vector3_1 = this.finalRatingBar1Bar.transform.localScale = localScale1;
        int num59 = 0;
        Vector3 localScale2 = this.finalRatingBar2Bar.transform.localScale;
        double num60 = (double)(localScale2.x = (float)num59);
        Vector3 vector3_2 = this.finalRatingBar2Bar.transform.localScale = localScale2;
        int num61 = 0;
        Vector3 localScale3 = this.finalRatingBar3Bar.transform.localScale;
        double num62 = (double)(localScale3.x = (float)num61);
        Vector3 vector3_3 = this.finalRatingBar3Bar.transform.localScale = localScale3;
        if (this.root.isAlarmLevel)
            this.killsText.text = this.root.GetTranslation("esNoAlarm");
        this.useGamepadIcons = this.root.useGamepadIcons;
        this.createNavigationHints();
        this.uiConfirmHintCanvasGroup.alpha = 0.0f;
        this.gifSaveButton = (UnityEngine.UI.Button)this.gameHighlight.Find("SaveGifButton").GetComponent(typeof(UnityEngine.UI.Button));
        if ((UnityEngine.Object)this.gifSaveButton != (UnityEngine.Object)null)
            this.gifSaveButtonScript = (UIButtonScript)this.gifSaveButton.GetComponent(typeof(UIButtonScript));
        this.tweeetButtonScript = (UIButtonScript)this.gameHighlight.Find("TweetGifButton").GetComponent(typeof(UIButtonScript));
        this.gifSaveButtonPrompt = ((InputHelperScript)GameObject.Find("Rewired Input Manager").GetComponent(typeof(InputHelperScript))).GetInputSymbol("UISPECIAL1", false);
        RectTransform component1 = (RectTransform)this.gifSaveButtonPrompt.GetComponent(typeof(RectTransform));
        component1.SetParent(this.gameHighlight.Find("SaveGifButton/ButtonGraphic"), false);
        component1.anchoredPosition = new Vector2(35.5f, 9.5f);
        component1.localScale = Vector3.one * 0.7f;
        if (this.rootShared.isDemo)
        {
            ((Selectable)this.gameHighlight.Find("TweetGifButton").GetComponent(typeof(UnityEngine.UI.Button))).interactable = false;
            ((Selectable)this.restartLevelButton.GetComponent(typeof(UnityEngine.UI.Button))).interactable = false;
            ((Selectable)this.exitButton.GetComponent(typeof(UnityEngine.UI.Button))).interactable = false;
        }
        if (this.rootShared.runningOnConsole)
        {
            this.gameHighlight.Find("SaveGifButton").gameObject.SetActive(false);
            this.gameHighlight.Find("TweetGifButton").gameObject.SetActive(false);
        }
        if (this.rootShared.chineseBuild)
        {
            RectTransform component2 = (RectTransform)this.gameHighlight.Find("TweetGifButton").GetComponent(typeof(RectTransform));
            component2.anchoredPosition = Vector2.one * -9999f;
            component2.localScale = Vector3.one * (1f / 1000f);
        }
        this.gameHighlight.Find("NvidiaHighlightsButton").gameObject.SetActive((UnityEngine.Object)GameObject.Find("NvidiaHighlights") != (UnityEngine.Object)null);
        if (!this.useGamepadIcons)
            this.gifSaveButtonPrompt.SetActive(false);
        this.rootShared.DoEndOfLevelShowTopLeaderboardScore(this.rootShared.GetLeaderboardName(SceneManager.GetActiveScene().buildIndex, !this.root.GetCCheck() ? string.Empty : "-999", false));

        if (!MultiplayerManagerTest.singleplayerMode)
        {
            restartLevelUIButton.interactable = false;
            nextLevelUIButton.interactable = false;

            MFPEditorUtils.doPedroHint("Next level will load in 5 seconds");

            MultiplayerManagerTest.clearDoOnce = true;

            StartCoroutine(LoadMPMapAsync());

        }
    }

    public void MultiplayerGoNextLevel() //executes for all clients once everyone is done loading
    {
        MultiplayerManagerTest.transitioningToNextLevel = true;
        Time.timeScale = 1;
        root.timescale = 1;
        asyncLoad.allowSceneActivation = true;
    }

    public virtual void Update()
    {

        if (MultiplayerManagerTest.playingAsHost)
        {
            if (Steamworks.SteamMatchmaking.GetNumLobbyMembers(MultiplayerManagerTest.inst.globalID) == MultiplayerManagerTest.inst.levelTransitionReady.Count)
                PacketSender.TransitionClientsToNextLevel();
        }

        float p = Mathf.Clamp(Time.unscaledDeltaTime * 60f, 0.0f, 3f);
        int num1 = !this.root.isAlarmLevel ? 1 : 0;
        if (num1 == 0)
        {
            int num2 = this.root.isAlarmLevel ? 1 : 0;
            num1 = num2 == 0 ? num2 : (!this.root.hasTriggeredAlarm ? 1 : 0);
        }
        bool flag1 = num1 != 0;
        bool flag2 = this.root.nrOfDeaths <= 0;
        bool flag3 = this.root.difficultyMode > 0;
        float num3 = 360f;
        if (flag1)
            num3 += 130f;
        if (flag2)
            num3 += 130f;
        if (flag3)
            num3 += 130f;
        if ((double)this.timer > 100.0 && (double)this.timer < (double)num3 + 5.0)
        {
            this.timer += 2f * p;
            if ((double)this.timer < (double)num3 && (this.player.GetButtonDown("Fire") || this.player.GetButtonDown("UISubmit")))
                this.timer = num3;
        }
        else
            this.timer += p;
        if (!this.enabledDoOnce)
        {
            this.weaponPanel.gameObject.SetActive(false);
            this.healthAndSlowMoPanel.gameObject.SetActive(false);
            this.scoreHud.gameObject.SetActive(false);
            this.bigFace.gameObject.SetActive(false);
            this.bigText.gameObject.SetActive(false);
            this.pedroHint.gameObject.SetActive(false);
            this.reactionPedro.gameObject.SetActive(false);
            this.timeBonusTextScore.text = this.root.convertToTimeFormat(0.0f);
            if (!this.root.isAlarmLevel)
                this.killsTextScore.text = RuntimeServices.op_Addition("0/", (object)this.root.nrOfEnemiesTotal);
            if ((double)this.root.maxScoreReference == 0.0)
                this.root.maxScoreReference = (float)(this.root.nrOfEnemiesTotal * 320 * this.root.potentialMultipliersFromEnemies + 115000);
            this.root.maxScoreReference += (float)(25000 * this.root.difficultyMode);
            this.mainCursor.gameObject.SetActive(false);
            if (!this.rootShared.neverChangeMouseCursor)
                Cursor.SetCursor(UnityEngine.Resources.Load("HUD/menu_cursor") as Texture2D, new Vector2(3f, 3f), CursorMode.Auto);
            this.root.SetCursorState();
            this.enabledDoOnce = true;
        }
        float num4 = this.root.DampUnscaled(this.theRectStartPos.x, this.theRect.anchoredPosition.x, 0.3f);
        Vector2 anchoredPosition1 = this.theRect.anchoredPosition;
        double num5 = (double)(anchoredPosition1.x = num4);
        Vector2 vector2_1 = this.theRect.anchoredPosition = anchoredPosition1;
        this.yellowTint.color = this.yellowTint.color + (this.yellowTintStartColour - this.yellowTint.color) * Mathf.Clamp01(0.2f * p);
        if (!this.root.dontShowPedroAtEndScreen)
        {
            float num2 = this.root.DampUnscaled(this.pedroStartPos.x, this.pedro.anchoredPosition.x, 0.1f);
            Vector2 anchoredPosition2 = this.pedro.anchoredPosition;
            double num6 = (double)(anchoredPosition2.x = num2);
            Vector2 vector2_2 = this.pedro.anchoredPosition = anchoredPosition2;
            this.pedroXSpeed += this.root.DampAddUnscaled(this.pedroStartPos.x, this.pedro.anchoredPosition.x, 0.06f);
            this.pedroXSpeed *= Mathf.Pow(0.85f, p);
            float num7 = this.pedro.anchoredPosition.x + this.pedroXSpeed * p;
            Vector2 anchoredPosition3 = this.pedro.anchoredPosition;
            double num8 = (double)(anchoredPosition3.x = num7);
            Vector2 vector2_3 = this.pedro.anchoredPosition = anchoredPosition3;
            float num9 = Mathf.Sin(Time.unscaledTime) * 5f;
            Vector2 anchoredPosition4 = this.pedro.anchoredPosition;
            double num10 = (double)(anchoredPosition4.y = num9);
            Vector2 vector2_4 = this.pedro.anchoredPosition = anchoredPosition4;
            float num11 = (float)(5.0 + (double)Mathf.Sin(Time.unscaledTime + 10f) * 3.0 + (double)this.pedroXSpeed * 0.25);
            Quaternion rotation = this.pedro.rotation;
            Vector3 eulerAngles = rotation.eulerAngles;
            double num12 = (double)(eulerAngles.z = num11);
            Vector3 vector3 = rotation.eulerAngles = eulerAngles;
            Quaternion quaternion = this.pedro.rotation = rotation;
        }
        this.levelCompleteTextXSpeed -= 0.5f * p;
        float num13 = this.levelCompleteText.anchoredPosition.x + this.levelCompleteTextXSpeed * p;
        Vector2 anchoredPosition5 = this.levelCompleteText.anchoredPosition;
        double num14 = (double)(anchoredPosition5.x = num13);
        Vector2 vector2_5 = this.levelCompleteText.anchoredPosition = anchoredPosition5;
        if ((double)this.levelCompleteText.anchoredPosition.x < (double)this.levelCompleteTextStartPos.x)
        {
            float x = this.levelCompleteTextStartPos.x;
            Vector2 anchoredPosition2 = this.levelCompleteText.anchoredPosition;
            double num2 = (double)(anchoredPosition2.x = x);
            Vector2 vector2_2 = this.levelCompleteText.anchoredPosition = anchoredPosition2;
            this.levelCompleteTextXSpeed *= -0.5f;
        }
        if ((double)this.timer < 80.0)
        {
            float num2 = this.root.DampUnscaled(30f, this.theRect.sizeDelta.y, 0.3f);
            Vector2 sizeDelta = this.theRect.sizeDelta;
            double num6 = (double)(sizeDelta.y = num2);
            Vector2 vector2_2 = this.theRect.sizeDelta = sizeDelta;
        }
        else if ((double)this.timer < 300.0)
        {
            float num2 = this.root.DampUnscaled(165f, this.theRect.sizeDelta.y, 0.3f);
            Vector2 sizeDelta = this.theRect.sizeDelta;
            double num6 = (double)(sizeDelta.y = num2);
            Vector2 vector2_2 = this.theRect.sizeDelta = sizeDelta;
            float num7 = this.root.DampUnscaled(this.levelCompleteTextStartPos.y, this.levelCompleteText.anchoredPosition.y, 0.3f);
            Vector2 anchoredPosition2 = this.levelCompleteText.anchoredPosition;
            double num8 = (double)(anchoredPosition2.y = num7);
            Vector2 vector2_3 = this.levelCompleteText.anchoredPosition = anchoredPosition2;
            float num9 = this.timer - 85f;
            float num10 = Mathf.Clamp(num9, 0.0f, 10f) / 20f;
            Color color1 = this.gameScoreTextScore.color;
            double num11 = (double)(color1.a = num10);
            Color color2 = this.gameScoreTextScore.color = color1;
            float num12 = num10;
            Color color3 = this.gameScoreText.color;
            double num15 = (double)(color3.a = num12);
            Color color4 = this.gameScoreText.color = color3;
            float num16 = Mathf.Clamp(num9 - 5f, 0.0f, 10f) / 20f;
            Color color5 = this.timeBonusTextScore.color;
            double num17 = (double)(color5.a = num16);
            Color color6 = this.timeBonusTextScore.color = color5;
            float num18 = num16;
            Color color7 = this.timeBonusText.color;
            double num19 = (double)(color7.a = num18);
            Color color8 = this.timeBonusText.color = color7;
            float num20 = Mathf.Clamp(num9 - 10f, 0.0f, 10f) / 20f;
            Color color9 = this.killsTextScore.color;
            double num21 = (double)(color9.a = num20);
            Color color10 = this.killsTextScore.color = color9;
            float num22 = num20;
            Color color11 = this.killsText.color;
            double num23 = (double)(color11.a = num22);
            Color color12 = this.killsText.color = color11;
            float num24 = Mathf.Clamp(num9 - 15f, 0.0f, 10f) / 20f;
            Color color13 = this.noDeathBonusTextScore.color;
            double num25 = (double)(color13.a = num24);
            Color color14 = this.noDeathBonusTextScore.color = color13;
            float num26 = num24;
            Color color15 = this.noDeathBonusText.color;
            double num27 = (double)(color15.a = num26);
            Color color16 = this.noDeathBonusText.color = color15;
            float num28 = Mathf.Clamp(num9 - 20f, 0.0f, 10f) / 20f;
            Color color17 = this.difficultyBonusTextScore.color;
            double num29 = (double)(color17.a = num28);
            Color color18 = this.difficultyBonusTextScore.color = color17;
            float num30 = num28;
            Color color19 = this.difficultyBonusText.color;
            double num31 = (double)(color19.a = num30);
            Color color20 = this.difficultyBonusText.color = color19;
            float num32 = Mathf.Clamp(num9 - 20f, 0.0f, 10f) / 10f;
            Color color21 = this.finalRatingTextScore.color;
            double num33 = (double)(color21.a = num32);
            Color color22 = this.finalRatingTextScore.color = color21;
            float num34 = num32;
            Color color23 = this.finalRatingText.color;
            double num35 = (double)(color23.a = num34);
            Color color24 = this.finalRatingText.color = color23;
            float num36 = Mathf.Clamp(num9 - 25f, 0.0f, 10f) / 10f;
            Color color25 = this.finalRatingBar1Bar.color;
            double num37 = (double)(color25.a = num36);
            Color color26 = this.finalRatingBar1Bar.color = color25;
            float num38 = num36;
            Color color27 = this.finalRatingBar1.color;
            double num39 = (double)(color27.a = num38);
            Color color28 = this.finalRatingBar1.color = color27;
            float num40 = Mathf.Clamp(num9 - 30f, 0.0f, 10f) / 10f;
            Color color29 = this.finalRatingBar2Bar.color;
            double num41 = (double)(color29.a = num40);
            Color color30 = this.finalRatingBar2Bar.color = color29;
            float num42 = num40;
            Color color31 = this.finalRatingBar2.color;
            double num43 = (double)(color31.a = num42);
            Color color32 = this.finalRatingBar2.color = color31;
            float num44 = Mathf.Clamp(num9 - 35f, 0.0f, 10f) / 10f;
            Color color33 = this.finalRatingBar3Bar.color;
            double num45 = (double)(color33.a = num44);
            Color color34 = this.finalRatingBar3Bar.color = color33;
            float num46 = num44;
            Color color35 = this.finalRatingBar3.color;
            double num47 = (double)(color35.a = num46);
            Color color36 = this.finalRatingBar3.color = color35;
        }
        else
        {
            int num2 = 1;
            Color color1 = this.finalRatingTextScore.color;
            double num6 = (double)(color1.a = (float)num2);
            Color color2 = this.finalRatingTextScore.color = color1;
            int num7 = num2;
            Color color3 = this.finalRatingText.color;
            double num8 = (double)(color3.a = (float)num7);
            Color color4 = this.finalRatingText.color = color3;
            int num9 = 1;
            Color color5 = this.finalRatingBar1Bar.color;
            double num10 = (double)(color5.a = (float)num9);
            Color color6 = this.finalRatingBar1Bar.color = color5;
            int num11 = num9;
            Color color7 = this.finalRatingBar1.color;
            double num12 = (double)(color7.a = (float)num11);
            Color color8 = this.finalRatingBar1.color = color7;
            int num15 = 1;
            Color color9 = this.finalRatingBar2Bar.color;
            double num16 = (double)(color9.a = (float)num15);
            Color color10 = this.finalRatingBar2Bar.color = color9;
            int num17 = num15;
            Color color11 = this.finalRatingBar2.color;
            double num18 = (double)(color11.a = (float)num17);
            Color color12 = this.finalRatingBar2.color = color11;
            int num19 = 1;
            Color color13 = this.finalRatingBar3Bar.color;
            double num20 = (double)(color13.a = (float)num19);
            Color color14 = this.finalRatingBar3Bar.color = color13;
            int num21 = num19;
            Color color15 = this.finalRatingBar3.color;
            double num22 = (double)(color15.a = (float)num21);
            Color color16 = this.finalRatingBar3.color = color15;
        }
        float nr1 = 0.0f;
        float num48 = this.timer - 100f;
        if ((double)num48 > 0.0)
        {
            int num2 = 1;
            Color color1 = this.gameScoreTextScore.color;
            double num6 = (double)(color1.a = (float)num2);
            Color color2 = this.gameScoreTextScore.color = color1;
            int num7 = num2;
            Color color3 = this.gameScoreText.color;
            double num8 = (double)(color3.a = (float)num7);
            Color color4 = this.gameScoreText.color = color3;
            float nr2 = Mathf.Round(Mathf.Clamp01(num48 / 120f) * this.root.score);
            nr1 += nr2;
            this.gameScoreTextScore.text = this.root.addCommasToNumber(nr2);
        }
        if ((double)num48 > 130.0)
        {
            num48 -= 130f;
            int num2 = 1;
            Color color1 = this.timeBonusTextScore.color;
            double num6 = (double)(color1.a = (float)num2);
            Color color2 = this.timeBonusTextScore.color = color1;
            int num7 = num2;
            Color color3 = this.timeBonusText.color;
            double num8 = (double)(color3.a = (float)num7);
            Color color4 = this.timeBonusText.color = color3;
            float nr2 = Mathf.Round(Mathf.Clamp01(num48 / 120f) * (900f - Mathf.Clamp(this.root.finishTime - this.root.startTime, 0.0f, 900f)) * 50f);
            nr1 += nr2;
            this.timeBonusTextScore.text = this.root.convertToTimeFormat(Mathf.Clamp01(num48 / 120f) * (this.root.finishTime - this.root.startTime));
            if ((double)num48 > 120.0)
                this.timeBonusTextScore.text = RuntimeServices.op_Addition(this.timeBonusTextScore.text, RuntimeServices.op_Addition("  =  ", this.root.addCommasToNumber(nr2)));
        }
        if (flag1 && (double)num48 > 130.0)
        {
            num48 -= 130f;
            int num2 = 1;
            Color color1 = this.killsTextScore.color;
            double num6 = (double)(color1.a = (float)num2);
            Color color2 = this.killsTextScore.color = color1;
            int num7 = num2;
            Color color3 = this.killsText.color;
            double num8 = (double)(color3.a = (float)num7);
            Color color4 = this.killsText.color = color3;
            if (!this.root.isAlarmLevel)
            {
                this.countedKills = Mathf.Round(Mathf.Clamp01(num48 / 120f) * (float)this.root.nrOfEnemiesKilled);
                this.killsTextScore.text = RuntimeServices.op_Addition(RuntimeServices.op_Addition((object)this.countedKills, "/"), (object)this.root.nrOfEnemiesTotal);
                if ((double)num48 > 120.0)
                {
                    if (this.root.nrOfEnemiesKilled >= this.root.nrOfEnemiesTotal)
                    {
                        this.killsTextScore.text = RuntimeServices.op_Addition(this.killsTextScore.text, RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("  =  ", this.root.GetTranslation("esBonus")), " "), this.root.addCommasToNumber(10000f)));
                        nr1 += 10000f;
                    }
                    else
                        this.killsTextScore.text = RuntimeServices.op_Addition(this.killsTextScore.text, RuntimeServices.op_Addition("  =  ", this.root.GetTranslation("esNoBonus")));
                }
            }
            else
            {
                float nr2 = Mathf.Round(Mathf.Clamp01(num48 / 120f) * 200000f);
                nr1 += nr2;
                this.killsTextScore.text = this.root.addCommasToNumber(nr2);
            }
        }
        if (flag2 && (double)num48 > 130.0)
        {
            num48 -= 130f;
            int num2 = 1;
            Color color1 = this.noDeathBonusTextScore.color;
            double num6 = (double)(color1.a = (float)num2);
            Color color2 = this.noDeathBonusTextScore.color = color1;
            int num7 = num2;
            Color color3 = this.noDeathBonusText.color;
            double num8 = (double)(color3.a = (float)num7);
            Color color4 = this.noDeathBonusText.color = color3;
            float nr2 = Mathf.Round(Mathf.Clamp01(num48 / 120f) * 20000f);
            nr1 += nr2;
            this.noDeathBonusTextScore.text = this.root.addCommasToNumber(nr2);
        }
        if (flag3 && (double)num48 > 130.0)
        {
            num48 -= 130f;
            int num2 = 1;
            Color color1 = this.difficultyBonusTextScore.color;
            double num6 = (double)(color1.a = (float)num2);
            Color color2 = this.difficultyBonusTextScore.color = color1;
            int num7 = num2;
            Color color3 = this.difficultyBonusText.color;
            double num8 = (double)(color3.a = (float)num7);
            Color color4 = this.difficultyBonusText.color = color3;
            float nr2 = Mathf.Round(Mathf.Clamp01(num48 / 120f) * (float)(25000 * this.root.difficultyMode));
            nr1 += nr2;
            this.difficultyBonusTextScore.text = this.root.addCommasToNumber(nr2);
        }
        this.finalRatingTextScore.text = this.root.addCommasToNumber(nr1);
        if ((double)this.prevTotalScore != (double)nr1)
        {
            this.doCountingSound(nr1 / this.root.maxScoreReference);
            this.prevTotalScore = nr1;
        }
        if ((double)this.prevCountedKills != (double)this.countedKills)
        {
            this.doCountingSound((float)((double)nr1 / (double)this.root.maxScoreReference + (double)this.countedKills / (double)this.root.nrOfEnemiesTotal * 0.200000002980232));
            this.prevCountedKills = this.countedKills;
        }
        float num49 = Mathf.Clamp01(nr1 / this.root.maxScoreReference);
        float num50 = Mathf.Clamp01(num49 * 3f - 2f);
        Vector3 localScale1 = this.finalRatingBar3Bar.transform.localScale;
        double num51 = (double)(localScale1.x = num50);
        Vector3 vector3_1 = this.finalRatingBar3Bar.transform.localScale = localScale1;
        float num52 = Mathf.Clamp01(num49 * 3f - 1f);
        Vector3 localScale2 = this.finalRatingBar2Bar.transform.localScale;
        double num53 = (double)(localScale2.x = num52);
        Vector3 vector3_2 = this.finalRatingBar2Bar.transform.localScale = localScale2;
        float num54 = Mathf.Clamp01(num49 * 3f);
        Vector3 localScale3 = this.finalRatingBar1Bar.transform.localScale;
        double num55 = (double)(localScale3.x = num54);
        Vector3 vector3_3 = this.finalRatingBar1Bar.transform.localScale = localScale3;
        if ((double)num48 <= 130.0)
            return;
        float num56 = num48 - 130f;
        if (!this.ratingTextDoOnce)
        {
            this.pedroFaceImage.sprite = this.root.pedroExpressions[(int)Mathf.Clamp(num49 * (float)(Extensions.get_length((System.Array)this.root.pedroExpressions) - 1), 0.0f, 26f)];
            this.theAudioSource.clip = this.ratingSound;
            this.theAudioSource.pitch = (float)(0.800000011920929 + (double)num49 * 0.699999988079071);
            this.theAudioSource.volume = (float)(0.699999988079071 + (double)num49 * 0.200000002980232);
            this.theAudioSource.Play();
            this.root.rumble(0, 0.9f, 0.2f);
            this.root.rumble(1, 0.8f, 0.15f);
            float num2 = UnityEngine.Random.value;
            int num6 = new int();
            int num7;
            if ((double)num49 >= 1.0)
            {
                this.pedroFaceImage.sprite = this.root.pedroExpressions[UnityEngine.Random.Range(27, 30)];
                this.finalRatingLetterText.text = this.root.GetTranslation("esRateS");
                this.finalRatingForText.text = (double)num2 <= 0.75 ? ((double)num2 <= 0.5 ? ((double)num2 <= 0.25 ? this.root.GetTranslation("esSFor4") : this.root.GetTranslation("esSFor3")) : this.root.GetTranslation("esSFor2")) : this.root.GetTranslation("esSFor1");
                num7 = 4;
            }
            else if ((double)num49 > 0.666666686534882)
            {
                this.finalRatingLetterText.text = this.root.GetTranslation("esRateA");
                this.finalRatingForText.text = (double)num2 <= 0.75 ? ((double)num2 <= 0.5 ? ((double)num2 <= 0.25 ? this.root.GetTranslation("esAFor4") : this.root.GetTranslation("esAFor3")) : this.root.GetTranslation("esAFor2")) : this.root.GetTranslation("esAFor1");
                num7 = 3;
            }
            else if ((double)num49 > 0.333333343267441)
            {
                this.finalRatingLetterText.text = this.root.GetTranslation("esRateB");
                this.finalRatingForText.text = (double)num2 <= 0.75 ? ((double)num2 <= 0.5 ? ((double)num2 <= 0.25 ? this.root.GetTranslation("esBFor4") : this.root.GetTranslation("esBFor3")) : this.root.GetTranslation("esBFor2")) : this.root.GetTranslation("esBFor1");
                num7 = 2;
            }
            else
            {
                this.finalRatingLetterText.text = this.root.GetTranslation("esRateC");
                this.finalRatingForText.text = (double)num2 <= 0.75 ? ((double)num2 <= 0.5 ? ((double)num2 <= 0.25 ? this.root.GetTranslation("esCFor4") : this.root.GetTranslation("esCFor3")) : this.root.GetTranslation("esCFor2")) : this.root.GetTranslation("esCFor1");
                num7 = 1;
            }
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            float bestScoreForLevel = this.rootShared.GetBestScoreForLevel(buildIndex, false);
            if ((double)nr1 > (double)bestScoreForLevel)
                this.newRecord = true;
            if (!this.rootShared.gameModifiersCheck())
            {
                if (num7 > SavedData.GetInt(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlRating", (object)buildIndex), "diff"), (object)this.root.difficultyMode))))
                    SavedData.SetInt(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlRating", (object)buildIndex), "diff"), (object)this.root.difficultyMode)), num7);
                if ((double)nr1 < 9999999.0 && (double)nr1 > (double)SavedData.GetInt(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)buildIndex), "diff"), (object)this.root.difficultyMode))))
                {
                    SavedData.SetInt(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)buildIndex), "diff"), (object)this.root.difficultyMode)), (int)nr1);
                    SavedData.SetString(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlScore", (object)buildIndex), "diff"), (object)this.root.difficultyMode), "ID")), !this.root.GetCCheck() ? this.rootShared.userID : "-999");
                }
                if (!SavedData.HasKey(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlTime", (object)buildIndex), "diff"), (object)this.root.difficultyMode))) || (double)this.root.finishTime - (double)this.root.startTime <= (double)SavedData.GetFloat(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlTime", (object)buildIndex), "diff"), (object)this.root.difficultyMode))))
                    SavedData.SetFloat(CryptoString.Encrypt(RuntimeServices.op_Addition(RuntimeServices.op_Addition(RuntimeServices.op_Addition("lvlTime", (object)buildIndex), "diff"), (object)this.root.difficultyMode)), this.root.finishTime - this.root.startTime);
            }
            if ((double)bestScoreForLevel > 0.0)
                this.personalBestScore.text = !this.newRecord ? this.rootShared.addCommasToNumber(bestScoreForLevel) : this.root.addCommasToNumber(nr1);
            this.statsTracker.countSRatings();
            this.statsTracker.checkChapterComplete();
            if (((double)nr1 < 9999999.0 || this.rootShared.speedrunnerLeaderboard) && (this.rootShared.OkToUploadScore() && !this.rootShared.gameModifiersCheck()))
                this.rootShared.DoUploadScore(!this.rootShared.speedrunnerLeaderboard ? (int)nr1 : (int)(((double)this.root.finishTime - (double)this.root.startTime) * 1000.0), this.rootShared.GetLeaderboardName(buildIndex, !this.root.GetCCheck() ? string.Empty : "-999", false));
            this.ratingTextDoOnce = true;
        }
        if (this.personalBestScore.text != "-")
        {
            if (this.leaderboardBestScore.text != "-")
            {
                float num2 = Mathf.Clamp01(this.leaderboardBestScore.color.a + 0.02f * p);
                Color color1 = this.leaderboardBestScore.color;
                double num6 = (double)(color1.a = num2);
                Color color2 = this.leaderboardBestScore.color = color1;
                float num7 = num2;
                Color color3 = this.leaderboardBestScoreHeader.color;
                double num8 = (double)(color3.a = num7);
                Color color4 = this.leaderboardBestScoreHeader.color = color3;
                float num9 = this.root.DampUnscaled(this.previousScoreDisplayStartPos.x, this.previousScoreDisplay.anchoredPosition.x, 0.3f);
                Vector2 anchoredPosition2 = this.previousScoreDisplay.anchoredPosition;
                double num10 = (double)(anchoredPosition2.x = num9);
                Vector2 vector2_2 = this.previousScoreDisplay.anchoredPosition = anchoredPosition2;
            }
            else
            {
                float num2 = this.root.DampUnscaled(this.previousScoreDisplayStartPos.x - 107f, this.previousScoreDisplay.anchoredPosition.x, 0.3f);
                Vector2 anchoredPosition2 = this.previousScoreDisplay.anchoredPosition;
                double num6 = (double)(anchoredPosition2.x = num2);
                Vector2 vector2_2 = this.previousScoreDisplay.anchoredPosition = anchoredPosition2;
            }
            if (this.newRecord)
                this.personalBestScoreHeader.text = (double)Mathf.Sin(Time.unscaledTime * 3f) <= 0.0 ? this.root.GetTranslation("esNRecord") : this.root.GetTranslation("esPBest");
        }
        float num57 = this.root.DampUnscaled(this.theRectStartScale.y, this.theRect.sizeDelta.y, 0.3f);
        Vector2 sizeDelta1 = this.theRect.sizeDelta;
        double num58 = (double)(sizeDelta1.y = num57);
        Vector2 vector2_6 = this.theRect.sizeDelta = sizeDelta1;
        float num59 = (float)((double)Mathf.Clamp(num56, 0.0f, 20f) / 20.0 * 0.100000001490116);
        Color color37 = this.finalRatingBackground.color;
        double num60 = (double)(color37.a = num59);
        Color color38 = this.finalRatingBackground.color = color37;
        float num61 = Mathf.Clamp(num56, 0.0f, 20f) / 20f;
        Color color39 = this.finalRatingForText.color;
        double num62 = (double)(color39.a = num61);
        Color color40 = this.finalRatingForText.color = color39;
        float num63 = num61;
        Color color41 = this.finalRatingLetterText.color;
        double num64 = (double)(color41.a = num63);
        Color color42 = this.finalRatingLetterText.color = color41;
        this.finalRatingLetterText.transform.localScale = Vector3.one * Mathf.Clamp(15f - num56, 1f, 15f);
        float num65 = Mathf.Clamp01(1f - num56 * 0.1f) * 10f;
        Vector3 eulerAngles1 = this.finalRatingLetterText.transform.eulerAngles;
        double num66 = (double)(eulerAngles1.z = num65);
        Vector3 vector3_4 = this.finalRatingLetterText.transform.eulerAngles = eulerAngles1;
        if ((double)num56 > 10.0 && (double)num56 < 20.0)
        {
            this.finalRatingLetterText.transform.localScale = this.finalRatingLetterText.transform.localScale - Vector3.one * ((20f - num56) / 20f) * 0.5f;
            float num2 = this.theRectStartPos.y + (float)((double)UnityEngine.Random.Range(-1, 1) * ((20.0 - (double)num56) / 20.0) * 30.0);
            Vector2 anchoredPosition2 = this.theRect.anchoredPosition;
            double num6 = (double)(anchoredPosition2.y = num2);
            Vector2 vector2_2 = this.theRect.anchoredPosition = anchoredPosition2;
            float num7 = this.theRectStartPos.x + (float)((double)UnityEngine.Random.Range(-1, 1) * ((20.0 - (double)num56) / 20.0) * 10.0);
            Vector2 anchoredPosition3 = this.theRect.anchoredPosition;
            double num8 = (double)(anchoredPosition3.x = num7);
            Vector2 vector2_3 = this.theRect.anchoredPosition = anchoredPosition3;
        }
        if ((double)num56 <= 60.0)
            return;
        float num67 = num56 - 60f;
        if (this.gameHighlightTweetPrompt.activeInHierarchy || this.gameHighlightPinPrompt.activeInHierarchy)
        {
            this.gameHighlight.anchoredPosition = this.root.DampV2Unscaled(this.gameHighlightStartPos + new Vector2(-this.gameHighlight.sizeDelta.x, 0.0f), this.gameHighlight.anchoredPosition, 0.3f);
            int num2 = 0;
            Color color1 = this.gameScoreTextScore.color;
            double num6 = (double)(color1.a = (float)num2);
            Color color2 = this.gameScoreTextScore.color = color1;
            int num7 = num2;
            Color color3 = this.gameScoreText.color;
            double num8 = (double)(color3.a = (float)num7);
            Color color4 = this.gameScoreText.color = color3;
            int num9 = 0;
            Color color5 = this.timeBonusTextScore.color;
            double num10 = (double)(color5.a = (float)num9);
            Color color6 = this.timeBonusTextScore.color = color5;
            int num11 = num9;
            Color color7 = this.timeBonusText.color;
            double num12 = (double)(color7.a = (float)num11);
            Color color8 = this.timeBonusText.color = color7;
            int num15 = 0;
            Color color9 = this.killsTextScore.color;
            double num16 = (double)(color9.a = (float)num15);
            Color color10 = this.killsTextScore.color = color9;
            int num17 = num15;
            Color color11 = this.killsText.color;
            double num18 = (double)(color11.a = (float)num17);
            Color color12 = this.killsText.color = color11;
            int num19 = 0;
            Color color13 = this.noDeathBonusTextScore.color;
            double num20 = (double)(color13.a = (float)num19);
            Color color14 = this.noDeathBonusTextScore.color = color13;
            int num21 = num19;
            Color color15 = this.noDeathBonusText.color;
            double num22 = (double)(color15.a = (float)num21);
            Color color16 = this.noDeathBonusText.color = color15;
            int num23 = 0;
            Color color17 = this.difficultyBonusTextScore.color;
            double num24 = (double)(color17.a = (float)num23);
            Color color18 = this.difficultyBonusTextScore.color = color17;
            int num25 = num23;
            Color color19 = this.difficultyBonusText.color;
            double num26 = (double)(color19.a = (float)num25);
            Color color20 = this.difficultyBonusText.color = color19;
            int num27 = 0;
            Color color21 = this.finalRatingTextScore.color;
            double num28 = (double)(color21.a = (float)num27);
            Color color22 = this.finalRatingTextScore.color = color21;
            int num29 = num27;
            Color color23 = this.finalRatingText.color;
            double num30 = (double)(color23.a = (float)num29);
            Color color24 = this.finalRatingText.color = color23;
            int num31 = 0;
            Color color25 = this.finalRatingBackground.color;
            double num32 = (double)(color25.a = (float)num31);
            Color color26 = this.finalRatingBackground.color = color25;
            int num33 = 0;
            Color color27 = this.finalRatingLetterText.color;
            double num34 = (double)(color27.a = (float)num33);
            Color color28 = this.finalRatingLetterText.color = color27;
            int num35 = 0;
            Color color29 = this.finalRatingForText.color;
            double num36 = (double)(color29.a = (float)num35);
            Color color30 = this.finalRatingForText.color = color29;
            int num37 = 0;
            Color color31 = this.finalRatingBar1Bar.color;
            double num38 = (double)(color31.a = (float)num37);
            Color color32 = this.finalRatingBar1Bar.color = color31;
            int num39 = num37;
            Color color33 = this.finalRatingBar1.color;
            double num40 = (double)(color33.a = (float)num39);
            Color color34 = this.finalRatingBar1.color = color33;
            int num41 = 0;
            Color color35 = this.finalRatingBar2Bar.color;
            double num42 = (double)(color35.a = (float)num41);
            Color color36 = this.finalRatingBar2Bar.color = color35;
            int num43 = num41;
            Color color43 = this.finalRatingBar2.color;
            double num44 = (double)(color43.a = (float)num43);
            Color color44 = this.finalRatingBar2.color = color43;
            int num45 = 0;
            Color color45 = this.finalRatingBar3Bar.color;
            double num46 = (double)(color45.a = (float)num45);
            Color color46 = this.finalRatingBar3Bar.color = color45;
            int num47 = num45;
            Color color47 = this.finalRatingBar3.color;
            double num68 = (double)(color47.a = (float)num47);
            Color color48 = this.finalRatingBar3.color = color47;
            if (!this.tweetUIStuffDoOnce)
            {
                this.nextLevelUIButton.interactable = false;
                this.restartLevelUIButton.interactable = false;
                this.exitUIButton.interactable = false;
                this.tweetUIStuffDoOnce = true;
            }
        }
        else
        {
            this.gameHighlight.anchoredPosition = this.root.DampV2Unscaled(this.gameHighlightStartPos, this.gameHighlight.anchoredPosition, 0.3f);
            if (this.tweetUIStuffDoOnce)
            {
                this.nextLevelUIButton.interactable = true;
                this.restartLevelUIButton.interactable = true;
                this.exitUIButton.interactable = true;
                this.tweetUIStuffDoOnce = false;
            }
        }
        if (!this.nextLevelButton.gameObject.activeInHierarchy)
        {
            this.nextLevelButton.gameObject.SetActive(true);
            this.restartLevelButton.gameObject.SetActive(true);
            this.exitButton.gameObject.SetActive(true);
            if (this.root.hasCapturedMoment)
                this.gameHighlight.gameObject.SetActive(true);
            this.gameModifiersNotice.SetActive(this.rootShared.gameModifiersCheck());
            this.optionSelected = 0;
            this.DoOptionSelectStuff();
        }
        float num69 = (float)(((double)Mathf.Sin(Time.unscaledTime * 0.33f) * 0.100000001490116 + (double)Mathf.Sin(Time.unscaledTime * 1.3f) * 0.300000011920929 + (double)Mathf.Sin(Time.unscaledTime * 5.3f) * 1.5) * (1.0 - (double)num49) * 0.025000000372529);
        this.theAudioSource.pitch = (float)(0.800000011920929 + (double)num49 * 0.699999988079071) + num69;
        this.theAudioSource.volume += (float)((0.699999988079071 + (double)num49 * 0.200000002980232 + (double)num69 * 10.0 - (double)this.theAudioSource.volume) * 0.200000002980232);
        if ((double)this.timer <= 622.0)
            return;
        this.uiConfirmHintCanvasGroup.alpha = !this.nextLevelUIButton.interactable ? 0.0f : 1f;
        if ((double)this.player.GetAxisRaw("UIYAxis") > 0.5)
        {
            if (!this.optionNavDoOnce)
            {
                if (!this.rootShared.isDemo)
                {
                    this.optionSelected = Mathf.Clamp(this.optionSelected - 1, 0, 2);
                    this.DoOptionSelectStuff();
                }
                this.optionNavDoOnce = true;
            }
        }
        else if ((double)this.player.GetAxisRaw("UIYAxis") < -0.5)
        {
            if (!this.optionNavDoOnce)
            {
                if (!this.rootShared.isDemo)
                {
                    this.optionSelected = Mathf.Clamp(this.optionSelected + 1, 0, 2);
                    this.DoOptionSelectStuff();
                }
                this.optionNavDoOnce = true;
            }
        }
        else if (this.player.GetButton("UISubmit"))
        {
            if (!this.optionNavDoOnce)
            {
                if (this.optionSelected == 0)
                    this.nextLevelButtonScript.mousePressed = true;
                else if (this.optionSelected == 1)
                    this.restartLevelButtonScript.mousePressed = true;
                else if (this.optionSelected == 2)
                    this.exitButtonScript.mousePressed = true;
                this.optionNavDoOnce = true;
            }
        }
        else if (this.optionNavDoOnce)
        {
            if (this.nextLevelUIButton.interactable)
            {
                if (this.nextLevelButtonScript.mousePressed)
                    ((UnityEngine.UI.Button)this.nextLevelButton.GetComponent(typeof(UnityEngine.UI.Button))).onClick.Invoke();
                else if (this.restartLevelButtonScript.mousePressed)
                    ((UnityEngine.UI.Button)this.restartLevelButtonScript.GetComponent(typeof(UnityEngine.UI.Button))).onClick.Invoke();
                else if (this.exitButtonScript.mousePressed)
                    ((UnityEngine.UI.Button)this.exitButtonScript.GetComponent(typeof(UnityEngine.UI.Button))).onClick.Invoke();
            }
            this.nextLevelButtonScript.mousePressed = false;
            this.restartLevelButtonScript.mousePressed = false;
            this.exitButtonScript.mousePressed = false;
            this.optionNavDoOnce = false;
        }
        if ((UnityEngine.Object)this.gifSaveButtonScript != (UnityEngine.Object)null && this.gifSaveButtonScript.actualMouseUsed || (UnityEngine.Object)this.tweeetButtonScript != (UnityEngine.Object)null && this.tweeetButtonScript.actualMouseUsed)
        {
            if ((UnityEngine.Object)this.gifSaveButtonScript != (UnityEngine.Object)null)
                this.gifSaveButtonScript.actualMouseUsed = false;
            if ((UnityEngine.Object)this.tweeetButtonScript != (UnityEngine.Object)null)
                this.tweeetButtonScript.actualMouseUsed = false;
            this.optionSelected = -1;
            this.DoOptionSelectStuff();
            this.nextLevelButtonScript.mousePressed = false;
            this.nextLevelButtonScript.mouseOver = false;
            this.restartLevelButtonScript.mousePressed = false;
            this.restartLevelButtonScript.mouseOver = false;
            this.exitButtonScript.mousePressed = false;
            this.exitButtonScript.mouseOver = false;
        }
        if (this.nextLevelButtonScript.actualMouseUsed)
        {
            this.optionSelected = 0;
            this.DoOptionSelectStuff();
            this.restartLevelButtonScript.mousePressed = false;
            this.restartLevelButtonScript.mouseOver = false;
            this.exitButtonScript.mousePressed = false;
            this.exitButtonScript.mouseOver = false;
        }
        if (this.restartLevelButtonScript.actualMouseUsed)
        {
            this.optionSelected = 1;
            this.DoOptionSelectStuff();
            this.nextLevelButtonScript.mousePressed = false;
            this.nextLevelButtonScript.mouseOver = false;
            this.exitButtonScript.mousePressed = false;
            this.exitButtonScript.mouseOver = false;
        }
        if (this.exitButtonScript.actualMouseUsed)
        {
            this.optionSelected = 2;
            this.DoOptionSelectStuff();
            this.nextLevelButtonScript.mousePressed = false;
            this.nextLevelButtonScript.mouseOver = false;
            this.restartLevelButtonScript.mousePressed = false;
            this.restartLevelButtonScript.mouseOver = false;
        }
        if (this.rootShared.runningOnConsole)
            return;
        if (this.player.GetButtonDown("UISpecial1") && !this.hasExportedGifFromGamepad)
        {
            ((HUDFunnelScript)this.transform.GetComponentInParent(typeof(HUDFunnelScript))).saveGif();
            this.gifSaveButtonPrompt.SetActive(false);
            this.hasExportedGifFromGamepad = true;
        }
        if ((!this.useGamepadIcons || !this.gifSaveButton.interactable) && this.gifSaveButtonPrompt.activeSelf)
        {
            this.gifSaveButtonPrompt.SetActive(false);
        }
        else
        {
            if (!this.useGamepadIcons || !this.gifSaveButton.interactable || this.gifSaveButtonPrompt.activeSelf)
                return;
            this.gifSaveButtonPrompt.SetActive(true);
        }

    }

    public virtual void DoOptionSelectStuff()
    {
        if (this.optionSelected == 0)
        {
            this.nextLevelButtonScript.mouseOver = true;
            this.nextLevelButtonScript.actualMouseUsed = false;
            this.restartLevelButtonScript.mouseOver = false;
            this.restartLevelButtonScript.actualMouseUsed = false;
            this.exitButtonScript.mouseOver = false;
            this.exitButtonScript.actualMouseUsed = false;
        }
        else if (this.optionSelected == 1)
        {
            this.nextLevelButtonScript.mouseOver = false;
            this.nextLevelButtonScript.actualMouseUsed = false;
            this.restartLevelButtonScript.mouseOver = true;
            this.restartLevelButtonScript.actualMouseUsed = false;
            this.exitButtonScript.mouseOver = false;
            this.exitButtonScript.actualMouseUsed = false;
        }
        else if (this.optionSelected == 2)
        {
            this.nextLevelButtonScript.mouseOver = false;
            this.nextLevelButtonScript.actualMouseUsed = false;
            this.restartLevelButtonScript.mouseOver = false;
            this.restartLevelButtonScript.actualMouseUsed = false;
            this.exitButtonScript.mouseOver = true;
            this.exitButtonScript.actualMouseUsed = false;
        }
        Controller activeController = this.player.controllers.GetLastActiveController();
        if (!RuntimeServices.EqualityOperator((object)activeController, (object)null))
        {
            if (this.useGamepadIcons && activeController.type == ControllerType.Keyboard)
            {
                this.useGamepadIcons = false;
                this.createNavigationHints();
            }
            else if (!this.useGamepadIcons && activeController.type == ControllerType.Joystick)
            {
                this.useGamepadIcons = true;
                this.createNavigationHints();
            }
        }
        if (!this.hasExportedGifFromGamepad && this.useGamepadIcons && ((UnityEngine.Object)this.gifSaveButtonPrompt != (UnityEngine.Object)null && !this.gifSaveButtonPrompt.activeInHierarchy))
        {
            this.gifSaveButtonPrompt.SetActive(true);
        }
        else
        {
            if (this.useGamepadIcons || !((UnityEngine.Object)this.gifSaveButtonPrompt != (UnityEngine.Object)null) || !this.gifSaveButtonPrompt.activeInHierarchy)
                return;
            this.gifSaveButtonPrompt.SetActive(false);
        }
    }

    public virtual void doCountingSound(float refNr)
    {
        if (this.theAudioSource.isPlaying && !((UnityEngine.Object)this.theAudioSource.clip == (UnityEngine.Object)this.appearSound))
            return;
        this.theAudioSource.clip = this.countSound;
        this.theAudioSource.volume = Mathf.Clamp01((float)(0.5 + (double)refNr * 0.5));
        this.theAudioSource.pitch = Mathf.Clamp(0.5f + refNr, 0.0f, 1.8f);
        this.theAudioSource.Play();
    }

    public virtual void createNavigationHints()
    {
        float num = 1f;
        if ((UnityEngine.Object)this.uiConfirmHint != (UnityEngine.Object)null)
        {
            num = this.uiConfirmHintCanvasGroup.alpha;
            UnityEngine.Object.Destroy((UnityEngine.Object)this.uiConfirmHint);
        }
        this.uiConfirmHint = this.rootShared.createHintText(RuntimeServices.op_Addition("<UISUBMIT> ", this.rootShared.GetTranslation("uiConfirm")), "uiConfirm", this.transform.parent, this.useGamepadIcons, false);
        this.uiConfirmHintCanvasGroup = (CanvasGroup)this.uiConfirmHint.AddComponent(typeof(CanvasGroup));
        this.uiConfirmHintCanvasGroup.alpha = num;
        this.uiConfirmHint.transform.localScale *= 0.75f;
        RectTransform component = (RectTransform)this.uiConfirmHint.GetComponent(typeof(RectTransform));
        component.anchorMin = component.anchorMax = new Vector2(0.0f, 0.0f);
        component.anchoredPosition = new Vector2(26f, 24f);
    }
}
