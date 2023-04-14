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

    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemDescriptionText;

    private void OnEnable()
    {
        ItemNameText.text = ItemName;
        ItemDescriptionText.text = ItemDesciption;
    }
    private void Start()
    {
        
    }
}
