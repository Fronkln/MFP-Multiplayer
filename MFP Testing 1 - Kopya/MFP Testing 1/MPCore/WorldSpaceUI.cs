using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldSpaceUI : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = Vector3.zero;
    public Vector3 startPos;

    protected RectTransform rootCanvasRect;
    public Camera theCamera;

    // Start is called before the first frame update
    public virtual void Start()
    {
        theCamera = Camera.main;
        rootCanvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public virtual void LateUpdate()
    {

        if (target == null)
            return; 

        Vector3 lookPos = target.position;
        float offsetPosY = lookPos.y;

        // Final position of marker above GO in world space
        Vector3 offsetPos = new Vector3(lookPos.x, offsetPosY, lookPos.z) + offset;

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 canvasPos;
        Vector2 screenPoint = theCamera.WorldToScreenPoint(offsetPos);

        //  bool seen = theCamera.WorldToViewportPoint


        Vector3 viewportpoint = theCamera.WorldToViewportPoint(lookPos);
        bool seen = (viewportpoint.x >= 0 && viewportpoint.x <= 1) && (viewportpoint.y >= 0 && viewportpoint.y <= 1) && viewportpoint.z >= 0;

        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rootCanvasRect, screenPoint, null, out canvasPos);

        // Set
        transform.localPosition = canvasPos;

    }
}