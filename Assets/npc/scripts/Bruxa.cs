using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bruxa : MonoBehaviour
{
    [Header("On UI elements")]
    [SerializeField] GameObject conversationObj; //the obj that contains the text object and pannels
    [SerializeField] TextMeshProUGUI conversationText;// the obj that contains the text component
    [SerializeField] TextMeshProUGUI npcNameText;

    [Header("The text")]
    [SerializeField] private string NpcName;
    [Space]
    [TextAreaAttribute] //give more space to write
    [SerializeField] private string[] NpcWords;// array of paragraph

    [Space]
    [TextAreaAttribute] //give more space to write
    [SerializeField] private string AfterFinishedText;
    [SerializeField]private bool Sold = false;

    [Header("Typing")]
    [Space]
    [SerializeField] private float typingSpeed = 0.02f;// the typing speed
   
    public int textLocation = 0;// get whi  ch one of the paragraphs or enabled
    public bool StartTyping;

    [Header("Trigger")]
    private BoxCollider2D trigger; // the box collider trigger
    public bool playerDetected = false; //variable to player detection
    private bool havingConversation = false; // variable to see if the paragraph is still been writing
    private bool nextFrase = false;//variable that checks if the player can go to the next paragraph


    [Header("Store")]
    public int[] Cost;
    [Space]

    public bool IsStore; ///if the npc have a store
    [SerializeField] GameObject StoreBruxa;//The blacksmith store panel 
    public bool OnStore = false; // Check's if is already on story
    //[SerializeField] private GameObject StoreButton; /// A button from the store UI panel, to be selected after the panel is enabled
    private bool inputPressed = false;

    private ItensInventory PlayerInventory; // Variable to get Player Inventory
    private Estamina PlayerEstamina; // Variable to get Player Health
    private GameObject Player; // Player GameObjet
    private MenuPause CanvasMenuPause;


    private GameObject InputFeedBack;

    public bool hadConversation = false;

    private GameObject HUD;
    private void Start()
    {
        //get's the trigger component
        trigger = this.GetComponent<BoxCollider2D>();

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerEstamina = Player.GetComponent<Estamina>();
        PlayerInventory = Player.GetComponent<ItensInventory>();
        CanvasMenuPause = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuPause>();

        InputFeedBack = gameObject.transform.GetChild(0).gameObject;

        HUD = GameObject.FindGameObjectWithTag("HUD");
    }

    private void Update()
    {
        if (playerDetected)
        {
            NextLineAndStop();
        }
        
    }

    private void NextLineAndStop()
    {
        if (Input.GetButtonDown("Interacao") && !Sold)
        {
            inputPressed = true;
            InputFeedBack.SetActive(false);
            //disable HUD
            HUD.SetActive(false);

            Player.GetComponent<PlayerMovement>().enabled = false; //freeze the player
            Player.GetComponent<Animator>().enabled = false;
        }
        //if player wasn't in a conversation, close to the npc and press the button to interact. Will display the interaction UI obj and the start the coroutine
        if (Input.GetButtonDown("Interacao") && havingConversation == false && textLocation < 2 && hadConversation == false && !Sold)
        {
            npcNameText.text = NpcName;
            //CanvasMenuPause.panelOpen = true;// set true the variable that cheks if a panel is enabled
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;


            StartTyping = false;
            StopAllCoroutines();
            conversationObj.SetActive(true);
            ContinueStory();
            // Debug.Log("apaguei");
            havingConversation = true;
        }
        if (Input.GetButtonDown("Interacao") && havingConversation == false && Player.GetComponent<PlayerMovement>().HaveMagicTrident && hadConversation == false && !Sold)
        {
            npcNameText.text = NpcName;
            conversationObj.SetActive(true);
            //CanvasMenuPause.panelOpen = true;// set true the variable that cheks if a panel is enabled
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;


            textLocation = 3;
            StartTyping = false;
            StopAllCoroutines();
            conversationObj.SetActive(true);
            ContinueStory();
            // Debug.Log("apaguei");
            havingConversation = true;
        }
        if (Input.GetButtonDown("Interacao") && havingConversation == false && Sold)
        {
            HUD.SetActive(false);

            npcNameText.text = NpcName;
            StartTyping = false;
            StopAllCoroutines();
            conversationObj.SetActive(true);
            ContinueStory();
           
        }
        //if player press the interaction button and the paragraph was over, go to the next paragraph
        if (Input.GetButtonDown("Interacao") && textLocation < NpcWords.Length && nextFrase == true)
        {
            StopAllCoroutines();
            StartTyping = false;
            textLocation += 1;
            ContinueStory();

        }
        //if paragraph were over than disable the UI interaction obj
        else if (havingConversation && textLocation == 2 && inputPressed)
        {
            conversationObj.SetActive(false);
            ///checks if his have a store, if it does display the store panel
            CanvasMenuPause.panelOpen = false;
            Player.GetComponent<PlayerMovement>().enabled = true;
            havingConversation = false;
            Player.GetComponent<Animator>().enabled = true;


            //enable HUD
            HUD.SetActive(true);
        }
        else if (textLocation == NpcWords.Length)
        {
            textLocation += 1;
            hadConversation = true;
        }
        if (inputPressed && hadConversation && playerDetected)
        {
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Player.GetComponent<PlayerMovement>().enabled = false; //freeze the player
            Player.GetComponent<Animator>().enabled = false;
            if (IsStore && OnStore == false)
            {
                conversationObj.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                StoreBruxa.SetActive(true);
                EventSystem.current.SetSelectedGameObject(StoreBruxa.GetComponent<BruxaStore>().SelectedButton);

                OnStore = true;
                HUD.gameObject.SetActive(true);
            }
        }
        
            // if player press the esc disable the UI interaction obj
        if (Input.GetKey(KeyCode.Escape))
        {
            inputPressed = false;
            OnStore = false;
            Player.GetComponent<PlayerMovement>().enabled = true;
            Player.GetComponent<Animator>().enabled = true;
            conversationObj.SetActive(false);
            StoreBruxa.SetActive(false);
            //enable HUD
            HUD.gameObject.SetActive(true);
        }
        
    }
    //method that run the courotine
    private void ContinueStory()
    {
        if (Sold)
        {
            StartCoroutine(DisplayLine(AfterFinishedText));
        }
        else
        {
            StartCoroutine(DisplayLine(NpcWords[textLocation]));
        }
    }

    //separate the string into chars and write one by one
    private IEnumerator DisplayLine(string line)
    {
        nextFrase = false;
        //empty the dialogue text
        conversationText.text = "";

        //display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            StartCoroutine(StartTypingDelay());
            conversationText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

            //if Player press the button it will display the entire paragraph and stop writing letter by letter
            if (Input.GetAxis("Interacao") > 0.9f && StartTyping)
            {
                conversationText.text = line;
                StartTyping = false;
                nextFrase = true;
                break;
            }
        }

        nextFrase = true;

    }
    private IEnumerator StartTypingDelay()
    {
        yield return new WaitForSeconds(0.5f);
        StartTyping = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if who enter the collider whas the player
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            InputFeedBack.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //checks if who exit the collider whas the player
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
            //disable the interaction UI
            conversationObj.SetActive(false);
            InputFeedBack.SetActive(false);

            OnStore = false;
            StoreBruxa.SetActive(false);

            //enable HUD
            HUD.SetActive(true);
        }

    }

    //Function when player press yes on the pannel

    public void WitchSell(GameObject NotEnoughMoney) //if Player have the enough Shells and corals to buy the item
    {
        if (PlayerInventory.conchas >= Cost[0] && PlayerInventory.coral >= Cost[1])
        {
            ///ativar animação de armadura
            PlayerInventory.conchas -= Cost[0];
            PlayerInventory.coral -= Cost[1];

            //give more mana
            PlayerEstamina.MaxEstamina = PlayerEstamina.MaxEstamina * 2;

            PlayerEstamina.GiveEstamina(5);

           Player.GetComponent<PlayerMovement>().enabled = true;
            Player.GetComponent<Animator>().enabled = true;
            StoreBruxa.SetActive(false);
            inputPressed = false;

            //enable HUD
            HUD.SetActive(true);

            Sold = true;
        }
        else
        {
            //Display not enough money
            NotEnoughMoney.SetActive(true);
        }

    }
    public void WitchDontSell(GameObject NotEnoughMoney) //if Player have the enough Shells and corals to buy the item
    {
        NotEnoughMoney.SetActive(false);
        Player.GetComponent<PlayerMovement>().enabled = true;
        inputPressed = false;
        Player.GetComponent<Animator>().enabled = true;
        StoreBruxa.SetActive(false);
        OnStore = false;

        //enable HUD
        HUD.gameObject.SetActive(true);
    }
}
