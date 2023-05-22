using Cinemachine;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

enum GuardianStatus { desativado, Attack, DisableAttack, JumpAtPlayer,FallAtPlayer, JumpAtEdge ,Dash, CheckPlayerDistance, Morto, Parado };
public class GuardianBehavior : MonoBehaviour
{

    [SerializeField]private GameObject TridenteFlutuando;
    [SerializeField] private GameObject Wall;


    private CinemachineImpulseSource impulseSource;
    public float tempoInicialDelay = 0f;
    public float tempoDecorridoInicial = 0f;
    public Animator animator;
    private GuardiaoHealth EnemyHealth;
    private EnemyHitFeedback EnemyHitFeedback;

    [Header("Status")]
    [SerializeField] private GuardianStatus status = GuardianStatus.Parado;
    public static bool terminou = false;

    [Header("StartFight")]
    public bool StartFight = false;//if player has enter the battle field
    private Rigidbody2D rb;
    [Space]
    public bool PlayerClose = false;// detecte if a Player was inside
    public bool lookingRight = false;
    [SerializeField]private bool ActionChosed = false;


    [Header("LookAt")]
    [HideInInspector] public GameObject PlayerObj;
    private bool Look = true;
    [Space]
     

    [Header("Jump")]
    public float jumpHeight;
    public bool jumped;
    public float TempoPular = 0;                            ///the current time to wait for the jump
    [SerializeField] private float tempoParaPular;          /// delay to jump
     
    [Header("Jump At Player")]
    [SerializeField] private float alturaAtaque = 0; 
    [SerializeField] private float distanciaAtaque = 0;
    private Vector3 cordenadaAlturaAtaque;
    [SerializeField] private float velocidadesubida;
    public float TempoPularNoPlayer = 0;                    ///the current time to wait for the jump
    [SerializeField] private float tempoParaPularNoPlayer; /// delay to jump
    [Space]
    [Space]
    [SerializeField] private float velocidadeAtaque;
    [SerializeField] private Vector3 miraAtaque;
    [SerializeField] private float TempoEsperaAtaque = 0;
    private float tempoAtaque = 0;


    [Space]
    [Header("Attack")]
    public GameObject AttackTrigger;
    [SerializeField] private BoxCollider2D AttackCollider;
    public bool Attacked = false;
    public float AttackImpulse;
    [SerializeField] private float DuracaoDeAtaque= 0;              //Attack duration
    public float tempoDeAtaque = 0f;                                //the current time to wait disable the attack trigger


    [Header("Ground")]
    [SerializeField] private LayerMask m_WhatIsGround;              // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                   // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f;                                     // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;                                         // Whether or not the player is grounded.


    [Header("Dash")]
    private int EdgeIndex; // Int variable to decide witch of the Edge points he should go
    [SerializeField] private GameObject[] EdgeRooms;//Array to get the transform from the edge points
    [SerializeField] private bool dash = false;
    private bool dashed = false;
    public float CurrentTimeDash = 0;//the current time to wait to dash
    [SerializeField] private float TimeToDoDash;// Delay to dash
    public float DashImpulse;
    [HideInInspector]public UnityEvent OnLandEvent;

    [Header("Tonto")]
    public float tempoTonto = 0;//current time that is waitting to do a action
    [SerializeField] private float TempoEsperaTonto = 0; // Amount of time to wait for the next action

    [Space]
    private bool DoOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        EnemyHealth = this.GetComponent<GuardiaoHealth>();
        EnemyHitFeedback = this.GetComponent<EnemyHitFeedback>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if(tempoDecorridoInicial <= tempoInicialDelay && DoOnce == false)
        {
            PlayerObj.GetComponent<PlayerMovement>().enabled = false;
            tempoDecorridoInicial += Time.deltaTime;
            animator.SetBool("acorda", true);
        }
        if (tempoDecorridoInicial >= tempoInicialDelay && DoOnce==false)
        {
           
            PlayerObj.GetComponent<PlayerMovement>().enabled = true;
            status = GuardianStatus.CheckPlayerDistance;
            animator.SetBool("acorda", false);
            DoOnce = true;
        }

      
        switch (status)
        {
            case GuardianStatus.Parado:
              
                break;

            case GuardianStatus.Morto:
                morto();
                break;

            case GuardianStatus.desativado:
                Tonto();
                break;

            case GuardianStatus.CheckPlayerDistance:
                animator.SetBool("acorda", false);
                PlayerDistance();
                break;

            case GuardianStatus.JumpAtPlayer:
                JumpAtPlayer();
                break;

            case GuardianStatus.FallAtPlayer:
                fallAtPlayer();
                break;

            case GuardianStatus.JumpAtEdge:
                JumpAtEdge();
                break;

            case GuardianStatus.Dash:
                Dash();
                break;

             case GuardianStatus.Attack:
                 AttackPlayer();
                 break;

        }
 

        if(EnemyHealth.currentHealth <= 0)
        {
            status = GuardianStatus.Morto;
            terminou = true;
        }

    }
    private void morto()
    {
        TridenteFlutuando.SetActive(true);
        Wall.GetComponent<Animator>().SetBool("abrindo", true);
        Wall.GetComponent<BoxCollider2D>().enabled = false;
    

        PlayerObj.GetComponent<PlayerMovement>().HaveMagicTrident = true;
        PlayerObj.GetComponent<Animator>().SetBool("Magico", true);
        
        /*
        // activated the have magic trident in player movment
        
        PlayerObj.GetComponent<PlayerMovement>().AtaqueHitBox.SetActive(false); // activated the have magic trident in the Attack obj
        PlayerObj.GetComponent<PlayerMovement>().AtaqueDownHitBox.SetActive(false);
        PlayerObj.GetComponent<PlayerMovement>().AtaqueUpHitBox.SetActive(false);
        
        PlayerObj.GetComponent<PlayerMovement>().AtaqueMagicoHitBox.SetActive(true);
        PlayerObj.GetComponent<PlayerMovement>().AtaqueUpHitBoxMagico.SetActive(true);
        PlayerObj.GetComponent<PlayerMovement>().AtaqueDownHitBoxMagico.SetActive(true);
        
        PlayerObj.GetComponent<Animator>().SetBool("Magico", true);
        PlayerObj.GetComponent<Dash>().enabled = true;
        PlayerObj.GetComponent<Estamina>().enabled = true;
        */
    }

    private void PlayerDistance()
    {

        ///Check if enemy is not half of life
        if (EnemyHealth.currentHealth > EnemyHealth.MaxHealth/2)
        {
            int i = Random.Range(0, 101);
            Debug.Log(i);
            /// if close Attack
            if ( PlayerClose && jumped == false && i >= 30 && i <= 100)
            {
                LookAtPlayer();
                status = GuardianStatus.Attack;
                tempoDeAtaque = 0;
                AttackCollider.enabled = true;
                animator.SetTrigger("ataque");
            }
            /// jump at player
            else
            {
                LookAtPlayer();
                CriaCordenadaAtaque();
                rb.gravityScale = 0;
                status = GuardianStatus.JumpAtPlayer;
                TempoPular = 0;
                animator.SetTrigger("pulo");
            }
        }
        else //if enemy is half of life
        {

            ///random choose from jump on player or do the dash
            int i = Random.Range(0, 101);
            Debug.Log(i);

            if (i >= 31 && i < 61 && PlayerClose)
            {
                LookAtPlayer();
                status = GuardianStatus.Attack;
                tempoDeAtaque = 0;
                AttackCollider.enabled = true;
                
                
         
                ActionChosed = false;
            }
            if (i >= 80 && i < 100)
            {
                LookAtPlayer();
                EdgeIndex = Random.Range(0, 2);
                Debug.Log(EdgeIndex);
                status = GuardianStatus.JumpAtEdge;///do the dash
                TempoPular = 0;
                ActionChosed = true;
                animator.SetTrigger("pulo");
            }
            else
            {
                LookAtPlayer();
                CriaCordenadaAtaque();
                rb.gravityScale = 0;
                status = GuardianStatus.JumpAtPlayer;
                TempoPular = 0;
                ActionChosed = true;
                animator.SetTrigger("pulo");
            }

        }

    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void AttackPlayer()
    {
        tempoDeAtaque += Time.deltaTime;
        
        if (tempoDeAtaque < DuracaoDeAtaque)
        {
            
            if (lookingRight)
            {
                //rb.AddForce(new Vector2(AttackImpulse, transform.position.y), ForceMode2D.Impulse);
                rb.velocity = new Vector2(AttackImpulse, 0f);
               // AudioManager.instance.PlayOneShot(FMODEvents.instance.AtaqueGuardiao, transform.position);
            }
            if (!lookingRight)
            {
                //rb.AddForce(new Vector2(-AttackImpulse, transform.position.y), ForceMode2D.Impulse);
                rb.velocity = new Vector2(-AttackImpulse, 0f);
                //AudioManager.instance.PlayOneShot(FMODEvents.instance.AtaqueGuardiao, transform.position);
            }
            AttackTrigger.SetActive(true);
            Attacked = true;
            
   
        }
        else
        {
            animator.SetTrigger("dormindo");
            AudioManager.instance.PlayOneShot(FMODEvents.instance.AtaqueGuardiao, transform.position);
            Attacked = false;
            AttackTrigger.SetActive(false);
            tempoTonto = 0;
            status = GuardianStatus.desativado;
        }
    }

    private void Tonto()
    {
        ///the enemy will wait a time after conclude a action
        if (m_Grounded)
        {
            
            tempoTonto += Time.deltaTime;
            //disableAnimations();
        }
        
        if (tempoTonto < TempoEsperaTonto)
        {
           
            
        }
        else if (dash && tempoTonto > TempoEsperaTonto)/// check if enemy have jumped to edge, if have do the dash
        {
            rb.velocity = Vector2.zero;
            TempoPular = 0;
            status = GuardianStatus.Dash;

            //Dash();
        }
        else
        {
            rb.velocity = Vector2.zero;
            TempoPular = 0;
            jumped = false;
            status = GuardianStatus.CheckPlayerDistance;
            
        }
    }

    public void JumpAtPlayer()
    {
        
        Look = false;
        TempoPularNoPlayer += Time.deltaTime;

        if (TempoPularNoPlayer < tempoParaPularNoPlayer)/// do nothing while time to jump haven't pass yet
        {
            // animator.SetBool("AtaqueAntecipa", true);
            
        }
        else
        {

            if (transform.position.Equals(cordenadaAlturaAtaque))
            {
                animator.SetTrigger("queda");
                status = GuardianStatus.FallAtPlayer;
                tempoAtaque = Time.time + TempoEsperaAtaque;
                LookAtPlayer();

            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, cordenadaAlturaAtaque, velocidadesubida * Time.deltaTime);
            }
        }

    }


    public void fallAtPlayer()
    {
        if (tempoAtaque < Time.time)
        {
            transform.position = Vector3.MoveTowards(transform.position, miraAtaque, velocidadeAtaque * Time.deltaTime);
        }
        else
        {
            
            miraAtaque = PlayerObj.transform.position;
        }



        if (transform.position.Equals(miraAtaque) || m_Grounded)
        {
            TempoPularNoPlayer = 0;
            rb.gravityScale = 3;
            status = GuardianStatus.desativado;
            tempoTonto = 0;
        }
    }

    public void JumpAtEdge()
    {
        
        TempoPular += Time.deltaTime;
        if (TempoPular < tempoParaPular)/// do nothing while time to jumpAtEdge haven't pass yet
        {

        }
        else if (jumped == false)
        {
            
            float distanceFromPlayer = EdgeRooms[EdgeIndex].transform.position.x - transform.position.x;
            if (m_Grounded)
            {
                rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
                jumped = true;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.PuloGuardiao, transform.position);
              
            }
        }
        /// if enemy hit's the ground go to wait time
        if (m_Grounded == true && jumped == true)
        {
            
            CurrentTimeDash = 0;
            status = GuardianStatus.desativado;
            tempoTonto = 0;
            animator.SetTrigger("dormindo");
            dash = true;
            LookAtPlayer();
        }

    }

    private void Dash()
    {
        LookAtPlayer();
        animator.SetTrigger("dash");
        CurrentTimeDash += Time.deltaTime;
        
        if(CurrentTimeDash >= TimeToDoDash)// do nothing while time to dash haven't pass yet
        {
            
        }
        else if(dashed == false)
        {
            AttackTrigger.SetActive(true);
            if (lookingRight)
            {

                rb.velocity = new Vector2(DashImpulse, 0f);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.DashGuardiao,transform.position);
                
            }
            if (!lookingRight)
            {
                rb.velocity = new Vector2(-DashImpulse, 0f);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.DashGuardiao, transform.position);
                
            }
            dashed = true;
        }
        else if(dashed)  /// if enemy did the dash go to wait time
        {
            tempoTonto = 0;
            status = GuardianStatus.desativado;
            dash = false;
            dashed = false;
            AttackTrigger.SetActive(false);
            //animator.SetBool("Dash", false);
        }
    }

    public void LookAtPlayer()
    {
        if(status == GuardianStatus.desativado && Attacked == false || Attacked == false)
        {
            Vector3 look = transform.InverseTransformPoint(PlayerObj.transform.position);
            float angle = Mathf.Atan2(0f, look.x) * Mathf.Rad2Deg;

            transform.Rotate(0f, angle, 0f);

            
        }
        if (transform.eulerAngles.y > 100)
        {
            lookingRight = false;
        }
        else
        {
            lookingRight = true;
        }

    }


    private void CriaCordenadaAtaque()
    {
        if (lookingRight)
        {
            cordenadaAlturaAtaque = new Vector3(PlayerObj.transform.position.x - distanciaAtaque, this.transform.position.y + alturaAtaque, 0);
        }
        else
        {
            cordenadaAlturaAtaque = new Vector3(PlayerObj.transform.position.x + distanciaAtaque, PlayerObj.transform.position.y + alturaAtaque, 0);
        }
        
    }

    private void OnDrawGizmos()
    {
        //desenha rpoximo ponto da patrulha da mosca
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(cordenadaAlturaAtaque, .5f);

        //desenha rpoximo ponto da patrulha da mosca
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(miraAtaque, .5f);

    }


    public void Gritar()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.FalaGuardiao, transform.position);
        
    }


}
