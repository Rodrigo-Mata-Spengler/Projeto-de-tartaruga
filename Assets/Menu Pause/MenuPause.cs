using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPause : MonoBehaviour
{
    private bool isPaused = false;
    private ItensInventory PlayerInventory;

    [Header("Menu Pause")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject AudioPanel;
    [SerializeField] private GameObject ControlsPanel;

    [HideInInspector] public bool panelOpen = false;

    [Header("Menu Morte")]
    [SerializeField] private GameObject deathPanel;
    private bool mortePanel = false;
    private bool doOnce = false;

    [Header("Menu Mapa")]
    [SerializeField] private GameObject mapa;

    [Header("Botão Menu Principal")]
    [SerializeField] private string nomeMenu;

    [Header("Hud GameObject")]
    [SerializeField] private GameObject HUDPanel;

    [Header("UIKeyboard move")]
    public GameObject PauseFirstButton;
    public GameObject OptionsFirstButton;
    public GameObject OptionsCloseButton;
    public GameObject primeiroItem;
    public GameObject ultimoSave;
    public GameObject voltarInventario;
    public GameObject MapButtonInventario;

    [HideInInspector]public bool painelOpen = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        deathPanel.SetActive(false);
        mortePanel = false;

        PlayerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<ItensInventory>();
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) && mortePanel == false )
        {
            SisPause(pausePanel);
            ///clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            ///set a new selected object
            EventSystem.current.SetSelectedGameObject(PauseFirstButton);
            
        }
        if(Input.GetButtonDown("Inventario") && mortePanel == false )
        {
            SisPause(InventoryPanel);
            ///clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            ///set a new selected object
            EventSystem.current.SetSelectedGameObject(primeiroItem);
            
        }
        if (Input.GetButtonDown("Mapa") && mortePanel == false && PlayerInventory.mapa == true)
        {
            SisPause(mapa);
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PanelToDisable.SetActive(true);
        isPaused = true;
        HUDPanel.SetActive(false);


    }

    //ao clicar esc de novo ou no botão sai do menu de pause
    public void DesPausar(GameObject PanelToDisable)
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PanelToDisable.SetActive(false);
        isPaused = false;

        HUDPanel.SetActive(true);
        InventoryPanel.SetActive(false);
        mapa.SetActive(false);
        OptionsPanel.SetActive(false);
        AudioPanel.SetActive(false);
        ControlsPanel.SetActive(false);
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
        Saveloader.doOnce = true;
        SceneManager.LoadScene(nomeMenu);
    }

    //clicar sair fecha o jogo
    public void SairJogo()
    {
        Saveloader.doOnce = true;
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

            Saveloader.doOnce = true;

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
            Saveloader.doOnce = true;
            Time.timeScale = 1;
            SceneManager.LoadScene(data.scenaAtual);
        }
    }

    public void SelectButton(GameObject buttonToSelected)
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(buttonToSelected);
    }
}
