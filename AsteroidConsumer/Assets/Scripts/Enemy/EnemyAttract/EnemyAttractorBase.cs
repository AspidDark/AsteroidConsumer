using System;
using UnityEngine;

public class EnemyAttractorBase : MonoBehaviour
{
    protected GameObject _go;
    protected EnemyStats _stats;
    Guid _objectId;
    public virtual void StartAttraction(GameObject go, EnemyStats stats)
    {
        _go = go;
        _stats = stats;
        _objectId = stats.objectId;
    }

}
