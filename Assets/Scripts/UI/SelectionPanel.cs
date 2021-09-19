using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Generates buttons based on the selected tab
/// </summary>
public class SelectionPanel : MonoBehaviour
{

    private RuntimeDatabase.CustomizableType activeTab;

    public GameObject ButtonPrefab;

    private void Start()
    {
        HairTab();
    }

    public void HairTab()
    {        
        SetTab(RuntimeDatabase.CustomizableType.Hair);        
    }

    public void LeftEyeTab()
    {
        SetTab(RuntimeDatabase.CustomizableType.Eye);       
    }

    public void RightEyeTab()
    {
        SetTab(RuntimeDatabase.CustomizableType.Eye);        
    }

    public void NoseTab()
    {
        SetTab(RuntimeDatabase.CustomizableType.Nose);        
    }

    public void MouthTab()
    {
        SetTab(RuntimeDatabase.CustomizableType.Mouth);        
    }

    private void SetTab(RuntimeDatabase.CustomizableType type)
    {        
        activeTab = type;

        //clear all previous customizable selection buttons
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
            if(child != transform) GameObject.Destroy(child.gameObject);

        //generate new customizable selection buttons
        DatabaseObject[] customizableOptions = RuntimeDatabase.GetCustomizableOptions(type);

        //Generate buttons and setup selection type, id, and preview image for each button
        for(int i = 0; i < customizableOptions.Length; i++)
            GameObject.Instantiate(ButtonPrefab, transform).GetComponent<SelectionHandler>().Init((int)type, i, customizableOptions[i].PreviewImage);           
    }
}
