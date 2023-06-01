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
    [SerializeField] private GameObject button_Dash;

    [Header("Magia")]//id:2
    [SerializeField] private Sprite imagem_Magia;
    [SerializeField] private string texto_Magia;
    [SerializeField] private GameObject button_Magia;

    [Header("Pulo duplo")]//id:2
    [SerializeField] private Sprite imagem_Pulo_Duplo;
    [SerializeField] private string texto_Pulo_Duplo;
    [SerializeField] private GameObject button_Pulo_Duplo;

    public bool teste1 = false;
    public int teste2;

    public void ShowHabilidade(int id)
    {
        button_Dash.SetActive(false);
        button_Magia.SetActive(false);
        button_Pulo_Duplo.SetActive(false);

        switch (id)
        {
            case 0:
                imagem_Habilidade.sprite = imagem_Dash;
                texto_Habilidade.text = texto_Dash;
                button_Dash.SetActive(true);
                break;
            case 1:
                imagem_Habilidade.sprite = imagem_Magia;
                texto_Habilidade.text = texto_Magia;
                button_Magia.SetActive(true);
                break;
            case 2:
                imagem_Habilidade.sprite = imagem_Pulo_Duplo;
                texto_Habilidade.text = texto_Pulo_Duplo;
                button_Pulo_Duplo.SetActive(true);
                break;
        }
        anim_panel.SetTrigger("Show");
    }

    private void Update()
    {
        if (teste1)
        {
            ShowHabilidade(teste2);

            teste1 = false;
        }
    }

}
