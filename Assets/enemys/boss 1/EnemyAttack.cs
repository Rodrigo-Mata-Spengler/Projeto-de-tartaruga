using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Box Cast Variables")]
    public Vector2 Area; //trigger area
    public bool Detected = false; // detecte if a enemy was inside
    public Rigidbody2D rb;
    [Space]
    public float impulseForce;
    public float EnemyimpulseForce;
    private int HitIndex = 0;
    [Space]
    public int Damage;

    private void Update()
    {
        //creates the box cast(trigger)
        RaycastHit2D BoxInfo = Physics2D.BoxCast(gameObject.GetComponent<Renderer>().bounds.center, Area, 0f, Area);

        if (BoxInfo.collider.gameObject.tag == "Player")
        {
            if (HitIndex == 0)
            {
                BoxInfo.transform.GetComponent<Health>().Damage(Damage);
                HitIndex = 1;
                //rb.AddForce(transform.right * -impulseForce);
                BoxInfo.transform.GetComponent<PlayerHitFeedback>().wasHit = true;
            }

        }
        else
        {
            Detected = false;
            HitIndex = 0;
        }


    }
    //Draw the box on unity
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(gameObject.GetComponent<Renderer>().bounds.center, Area);

    }
}
