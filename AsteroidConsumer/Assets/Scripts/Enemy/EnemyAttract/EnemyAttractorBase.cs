﻿using UnityEngine;

public class EnemyAttractorBase : MonoBehaviour
{
    protected GameObject _go;
    protected EnemyStats _stats;
    public virtual void StartAttraction(GameObject go, EnemyStats stats)
    {
        _go = go;
        _stats = stats;
    }

}
