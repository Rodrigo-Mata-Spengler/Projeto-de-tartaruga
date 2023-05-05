using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPause : MonoBehaviour
{
    private bool isPaused = false;
    

    [Header("Menu Pause")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject InventoryPanel;

    public bool panelOpen = false;

    [Header("Botão Menu Principal")]
    [SerializeField] private string nomeMenu;

    [Header("UIKeyboard move")]
    public GameObject PauseFirstButton;
    public GameObject OptionsFirstButton;
    public GameObject OptionsCloseButton;
    public GameObject primeiroItem;

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
            SisPause(pausePanel);
            ///clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            ///set a new selected object
            EventSystem.current.SetSelectedGameObject(PauseFirstButton);
            
        }
        if(Input.GetKeyDown(KeyCode.I) )
        {
            SisPause(InventoryPanel);
            ///clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            ///set a new selected object
            EventSystem.current.SetSelectedGameObject(primeiroItem);
            
        }
    }
    //ao clicar esc vai ao menu de pause
    private void Pausar(GameObject PanelToDisable)
    {
        Time.timeScale = 0;
       /// Cursor.lockState = CursorLockMode.None;
        ///Cursor.visible = true;
        PanelToDisable.SetActive(true);
        isPaused = true;
        panelOpen = true;
    }

    //ao clicar esc de novo ou no botão sai do menu de pause
    public void DesPausar(GameObject PanelToDisable)
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PanelToDisable.SetActive(false);
        isPaused = false;
        panelOpen = false;
    }

    private void SisPause(GameObject Panel)
    {
        if (isPaused)
        {
            DesPausar(Panel);
        }
        else
        {
            Pausar(Panel);
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
