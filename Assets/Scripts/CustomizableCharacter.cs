using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizableCharacter : MonoBehaviour
{
    public enum CustomizableTextureType { LeftEye, RightEye, Nose, Mouth }

    public Transform FaceTransform;
    public Renderer FaceRenderer, LeftEyeRenderer, RightEyeRenderer, NoseRenderer, MouthRenderer;

    protected static CustomizableCharacter _instance;

    protected GameObject _currentHair;
    protected Color _hairColor = Color.black;    

    //Store current visual values
    protected static Dictionary<RuntimeDatabase.CustomizableType, int> ActiveVisuals = new Dictionary<RuntimeDatabase.CustomizableType, int>();
    protected static Dictionary<RuntimeDatabase.ColorType, int> ActiveColors = new Dictionary<RuntimeDatabase.ColorType, int>();

    //Delegate for outside elements(eg. UI elements) to subscribe to changes in character
    public delegate void ChangeListener(int t, int id);
    protected static ChangeListener CustomizableChangeListeners;
    protected static ChangeListener ColorChangeListeners;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        RandomizeAll();
    }

    #region Randomization

    public static void RandomizeAll()
    {
        LoadRandomForType(RuntimeDatabase.CustomizableType.Hair);
        LoadRandomForType(RuntimeDatabase.CustomizableType.Eye);
        LoadRandomForType(RuntimeDatabase.CustomizableType.Nose);
        LoadRandomForType(RuntimeDatabase.CustomizableType.Mouth);
        LoadRandomForType(RuntimeDatabase.ColorType.Face);
        LoadRandomForType(RuntimeDatabase.ColorType.Details);
        LoadRandomForType(RuntimeDatabase.ColorType.Hair);
    }

    private static void LoadRandomForType(RuntimeDatabase.CustomizableType type)
    {
        LoadItem(type, Random.Range(0, RuntimeDatabase.GetCustomizableOptions(type).Length));
    }

    private static void LoadRandomForType(RuntimeDatabase.ColorType type)
    {
        List<DatabaseObjectColor> colorOptions = RuntimeDatabase.GetColors(type);
        LoadColor(type,  colorOptions[Random.Range(0, colorOptions.Count)].ID);
    }



    #endregion

    #region  Load Customizable

    public static void LoadItem(RuntimeDatabase.CustomizableType type, int id)
    {
        //Apply the selection's art assets
        switch (type)
        {
            case RuntimeDatabase.CustomizableType.Hair:
                DatabaseObjectPrefab dbopHair =  (DatabaseObjectPrefab)RuntimeDatabase.GetCustomizableOptions(type)[id];
                CustomizableCharacter.LoadHair(dbopHair.Prefab);
                break;
            case RuntimeDatabase.CustomizableType.Eye:
                DatabaseObjectTexture dbotEye = (DatabaseObjectTexture)RuntimeDatabase.GetCustomizableOptions(type)[id];
                CustomizableCharacter.LoadTexture(CustomizableCharacter.CustomizableTextureType.LeftEye, dbotEye.Texture);
                CustomizableCharacter.LoadTexture(CustomizableCharacter.CustomizableTextureType.RightEye, dbotEye.Texture);
                break;
            case RuntimeDatabase.CustomizableType.Nose:
                DatabaseObjectTexture dbotNose = (DatabaseObjectTexture)RuntimeDatabase.GetCustomizableOptions(type)[id];
                CustomizableCharacter.LoadTexture(CustomizableCharacter.CustomizableTextureType.Nose, dbotNose.Texture);
                break;
            case RuntimeDatabase.CustomizableType.Mouth:
                DatabaseObjectTexture dbotMouth = (DatabaseObjectTexture)RuntimeDatabase.GetCustomizableOptions(type)[id];
                CustomizableCharacter.LoadTexture(CustomizableCharacter.CustomizableTextureType.Mouth, dbotMouth.Texture);
                break;
        }

        //Store changes
        ActiveVisuals[type] = id;

        //Notify listeners (UI Elements) of processed changes
        if(CustomizableChangeListeners != null)
            CustomizableChangeListeners((int)type, id);
    }

    private static void LoadTexture(CustomizableTextureType type, Sprite sprite)
    {
        //Apply Texture to the appropriate renderer's material
        if(type == CustomizableTextureType.LeftEye)
            _instance.LeftEyeRenderer.sharedMaterial.SetTexture("_MainTex", sprite.texture);          
        
        else if (type == CustomizableTextureType.RightEye)
            _instance.RightEyeRenderer.sharedMaterial.SetTexture("_MainTex", sprite.texture);
        
        else if (type == CustomizableTextureType.Nose)
            _instance.NoseRenderer.sharedMaterial.SetTexture("_MainTex", sprite.texture);

        else if (type == CustomizableTextureType.Mouth)
            _instance.MouthRenderer.sharedMaterial.SetTexture("_MainTex", sprite.texture);
    }

    private static void LoadHair(GameObject go)
    {
        //clean up old hair object
        if(_instance._currentHair != null)
            GameObject.Destroy(_instance._currentHair);

        //create new hair with color
        _instance._currentHair = GameObject.Instantiate(go, _instance.FaceTransform);
        _instance._currentHair.transform.localPosition = Vector3.zero;
        _instance._currentHair.transform.localEulerAngles = Vector3.zero;
    }

    #endregion

    #region  Load Color

    public static void LoadColor(RuntimeDatabase.ColorType type, int id)
    {
        DatabaseObjectColor color = RuntimeDatabase.GetColorByID(id);

        switch (type)
        {
            case RuntimeDatabase.ColorType.Face:
                _instance.FaceRenderer.sharedMaterial.SetColor("_Color", color.Color);
                break;
            case RuntimeDatabase.ColorType.Details:
                _instance.LeftEyeRenderer.sharedMaterial.SetColor("_Color", color.Color);
                _instance.RightEyeRenderer.sharedMaterial.SetColor("_Color", color.Color);
                _instance.NoseRenderer.sharedMaterial.SetColor("_Color", color.Color);
                _instance.MouthRenderer.sharedMaterial.SetColor("_Color", color.Color);
                break;
            case RuntimeDatabase.ColorType.Hair:
                _instance._currentHair.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", color.Color);
                break;
        }

        ActiveColors[type] = id;

        if(ColorChangeListeners != null)
            ColorChangeListeners((int) type, id);
    }

    #endregion

    #region Change and State helpers

    public static void AddCustomizableListener(ChangeListener listener)
    {
        CustomizableChangeListeners += listener;
    }

    public static void RemoveCustomizableListener(ChangeListener listener)
    {
        CustomizableChangeListeners -= listener;
    }

    public static void AddColorListener(ChangeListener listener)
    {
        ColorChangeListeners += listener;
    }

    //Checks to see if the customizable option is currently active
    public static bool CheckIfActive(RuntimeDatabase.CustomizableType type, int id)
    {
        int value;        
        if(ActiveVisuals.TryGetValue(type, out value))
        {
            if(value == id)
                return true;
        }

        return false;
    }

    //Checks to see if the color option is currently active
    public static bool CheckIfActive(RuntimeDatabase.ColorType type, int id)
    {
        int value;        
        if(ActiveColors.TryGetValue(type, out value))
        {
            if(value == id)
                return true;
        }

        return false;
    }

    #endregion
}
