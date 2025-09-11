using Random = UnityEngine.Random;
using System;
using UnityEngine;
using DG.Tweening;

public class CameraShellLaunchAnimation : MonoBehaviour
{
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [SerializeField] private CameraShakeSettings _settings;

    private Tween _currentTween;

    private void OnEnable()
    {
        _mortarShellLauncher.ShellLaunched += OnShellLaunched;
    }

    private void OnShellLaunched(ProjectileProperties projectile)
    {
        _currentTween?.Kill();

        var intensity = Mathf.Max(_settings.ShakeIntensity * projectile.InitialSpeed, _settings.MinShakeValue);

        var targetRotation = _settings.DefaultLookDirection * Quaternion.Euler(
            Random.Range(-intensity, intensity),
            Random.Range(-intensity, intensity),
            Random.Range(-intensity, intensity)
        );
        
        _currentTween = transform.DOLocalRotateQuaternion(targetRotation, _settings.AnimationDuration / 2f)
            .SetEase(_settings.OutEase)
            .OnComplete(() =>
            {
                transform.DOLocalRotateQuaternion(_settings.DefaultLookDirection, _settings.AnimationDuration / 2f)
                    .SetEase(_settings.InEase);
            });
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
    [field: SerializeField] public Ease OutEase { get; private set; } = Ease.OutQuad;
    [field: SerializeField] public Ease InEase  { get; private set; } = Ease.InQuad;
}