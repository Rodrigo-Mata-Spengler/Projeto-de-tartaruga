using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

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

    [Header("Store")]
    public bool IsStore;
    [SerializeField] GameObject StoreObj;
    private bool OnStore = false;
    [SerializeField] private GameObject StoreButton;

    private void Start()
    {
        //get's the trigger component
       trigger = this.GetComponent<BoxCollider2D>();
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
            conversationObj.SetActive(true);
            ContinueStory();
           // Debug.Log("apaguei");
            havingConversation = true;
        }
        //if player press the interaction button and the paragraph was over, go to the next paragraph
        if (Input.GetButtonDown("Interacao") && textLocation < NpcWords.Length && nextFrase == true)
        {
            textLocation += 1;
            ContinueStory();
            
        }
        //if paragraph were over than disable the UI interaction obj
        if (havingConversation && textLocation == NpcWords.Length)
        {
            conversationObj.SetActive(false);
            havingConversation = false;

            ///checks if his have a store, if it does disply the store panel
            if(IsStore)
            {
                StoreObj.SetActive(true);
                OnStore = true;


                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(StoreButton);
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
        yield return new WaitForSeconds(2f);
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
}
