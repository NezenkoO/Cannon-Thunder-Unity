using System;
using UnityEngine;
using DG.Tweening;

public class MortarRecoilAnimation : MonoBehaviour
{
    [SerializeField] private Barrel _barrel;
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [SerializeField] private MortarRecoilSettings _settings;

    private Tween _recoilTween;

    private void OnEnable() => 
        _mortarShellLauncher.ShellLaunched += Recoil;

    private void Recoil(ProjectileProperties projectile)
    {
        _recoilTween?.Kill();
        
        var normSpeed = Mathf.Clamp01(projectile.InitialSpeed / _settings.SpeedNormalization);
        var recoilDistance = Mathf.Clamp(
            _settings.RecoilMagnitude * _settings.RecoilCurve.Evaluate(normSpeed),
            _settings.MinRecoilValue,
            _settings.MaxRecoilValue);
        
        var worldPunch = -_barrel.Transform.up * recoilDistance;
        var localPunch = _barrel.Transform.parent != null
            ? _barrel.Transform.parent.InverseTransformVector(worldPunch)
            : worldPunch;

        _barrel.Transform.localPosition = _barrel.DefaultLocalPosition;

        _recoilTween = _barrel.Transform.DOPunchPosition(localPunch,
                _settings.AnimationDuration,
                _settings.Vibrato,
                _settings.Elasticity)
            .OnComplete(() => _barrel.Transform.localPosition = _barrel.DefaultLocalPosition);
    }
    
    private void OnDisable()
    {
        _mortarShellLauncher.ShellLaunched -= Recoil;
        _recoilTween?.Kill();
    }
}

[Serializable]
public class MortarRecoilSettings
{
    [field: SerializeField] public float RecoilMagnitude { get; private set; } = 0.1f;
    [field: SerializeField] public float MinRecoilValue { get; private set; } = 0.01f;
    [field: SerializeField] public float MaxRecoilValue { get; private set; } = 0.12f;
    [field: SerializeField] public float SpeedNormalization { get; private set; } = 50f; // 
    [field: SerializeField] public float AnimationDuration { get; private set; } = 0.18f;
    [field: SerializeField] public int Vibrato { get; private set; } = 6;
    [field: SerializeField] public float Elasticity { get; private set; } = 0.35f;
    [field: SerializeField] public AnimationCurve RecoilCurve { get; private set; } = AnimationCurve.Linear(0, 0.2f, 1, 1f);
}
