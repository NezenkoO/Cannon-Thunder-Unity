using UnityEngine;

[CreateAssetMenu(fileName = "WarFactory")]
public class WarFactory : GameObjectFactory, IWarEntityRecycler
{
    [SerializeField] private ShellConfig _shellConfig;
    [Header("Prefabs")]
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private Explosion _explosionPrefab;


    public Explosion GetExplosion() => Get(_explosionPrefab);

    public Shell GetShell()
    {
        var shell = Get(_shellPrefab);
        shell.SetConfig(_shellConfig);
        return shell;
    }

    private T Get<T>(T prefab) where T : WarEntity
    {
        T instance = CreateGameObjectInstance(prefab);
        instance.WarEntityReclaim = this;
        return instance;
    }

    public void Recycle(WarEntity warEntity)
    {
        Destroy(warEntity.gameObject);  
    }
}
