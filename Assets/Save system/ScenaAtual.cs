using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenaAtual : MonoBehaviour
{
    public string scenaAtual;

    private void LateUpdate()
    {
        scenaAtual = SceneManager.GetActiveScene().name;
    }
}
