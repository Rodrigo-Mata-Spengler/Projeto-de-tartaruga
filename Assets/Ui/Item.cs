using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [Header("ItemDescription")]
    public string ItemName;
    [TextAreaAttribute]
    public string ItemDesciption; 

    public TextMeshProUGUI ItemNameText; ///the text title on UI
    public TextMeshProUGUI ItemDescriptionText;///the text description on the UI

    private void OnEnable()
    {
        ///pass the texts to the UI 
        ItemNameText.text = ItemName;
        ItemDescriptionText.text = ItemDesciption;
    }

}
