using System;
using UnityEngine;
using Steamworks;

public class SpectateMode : MonoBehaviour
{
    public static SpectateMode inst;
    public CSteamID followingPlayer;

    public static void InitCam(CSteamID followingPlayer = default(CSteamID))
    {
        if (inst != null)
            return;

        GameObject spectateOBJ = new GameObject();
        Camera spectatingCamera = spectateOBJ.AddComponent<Camera>();

        spectatingCamera.depth = -1;
        spectatingCamera.clearFlags = CameraClearFlags.Depth;

        CameraScript camScript = GameObject.FindObjectOfType<CameraScript>();

        spectateOBJ.transform.position = camScript.transform.position;

        camScript.GetComponent<Camera>().enabled = false;
        spectateOBJ.AddComponent<SpectateMode>().followingPlayer = followingPlayer;
        spectateOBJ.AddComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().profile = camScript.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().profile;

    }

    public void Awake() => inst = this;


    public void Update()
    {
        if (MultiplayerManagerTest.inst.playerObjects.ContainsKey(followingPlayer))
        {
            GameObject playerGhost = MultiplayerManagerTest.inst.playerObjects[followingPlayer].gameObject;
            transform.position = new Vector3(playerGhost.transform.position.x, playerGhost.transform.position.y, transform.position.z); ;
        }
    }
}

