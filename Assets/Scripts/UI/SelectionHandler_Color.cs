using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// SelectionHandler for Color buttons
/// </summary>
public class SelectionHandler_Color : SelectionHandler
{
    protected RuntimeDatabase.ColorType Type;

    protected override void Init2()
    {
        Type = (RuntimeDatabase.ColorType)TypeInt;

        //register listener
        CustomizableCharacter.AddColorListener(ChangeListener);

        //Activate highlight if active
        Highlight.SetActive(CustomizableCharacter.CheckIfActive(Type, ID));

        PreviewImage.color = RuntimeDatabase.GetColorByID(ID).Color;
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        //Process selection
        CustomizableCharacter.LoadColor(Type, ID);
    }

}
