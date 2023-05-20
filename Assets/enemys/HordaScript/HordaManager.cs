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

    public GameObject Wall;

    private void Start()
    {
      
    }
    private void Update()
    {
        if (ZombsInScene.Length == 0)
        {
            StartRoundTime += Time.deltaTime;
        }
        if (AmountEnemyToSpawnByRound[CurrentRound] >= 0 && Spawn)
        {
            int i = Random.Range(0, SpawnPoints.Count);
            
            Instantiate(ZombiPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            ZombiPrefab.GetComponent<BTZombiTurtle>().enabled = true;
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
            //Acabou
            Debug.Log("Vitoria");
            terminou = true;
            Wall.GetComponent<Animator>().SetBool("abrindo", true);
            Wall.GetComponent<BoxCollider2D>().enabled = true;
        }
       
    }

}
