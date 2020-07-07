// Decompiled with JetBrains decompiler
// Type: LaserDetectorScript
// Assembly: Assembly-UnityScript, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C6D02802-CAF3-4F5F-B720-0E01D2380B81
// Assembly location: F:\steamapps2\steamapps\common\My Friend Pedro\My Friend Pedro - Blood Bullets Bananas_Data\Managed\Assembly-UnityScript.dll

using System;
using UnityEngine;

[Serializable]
public class LaserDetectorScript : MonoBehaviour
{

    private BaseNetworkEntity networkHelper;

    private RootScript root;
    public bool turnOffWhenTriggered;
    public bool invertLight;
    public bool onlyDetectPlayer;
    private bool beenTriggered;
    private Transform laser;
    private Transform detectLight;
    private float laserStartLength;
    private RaycastHit ray;
    private SwitchScript switchScript;
    private LayerMask layerMask;
    private Transform mainPlayerUpperBack;
    public bool adjustRaycastZToPlayer;
    public float turnOffDelay;
    private float startTurnOffDelay;
    private bool turnOff;
    private bool beenTriggeredS;
    private float turnOffDelayS;
    private float startTurnOffDelayS;
    private bool turnOffS;

    public LaserDetectorScript()
    {
        this.adjustRaycastZToPlayer = true;
    }

    public void OnLaserTriggered()
    {
        this.switchScript.output = 1f;
        this.turnOff = false;
        this.turnOffDelay = this.startTurnOffDelay;
        if (this.turnOffWhenTriggered)
        {
            this.root.hasTriggeredAlarm = true;
            this.beenTriggered = true;
            this.laser.gameObject.SetActive(false);
        }
    }

    public virtual void Start()
    {
        this.root = (RootScript)GameObject.Find("Root").GetComponent(typeof(RootScript));
        this.layerMask = (LayerMask)71937;
        this.laser = this.transform.Find("LaserDetectorLaser");
        this.detectLight = this.transform.Find("LaserDetectorLight");
        if (this.onlyDetectPlayer)
            this.laserStartLength = 50f;
        else if (Physics.Raycast(this.laser.position, -this.transform.right, out this.ray, 50f, (int)this.layerMask))
        {
            this.laserStartLength = this.ray.distance - 0.01f;
            float laserStartLength = this.laserStartLength;
            Vector3 localScale = this.laser.localScale;
            double num = (double)(localScale.x = laserStartLength);
            Vector3 vector3 = this.laser.localScale = localScale;
        }
        else
            this.laserStartLength = this.laser.localScale.x;
        if ((UnityEngine.Object)this.transform.parent != (UnityEngine.Object)null && this.transform.parent.tag != "Group")
        {
            this.switchScript = (SwitchScript)this.transform.parent.GetComponent(typeof(SwitchScript));
            this.turnOffWhenTriggered = true;
        }
        if ((UnityEngine.Object)this.switchScript == (UnityEngine.Object)null)
            this.switchScript = (SwitchScript)this.transform.GetComponent(typeof(SwitchScript));
        this.mainPlayerUpperBack = GameObject.Find("Player/PlayerGraphics/Armature/Center/LowerBack/UpperBack/Neck").transform;
        this.startTurnOffDelay = this.turnOffDelay;

    }

    public virtual void Update()
    {
        if ((double)this.switchScript.output == 1.0 && this.turnOffWhenTriggered && !this.beenTriggered)
        {
            int num1 = !this.invertLight ? 0 : 180;
            Quaternion localRotation = this.detectLight.localRotation;
            Vector3 eulerAngles = localRotation.eulerAngles;
            double num2 = (double)(eulerAngles.y = (float)num1);
            Vector3 vector3 = localRotation.eulerAngles = eulerAngles;
            Quaternion quaternion = this.detectLight.localRotation = localRotation;
            //  this.beenTriggered = true;
            // this.laser.gameObject.SetActive(false);
            PacketSender.BaseNetworkedEntityRPC("OnLaserTriggered", networkHelper.entityIdentifier);
        }
        if (this.turnOffWhenTriggered && this.turnOffWhenTriggered && this.beenTriggered)
            return;
        if ((double)Vector2.Distance((Vector2)this.transform.position, (Vector2)this.mainPlayerUpperBack.position) < 40.0)
        {
            if (Physics.Raycast(this.laser.position + (!this.adjustRaycastZToPlayer ? Vector3.zero : new Vector3(0.0f, 0.0f, Mathf.Clamp(this.mainPlayerUpperBack.position.z - this.laser.position.z, -1f, 1f))), -this.transform.right, out this.ray, this.laserStartLength, (int)this.layerMask))
            {
                if ((this.onlyDetectPlayer && this.ray.transform.tag == "Player" || !this.onlyDetectPlayer) && this.ray.transform.tag != "Bullet")
                {
                    int num1 = !this.invertLight ? 0 : 180;
                    Quaternion localRotation = this.detectLight.localRotation;
                    Vector3 eulerAngles = localRotation.eulerAngles;
                    double num2 = (double)(eulerAngles.y = (float)num1);
                    Vector3 vector3 = localRotation.eulerAngles = eulerAngles;
                    Quaternion quaternion = this.detectLight.localRotation = localRotation;
                    this.switchScript.output = 1f;
                    this.turnOff = false;
                    this.turnOffDelay = this.startTurnOffDelay;
                    if (this.turnOffWhenTriggered)
                    {
                        this.root.hasTriggeredAlarm = true;
                        PacketSender.BaseNetworkedEntityRPC("OnLaserTriggered", networkHelper.entityIdentifier);
                        //    this.beenTriggered = true;
                        //  this.laser.gameObject.SetActive(false);
                    }
                }
                else if (this.onlyDetectPlayer)
                {
                    int num1 = !this.invertLight ? 180 : 0;
                    Quaternion localRotation = this.detectLight.localRotation;
                    Vector3 eulerAngles = localRotation.eulerAngles;
                    double num2 = (double)(eulerAngles.y = (float)num1);
                    Vector3 vector3 = localRotation.eulerAngles = eulerAngles;
                    Quaternion quaternion = this.detectLight.localRotation = localRotation;
                    this.switchScript.output = -1f;
                }
                float distance = this.ray.distance;
                Vector3 localScale = this.laser.localScale;
                double num = (double)(localScale.x = distance);
                Vector3 vector3_1 = this.laser.localScale = localScale;
            }
            else
                this.turnOff = true;
        }
        if (!this.turnOff)
            return;
        this.turnOffDelay = Mathf.Clamp(this.turnOffDelay - this.root.timescale, 0.0f, this.startTurnOffDelay);
        if ((double)this.turnOffDelay > 0.0)
            return;
        int num3 = !this.invertLight ? 180 : 0;
        Quaternion localRotation1 = this.detectLight.localRotation;
        Vector3 eulerAngles1 = localRotation1.eulerAngles;
        double num4 = (double)(eulerAngles1.y = (float)num3);
        Vector3 vector3_2 = localRotation1.eulerAngles = eulerAngles1;
        Quaternion quaternion1 = this.detectLight.localRotation = localRotation1;
        this.switchScript.output = -1f;
        float laserStartLength = this.laserStartLength;
        Vector3 localScale1 = this.laser.localScale;
        double num5 = (double)(localScale1.x = laserStartLength);
        Vector3 vector3_3 = this.laser.localScale = localScale1;
    }
}
