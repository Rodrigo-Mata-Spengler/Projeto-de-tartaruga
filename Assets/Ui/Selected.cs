using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Selected : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Item itemScript;

    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
        itemScript.enabled = true;
    }
    //Do this when the UI object is deselected
    public void OnDeselect(BaseEventData eventData)
    {
        itemScript.enabled = false;
    }

}
