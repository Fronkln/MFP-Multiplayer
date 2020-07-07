using System;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x02000048 RID: 72
[Serializable]
public class EnemyOptimizerScript : MonoBehaviour
{
    // Token: 0x0600019D RID: 413 RVA: 0x00002C87 File Offset: 0x00000E87
    public EnemyOptimizerScript()
    {
        this.checkDistance = (float)40;
    }

    // Token: 0x0600019E RID: 414 RVA: 0x0001E3A0 File Offset: 0x0001C5A0
    public virtual void saveState()
    {
        this.dontRunCodeS = this.dontRunCode;
        this.checkDistanceS = this.checkDistance;
        this.onlyFreezeOffScreenS = this.onlyFreezeOffScreen;
        this.playerDistanceS = this.playerDistance;
        this.inRangeDoOnceS = this.inRangeDoOnce;
        int i = 0;
        this.physicsSoundsScriptsInChildrenS = new PhysicsSoundsScript[Extensions.get_length(this.physicsSoundsScriptsInChildren)];
        for (i = 0; i < Extensions.get_length(this.physicsSoundsScriptsInChildrenS); i++)
        {
            this.physicsSoundsScriptsInChildrenS[i] = this.physicsSoundsScriptsInChildren[i];
        }
        if (!this.enemyScript.dontCheckDistanceBeforeRunningLogic)
        {
            this.rigidBodiesInChildrenS = new Rigidbody[Extensions.get_length(this.rigidBodiesInChildren)];
            for (i = 0; i < Extensions.get_length(this.rigidBodiesInChildrenS); i++)
            {
                this.rigidBodiesInChildrenS[i] = this.rigidBodiesInChildren[i];
            }
            this.rigidBodiesInChildrenKinematicStateS = new bool[Extensions.get_length(this.rigidBodiesInChildrenKinematicState)];
            for (i = 0; i < Extensions.get_length(this.rigidBodiesInChildrenKinematicStateS); i++)
            {
                this.rigidBodiesInChildrenKinematicStateS[i] = this.rigidBodiesInChildrenKinematicState[i];
            }
            if (!RuntimeServices.EqualityOperator(this.rigidBodySlowMotionInChildren, null))
            {
                this.rigidBodySlowMotionInChildrenS = new RigidBodySlowMotion[Extensions.get_length(this.rigidBodySlowMotionInChildren)];
                for (i = 0; i < Extensions.get_length(this.rigidBodySlowMotionInChildrenS); i++)
                {
                    this.rigidBodySlowMotionInChildrenS[i] = this.rigidBodySlowMotionInChildren[i];
                }
            }
            if (!RuntimeServices.EqualityOperator(this.rigidBodySlowMotionInChildrenEnabledState, null))
            {
                this.rigidBodySlowMotionInChildrenEnabledStateS = new bool[Extensions.get_length(this.rigidBodySlowMotionInChildrenEnabledState)];
                for (i = 0; i < Extensions.get_length(this.rigidBodySlowMotionInChildrenEnabledStateS); i++)
                {
                    this.rigidBodySlowMotionInChildrenEnabledStateS[i] = this.rigidBodySlowMotionInChildrenEnabledState[i];
                }
            }
        }
        this.frameCounterS = this.frameCounter;
        this.doStaticTimerS = this.doStaticTimer;
        this.staticTimerS = this.staticTimer;
        this.allowPutToSleepS = this.allowPutToSleep;
        this.disableArmatureTimerS = this.disableArmatureTimer;
        this.dontDoThingSafetyFramesS = this.dontDoThingSafetyFrames;
    }

    // Token: 0x0600019F RID: 415 RVA: 0x0001E5DC File Offset: 0x0001C7DC
    public virtual void loadState()
    {
        this.dontRunCode = this.dontRunCodeS;
        this.checkDistance = this.checkDistanceS;
        this.onlyFreezeOffScreen = this.onlyFreezeOffScreenS;
        this.playerDistance = this.playerDistanceS;
        this.inRangeDoOnce = this.inRangeDoOnceS;
        int i = 0;
        for (i = 0; i < Extensions.get_length(this.physicsSoundsScriptsInChildren); i++)
        {
            this.physicsSoundsScriptsInChildren[i] = this.physicsSoundsScriptsInChildrenS[i];
        }
        if (!this.enemyScript.dontCheckDistanceBeforeRunningLogic)
        {
            for (i = 0; i < Extensions.get_length(this.rigidBodiesInChildren); i++)
            {
                this.rigidBodiesInChildren[i] = this.rigidBodiesInChildrenS[i];
            }
            for (i = 0; i < Extensions.get_length(this.rigidBodiesInChildrenKinematicState); i++)
            {
                this.rigidBodiesInChildrenKinematicState[i] = this.rigidBodiesInChildrenKinematicStateS[i];
            }
            if (!RuntimeServices.EqualityOperator(this.rigidBodySlowMotionInChildren, null) && !RuntimeServices.EqualityOperator(this.rigidBodySlowMotionInChildrenS, null))
            {
                for (i = 0; i < Extensions.get_length(this.rigidBodySlowMotionInChildren); i++)
                {
                    this.rigidBodySlowMotionInChildren[i] = this.rigidBodySlowMotionInChildrenS[i];
                }
            }
            if (!RuntimeServices.EqualityOperator(this.rigidBodySlowMotionInChildrenEnabledState, null) && !RuntimeServices.EqualityOperator(this.rigidBodySlowMotionInChildrenEnabledStateS, null))
            {
                for (i = 0; i < Extensions.get_length(this.rigidBodySlowMotionInChildrenEnabledState); i++)
                {
                    this.rigidBodySlowMotionInChildrenEnabledState[i] = this.rigidBodySlowMotionInChildrenEnabledStateS[i];
                }
            }
        }
        this.frameCounter = this.frameCounterS;
        this.doStaticTimer = this.doStaticTimerS;
        this.staticTimer = this.staticTimerS;
        this.allowPutToSleep = this.allowPutToSleepS;
        this.disableArmatureTimer = this.disableArmatureTimerS;
        this.dontDoThingSafetyFrames = this.dontDoThingSafetyFramesS;
    }
    // Token: 0x060001A1 RID: 417 RVA: 0x0001E7CC File Offset: 0x0001C9CC
    public virtual void Start()
    {
        dontRunCode = true;

        this.root = RootScript.RootScriptInstance;
        this.centerRigidBody = (Rigidbody)this.transform.Find("EnemyGraphics/Armature/Center").GetComponent(typeof(Rigidbody));
        this.headRigidBody = (Rigidbody)this.transform.Find("EnemyGraphics/Armature/Center/LowerBack/UpperBack/Neck/Head").GetComponent(typeof(Rigidbody));
        this.enemyMeshRenderer = (SkinnedMeshRenderer)this.transform.Find("EnemyGraphics/TorsoWhiteTankTop").GetComponent(typeof(SkinnedMeshRenderer));
        this.theArmature = this.transform.Find("EnemyGraphics/Armature").gameObject;
        this.theAnimator = (Animator)this.transform.Find("EnemyGraphics").GetComponent(typeof(Animator));
        this.mainPlayer = GameObject.Find("Player").transform;
        this.enemyScript = (EnemyScript)this.GetComponent(typeof(EnemyScript));
        this.physicsSoundsScriptsInChildren = this.GetComponentsInChildren<PhysicsSoundsScript>();
        if (!this.enemyScript.dontCheckDistanceBeforeRunningLogic)
        {
            this.rigidBodiesInChildren = this.GetComponentsInChildren<Rigidbody>();
            this.rigidBodiesInChildrenKinematicState = new bool[Extensions.get_length(this.rigidBodiesInChildren)];
            for (int i = 0; i < Extensions.get_length(this.rigidBodiesInChildren); i++)
            {
                this.rigidBodiesInChildrenKinematicState[i] = this.rigidBodiesInChildren[i].isKinematic;
            }
        }
        this.enemyGameCollision = (CapsuleCollider)this.GetComponent(typeof(CapsuleCollider));
        this.playerDistance = Vector2.Distance(this.transform.position, this.mainPlayer.position);
        if (this.enemyScript.motorcycle == null && !this.enemyScript.skyfall)
        {
            if (this.playerDistance < this.checkDistance)
            {
                this.onInRange();
            }
            else
            {
                this.onOutOfRange();
            }
        }
        else
        {
            //this.dontRunCode = false;
        }
        if (!this.dontRunCode)
        {
            this.saveStateControllerScript = (SaveStateControllerScript)this.GetComponent(typeof(SaveStateControllerScript));
            this.saveStateControllerScript.requireDirtyBeforeSaving = true;
            this.saveStateControllerScript.isDirty = true;
        }
    }

    // Token: 0x060001A2 RID: 418 RVA: 0x00002CC6 File Offset: 0x00000EC6
    public virtual void getRigidBodySlowMotionComponents()
    {
        if (!this.enemyScript.dontCheckDistanceBeforeRunningLogic)
        {
            this.rigidBodySlowMotionInChildren = this.GetComponentsInChildren<RigidBodySlowMotion>();
            this.rigidBodySlowMotionInChildrenEnabledState = new bool[Extensions.get_length(this.rigidBodySlowMotionInChildren)];
        }
    }

    // Token: 0x060001A3 RID: 419 RVA: 0x0001EA30 File Offset: 0x0001CC30
    public virtual void onInRange()
    {
        int i = 0;
        PhysicsSoundsScript[] array = this.physicsSoundsScriptsInChildren;
        int length = array.Length;
        while (i < length)
        {
            array[i].enabled = true;
            i++;
        }
        if (!this.enemyScript.simpleEnemy && !this.enemyScript.dontCheckDistanceBeforeRunningLogic)
        {
            for (int j = 0; j < Extensions.get_length(this.rigidBodiesInChildren); j++)
            {
                if (this.rigidBodiesInChildren[j] != null)
                {
                    this.rigidBodiesInChildren[j].isKinematic = this.rigidBodiesInChildrenKinematicState[j];
                }
            }
            if (!RuntimeServices.EqualityOperator(this.rigidBodySlowMotionInChildren, null))
            {
                for (int k = 0; k < Extensions.get_length(this.rigidBodySlowMotionInChildren); k++)
                {
                    if (this.rigidBodySlowMotionInChildren[k] != null)
                    {
                        this.rigidBodySlowMotionInChildren[k].enabled = this.rigidBodySlowMotionInChildrenEnabledState[k];
                    }
                }
            }
        }
        this.inRangeDoOnce = true;
    }

    // Token: 0x060001A4 RID: 420 RVA: 0x0001EB4C File Offset: 0x0001CD4C
    public virtual void onOutOfRange()
    {
        onInRange();
        return;

        int i = 0;
        PhysicsSoundsScript[] array = this.physicsSoundsScriptsInChildren;
        int length = array.Length;
        while (i < length)
        {
            array[i].enabled = false;
            i++;
        }
        if (!this.enemyScript.dontCheckDistanceBeforeRunningLogic)
        {
            for (int j = 0; j < Extensions.get_length(this.rigidBodiesInChildren); j++)
            {
                if (this.rigidBodiesInChildren[j] != null)
                {
                    if (this.dontDoThingSafetyFrames <= 0)
                    {
                        this.rigidBodiesInChildrenKinematicState[j] = this.rigidBodiesInChildren[j].isKinematic;
                    }
                    this.rigidBodiesInChildren[j].isKinematic = true;
                }
            }
            if (!RuntimeServices.EqualityOperator(this.rigidBodySlowMotionInChildren, null))
            {
                for (int k = 0; k < Extensions.get_length(this.rigidBodySlowMotionInChildren); k++)
                {
                    if (this.rigidBodySlowMotionInChildren[k] != null)
                    {
                        if (this.dontDoThingSafetyFrames <= 0)
                        {
                            this.rigidBodySlowMotionInChildrenEnabledState[k] = this.rigidBodySlowMotionInChildren[k].enabled;
                        }
                        this.rigidBodySlowMotionInChildren[k].enabled = false;
                    }
                }
            }
        }
        this.inRangeDoOnce = false;
    }

    // Token: 0x060001A5 RID: 421 RVA: 0x0001EC8C File Offset: 0x0001CE8C
    public virtual void Update()
    {
        this.playerDistance = Vector2.Distance(this.transform.position, this.mainPlayer.position);
        if (!this.dontRunCode)
        {
            if (!this.enemyScript.simpleEnemy)
            {
                if (!this.enemyScript.enabled)
                {
                    if (!this.allowPutToSleep)
                    {
                        if (this.frameCounter < 5)
                        {
                            this.frameCounter++;
                        }
                        else
                        {
                            if (this.centerRigidBody.velocity.sqrMagnitude < 0.1f && (!this.enemyMeshRenderer.isVisible || this.headRigidBody.velocity.sqrMagnitude < 0.1f))
                            {
                                this.doStaticTimer = true;
                            }
                            else
                            {
                                this.doStaticTimer = false;
                            }
                            this.frameCounter = 0;
                        }
                        if (this.doStaticTimer)
                        {
                            if (this.staticTimer > (float)120)
                            {
                                this.allowPutToSleep = true;
                            }
                            else
                            {
                                this.staticTimer += this.root.timescale;
                            }
                        }
                    }
                }
                else
                {
                    this.allowPutToSleep = false;
                }
                if (this.onlyFreezeOffScreen && this.enemyMeshRenderer.isVisible)
                {
                    this.allowPutToSleep = false;
                }
            }
            if ((this.enemyScript.simpleEnemy || !this.allowPutToSleep) && (this.playerDistance < this.checkDistance || this.enemyScript.runLogic))
            {
                this.saveStateControllerScript.isDirty = true;
                if (!this.enemyScript.simpleEnemy && !this.inRangeDoOnce)
                {
                    this.onInRange();
                }
            }
            else if (!this.enemyScript.simpleEnemy && this.inRangeDoOnce)
            {
                this.onOutOfRange();
            }
            if (this.dontDoThingSafetyFrames > 0)
            {
                this.dontDoThingSafetyFrames--;
                if (this.enemyMeshRenderer.isVisible)
                {
                    if (!this.enemyScript.simpleEnemy && !this.theArmature.activeSelf)
                    {
                        this.theArmature.SetActive(true);
                    }
                    this.disableArmatureTimer = (float)120;
                }
            }
            else
            {
                if ((this.enemyMeshRenderer.isVisible && this.playerDistance < this.checkDistance) || (this.enemyScript.enabled && !this.enemyScript.idle))
                {
                    this.disableArmatureTimer = (float)120;
                }
                if (this.disableArmatureTimer > (float)0)
                {
                    if (!this.enemyScript.simpleEnemy && !this.theArmature.activeSelf)
                    {
                        this.theArmature.SetActive(true);
                    }
                    if (!this.root.doCheckpointLoad && this.enemyScript.enabled)
                    {
                        if (!this.theAnimator.enabled)
                        {
                            this.theAnimator.enabled = true;
                        }
                        if (!this.enemyGameCollision.enabled)
                        {
                            this.enemyGameCollision.enabled = true;
                        }
                        if (this.enemyScript.simpleEnemy && !this.sCol.enabled)
                        {
                            this.sCol.enabled = true;
                        }
                    }
                    this.disableArmatureTimer -= this.root.timescale;
                }
                else
                {
                    if (!this.enemyScript.simpleEnemy && this.theArmature.activeSelf)
                    {
                        this.theArmature.SetActive(false);
                    }
                    if (!this.root.doCheckpointLoad && this.enemyScript.enabled)
                    {
                        if (this.saveStateControllerScript.objStateSaved && this.theAnimator.enabled)
                        {
                            this.theAnimator.enabled = false;
                        }
                        if (this.enemyGameCollision.enabled)
                        {
                            this.enemyGameCollision.enabled = false;
                        }
                        if (this.enemyScript.simpleEnemy && this.sCol.enabled)
                        {
                            this.sCol.enabled = false;
                        }
                    }
                }
            }
        }
    }

    // Token: 0x060001A6 RID: 422 RVA: 0x000020A7 File Offset: 0x000002A7
    public virtual void Main()
    {
    }

    // Token: 0x0400043C RID: 1084
    private RootScript root;

    // Token: 0x0400043D RID: 1085
    private Transform mainPlayer;

    // Token: 0x0400043E RID: 1086
    private EnemyScript enemyScript;

    // Token: 0x0400043F RID: 1087
    private SaveStateControllerScript saveStateControllerScript;

    // Token: 0x04000440 RID: 1088
    private bool dontRunCode;

    // Token: 0x04000441 RID: 1089
    public float checkDistance;

    // Token: 0x04000442 RID: 1090
    public bool onlyFreezeOffScreen;

    // Token: 0x04000443 RID: 1091
    [HideInInspector]
    public float playerDistance;

    // Token: 0x04000444 RID: 1092
    private bool inRangeDoOnce;

    // Token: 0x04000445 RID: 1093
    private PhysicsSoundsScript[] physicsSoundsScriptsInChildren;

    // Token: 0x04000446 RID: 1094
    private Rigidbody[] rigidBodiesInChildren;

    // Token: 0x04000447 RID: 1095
    private bool[] rigidBodiesInChildrenKinematicState;

    // Token: 0x04000448 RID: 1096
    private RigidBodySlowMotion[] rigidBodySlowMotionInChildren;

    // Token: 0x04000449 RID: 1097
    private bool[] rigidBodySlowMotionInChildrenEnabledState;

    // Token: 0x0400044A RID: 1098
    private int frameCounter;

    // Token: 0x0400044B RID: 1099
    private bool doStaticTimer;

    // Token: 0x0400044C RID: 1100
    private float staticTimer;

    // Token: 0x0400044D RID: 1101
    private bool allowPutToSleep;

    // Token: 0x0400044E RID: 1102
    private Rigidbody centerRigidBody;

    // Token: 0x0400044F RID: 1103
    private Rigidbody headRigidBody;

    // Token: 0x04000450 RID: 1104
    private SkinnedMeshRenderer enemyMeshRenderer;

    // Token: 0x04000451 RID: 1105
    private GameObject theArmature;

    // Token: 0x04000452 RID: 1106
    private float disableArmatureTimer;

    // Token: 0x04000453 RID: 1107
    private Animator theAnimator;

    // Token: 0x04000454 RID: 1108
    private CapsuleCollider enemyGameCollision;

    // Token: 0x04000455 RID: 1109
    [HideInInspector]
    public CapsuleCollider sCol;

    // Token: 0x04000456 RID: 1110
    private int dontDoThingSafetyFrames;

    // Token: 0x04000457 RID: 1111
    private bool dontRunCodeS;

    // Token: 0x04000458 RID: 1112
    private float checkDistanceS;

    // Token: 0x04000459 RID: 1113
    private bool onlyFreezeOffScreenS;

    // Token: 0x0400045A RID: 1114
    private float playerDistanceS;

    // Token: 0x0400045B RID: 1115
    private bool inRangeDoOnceS;

    // Token: 0x0400045C RID: 1116
    private PhysicsSoundsScript[] physicsSoundsScriptsInChildrenS;

    // Token: 0x0400045D RID: 1117
    private Rigidbody[] rigidBodiesInChildrenS;

    // Token: 0x0400045E RID: 1118
    private bool[] rigidBodiesInChildrenKinematicStateS;

    // Token: 0x0400045F RID: 1119
    private RigidBodySlowMotion[] rigidBodySlowMotionInChildrenS;

    // Token: 0x04000460 RID: 1120
    private bool[] rigidBodySlowMotionInChildrenEnabledStateS;

    // Token: 0x04000461 RID: 1121
    private int frameCounterS;

    // Token: 0x04000462 RID: 1122
    private bool doStaticTimerS;

    // Token: 0x04000463 RID: 1123
    private float staticTimerS;

    // Token: 0x04000464 RID: 1124
    private bool allowPutToSleepS;

    // Token: 0x04000465 RID: 1125
    private float disableArmatureTimerS;

    // Token: 0x04000466 RID: 1126
    private int dontDoThingSafetyFramesS;
}
