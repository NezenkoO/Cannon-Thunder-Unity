using System.Collections;
using UnityEngine;

public class MortarRecoilAnimation : MonoBehaviour
{
    [SerializeField] private Barrel _barrel;
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [SerializeField] private float _recoilMagnitude;
    [SerializeField] private float _minRecoilValue;
    [SerializeField] private float _animationDuration;

    private Coroutine _coroutine;
    private Vector3 _originalPosition;

    private void OnEnable()
    {
        _mortarShellLauncher.ShellLaunched += Recoil;
        _originalPosition = _barrel.Transform.localPosition;
    }

    private void Recoil(ProjectileProperties projectileProperties)
    {
        if (_coroutine != null) 
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(AnimateRecoil(projectileProperties));
    }

    private IEnumerator AnimateRecoil(ProjectileProperties projectileProperties)
    {
        float time = 0;
        var recoilDistance = Mathf.Max(_recoilMagnitude * projectileProperties.InitialSpeed, _minRecoilValue);
        _barrel.Transform.localPosition = _originalPosition;

        while (time <= _animationDuration / 2)
        {
            time += Time.deltaTime;
            _barrel.Transform.Translate(-_barrel.Transform.up * (recoilDistance * Time.deltaTime), Space.World);
            yield return null;
        }

        while (time <= _animationDuration)
        {
            time += Time.deltaTime;
            _barrel.Transform.Translate(_barrel.Transform.up * (recoilDistance * Time.deltaTime), Space.World);
            yield return null;
        }

        _barrel.Transform.localPosition = _originalPosition;
    }
    
    private void OnDisable()
    {
        _mortarShellLauncher.ShellLaunched -= Recoil;
    }
}