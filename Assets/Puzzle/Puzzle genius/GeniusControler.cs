using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PuzzleGenius { desativado, mostrandoPadrao, esperandoInput, acertou, errou};
public class GeniusControler : MonoBehaviour
{
    [Header("interação")]
    [SerializeField] private PuzzleGenius status = PuzzleGenius.desativado;
    private bool jogoIniciado = false;
    private bool playerIn = false;

    [Header("Blocos")]
    [SerializeField] private GameObject bloco1;
    [SerializeField] private GameObject bloco2;
    [SerializeField] private GameObject bloco3;
    [SerializeField] private GameObject bloco4;

    [Header("Sequencia")]
    [SerializeField] private int[] sequencia;
    [SerializeField] private int tamanhoSequencia;
    [SerializeField] private int sequenciaAtual = 1;
    private int sequenciaSuporte = 0;

    [SerializeField] private float tempoSequencia;
    private float tempoProxima;
    private bool tempusando = false;

    [Header("seuqncia player")]
    [SerializeField] private int[] sequenciaPlayer;
    private int playersequancia = 0;
    [SerializeField] private float tempoEspera = 0;
    private float tempoEsperavariavel = 0;


    [SerializeField] private bool teste = false;

    private void Start()
    {
        sequencia = new int[tamanhoSequencia];
        sequenciaPlayer = new int[tamanhoSequencia];
    }

    private void Update()// a mudar
    {

        switch (status)
        {
            case PuzzleGenius.mostrandoPadrao:
                PlaySequencia();
                break;
            case PuzzleGenius.esperandoInput:
                break;
            case PuzzleGenius.errou:
                break;
            case PuzzleGenius.acertou:
                break;
        }


        if (Input.GetButtonDown("Interacao") && status == PuzzleGenius.desativado && playerIn)
        {
            CriarSequencia();

            this.GetComponent<SpriteRenderer>().color = Color.green;
            status = PuzzleGenius.mostrandoPadrao;
            tempoProxima = Time.time + tempoSequencia;
        }
    }

    // iniciar o puzzle interagindo com o main
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIn = false;
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
        if (tempoProxima <= Time.time)
        {
            if (tempusando == false && sequenciaSuporte <= sequenciaAtual)
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

            }
            else if (tempusando == true)
            {
                tempoProxima = Time.time + .5f;
                bloco1.GetComponent<SpriteRenderer>().color = Color.white;

                bloco2.GetComponent<SpriteRenderer>().color = Color.white;

                bloco3.GetComponent<SpriteRenderer>().color = Color.white;

                bloco4.GetComponent<SpriteRenderer>().color = Color.white;
                tempusando = false;

            }

            /*if (sequenciaSuporte >= sequenciaAtual)
            {
                bloco1.GetComponent<SpriteRenderer>().color = Color.white;

                bloco2.GetComponent<SpriteRenderer>().color = Color.white;

                bloco3.GetComponent<SpriteRenderer>().color = Color.white;

                bloco4.GetComponent<SpriteRenderer>().color = Color.white;

                status = PuzzleGenius.esperandoInput;
                sequenciaPlayer = new int[tamanhoSequencia];
            }*/
        }
    }

    // recebe sequencia do player
    public void InteracaoObjeto(int interacao)
    {
        if (tempoEsperavariavel < Time.time)
        {
            tempoEsperavariavel = Time.time + tempoEspera;

            sequenciaPlayer[playersequancia] = interacao;
            playersequancia++;
        }
        else
        {
            bloco1.GetComponent<SpriteRenderer>().color = Color.white;

            bloco2.GetComponent<SpriteRenderer>().color = Color.white;

            bloco3.GetComponent<SpriteRenderer>().color = Color.white;

            bloco4.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // verifica se a sequencia esta certa
    private bool VerificarSequencia()
    {
        for (int i = 0; i < sequenciaAtual; i++)
        {
            if (sequenciaPlayer[i] != sequencia[i])
            {
                return false;
            }
        }

        return true;
    }

    // recebe verificação e toma a descisão 
    // caso erre, é a secção do jogo acaba
    private void ErrouSequencia()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;

        sequencia = new int[tamanhoSequencia];
        sequenciaPlayer = new int[tamanhoSequencia];

        sequenciaSuporte = 0;
        sequenciaAtual = 0;

        jogoIniciado = true;
    }

    // caso acerte, segue para proxima sequencia ou completa o jogo
    private void AcertouSequencia()
    {
        if (sequenciaAtual == sequencia.Length)
        {
            //terminou o puzzle
        }
        else
        {
            sequenciaAtual++;
        }
    }


}
