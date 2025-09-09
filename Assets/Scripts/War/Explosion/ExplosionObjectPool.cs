using System.Collections.Generic;
using UnityEngine;

public class ExplosionObjectPool : MonoBehaviour, IWarEntityRecycler
{
    [SerializeField] protected WarFactory _warFactory;
    [SerializeField] private int _initialSize = 10;

    private List<WarEntity> _availableEntities = new List<WarEntity>();
    private List<WarEntity> _allEntities = new List<WarEntity>();

    private void Start()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            CreateNewEntity();
        }
    }

    private WarEntity CreateNewEntity()
    {
        var explosion = _warFactory.GetExplosion();
        explosion.WarEntityReclaim = this;
        explosion.gameObject.SetActive(false);
        _availableEntities.Add(explosion);
        _allEntities.Add(explosion);
        return explosion;
    }

    public WarEntity GetFromPool()
    {
        if (_availableEntities.Count <= 0) 
            return CreateNewEntity();
        
        var entity = _availableEntities[0];
        _availableEntities.RemoveAt(0);
        entity.gameObject.SetActive(true);
        return entity;
    }

    public void Recycle(WarEntity warEntity)
    {
        warEntity.gameObject.SetActive(false);
        _availableEntities.Add(warEntity);
    }

    public void DestroyAllEntities()
    {
        foreach (var entity in _allEntities)
        {
            Destroy(entity.gameObject);
        }
        _availableEntities.Clear();
        _allEntities.Clear();
    }
}
