using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoYTigger : MonoBehaviour
{
    public Vector3 Area;

    public bool Detected = false;
    Vector2 direction;
    public Transform Target;



    public void Update()
    {
        Vector2 targetPos = Target.position;

        direction = targetPos - (Vector2)transform.position;

        RaycastHit2D BoxInfo = Physics2D.BoxCast(transform.position,Area,0f, direction);

        //checa se o player entrou no quadrado 
        if (BoxInfo.collider.gameObject.tag == "Player")
        {
            Detected = true;
            Debug.DrawLine(transform.position, targetPos);

        }
        else
        {
            Detected = false;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Area);

       

    }




}
