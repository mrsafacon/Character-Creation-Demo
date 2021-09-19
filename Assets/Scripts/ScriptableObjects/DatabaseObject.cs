using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for any Customization/Color options.
/// </summary>
public class DatabaseObject : ScriptableObject
{
    public Sprite PreviewImage;
    
    [HideInInspector] public int ID { get; protected set; }

    public void Init(int id)
    {
        //assign the ID at runtime
        ID = id;
    }
}
