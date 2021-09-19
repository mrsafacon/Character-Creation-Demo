using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDatabase : MonoBehaviour
{
    public enum CustomizableType { Hair, Eye, Nose, Mouth }
    public enum ColorType { Face, Details, Hair }

    protected static DatabaseObjectPrefab[] HairDatabase;
    protected static DatabaseObjectTexture[] EyeDatabase;
    protected static DatabaseObjectTexture[] NoseDatabase;
    protected static DatabaseObjectTexture[] MouthDatabase;

    protected static DatabaseObjectColor[] ColorDatabase;

    #region  Initializations

    private void Awake()
    {
        //Init the Runtime Database: Loading art resources from our Database folders

        Load(ref HairDatabase, "Database/Hair");
        Debug.Log("Hair loaded from resources: " + HairDatabase.Length);

        Load(ref EyeDatabase, "Database/Eyes");
        Debug.Log("Eyes loaded from resources: " + EyeDatabase.Length);

        Load(ref NoseDatabase, "Database/Noses");
        Debug.Log("Noses loaded from resources: " + NoseDatabase.Length);

        Load(ref MouthDatabase, "Database/Mouths");
        Debug.Log("Mouths loaded from resources: " + MouthDatabase.Length);

        Load(ref ColorDatabase, "Database/Colors");
        Debug.Log("Colors loaded from resources: " + ColorDatabase.Length);
    }    

    //Loading method for DatabaseObjectPrefabs
    void Load(ref DatabaseObjectPrefab[] array, string path )
    {
        Object[] loadedResources = Resources.LoadAll(path, typeof(DatabaseObjectPrefab));
        array = new DatabaseObjectPrefab[loadedResources.Length];

        for(int i = 0; i < loadedResources.Length; i++)
        {
            array[i] = (DatabaseObjectPrefab) loadedResources[i];
            array[i].Init(i);
        }
    }

    //Loading method for DatabaseObjectTextures
    void Load(ref DatabaseObjectTexture[] array, string path )
    {
        Object[] loadedResources = Resources.LoadAll(path, typeof(DatabaseObjectTexture));
        array = new DatabaseObjectTexture[loadedResources.Length];

        for(int i = 0; i < loadedResources.Length; i++)
        {
            array[i] = (DatabaseObjectTexture) loadedResources[i];
            array[i].Init(i);
        }
    }

    //Loading method for DatabaseObjectColors
    void Load(ref DatabaseObjectColor[] array, string path )
    {
        Object[] loadedResources = Resources.LoadAll(path, typeof(DatabaseObjectColor));
        array = new DatabaseObjectColor[loadedResources.Length];

        for(int i = 0; i < loadedResources.Length; i++)
        {
            array[i] = (DatabaseObjectColor) loadedResources[i];
            array[i].Init(i);
        }
    }

    #endregion

    #region  Static Methods

    public static DatabaseObject[] GetCustomizableOptions(CustomizableType type)
    {
        //return the requested database
        if(type == CustomizableType.Hair)
            return HairDatabase;
        else if (type == CustomizableType.Eye)
            return EyeDatabase;
        else if (type == CustomizableType.Nose)
            return NoseDatabase;
        else if(type == CustomizableType.Mouth)
            return MouthDatabase;    
        else return null;
    }

    public static DatabaseObject GetCustomizableById(CustomizableType t, int id)
    {
        //return the requested database object
        DatabaseObject[] targetDatabase = GetCustomizableOptions(t);
        return targetDatabase[id];
    }

    public static List<DatabaseObjectColor> GetColors(ColorType type)
    {
        List<DatabaseObjectColor> returnList = new List<DatabaseObjectColor>();

        //Filter out colors that dont include the requested type
        foreach(DatabaseObjectColor c in ColorDatabase)
        {
            if(type == ColorType.Face && c.Face)
                returnList.Add(c);

            else if(type == ColorType.Details && c.Details)
                returnList.Add(c);

            else if(type == ColorType.Hair && c.Hair)
                returnList.Add(c);
        }

        return returnList;
    }

    public static DatabaseObjectColor GetColorByID(int id)
    {
        return ColorDatabase[id];
    }

    #endregion
}
