using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItensInventory : MonoBehaviour
{
    public int conchas;//Shells
    public int coral; //coral
    public int calcio; //calcium
    public int ossos; // bones
    [Space]
    public bool mapa = false;
    //[Space]
    //public int MaxAmount = 10;


    private TextMeshProUGUI conchasUI;//Shells
    private TextMeshProUGUI coralUI; //coral
    private TextMeshProUGUI calcioUI; //calcium
    private TextMeshProUGUI ossosUI; // bones

    private bool passar = false;
    private bool DoOnceMap = false;
    private void Start()
    {
       
    }
    private void Update()
    {
        if (Input.GetButtonUp("Inventario"))
        {
            conchasUI = GameObject.FindGameObjectWithTag("ConchaUI").GetComponentInChildren<TextMeshProUGUI>();
            coralUI = GameObject.FindGameObjectWithTag("CoralUI").GetComponentInChildren<TextMeshProUGUI>();
            calcioUI = GameObject.FindGameObjectWithTag("CalcioUI").GetComponentInChildren<TextMeshProUGUI>();
            ossosUI = GameObject.FindGameObjectWithTag("OssoUI").GetComponentInChildren<TextMeshProUGUI>();

            passar = true;

        }
        if (passar)
        {
            conchasUI.text = conchas.ToString();
            coralUI.text = coral.ToString();
            ossosUI.text = ossos.ToString();
            calcioUI.text = calcio.ToString();
          
            passar = false;
        }

        if(mapa && !DoOnceMap)
        {
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuPause>().MapButtonInventario.SetActive(true);
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuPause>().hasMapa = true;
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuPause>().MapaItem.SetActive(true);

            DoOnceMap = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Conchas"))
        {
            //int i = Random.Range(0, MaxAmount);
            conchas += 1;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemGrab, transform.position);

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Coral"))
        {
            //int i = Random.Range(0, MaxAmount);
            coral += 1;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemGrab, transform.position);

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Ossos"))
        {
            //int i = Random.Range(0, MaxAmount);
            ossos += 1;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemGrab, transform.position);

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Calcio"))
        {
            //int i = Random.Range(0, MaxAmount);
            calcio += 1;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemGrab, transform.position);

            Destroy(collision.gameObject);
        }
    }

}
