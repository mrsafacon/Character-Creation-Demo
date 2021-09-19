using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPanel : MonoBehaviour
{
    public GameObject PreviewPrefab;

    public Transform FaceButtonParent, DetailButtonParent, HairButtonParent;

    private void Start()
    {
        //Generate buttons for each color of type in Database
        GenerateButtons(RuntimeDatabase.ColorType.Face, FaceButtonParent);
        GenerateButtons(RuntimeDatabase.ColorType.Details, DetailButtonParent);
        GenerateButtons(RuntimeDatabase.ColorType.Hair, HairButtonParent);
    }

    
    private void GenerateButtons(RuntimeDatabase.ColorType type, Transform parentTransform)
    {
        //Generate and init with asset details
        foreach(DatabaseObjectColor colorOption in RuntimeDatabase.GetColors(type))
            GameObject.Instantiate(PreviewPrefab, parentTransform).GetComponent<SelectionHandler>().Init((int)type, colorOption.ID, colorOption.PreviewImage);
    }

}
