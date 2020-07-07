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

   IEnumerator ResetList()
    {
        yield return new WaitForSeconds(1.5f);
        debugText.text = "";
        StartCoroutine(ResetList());
    }

    public void Start()
    {
        if (MultiplayerManagerTest.inst.debugText != null && MultiplayerManagerTest.inst.multiplayerUI != null)
        {
            debugText = Instantiate(MultiplayerManagerTest.inst.debugText.GetComponent<Text>());
            debugText.transform.SetParent(MultiplayerManagerTest.inst.multiplayerUI.transform, false);
            debugText.text = "";
        }
        else
        {
            debugText = RuntimeUI.CreateNewText(RuntimeUI.UIAnchor.middle, TextAnchor.MiddleCenter, Vector2.zero, new Vector2(100, 100), new Vector2(0.5f, 0.5f), new Vector3(2, 2, 2), Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font, Color.gray, VerticalWrapMode.Overflow, HorizontalWrapMode.Overflow, MultiplayerManagerTest.inst.multiplayerUI.gameObject, "TestText", "DebuggerText", 48); ;
            debugText.transform.SetParent(MultiplayerManagerTest.inst.multiplayerUI.transform, false);
            debugText.text = "";
        }

        StartCoroutine(ResetList());
    }

    public void Update()
    {
        if(debugText != null && Camera.main != null)
        if(!string.IsNullOrEmpty(debugText.text))
            debugText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    public void OnRPC(string rpc, RPCSendMode sendmode)
    {
        if (debugText == null)
            return;
        debugText.text += rpc + "(" + (sendmode == RPCSendMode.EVERYONE ? "everyone" : "server") + ")" + "\n";
    }
}

