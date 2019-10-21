using UnityEngine;

public class EnemyAttractorBase : MonoBehaviour
{
    protected GameObject _go;
    protected EnemyStats _stats;
    public EnemyAttractorBase(GameObject go, EnemyStats stats)
    {
        _go = go;
        _stats = stats;

        //в зависимости от stats  выбираем объект аттрактора!
    }

}
