using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : TruongMonoBehaviour
{
    [SerializeField] protected List<Transform> points;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPoints();
    }

    protected void LoadPoints()
    {
        foreach(Transform t in transform) 
            points.Add(t);
    }
}
