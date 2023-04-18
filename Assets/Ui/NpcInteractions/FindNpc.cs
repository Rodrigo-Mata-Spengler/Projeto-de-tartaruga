using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class FindNpc : MonoBehaviour
{
    private GameObject Farmer; // Npc Farmer GameObject
    private GameObject Witch;  // Npc Witch GameObject
    private GameObject Ferreiro;  // Npc Blacsmith GameObject

    public int Cost; // The Amount of the item (depending witch npc the item will be different) the player needs to be able to buy/trade
    public TextMeshProUGUI costText; // the text on the UI
    public int Cost2;// The Amount of the second item (depending witch npc the item will be different) the player needs to be able to buy/trade
    public TextMeshProUGUI cost2Text; // the text on the UI


    private void Awake()
    {
        Farmer = GameObject.FindGameObjectWithTag("Farmer"); //Get's the farmer object by searching his tag
        Ferreiro = GameObject.FindGameObjectWithTag("Ferreiro"); //Get's the blacsmith object by searching his tag
        Witch = GameObject.FindGameObjectWithTag("Witch"); //Get's the witch object by searching his tag

        // pass the cost values to the UI
        costText.text = Cost.ToString(); 
        cost2Text.text = Cost2.ToString();
    }

}
