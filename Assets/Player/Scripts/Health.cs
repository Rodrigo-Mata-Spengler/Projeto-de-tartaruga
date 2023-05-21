using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.VFX;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    private Rigidbody2D rb;

    [Header("Health")]
    public int maxLife;
    public int currentLife;
    //[SerializeField] private GameObject lifeImages;
    [HideInInspector]public Slider HealthSlider;

    public bool haveArmor = false;
    private CinemachineImpulseSource impulseSource;

    private PlayerHitFeedback PlayerHitFeedbackScript;

    public bool DoDamage = false;

    public VisualEffect DamageEffect;

    [Header("heal")]
    public int MaxHealSeaweed;
    public int HealSeaweed;

    public float CurrenTime = 0f;
    public float TimeToHeal = 1.5f;

     private TextMeshProUGUI AmountOfSeaweed;

    [Space]
     private MenuPause pause;

    public VisualEffect HealEffect;
    public VisualEffect RezarEffect;


    private Animator PlayerAnimator;

    private PlayerMovement PlayerMovment;

    private bool DoOnce = false;
    private void Start()
    {
        //currentLife = maxLife;

        //HealSeaweed = MaxHealSeaweed;
        pause = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuPause>();
        AmountOfSeaweed = GameObject.FindGameObjectWithTag("AlgaText").GetComponent<TextMeshProUGUI>();
        HealthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();

        rb = gameObject.GetComponent<Rigidbody2D>();

        impulseSource = GetComponent<CinemachineImpulseSource>();

        PlayerHitFeedbackScript = gameObject.GetComponent<PlayerHitFeedback>();

        AmountOfSeaweed.text = HealSeaweed.ToString();

        HealthSlider.value = currentLife * 8;

        PlayerAnimator = gameObject.GetComponent<Animator>();

       //PlayerMovment = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {

        if(Input.GetAxis("Curar")> 0 && HealSeaweed <= MaxHealSeaweed && currentLife < maxLife && HealSeaweed > 0)
        {
            PlayerAnimator.SetBool("Curar", true);
            if (DoOnce == false)
            {
                gameObject.GetComponent<PlayerMovement>().enabled= false;
                HealEffect.Play();
                DoOnce= true;
            }
            
            CurrenTime += Time.deltaTime;
            if (CurrenTime >= TimeToHeal)
            {
                GiveHealth(1);
                HealSeaweed -= 1;
                CurrenTime = 0;
                HealEffect.Stop();
                //gameObject.GetComponent<PlayerMovement>().enabled = false;
                
                AmountOfSeaweed.text = HealSeaweed.ToString();
                
            }
        }
        if(Input.GetButtonUp("Curar"))
        {
            DoOnce = false;
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            CurrenTime = 0;
            HealEffect.Stop();
            PlayerAnimator.SetBool("Curar", false);
        }
        /*
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
        */
        if (DoDamage)
        {
            Damage(1);
            DoDamage= false;
        }

        if(currentLife <= 0f)
        {
            PlayerAnimator.SetTrigger("Morto");

            //Rodrigo Time !!!!!!
            PlayerData data = SaveSystem.LoadPlayer();
            if (data != null)
            {

                Time.timeScale = 1;
                SceneManager.LoadScene(data.scenaAtual);
            }
            else
            {
                SceneManager.LoadScene("Cena 1");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            //rb.velocity = Vector2.zero;
            Damage(1);
           //rb.velocity = new Vector2(transform.position.x, 15f);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Alga") && HealSeaweed < MaxHealSeaweed)
        {
            //int i = Random.Range(0, MaxAmount);
            HealSeaweed += 1;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemGrab, transform.position);
            Destroy(collision.gameObject);
            AmountOfSeaweed.text = HealSeaweed.ToString();
        }
        else if (collision.gameObject.CompareTag("Alga") && HealSeaweed == MaxHealSeaweed)
        {
            Destroy(collision.gameObject);
        }
    }

    // Do damage
    public void Damage(int GiveDamageAmount)
    {
        if(currentLife > 0f)
        {
            CameraShakeManager.instance.CameraShake(impulseSource);
            currentLife -= GiveDamageAmount;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.DamageFeedback, this.transform.position);
            PlayerHitFeedbackScript.wasHit = true; //Pass the feedback that the player was hit, so the Health can be disble and enabled after a amount of time
            DamageEffect.Play();

            HealthSlider.value -= 8;
        }

        
    }

    //give heath
    public void GiveHealth(int GiveHealthAmount)
    {
        if(currentLife < maxLife)
        {
            currentLife += GiveHealthAmount;
            HealthSlider.value = 8 * currentLife;
        }

    }

    public void ResetLife()
    {
        currentLife = maxLife;
        HealthSlider.value = currentLife * 8;
    }
}
