using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Conections conetion;

    [SerializeField] private string targetSceneName;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject player;

    [SerializeField] private Animator fade;

    [SerializeField] private float transisionTime = 1;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("Player não encontrado");
        }

        if (conetion == Conections.activeConetion && Conections.wasConetion)
        {

            Debug.Log("Player teleportado");
            player.transform.position = spawnPoint.position;

            Conections.wasConetion = false;
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadLevel(targetSceneName));
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
