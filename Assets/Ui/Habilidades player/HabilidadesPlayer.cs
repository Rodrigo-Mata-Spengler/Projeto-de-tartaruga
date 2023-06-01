using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HabilidadesPlayer : MonoBehaviour
{
    [Header("Painel habilidade")]
    [SerializeField] private GameObject painel;
    [SerializeField] private Image imagem_Habilidade;
    [SerializeField] private TMP_Text texto_Habilidade;
    [SerializeField] private Image button_habilidade;
    [SerializeField] private Animator anim_panel;

    [Header("Dash")]//id:2
    [SerializeField] private Sprite imagem_Dash;
    [SerializeField] private string texto_Dash;
    [SerializeField] private Sprite button_Dash;

    [Header("Magia")]//id:2
    [SerializeField] private Sprite imagem_Magia;
    [SerializeField] private string texto_Magia;
    [SerializeField] private Sprite button_Magia;

    [Header("Pulo duplo")]//id:2
    [SerializeField] private Sprite imagem_Pulo_Duplo;
    [SerializeField] private string texto_Pulo_Duplo;
    [SerializeField] private Sprite button_Pulo_Duplo;

    public void ShowHabilidade(int id)
    {
        switch (id)
        {
            case 0:
                imagem_Habilidade.sprite = imagem_Dash;
                texto_Habilidade.text = texto_Dash;
                button_habilidade.sprite = button_Dash;
                break;
            case 1:
                imagem_Habilidade.sprite = imagem_Magia;
                texto_Habilidade.text = texto_Magia;
                button_habilidade.sprite = button_Magia;
                break;
            case 2:
                imagem_Habilidade.sprite = imagem_Pulo_Duplo;
                texto_Habilidade.text = texto_Pulo_Duplo;
                button_habilidade.sprite = button_Pulo_Duplo;
                break;
        }
        anim_panel.SetTrigger("Show");
    }
}
