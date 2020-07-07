// Decompiled with JetBrains decompiler
// Type: PedroScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using Boo.Lang.Runtime;
using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class PedroScript : MonoBehaviour
{
    [Header("MFP Multiplayer")]
    public bool multiplayerPedro = false; //multiplayer PedroScripts can fly around freely

    public bool forceSpawn;
    public SwitchScript[] inputSwitch;
    private float switchInput;
    private bool showPedroDoOnce;
    private bool haveBeenActivated;
    public float disappearDelayTimer;
    private float disappearDelayTimerToUse;
    public bool dontSetPosition;
    [Header("Follow options")]
    public bool followPlayer;
    public Vector2 followArea;
    public Vector2 followAreaOffset;
    [Header("Visibility options")]
    public bool stayVisible;
    public bool appearEffect;
    public bool disappearEffect;
    [Header("Destroys gameObject after disappearing")]
    public bool doDestroy;
    private Transform mainPlayer;
    private Renderer pRenderer;
    private Light pLight;
    private Vector3 startPos;
    private Vector3 actualStartPos;
    private Quaternion startRot;
    private RootScript root;
    private bool move;
    private Transform glow;
    private Transform[] allAmazeLines;
    private Quaternion[] allAmazeLinesStartRot;
    private ParticleSystem sparkParticle;
    private ParticleSystem flashParticle;
    private ParticleSystem smokeParticle;

    public PedroScript()
    {
        disappearDelayTimer = 60f;
        appearEffect = true;
        disappearEffect = true;
    }

    public virtual void LateUpdate()
    {
        if (!root.doCheckpointLoad || !haveBeenActivated || stayVisible)
            return;
        UnityEngine.Object.Destroy((UnityEngine.Object)gameObject);
    }

    public virtual void Start()
    {
        mainPlayer = GameObject.Find("Player").transform;
        pRenderer = (Renderer)GetComponent(typeof(Renderer));
        pLight = (Light)GetComponent(typeof(Light));
        root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        glow = transform.Find("Pedro_Glow");
        allAmazeLines = glow.GetComponentsInChildren<Transform>();
        allAmazeLinesStartRot = new Quaternion[Extensions.get_length((System.Array)allAmazeLines)];
        for (int index = new int(); index < Extensions.get_length((System.Array)allAmazeLines); ++index)
            allAmazeLinesStartRot[index] = allAmazeLines[index].localRotation;
        sparkParticle = (ParticleSystem)GameObject.Find("Main Camera/SparksParticle").GetComponent(typeof(ParticleSystem));
        flashParticle = (ParticleSystem)GameObject.Find("Main Camera/BulletHitParticle").GetComponent(typeof(ParticleSystem));
        smokeParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeParticle").GetComponent(typeof(ParticleSystem));
        startPos = actualStartPos = transform.position;
        startRot = transform.rotation;
        disappearDelayTimerToUse = 0.0f;
        pRenderer.enabled = pLight.enabled = false;
        glow.gameObject.SetActive(false);
    }

    public virtual void Update()
    {
        Vector3 vector3_1 = new Vector3();
        switchInput = -1f;
        int index1 = 0;
        SwitchScript[] inputSwitch = this.inputSwitch;
        for (int length = inputSwitch.Length; index1 < length; ++index1)
        {
            if ((UnityEngine.Object)inputSwitch[index1] != (UnityEngine.Object)null && (double)inputSwitch[index1].output > (double)switchInput)
                switchInput = inputSwitch[index1].output;
        }
        if (forceSpawn && (UnityEngine.Object)root.currentForceSpawnedPedro == (UnityEngine.Object)transform)
            switchInput = 1f;
        disappearDelayTimerToUse = Mathf.Clamp(disappearDelayTimerToUse - root.timescale, 0.0f, disappearDelayTimerToUse);
        if ((double)switchInput >= 1.0)
            disappearDelayTimerToUse = disappearDelayTimer;
        if ((double)switchInput >= 1.0 || haveBeenActivated && stayVisible || (double)disappearDelayTimerToUse > 0.0 && (!forceSpawn || forceSpawn && (UnityEngine.Object)root.currentForceSpawnedPedro == (UnityEngine.Object)transform))
        {
            if (!showPedroDoOnce)
            {
                haveBeenActivated = true;
                pRenderer.enabled = pLight.enabled = true;
                glow.gameObject.SetActive(true);
                move = true;
                for (int index2 = new int(); index2 < Extensions.get_length((System.Array)allAmazeLines); ++index2)
                {
                    float num1 = 0.1f;
                    Vector3 localScale = allAmazeLines[index2].localScale;
                    double num2 = (double)(localScale.y = num1);
                    Vector3 vector3_2 = allAmazeLines[index2].localScale = localScale;
                }
                glow.localScale = Vector3.one * 5f;
                if (followPlayer)
                {
                    startPos = mainPlayer.position + new Vector3((double)(mainPlayer.position - transform.position).x <= 0.0 ? 2f : -2f, 2f, 0.0f);
                    limitArea();
                }
                if (appearEffect)
                {
                    flashParticle.Emit(root.generateEmitParams(transform.position + new Vector3(0.0f, 0.0f, -1f), Vector3.zero, 4f, 0.1f, new Color(1f, 1f, 1f, 1f)), 1);
                    for (int index2 = 0; index2 < 8; ++index2)
                        sparkParticle.Emit(root.generateEmitParams(transform.position, new Vector3((float)(UnityEngine.Random.Range(4, 8) * ((double)UnityEngine.Random.value >= 0.5 ? 1 : -1)), (float)UnityEngine.Random.Range(0, 8), UnityEngine.Random.Range(-0.5f, 0.5f)), UnityEngine.Random.Range(0.06f, 0.08f), UnityEngine.Random.Range(0.2f, 0.4f), new Color(1f, 1f, 0.5f, 1f)), 1);
                }
                showPedroDoOnce = true;
            }
        }
        else if (showPedroDoOnce)
        {
            haveBeenActivated = false;
            move = false;
            if (disappearEffect)
                doDisappearEffect();
            if (doDestroy)
            {
                UnityEngine.Object.Destroy((UnityEngine.Object)gameObject);
            }
            else
            {
                pRenderer.enabled = pLight.enabled = false;
                glow.gameObject.SetActive(false);
            }
            showPedroDoOnce = false;
        }
        if (!move)
            return;
        Vector3 vector3_3 = mainPlayer.position - transform.position;
        float num3 = Mathf.Clamp(Mathf.Abs(vector3_3.x), 0.0f, 2f);
        if (!dontSetPosition)
        {
            if (followPlayer)
            {
                startPos += Vector3.ClampMagnitude((mainPlayer.position + new Vector3((double)vector3_3.x <= 0.0 ? 2f : -2f, 2f + (float)((2.0 - (double)num3) * 0.300000011920929), 0.0f) - startPos) * Mathf.Clamp01(0.05f * num3 * root.timescale), 0.5f);
                float x = startPos.x;
                Vector3 position = transform.position;
                double num1 = (double)(position.x = x);
                Vector3 vector3_2 = transform.position = position;
                limitArea();
            }
            float num2 = startPos.z - (2f - num3);
            Vector3 position1 = transform.position;
            double num4 = (double)(position1.z = num2);
            Vector3 vector3_4 = transform.position = position1;
            float num5 = startPos.y + Mathf.Sin(Time.time * 1.5f) * 0.4f;
            Vector3 position2 = transform.position;
            double num6 = (double)(position2.y = num5);
            Vector3 vector3_5 = transform.position = position2;
        }
        transform.rotation = startRot * Quaternion.Euler(Mathf.Sin(Time.time * 0.15f) * 10f, (float)(90.0 + ((double)vector3_3.x >= 0.0 ? 90.0 : -90.0) * ((double)num3 / 2.0) + (double)Mathf.Sin(Time.time * 0.3f) * 10.0), (float)((double)Mathf.Sin((float)(0.75 + (double)Time.time * 1.5)) * 10.0 - 5.0));
    }

    public virtual void doDisappearEffect()
    {
        if ((UnityEngine.Object)smokeParticle == (UnityEngine.Object)null)
        {
            root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
            sparkParticle = (ParticleSystem)GameObject.Find("Main Camera/SparksParticle").GetComponent(typeof(ParticleSystem));
            flashParticle = (ParticleSystem)GameObject.Find("Main Camera/BulletHitParticle").GetComponent(typeof(ParticleSystem));
            smokeParticle = (ParticleSystem)GameObject.Find("Main Camera/SmokeParticle").GetComponent(typeof(ParticleSystem));
        }
        smokeParticle.Emit(root.generateEmitParams(transform.position, Vector3.up * UnityEngine.Random.value / 4f, UnityEngine.Random.Range(1.5f, 2.5f), (float)UnityEngine.Random.Range(1, 2), new Color(1f, 1f, 1f, UnityEngine.Random.Range(0.08f, 0.2f))), 1);
        flashParticle.Emit(root.generateEmitParams(transform.position + new Vector3(0.0f, 0.0f, -1f), Vector3.zero, 3f, 0.1f, new Color(1f, 1f, 1f, 1f)), 1);
        for (int index = 0; index < 8; ++index)
            sparkParticle.Emit(root.generateEmitParams(transform.position, new Vector3((float)(UnityEngine.Random.Range(4, 8) * ((double)UnityEngine.Random.value >= 0.5 ? 1 : -1)), (float)UnityEngine.Random.Range(0, 8), UnityEngine.Random.Range(-0.5f, 0.5f)), UnityEngine.Random.Range(0.06f, 0.08f), UnityEngine.Random.Range(0.1f, 0.2f), new Color(1f, 1f, 0.5f, 1f)), 1);
    }

    private void limitArea()
    {
        if (multiplayerPedro)
            return;

        if (RuntimeServices.EqualityOperator((object)followArea, (object)null) && !(followArea != Vector2.zero))
            return;
        startPos.x = Mathf.Clamp(startPos.x, (float)((double)actualStartPos.x + (double)followAreaOffset.x - (double)followArea.x / 2.0), (float)((double)actualStartPos.x + (double)followAreaOffset.x + (double)followArea.x / 2.0));
        startPos.y = Mathf.Clamp(startPos.y, (float)((double)actualStartPos.y + (double)followAreaOffset.y - (double)followArea.y / 2.0), (float)((double)actualStartPos.y + (double)followAreaOffset.y + (double)followArea.y / 2.0));
    }

    public virtual void Main()
    {
    }
}
