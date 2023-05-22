using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordaManager : MonoBehaviour
{
    public GameObject ZombiPrefab;
    public GameObject[] ZombsInScene;

    public List<Transform> SpawnPoints;
    [Space] 
    public List<int> AmountEnemyToSpawnByRound;
    [Space]
    public int CurrentRound = 0;
    private float StartRoundTime;
    private float EnemySpawnTime;

    public float StartRoundDeleay;
    public float EnemySpawnDelay;
    private bool Spawn = true;

    //Rodrigo Time !!!!!!!!!!!
    //Importante para o Save
    public static bool terminou = false;

    [SerializeField]private GameObject Selo;
    [SerializeField] private GameObject Wall;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (terminou)
        {
            Destroy(gameObject);
        }
        if (ZombsInScene.Length == 0)
        {
            StartRoundTime += Time.deltaTime;
        }
        if (AmountEnemyToSpawnByRound[CurrentRound] > 0 && Spawn)
        {
            int i = Random.Range(0, SpawnPoints.Count + 1);
            ZombiPrefab.GetComponent<BTZombiTurtle>().enabled= true;
            Instantiate(ZombiPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            AmountEnemyToSpawnByRound[CurrentRound] -= 1;

            Spawn = false;
            EnemySpawnTime = 0f;
        }
        ZombsInScene = GameObject.FindGameObjectsWithTag("Zombi");
        if (!Spawn)
        {
            EnemySpawnTime += Time.deltaTime;
        }
        if (EnemySpawnTime >= EnemySpawnDelay)
        {
            Spawn = true;
            EnemySpawnTime = 0f;
        }

       if (StartRoundTime >= StartRoundDeleay && CurrentRound < AmountEnemyToSpawnByRound.Count)
       {
            Spawn = true;
           CurrentRound += 1;
            StartRoundTime = 0;
          
       }

       if(CurrentRound == AmountEnemyToSpawnByRound.Count)
       {
            Wall.GetComponent<Animator>().SetBool("abrindo", true);
            Wall.GetComponent<BoxCollider2D>().enabled = false;
            Selo.SetActive(true);
            //Acabou
            Debug.Log("Vitoria");
            terminou = true;

            SaveSystem.SavePlayer(player);
            Iconesalvando.Mostraricone();
        }
       
    }

}
