using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth;
    public float currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }
    private void Update()
    {
        if(currentHealth <= 0f)
        {
           Destroy(gameObject);
        }
    }
    public void Damage(float damage)
    {
        currentHealth -= damage;
    }
}
