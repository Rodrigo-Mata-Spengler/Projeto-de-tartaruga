using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum EnemyTag { Zombi, Guardiao, Planta, Mosca, Caranguejo,Ourico,Sombra, Enemy};
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyTag EnemyTag = EnemyTag.Enemy;

    public float MaxHealth;
    public float currentHealth;

    private EnemyHealth EnemyHealthScript;

    [SerializeField] private Transform DropItemPoint;
    [Space]
    [SerializeField] private GameObject[] ItensPrefabs;
    [Space]
    [SerializeField] private float TimeToDestroyDelay = 0f;
    private float TimeToDestroy = 0f;
    private bool Droped = false;

    [SerializeField] private float tempoParaMorte = 0f;

    private int itemDropNumber; // the position of a item drop in the array
    private void Start()
    {
        currentHealth = MaxHealth;  
    }
    
    private void Update()
    {
        if(currentHealth <= 0f && !Droped)
        {
            switch(EnemyTag)
            {
                case EnemyTag.Enemy:
                    itemDropNumber = Random.Range(0, ItensPrefabs.Length);
                    DropItem(ItensPrefabs[itemDropNumber]);

                    break;

                case EnemyTag.Mosca:
                    itemDropNumber = Random.Range(0, ItensPrefabs.Length);
                    DropItem(ItensPrefabs[itemDropNumber]);
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedbackMorteMosca, this.transform.position);
                    break;

                case EnemyTag.Planta:
                    itemDropNumber = Random.Range(0, ItensPrefabs.Length);
                    DropItem(ItensPrefabs[itemDropNumber]);
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedbackMortePlanta, this.transform.position);
                    break;

                case EnemyTag.Zombi:
                    itemDropNumber = Random.Range(0, ItensPrefabs.Length);
                    DropItem(ItensPrefabs[itemDropNumber]);
                    GetComponent<Animator>().SetTrigger("Morte");
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.FeedBackMorteZombi, this.transform.position);
                    break;

                case EnemyTag.Guardiao:
                    itemDropNumber = Random.Range(0, ItensPrefabs.Length);
                    DropItem(ItensPrefabs[itemDropNumber]);

                    break;

                case EnemyTag.Caranguejo:
                    itemDropNumber = Random.Range(0, ItensPrefabs.Length);
                    DropItem(ItensPrefabs[itemDropNumber]);
                    break;

                case EnemyTag.Sombra:
                    itemDropNumber = Random.Range(0, ItensPrefabs.Length);
                    DropItem(ItensPrefabs[itemDropNumber]);
                    break;
            }  
        }

        if(Droped && Time.time > TimeToDestroy)
        {
            if (EnemyTag == EnemyTag.Planta)
            {
                Destroy(transform.parent.gameObject, tempoParaMorte);
            }
            Destroy(gameObject, tempoParaMorte);
        }

    }
    public void Damage(float damage)
    {
        currentHealth -= damage;
    }

    private void DropItem(GameObject Item)
    {
        TimeToDestroy = Time.deltaTime + TimeToDestroyDelay;
        Instantiate(Item, DropItemPoint.position, DropItemPoint.rotation);
        Droped = true;
        //Debug.Log("Euuuuu");
    }
}
