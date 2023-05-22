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
    private static GameObject manaSliderGO;
    [SerializeField] private GameObject mana;

    public static int seaweedAtual = 0;
    [SerializeField] private TMP_Text seaWeedText;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        sliderVida.value = player.GetComponent<Health>().currentLife * 8;
        sliderMana.value = player.GetComponent<Estamina>().CurrentEstamina;
        seaWeedText.text = player.GetComponent<Health>().HealSeaweed.ToString();
        seaweedAtual = player.GetComponent<Health>().HealSeaweed;

        manaSliderGO = mana;
    }
    private void Update()
    {
        sliderVida.value = player.GetComponent<Health>().currentLife * 8;
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
