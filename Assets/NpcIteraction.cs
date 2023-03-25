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
    private bool nextFrase = false;

    private void Start()
    {
       trigger = this.GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        NextLineAndStop();
    }

    private void NextLineAndStop()
    {
        if (playerDetected && Input.GetButtonDown("Fire3") && havingConversation == false)
        {
            conversationObj.SetActive(true);
            ContinueStory();
            Debug.Log("apaguei");
            havingConversation = true;
        }
        if (Input.GetButtonDown("Fire1") && textLocation < NpcWords.Length && nextFrase == true)
        {
            textLocation += 1;
            ContinueStory();
        }
        if (havingConversation && textLocation == NpcWords.Length)
        {
            conversationObj.SetActive(false);

            havingConversation = false;
        }
        if (Input.GetKey(KeyCode.Escape) || textLocation == NpcWords.Length)
        {
            conversationObj.SetActive(false);
        }
    }
    private void ContinueStory()
    {

        StartCoroutine(DisplayLine(NpcWords[textLocation]));
            
       
       
    }

    private IEnumerator DisplayLine(string line)
    {
        nextFrase = false;
        //empty the dialogue text
        conversationText.text = "";

        //display each letter one at a time
        foreach(char letter in line.ToCharArray())
        {
            conversationText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

            if (Input.GetAxis("Fire1")>0.4f)
            {
                conversationText.text = line;
                nextFrase = true;
                break;
            }
        }
        nextFrase= true;

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
            conversationObj.SetActive(false);
        }

    }
}
