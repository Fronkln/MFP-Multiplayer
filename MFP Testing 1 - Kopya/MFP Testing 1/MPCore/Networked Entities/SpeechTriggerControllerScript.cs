using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x020000C2 RID: 194
[Serializable]
public class SpeechTriggerControllerScript : MonoBehaviour
{
    private BaseNetworkEntity networkHelper;

    // Token: 0x0400112C RID: 4396
    public SwitchScript[] inputSwitch;

    // Token: 0x0400112D RID: 4397
    private float switchInput;

    // Token: 0x0400112E RID: 4398
    public bool stayOnOnceActivated;

    // Token: 0x0400112F RID: 4399
    public bool triggerWithoutPlayer;

    // Token: 0x04001130 RID: 4400
    private bool beenActivated;

    // Token: 0x04001131 RID: 4401
    public bool disableActionModeIfClickToContinue;

    // Token: 0x04001132 RID: 4402
    [Header("Add speechTriggerScripts below and they will trigger in order")]
    public bool reTrigger;

    // Token: 0x04001133 RID: 4403
    public bool activateSwitchAfterSpeech;

    // Token: 0x04001134 RID: 4404
    public bool overrideCurrentSpeechBubble;

    // Token: 0x04001135 RID: 4405
    [Header("Extra SwitchScript")]
    public SwitchScript activateExtraSwitchScriptAfterSpeech;

    // Token: 0x04001136 RID: 4406
    [Header("HintText options - Disable ForceActivate on target object to work")]
    public bool clearHintTextOnStart;

    // Token: 0x04001137 RID: 4407
    public HintTextScript triggerHintTextOnFinish;

    // Token: 0x04001138 RID: 4408
    public bool clearInstructionTextOnStart;

    // Token: 0x04001139 RID: 4409
    public InstructionTextScript triggerInstructionTextOnFinish;

    // Token: 0x0400113A RID: 4410
    private SwitchScript switchScript;

    // Token: 0x0400113B RID: 4411
    private RootScript root;

    // Token: 0x0400113C RID: 4412
    private SpeechTriggerScript[] speechScripts;

    // Token: 0x0400113D RID: 4413
    private int curSpeech;

    // Token: 0x0400113E RID: 4414
    private bool triggered;

    // Token: 0x0400113F RID: 4415
    private bool doNotActivateAgain;

    // Token: 0x04001140 RID: 4416
    private bool noSpeechBubbleActive;

    // Token: 0x04001141 RID: 4417
    private bool noSpeechBubbleActiveDoOnce;

    // Token: 0x04001142 RID: 4418
    private RectTransform sbBubble;

    // Token: 0x04001143 RID: 4419
    private bool reachedEndOfSpeeches;

    // Token: 0x0600055C RID: 1372 RVA: 0x00004265 File Offset: 0x00002465
    public SpeechTriggerControllerScript()
    {
        this.disableActionModeIfClickToContinue = true;
    }

    public void TriggerTheSpeech(Steamworks.CSteamID activator)
    {
        if (!EMFDNS.isLocalUser(activator))
            if (speechScripts[curSpeech].clickToContinue && !speechScripts[curSpeech].clickToContinueDontFreeze && Time.timeSinceLevelLoad > 2)
            {
                PlayerScript.PlayerInstance.transform.position = MultiplayerManagerTest.inst.playerObjects[activator].realRootPosition;
                MFPEditorUtils.Log("Teleported client");
            }

        triggered = true;
        PacketSender.SendHostSpeechScriptState(true);
        // StartCoroutine(triggerSpeech());
    }

    public virtual void Awake()
    {
        networkHelper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    // Token: 0x0600055D RID: 1373 RVA: 0x0008F514 File Offset: 0x0008D714
    public virtual void Start()
    {
        if (inputSwitch == null)
            inputSwitch = new SwitchScript[0];

        this.switchScript = (SwitchScript)this.gameObject.GetComponent(typeof(SwitchScript));
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.speechScripts = this.GetComponents<SpeechTriggerScript>();
        this.switchScript.output = (float)-1;
        this.sbBubble = (RectTransform)GameObject.Find("HUD/Canvas/SpeechBubble_Bubble").GetComponent(typeof(RectTransform));

        if (this.triggerWithoutPlayer)
            this.triggered = true;
    }

    // Token: 0x0600055E RID: 1374 RVA: 0x0008F5B4 File Offset: 0x0008D7B4
    public virtual void Update()
    {
        if (this.triggerWithoutPlayer && !this.triggered)
        {
            this.triggered = true;
        }
        if (Extensions.get_length(this.inputSwitch) > 0)
        {
            this.switchInput = (float)-1;
            int i = 0;
            SwitchScript[] array = this.inputSwitch;
            int length = array.Length;
            while (i < length)
            {
                if (array[i].output > this.switchInput)
                {
                    this.switchInput = array[i].output;
                }
                i++;
            }
        }
        else
        {
            this.switchInput = (float)1;
        }
        bool flag;
        if ((flag = !this.root.sbClickCont) && !(flag = (this.root.sbTimer <= (float)0)) && (flag = this.overrideCurrentSpeechBubble))
        {
            flag = (this.root.sbTriggerTransform != this.transform);
        }
        this.noSpeechBubbleActive = flag;
        if ((this.stayOnOnceActivated && this.beenActivated) || (!this.noSpeechBubbleActive && this.transform == this.root.sbTriggerTransform))
        {
            this.switchInput = (float)1;
        }
        if (this.switchInput >= (float)1)
        {
            if (!this.beenActivated)
            {
                if (this.clearHintTextOnStart)
                {
                }
                if (this.clearInstructionTextOnStart)
                {
                    this.root.clearInstructionText();
                }
                this.beenActivated = true;
            }
            if (this.noSpeechBubbleActive)
            {
                if (!this.noSpeechBubbleActiveDoOnce)
                {
                    if (this.reachedEndOfSpeeches)
                    {
                        if (this.triggerHintTextOnFinish != null)
                        {
                            this.triggerHintTextOnFinish.forceActivate = true;
                        }
                        if (this.triggerInstructionTextOnFinish != null)
                        {
                            this.triggerInstructionTextOnFinish.forceActivate = true;
                        }
                        if (this.speechScripts[this.speechScripts.Length - 1].forceSpawnPedro != null)
                        {
                            this.speechScripts[this.speechScripts.Length - 1].forceSpawnPedro.forceSpawn = false;
                        }
                        if (!this.speechScripts[this.speechScripts.Length - 1].keepExtraSwitchScriptActivated && this.speechScripts[this.speechScripts.Length - 1].activateExtraSwitchScript != null)
                        {
                            this.speechScripts[this.speechScripts.Length - 1].activateExtraSwitchScript.output = (float)0;
                        }
                    }
                    this.noSpeechBubbleActiveDoOnce = true;
                }
                if (!this.doNotActivateAgain)
                {
                    if (this.triggered)
                    {
                        this.StartCoroutine(this.triggerSpeech());
                    }
                    else
                    {
                        if (this.reachedEndOfSpeeches && this.activateSwitchAfterSpeech)
                        {
                            this.switchScript.output = (float)1;
                        }
                        else
                        {
                            this.switchScript.output = (float)-1;
                        }
                        if (this.reachedEndOfSpeeches && this.activateExtraSwitchScriptAfterSpeech != null)
                        {
                            this.activateExtraSwitchScriptAfterSpeech.output = (float)1;
                        }
                    }
                }
                else
                {
                    if (this.activateExtraSwitchScriptAfterSpeech != null)
                    {
                        this.activateExtraSwitchScriptAfterSpeech.output = (float)1;
                    }
                    if (this.activateSwitchAfterSpeech)
                    {
                        this.switchScript.output = (float)1;
                    }
                    else
                    {
                        this.switchScript.output = (float)-1;
                        UnityEngine.Object.Destroy(this.gameObject);
                    }
                }
            }
            else if (this.noSpeechBubbleActiveDoOnce)
            {
                this.noSpeechBubbleActiveDoOnce = false;
            }
        }
        else if (!this.activateSwitchAfterSpeech)
        {
            this.switchScript.output = (float)-1;
        }
    }

    // Token: 0x0600055F RID: 1375 RVA: 0x0008F93C File Offset: 0x0008DB3C
    public virtual void OnTriggerEnter(Collider col)
    {
        if (this.switchInput >= (float)1 && !this.doNotActivateAgain && col.tag == "Player" && !this.triggered)
        {
            if (MultiplayerManagerTest.singleplayerMode)
                this.triggered = true;
            else
                PacketSender.BaseNetworkedEntityRPC("TriggerTheSpeech", networkHelper.entityIdentifier);

            this.StartCoroutine(this.triggerSpeech());
        }
    }

    // Token: 0x06000560 RID: 1376 RVA: 0x00004274 File Offset: 0x00002474
    private IEnumerator triggerSpeech()
    {
        return new SpeechTriggerControllerScript.triggerSpeech2590(this).GetEnumerator();
    }

    // Token: 0x06000561 RID: 1377 RVA: 0x000020A7 File Offset: 0x000002A7
    public virtual void Main()
    {
    }

    // Token: 0x020000C3 RID: 195
    [CompilerGenerated]
    [Serializable]
    internal sealed class triggerSpeech2590 : GenericGenerator<object>
    {
        // Token: 0x04001144 RID: 4420
        internal SpeechTriggerControllerScript self_2593;

        // Token: 0x06000562 RID: 1378 RVA: 0x00004281 File Offset: 0x00002481
        public triggerSpeech2590(SpeechTriggerControllerScript self_)
        {
            this.self_2593 = self_;
        }

        // Token: 0x06000563 RID: 1379 RVA: 0x00004290 File Offset: 0x00002490
        public override IEnumerator<object> GetEnumerator()
        {
            return new SpeechTriggerControllerScript.triggerSpeech2590.sealedClass(this.self_2593);
        }

        // Token: 0x020000C4 RID: 196
        [CompilerGenerated]
        [Serializable]
        internal sealed class sealedClass : GenericGeneratorEnumerator<object>, IEnumerator
        {
            // Token: 0x04001145 RID: 4421
            internal string textToUse2591;

            // Token: 0x04001146 RID: 4422
            internal SpeechTriggerControllerScript self_2592;

            // Token: 0x06000564 RID: 1380 RVA: 0x0000429D File Offset: 0x0000249D
            public sealedClass(SpeechTriggerControllerScript self_)
            {

                this.self_2592 = self_;

            }

            // Token: 0x06000565 RID: 1381 RVA: 0x0008F998 File Offset: 0x0008DB98
            public override bool MoveNext()
            {
                switch (this._state)
                {
                    default:
                        if (this.self_2592.speechScripts[this.self_2592.curSpeech].voice != null)
                        {
                            this.self_2592.root.setVoice(this.self_2592.speechScripts[this.self_2592.curSpeech].voice);
                        }
                        this.textToUse2591 = null;
                        if (this.self_2592.speechScripts[this.self_2592.curSpeech].locStringId != string.Empty && this.self_2592.speechScripts[this.self_2592.curSpeech].locStringId != null)
                        {
                            this.textToUse2591 = this.self_2592.root.GetTranslation(this.self_2592.speechScripts[this.self_2592.curSpeech].locStringId);
                        }
                        else
                        {
                            Debug.Log("NO LOCALIZATION STRING ID IN SPEECH SCRIPT!");
                            this.textToUse2591 = this.self_2592.speechScripts[this.self_2592.curSpeech].text;
                        }
                        this.self_2592.root.speechBubble(this.self_2592.speechScripts[this.self_2592.curSpeech].followTransform, this.self_2592.speechScripts[this.self_2592.curSpeech].followOffset, this.textToUse2591, this.self_2592.root.GetTranslation(this.self_2592.speechScripts[this.self_2592.curSpeech].speakerName), this.self_2592.speechScripts[this.self_2592.curSpeech].clickToContinue, this.self_2592.speechScripts[this.self_2592.curSpeech].clickToContinueDontFreeze, this.self_2592.speechScripts[this.self_2592.curSpeech].speechBubbleTimerMultiplier, this.self_2592.speechScripts[this.self_2592.curSpeech].speechBubbleAppearDelay, this.self_2592.transform, this.self_2592.speechScripts[this.self_2592.curSpeech].targetAnimator);
                        if (this.self_2592.speechScripts[this.self_2592.curSpeech].activateSwitchScript)
                        {
                            this.self_2592.switchScript.output = (float)1;
                        }
                        else
                        {
                            this.self_2592.switchScript.output = (float)-1;
                        }
                        if (this.self_2592.curSpeech > 0 && !this.self_2592.speechScripts[this.self_2592.curSpeech - 1].keepExtraSwitchScriptActivated && this.self_2592.speechScripts[this.self_2592.curSpeech - 1].activateExtraSwitchScript != null)
                        {
                            this.self_2592.speechScripts[this.self_2592.curSpeech - 1].activateExtraSwitchScript.output = (float)0;
                        }
                        if (this.self_2592.speechScripts[this.self_2592.curSpeech].activateExtraSwitchScript != null)
                        {
                            this.self_2592.speechScripts[this.self_2592.curSpeech].activateExtraSwitchScript.output = (float)1;
                        }
                        if (this.self_2592.curSpeech > 0 && this.self_2592.speechScripts[Mathf.Clamp(this.self_2592.curSpeech - 1, 0, this.self_2592.curSpeech)].forceSpawnPedro != null)
                        {
                            this.self_2592.speechScripts[Mathf.Clamp(this.self_2592.curSpeech - 1, 0, this.self_2592.curSpeech)].forceSpawnPedro.forceSpawn = false;
                        }
                        if (this.self_2592.speechScripts[this.self_2592.curSpeech].forceSpawnPedro != null)
                        {
                            this.self_2592.root.currentForceSpawnedPedro = this.self_2592.speechScripts[this.self_2592.curSpeech].forceSpawnPedro.transform;
                            return this.YieldDefault(2);
                        }
                        break;
                    case 1:
                    IL_589:
                        return false;
                    case 2:
                        this.self_2592.speechScripts[this.self_2592.curSpeech].forceSpawnPedro.forceSpawn = true;
                        break;
                }
                if (this.self_2592.disableActionModeIfClickToContinue && this.self_2592.speechScripts[this.self_2592.curSpeech].clickToContinue)
                {
                    this.self_2592.root.actionModeActivated = false;
                    this.self_2592.root.kAction = false;
                }
                this.self_2592.curSpeech = this.self_2592.curSpeech + 1;
                if (this.self_2592.curSpeech >= this.self_2592.speechScripts.Length)
                {
                    this.self_2592.reachedEndOfSpeeches = true;
                    if (!this.self_2592.reTrigger)
                    {
                        this.self_2592.doNotActivateAgain = true;
                    }
                    else
                    {
                        if (this.self_2592.speechScripts[this.self_2592.curSpeech - 1].forceSpawnPedro != null)
                        {
                            this.self_2592.speechScripts[this.self_2592.curSpeech - 1].forceSpawnPedro.forceSpawn = false;
                        }
                        this.self_2592.triggered = false;
                        this.self_2592.curSpeech = 0;
                    }
                }
                else
                {
                    this.self_2592.reachedEndOfSpeeches = false;
                }
                this.YieldDefault(1);


                return false;
            }
        }
    }
}
