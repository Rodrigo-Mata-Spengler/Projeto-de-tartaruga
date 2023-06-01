using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptVitoria : MonoBehaviour
{
    [SerializeField] private Animator anim_fade_white;

    [SerializeField] private float tempo_espera = 0;
    private float tempo_para_espera = 0;

    private bool ctrl = false;

    private void Update()
    {
        if (tempo_para_espera <= Time.time && ctrl)
        {
            SceneManager.LoadScene("Tela Vitoria temp");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tempo_para_espera = tempo_espera + Time.time;
            anim_fade_white.SetTrigger("fade");
            ctrl = true;
        }
    }



}
