using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ExplosionsSpawner : MonoBehaviour
{
    [SerializeField] private ExplosionObjectPool _explosionObjectPool;

    public void AddExplosion(Vector3 position, Quaternion rotation)
    {
        var explosion = _explosionObjectPool.GetFromPool();
        explosion.transform.position = position;
        explosion.transform.rotation = rotation;
    }
}
