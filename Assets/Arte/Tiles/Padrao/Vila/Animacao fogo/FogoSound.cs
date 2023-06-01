using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogoSound : MonoBehaviour
{
    public void Playfogo()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.TochaApaga, transform.position);
    }
}
