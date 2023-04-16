using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class FindNpc : MonoBehaviour
{
    private GameObject Farmer;
    private GameObject Witch;
    private GameObject Ferreiro;

    private void Start()
    {
        Farmer = GameObject.FindGameObjectWithTag("Farmer");
        Ferreiro = GameObject.FindGameObjectWithTag("Ferreiro");
        Witch = GameObject.FindGameObjectWithTag("Witch");
    }

}
