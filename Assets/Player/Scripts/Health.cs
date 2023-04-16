using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    private Rigidbody2D rb;

    public int maxLife;
    public int currentLife;
    public int HealSeaweed;

    public float CurrenTime = 0f;
    public float TimeToHeal = 1.5f;
    [SerializeField] private GameObject[]lifeImages;

    private CinemachineImpulseSource impulseSource;

    public bool DoDamage = false;
    private void Start()
    {
        currentLife = maxLife;

        rb = gameObject.GetComponent<Rigidbody2D>();

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {

        if(Input.GetAxis("Curar")> 0)
        {
            CurrenTime += Time.deltaTime;
            if (currentLife < maxLife && HealSeaweed > 0 && CurrenTime >= TimeToHeal)
            {
                GiveHealth(1);
                HealSeaweed -= 1;
                CurrenTime = 0;

                if (currentLife == maxLife)
                {
                    
                }
            }
        }
        if (DoDamage)
        {
            Damage(1);
            DoDamage= false;
        }

        if(currentLife <= 0f)
        {
            Debug.Log("morreu");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Damage(1);
            rb.AddForce(transform.up * 800);
        }
    }

    // Do damage
    public void Damage(int GiveDamageAmount)
    {
        CameraShakeManager.instance.CameraShake(impulseSource);
        currentLife -= GiveDamageAmount;
        lifeImages[currentLife].gameObject.SetActive(false);
    }

    //give heath
    public void GiveHealth(int GiveHealthAmount)
    {
        currentLife += GiveHealthAmount;
        lifeImages[(currentLife - 1)].gameObject.SetActive(true);
    }
}
