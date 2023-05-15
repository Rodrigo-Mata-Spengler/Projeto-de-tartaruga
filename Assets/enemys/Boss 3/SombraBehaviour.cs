using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SombraStatus { idle, ataque1, ataque2, ataque3, ataque4, ataque5, ataque6, teste};
enum DirecaoAtaque { direita, esquerda, none};
public class SombraBehaviour : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private SombraStatus status = SombraStatus.idle; //mostra o status da sombra atual
    [SerializeField] private bool livreArbitrio = true;//defini se a sombra pode ou não fazer suas próprias desisões 

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
    [SerializeField] private float quantidadeAtaque = 0;//qunatos ataque devem ser
    private float ataqueAtual = 0;

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
    private bool temp3;

    [Header("Ataque 5")]
    private bool temp4;

    [Header("Ataque 6")]
    private bool temp5;



    //Metodos gerais da Sombra
    private void Start()
    {
        SetUpAtaque1();
        SetUpAtaque2();
        SetUpAtaque3();
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
                break;
            case SombraStatus.ataque5:
                break;
            case SombraStatus.ataque6:
                break;
            case SombraStatus.teste:
                SetUpIdle();
                SetUpAtaque1();
                SetUpAtaque2();
                SetUpAtaque3();
                break;
        }
    }

    private void SombraBrain()
    {
        if (livreArbitrio)
        {

        }
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
        ataqueAtual = 0;

        tempoProximoOffset = tempoOffSet + Time.time;

        pinca1.transform.position = posInicialPinca1;
        descendoPinca1 = true;

        pinca2.transform.position = posInicialPinca2;
        descendoPinca2 = true;

    }

    private void Ataque1()
    {
        if (ataqueAtual != quantidadeAtaque)
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

                ataqueAtual++;//conta qunatos ataque foram dados
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
            pinca3.transform.position = Vector3.MoveTowards(pinca3.transform.position, posInicialPinca3, velocidadeAtaque2 * Time.deltaTime);
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
            pinca4.transform.position = Vector3.MoveTowards(pinca4.transform.position, posInicialPinca4, velocidadeAtaque2 * Time.deltaTime);
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
            pinca5.transform.position = Vector3.MoveTowards(pinca5.transform.position, posInicialPinca5, velocidadePreparoAtaque3 * Time.deltaTime);
            pinca6.transform.position = Vector3.MoveTowards(pinca6.transform.position, posInicialPinca6, velocidadePreparoAtaque3 * Time.deltaTime);
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

    //Metodos relacionados ao Ataque 5

    //Metodos relacionados ao Ataque 6

}
