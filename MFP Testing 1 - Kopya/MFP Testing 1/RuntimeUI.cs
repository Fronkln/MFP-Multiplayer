using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class RuntimeUI
{
    public enum UIAnchor
    {
        SKIP,

        topLeft,
        topMiddle,
        topRight,

        middleLeft,
        middle,
        middleRight,

        bottomLeft,
        bottomMiddle,
        bottomRight,

        stretchLeftRightBottom,
        stretchLeftRightMiddle,
        stretchLeftRightTop,

        stretchFull
    }

    public static void SetRect(RectTransform rect, UIAnchor uianch, Vector2 anchoredPosition, Vector2 sizeDelta, Vector2 pivot)
    {
        if (rect == null)
            Debug.LogError("what");

        rect.pivot = pivot;

        if (uianch != UIAnchor.SKIP)
        {
            rect.anchorMin = ReturnAnchor(uianch);
            rect.anchorMax = ReturnAnchor(uianch);
        }

        rect.sizeDelta = sizeDelta;

        rect.anchoredPosition = anchoredPosition;
    }

    public static Vector2 ReturnAnchor(UIAnchor uIAnchor)
    {
        switch (uIAnchor)
        {
            default:
                return new Vector2(0, 0);

            case UIAnchor.bottomLeft:
                return new Vector2(0, 0);
            case UIAnchor.bottomMiddle:
                return new Vector2(0.5f, 0);
            case UIAnchor.bottomRight:
                return new Vector2(1, 0);
            case UIAnchor.middleLeft:
                return new Vector2(0, 0.5f);
            case UIAnchor.middle:
                return new Vector2(0.5f, 0.5f);
            case UIAnchor.middleRight:
                return new Vector2(1, 0.5f);
            case UIAnchor.topLeft:
                return new Vector2(0, 1);
            case UIAnchor.topMiddle:
                return new Vector2(0.5f, 1);
            case UIAnchor.topRight:
                return new Vector2(1, 1);
        }
    }

    public static Vector2 ReturnAnchor(UIAnchor uIAnchor, bool anchorMin)
    {
        switch (uIAnchor)
        {
            default:
                return new Vector2(0, 0);
            case UIAnchor.stretchLeftRightBottom:
                if (anchorMin)
                    return new Vector2(0, 0);
                else
                    return new Vector2(1, 0);
            case UIAnchor.stretchLeftRightTop:
                if (anchorMin)
                    return new Vector2(0, 1);
                else
                    return new Vector2(1, 1);
            case UIAnchor.stretchLeftRightMiddle:
                if (anchorMin)
                    return new Vector2(0, 0.5f);
                else
                    return new Vector2(1, 0.5f);
            case UIAnchor.stretchFull:
                if (anchorMin)
                    return new Vector2(0, 0);
                else
                    return new Vector2(1, 1);
        }

    }

    public static ScrollRect CreateNewScrollRect(GameObject target, bool horizontal, bool vertical, ScrollRect.MovementType moveType, ScrollRect.ScrollbarVisibility visibility, bool inertia, float decelerationRate, RectTransform content, int spacing, RectTransform viewPort, Scrollbar horizontalScrollbar = null, Scrollbar verticalScrollbar = null)
    {
        ScrollRect newScrollRect = target.AddComponent<ScrollRect>();
        newScrollRect.content = content;
        newScrollRect.horizontal = horizontal;
        newScrollRect.vertical = vertical;

        newScrollRect.movementType = moveType;
        newScrollRect.inertia = inertia;
        newScrollRect.decelerationRate = decelerationRate;

        newScrollRect.scrollSensitivity = 1;

        newScrollRect.viewport = viewPort;

        newScrollRect.horizontalScrollbar = horizontalScrollbar;
        newScrollRect.verticalScrollbar = verticalScrollbar;

        if (horizontalScrollbar != null)
        {
            newScrollRect.horizontalScrollbarVisibility = visibility;
            newScrollRect.horizontalScrollbarSpacing = spacing;
        }
        if (verticalScrollbar != null)
        {
            newScrollRect.verticalScrollbarVisibility = visibility;
            newScrollRect.verticalScrollbarSpacing = spacing;
        }

        return newScrollRect;
    }

    public static InputField CreateNewInputField(UIAnchor uianch, Vector2 anchoredPosition, Vector2 sizeDelta, Vector2 pivot, Vector3 localSize, GameObject parent = null, string name = "New InputField", string imageSource = "", Text textComponent = null, bool hasBackground = false, InputField.CharacterValidation inputType = InputField.CharacterValidation.None)
    {
        GameObject newInputField = new GameObject();
        newInputField.name = name;
        newInputField.layer = 5;
        newInputField.transform.SetParent(parent.transform);

        RectTransform inputRect = newInputField.AddComponent<RectTransform>();

        SetRect(inputRect, uianch, anchoredPosition, sizeDelta, pivot);

        InputField inputField = newInputField.AddComponent<InputField>();

        inputField.interactable = true;
        inputField.transition = Selectable.Transition.ColorTint;
        inputField.contentType = InputField.ContentType.Standard;
        inputField.lineType = InputField.LineType.SingleLine;
        inputField.caretBlinkRate = 0.85f;
        inputField.caretWidth = 1;
        inputField.readOnly = false;
        inputField.characterValidation = inputType;
        inputField.gameObject.AddComponent<Image>();

        if (textComponent == null)
        {
            Text textComp = CreateNewText(uianch, TextAnchor.MiddleCenter, new Vector2(0, 0), new Vector2(160, 30), new Vector2(0.5f, 0.5f), new Vector3(1, 1, 1), (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font), new Color32(255, 0, 0, 255), VerticalWrapMode.Overflow, HorizontalWrapMode.Overflow, newInputField, "", "InputfieldText", 16);
            inputField.textComponent = textComp;
        }
        else
            inputField.textComponent = textComponent;

        newInputField.AddComponent<Image>();
        if (hasBackground)
            newInputField.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        inputField.targetGraphic = newInputField.GetComponent<Image>();

        return inputField;
    }


    public static Text CreateNewText(UIAnchor uianch, TextAnchor textAlign, Vector2 anchoredPosition, Vector2 sizeDelta, Vector2 pivot, Vector3 localSize, Font font, Color32 textColor, VerticalWrapMode wrapV, HorizontalWrapMode wrapH, GameObject parent = null, string text = "", string name = "New Text", int fontSize = 16)
    {
        GameObject createdTextObject = new GameObject();
        createdTextObject.transform.SetParent(parent.transform, false);
        createdTextObject.name = name;
        createdTextObject.layer = 5;

        RectTransform textRect = createdTextObject.AddComponent<RectTransform>();

        SetRect(textRect, uianch, anchoredPosition, sizeDelta, pivot);

        Text newText = createdTextObject.AddComponent<Text>();

        newText.resizeTextForBestFit = false;
        newText.alignByGeometry = false;

        newText.font = font;
        newText.text = text;
        newText.lineSpacing = 1;
        newText.alignment = textAlign;

        newText.color = textColor;
        newText.fontSize = fontSize;

        textRect.localScale = localSize;

        newText.verticalOverflow = wrapV;
        newText.horizontalOverflow = wrapH;

        return newText;
    }
}


