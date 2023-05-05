using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.VFX;

public class Health : MonoBehaviour
{

    private Rigidbody2D rb;

    [Header("Health")]
    public int maxLife;
    public int currentLife;
    [SerializeField] private GameObject[]lifeImages;

    [SerializeField] private GameObject[] lifeImageToEnable;
    public bool haveArmor = false;
    private CinemachineImpulseSource impulseSource;

    private PlayerHitFeedback PlayerHitFeedbackScript;

    public bool DoDamage = false;

    public VisualEffect DamageEffect;

    [Header("heal")]
    public int HealSeaweed;

    public float CurrenTime = 0f;
    public float TimeToHeal = 1.5f;

    [SerializeField] private GameObject[] HealImages;
    private void Start()
    {
        currentLife = maxLife;

        rb = gameObject.GetComponent<Rigidbody2D>();

        impulseSource = GetComponent<CinemachineImpulseSource>();

        PlayerHitFeedbackScript = gameObject.GetComponent<PlayerHitFeedback>();
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
                HealImages[HealSeaweed].SetActive(false);

            }
        }
        if(Input.GetButtonUp("Curar"))
        {
            CurrenTime = 0;
        }
        if(haveArmor)
        {
            for(int i = 0; i <= lifeImageToEnable.Length; i++)
            {
                lifeImageToEnable[i].SetActive(true);

                lifeImages[i + 5] = lifeImageToEnable[i];
                if(i == lifeImageToEnable.Length - 1)
                {
                    haveArmor = false;
                    break;
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
            rb.velocity = Vector2.zero;
            Damage(1);
            rb.velocity = new Vector2(transform.position.y, 15f);

        }
    }

    // Do damage
    public void Damage(int GiveDamageAmount)
    {

        CameraShakeManager.instance.CameraShake(impulseSource);
        currentLife -= GiveDamageAmount;
        lifeImages[currentLife].gameObject.SetActive(false);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.DamageFeedback, this.transform.position);
        PlayerHitFeedbackScript.wasHit= true; //Pass the feedback that the player was hit, so the Health can be disble and enabled after a amount of time
        DamageEffect.Play();
        
    }

    //give heath
    public void GiveHealth(int GiveHealthAmount)
    {
        currentLife += GiveHealthAmount;
        lifeImages[(currentLife - 1)].gameObject.SetActive(true);
        
    }
}
