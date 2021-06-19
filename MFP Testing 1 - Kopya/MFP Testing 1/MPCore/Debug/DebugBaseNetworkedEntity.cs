using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum RPCSendMode
{
    SERVER,
    EVERYONE
}

public class DebugBaseNetworkedEntity : MonoBehaviour
{
    private Text debugText;
    private BaseNetworkEntity entity;

    IEnumerator ResetList()
    {
        yield return new WaitForSeconds(1.5f);
        debugText.text = "";
        StartCoroutine(ResetList());
    }

    public void Awake()
    {
        entity = gameObject.GetComponent<BaseNetworkEntity>();
    }

    public void Start()
    {

#if !DEBUG
        DestroyImmediate(this);
#endif

        if (Debugging.debugText != null && MultiplayerManagerTest.inst.multiplayerUI != null)
        {
            debugText = Instantiate(Debugging.debugText.GetComponent<Text>());
            debugText.transform.SetParent(MultiplayerManagerTest.inst.multiplayerUI.transform, false);
            debugText.text = "";
        }
        else
        {
            MFPEditorUtils.Log("debug asset load error");
            // debugText = RuntimeUI.CreateNewText(RuntimeUI.UIAnchor.middle, TextAnchor.MiddleCenter, Vector2.zero, new Vector2(100, 100), new Vector2(0.5f, 0.5f), new Vector3(2, 2, 2), Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font, Color.gray, VerticalWrapMode.Overflow, HorizontalWrapMode.Overflow, MultiplayerManagerTest.inst.multiplayerUI.gameObject, "TestText", "DebuggerText", 48); ;
            // debugText.transform.SetParent(MultiplayerManagerTest.inst.multiplayerUI.transform, false);
            //debugText.text = "";
        }

        StartCoroutine(ResetList());
    }

    public void Update()
    {
        if (debugText == null && Debugging.debugText != null && MultiplayerManagerTest.inst.multiplayerUI != null)
        {
            debugText = Instantiate(Debugging.debugText.GetComponent<Text>());
            debugText.transform.SetParent(MultiplayerManagerTest.inst.multiplayerUI.transform, false);
            debugText.text = "";
        }

        if (debugText != null && Camera.main != null)
            if (!string.IsNullOrEmpty(debugText.text))
                debugText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    public void OnRPC(string rpc, RPCSendMode sendmode)
    {
        if (debugText == null || entity.dontDoDebug || entity.debugHelper == null)
            return;
        debugText.text += rpc + "(" + (sendmode == RPCSendMode.EVERYONE ? "everyone" : "server") + ")" + "\n";
    }
}
