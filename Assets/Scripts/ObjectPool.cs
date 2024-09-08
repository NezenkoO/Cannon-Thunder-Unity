using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _initialSize;

    private List<WarEntity> _availableEntities = new List<WarEntity>();
    private List<WarEntity> _allEntities = new List<WarEntity>();

    protected abstract void CreateNewPooledGameObject();
}
