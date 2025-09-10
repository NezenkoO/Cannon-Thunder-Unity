using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MortarShellLauncher : MonoBehaviour
{
    public event Action<ProjectileProperties> ShellLaunched;
    public Vector3 LaunchPoint => _launchPoint.transform.position;

    [SerializeField] private Barrel _barrel;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private WarFactory _warFactory;
    [SerializeField] private SliderWithLabel _powerSlider;
    [SerializeField] private ExplosionsSpawner _explosionsSpawner;

    private readonly GameBehaviorCollection _shellGameBehaviorCollection = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        _shellGameBehaviorCollection.GameUpdate();
    }

    public void Launch()
    {
        var shell = _warFactory.GetShell();
        shell.SetExplosionsSpawner(_explosionsSpawner);
        shell.Launch(LaunchPoint, _barrel.LookDirection * _powerSlider.Value);
        _shellGameBehaviorCollection.Add(shell);
        ShellLaunched?.Invoke(new ProjectileProperties(_barrel.LookDirection, LaunchPoint, _powerSlider.Value));
    }
}
