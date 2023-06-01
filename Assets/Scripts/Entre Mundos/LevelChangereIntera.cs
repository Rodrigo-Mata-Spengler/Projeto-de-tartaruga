using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangereIntera : MonoBehaviour
{
    [SerializeField] private Conections conetion;

    [SerializeField] private string targetSceneName;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject player;

    [SerializeField] private Animator fade;

    [SerializeField] private float transisionTime = 1;

    private bool interacao = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (conetion == Conections.activeConetion && Conections.wasConetion)
        {
            player.transform.position = spawnPoint.position;

            Conections.wasConetion = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interacao") && interacao)
        {
            StartCoroutine(LoadLevel(targetSceneName));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interacao = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interacao = false;
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
