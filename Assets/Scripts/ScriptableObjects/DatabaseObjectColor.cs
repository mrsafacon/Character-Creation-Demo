using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorObject", menuName = "CustomizableCharacterObject/ColorObject", order = 1)]
public class DatabaseObjectColor : DatabaseObject
{
    public Color Color = Color.black;
    public bool Face;
    public bool Details;
    public bool Hair;
}
