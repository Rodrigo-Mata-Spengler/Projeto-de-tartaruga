using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControler : MonoBehaviour
{
    [SerializeField] public static bool mapa1 = false;
    [SerializeField] private AreaStatus mapa1status;

    [SerializeField] public static bool mapa2 = false;
    [SerializeField] private AreaStatus mapa2status;

    [SerializeField] public static bool mapa3 = false;
    [SerializeField] private AreaStatus mapa3status;

    private void Awake()
    {
        mapa1status.descoberto = mapa1;

        mapa2status.descoberto = mapa2;

        mapa3status.descoberto = mapa3;
    }
    private void Update()
    {
        mapa1 = mapa1status.descoberto;

        mapa2 = mapa2status.descoberto;

        mapa3 = mapa3status.descoberto;
    }

}
