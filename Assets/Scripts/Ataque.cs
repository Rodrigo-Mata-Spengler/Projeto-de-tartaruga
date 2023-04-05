using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    [Header("Box Cast Variables")]
    public Vector2 Area; //trigger area
    public bool Detected = false; // detecte if a enemy was inside
    public Rigidbody2D rb;

    [HideInInspector] public bool up, down, right;
    public float impulseForce;
    public float EnemyimpulseForce;
    private int HitIndex = 0;

    private void Update()
    {
        //creates the box cast(trigger)
       RaycastHit2D BoxInfo = Physics2D.BoxCast(gameObject.GetComponent<Renderer>().bounds.center, Area, 0f, Area);

          if (BoxInfo.collider.gameObject.tag == "Enemy")
          {
                if(HitIndex == 0)
                {
                    BoxInfo.transform.GetComponent<EnemyHealth>().Damage(30f);
                    HitIndex= 1;
                }
          
          }
            //checks if hit a enemy
          if (BoxInfo.collider.gameObject.tag == "Enemy")
          {
                 Detected = true;

                if (up)
                {
                    rb.AddForce(transform.up * impulseForce);

                    up = false;
                }
                if (down)
                {
                    rb.AddForce(transform.up * -impulseForce);
                    down = false;
                }
                if (right)
                {
                    rb.AddForce(transform.right * -impulseForce);
                    BoxInfo.transform.GetComponent<EnemyHitFeedback>().wasHit = true;
                    right = false;
                }
          }
          else
          {
            Detected = false;
             HitIndex= 0;
          }
            
        
    }
    //Draw the box on unity
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(gameObject.GetComponent<Renderer>().bounds.center, Area);

    }
}
