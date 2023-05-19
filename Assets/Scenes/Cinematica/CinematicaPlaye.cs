using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicaPlaye : MonoBehaviour
{
    [SerializeField] private VideoPlayer video;

    [SerializeField]private string proximaScena = "Main Menu";

    private bool ctrl = false;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            video.Pause();
            SceneManager.LoadScene(proximaScena);
        }

        if (!video.isPlaying && ctrl)
        {
            SceneManager.LoadScene(proximaScena);
        }
    }

    private void LateUpdate()
    {
        video.Play();
        if (video.isPlaying)
        {
            ctrl = true;
        }
    }
}
