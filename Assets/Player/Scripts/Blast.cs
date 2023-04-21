using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public Transform ShootPoint;
    public GameObject BlastPrefab;

    public float FireRate = 15f;
    public float NextTimeToFire = 0f;

    private void Update()
    {
        if(Input.GetButtonDown("Blast") && Time.time >= NextTimeToFire)
        {
           
            NextTimeToFire = Time.time + 1f / FireRate;
            Debug.Log("shoot");
            Shoot();
        }
    }
    private void Shoot()
    {
        Instantiate(BlastPrefab, ShootPoint.position, ShootPoint.rotation);
    }
}
