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

    [Header("Menu Morte")]
    [SerializeField] private GameObject deathPanel;
    private bool mortePanel = false;
    private bool doOnce = false;

    [Header("Menu Mapa")]
    [SerializeField] private GameObject mapa;

    [Header("Botão Menu Principal")]
    [SerializeField] private string nomeMenu;

    [Header("UIKeyboard move")]
    public GameObject PauseFirstButton;
    public GameObject OptionsFirstButton;
    public GameObject OptionsCloseButton;
    public GameObject primeiroItem;
    public GameObject ultimoSave;
    public GameObject voltarInventario;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        deathPanel.SetActive(false);
        mortePanel = false;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && mortePanel == false)
        {
            SisPause(pausePanel);
            ///clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            ///set a new selected object
            EventSystem.current.SetSelectedGameObject(PauseFirstButton);
            
        }
        if(Input.GetButtonDown("Mapa") && mortePanel == false)
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

    public void IrMapa()
    {
        InventoryPanel.SetActive(false);
        mapa.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(voltarInventario);
    }

    public void VoltarMenu()
    {
        InventoryPanel.SetActive(true);
        mapa.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        
        EventSystem.current.SetSelectedGameObject(primeiroItem);
    }

    public void PlayerMorreu()
    {
        if (doOnce == false)
        {
            Time.timeScale = 0;
            deathPanel.SetActive(true);
            mortePanel = true;

            EventSystem.current.SetSelectedGameObject(null);

            EventSystem.current.SetSelectedGameObject(ultimoSave);
            doOnce = true;
        }
    }

    public void UltimoSave()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {

            Time.timeScale = 1;
            SceneManager.LoadScene(data.scenaAtual);
        }
    }
}
