using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TItuloRegiao : MonoBehaviour
{
    private static string regiao = null;

    [SerializeField] private string areaDeTreinamento;

    [SerializeField] private string aldeia;

    [SerializeField] private string cavernaDosSusurros;

    [SerializeField] private string cavernaDeMusgo;

    [SerializeField] private string tumulosDeSal;

    [SerializeField] private TMP_Text text;

    [SerializeField] private Animator anim;

    public bool teste = false;
    private void Update()
    {
        if (teste)
        {
            teste = false;
            anim.SetTrigger("Mostra");

        }
    }

    private void Awake()
    {
        if (regiao != SceneManager.GetActiveScene().name)
        {

            regiao = SceneManager.GetActiveScene().name;
            ShowName();
        }
    }

    private void ShowName()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Cena 1":
                Show(areaDeTreinamento);
                break;
            case "Cena2":
                Show(aldeia);
                break;
            case "Cena3.1":
                Show(cavernaDosSusurros);
                break;
            case "Cena3.2":
                Show(cavernaDosSusurros);
                break;
            case "Cena3.3":
                Show(cavernaDosSusurros);
                break;
            case "Cena4.1":
                Show(cavernaDeMusgo);
                break;
            case "Cena4.2":
                Show(cavernaDeMusgo);
                break;
            case "Cena4.3":
                Show(cavernaDeMusgo);
                break;
            case "Cena4.4":
                Show(cavernaDeMusgo);
                break;
        }
    }

    private void Show(string nomeDisplay)
    {
        Debug.Log("entrei");
        text.text = nomeDisplay;
        anim.SetTrigger("Mostra");
    }
}
