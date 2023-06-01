using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Informa��es do player
    public float stamina;
    public bool hasEstamina;
    public bool hasEstamina2;
    public int vida;
    public float[] position;
    public int seaWeed;

    //Informa��es da Anima��o do player
    public bool haveMagicTrident;
    public bool haveArmor;

    //Informa��es do inventario
    public int conchas;
    public int coral;
    public int calcio;
    public int ossos;

    //Informa��es de habilidade
    public bool wallJump;
    public bool doubleJump;
    public bool dash;
    public bool blast;

    //Informa��es de scena
    public string scenaAtual;

    //Informa��oes de Mapa
    public bool mapa;
    public bool mapa1;
    public bool mapa2;
    public bool mapa3_1;
    public bool mapa3_2;
    public bool mapa3_3;
    public bool mapa4_1;
    public bool mapa4_2;
    public bool mapa4_3;
    public bool mapa4_4;
    public bool mapa5;
    public bool mapa6;

    //Informa��es de derrota de boss
    public bool boss1Derrotado;
    public bool boss2Derrotado;

    public PlayerData(GameObject player)
    {
        //captura as informa��es do player
        stamina = player.GetComponent<Estamina>().CurrentEstamina;
        hasEstamina = player.GetComponent<Estamina>().hasEstamina;
        hasEstamina2 = player.GetComponent<Estamina>().hasEstamina2;
        vida = player.GetComponent<Health>().maxLife;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        seaWeed = player.GetComponent<Health>().HealSeaweed;

        //Captura As Informa��es de anima��o do player
        haveArmor = player.GetComponent<PlayerMovement>().HaveArmor;
        haveMagicTrident = player.GetComponent<PlayerMovement>().HaveMagicTrident;

        //captura as informa��es do inventario
        conchas = player.GetComponent<ItensInventory>().conchas;
        coral = player.GetComponent<ItensInventory>().coral;
        calcio = player.GetComponent<ItensInventory>().calcio;
        ossos = player.GetComponent<ItensInventory>().ossos;

        //caputura as informa��es de habilidades
        wallJump = player.GetComponent<PlayerMovement>().haveWallJump;
        doubleJump = player.GetComponent<PlayerMovement>().haveDoubleJump;
        dash = player.GetComponent<Dash>().enabled;
        blast = player.GetComponent<Blast>().enabled;

        //captura as informa��es de mundo
        scenaAtual = player.GetComponent<ScenaAtual>().scenaAtual;

        //captura as informa��es do mapa
        mapa = player.GetComponent<ItensInventory>().mapa;
        mapa1 = MapControler.mapa1;
        mapa2 = MapControler.mapa2;
        mapa3_1 = MapControler.mapa3_1;
        mapa3_2 = MapControler.mapa3_2;
        mapa3_3 = MapControler.mapa3_3;
        mapa4_1 = MapControler.mapa4_1;
        mapa4_2 = MapControler.mapa4_2;
        mapa4_3 = MapControler.mapa4_3;
        mapa4_4 = MapControler.mapa4_4;
        mapa5 = MapControler.mapa5;
        mapa6 = MapControler.mapa6;

        //captura as informa��es dos boses
        boss1Derrotado = GuardianBehavior.terminou;
        boss2Derrotado = HordaManager.terminou;
    }

}
