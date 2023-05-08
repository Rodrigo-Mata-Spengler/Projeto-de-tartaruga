using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuiscChangeTrigger : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private MusicArea area;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            AudioManager.instance.SetMusicArea(area);
        }
    }
}
