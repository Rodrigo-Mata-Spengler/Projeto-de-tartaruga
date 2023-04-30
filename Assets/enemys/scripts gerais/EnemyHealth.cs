using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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

    [SerializeField]private int itemDropNumber; // the position of a item drop in the array
    private void Start()
    {
        currentHealth = MaxHealth;

        
    }
    
    private void Update()
    {
        if(currentHealth <= 0f && !Droped)
        {
            itemDropNumber = Random.Range(0, ItensPrefabs.Length);
            DropItem(ItensPrefabs[itemDropNumber]);
            
        }

        if(Droped && Time.time > TimeToDestroy)
        {
            Destroy(gameObject);
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
        Debug.Log("Euuuuu");
    }
}
