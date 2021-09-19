using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPanel : MonoBehaviour
{
    public GameObject PreviewPrefab;

    public Transform FaceButtonParent;
    public Transform DetailButtonParent;
    public Transform HairButtonParent;

    private void Start()
    {
        GenerateButtons(RuntimeDatabase.ColorType.Face, FaceButtonParent);
        GenerateButtons(RuntimeDatabase.ColorType.Details, DetailButtonParent);
        GenerateButtons(RuntimeDatabase.ColorType.Hair, HairButtonParent);
    }

    //Generate buttons for each color of type in Database
    private void GenerateButtons(RuntimeDatabase.ColorType type, Transform parentTransform)
    {
        foreach(DatabaseObjectColor colorOption in RuntimeDatabase.GetColors(type))
            GameObject.Instantiate(PreviewPrefab, parentTransform).GetComponent<SelectionHandler>().Init((int)type, colorOption.ID, colorOption.PreviewImage);
    }

}
