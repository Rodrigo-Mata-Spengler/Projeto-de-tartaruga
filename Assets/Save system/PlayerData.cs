using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Informações do player
    public float stamina;
    public int vida;
    public float[] position;
    public int seaWeed;

    //Informações do inventario
    public int conchas;
    public int coral;
    public int calcio;
    public int ossos;

    //Informações de habilidade
    public bool wallJump;
    public bool doubleJump;
    public bool dash;
    public bool blast;

    //Informações de scena
    public string scenaAtual;

    //Informaçãoes de Mapa
    public bool mapa1;
    public bool mapa2;
    public bool mapa3;

    //Informações de derrota de boss
    public bool boss1Derrotado;
    public bool boss2Derrotado;

    public PlayerData(GameObject player)
    {
        //captura as informações do player
        stamina = player.GetComponent<Estamina>().CurrentEstamina;
        vida = player.GetComponent<Health>().maxLife;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        seaWeed = player.GetComponent<Health>().HealSeaweed;

        //captura as informações do inventario
        conchas = player.GetComponent<ItensInventory>().conchas;
        coral = player.GetComponent<ItensInventory>().coral;
        calcio = player.GetComponent<ItensInventory>().calcio;
        ossos = player.GetComponent<ItensInventory>().ossos;

        //caputura as informações de habilidades
        wallJump = player.GetComponent<PlayerMovement>().haveWallJump;
        doubleJump = player.GetComponent<PlayerMovement>().haveDoubleJump;
        dash = player.GetComponent<Dash>().enabled;
        blast = player.GetComponent<Blast>().enabled;

        //captura as informações de mundo
        scenaAtual = player.GetComponent<ScenaAtual>().scenaAtual;

        //captura as informaçôes do mapa
        mapa1 = MapControler.mapa1;
        mapa2 = MapControler.mapa2;
        mapa3 = MapControler.mapa3;

        //captura as informações dos boses
        boss1Derrotado = HordaManager.terminou;
        boss2Derrotado = GuardianBehavior.terminou;
    }

}
