using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomGarra : MonoBehaviour
{
    public void Ataque6PlaySound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.SombraAntecipaçãoGarra, this.transform.position);
    }
}
