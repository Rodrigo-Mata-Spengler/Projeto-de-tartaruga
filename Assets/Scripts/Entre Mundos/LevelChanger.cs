using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Conections conetion;

    [SerializeField] private string targetSceneName;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject player;

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
            Conections.activeConetion = conetion;
            SceneManager.LoadScene(targetSceneName);
            Conections.wasConetion = true;
        }
    }
}
