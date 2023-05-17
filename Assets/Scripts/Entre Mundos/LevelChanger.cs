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
        if (conetion == Conections.activeConetion && Conections.wasConetion)
        {
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

        yield return new WaitForSeconds(transisionTime);

        Conections.activeConetion = conetion;
        SceneManager.LoadScene(cena);
        Conections.wasConetion = true;
    }
}
