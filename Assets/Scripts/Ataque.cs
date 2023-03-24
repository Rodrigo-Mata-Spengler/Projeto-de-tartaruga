using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    [Header("Box Cast Variables")]
    public Vector2 Area;
    public bool Detected = false;
    Vector2 direction;


    private void Update()
    {
        //direction= Vector2.zero;
       RaycastHit2D BoxInfo = Physics2D.BoxCast(gameObject.GetComponent<Renderer>().bounds.center, Area, 0f, Area);

      if (BoxInfo.collider.gameObject.tag == "Enemy")
      {
              Detected = true;
             // Debug.DrawLine(transform.position, targetPos);

             //BoxInfo.transform.gameObject.LifeDamage();
            BoxInfo.transform.gameObject.SetActive(false);

      }
      else
      {
         Detected= false;
      }
            
        
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(gameObject.GetComponent<Renderer>().bounds.center, Area);

    }
}
