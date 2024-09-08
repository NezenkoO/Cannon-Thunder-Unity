using System.Collections;
using UnityEngine;

public class MortarRecoilAnimation : MonoBehaviour
{
    [SerializeField] private Transform _barrel;
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [SerializeField] private float _recoilDistance;
    [SerializeField] private float _animationDuration;

    private Coroutine _coroutine;
    private Vector3 _originalPosition;

    private void OnEnable()
    {
        _mortarShellLauncher.ShellLaunched += Recoil;
        _originalPosition = _barrel.localPosition;
    }

    private void OnDisable()
    {
        _mortarShellLauncher.ShellLaunched -= Recoil;
    }

    private void Recoil()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(AnimateRecoil());
    }

    private IEnumerator AnimateRecoil()
    {
        float time = 0;

        _barrel.localPosition = _originalPosition;

        while (time <= _animationDuration / 2)
        {
            time += Time.deltaTime;
            _barrel.Translate(-_barrel.up * (_recoilDistance * Time.deltaTime), Space.World);
            yield return null;
        }

        while (time <= _animationDuration)
        {
            time += Time.deltaTime;
            _barrel.Translate(_barrel.up * (_recoilDistance * Time.deltaTime), Space.World);
            yield return null;
        }

        _barrel.localPosition = _originalPosition;
    }
}
