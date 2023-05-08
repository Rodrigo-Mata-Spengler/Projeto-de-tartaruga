using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Boss3Status { idle, vuneravel, ataque1, ataque2, ataque3};
//ataque 1: patas caem do ceu no chão e ficam presas no chão por um tempo
//ataque 2: as piças atacão pela direita ou pela esquerda
//ataque 3: as duas pinças atacão pelos dois lados
//ataque 4: taca uma das piças no chão e arrasta pelo mapa
public class CarangueijoBossBehaviuor : MonoBehaviour
{
    [Header("Cabeça Boss")]
    [SerializeField] private bool isVunerable = false;
}
