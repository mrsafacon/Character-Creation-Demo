using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class SelectionHandler : MonoBehaviour, IPointerClickHandler
{
    public UnityEngine.UI.Image PreviewImage;
    public GameObject Highlight;
    protected int TypeInt;
    protected int ID;
    
    //New button setup
    public void Init(int t, int id, Sprite previewImage)
    {
        //setup values
        TypeInt = t;
        ID = id;      

        //setup preview image
        PreviewImage.sprite = previewImage;    

        Init2();
    }

    protected abstract void Init2();

    public abstract void OnPointerClick(PointerEventData pointerEventData);

    protected void ChangeListener(int changedType, int changedID)
    {
        if(changedType == TypeInt)
        {
            //toggle selected highlight based on the changes that were made

            if(changedID == ID)
                Highlight.SetActive(true);
            else
                Highlight.SetActive(false);
        }
    }
}
