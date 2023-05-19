using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuVitória : MonoBehaviour
{
    [Header("Scenas novo jogo")]
    [SerializeField] private string cenaInicio;
    [SerializeField] private string Menu;

    private string path;
    private void Start()
    {
        path = Application.persistentDataPath + "/PlayerData.cpd";
    }

    public void NovoSave()
    {
        File.Delete(path);
        SceneManager.LoadScene(cenaInicio);
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene(Menu);
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}
