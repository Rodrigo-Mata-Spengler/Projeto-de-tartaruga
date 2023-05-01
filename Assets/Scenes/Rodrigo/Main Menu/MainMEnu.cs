using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMEnu : MonoBehaviour
{
    [Header("Botões de inicio de jogo")]
    [SerializeField] private Button continuarJogo;
    [SerializeField] private Button novoJogo;

    [Header("Scenas novo jogo")]
    [SerializeField] private string cenaInicio;

    private string path;
    private void Start()
    {
        TemUmSave();
        path = Application.persistentDataPath + "/PlayerData.cpd";
    }

    private void Update()
    {
        TemUmSave();
    }

    private void TemUmSave()
    {
        if (File.Exists(path))
        {
            continuarJogo.interactable = true;
        }
        else
        {
            continuarJogo.interactable = false;
        }
    }

    public void NovoSave()
    {
        File.Delete(path);
        SceneManager.LoadScene(cenaInicio);
    }

    public void CarregarJogo()
    {
        if (File.Exists(path))
        {
            PlayerData data = SaveSystem.LoadPlayer();

            SceneManager.LoadScene(data.scenaAtual);

        }
    }
}
