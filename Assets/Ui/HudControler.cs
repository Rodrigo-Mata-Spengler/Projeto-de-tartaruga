using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudControler : MonoBehaviour
{
    public static float vidaAtual = 0;
    [SerializeField] private Slider sliderVida;

    public static float manaAtual = 0;
    [SerializeField] private Slider sliderMana;
    public static bool hasMana = false;
    private static GameObject manaSliderGO;
    [SerializeField] private GameObject mana;

    public static int seaweedAtual = 0;
    [SerializeField] private TMP_Text seaWeedText;

    private GameObject player;

    private void Start()
    {
        manaSliderGO = mana;

        manaSliderGO.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");

        sliderVida.value = player.GetComponent<Health>().currentLife * 8;
        sliderMana.value = player.GetComponent<Estamina>().CurrentEstamina;
        if (player.GetComponent<Estamina>().hasEstamina)
        {
            hasMana = true;
            manaSliderGO.SetActive(true);
        }
        seaWeedText.text = player.GetComponent<Health>().HealSeaweed.ToString();
        seaweedAtual = player.GetComponent<Health>().HealSeaweed;
    }
    private void Update()
    {
        sliderVida.value = player.GetComponent<Health>().currentLife * 8;
        manaSliderGO.SetActive(hasMana);
        sliderMana.value = player.GetComponent<Estamina>().CurrentEstamina;
        seaWeedText.text = seaweedAtual.ToString();
    }

    public static void EnableMana(bool enable)
    {
        manaSliderGO.SetActive(enable);
    }

    public static void ChangeHealth(float value)
    {
        vidaAtual = value;
    }

    public static void ChangeMana(float value)
    {
        manaAtual = value;
    }

    public static void ChangeSeaWeed(int value)
    {
        seaweedAtual = value;
    }
}
