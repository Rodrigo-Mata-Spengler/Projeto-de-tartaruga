using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    private bool isPaused = false;

    [Header("Menu Pause")]
    [SerializeField] private GameObject pausePanel;

    [Header("Botão Menu Principal")]
    [SerializeField] private string nomeMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SisPause();
        }
    }
    //ao clicar esc vai ao menu de pause
    private void Pausar()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pausePanel.SetActive(true);
        isPaused = true;
    }

    //ao clicar esc de novo ou no botão sai do menu de pause
    public void DesPausar()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    private void SisPause()
    {
        if (isPaused)
        {
            DesPausar();
        }
        else
        {
            Pausar();
        }
    }

    //clicar no botão voltar ao menu principal volta ao menu principal
    public void IrMenuPrincipal()
    {
        SceneManager.LoadScene(nomeMenu);
    }

    //clicar sair fecha o jogo
    public void SairJogo()
    {
        Application.Quit();
    }
}
