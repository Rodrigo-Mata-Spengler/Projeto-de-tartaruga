using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

enum InteraMode { colisao, interacao}
public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Conections conetion;

    [SerializeField] private InteraMode modo = InteraMode.colisao;

    [SerializeField] private GameObject fInteracao;

    [SerializeField] private string targetSceneName;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject player;

    [SerializeField] private Animator fade;

    [SerializeField] private float transisionTime = 1;

    private void Start()
    {
        fInteracao.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        if (conetion == Conections.activeConetion && Conections.wasConetion)
        {
            player.transform.position = spawnPoint.position;

            Conections.wasConetion = false;
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (modo)
        {
            case InteraMode.colisao:
                if (collision.gameObject.CompareTag("Player"))
                {
                    StartCoroutine(LoadLevel(targetSceneName));
                }
                break;
            case InteraMode.interacao:
                if (Input.GetButton("Interacao"))
                {
                    fInteracao.SetActive(true);
                    StartCoroutine(LoadLevel(targetSceneName));
                }
                break;
        }
    }
   
    

    IEnumerator LoadLevel(string cena)
    {
        fade.SetTrigger("Start");

        Conections.activeConetion = conetion;
        Conections.wasConetion = true;
        yield return new WaitForSeconds(transisionTime);

        SceneManager.LoadScene(cena);
        
    }

}
