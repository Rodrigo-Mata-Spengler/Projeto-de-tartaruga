using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience SFX")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music SFX")]
    [field: SerializeField] public EventReference Music { get; private set; }

    [field: Header("Interface SFX")]
    [field: SerializeField] public EventReference Confirmacao { get; private set; }
    [field: SerializeField] public EventReference Negacao { get; private set; }


    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference PlayerFootstep { get; private set; }
    [field: SerializeField] public EventReference AttackSound { get; private set; }
    [field: SerializeField] public EventReference DamageFeedback { get; private set; }
    [field: SerializeField] public EventReference Dash { get; private set; }
    [field: SerializeField] public EventReference Jump { get; private set; }
    [field: SerializeField] public EventReference Fall { get; private set; }
    [field: SerializeField] public EventReference ItemGrab { get; private set; }
    [field: SerializeField] public EventReference Compra { get; private set; }


    [field: Header("Mundo SFX")]
    [field: SerializeField] public EventReference Estactite { get; private set; }
    [field: SerializeField] public EventReference Praia { get; private set; }
    [field: SerializeField] public EventReference SacoDePancada { get; private set; }
    [field: SerializeField] public EventReference Save { get; private set; }
    [field: SerializeField] public EventReference SombraGrito{ get; private set; }
    [field: SerializeField] public EventReference SombraAreaIsolada { get; private set; }

    [field: Header("Puzzles SFX")]
    [field: SerializeField] public EventReference Circular { get; private set; }
    [field: SerializeField] public EventReference Conclusao { get; private set; }
    [field: SerializeField] public EventReference EstatuaGeniusA { get; private set; }
    [field: SerializeField] public EventReference EstatuaGeniusB { get; private set; }
    [field: SerializeField] public EventReference EstatuaGeniusC { get; private set; }
    [field: SerializeField] public EventReference EstatuaGeniusD { get; private set; }
    [field: SerializeField] public EventReference Painel { get; private set; }
    [field: SerializeField] public EventReference Quadrado { get; private set; }


    [field: Header("Ferreiro SFX")]
    [field: SerializeField] public EventReference Ferreiro { get; private set; }

    [field: Header("Bruxa SFX")]
    [field: SerializeField] public EventReference Bruxa { get; private set; }

    [field: Header("Fazendeiro SFX")]
    [field: SerializeField] public EventReference Fazendeiro { get; private set; }

    [field: Header("Anciao SFX")]
    [field: SerializeField] public EventReference Anciao { get; private set; }

    [field: Header("Caranguejo SFX")]
    [field: SerializeField] public EventReference FeedBackMorteCaranguejo { get; private set; }
    [field: SerializeField] public EventReference FeedBackDanoCaranguejo { get; private set; }
    [field: SerializeField] public EventReference FeedBackPincaCaranguejo { get; private set; }

    [field: Header("Guardiao SFX")]
    [field: SerializeField] public EventReference AtaqueGuardiao { get; private set; }
    [field: SerializeField] public EventReference FeedBackDanoGuardiao { get; private set; }
    [field: SerializeField] public EventReference DashGuardiao{ get; private set; }
    [field: SerializeField] public EventReference FalaGuardiao { get; private set; }
    [field: SerializeField] public EventReference PuloGuardiao { get; private set; }

    [field: Header("ZumbiTurtle SFX")]
    [field: SerializeField] public EventReference AtaqueZombi { get; private set; }
    [field: SerializeField] public EventReference FeedBackDanoZombi { get; private set; }
    [field: SerializeField] public EventReference FeedBackMorteZombi { get; private set; }
    [field: SerializeField] public EventReference MovimentoZombi { get; private set; }
    [field: SerializeField] public EventReference AcordandoZombi{ get; private set; }


    [field: Header("Mosca SFX")]
    [field: SerializeField] public EventReference FeedbackMorteMosca { get; private set; }
    [field: SerializeField] public EventReference FeedBackDanoMosca { get; private set; }
    [field: SerializeField] public EventReference rasanteMosca { get; private set; }
    [field: SerializeField] public EventReference SomMosca { get; private set; }
    [field: SerializeField] public EventReference TontaMosca { get; private set; }


    [field: Header("Ouriço SFX")]
    [field: SerializeField] public EventReference DisparoOurico { get; private set; }

    [field: Header("Planta Carnivora SFX")]
    [field: SerializeField] public EventReference FeedbackMortePlanta { get; private set; }
    [field: SerializeField] public EventReference FeedBackDanoPlanta { get; private set; }
    [field: SerializeField] public EventReference MordidaPlanta { get; private set; }
    [field: SerializeField] public EventReference MordidaErrarPlanta { get; private set; }
    

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found More than one Audio Manager in the scene");
        }
        instance = this;
    }
}
