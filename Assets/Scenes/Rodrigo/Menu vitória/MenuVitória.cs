using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuVitória : MonoBehaviour
{
    [Header("Scenas novo jogo")]
    [SerializeField] private string Menu;
    public bool voltar_menu = false;
    private void Update()
    {
        if (voltar_menu)
        {
            VoltarMenu();
        }
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene(Menu);
    }
}
