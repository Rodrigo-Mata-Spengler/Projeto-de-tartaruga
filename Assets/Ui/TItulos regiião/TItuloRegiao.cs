using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

enum Regiao { areaDeTreinamento, aldeia, cavernaDosSusurros, cavernaDeMusgo, tumulosDeSal, nada };
public class TItuloRegiao : MonoBehaviour
{
    private static Regiao regiao = Regiao.nada;

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
        TesteNome();
    }

    private void TesteNome()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Cena 1":
                if (regiao != Regiao.areaDeTreinamento)
                {
                    ShowName();
                    regiao = Regiao.areaDeTreinamento;
                }
                break;
            case "Cena2":
                if (regiao != Regiao.aldeia)
                {
                    ShowName();
                    regiao = Regiao.aldeia;
                }
                break;
            case "Cena3.1":
                if (regiao != Regiao.cavernaDosSusurros)
                {
                    ShowName();
                    regiao = Regiao.cavernaDosSusurros;
                }
                break;
            case "Cena3.2":
                if (regiao != Regiao.cavernaDosSusurros)
                {
                    ShowName();
                    regiao = Regiao.cavernaDosSusurros;
                }
                break;
            case "Cena3.3":
                if (regiao != Regiao.cavernaDosSusurros)
                {
                    ShowName();
                    regiao = Regiao.cavernaDosSusurros;
                }
                break;
            case "Cena4.1":
                if (regiao != Regiao.cavernaDeMusgo)
                {
                    ShowName();
                    regiao = Regiao.cavernaDeMusgo;
                }
                break;
            case "Cena4.2":
                if (regiao != Regiao.cavernaDeMusgo)
                {
                    ShowName();
                    regiao = Regiao.cavernaDeMusgo;
                }
                break;
            case "Cena4.2 esquerda":
                if (regiao != Regiao.cavernaDeMusgo)
                {
                    ShowName();
                    regiao = Regiao.cavernaDeMusgo;
                }
                break;
            case "Cena4.3":
                if (regiao != Regiao.cavernaDeMusgo)
                {
                    ShowName();
                    regiao = Regiao.cavernaDeMusgo;
                }
                break;
            case "Cena4.4":
                if (regiao != Regiao.cavernaDeMusgo)
                {
                    ShowName();
                    regiao = Regiao.cavernaDeMusgo;
                }
                break;
            case "Cena5":
                if (regiao != Regiao.tumulosDeSal)
                {
                    ShowName();
                    regiao = Regiao.tumulosDeSal;
                }
                break;
            case "Cena6":
                if (regiao != Regiao.tumulosDeSal)
                {
                    ShowName();
                    regiao = Regiao.tumulosDeSal;
                }
                break;
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
            case "Cena4.2 esquerda":
                Show(cavernaDeMusgo);
                break;
            case "Cena4.3":
                Show(cavernaDeMusgo);
                break;
            case "Cena4.4":
                Show(cavernaDeMusgo);
                break;
            case "Cena5":
                Show(tumulosDeSal);
                break;
            case "Cena6":
                Show(tumulosDeSal);
                break;
        }
    }

    public void Show(string nomeDisplay)
    {
        text.text = nomeDisplay;
        anim.SetTrigger("Mostra");
    }
}
