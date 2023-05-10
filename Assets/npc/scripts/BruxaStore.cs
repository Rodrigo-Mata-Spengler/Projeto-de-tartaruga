using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BruxaStore : MonoBehaviour
{
    [HideInInspector] public GameObject Witch;  // Npc Blacsmith GameObject

    private int Cost; // The Amount of the item (depending witch npc the item will be different) the player needs to be able to buy/trade
    public TextMeshProUGUI costText; // the text on the UI
    private int Cost2;// The Amount of the second item (depending witch npc the item will be different) the player needs to be able to buy/trade
    public TextMeshProUGUI cost2Text; // the text on the UI

    public GameObject SelectedButton;

    private void Awake()
    {
        Witch = GameObject.FindGameObjectWithTag("Witch"); //Get's the blacsmith object by searching his tag
        Cost = Witch.gameObject.GetComponent<Bruxa>().Cost[0];
        Cost2 = Witch.gameObject.GetComponent<Bruxa>().Cost[1];

        // pass the cost values to the UI
        costText.text = Cost.ToString();
        cost2Text.text = Cost2.ToString();

    }
}
