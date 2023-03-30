using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PuzzleGenius { desativado, mostrandoPadrao, esperandoInput, verificando, acertou, errou};
public class GeniusControler : MonoBehaviour
{
    [Header("interação")]
    [SerializeField] private PuzzleGenius status = PuzzleGenius.desativado;
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
    private bool tocandoSequencia = false;

    [SerializeField] private float tempoSequencia;

    [Header("seuqncia player")]
    [SerializeField] private int[] sequenciaPlayer;
    private int playersequancia = 0;
    [SerializeField] private float tempoEspera = 0;
    private float tempoEsperavariavel = 0;


    [SerializeField] public bool jogoFinalizado = false;

    private void Start()
    {
        sequencia = new int[tamanhoSequencia];
        sequenciaPlayer = new int[tamanhoSequencia];
        sequenciaAtual = 1;
    }

    private void Update()// a mudar
    {
        if (Input.GetButtonDown("Interacao") && status == PuzzleGenius.desativado && playerIn)
        {
            CriarSequencia();

            this.GetComponent<SpriteRenderer>().color = Color.green;
            status = PuzzleGenius.mostrandoPadrao;
        }


        switch (status)
        {
            case PuzzleGenius.mostrandoPadrao:
                
                if (!tocandoSequencia)
                {
                    StartCoroutine(PlaySequencia());
                }
                
                break;
            case PuzzleGenius.esperandoInput:
                StopCoroutine(PlaySequencia());
                PermitirInteracao();
                break;
            case PuzzleGenius.errou:
                break;
            case PuzzleGenius.verificando:
                BloquearInteracao();
                TomarDecisao();
                break;
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
    private IEnumerator PlaySequencia()
    {
        sequenciaSuporte = 0;
        while (sequenciaSuporte < sequenciaAtual)
        {
            tocandoSequencia = true;
            yield return new WaitForSeconds(1);
            switch (sequencia[sequenciaSuporte])
            {
                case 1:
                    bloco1.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case 2:
                    bloco2.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case 3:
                    bloco3.GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
                case 4:
                    bloco4.GetComponent<SpriteRenderer>().color = Color.magenta;
                    break;
            }
            sequenciaSuporte++;
            yield return new WaitForSeconds(tempoSequencia);

            BlocosDefault();

            yield return new WaitForSeconds(.5f);
        }
        status = PuzzleGenius.esperandoInput;
        tocandoSequencia = false;
        yield break;
    }

    private void BlocosDefault()
    {
        bloco1.GetComponent<SpriteRenderer>().color = Color.white;

        bloco2.GetComponent<SpriteRenderer>().color = Color.white;

        bloco3.GetComponent<SpriteRenderer>().color = Color.white;

        bloco4.GetComponent<SpriteRenderer>().color = Color.white;
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
            BlocosDefault();
        }

        if (playersequancia >= sequenciaAtual)
        {
            Debug.Log(playersequancia);
            Debug.Log(sequenciaAtual);
            status = PuzzleGenius.verificando;
        }
    }

    private void PermitirInteracao()
    {
        bloco1.GetComponent<InteragirObjeto>().permitirInteracao = true;
        bloco2.GetComponent<InteragirObjeto>().permitirInteracao = true;
        bloco3.GetComponent<InteragirObjeto>().permitirInteracao = true;
        bloco4.GetComponent<InteragirObjeto>().permitirInteracao = true;
    }
    private void BloquearInteracao()
    {
        bloco1.GetComponent<InteragirObjeto>().permitirInteracao = false;
        bloco2.GetComponent<InteragirObjeto>().permitirInteracao = false;
        bloco3.GetComponent<InteragirObjeto>().permitirInteracao = false;
        bloco4.GetComponent<InteragirObjeto>().permitirInteracao = false;
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

    private void TomarDecisao()
    {
        if (VerificarSequencia())
        {
            AcertouSequencia();
        }
        else
        {
            ErrouSequencia();
        }
    }
    // caso erre, é a secção do jogo acaba
    private void ErrouSequencia()
    {
        status = PuzzleGenius.errou;
        BlocosDefault();
        this.GetComponent<SpriteRenderer>().color = Color.red;

        sequencia = new int[tamanhoSequencia];
        sequenciaPlayer = new int[tamanhoSequencia];

        sequenciaSuporte = 0;
        sequenciaAtual = 1;
        playersequancia = 0;
        status = PuzzleGenius.desativado;
    }

    // caso acerte, segue para proxima sequencia ou completa o jogo
    private void AcertouSequencia()
    {
        status = PuzzleGenius.acertou;
        if (sequenciaAtual == sequencia.Length)
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;

            bloco1.GetComponent<SpriteRenderer>().color = Color.blue;
            bloco2.GetComponent<SpriteRenderer>().color = Color.red;
            bloco3.GetComponent<SpriteRenderer>().color = Color.yellow;
            bloco4.GetComponent<SpriteRenderer>().color = Color.magenta;

            jogoFinalizado = true;
        }
        else
        {
            sequenciaAtual++;
            sequenciaSuporte = 0;
            playersequancia = 0;
            sequenciaPlayer = new int[tamanhoSequencia];
            BlocosDefault();
            status = PuzzleGenius.mostrandoPadrao;

        }

    }
}
