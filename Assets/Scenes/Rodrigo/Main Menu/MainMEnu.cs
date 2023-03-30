using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMEnu : MonoBehaviour
{
    [Header("Botão Jogar")]
    [SerializeField] private string scenaJogar;
    //botão que quando clica vai para o jogo
    public void IrAoJogo()
    {
        SceneManager.LoadScene(scenaJogar);
    }

    //botão que quando clica vai para o menu de opções
}
