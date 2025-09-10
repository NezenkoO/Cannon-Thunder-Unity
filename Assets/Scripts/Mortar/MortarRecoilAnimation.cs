using System.Collections;
using System;
using UnityEngine;

public class MortarRecoilAnimation : MonoBehaviour
{
    [SerializeField] private Barrel _barrel;
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [SerializeField] private MortarRecoilSettings _settings;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _mortarShellLauncher.ShellLaunched += Recoil;
    }

    private void Recoil(ProjectileProperties projectileProperties)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(AnimateRecoil(projectileProperties));
    }

    private IEnumerator AnimateRecoil(ProjectileProperties projectileProperties)
    {
        var time = 0f;
        var recoilDistance = Mathf.Max(_settings.RecoilMagnitude * projectileProperties.InitialSpeed, _settings.MinRecoilValue);
        _barrel.Transform.localPosition = _barrel.DefaultLocalPosition;
        
        while (time <= _settings.AnimationDuration / 2f)
        {
            time += Time.deltaTime;
            _barrel.Transform.Translate(-_barrel.Transform.up * (recoilDistance * Time.deltaTime), Space.World);
            yield return null;
        }
        
        while (time <= _settings.AnimationDuration)
        {
            time += Time.deltaTime;
            _barrel.Transform.Translate(_barrel.Transform.up * (recoilDistance * Time.deltaTime), Space.World);
            yield return null;
        }

        _barrel.Transform.localPosition = _barrel.DefaultLocalPosition;
    }
    
    private void OnDisable()
    {
        _mortarShellLauncher.ShellLaunched -= Recoil;
    }
}

[Serializable]
public class MortarRecoilSettings
{
    [field: SerializeField] public float RecoilMagnitude { get; private set; }
    [field: SerializeField] public float MinRecoilValue { get; private set; }
    [field: SerializeField] public float AnimationDuration { get; private set; }
}