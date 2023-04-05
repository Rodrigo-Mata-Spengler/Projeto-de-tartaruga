using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpoRenderer : MonoBehaviour
{
    [Header("REAnderizar a linha")]
    private LineRenderer lr;
    [SerializeField]private Transform[] points;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }

    private void Start()
    {
        lr.positionCount = points.Length;
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}
