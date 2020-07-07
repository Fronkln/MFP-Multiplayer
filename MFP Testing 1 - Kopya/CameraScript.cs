// Decompiled with JetBrains decompiler
// Type: CameraScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using UnityEngine;

[Serializable]
public class CameraScript : MonoBehaviour
{

    public bool isNoclipCam;

    private RootScript root;
    private RootSharedScript rootShared;
    private Vector3 mousePos;
    private Transform player;
    public Transform playerPublic { get { return player; } set { player = value; } } //MFP MULTIPLAYER
    private PlayerScript playerScript;
    private RectTransform mainCursor;
    public float tiltMultiplier;
    public Vector3 trackPos2;
    public float screenShake;
    public float onOffScreenShake;
    public Vector2 appliedScreenShake;
    public float bigScreenShake;
    public Vector2 bigScreenShakeOffset;
    private Vector3 finalTrackPos;
    [HideInInspector]
    public Vector3 camPos;
    private float frameTimer;
    private Camera curCamera;
    public Transform activeCameraZone;
    public Vector3 cameraZonePosOffset;
    private Vector3 cameraZonePos;
    public float cameraZoneBlendTarget;
    public float cameraZoneBlend;
    public float cameraZoneTiltMultiplier;
    public float cameraZoneZoom;
    public float handyAmount;
    public float handySpeed;
    private float handyShake1;
    private float handyShake2;
    private float handyShake3;
    [HideInInspector]
    public Vector2 kickBackReadValue;
    private Vector2 gamepadMouseOffset;
    [HideInInspector]
    public Vector2 externalCameraOffset;
    private Vector2 externalCameraOffsetSmoothed;
    [HideInInspector]
    public bool invertGamepadMouseOffset;
    [HideInInspector]
    public float yClampPos;
    private Vector3 prevFixedPos;
    private Quaternion prevFixedRot;
    private bool updateFakePos;
    public Vector3 fakePos;
    public Quaternion fakeRot;
    private bool cineDoOnce;
    private Transform cineTransform;
    private CinematicCameraScript cineScript;
    private float startFogEndDistance;
    private float startFogStartDistance;
    private Vector3 prevFixedPosS;
    private Quaternion prevFixedRotS;
    private bool updateFakePosS;
    private Vector3 fakePosS;
    private Quaternion fakeRotS;
    private Vector3 mousePosS;
    private float tiltMultiplierS;
    private Vector3 trackPos2S;
    private float screenShakeS;
    private float onOffScreenShakeS;
    private Vector2 appliedScreenShakeS;
    private float bigScreenShakeS;
    private Vector2 bigScreenShakeOffsetS;
    private Vector3 finalTrackPosS;
    private Vector3 camPosS;
    private float frameTimerS;

    public CameraScript()
    {
        this.tiltMultiplier = 1f;
        this.yClampPos = -999f;
    }

    public virtual void saveState()
    {
        this.prevFixedPosS = this.prevFixedPos;
        this.prevFixedRotS = this.prevFixedRot;
        this.updateFakePosS = this.updateFakePos;
        this.fakePosS = this.fakePos;
        this.fakeRotS = this.fakeRot;
        this.mousePosS = this.mousePos;
        this.tiltMultiplierS = this.tiltMultiplier;
        this.trackPos2S = this.trackPos2;
        this.screenShakeS = this.screenShake;
        this.onOffScreenShakeS = this.onOffScreenShake;
        this.appliedScreenShakeS = this.appliedScreenShake;
        this.bigScreenShakeS = this.bigScreenShake;
        this.bigScreenShakeOffsetS = this.bigScreenShakeOffset;
        this.finalTrackPosS = this.finalTrackPos;
        this.camPosS = this.camPos;
        this.frameTimerS = this.frameTimer;
    }

    public virtual void loadState()
    {
        this.prevFixedPos = this.prevFixedPosS;
        this.prevFixedRot = this.prevFixedRotS;
        this.updateFakePos = this.updateFakePosS;
        this.fakePos = this.fakePosS;
        this.fakeRot = this.fakeRotS;
        this.mousePos = this.mousePosS;
        this.tiltMultiplier = this.tiltMultiplierS;
        this.trackPos2 = this.trackPos2S;
        this.screenShake = this.screenShakeS;
        this.onOffScreenShake = this.onOffScreenShakeS;
        this.appliedScreenShake = this.appliedScreenShakeS;
        this.bigScreenShake = this.bigScreenShakeS;
        this.bigScreenShakeOffset = this.bigScreenShakeOffsetS;
        this.finalTrackPos = this.finalTrackPosS;
        this.camPos = this.camPosS;
        this.frameTimer = this.frameTimerS;
        this.yClampPos = -999f;
    }

    public virtual void LateUpdate()
    {
        if (!this.playerScript.gamepad)
        {
            if (this.rootShared.simulateMousePos)
                this.mainCursor.position = (Vector3)this.rootShared.fakeMousePos;
            else
                this.mainCursor.position = Input.mousePosition;
        }
        if (this.root.doCheckpointSave)
            this.saveState();
        if (!this.root.doCheckpointLoad)
            return;
        this.loadState();
    }

    public virtual void kickBack(float amount)
    {
        this.kickBackReadValue = (Vector2)((this.camPos - this.mousePos).normalized * amount);
        this.camPos += (Vector3)(this.kickBackReadValue * this.root.optionsScreenShakeMultiplier);
    }

    public virtual void centerCamPosOnPlayer()
    {
        ref Vector3 local1 = ref this.camPos;
        ref Vector3 local2 = ref this.prevFixedPos;
        ref Vector3 local3 = ref this.fakePos;
        Vector3 position1 = this.player.position;
        double x;
        float num1 = (float)(x = (double)position1.x);
        local3.x = (float)x;
        double num2;
        float num3 = (float)(num2 = (double)num1);
        local2.x = (float)num2;
        float num4 = num3;
        Vector3 position2 = this.transform.position;
        double num5 = (double)(position2.x = num4);
        Vector3 vector3_1 = this.transform.position = position2;
        double num6 = (double)num4;
        local1.x = (float)num6;
        ref Vector3 local4 = ref this.camPos;
        ref Vector3 local5 = ref this.prevFixedPos;
        ref Vector3 local6 = ref this.fakePos;
        Vector3 position3 = this.player.position;
        double y;
        float num7 = (float)(y = (double)position3.y);
        local6.y = (float)y;
        double num8;
        float num9 = (float)(num8 = (double)num7);
        local5.y = (float)num8;
        float num10 = num9;
        Vector3 position4 = this.transform.position;
        double num11 = (double)(position4.y = num10);
        Vector3 vector3_2 = this.transform.position = position4;
        double num12 = (double)num10;
        local4.y = (float)num12;
    }

    public virtual void Start()
    {
        int num1 = (int)(this.fakePos.z = this.prevFixedPos.z = -18f);
        Vector3 position = this.transform.position;
        double num2 = (double)(position.z = (float)num1);
        Vector3 vector3 = this.transform.position = position;
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.rootShared = (RootSharedScript)GameObject.Find("RootShared").GetComponent(typeof(RootSharedScript));
        this.player = GameObject.Find("Player").transform;
        this.playerScript = (PlayerScript)this.player.GetComponent(typeof(PlayerScript));
        this.mainCursor = (RectTransform)GameObject.Find("HUD/Canvas/Cursors/MainCursor").GetComponent(typeof(RectTransform));
        this.camPos = this.transform.position;
        this.curCamera = (Camera)this.GetComponent(typeof(Camera));
        this.curCamera.depthTextureMode = !this.rootShared.lowEndHardware ? DepthTextureMode.Depth : DepthTextureMode.None;
        this.mousePos = this.player.position;
        if (this.rootShared.modCinematicCamera)
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "CinematicCameraObj";
            this.cineScript = (CinematicCameraScript)gameObject.AddComponent(typeof(CinematicCameraScript));
            this.cineTransform = gameObject.transform;
        }
        this.startFogEndDistance = RenderSettings.fogEndDistance;
        this.startFogStartDistance = RenderSettings.fogStartDistance;
        if (!this.rootShared.modSideOnCamera)
            return;
        RenderSettings.fogEndDistance += 50f;
        RenderSettings.fogStartDistance += 50f;
        ((Behaviour)this.GetComponent(typeof(AudioListener))).enabled = false;
        GameObject gameObject1 = new GameObject();
        gameObject1.name = "Audio Listener";
        gameObject1.transform.parent = this.transform;
        gameObject1.transform.localPosition = new Vector3(0.0f, 0.0f, 50f);
        gameObject1.AddComponent(typeof(AudioListener));
    }

    public virtual void ResetTrackPos()
    {
        this.trackPos2 = this.mousePos;
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            this.isNoclipCam = !this.isNoclipCam;
        }
        if (!this.isNoclipCam)
        {
            base.transform.position = Vector3.Lerp(this.prevFixedPos, this.fakePos, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
            base.transform.rotation = Quaternion.Slerp(this.prevFixedRot, this.fakeRot, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
            return;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            base.transform.position = new Vector3(base.transform.position.x - 0.2f, base.transform.position.y, base.transform.position.z);
        }
        if (Input.GetKey(KeyCode.Keypad8))
        {
            base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.2f, base.transform.position.z);
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            base.transform.position = new Vector3(base.transform.position.x + 0.2f, base.transform.position.y, base.transform.position.z);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.2f, base.transform.position.z);
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - 0.2f);
        }
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + 0.2f);
        }



        //   this.transform.position = Vector3.Lerp(this.prevFixedPos, this.fakePos, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
        if (this.rootShared.modCinematicCamera && this.root.kAction)
        {
            if (!this.cineDoOnce)
            {
                Vector3 vector3_1 = new Vector3();
                if (this.playerScript.gamepad && (UnityEngine.Object)this.playerScript.normalAutoAimEnemyForCinematicCamera != (UnityEngine.Object)null)
                    vector3_1 = this.playerScript.normalAutoAimEnemyForCinematicCamera.position;
                if (vector3_1 == Vector3.zero)
                {
                    Vector3 vector3_2 = new Vector3(Mathf.Clamp(this.mousePos.x, this.player.position.x - 15f, this.player.position.x + 15f), Mathf.Clamp(this.mousePos.y, this.player.position.y - 10f, this.player.position.y + 10f), this.mousePos.z);
                    float num1 = 99999f;
                    Transform transform = (Transform)null;
                    int index = 0;
                    Transform[] allEnemies = this.root.allEnemies;
                    for (int length = allEnemies.Length; index < length; ++index)
                    {
                        float num2 = Vector2.Distance((Vector2)vector3_2, (Vector2)allEnemies[index].position);
                        EnemyScript enemyScript = (EnemyScript)null;
                        if ((double)num2 < 8.0)
                        {
                            if ((UnityEngine.Object)enemyScript == (UnityEngine.Object)null)
                                enemyScript = (EnemyScript)allEnemies[index].GetComponent(typeof(EnemyScript));
                            if (enemyScript.enabled && (double)num2 < (double)num1)
                            {
                                num1 = num2;
                                transform = allEnemies[index];
                            }
                        }
                    }
                    vector3_1 = !((UnityEngine.Object)transform != (UnityEngine.Object)null) ? vector3_2 : transform.position;
                }
                this.cineTransform.position = vector3_1 + Vector3.forward * ((float)((double)Mathf.Clamp(Vector2.Distance((Vector2)vector3_1, (Vector2)this.player.position), 0.0f, 20f) / 20.0 * -6.0) - 8f);
                this.cineScript.startPos = this.cineTransform.position;
                this.cineScript.lookAtPlayerInverseSmooth = 0.04f;
                this.cineScript.lookAtPlayerOffset = new Vector2(Mathf.Clamp(vector3_1.x - this.player.position.x, -5f, 5f), Mathf.Clamp((float)(((double)vector3_1.y - (double)this.player.position.y) * 0.800000011920929), -3f, 3f));
                this.cineScript.lookAtAngleClamp = new Vector2(5f, 5f);
                this.cineScript.handyAmount = 0.8f;
                this.cineScript.handySpeed = 1.5f;
                this.cineScript.tiltBasedOnPlayerXPosMultiplier = 0.9f;
                this.cineScript.tiltSmoothing = 0.1f;
                this.cineScript.followPlayerMultiplier = new Vector2(-0.4f, -0.3f);
                this.cineScript.followPlayerSmoothing = 0.2f;
                this.cineScript.doRotation(true);
                if (this.rootShared.modSideOnCamera)
                {
                    this.curCamera.fieldOfView = 60f;
                    RenderSettings.fogEndDistance = this.startFogEndDistance;
                    RenderSettings.fogStartDistance = this.startFogStartDistance;
                }
                this.cineDoOnce = true;
            }
            this.transform.position = this.cineTransform.position;
            this.transform.rotation = this.cineTransform.rotation;
        }
        else if (this.rootShared.modSideOnCamera)
        {
            if (this.playerScript.onMotorcycle)
            {
                this.transform.rotation = Quaternion.Euler(20f, 0.0f, 0.0f);
                float num1 = this.transform.position.y + 25f;
                Vector3 position = this.transform.position;
                double num2 = (double)(position.y = num1);
                Vector3 vector3 = this.transform.position = position;
            }
            else
                this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            this.curCamera.fieldOfView = 20f;
            float num3 = this.transform.position.z - 50f;
            Vector3 position1 = this.transform.position;
            double num4 = (double)(position1.z = num3);
            Vector3 vector3_1 = this.transform.position = position1;
        }
        //   else
        //this.transform.rotation = Quaternion.Slerp(this.prevFixedRot, this.fakeRot, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
        if ((!this.rootShared.modCinematicCamera || !this.root.kAction) && this.cineDoOnce)
        {
            if (this.rootShared.modSideOnCamera)
            {
                RenderSettings.fogEndDistance = this.startFogEndDistance + 50f;
                RenderSettings.fogStartDistance = this.startFogStartDistance + 50f;
            }
            this.cineDoOnce = false;
        }
        if (this.playerScript.gamepad)
            return;
        if (this.rootShared.simulateMousePos)
            this.mainCursor.position = (Vector3)this.rootShared.fakeMousePos;
        else
            this.mainCursor.position = Input.mousePosition;
    }

    public virtual void FixedUpdate()
    {
        this.prevFixedPos = this.fakePos;
        this.prevFixedRot = this.fakeRot;
        this.externalCameraOffsetSmoothed = this.root.DampV2Fixed(this.externalCameraOffset, this.externalCameraOffsetSmoothed, 0.05f);
        Vector3 position = this.player.position;
        if ((double)this.yClampPos != -999.0 && (double)position.y < (double)this.yClampPos)
            position.y = this.yClampPos;
        if (this.root.dead)
        {
            this.screenShake = 0.0f;
            this.onOffScreenShake = 0.0f;
            this.appliedScreenShake = Vector2.zero;
            this.bigScreenShake = 0.0f;
            this.bigScreenShakeOffset = Vector2.zero;
        }
        if (!this.playerScript.kChangeWeapon)
        {
            if (this.root.dead)
            {
                this.mousePos = !this.playerScript.gamepad ? (!this.rootShared.simulateMousePos ? this.curCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, position.z - this.curCamera.transform.position.z)) : this.curCamera.ScreenToWorldPoint(new Vector3(this.rootShared.fakeMousePos.x, this.rootShared.fakeMousePos.y, position.z - this.curCamera.transform.position.z))) : this.playerScript.mousePos;
            }
            else
            {
                if (this.root.sbClickCont || this.playerScript.overrideControls || !this.playerScript.gamepad)
                {
                    this.mousePos = this.playerScript.mousePos;
                }
                else
                {
                    if ((double)Mathf.Abs(this.playerScript.kXDir) > 0.800000011920929)
                        this.gamepadMouseOffset += (Vector2)((new Vector3((float)(((double)this.playerScript.kXDir * 2.0 + (double)this.playerScript.xSpeed * 0.800000011920929) * (!this.invertGamepadMouseOffset ? 1.0 : -1.0)), 0.0f, 0.0f) - (Vector3)this.gamepadMouseOffset) * 0.1f);
                    this.mousePos += (this.playerScript.mousePos + new Vector3((float)(((double)this.playerScript.mousePos.x - (double)position.x) * 0.800000011920929), 0.0f, 0.0f) + (Vector3)this.gamepadMouseOffset + (Vector3)this.externalCameraOffsetSmoothed - this.mousePos) * 0.025f;
                }
                this.fakePos.z = !this.root.kAction ? this.root.DampFixed((float)((!((UnityEngine.Object)this.activeCameraZone != (UnityEngine.Object)null) ? 0.0 : (double)this.cameraZoneZoom) - 18.0), this.fakePos.z, 0.02f) : Mathf.Clamp(this.fakePos.z + 0.05f * this.root.fixedTimescale, -18f, -15.5f);
                if (this.playerScript.weapon != 9 || !this.playerScript.kSecondaryAim)
                    ;
            }
        }
        if (this.playerScript.onMotorcycle)
            this.trackPos2 = new Vector3(-position.x, 0.0f, 0.0f) * 0.5f;
        if ((UnityEngine.Object)this.activeCameraZone != (UnityEngine.Object)null)
        {
            this.cameraZonePos = this.activeCameraZone.position + this.cameraZonePosOffset;
            this.cameraZoneBlend += Mathf.Clamp(this.root.DampAddFixed(this.cameraZoneBlendTarget, this.cameraZoneBlend, 0.05f), -0.005f, 0.005f);
            this.tiltMultiplier += Mathf.Clamp(this.root.DampAddFixed(this.cameraZoneTiltMultiplier, this.tiltMultiplier, 0.1f), -0.005f, 0.005f);
        }
        else
        {
            this.cameraZoneBlend += Mathf.Clamp(this.root.DampAddFixed(0.0f, this.cameraZoneBlend, 0.05f), -0.005f, 0.005f);
            this.tiltMultiplier += Mathf.Clamp(this.root.DampAddFixed(1f, this.tiltMultiplier, 0.1f), -0.005f, 0.005f);
            this.handyAmount = this.root.DampFixed((float)(0.200000002980232 + 0.25 * (1.0 - (double)this.playerScript.health)), this.handyAmount, 0.05f);
            this.handySpeed = this.root.DampFixed(1f + (float)(0.25 * (1.0 - (double)this.playerScript.health)), this.handySpeed, 0.05f);
        }
        this.finalTrackPos = position + ((this.mousePos - position) / 2f + (this.trackPos2 - position) / 2f) / 4.5f + (this.cameraZonePos - position) / 2f * this.cameraZoneBlend;
        this.finalTrackPos.x = Mathf.Clamp(this.finalTrackPos.x, position.x - 2.9f, position.x + 2.9f);
        this.finalTrackPos.y = Mathf.Clamp(this.finalTrackPos.y, position.y - 2f, position.y + 2f);
        if ((double)this.yClampPos != -999.0 && (double)this.finalTrackPos.y < (double)this.yClampPos)
            this.finalTrackPos.y = this.yClampPos;
        if (this.playerScript.onMotorcycle)
        {
            this.finalTrackPos.y += 7f;
            this.tiltMultiplier = 0.6f;
            this.fakePos.z = -19f;
        }
        if (this.rootShared.modSideOnCamera && !this.playerScript.kChangeWeapon && (!this.root.dead && this.playerScript.weapon == 9) && this.playerScript.kSecondaryAim)
            this.finalTrackPos += (Vector3)(Vector2)((this.mousePos - position).normalized * 10f);
        this.camPos.x = this.root.DampUnscaledFixed(this.finalTrackPos.x, this.camPos.x, 0.1f);
        this.camPos.y = this.root.DampUnscaledFixed(this.finalTrackPos.y, this.camPos.y, 0.1f);
        this.kickBackReadValue += (Vector2.zero - this.kickBackReadValue) * 0.1f;
        if ((double)this.screenShake > 0.150000005960464)
        {
            this.screenShake += (float)((0.0 - (double)this.screenShake) * 0.980000019073486);
            if ((double)this.screenShake < 0.150000005960464)
                this.screenShake = 0.15f;
        }
        this.appliedScreenShake.x = UnityEngine.Random.Range(-this.screenShake, this.screenShake);
        this.appliedScreenShake.y = UnityEngine.Random.Range(-this.screenShake, this.screenShake);
        if ((double)this.appliedScreenShake.x < 0.0 && (double)this.appliedScreenShake.x > (double)this.screenShake / 1.5)
            this.appliedScreenShake.x = this.screenShake / 1.5f;
        else if ((double)this.appliedScreenShake.x > 0.0 && (double)this.appliedScreenShake.x < (double)this.screenShake / 1.5)
            this.appliedScreenShake.x = this.screenShake / 1.5f;
        if ((double)this.appliedScreenShake.y < 0.0 && (double)this.appliedScreenShake.y > (double)this.screenShake / 1.5)
            this.appliedScreenShake.y = this.screenShake / 1.5f;
        else if ((double)this.appliedScreenShake.y > 0.0 && (double)this.appliedScreenShake.y < (double)this.screenShake / 1.5)
            this.appliedScreenShake.y = this.screenShake / 1.5f;
        this.fakePos.x = this.camPos.x + this.appliedScreenShake.x * this.root.optionsScreenShakeMultiplier + UnityEngine.Random.Range(-this.onOffScreenShake * this.root.optionsScreenShakeMultiplier, this.onOffScreenShake * this.root.optionsScreenShakeMultiplier);
        this.fakePos.y = this.camPos.y + this.appliedScreenShake.y * this.root.optionsScreenShakeMultiplier + UnityEngine.Random.Range(-this.onOffScreenShake * this.root.optionsScreenShakeMultiplier, this.onOffScreenShake * this.root.optionsScreenShakeMultiplier);
        this.onOffScreenShake = 0.0f;
        if (!this.playerScript.kChangeWeapon)
        {
            this.handyShake1 += (float)(0.5 * (double)this.handySpeed * (double)this.root.fixedTimescale / 60.0);
            this.handyShake2 += (float)(1.5 * (double)this.handySpeed * (double)this.root.fixedTimescale / 60.0);
            this.handyShake3 += (float)(0.649999976158142 * (double)this.handySpeed * (double)this.root.fixedTimescale / 60.0);
            this.fakeRot = Quaternion.Slerp(this.fakeRot, Quaternion.Euler((float)((double)Mathf.Clamp(position.y - this.fakePos.y, -25f, 25f) * 3.0 * (double)this.tiltMultiplier + (double)Mathf.Sin(this.handyShake1) * (double)this.handyAmount + (double)Mathf.Sin(this.handyShake2) * ((double)this.handyAmount / 3.0) + (!this.playerScript.onMotorcycle ? 0.0 : 30.0)), (float)(-(double)Mathf.Clamp((float)((double)position.x - (double)this.fakePos.x + (double)this.externalCameraOffsetSmoothed.x * -0.200000002980232), -25f, 25f) * 4.5 * (double)this.tiltMultiplier + (double)Mathf.Cos(this.handyShake3) * (double)this.handyAmount), 0.0f), 0.1f);
        }
        this.screenShake -= 0.005f;
        if ((double)this.screenShake < 0.0)
            this.screenShake = 0.0f;
        this.frameTimer += this.root.fixedTimescale;
        if ((double)this.bigScreenShake > 0.0)
        {
            if ((double)this.frameTimer > 1.0)
            {
                this.bigScreenShakeOffset.x = UnityEngine.Random.Range(this.bigScreenShake / 2f, this.bigScreenShake / 1.5f) * ((double)UnityEngine.Random.value >= 0.5 ? 1f : -1f);
                this.bigScreenShakeOffset.y = UnityEngine.Random.Range(this.bigScreenShake / 2f, this.bigScreenShake / 1.5f) * ((double)UnityEngine.Random.value >= 0.5 ? 1f : -1f);
            }
            this.bigScreenShake -= this.root.fixedTimescale * 0.012f;
            this.fakePos.x += this.bigScreenShakeOffset.x * this.root.optionsScreenShakeMultiplier;
            this.fakePos.y += this.bigScreenShakeOffset.y * this.root.optionsScreenShakeMultiplier;
            this.bigScreenShakeOffset = this.root.DampV2Fixed(Vector2.zero, this.bigScreenShakeOffset, 0.4f);
        }
        if ((double)this.frameTimer > 1.0)
            this.frameTimer = 0.0f;
        this.invertGamepadMouseOffset = false;
        this.externalCameraOffset = Vector2.zero;
    }
}
