using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Boss3Status { idle, vuneravel, ataque1, ataque2, ataque3};
//ataque 1: patas caem do ceu no ch�o e ficam presas no ch�o por um tempo
//ataque 2: as pi�as atac�o pela direita ou pela esquerda
//ataque 3: as duas pin�as atac�o pelos dois lados
//ataque 4: taca uma das pi�as no ch�o e arrasta pelo mapa
public class CarangueijoBossBehaviuor : MonoBehaviour
{
    [Header("Cabe�a Boss")]
    [SerializeField] private bool isVunerable = false;
}
