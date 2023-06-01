using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum habilidadeStatus { dash, magia, pulo_duplo}
public class CeloVila : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject player;

    [SerializeField] private bool status = true;

    [SerializeField] private habilidadeStatus habilidade;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            switch (habilidade)
            {
                case habilidadeStatus.dash:
                    if (player.GetComponent<Dash>().enabled)
                    {
                        anim.SetBool("quebrado", true);
                        status = false;
                    }
                    break;
                case habilidadeStatus.magia:
                    if (player.GetComponent<Blast>().enabled)
                    {
                        anim.SetBool("quebrado", true);
                        status = false;
                    }
                    break;
                case habilidadeStatus.pulo_duplo:
                    if (player.GetComponent<PlayerMovement>().haveDoubleJump)
                    {
                        anim.SetBool("quebrado", true);
                        status = false;
                    }
                    break;
            }
        }
    }
}
