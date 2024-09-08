using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MortarShellLauncher : MonoBehaviour
{
    public event Action ShellLaunched;
    public Vector3 LaunchPoint => _launchPoint.transform.position;

    [SerializeField] private Transform _launchPoint;
    [SerializeField] private WarFactory _warFactory;
    [SerializeField] private MortarMovement _mortarMovement;
    [SerializeField] private PowerSlider _powerSlider;
    [SerializeField] private ExplosionsSpawner _explosionsSpawner;

    private GameBehaviorCollection _shellGameBehaviorCollection = new GameBehaviorCollection();

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
        shell.Launch(LaunchPoint, _mortarMovement.LookDirection * _powerSlider.Value);
        _shellGameBehaviorCollection.Add(shell);
        ShellLaunched?.Invoke();
    }
}
