using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum StatusMapa { naoDescoberto, descoberto, emArea};
public class AreaStatus : MonoBehaviour
{
    [SerializeField] private string nomeCena;
    [SerializeField] public bool descoberto = false;
    [SerializeField] private StatusMapa status = StatusMapa.naoDescoberto;
    [SerializeField] private Color corNaoAtivo = Color.gray;
    [SerializeField] private Color corAtivo = Color.white;
    [SerializeField] private Color corNaoDescoberto = Color.black;
    [SerializeField] private GameObject totemPlayer;

    
    
    private void VerificarCena()
    {
        if (SceneManager.GetActiveScene().name == nomeCena)
        {
            if (descoberto == false)
            {
                descoberto = true;
            }
            status = StatusMapa.emArea;
        }
        else if(descoberto)
        {
            status = StatusMapa.descoberto;
        }
    }

    private void Update()
    {
        VerificarCena();

        switch (status)
        {
            case StatusMapa.naoDescoberto:
                gameObject.GetComponent<Image>().color = corNaoDescoberto;
                if (descoberto)
                {
                    status = StatusMapa.descoberto;
                }
                break;
            case StatusMapa.descoberto:
                gameObject.GetComponent<Image>().enabled = true;
                gameObject.GetComponent<Image>().color = corNaoAtivo;
                break;
            case StatusMapa.emArea:
                gameObject.GetComponent<Image>().enabled = true;
                gameObject.GetComponent<Image>().color = corAtivo;
                totemPlayer.transform.position = transform.position;
                break;
        }
    }
}
