using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PuzzleGenius { desativado, mostrandoPadrao, esperandoInput, verificando, acertou, errou};
public class GeniusControler : MonoBehaviour
{
    [Header("interação")]
    [SerializeField] private PuzzleGenius status = PuzzleGenius.desativado;
    private bool playerIn = false;

    [SerializeField] private float frequenciaErro = 0;
    private float frequenciaProximoErro = 0;
    private bool piscandovermelho = true;

    [Header("Sprintes Main")]
    [SerializeField] private Sprite mainAmarelo;
    [SerializeField] private Sprite mainAzul;
    [SerializeField] private Sprite mainRoxo;
    [SerializeField] private Sprite mainVerde;
    [SerializeField] private Sprite mainAcerto;
    [SerializeField] private Sprite mainErro;
    [SerializeField] private Sprite mainApagado;
    private SpriteRenderer rend;

    [Header("Blocos")]
    [SerializeField] private GameObject amarelo;
    [SerializeField] private GameObject azul;
    [SerializeField] private GameObject roxo;
    [SerializeField] private GameObject verde;

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

        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()// a mudar
    {
        if (Input.GetButtonDown("Interacao") && status == PuzzleGenius.desativado && playerIn)
        {
            CriarSequencia();

            //this.GetComponent<SpriteRenderer>().color = Color.green;
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
                ErrouPiscando();
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

                    rend.sprite = mainAmarelo;
                    break;
                case 2:

                    rend.sprite = mainAzul;
                    break;
                case 3:

                    rend.sprite = mainRoxo;
                    break;
                case 4:

                    rend.sprite = mainVerde;
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
        amarelo.GetComponent<InteragirObjeto>().BlocoApagado();

        azul.GetComponent<InteragirObjeto>().BlocoApagado();

        roxo.GetComponent<InteragirObjeto>().BlocoApagado();

        verde.GetComponent<InteragirObjeto>().BlocoApagado();

        rend.sprite = mainApagado;
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
        amarelo.GetComponent<InteragirObjeto>().permitirInteracao = true;
        azul.GetComponent<InteragirObjeto>().permitirInteracao = true;
        roxo.GetComponent<InteragirObjeto>().permitirInteracao = true;
        verde.GetComponent<InteragirObjeto>().permitirInteracao = true;
    }
    private void BloquearInteracao()
    {
        amarelo.GetComponent<InteragirObjeto>().permitirInteracao = false;
        azul.GetComponent<InteragirObjeto>().permitirInteracao = false;
        roxo.GetComponent<InteragirObjeto>().permitirInteracao = false;
        verde.GetComponent<InteragirObjeto>().permitirInteracao = false;
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
        BlocosDefault();

        frequenciaProximoErro = frequenciaErro + Time.time;

        status = PuzzleGenius.errou;

        sequencia = new int[tamanhoSequencia];
        sequenciaPlayer = new int[tamanhoSequencia];

        sequenciaSuporte = 0;
        sequenciaAtual = 1;
        playersequancia = 0;
    }

    // caso acerte, segue para proxima sequencia ou completa o jogo
    private void AcertouSequencia()
    {
        status = PuzzleGenius.acertou;
        if (sequenciaAtual == sequencia.Length)
        {
            rend.sprite = mainAcerto;

            amarelo.GetComponent<InteragirObjeto>().BlocoAceso();

            azul.GetComponent<InteragirObjeto>().BlocoAceso();

            roxo.GetComponent<InteragirObjeto>().BlocoAceso();

            verde.GetComponent<InteragirObjeto>().BlocoAceso();

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

    private void ErrouPiscando()
    {
        if (piscandovermelho)
        {
            rend.sprite = mainErro;
            if (frequenciaProximoErro <= Time.time)
            {
                frequenciaProximoErro = frequenciaErro + Time.time;
                piscandovermelho = false;
            }

        }
        else
        {
            rend.sprite = mainApagado;
            if (frequenciaProximoErro <= Time.time)
            {
                frequenciaProximoErro = frequenciaErro + Time.time;
                piscandovermelho = true;
            }
        }
    }
}
