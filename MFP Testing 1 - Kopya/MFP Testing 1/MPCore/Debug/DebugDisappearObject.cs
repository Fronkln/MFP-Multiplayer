using System.Collections;
using UnityEngine;

public class DebugDisappearObject : MonoBehaviour
{
    public float waitTime = 1.5f;

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
    
    public void Awake()
    {
        StartCoroutine(DestroyObject());
    }
}

