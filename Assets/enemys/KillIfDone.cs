using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BossType { boss1, boss2}
public class KillIfDone : MonoBehaviour
{
    private PlayerData data;
    [SerializeField] private BossType tipo;

    private void Awake()
    {
        data = SaveSystem.LoadPlayer();
        switch (tipo)
        {
            case BossType.boss1:
                if (data.boss1Derrotado)
                {
                    Destroy(gameObject);
                }
                break;
            case BossType.boss2:
                if (data.boss2Derrotado)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
