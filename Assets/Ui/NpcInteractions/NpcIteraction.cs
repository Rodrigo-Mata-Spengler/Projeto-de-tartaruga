using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


enum SellItem { Nothing ,SeaWeedMoreHealth, Armor, Potions };
enum NpcJob { Witch, Farmer, Ferreiro, none };
public class NpcIteraction : MonoBehaviour
{
    [Header("On UI elements")]
    [SerializeField] GameObject conversationObj; //the obj that contains the text object and pannels
    [SerializeField] TextMeshProUGUI conversationText;// the obj that contains the text component

    [Header("The text")]
    [Space]
    [TextAreaAttribute] //give more space to write
    [SerializeField] private string[] NpcWords;// array of paragraph

    [Header("Typing")]
    [Space]
    [SerializeField]private float typingSpeed = 0.04f;// the typing speed
    private bool endText = true;
    public int textLocation = 0;// get whi  ch one of the paragraphs or enabled
    public bool StartTyping;

    [Header("Trigger")]
    private BoxCollider2D trigger; // the box collider trigger
    public bool playerDetected = false; //variable to player detection
    private bool havingConversation = false; // variable to see if the paragraph is still been writing
    private bool nextFrase = false;//variable that checks if the player can go to the next paragraph


    [Header("What item Sell")]
    [SerializeField] private SellItem itemToSell = SellItem.Nothing;
    [SerializeField] private NpcJob NpcJob = NpcJob.none;

    [Header("Store")]
    public bool IsStore; ///if the npc have a store
    [HideInInspector]public int Cost;
    [HideInInspector] public int Cost2;
    [SerializeField] GameObject StoreFerreiro;//The blacksmith store panel 
    [SerializeField] GameObject StoreWitch;//The Witch store panel 
    [SerializeField] GameObject StoreFarmer;//The Farmer store panel 
    private bool OnStore = false; // Check's if is already on story
    //[SerializeField] private GameObject StoreButton; /// A button from the store UI panel, to be selected after the panel is enabled

    private ItensInventory PlayerInventory; // Variable to get Player Inventory
    private Health PlayerHealth; // Variable to get Player Health
    private GameObject Player; // Player GameObjet

    private void Start()
    {
        //get's the trigger component
       trigger = this.GetComponent<BoxCollider2D>();

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = Player.GetComponent<Health>();
        PlayerInventory = Player.GetComponent<ItensInventory>();
    }
    
    private void Update()
    {
        NextLineAndStop();
    }

    private void NextLineAndStop()
    {
        //if player wasn't in a conversation, close to the npc and press the button to interact. Will display the interaction UI obj and the start the coroutine
        if (playerDetected && Input.GetButtonDown("Interacao") && havingConversation == false)
        {
            StartTyping = false;
            StopAllCoroutines();
            conversationObj.SetActive(true);
            ContinueStory();
           // Debug.Log("apaguei");
            havingConversation = true;
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
        if (havingConversation && textLocation >= NpcWords.Length)
        {
            conversationObj.SetActive(false);
            havingConversation = false;

            ///checks if his have a store, if it does display the store panel
            if(IsStore)
            {
             
                switch (NpcJob)// depending on the what function the npc have will appear he's store panel 
                {
                    case NpcJob.Ferreiro:
                       StoreFerreiro.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(StoreFerreiro.transform.GetChild(7).gameObject);
                        Cost = StoreFerreiro.GetComponent<FindNpc>().Cost;
                        break;

                    case NpcJob.Farmer:
                        StoreFarmer.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(StoreFarmer.transform.GetChild(7).gameObject);
                        Cost = StoreFarmer.GetComponent<FindNpc>().Cost;
                        break;

                    case NpcJob.Witch:
                        StoreWitch.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(StoreWitch.transform.GetChild(7).gameObject);
                        Cost = StoreWitch.GetComponent<FindNpc>().Cost;
                        Cost2 = StoreWitch.GetComponent<FindNpc>().Cost2;

                        break;
                }

                OnStore = true;
 
            }
        }

        // if player press the esc disable the UI interaction obj
        if (Input.GetKey(KeyCode.Escape) /* || textLocation == NpcWords.Length*/)
        {
            conversationObj.SetActive(false); 

        }
    }
    //method that run the courotine
    private void ContinueStory()
    {
        StartCoroutine(DisplayLine(NpcWords[textLocation]));

    }

    //separate the string into chars and write one by one
    private IEnumerator DisplayLine(string line)
    {
        nextFrase = false;
        //empty the dialogue text
        conversationText.text = "";

        //display each letter one at a time
        foreach(char letter in line.ToCharArray())
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
        
        nextFrase= true;

    }
    private IEnumerator StartTypingDelay()
    {
        yield return new WaitForSeconds(0.5f);
        StartTyping = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if who enter the collider whas the player
        if(collision.gameObject.CompareTag("Player"))
        {
            playerDetected= true;
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
        }

    }

    //Function when player press yes on the pannel
    public void FerreiroSell()
    {
        if(PlayerInventory.calcio >= Cost) //if Player have the enough calcio to buy the item
        {
            ///ativar animação de armadura
            PlayerInventory.calcio -= Cost;
            PlayerHealth.maxLife *= 2;
            PlayerHealth.currentLife = PlayerHealth.maxLife;
            PlayerHealth.haveArmor = true;

            StoreFerreiro.SetActive(false);
        }

    }
    public void FarmerSell() //if Player have the enough bones to buy the item
    {
        if (PlayerInventory.ossos >= Cost)
        {
            PlayerInventory.ossos -= Cost;
            PlayerHealth.maxLife += 1;
            PlayerHealth.currentLife = PlayerHealth.maxLife;

            StoreFarmer.SetActive(false);
            ///give more mana
        }

    }
    public void WitchSell() //if Player have the enough Shells and corals to buy the item
    {
        if (PlayerInventory.conchas >= Cost && PlayerInventory.coral >= Cost2)
        {
            ///ativar animação de armadura
            PlayerInventory.conchas -= Cost;
            PlayerInventory.coral -= Cost2;

            StoreWitch.SetActive(false);
        }

    }
}
