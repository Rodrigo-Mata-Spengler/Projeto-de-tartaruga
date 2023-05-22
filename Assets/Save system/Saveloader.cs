using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saveloader : MonoBehaviour
{
    private PlayerData data;
    [SerializeField] private GameObject player;



    private void Awake()
    {
        data = SaveSystem.LoadPlayer();

        //definir localiza��o do player
        Vector3 pos = new Vector3(data.position[0], data.position[1], data.position[2]);

        player.transform.position = pos;

        //defini vida e estamina
        player.GetComponent<Estamina>().CurrentEstamina = data.stamina;
        player.GetComponent<Health>().maxLife = data.vida;
        player.GetComponent<Health>().HealSeaweed = data.seaWeed;

        //defini se tem armadura ou magia
        player.GetComponent<PlayerMovement>().HaveArmor = data.haveArmor;
        player.GetComponent<PlayerMovement>().HaveMagicTrident = data.haveMagicTrident;

        //defini as informa��es do inventario
        player.GetComponent<ItensInventory>().conchas = data.conchas;
        player.GetComponent<ItensInventory>().coral = data.coral;
        player.GetComponent<ItensInventory>().calcio = data.calcio;
        player.GetComponent<ItensInventory>().ossos = data.ossos;

        //defini as informa��es de habilidades
        player.GetComponent<PlayerMovement>().haveWallJump = data.wallJump;
        player.GetComponent<PlayerMovement>().haveDoubleJump = data.doubleJump;
        player.GetComponent<Dash>().enabled = data.dash;
        player.GetComponent<Blast>().enabled = data.blast;

        //defini as informa��es do mapa
        MapControler.mapa1 = data.mapa1;
        MapControler.mapa2 = data.mapa2;
        MapControler.mapa3 = data.mapa3;

        //defini as informa��es dos boses
        HordaManager.terminou = data.boss1Derrotado;
        GuardianBehavior.terminou = data.boss2Derrotado;
    }
}
