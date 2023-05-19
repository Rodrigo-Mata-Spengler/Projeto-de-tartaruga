using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SombraStatus { idle, ataque1, ataque2, ataque3, ataque4, ataque5, ataque6, teste, desativado, ativado};
enum DirecaoAtaque { direita, esquerda, none};
public class SombraBehaviour : MonoBehaviour
{
    [Header("Referencia do Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private string playerTag;

    [Header("Status")]
    [SerializeField] private SombraStatus status = SombraStatus.desativado; //mostra o status da sombra atual
    [SerializeField] private float vidaMaxima;

    [Header("Sombra Brain")]
    [SerializeField] private bool livreArbitrio = true;//defini se a sombra pode ou não fazer suas próprias desisões 
    private int ataque = 0;
    [SerializeField] private int quantidadeAtaques = 0;
    private int ataquesAtual = 0;

    [Header("idle")]
    [SerializeField] private float tempoIdle = 0;//tempo de duração do idle
    private float tempoProximoIdle = 0;

    [Header("Ataque 1")]
    //Pinca 1 :
    [SerializeField] private GameObject pinca1;//O game object da pinça
    [SerializeField] private Vector3 posInicialPinca1;//posição inicial do ataque
    [SerializeField] private Vector3 posFinalPinca1;//posição final do ataque
    private bool descendoPinca1;//controle de direção 
    //Pinca 2 :
    [SerializeField] private GameObject pinca2;//O game object da pinça
    [SerializeField] private Vector3 posInicialPinca2;//posição inicial do ataque
    [SerializeField] private Vector3 posFinalPinca2;//posição final do ataque
    private bool descendoPinca2;//controle de direção 
    //Espera para alternar o o ataque da pinça 2
    [SerializeField] private float tempoOffSet = 0;//OffSet de tempo entre as duas pinças
    private float tempoProximoOffset = 0;
    //geral ataque 1
    [SerializeField] private float velocidadeAtaque1 = 0;//velocidade do ataque 
    [SerializeField] private float quantidadeAtaque1 = 0;//qunatos ataque devem ser
    private float ataque1Atual = 0;

    [Header("Ataque 2")]
    //pinça 3
    [SerializeField] private GameObject pinca3;//O game object da pinça
    [SerializeField] private Vector3 posInicialPinca3;//posição inicial do ataque
    [SerializeField] private Vector3 posIntermediariaPinca3;//posição de espera do ataque
    [SerializeField] private Vector3 posfinalPinca3;//posição final do ataque
    //Pinça 4
    [SerializeField] private GameObject pinca4;//O game object da pinça
    [SerializeField] private Vector3 posInicialPinca4;//posição inicial do ataque
    [SerializeField] private Vector3 posIntermediariaPinca4;//posição de espera do ataque
    [SerializeField] private Vector3 posfinalPinca4;//posição final do ataque
    //tempo de espera na posição intermediaria antes do ataque
    [SerializeField] private float tempoEsperaAtaque2 = 0;
    private float tempoEsperaProximoAtaque2 = 0;
    //geral ataque 2
    [SerializeField] private float velocidadePreparoAtaque2 = 0;
    [SerializeField] private float velocidadeAtaque2 = 0;
    [SerializeField] private bool ataque2Preparado = true;//defini se o ataque esta preparado
    [SerializeField] private bool ataque2Voltando = false;//defini se o ataque ja acabou
    [SerializeField] private DirecaoAtaque direcao = DirecaoAtaque.none;

    [Header("Ataque 3")]
    //pinça 5
    [SerializeField] private GameObject pinca5;//O game object da pinça
    [SerializeField] private Vector3 posInicialPinca5;//posição inicial do ataque
    [SerializeField] private Vector3 posIntermediariaPinca5;//posição de espera do ataque
    [SerializeField] private Vector3 posfinalPinca5;//posição final do ataque
    //pinça 6
    [SerializeField] private GameObject pinca6;//O game object da pinça
    [SerializeField] private Vector3 posInicialPinca6;//posição inicial do ataque
    [SerializeField] private Vector3 posIntermediariaPinca6;//posição de espera do ataque
    [SerializeField] private Vector3 posfinalPinca6;//posição final do ataque
    //tempo de espera na posição intermediaria antes do ataque
    [SerializeField] private float tempoEsperaAtaque3 = 0;
    private float tempoEsperaProximoAtaque3 = 0;
    //geral ataque 3
    [SerializeField] private float velocidadePreparoAtaque3 = 0;
    [SerializeField] private float velocidadeAtaque3 = 0;
    [SerializeField] private bool ataque3Preparado = true;//defini se o ataque esta preparado
    [SerializeField] private bool ataque3Voltando = false;//defini se o ataque ja acabou

    [Header("Ataque 4")]
    //pinça 7
    [SerializeField] private GameObject pinca7;//O game object da pinça
    [SerializeField] private Vector3 posInicialPinca7;//posição inicial do ataque
    [SerializeField] private Vector3 posfinalPinca7;//posição final do ataque
    //Onda 1
    [SerializeField] private GameObject Onda1;//O game object da pinça
    [SerializeField] private Vector3 posInicialOnda1;//posição inicial do ataque
    [SerializeField] private Vector3 posIntermediariaOnda1;//posição inicial do ataque
    [SerializeField] private Vector3 posfinalOnda1;//posição final do ataque
    //Onda 2
    [SerializeField] private GameObject Onda2;//O game object da pinça
    [SerializeField] private Vector3 posInicialOnda2;//posição inicial do ataque
    [SerializeField] private Vector3 posIntermediariaOnda2;//posição inicial do ataque
    [SerializeField] private Vector3 posfinalOnda2;//posição final do ataque
    //geral ataque 4
    [SerializeField] private float velocidadeAtaque4 = 0;
    [SerializeField] private float velocidadeOndaAtaque4 = 0;
    private bool ondaAtaque = false;
    [SerializeField] private float tempoRetornoAtaque4 = 0;//tempo para a pinça começar a voltar
    private float tempoParaRetornoAtaque4 = 0;

    [Header("Ataque 5")]
    //tentaculo 1
    [SerializeField] private GameObject tentaculo1;//o Game objevt do tentaculo
    [SerializeField] private Animator animTentaculo1;//O animador do tentaculo
    [SerializeField] private Vector3 posTentaculo1;//posição inicial do tentaculo
    [SerializeField] private Vector3 posTentaculo1Final;//posição inicial do tentaculo
    [SerializeField] private Vector3 posInicialTentaculo1;//poição inicial do dano do tentaculo
    [SerializeField] private Vector3 posFinalTentaculo1;//poição final do dano do tentaculo
    //tentaculo 2
    [SerializeField] private GameObject tentaculo2;//o Game objevt do tentaculo
    [SerializeField] private Animator animTentaculo2;//O animador do tentaculo
    [SerializeField] private Vector3 posTentaculo2;//posição inicial do tentaculo
    [SerializeField] private Vector3 posTentaculo2Final;//posição inicial do tentaculo
    [SerializeField] private Vector3 posInicialTentaculo2;//poição inicial do dano do tentaculo
    [SerializeField] private Vector3 posFinalTentaculo2;//poição final do dano do tentaculo
    //tentaculo 3
    [SerializeField] private GameObject tentaculo3;//o Game objevt do tentaculo
    [SerializeField] private Animator animTentaculo3;//O animador do tentaculo
    [SerializeField] private Vector3 posTentaculo3;//posição inicial do tentaculo
    [SerializeField] private Vector3 posTentaculo3Final;//posição inicial do tentaculo
    [SerializeField] private Vector3 posInicialTentaculo3;//poição inicial do dano do tentaculo
    [SerializeField] private Vector3 posFinalTentaculo3;//poição final do dano do tentaculo
    //tentaculo 4
    [SerializeField] private GameObject tentaculo4;//o Game objevt do tentaculo
    [SerializeField] private Animator animTentaculo4;//O animador do tentaculo
    [SerializeField] private Vector3 posTentaculo4;//posição inicial do tentaculo
    [SerializeField] private Vector3 posTentaculo4Final;//posição inicial do tentaculo
    [SerializeField] private Vector3 posInicialTentaculo4;//poição inicial do dano do tentaculo
    [SerializeField] private Vector3 posFinalTentaculo4;//poição final do dano do tentaculo
    //Geral ataque 5
    [SerializeField] private float velocidadeAtaque5 = 0;
    [SerializeField] private float velocidadePreparoAtaque5 = 0;
    [SerializeField]private float tempoEsperaAtaque5 = 0;
    private float tempoProximaEsperaAtaque5 = 0;
    [SerializeField] private float tempoDuracaoAtaque5 = 0;
    private float tempoProximaDuracaoAtaque5 = 0;
    private bool ctrlAtaque5 = false;
    private bool posControle = false;

    [Header("Ataque 6")]
    //pinca8
    [SerializeField] private GameObject pinca8;//O game object da pinça
    [SerializeField] private Vector3 posInicialPinca8;
    [SerializeField] private float posInicialYPinca8 = 0;
    [SerializeField] private float posIntermediarialYPinca8 = 0;
    [SerializeField] private float posFinalYPinca8 = 0;
    //geral ataque 8
    [SerializeField] private float velocidadeAtaque6 = 0;
    [SerializeField] private float velocidadePrepararAtaque6 = 0;
    [SerializeField] private float tempoMiraAtaque6 = 0;
    private float tempoProximaMiraAtaque6 = 0;
    [SerializeField] private float tempoTravadoAtaque6 = 0;
    private float tempoProximoTravadoAtaque6 = 0;
    private bool preparaAtaque6 = false;
    private bool acaoAtaque6 = false;
    private bool voltarAtaque6 = false;



    //Metodos gerais da Sombra
    private void Awake()
    {
        VidaBossSombra.vidaAtual = vidaMaxima;
    }
    private void Start()
    {
        SetUpAtaque1();
        SetUpAtaque2();
        SetUpAtaque3();
        SetUpAtaque4();
        SetUpAtaque5();
        ShutDownAtaque5();
        SetUpAtaque6();
        ataque1Atual = 0;

        status = SombraStatus.desativado;
    }
    private void Update()
    {
        switch (status)
        {
            case SombraStatus.idle:
                Idle();
                break;
            case SombraStatus.ataque1:
                Ataque1();
                break;
            case SombraStatus.ataque2:
                Ataque2();
                break;
            case SombraStatus.ataque3:
                Ataque3();
                break;
            case SombraStatus.ataque4:
                Ataque4();
                break;
            case SombraStatus.ataque5:
                Ataque5();
                break;
            case SombraStatus.ataque6:
                Ataque6();
                break;
            case SombraStatus.teste:
                SetUpIdle();
                SetUpAtaque1();
                SetUpAtaque2();
                SetUpAtaque3();
                SetUpAtaque4();
                SetUpAtaque5();
                SetUpAtaque6();
                break;
            case SombraStatus.desativado:
                Desativado();
                break;
            case SombraStatus.ativado:
                ativado();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            status = SombraStatus.ativado;
            player = collision.gameObject;
        }
    }

    private void SombraBrain()
    {

        if (livreArbitrio)
        {
            if (ataquesAtual > quantidadeAtaques)
            {
                ataque = 6;
                ataquesAtual = 0;
            }
            else
            {
                ataque = Random.Range(0, 6);
                ataquesAtual++;
            }
            
            switch (ataque)
            {
                case 0:
                    SetUpIdle();
                    status = SombraStatus.idle;
                    break;
                case 1:
                    SetUpAtaque1();
                    status = SombraStatus.ataque1;
                    break;
                case 2:
                    SetUpAtaque2();
                    status = SombraStatus.ataque2;
                    break;
                case 3:
                    SetUpAtaque3();
                    status = SombraStatus.ataque3;
                    break;
                case 4:
                    SetUpAtaque4();
                    status = SombraStatus.ataque4;
                    break;
                case 5:
                    SetUpAtaque5();
                    status = SombraStatus.ataque5;
                    break;
                case 6:
                    SetUpAtaque6();
                    status = SombraStatus.ataque6;
                    break;
            }
        }
    }

    private void Desativado()
    {
        pinca1.SetActive(false);
        pinca2.SetActive(false);
        pinca3.SetActive(false);
        pinca4.SetActive(false);
        pinca5.SetActive(false);
        pinca6.SetActive(false);
        pinca7.SetActive(false);
        pinca8.SetActive(false);

        tentaculo1.SetActive(false);
        tentaculo2.SetActive(false);
        tentaculo3.SetActive(false);
        tentaculo4.SetActive(false);

        Onda1.SetActive(false);
        Onda2.SetActive(false);
    }

    private void ativado()
    {
        pinca1.SetActive(true);
        pinca2.SetActive(true);
        pinca3.SetActive(true);
        pinca4.SetActive(true);
        pinca5.SetActive(true);
        pinca6.SetActive(true);
        pinca7.SetActive(true);
        pinca8.SetActive(true);

        tentaculo1.SetActive(true);
        tentaculo2.SetActive(true);
        tentaculo3.SetActive(true);
        tentaculo4.SetActive(true);

        Onda1.SetActive(true);
        Onda2.SetActive(true);

        GetComponent<BoxCollider2D>().enabled = false;

        SombraBrain();

    }

    //Metodos relacionados a idle
    private void SetUpIdle() //Prepara o tempo do idle
    {
        tempoProximoIdle = tempoIdle + Time.time;
    }

    private void Idle()//executa o idle, depois chama o Brain
    {
        if (tempoProximoIdle <= Time.time)
        {
            SombraBrain();
        }
    }

    //Metodos relacionados ao Ataque 1
    private void SetUpAtaque1()
    {
        ataque1Atual = 0;

        tempoProximoOffset = tempoOffSet + Time.time;

        pinca1.transform.position = posInicialPinca1;
        descendoPinca1 = true;

        pinca2.transform.position = posInicialPinca2;
        descendoPinca2 = true;

    }

    private void Ataque1()
    {
        if (ataque1Atual != quantidadeAtaque1)
        {
            Pinca1Ataque();

            if (tempoProximoOffset <= Time.time)
            {
                Pinca2Ataque();
            }
        }
        else
        {
            Pinca1AtaqueSobe();
            SombraBrain();
        }

        
    }

    private void Pinca1Ataque()
    {
        if (descendoPinca1)
        {
            pinca1.transform.position = Vector3.MoveTowards(pinca1.transform.position, posFinalPinca1, velocidadeAtaque1 * Time.deltaTime);

            if (pinca1.transform.position == posFinalPinca1)
            {
                descendoPinca1 = false;
            }
        }
        else
        {
            pinca1.transform.position = Vector3.MoveTowards(pinca1.transform.position, posInicialPinca1, velocidadeAtaque1 * Time.deltaTime);

            if (pinca1.transform.position == posInicialPinca1)
            {
                descendoPinca1 = true;
            }
        }
    }

    private void Pinca1AtaqueSobe()
    {
        pinca1.transform.position = Vector3.MoveTowards(pinca1.transform.position, posInicialPinca1, velocidadeAtaque1 * Time.deltaTime);
    }

    private void Pinca2Ataque()
    {
        if (descendoPinca2)
        {
            pinca2.transform.position = Vector3.MoveTowards(pinca2.transform.position, posFinalPinca2, velocidadeAtaque1 * Time.deltaTime);

            if (pinca2.transform.position == posFinalPinca2)
            {
                descendoPinca2 = false;
            }
        }
        else
        {
            pinca2.transform.position = Vector3.MoveTowards(pinca2.transform.position, posInicialPinca2, velocidadeAtaque1 * Time.deltaTime);

            if (pinca2.transform.position == posInicialPinca2)
            {
                descendoPinca2 = true;

                ataque1Atual++;//conta qunatos ataque foram dados
            }
        }
    }

    //Metodos relacionados ao Ataque 2
    private void SetUpAtaque2()
    {
        ataque2Preparado = true;
        ataque2Voltando = false;

        DefinirDiracaoAtaque2();

        pinca3.transform.position = posInicialPinca3;
        pinca4.transform.position = posInicialPinca4;
    }

    private void DefinirDiracaoAtaque2()
    {
        int aux = Random.Range(0,2);

        if (aux == 0 && livreArbitrio)
        {
            direcao = DirecaoAtaque.esquerda;
        }
        else if(livreArbitrio)
        {
            direcao = DirecaoAtaque.direita;
        }
    }

    private void Ataque2()
    {
        switch (direcao)
        {
            case DirecaoAtaque.direita:
                Ataque2Direita();
                break;
            case DirecaoAtaque.esquerda:
                Ataque2Esquerda();
                break;
        }

    }

    private void Ataque2Esquerda()
    {
        if (ataque2Preparado)
        {
            pinca3.transform.position = Vector3.MoveTowards(pinca3.transform.position, posIntermediariaPinca3, velocidadePreparoAtaque2 * Time.deltaTime);
            tempoEsperaProximoAtaque2 = tempoEsperaAtaque2 + Time.time;

            if (pinca3.transform.position == posIntermediariaPinca3)
            {
                ataque2Preparado = false;
            }

        }
        else if (ataque2Voltando)
        {
            SombraBrain();
        }
        else if (tempoEsperaProximoAtaque2 <= Time.time)
        {
            pinca3.transform.position = Vector3.MoveTowards(pinca3.transform.position, posfinalPinca3, velocidadeAtaque2 * Time.deltaTime);
            if (pinca3.transform.position == posfinalPinca3)
            {
                ataque2Voltando = true;
            }
        }
    }

    private void Ataque2Direita()
    {
        if (ataque2Preparado)
        {
            pinca4.transform.position = Vector3.MoveTowards(pinca4.transform.position, posIntermediariaPinca4, velocidadePreparoAtaque2 * Time.deltaTime);
            tempoEsperaProximoAtaque2 = tempoEsperaAtaque2 + Time.time;

            if (pinca4.transform.position == posIntermediariaPinca4)
            {
                ataque2Preparado = false;
            }

        }
        else if (ataque2Voltando)
        {
            SombraBrain();
        }
        else if (tempoEsperaProximoAtaque2 <= Time.time)
        {
            pinca4.transform.position = Vector3.MoveTowards(pinca4.transform.position, posfinalPinca4, velocidadeAtaque2 * Time.deltaTime);
            if (pinca4.transform.position == posfinalPinca4)
            {
                ataque2Voltando = true;
            }
        }
    }

    //Metodos relacionados ao Ataque 3
    private void SetUpAtaque3()
    {
        ataque3Preparado = true;
        ataque3Voltando = false;

        DefinirDiracaoAtaque2();

        pinca5.transform.position = posInicialPinca5;
        pinca6.transform.position = posInicialPinca6;
    }

    private void Ataque3()
    {
        if (ataque3Preparado)
        {
            pinca5.transform.position = Vector3.MoveTowards(pinca5.transform.position, posIntermediariaPinca5, velocidadePreparoAtaque3 * Time.deltaTime);
            pinca6.transform.position = Vector3.MoveTowards(pinca6.transform.position, posIntermediariaPinca6, velocidadePreparoAtaque3 * Time.deltaTime);
            tempoEsperaProximoAtaque3 = tempoEsperaAtaque3 + Time.time;

            if (pinca5.transform.position == posIntermediariaPinca5)
            {
                ataque3Preparado = false;
            }

        }
        else if (ataque3Voltando)
        {
            SombraBrain();
        }
        else if (tempoEsperaProximoAtaque3 <= Time.time)
        {
            pinca5.transform.position = Vector3.MoveTowards(pinca5.transform.position, posfinalPinca5, velocidadePreparoAtaque3 * Time.deltaTime);
            pinca6.transform.position = Vector3.MoveTowards(pinca6.transform.position, posfinalPinca6, velocidadePreparoAtaque3 * Time.deltaTime);
            if (pinca5.transform.position == posfinalPinca5)
            {
                ataque3Voltando = true;
            }
        }
    }

    //Metodos relacionados ao Ataque 4
    private void SetUpAtaque4()
    {
        ondaAtaque = false;

        pinca7.transform.position = posInicialPinca7;
        Onda1.transform.position = posInicialOnda1;
        Onda2.transform.position = posInicialOnda2;
    }

    private void Ataque4()
    {
        if (ondaAtaque == false)
        {
            pinca7.transform.position = Vector3.MoveTowards(pinca7.transform.position, posfinalPinca7, velocidadeAtaque4 * Time.deltaTime);
            Onda1.transform.position = Vector3.MoveTowards(Onda1.transform.position, posIntermediariaOnda1, velocidadeAtaque4 * Time.deltaTime);
            Onda2.transform.position = Vector3.MoveTowards(Onda2.transform.position, posIntermediariaOnda2, velocidadeAtaque4 * Time.deltaTime);
            if (Onda1.transform.position == posIntermediariaOnda1)
            {
                tempoParaRetornoAtaque4 = tempoRetornoAtaque4 + Time.time;
                ondaAtaque = true;
            }
        }
        else
        {
            Onda1.transform.position = Vector3.MoveTowards(Onda1.transform.position, posfinalOnda1, velocidadeOndaAtaque4 * Time.deltaTime);
            Onda2.transform.position = Vector3.MoveTowards(Onda2.transform.position, posfinalOnda2, velocidadeOndaAtaque4 * Time.deltaTime);
            if (tempoParaRetornoAtaque4 <= Time.time)
            {
                pinca7.transform.position = Vector3.MoveTowards(pinca7.transform.position, posInicialPinca7, velocidadeAtaque4 * Time.deltaTime);
                
            }
            if (Onda1.transform.position == posfinalOnda1)
            {
                SombraBrain();
            }
        }
        
    }

    //Metodos relacionados ao Ataque 5
    private void SetUpAtaque5()
    {
        ctrlAtaque5 = false;
        posControle = false;

        tentaculo1.SetActive(true);
        tentaculo1.transform.position = posTentaculo1;
        tentaculo1.transform.GetChild(0).transform.position = posInicialTentaculo1;

        tentaculo2.SetActive(true);
        tentaculo2.transform.position = posTentaculo2;
        tentaculo2.transform.GetChild(0).transform.position = posInicialTentaculo2;

        tentaculo3.SetActive(true);
        tentaculo3.transform.position = posTentaculo3;
        tentaculo3.transform.GetChild(0).transform.position = posInicialTentaculo3;

        tentaculo4.SetActive(true);
        tentaculo4.transform.position = posTentaculo4;
        tentaculo4.transform.GetChild(0).transform.position = posInicialTentaculo4;
    }

    private void ShutDownAtaque5()
    {
        tentaculo1.SetActive(false);
        tentaculo2.SetActive(false);
        tentaculo3.SetActive(false);
        tentaculo4.SetActive(false);
    }

    private void Ataque5()
    {
        if (posControle == false)
        {
            Debug.Log("teste");
            tentaculo1.transform.position = Vector3.MoveTowards(tentaculo1.transform.position, posTentaculo1Final, velocidadePreparoAtaque5 * Time.deltaTime);

            tentaculo2.transform.position = Vector3.MoveTowards(tentaculo2.transform.position, posTentaculo2Final, velocidadePreparoAtaque5 * Time.deltaTime);

            tentaculo3.transform.position = Vector3.MoveTowards(tentaculo3.transform.position, posTentaculo3Final, velocidadePreparoAtaque5 * Time.deltaTime);

            tentaculo4.transform.position = Vector3.MoveTowards(tentaculo4.transform.position, posTentaculo4Final, velocidadePreparoAtaque5 * Time.deltaTime);

            if (tentaculo1.transform.position == posTentaculo1Final)
            {
                posControle = true;

                tempoProximaEsperaAtaque5 = tempoEsperaAtaque5 + Time.time;
            }
        }
        else if (tempoProximaEsperaAtaque5 <= Time.time && ctrlAtaque5 == false)
        {
            tentaculo1.transform.GetChild(0).transform.position = Vector3.MoveTowards(tentaculo1.transform.GetChild(0).transform.position, posFinalTentaculo1, velocidadeAtaque5 * Time.deltaTime);
            animTentaculo1.SetTrigger("Ataque");

            tentaculo2.transform.GetChild(0).transform.position = Vector3.MoveTowards(tentaculo2.transform.GetChild(0).transform.position, posFinalTentaculo2, velocidadeAtaque5 * Time.deltaTime);
            animTentaculo2.SetTrigger("Ataque");

            tentaculo3.transform.GetChild(0).transform.position = Vector3.MoveTowards(tentaculo3.transform.GetChild(0).transform.position, posFinalTentaculo3, velocidadeAtaque5 * Time.deltaTime);
            animTentaculo3.SetTrigger("Ataque");

            tentaculo4.transform.GetChild(0).transform.position = Vector3.MoveTowards(tentaculo4.transform.GetChild(0).transform.position, posFinalTentaculo4, velocidadeAtaque5 * Time.deltaTime);
            animTentaculo4.SetTrigger("Ataque");

            if (tentaculo1.transform.GetChild(0).transform.position == posFinalTentaculo1)
            {
                ctrlAtaque5 = true;
                tempoProximaDuracaoAtaque5 = tempoDuracaoAtaque5 + Time.time;
            }
        }else if (tempoProximaDuracaoAtaque5 <= Time.time && ctrlAtaque5)
        {
            tentaculo1.transform.GetChild(0).transform.position = Vector3.MoveTowards(tentaculo1.transform.GetChild(0).transform.position, posInicialTentaculo1, velocidadeAtaque5 * Time.deltaTime);
            animTentaculo1.SetTrigger("Desce");

            tentaculo2.transform.GetChild(0).transform.position = Vector3.MoveTowards(tentaculo2.transform.GetChild(0).transform.position, posInicialTentaculo2, velocidadeAtaque5 * Time.deltaTime);
            animTentaculo2.SetTrigger("Desce");

            tentaculo3.transform.GetChild(0).transform.position = Vector3.MoveTowards(tentaculo3.transform.GetChild(0).transform.position, posInicialTentaculo3, velocidadeAtaque5 * Time.deltaTime);
            animTentaculo3.SetTrigger("Desce");

            tentaculo4.transform.GetChild(0).transform.position = Vector3.MoveTowards(tentaculo4.transform.GetChild(0).transform.position, posInicialTentaculo4, velocidadeAtaque5 * Time.deltaTime);
            animTentaculo4.SetTrigger("Desce");

            if (tentaculo1.transform.GetChild(0).transform.position == posInicialTentaculo1)
            {
                ShutDownAtaque5();
                SombraBrain();
            }
        }

    }

    //Metodos relacionados ao Ataque 6
    private void SetUpAtaque6()
    {
        preparaAtaque6 = false;
        acaoAtaque6 = false;
        voltarAtaque6 = false;

        pinca8.transform.position = posInicialPinca8;
    }

    private void Ataque6()
    {
        if (preparaAtaque6 == false)
        {
            pinca8.transform.position = Vector3.MoveTowards(pinca8.transform.position, new Vector3(0, posIntermediarialYPinca8, 0), velocidadePrepararAtaque6 * Time.deltaTime);
            if (pinca8.transform.position.y != posIntermediarialYPinca8)
            {
                preparaAtaque6 = true;

                tempoProximaMiraAtaque6 = tempoMiraAtaque6 + Time.time;
            }
        }
        else if(acaoAtaque6 == false)
        {
            pinca8.transform.position = new Vector3(Mathf.Lerp(pinca8.transform.position.x, player.transform.position.x, velocidadePrepararAtaque6 * Time.deltaTime), posIntermediarialYPinca8, 0);

            if (tempoProximaMiraAtaque6 <= Time.time)
            {
                acaoAtaque6 = true;
                tempoProximoTravadoAtaque6 = tempoTravadoAtaque6 + Time.time;
            }
        }else if(voltarAtaque6 == false)
        {
            pinca8.transform.position = Vector3.MoveTowards(pinca8.transform.position, new Vector3(pinca8.transform.position.x, posFinalYPinca8, 0), velocidadeAtaque6 * Time.deltaTime);

            if (tempoProximoTravadoAtaque6 <= Time.time)
            {
                voltarAtaque6 = true;
            }
        }
        else
        {
            pinca8.transform.position = Vector3.MoveTowards(pinca8.transform.position, new Vector3(pinca8.transform.position.x, posInicialYPinca8, 0), velocidadeAtaque6 * Time.deltaTime);

            if (pinca8.transform.position == new Vector3(pinca8.transform.position.x, posInicialYPinca8, 0))
            {
                SombraBrain();
            }
            
        }

        

    }
}
