using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadNextLevelAllClientsAsync : MonoBehaviour
{
    public AsyncOperation asyncLoad;
    private bool asyncLoadDone = false;

    public IEnumerator LoadNextLevel()
    {

        asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // Check if the load has finished
            if (asyncLoad.progress >= 0.9f && !asyncLoadDone)
            {
                asyncLoadDone = true;
                PacketSender.SendTransitionReadyMessage(); //can also be used for async loads
                MFPEditorUtils.Log("Lobby scene is loaded.");
            }

            yield return null;
        }

        MFPEditorUtils.Log("done");
    }

    public void Awake()
    {
        StartCoroutine(LoadNextLevel());
        MultiplayerManagerTest.inst.forceNextLevelDoOnce = true;
    }

    public void Update()
    {
        if (!MultiplayerManagerTest.inMenu)
            if (!MultiplayerManagerTest.inst.globalID.isNull())
                if (MultiplayerManagerTest.everyoneLoaded)
                    if (MultiplayerManagerTest.playingAsHost)
                        PacketSender.TransitionClientsToNextLevel();
    }
}

