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


    [Header("Area")]
    [SerializeField] private MusicArea MusicAreaToGoTo;
    private void Update()
    {
       /* if (Input.anyKeyDown)
        {
            MudarMusica();
            video.Pause();
            SceneManager.LoadScene(proximaScena);
        }*/

        if (!video.isPlaying && ctrl)
        {
            MudarMusica();
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

    public void MudarMusica()
    {
        AudioManager.instance.SetMusicArea(MusicAreaToGoTo);
    }
}
