using System;
using UnityEngine;

public static class Debugging
{
    public static GameObject debugCube;
    public static GameObject debugText;

    public static GameObject CreateDisappearingCube(Vector3 position, Quaternion rotation, Vector3 scale, float time = 2.5f)
    {
        if (debugCube == null)
            return null;

        GameObject disappearCube = GameObject.Instantiate(debugCube);
        disappearCube.transform.position = position;
        disappearCube.transform.rotation = rotation;
        disappearCube.transform.localScale = scale;
        disappearCube.GetComponent<DebugDisappearObject>().waitTime = time;
        disappearCube.SetActive(true);


        return disappearCube;
    }
}

