using Random = UnityEngine.Random;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraShellLaunchAnimation : MonoBehaviour
{
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [SerializeField] private CameraShakeSettings _settings;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _mortarShellLauncher.ShellLaunched += OnShellLaunched;
    }

    private void OnShellLaunched(ProjectileProperties projectile)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ShakeRoutine(projectile.InitialSpeed));
    }

    private IEnumerator ShakeRoutine(float projectileSpeed)
    {
        var elapsed = 0f;
        var intensity = Mathf.Max(_settings.ShakeIntensity * projectileSpeed, _settings.MinShakeValue);

        var targetRotation = _settings.DefaultLookDirection * Quaternion.Euler(
            Random.Range(-intensity, intensity),
            Random.Range(-intensity, intensity),
            Random.Range(-intensity, intensity)
        );

        while (elapsed < _settings.AnimationDuration)
        {
            elapsed += Time.deltaTime;
            var t = elapsed / _settings.AnimationDuration;

            var shakeProgress = t <= 0.5f
                ? t * 2f
                : (1f - t) * 2f;

            transform.localRotation = Quaternion.Lerp(_settings.DefaultLookDirection, targetRotation, shakeProgress);
            yield return null;
        }

        transform.localRotation = _settings.DefaultLookDirection;
    }
    
    private void OnDisable()
    {
        _mortarShellLauncher.ShellLaunched -= OnShellLaunched;
    }
}

[Serializable]
public class CameraShakeSettings
{
    [field: SerializeField] public Quaternion DefaultLookDirection { get; private set; }
    [field: SerializeField] public float AnimationDuration { get; private set; }
    [field: SerializeField] public float ShakeIntensity { get; private set; }
    [field: SerializeField] public float MinShakeValue { get; private set; }
}

