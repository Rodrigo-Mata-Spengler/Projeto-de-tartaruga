using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepGoing : MonoBehaviour
{
    public float timeAlive = 0;
    [SerializeField]private string compareTag;

    private void Awake()
    {
        timeAlive = Time.time;

        Keep();
    }
    private void Keep()
    {
        GameObject[] outros = GameObject.FindGameObjectsWithTag(compareTag);

        foreach (var outro in outros)
        {
            if (timeAlive < outro.GetComponent<KeepGoing>().timeAlive)
            {
                Destroy(outro.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
