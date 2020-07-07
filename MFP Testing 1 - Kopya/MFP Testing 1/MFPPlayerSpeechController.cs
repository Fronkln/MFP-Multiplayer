// Decompiled with JetBrains decompiler
// Type: EnemySpeechHandlerScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

//Basically modified EnemySpeechHandlerScript.cs

using Boo.Lang.Runtime;
using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Lang;

[Serializable]
public class MFPPlayerSpeechController : MonoBehaviour
{
    private RootScript root;
    private Camera mainCamera;
    private string theText;
    private float textTimer;
    private float textSpeedMultiplier;
    private int lastChar;
    private GameObject uiTextGameObject;
    private Text uiText;
    private RectTransform rectTrans;
    private Transform followTransform;
    private AudioSource voice;
    private bool voiceDoOnce;
    private VoiceControllerScript voiceController;
    public bool speaking;
    public bool conversationSpeaking;
    public bool finishedConversationSpeaking;
    public bool dontInterrupt = true;
    public bool dontAnimate = true;


    public virtual void Awake()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.mainCamera = (Camera)GameObject.Find("Main Camera").GetComponent(typeof(Camera));
;
        this.followTransform = this.transform.Find("PlayerGraphics/Armature/Center/LowerBack/UpperBack/Neck/Head");
        this.voiceController = (VoiceControllerScript)this.gameObject.AddComponent(typeof(VoiceControllerScript));
        this.uiTextGameObject = (GameObject)UnityEngine.Object.Instantiate(UnityEngine.Resources.Load("HUD/EnemySpeech"));
        this.voice = (AudioSource)this.uiTextGameObject.transform.Find("EnemyVoice").GetComponent(typeof(AudioSource));
        this.voice.transform.parent = this.followTransform;
        this.voice.transform.localPosition = Vector3.zero;
        this.voice.transform.localScale = Vector3.one;
        this.voice.clip = transform.Find("Voice").GetComponent<AudioSource>().clip;
        this.voiceController.voice = this.voice;
       // this.voiceController.voicePitchOffset = this.enemyScript.voicePitchOffset;
        this.voiceController.voicePitchRange = 1.5f;
        this.voiceController.voicePitchWobbleMultiplier = 0.1f;
        this.uiTextGameObject.transform.SetParent(GameObject.Find("HUD/Canvas/EnemySpeechHolder").transform);
        this.uiTextGameObject.transform.localScale = Vector3.one;
        this.rectTrans = (RectTransform)this.uiTextGameObject.GetComponent(typeof(RectTransform));
        this.uiText = (Text)this.uiTextGameObject.transform.Find("EnemySpeechText").GetComponent(typeof(Text));
        this.clearText();
    }

    public virtual void updateBubbleSize()
    {
        float num1 = this.uiText.preferredHeight + 5f;
        Vector2 sizeDelta1 = this.rectTrans.sizeDelta;
        double num2 = (double)(sizeDelta1.y = num1);
        Vector2 vector2_1 = this.rectTrans.sizeDelta = sizeDelta1;
        float num3 = Mathf.Clamp(this.uiText.preferredWidth + 10f, 0.0f, 140f);
        Vector2 sizeDelta2 = this.rectTrans.sizeDelta;
        double num4 = (double)(sizeDelta2.x = num3);
        Vector2 vector2_2 = this.rectTrans.sizeDelta = sizeDelta2;
    }

    public virtual void Update()
    {
        if ((UnityEngine.Object)this.followTransform == (UnityEngine.Object)null)
            return;
        if (!string.IsNullOrEmpty(this.theText))
        {
            this.uiTextGameObject.transform.position = this.mainCamera.WorldToScreenPoint(this.followTransform.position);
            float num1 = this.rectTrans.anchoredPosition.y + 30f;
            Vector2 anchoredPosition = this.rectTrans.anchoredPosition;
            double num2 = (double)(anchoredPosition.y = num1);
            Vector2 vector2 = this.rectTrans.anchoredPosition = anchoredPosition;
            this.textTimer += this.textSpeedMultiplier * this.root.timescale;
            int num3 = !this.dontAnimate ? 1 : 0;

            /*if (num3 != 0)
                num3 = this.enemyScript.idle ? 1 : 0;
            if (num3 != 0)
                num3 = (double)Mathf.Abs(this.enemyScript.targetXSpeed) < 0.300000011920929 ? 1 : 0;
            if (num3 != 0)
                num3 = (double)this.enemyScript.alertAmount < 0.0500000007450581 ? 1 : 0;
                */
            bool flag = num3 != 0;
            if (this.lastChar < this.theText.Length)
            {
                if (!this.voiceDoOnce)
                {
                    if (this.voice.gameObject.activeInHierarchy)
                        this.voice.Play();
                    this.speaking = true;
 
                    this.voiceDoOnce = true;
                }
                if ((double)this.textTimer >= 3.0)
                {
                    if (this.theText[this.lastChar] == '<')
                    {
                        int num4 = this.theText.IndexOf(">", this.lastChar);
                        if (flag)
                        {
                            MonoBehaviour.print((object)this.theText.Substring(this.lastChar + 1, num4 - this.lastChar - 1));
                        }
                        this.lastChar = num4 + 1;
                    }
                    this.uiText.text = RuntimeServices.op_Addition(this.uiText.text, (object)this.theText[this.lastChar]);
                    this.textTimer = this.theText[this.lastChar] == '.' || this.theText[this.lastChar] == '?' || this.theText[this.lastChar] == '!' ? -8f : 0.0f;
                    this.voiceController.curVoiceChar = this.theText[this.lastChar];
                    this.voiceController.voiceTimer = (float)this.lastChar;
                    this.updateBubbleSize();
                    ++this.lastChar;
                }
                this.voiceController.doVoice();
            }
            else
            {
                if (this.voiceDoOnce)
                {
                    this.voice.Stop();
                    this.voiceDoOnce = false;
                }
                if ((double)this.textTimer > (double)Mathf.Clamp(this.uiText.text.Length / 2, 60, 120))
                {
                    this.speaking = false;
                    if (this.conversationSpeaking)
                        this.finishedConversationSpeaking = true;
                }
                if ((double)this.textTimer > (double)Mathf.Clamp(this.uiText.text.Length / 2, 100, 240))
                    this.clearText();
            }
        }
        if ((double)PlayerScript.PlayerInstance.health > 0.0 || !this.voice.isPlaying) //1
            return;
        this.voice.Stop();
    }

    public virtual void clearText()
    {
        this.theText = string.Empty;
        this.uiText.text = string.Empty;
        this.textTimer = 0.0f;
        this.uiTextGameObject.SetActive(false);
    }

    public virtual void speak(string txt, float speed, bool additive)
    {
        if (txt == string.Empty)
        {
            this.clearText();
        }
        else
        {
            if (additive && this.uiText.text.Length > 0)
            {
                if (this.uiText.text.Length < 4 || this.uiText.text.Length >= 4 && !this.uiText.text.EndsWith("... "))
                    this.uiText.text = !(LocalizationManager.CurrentLanguageCode == "de") ? RuntimeServices.op_Addition(this.uiText.text, "... ") : RuntimeServices.op_Addition(this.uiText.text, " ... ");
                this.textTimer = -10f;
                this.lastChar = this.theText.Length;
                this.theText = RuntimeServices.op_Addition(this.theText, txt);
            }
            else
            {
                this.theText = txt;
                this.uiText.text = string.Empty;
                this.lastChar = 0;
                this.textTimer = 0.0f;
            }
            this.voiceController.voiceLength = (float)txt.Length;
            this.voiceController.isQuestion = this.theText[Extensions.get_length(this.theText) - 1] == '?';
            this.textSpeedMultiplier = speed;
            this.conversationSpeaking = false;
            this.uiTextGameObject.SetActive(true);
            this.updateBubbleSize();
        }
    }

    public virtual void conversationSpeak(string txt)
    {
        this.speak(txt, 1f, false);
        this.conversationSpeaking = true;
        this.finishedConversationSpeaking = false;
    }

    public virtual void destroySpeechBubble()
    {
        UnityEngine.Object.Destroy((UnityEngine.Object)this.uiTextGameObject);
    }

    public virtual void Main()
    {
    }
}
