using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GuardiaoHealth : MonoBehaviour
{
    public Animator animator;
    private EnemyHitFeedback enemyHit;

    public float MaxHealth;
    public float currentHealth;

    private GuardianBehavior GuardianBehavior;

    [SerializeField] private Transform DropItemPoint;
    [Space]
    [SerializeField] private GameObject[] ItensPrefabs;
    [Space]
    [SerializeField] private float TimeToDestroyDelay = 0f;
    private float TimeToDestroy = 0f;
    private bool Droped = false;

    private int itemDropNumber; // the position of a item drop in the array

    private float LifePorcentage;

    private GuardianBehavior guardianBehavior;

    [Header("Norteado")]
    public float tempoNorteado = 0;//current time that is waitting to do a action
    [SerializeField] private float TempoEsperaNorteado = 0; // Amount of time to wait for the next action

    [SerializeField] private bool norteado1 = false;
    [SerializeField] private bool norteado2 = false;

    public VisualEffect HitEffect;
    private void Start()
    {
        currentHealth = MaxHealth;

        LifePorcentage = currentHealth / 3;

        guardianBehavior = this.GetComponent<GuardianBehavior>();

        enemyHit= this.GetComponent<EnemyHitFeedback>();
    }

    private void Update()
    {
        if (currentHealth <= (MaxHealth - LifePorcentage) && norteado1 == false)
        {
            guardianBehavior.enabled = false;
            animator.SetBool("cansado", true);
            Norteado1();
        }
        if (currentHealth <= (MaxHealth - (LifePorcentage * 2)) && norteado2 == false)
        {
            animator.SetBool("cansado", true);
            guardianBehavior.enabled= false;
            
            Norteado2();
        }




        if (currentHealth <= 0f && !Droped)
        {
            itemDropNumber = Random.Range(0, ItensPrefabs.Length);
            DropItem(ItensPrefabs[itemDropNumber]);

        }

        if (Droped && Time.time > TimeToDestroy)
        {
            Destroy(gameObject);
        }

    }

    private void Norteado1()
    {
        ///the enemy will wait a time after conclude a action
        tempoNorteado += Time.deltaTime;
        if (tempoNorteado > TempoEsperaNorteado /*|| enemyHit.wasHit */)
        {
            guardianBehavior.enabled = true;
            norteado1 = true;
            tempoNorteado = 0;
            animator.SetBool("cansado", false);
        }
    }
    private void Norteado2()
    {
        ///the enemy will wait a time after conclude a action
        tempoNorteado += Time.deltaTime;
        if (tempoNorteado > TempoEsperaNorteado /*|| enemyHit.wasHit*/)
        {
            guardianBehavior.enabled = true;
            animator.SetBool("cansado", false);
            norteado2 = true;
        }
    }


    public void Damage(float damage)
    {
        currentHealth -= damage;
        HitEffect.Play();
    }

    private void DropItem(GameObject Item)
    {
        TimeToDestroy = Time.time + TimeToDestroyDelay;
        Instantiate(Item, DropItemPoint.position, DropItemPoint.rotation);
        Droped = true;
        //Debug.Log("Euuuuu");
    }

}

