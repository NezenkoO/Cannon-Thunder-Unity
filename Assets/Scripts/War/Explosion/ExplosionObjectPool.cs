using System.Collections.Generic;
using UnityEngine;

public class ExplosionObjectPool : MonoBehaviour, IWarEntityRecycler
{
    [SerializeField] private WarFactory _warFactory;
    [SerializeField] private int _initialSize = 10;

    private readonly List<WarEntity> _availableEntities = new();
    private readonly List<WarEntity> _allEntities = new();

    private void Start()
    {
        for (var i = 0; i < _initialSize; i++)
        {
            CreateNewEntity();
        }
    }

    private WarEntity CreateNewEntity()
    {
        var entity = _warFactory.GetExplosion();
        entity.WarEntityReclaim = this;
        entity.gameObject.SetActive(false);

        _availableEntities.Add(entity);
        _allEntities.Add(entity);

        return entity;
    }

    public WarEntity GetFromPool()
    {
        var entity = _availableEntities.Count > 0
            ? _availableEntities[0]
            : CreateNewEntity();

        if (_availableEntities.Count > 0)
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