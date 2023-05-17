using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepGoing : MonoBehaviour
{
    public float timeAlive = 0;
    public string tag;

    void Start()
    {
        timeAlive = Time.time;
    }

    private void Update()
    {
        
        GameObject[] outros = GameObject.FindGameObjectsWithTag(tag);

        foreach (var outro in outros)
        {
            if (timeAlive < outro.GetComponent<KeepGoing>().timeAlive)
            {
                Destroy(outro);
            }
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
}
