using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControler : MonoBehaviour
{
    [SerializeField] public static bool mapa1 = false;
    [SerializeField] private AreaStatus mapa1status;

    [SerializeField] public static bool mapa2 = false;
    [SerializeField] private AreaStatus mapa2status;

    [SerializeField] public static bool mapa3_1 = false;
    [SerializeField] private AreaStatus mapa3_1status;

    [SerializeField] public static bool mapa3_2 = false;
    [SerializeField] private AreaStatus mapa3_2status;

    [SerializeField] public static bool mapa3_3 = false;
    [SerializeField] private AreaStatus mapa3_3status;

    [SerializeField] public static bool mapa4_1 = false;
    [SerializeField] private AreaStatus mapa4_1status;

    [SerializeField] public static bool mapa4_2 = false;
    [SerializeField] private AreaStatus mapa4_2status;

    [SerializeField] public static bool mapa4_3 = false;
    [SerializeField] private AreaStatus mapa4_3status;

    [SerializeField] public static bool mapa4_4 = false;
    [SerializeField] private AreaStatus mapa4_4status;

    [SerializeField] public static bool mapa5 = false;
    [SerializeField] private AreaStatus mapa5status;

    [SerializeField] public static bool mapa6 = false;
    [SerializeField] private AreaStatus mapa6status;

    private void Awake()
    {
        mapa1status.descoberto = mapa1;

        mapa2status.descoberto = mapa2;

        mapa3_1status.descoberto = mapa3_1;

        mapa3_2status.descoberto = mapa3_2;

        mapa3_3status.descoberto = mapa3_3;

        mapa4_1status.descoberto = mapa4_1;

        mapa4_2status.descoberto = mapa4_2;

        mapa4_3status.descoberto = mapa4_3;

        mapa4_4status.descoberto = mapa4_4;

        mapa5status.descoberto = mapa5;

        mapa6status.descoberto = mapa6;
    }
    private void Update()
    {
        mapa1 = mapa1status.descoberto;

        mapa2 = mapa2status.descoberto;

        mapa3_1 = mapa3_1status.descoberto;

        mapa3_2 = mapa3_2status.descoberto;

        mapa3_3 = mapa3_3status.descoberto;

        mapa4_1 = mapa4_1status.descoberto;

        mapa4_2 = mapa4_2status.descoberto;

        mapa4_3 = mapa4_3status.descoberto;

        mapa4_4 = mapa4_4status.descoberto;

        mapa5 = mapa5status.descoberto;

        mapa6 = mapa6status.descoberto;
    }

}
