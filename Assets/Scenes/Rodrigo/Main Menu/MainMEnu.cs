using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMEnu : MonoBehaviour
{
    [Header("Bot�o Jogar")]
    [SerializeField] private string scenaJogar;
    //bot�o que quando clica vai para o jogo
    public void IrAoJogo()
    {
        SceneManager.LoadScene(scenaJogar);
    }

    //bot�o que quando clica vai para o menu de op��es
}
