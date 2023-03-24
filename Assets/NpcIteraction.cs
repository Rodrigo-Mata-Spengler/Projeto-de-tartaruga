using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class NpcIteraction : MonoBehaviour
{
    [Header("On UI elements")]
    [SerializeField] GameObject conversationObj;
    [SerializeField] TextMeshProUGUI conversationText;
    [Space]
    [SerializeField] GameObject storeObj;
    [SerializeField] TextMeshProUGUI storeText;

    [Header("The text")]
    [Space]
    [TextAreaAttribute]
    [SerializeField] private string[] NpcWords;

    [Header("Typing")]
    [Space]
    [SerializeField]private float typingSpeed = 0.04f;
    private bool endText = true;
    public int textLocation = 0;

    [Header("Trigger")]
    private BoxCollider2D trigger;
    public bool playerDetected = false;
    private bool hadConversation = false;
    private bool havingConversation = false;


    private void Start()
    {
       trigger = this.GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        if(playerDetected && Input.GetButtonDown("Fire3") && havingConversation == false)
        {
            conversationObj.SetActive(true);
            ContinueStory();
            Debug.Log("apaguei");
            havingConversation = true;
        }
        if(havingConversation && Input.GetButtonDown("Fire3") && textLocation <=NpcWords.Length)
        {
            textLocation += 1;
            ContinueStory();
        }
        if(havingConversation && textLocation == NpcWords.Length)
        {
            conversationObj.SetActive(false);

            havingConversation = false;
        }
        
    }
    /*
    private Coroutine displayLineCoroutine()
    {
        return ;
    }
    */
    private void ContinueStory()
    {
        while(endText) 
        {
            StartCoroutine(DisplayLine(NpcWords[textLocation]));
            
        }
       
    }

    private IEnumerator DisplayLine(string line)
    {
        endText= false;
        //empty the dialogue text
        conversationText.text = "";

        //display each letter one at a time
        foreach(char letter in line.ToCharArray())
        {
            conversationText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        endText = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerDetected= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
        }
        if (hadConversation)
        {
            conversationObj.SetActive(false);
        }


    }
}
