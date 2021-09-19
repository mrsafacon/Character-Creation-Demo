using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// SelectionHandler for 'Customizables' buttons
/// </summary>
public class SelectionHandler_Customizable : SelectionHandler
{
    protected RuntimeDatabase.CustomizableType Type;

    protected override void Init2()
    {
        Type = (RuntimeDatabase.CustomizableType)TypeInt;

        //register listener
        CustomizableCharacter.AddCustomizableListener(ChangeListener);

        //Activate highlight if active
        Highlight.SetActive(CustomizableCharacter.CheckIfActive(Type, ID));

        if(Type == RuntimeDatabase.CustomizableType.Eye || Type == RuntimeDatabase.CustomizableType.Nose || Type == RuntimeDatabase.CustomizableType.Mouth)
            PreviewImage.color = Color.black;
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        //Process selection
        CustomizableCharacter.LoadItem( Type, ID);
    }

    private void OnDestroy()
    {
        //unregister listener
        CustomizableCharacter.RemoveCustomizableListener(ChangeListener);
    }
}
