using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeniusControler : MonoBehaviour
{
    [Header("interação")]
    private int ultimaInteracao = 0;
    private bool jogoIniciado = false;


    [Header("Blocos")]
    [SerializeField] private GameObject bloco1;
    [SerializeField] private GameObject bloco2;
    [SerializeField] private GameObject bloco3;
    [SerializeField] private GameObject bloco4;

    [Header("Sequencia")]
    [SerializeField] private int[] sequencia;
    [SerializeField] private int tamanhoSequencia;
    private int sequenciaatual = 0;
    private int sequenciaSuporte = 0;

    [SerializeField] private float tempoSequencia;
    private float tempoProxima;
    private bool tempusando = false;

    [Header("seuqncia player")]
    [SerializeField] private int[] sequenciaPlayer;
    private int playersequancia = 0;


    private bool aux = true;
    [SerializeField] private bool teste = false;
    [SerializeField] private float temptemp;

    private void Start()
    {
        sequencia = new int[tamanhoSequencia];
        sequenciaPlayer = new int[tamanhoSequencia];
    }

    private void Update()// a mudar
    {
        temptemp = Time.time;
        if (teste && sequenciaatual < sequencia.Length)
        {
            PlaySequencia();
        }
    }

    // iniciar o puzzle interagindo com o main
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Interacao") && jogoIniciado == false)
        {
            CriarSequencia();

            jogoIniciado = true;

            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    // criar sequencia a ser seguida
    private void CriarSequencia()
    {
        for (int i = 0; i < tamanhoSequencia; i++)
        {
            sequencia[i] = Random.Range(1, 5);
        }
    }

    // mostrar sequencia em partes, começando com um character, e acresentando mais um conforme o jogo caminha
    private void PlaySequencia()
    {
        if (tempoProxima <= Time.time && tempusando == false &&  sequenciaatual < sequencia.Length)
        {
            tempoProxima = Time.time + tempoSequencia;
            switch (sequencia[sequenciaSuporte])
            {
                case 1:
                    bloco1.GetComponent<SpriteRenderer>().color = Color.blue;
                    tempusando = true;
                    break;
                case 2:
                    bloco2.GetComponent<SpriteRenderer>().color = Color.red;
                    tempusando = true;
                    break;
                case 3:
                    bloco3.GetComponent<SpriteRenderer>().color = Color.yellow;
                    tempusando = true;
                    break;
                case 4:
                    bloco4.GetComponent<SpriteRenderer>().color = Color.magenta;
                    tempusando = true;
                    break;
            }
            sequenciaSuporte++;
            sequenciaatual++;
        }
        else if (tempoProxima <= Time.time && tempusando == true)
        {
            tempoProxima = Time.time + .5f;
            bloco1.GetComponent<SpriteRenderer>().color = Color.white;
            tempusando = false;

            bloco2.GetComponent<SpriteRenderer>().color = Color.white;
            tempusando = false;

            bloco3.GetComponent<SpriteRenderer>().color = Color.white;
            tempusando = false;

            bloco4.GetComponent<SpriteRenderer>().color = Color.white;
            tempusando = false;

        }else if (sequenciaatual >= sequencia.Length)
        {
            sequenciaatual = 0;// a mudar
        }
    }

    // recebe sequencia do player
    public void InteracaoObjeto(int interacao)
    {
        if (aux)
        {
            sequenciaPlayer[playersequancia] = interacao;
            playersequancia++;
           // aux = false;
        }
        if (Input.GetKeyUp(KeyCode.F) && aux == false)
        {
            aux = true;
        }
    }

    // verifica se a sequencia esta certa
    // caso erre, é a secção do jogo acaba
    // caso acerte, segue para proxima sequencia ou completa o jogo


}
