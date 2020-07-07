// Decompiled with JetBrains decompiler
// Type: OptimizerScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript - Kopya (5).dll

using System;
using UnityEngine;

[Serializable]
public class OptimizerScript : MonoBehaviour
{
    private RootScript root;
    private Transform thePlayer;
    public float checkRange;
    public float deactivationTimer;
    [NonSerialized]
    public static GameObject megaOptimizer;
    private SaveStateControllerScript saveScript;
    private GameObject theGameObject;
    private Renderer theRenderer;
    private bool theGameObjectPrevActiveState;
    private float disableTimer;
    private bool disableDoOnce;
    private bool runLogic;
    private bool theGameObjectPrevActiveStateS;
    private float disableTimerS;
    private bool disableDoOnceS;
    private bool theGameObjectActiveS;
    private bool hasSaved;

    public OptimizerScript()
    {
        this.checkRange = 40f;
        this.deactivationTimer = 120f;
    }

    public virtual void saveState()
    {
        this.theGameObjectPrevActiveStateS = this.theGameObjectPrevActiveState;
        this.disableTimerS = this.disableTimer;
        this.disableDoOnceS = this.disableDoOnce;
        this.theGameObjectActiveS = this.theGameObject.activeSelf;
        this.hasSaved = true;
    }

    public virtual void loadState()
    {
        if ((UnityEngine.Object)this.theGameObject == (UnityEngine.Object)null)
        {
            UnityEngine.Object.Destroy((UnityEngine.Object)this);
        }
        else
        {
            this.theGameObjectPrevActiveState = this.theGameObjectPrevActiveStateS;
            this.disableTimer = this.disableTimerS;
            this.disableDoOnce = this.disableDoOnceS;
            this.theGameObject.SetActive(this.theGameObjectActiveS);
        }
    }

    public virtual void LateUpdate()
    {
        if (this.root.doCheckpointSave)
            this.saveState();
        if (!this.root.doCheckpointLoad)
            return;
        this.loadState();
    }

    public virtual void Awake()
    {
        if (!((UnityEngine.Object)OptimizerScript.megaOptimizer == (UnityEngine.Object)null))
            return;
        OptimizerScript.megaOptimizer = new GameObject();
        OptimizerScript.megaOptimizer.name = "MegaOptimizer";
    }

    public virtual void Start()
    {
        if ((UnityEngine.Object)this.gameObject != (UnityEngine.Object)OptimizerScript.megaOptimizer)
        {
            OptimizerScript optimizerScript = (OptimizerScript)OptimizerScript.megaOptimizer.AddComponent(typeof(OptimizerScript));
            optimizerScript.theGameObject = this.gameObject;
            optimizerScript.checkRange = this.checkRange;
            optimizerScript.deactivationTimer = this.deactivationTimer;
            UnityEngine.Object.Destroy((UnityEngine.Object)this);
        }
        else
        {
            this.root = RootScript.RootScriptInstance;
            this.thePlayer = PlayerScript.PlayerInstance.transform;
            this.saveScript = (SaveStateControllerScript)this.theGameObject.GetComponent(typeof(SaveStateControllerScript));
            if ((UnityEngine.Object)this.saveScript != (UnityEngine.Object)null && this.saveScript.createWrapper)
                this.saveScript = this.saveScript.wrapperSaveScript;
            this.theRenderer = (Renderer)this.theGameObject.GetComponent(typeof(Renderer));
            this.runLogic = true;
        }
    }

    public virtual void Update()
    {
        /*
        if (this.root.doCheckpointLoad)
        {
            this.theGameObject.SetActive(true);
        }
        else
        {
            if (!this.runLogic)
                return;
            if ((UnityEngine.Object)this.saveScript == (UnityEngine.Object)null && !this.hasSaved || (UnityEngine.Object)this.saveScript != (UnityEngine.Object)null && (!this.saveScript.objStateSaved || this.saveScript.isDirty) || ((UnityEngine.Object)this.theRenderer != (UnityEngine.Object)null && this.theRenderer.isVisible || (double)Vector2.Distance((Vector2)this.thePlayer.position, (Vector2)this.theGameObject.transform.position) < (double)this.checkRange))
                this.disableTimer = this.deactivationTimer;
            if ((double)this.disableTimer > 0.0)
            {
                this.disableTimer -= this.root.timescale;
                if (!this.disableDoOnce)
                    return;
                this.theGameObject.SetActive(this.theGameObjectPrevActiveState);
                this.disableDoOnce = false;
            }
            else
            {
                if (this.disableDoOnce)
                    return;
                this.theGameObjectPrevActiveState = this.theGameObject.activeSelf;
                this.theGameObject.SetActive(false);
                this.disableDoOnce = true;
            }
        }
        */
    }
}
