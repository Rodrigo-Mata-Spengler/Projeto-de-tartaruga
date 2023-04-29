using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensInventory : MonoBehaviour
{
    public int conchas;//Shells
    public int coral; //coral
    public int calcio; //calcium
    public int ossos; // bones

    [Space]
    public int MaxAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Conchas"))
        {
            int i = Random.Range(0, MaxAmount);
            conchas += i;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Coral"))
        {
            int i = Random.Range(0, MaxAmount);
            coral += i;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Ossos"))
        {
            int i = Random.Range(0, MaxAmount);
            ossos += i;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Calcio"))
        {
            int i = Random.Range(0, MaxAmount);
            calcio += i;
            Destroy(collision.gameObject);
        }
    }
}
