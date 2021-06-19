using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Steamworks;


public enum PlayerSkins
{
    Default = 0,
    Victor_Agren = 1,
    Webgame_Protagonist = 2,
   // Denny = 3,
   // Ophelia = 4
}

public class MFPPlayerGhost : MonoBehaviour
{

    public bool debugFreezeGhost;

    private float packetResetTime = 0.2f;
    private float packetTime = 0;

    public int maxAllowedPackets = 30;
    public int curPackets = 0;

    private bool tpToPositionDoOnce = true;


    public CSteamID owner;
    public static Transform localInstance
    {
        get
        {
            if (MultiplayerManagerTest.connected && MultiplayerManagerTest.inst.playerObjects.ContainsKey(MultiplayerManagerTest.inst.playerID))
                return MultiplayerManagerTest.inst.playerObjects[MultiplayerManagerTest.inst.playerID].transform;
            else
                return PlayerScript.PlayerInstance.transform;
        }
    }


    public Animator playerAnimator;
    public Transform playerGraphics;

    public MFPPlayerSpeechController playerSpeechController;

    private bool debugPlayer = false;

    public int weapon = 0;

    private int deathWeapon = -1;

    public bool dead = false;
    private bool deadDoOnce = false;

    public Transform groundTransform;

    //Player body

    private GameObject CLIENT_BetaLegs, CLIENT_BetaHead, CLIENT_BetaHair, CLIENT_BETATORSOR;

    public Transform center;

    public Transform head;
    public Transform neck;

    public Transform lowerBack;
    public Transform upperBack;

    public Transform shoulderR;
    public Transform upperArmR;
    public Transform lowerArmR;
    public Transform handR;

    public Transform shoulderL;
    public Transform upperArmL;
    public Transform lowerArmL;
    public Transform handL;

    public Transform hipR;
    public Transform upperLegR;
    public Transform lowerLegR;
    public Transform footR;

    public Transform hipL;
    public Transform upperLegL;
    public Transform lowerLegL;
    public Transform footL;

    //IK Info

    public Vector3 realRootPosition;
    public Vector3 realGraphicsPosition;
    public Vector3 realCenterPosition;
    public Quaternion realGraphicsRotation;

    public Quaternion headIKRotation;
    public Quaternion lowerBackIKRotation;

    public Quaternion shoulderRIKRotation;
    public Quaternion upperArmRIKRotation;
    public Quaternion lowerArmRIKRotation;
    public Quaternion lowerHandRIKRotation;

    public Quaternion shoulderLIKRotation;
    public Quaternion upperArmLIKRotation;
    public Quaternion lowerArmLIKRotation;
    public Quaternion lowerHandLIKRotation;

    public Quaternion upperLegLIKRotation;
    public Quaternion lowerLegLIKRotation;
    public Quaternion footLIKRotation;

    public Quaternion upperLegRIKRotation;
    public Quaternion lowerLegRIKRotation;
    public Quaternion footRIKRotation;


    public Transform pistolR;
    public Transform pistolL;
    public Transform uziR;
    public Transform uziL;

    public Transform machineGun;
    public Transform shotgun;
    public Transform rifle;

    public Transform turretGun;
    public Transform shockRifle;
    public Transform crossbow;

    public Transform bulletPointR;
    public Transform bulletPointL;


    public AudioSource handRAudio;
    public AudioSource handLAudio;

    private MultiplayerManagerTest mpManager;


    private SkinnedMeshRenderer VhandL, VhandR;
    private SkinnedMeshRenderer defaultRenderer;
    public SkinnedMeshRenderer[] agrenRenderers;
    public MeshRenderer[] weaponRenderers;


    public PlayerSkins currentPlayerSkin = PlayerSkins.Default;

    public List<SkinnedMeshRenderer> activePlayerSkin = new List<SkinnedMeshRenderer>();
    public List<MeshRenderer> activePlayerGuns = new List<MeshRenderer>();

    public static MFPPlayerGhost NewPlayer(CSteamID client, bool debugPlayer = false)
    {
        MFPPlayerGhost player = Instantiate(MultiplayerManagerTest.inst.playerPrefab).AddComponent<MFPPlayerGhost>();
        player.name = "PlayerGhost_" + client.m_SteamID.ToString();
        player.owner = client;
        player.debugPlayer = debugPlayer;
        return player;
    }

    public float[] layerBlendsGhost;
    public float[] layerBlendsPlayer;

    private void PrepareSkins()
    {
        weaponRenderers = GetComponentsInChildren<MeshRenderer>();

        defaultRenderer = transform.Find("PlayerGraphics/TorsorBlackLongSleeve").GetComponent<SkinnedMeshRenderer>();
        agrenRenderers = new SkinnedMeshRenderer[]
        {
             transform.Find("PlayerGraphics/Head01").GetComponent<SkinnedMeshRenderer>(),
             transform.Find("PlayerGraphics/Legs01").GetComponent<SkinnedMeshRenderer>(),
             transform.Find("PlayerGraphics/Hair").GetComponent<SkinnedMeshRenderer>(),
             null
        };


        CLIENT_BetaHair = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault((GameObject g) => g.name == "Hair" && g.transform.root.GetComponent<PlayerScript>());
        CLIENT_BetaHead = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault((GameObject g) => g.name == "Head01" && g.transform.root.GetComponent<PlayerScript>());
        CLIENT_BetaLegs = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault((GameObject g) => g.name == "Legs01" && g.transform.root.GetComponent<PlayerScript>());

        agrenRenderers[0].materials = CLIENT_BetaHead.GetComponent<SkinnedMeshRenderer>().materials;
        agrenRenderers[1].materials = CLIENT_BetaLegs.GetComponent<SkinnedMeshRenderer>().materials;
        agrenRenderers[2].materials = CLIENT_BetaHair.GetComponent<SkinnedMeshRenderer>().materials;

        #region Agren Torso Load Ghost

        GameObject torsor = new GameObject();
        SkinnedMeshRenderer torsorRenderer = torsor.AddComponent<SkinnedMeshRenderer>();
        torsorRenderer.sharedMesh = DiscordCT.multiplayerBundle.LoadAsset("TorsoLongCoatAndHoodie") as Mesh;
        torsorRenderer.sharedMaterial = DiscordCT.multiplayerBundle.LoadAsset("torsor_long_coat_and_hoodie") as Material;

        // torsorRenderer.sharedMaterial.DisableKeyword("_SPECULARHIGHLIGHTS_OFF");
        //torsorRenderer.sharedMaterial.SetFloat("_SpecularHighlights", 0.139f);

        torsorRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        torsorRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;

        torsor.transform.parent = defaultRenderer.transform;
        torsor.transform.position = defaultRenderer.transform.position;

        SkinnedMeshRenderer originalRenderer = defaultRenderer.GetComponent<SkinnedMeshRenderer>();


        Transform[] replacementBones = new Transform[25];

        for (int i = 0; i < 23; i++)
            replacementBones[i] = originalRenderer.bones[i];

        replacementBones[23] = replacementBones[0];
        replacementBones[24] = replacementBones[0];

        torsorRenderer.bones = replacementBones;
        torsorRenderer.rootBone = originalRenderer.rootBone;
        torsorRenderer.probeAnchor = originalRenderer.probeAnchor;

        torsorRenderer.updateWhenOffscreen = true;
        agrenRenderers[3] = torsorRenderer;

        #endregion

        /*  foreach (SkinnedMeshRenderer rend in GetComponentsInChildren<SkinnedMeshRenderer>())
          {
              rend.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
              rend.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
          }
          */


        /*  foreach (MeshRenderer weaponRend in weaponRenderers)
          {
             // weaponRend.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
              //weaponRend.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
          }
          */


        for(int i = 0; i < weaponRenderers.Length; i++)
            weaponRenderers[i].material = PlayerScript.PlayerInstance.allWepRenderers[i].material;

        defaultRenderer.materials = PlayerScript.PlayerInstance.transform.Find("PlayerGraphics/TorsorBlackLongSleeve").GetComponentInChildren<SkinnedMeshRenderer>().materials;
         defaultRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.BlendProbes;
        defaultRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.BlendProbes;
    }

    public void ToggleGhost()
    {
        if (MFPMPUI.inst.toggleMPGhost.isOn)
            ChangeSkin(currentPlayerSkin);
        else
            DisableRenderers();
    }

    public void DisableRenderers()
    {
        defaultRenderer.enabled = false;

        foreach (SkinnedMeshRenderer rend in agrenRenderers)
            rend.enabled = false;

        foreach (MeshRenderer wepRend in weaponRenderers)
            wepRend.enabled = false;

        VhandL.enabled = false;
        VhandR.enabled = false;
    }

    public void ChangeSkin(PlayerSkins newPlayerSkin)
    {
        DisableRenderers();
        currentPlayerSkin = newPlayerSkin;

        PlayerScript player = PlayerScript.PlayerInstance;

        VhandL.enabled = true;
        VhandR.enabled = true;


        if(MultiplayerManagerTest.inst.isSkyfallLevel)
        {
            MFPEditorUtils.Log("Deadtoast and Web protag is broken in skyfall, switching to default");
            currentPlayerSkin = PlayerSkins.Default;
        }

        switch (currentPlayerSkin)
        {
            default:
                goto case PlayerSkins.Default;

            case PlayerSkins.Default:
                defaultRenderer.enabled = true;
                break;
            case PlayerSkins.Victor_Agren:           

                #region Client-side

                if (owner.isLocalUser())
                {

                    foreach (SkinnedMeshRenderer rend in player.transform.GetComponentsInChildren<SkinnedMeshRenderer>())
                    {
                        bool notValid = rend.transform.name == "Legs01" || rend.transform.name == "Head01" || rend.transform.name == "Hair" || rend.transform.name.ToLower().Contains("hand");

                        if (!notValid)
                        {
                            MFPEditorUtils.Log("disabled " + rend.transform.name);
                            rend.enabled = false;
                        }
                    }

                    if (CLIENT_BetaHead != null)
                    {
                        CLIENT_BetaHead.SetActive(true);
                        CLIENT_BetaHead.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
                    }
                    else
                        MFPEditorUtils.Log("Head01 not present!");

                    if(CLIENT_BetaLegs != null)
                    CLIENT_BetaLegs.SetActive(true);

                    //  agrenRenderers[1].material = betaLegs.GetComponent<SkinnedMeshRenderer>().sharedMaterial;

                    if(CLIENT_BetaHair != null)
                    CLIENT_BetaHair.SetActive(true);


                    #region Agren Torso Load Client


                    SkinnedMeshRenderer originalRenderer = PlayerScript.PlayerInstance.transform.Find("PlayerGraphics/TorsorBlackLongSleeve").GetComponent<SkinnedMeshRenderer>();

                    GameObject torsor = new GameObject();
                    SkinnedMeshRenderer torsorRenderer = torsor.AddComponent<SkinnedMeshRenderer>();
                    torsorRenderer.sharedMesh = DiscordCT.multiplayerBundle.LoadAsset("TorsoLongCoatAndHoodie") as Mesh;
                    torsorRenderer.sharedMaterial = DiscordCT.multiplayerBundle.LoadAsset("torsor_long_coat_and_hoodie") as Material;

                    torsorRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                    torsorRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;

                    torsor.transform.parent = originalRenderer.transform;
                    torsor.transform.position = originalRenderer.transform.position;

                    CLIENT_BETATORSOR = torsor.gameObject;


                    Transform[] replacementBones = new Transform[25];

                    for (int i = 0; i < 23; i++)
                        replacementBones[i] = originalRenderer.bones[i];

                    replacementBones[23] = replacementBones[0];
                    replacementBones[24] = replacementBones[0];

                    torsorRenderer.bones = replacementBones;
                    torsorRenderer.rootBone = originalRenderer.rootBone;
                    torsorRenderer.probeAnchor = originalRenderer.probeAnchor;

                    torsorRenderer.updateWhenOffscreen = true;

                    #endregion


                    MFPEditorUtils.Log("agren clientside load");
                }

                #endregion

                foreach (SkinnedMeshRenderer rend in agrenRenderers)
                    rend.enabled = true;

                break;
            case PlayerSkins.Webgame_Protagonist:
                goto case PlayerSkins.Victor_Agren;
        }

        if (currentPlayerSkin == PlayerSkins.Webgame_Protagonist)
        {

            agrenRenderers[2].enabled = false;


            Material[] removeGlassShine = new Material[1] { agrenRenderers[0].material };

            if (owner.isLocalUser())
            {
                CLIENT_BetaHair.SetActive(false);

                VhandL.GetComponent<SkinnedMeshRenderer>().material.color = Color.black;
                VhandR.GetComponent<SkinnedMeshRenderer>().material.color = Color.black;

                SkinnedMeshRenderer betaHeadRend = CLIENT_BetaHead.GetComponent<SkinnedMeshRenderer>();

                betaHeadRend.material.mainTexture = CustomizationAssets.mfpClassicHead.mainTexture;
                betaHeadRend.material.color = new Color(0.6f, 0.6f, 0.6f);
                betaHeadRend.materials = removeGlassShine;

                CLIENT_BetaLegs.GetComponent<SkinnedMeshRenderer>().material.mainTexture = CustomizationAssets.mfpClassicLegs;
                CLIENT_BetaLegs.GetComponent<SkinnedMeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 1);


                CLIENT_BETATORSOR.GetComponent<SkinnedMeshRenderer>().material.mainTexture = CustomizationAssets.mfpClassicTorso.mainTexture;
              

                PlayerScript.PlayerInstance.handRPublic.Find("hand_01").GetComponent<SkinnedMeshRenderer>().material.color = Color.black;
                PlayerScript.PlayerInstance.handLPublic.Find("hand_01_L").GetComponent<SkinnedMeshRenderer>().material.color = Color.black;
            }


            SkinnedMeshRenderer ghostHeadAgrenRend = agrenRenderers[0].GetComponent<SkinnedMeshRenderer>();

            ghostHeadAgrenRend.material.mainTexture = CustomizationAssets.mfpClassicHead.mainTexture;
            ghostHeadAgrenRend.material.color = new Color(0.6f, 0.6f, 0.6f);
            ghostHeadAgrenRend.materials = removeGlassShine;

            SkinnedMeshRenderer ghostLegsAgrenRend = agrenRenderers[1].GetComponent<SkinnedMeshRenderer>();

            ghostLegsAgrenRend.material.mainTexture = CustomizationAssets.mfpClassicLegs;
            ghostLegsAgrenRend.material.color = new Color(0.5f, 0.5f, 0.5f, 1);

            agrenRenderers[3].material.mainTexture = CustomizationAssets.mfpClassicTorso.mainTexture;

            MFPEditorUtils.Log("classic ghost end");
        }


        foreach (MeshRenderer wepRend in weaponRenderers)
            wepRend.enabled = true;

    }

    public void Awake()
    {
        mpManager = MultiplayerManagerTest.inst;

        playerGraphics = transform.GetChild(0);
        playerAnimator = playerGraphics.GetComponent<Animator>();

       

        layerBlendsGhost = new float[playerAnimator.layerCount];
        for (int i = 0; i < layerBlendsGhost.Length; i++)
            layerBlendsGhost[i] = playerAnimator.GetLayerWeight(i);


        if (mpManager.isMotorcycleLevel)
            playerAnimator.Play("MotorcycleBlend", 0);

        center = playerGraphics.Find("Armature/Center");

        lowerBack = playerGraphics.Find("Armature/Center/LowerBack");
        upperBack = playerGraphics.Find("Armature/Center/LowerBack/UpperBack");

        neck = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Neck");
        head = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Neck/Head");

        shoulderR = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_R");
        upperArmR = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R");
        lowerArmR = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R");
        handR = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R");
        bulletPointR = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_R/UpperArm_R/LowerArm_R/Hand_R/BulletPoint");

        shoulderL = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_L");
        upperArmL = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L");
        lowerArmL = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L/LowerArm_L");
        handL = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L");
        bulletPointL = playerGraphics.Find("Armature/Center/LowerBack/UpperBack/Shoulder_L/UpperArm_L/LowerArm_L/Hand_L/BulletPoint");

        hipR = playerGraphics.Find("Armature/Center/Hip_R");
        upperLegR = playerGraphics.Find("Armature/Center/Hip_R/UpperLeg_R");
        lowerLegR = playerGraphics.Find("Armature/Center/Hip_R/UpperLeg_R/LowerLeg_R");
        footL = transform.Find("PlayerGraphics/Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L/Foot_L");

        hipL = playerGraphics.Find("Armature/Center/Hip_L");
        upperLegL = playerGraphics.Find("Armature/Center/Hip_L/UpperLeg_L");
        lowerLegL = playerGraphics.Find("Armature/Center/Hip_L/UpperLeg_L/LowerLeg_L");
        footR = transform.Find("PlayerGraphics/Armature/Center/Hip_R/UpperLeg_R/LowerLeg_R/Foot_R");


        pistolR = handR.Find("pistol");
        pistolL = handL.Find("pistol");
        uziR = handR.Find("uzi");
        uziL = handL.Find("uzi");
        machineGun = handR.Find("machinegun");
        shotgun = handR.Find("shotgun");
        turretGun = handR.Find("turret_gun");
        // this.turretGunTop = this.turretGun.Find("Turret_Gun_Pipe/BulletPoint");
        shockRifle = handR.Find("ShockRifle");
        //   this.shockRifleShield = this.shockRifle.Find("ShockRifle_Shield");
        // this.shockRifleShieldMaterial = ((Renderer)this.shockRifleShield.GetComponent(typeof(Renderer))).sharedMaterials[1];
        // this.shockRifleShieldCollider = (BoxCollider)this.shockRifleShield.GetComponent(typeof(BoxCollider));
        rifle = this.handR.Find("Rifle");
        // this.rifleLaser = this.rifle.Find("RifleLaser");
        crossbow = this.handR.Find("Crossbow");

        bulletPointR = handR.Find("BulletPoint");
        bulletPointL = handL.Find("BulletPoint");

        handRAudio = handR.GetComponent<AudioSource>();
        handLAudio = handL.GetComponent<AudioSource>();


        VhandR = handR.Find("hand_01").GetComponent<SkinnedMeshRenderer>();
        VhandL = handL.Find("hand_01_L").GetComponent<SkinnedMeshRenderer>();

        playerSpeechController = gameObject.AddComponent<MFPPlayerSpeechController>();

        PrepareSkins();

        if (debugPlayer)
            return;

        PacketSender.SendPlayerWeapon();
    }

    public virtual void playGunSound(bool isRight, bool dualAiming = false)
    {
        if (dualAiming)
        {
            handRAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            handRAudio.volume = UnityEngine.Random.Range(0.95f, 1.1f);
            handRAudio.Play();

            handLAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            handLAudio.volume = UnityEngine.Random.Range(0.95f, 1.1f);
            handLAudio.Play();

            return;
        }

        if (isRight)
        {
            handRAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            handRAudio.volume = UnityEngine.Random.Range(0.95f, 1.1f);
            handRAudio.Play();
        }
        else
        {
            handLAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            handLAudio.volume = UnityEngine.Random.Range(0.95f, 1.1f);
            handLAudio.Play();
        }
        /*------------------------------------- GUN CLICKING SOUNDS... ADD LATER????-------------------------------------
        float num = 1f - this.ammo[this.weapon] / this.ammoFullClip[this.weapon];
        this.ammoLeftAudio.pitch = (float)(0.200000002980232 + 1.20000004768372 * (double)num) + UnityEngine.Random.Range(-0.05f, 0.05f);
        this.ammoLeftAudio.volume = (float)(1.5 * (double)num - 0.5) + UnityEngine.Random.Range(-0.05f, 0.05f);
        this.ammoLeftAudio.Play(); 
        */
        
    }
    public Texture2D GetSmallAvatar()
    {
        int FriendAvatar = SteamFriends.GetMediumFriendAvatar(owner);
        uint ImageWidth;
        uint ImageHeight;
        bool success = SteamUtils.GetImageSize(FriendAvatar, out ImageWidth, out ImageHeight);

        if (success && ImageWidth > 0 && ImageHeight > 0)
        {
            byte[] Image = new byte[ImageWidth * ImageHeight * 4];
            Texture2D returnTexture = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
            success = SteamUtils.GetImageRGBA(FriendAvatar, Image, (int)(ImageWidth * ImageHeight * 4));
            if (success)
            {
                returnTexture.LoadRawTextureData(Image);
                returnTexture.Apply();
            }
            return returnTexture;
        }
        else
        {
            Debug.LogError("Couldn't get avatar.");
            return new Texture2D(0, 0);
        }
    }

    public void Start()
    {

        MFPEditorUtils.Log("something will go terribly wrong");
        MFPEditorUtils.Log((MFPMPUI.playerAvatarTemplate.gameObject != null).ToString());
        WorldSpaceUI ui = Instantiate(MFPMPUI.playerAvatarTemplate).GetComponent<WorldSpaceUI>();
        ui.transform.localScale = new Vector3(ui.transform.localScale.x, -ui.transform.localScale.y, ui.transform.localScale.z);
        ui.transform.parent = MultiplayerManagerTest.inst.multiplayerUI.transform;
        ui.transform.GetComponent<UnityEngine.UI.RawImage>().texture = GetSmallAvatar();
        ui.offset = new Vector3(0, -2.4f, 0);
        //ui.GetComponent<UnityEngine.UI.Text>().text = "AMON";
        ui.target = transform;

        if (!debugPlayer)
        {
            string playerSk = SteamMatchmaking.GetLobbyMemberData(MultiplayerManagerTest.lobbyID, owner, "playerSkin");
            ChangeSkin((PlayerSkins)int.Parse(playerSk));
        }
        else
        {
            ChangeWeapon(1);
            ChangeSkin((PlayerSkins)UnityEngine.Random.Range(0, 3));
        }


        if (!owner.isLocalUser())
        {// DANGEROUS CHANGE MAYBE??????????? eğer limblere de Player tagı verirsen OnStartInteract'de mfpplayerghost olup olmadığını kontrol etmen gerekir
            gameObject.tag = "Player";

            if (MultiplayerManagerTest.inst.gamemode == MPGamemodes.Normal)
            {
                lowerBack.gameObject.layer = 15;
                upperBack.gameObject.layer = 15;

                head.gameObject.layer = 15;
                neck.gameObject.layer = 15;

                upperLegL.gameObject.layer = 15;
                lowerLegL.gameObject.layer = 15;

                upperLegR.gameObject.layer = 15;
                upperLegL.gameObject.layer = 15;
            }
            else if (MultiplayerManagerTest.inst.gamemode == MPGamemodes.PvP)
            {
                MFPEditorUtils.Log("PVP Collider");

                lowerBack.gameObject.layer = 8;
                upperBack.gameObject.layer = 8;

                head.gameObject.layer = 8;
                neck.gameObject.layer = 8;

                upperLegL.gameObject.layer = 8;
                lowerLegL.gameObject.layer = 8;

                upperLegR.gameObject.layer = 8;
                upperLegL.gameObject.layer = 8;
            }
            else if (MultiplayerManagerTest.inst.gamemode == MPGamemodes.Race)
            {
                foreach (Collider coll in gameObject.GetComponentsInChildren<Collider>())
                    coll.enabled = false;
            }
        }

        else
        {
            lowerBack.gameObject.layer = 0;
            upperBack.gameObject.layer = 0;

            head.gameObject.layer = 0;
            neck.gameObject.layer = 0;

            upperLegL.gameObject.layer = 0;
            lowerLegL.gameObject.layer = 0;

            upperLegR.gameObject.layer = 0;
            upperLegL.gameObject.layer = 0;
        }

    }

    public void ChangeWeapon(byte weapon)
    {
        this.weapon = weapon;

        pistolL.gameObject.SetActive(false);
        pistolR.gameObject.SetActive(false);
        uziR.gameObject.SetActive(false);
        uziL.gameObject.SetActive(false);
        machineGun.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
        turretGun.gameObject.SetActive(false);
        shockRifle.gameObject.SetActive(false);
        rifle.gameObject.SetActive(false);
        crossbow.gameObject.SetActive(false);


        handRAudio.clip = PlayerScript.PlayerInstance.weaponSound[weapon];
        handLAudio.clip = PlayerScript.PlayerInstance.weaponSound[weapon];

        if (weapon == 5)
            handLAudio.clip = PlayerScript.PlayerInstance.grenadeLauncher;

        switch (weapon)
        {
            case 1:
                pistolR.gameObject.SetActive(true);
                break;
            case 2:
                pistolR.gameObject.SetActive(true);
                pistolL.gameObject.SetActive(true);
                break;
            case 3:
                uziR.gameObject.SetActive(true);
                break;
            case 4:
                uziR.gameObject.SetActive(true);
                uziL.gameObject.SetActive(true);
                break;
            case 5:
                machineGun.gameObject.SetActive(true);
                break;
            case 6:
                shotgun.gameObject.SetActive(true);
                break;
            case 7:
                turretGun.gameObject.SetActive(true);
                break;
            case 8:
                shockRifle.gameObject.SetActive(true);
                break;
            case 9:
                rifle.gameObject.SetActive(true);
                break;
            case 10:
                crossbow.gameObject.SetActive(true);
                break;
        }

    }

    public void DisposePlayer()
    {
        Destroy(gameObject);
        MultiplayerManagerTest.inst.playerObjects.Remove(owner);
    }

    public void Update()
    {
        if (!MultiplayerManagerTest.connected)
            DisposePlayer();


        if (MFPMPUI.inst.disableGhostsOnInitDoOnce && owner.isLocalUser())
        {
            MFPMPUI.inst.disableGhostsOnInitDoOnce = false;
            MFPMPUI.inst.toggleMPGhost.isOn = false;
        }

        if (tpToPositionDoOnce && realRootPosition != Vector3.zero)
        {
            transform.position = realRootPosition;
            playerGraphics.position = realGraphicsPosition;
            playerGraphics.rotation = realGraphicsRotation;
            center.transform.position = realCenterPosition;

            tpToPositionDoOnce = false;
        }

        if (debugPlayer)
        {
            if (Input.GetKeyDown("up"))
                transform.Translate(Vector3.up * 2);
            if (Input.GetKeyDown("down"))
                transform.Translate(-Vector3.up * 2);
            if (Input.GetKeyDown("left"))
                transform.Translate(-Vector3.right * 2);
            if (Input.GetKeyDown("right"))
                transform.Translate(Vector3.right * 2);

            return;
        }


        // agrenRenderers[3].enabled = true;

    /*    if (mpManager.root.dead)
        {
            if (!dead && !deadDoOnce)
            {
                MFPEditorUtils.Log("IM DETT");
                PacketSender.SendPlayerLifeState(false);
                deadDoOnce = true;
            }
        }
        else
        {
            if (dead)
                PacketSender.SendPlayerLifeState(true);
        }
        */


        if (!debugFreezeGhost)
        {
            transform.position = Vector3.Lerp(transform.position, realRootPosition, .1f);
            playerGraphics.position = Vector3.Lerp(playerGraphics.transform.position, realGraphicsPosition, .1f);
            playerGraphics.rotation = Quaternion.Lerp(playerGraphics.rotation, realGraphicsRotation, .1f);
            center.transform.position = Vector3.Lerp(transform.position, realCenterPosition, .1f);
        }



        if (owner.isLocalUser())
        {

            if (dead && PlayerScript.PlayerInstance.health > 0)
               PacketSender.SendPlayerLifeState(true);


            weapon = PlayerScript.PlayerInstance.weapon;

            if (curPackets != 0)
            {
                if (packetTime < packetResetTime)
                    packetTime += Time.unscaledDeltaTime;
                else
                {
                    packetTime = 0;
                    curPackets = 0;
                }
            }

            if (curPackets < maxAllowedPackets)
            {
                PacketSender.SendPlayerAnimator();
                PacketSender.SendPlayerTransform();
                curPackets++;
            }
        }
    }


    public void GhostRespawn()
    {
        for (int i = 0; i < layerBlendsGhost.Length; i++)
            playerAnimator.SetLayerWeight(i, layerBlendsGhost[i]);

        MFPEditorUtils.Log("ghost layer blend set");

       playerAnimator.Play("OnGround Blend Tree", 0, 0); 

        if (owner.isLocalUser())
        {
            Animator actualPlayerAnim = PlayerScript.PlayerInstance.playerAnimator;

            if (actualPlayerAnim == null)
                MFPEditorUtils.LogError("This shouldnt happen, animator is null");

            try
            {
                for (int i = 0; i < layerBlendsPlayer.Length; i++)
                    actualPlayerAnim.SetLayerWeight(i, layerBlendsPlayer[i]);
            }
            catch { MFPEditorUtils.LogError("Error in setting client layer blend"); }

            playerAnimator.Play("OnGround Blend Tree", 0, 0);

        }

        dead = false;
        deadDoOnce = false;
    }

    public void GhostDeath()
    {

        if (owner.isLocalUser())
        {
            Animator playerAnim = PlayerScript.PlayerInstance.playerAnimator;

           layerBlendsPlayer = new float[playerAnim.layerCount];
            for (int i = 0; i < layerBlendsPlayer.Length; i++)
                layerBlendsPlayer[i] = playerAnim.GetLayerWeight(i);

            playerAnim.SetLayerWeight(0, 1f);
            playerAnim.SetLayerWeight(1, 0.0f);
            playerAnim.SetLayerWeight(2, 0.0f);
            playerAnim.SetLayerWeight(3, 0.0f);
            playerAnim.SetLayerWeight(4, 0.0f);
            playerAnim.SetLayerWeight(5, 0.0f);
            playerAnim.Play("Death", 0, 0);

        }

        dead = true;

        playerAnimator.SetLayerWeight(0, 1f);
        playerAnimator.SetLayerWeight(1, 0.0f);
        playerAnimator.SetLayerWeight(2, 0.0f);
        playerAnimator.SetLayerWeight(3, 0.0f);
        playerAnimator.SetLayerWeight(4, 0.0f);
        playerAnimator.SetLayerWeight(5, 0.0f);
        playerAnimator.Play("Death", 0, 0);
    }

    public void LateUpdate()
    {
        if (dead || debugPlayer || debugFreezeGhost)
            return;

        head.transform.rotation = headIKRotation;
        lowerBack.transform.rotation = lowerBackIKRotation;

        shoulderR.transform.rotation = shoulderRIKRotation;
        upperArmR.transform.rotation = upperArmRIKRotation;
        lowerArmR.transform.rotation = lowerArmRIKRotation;
        handR.transform.rotation = lowerHandRIKRotation;


        shoulderL.transform.rotation = shoulderLIKRotation;
        upperArmL.transform.rotation = upperArmLIKRotation;
        lowerArmL.transform.rotation = lowerArmLIKRotation;
        handL.transform.rotation = lowerHandLIKRotation;

        upperLegL.transform.rotation = upperLegLIKRotation;
        lowerLegL.transform.rotation = lowerLegLIKRotation;
        footL.transform.rotation = footLIKRotation;

        upperLegR.transform.rotation = upperLegRIKRotation;
        lowerLegR.transform.rotation = lowerLegRIKRotation;
        footR.transform.rotation = footRIKRotation;
    }
}

