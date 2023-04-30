using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Informa��es do player
    public float stamina;
    public int vida;
    public float[] position;

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

    public PlayerData(GameObject player)
    {
        //captura as informa��es do player
        stamina = player.GetComponent<Estamina>().CurrentEstamina;
        vida = player.GetComponent<Health>().maxLife;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

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

    }

}
