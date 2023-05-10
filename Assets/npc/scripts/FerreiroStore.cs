using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FerreiroStore : MonoBehaviour
{

    [HideInInspector]public GameObject Ferreiro;  // Npc Blacsmith GameObject

    private int Cost; // The Amount of the item (depending witch npc the item will be different) the player needs to be able to buy/trade
    public TextMeshProUGUI costText; // the text on the UI

    public GameObject SelectedButton;

    private void Awake()
    {
        Ferreiro = GameObject.FindGameObjectWithTag("Ferreiro"); //Get's the blacsmith object by searching his tag
        Cost = Ferreiro.gameObject.GetComponent<Ferreiro>().Cost[0];

        // pass the cost values to the UI
        costText.text = Cost.ToString();

    }
}
