using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine;

public class CameraShellLaunchAnimation : MonoBehaviour
{
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [Header("Settings")]
    [SerializeField] private Quaternion _baseRotation;
    [SerializeField] private float _animationDuration = 0.3f;
    [SerializeField] private float _shakeIntensity = 1f;
    [SerializeField] private float _minShakeValue = 0.1f;

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
        var intensity = Mathf.Max(_shakeIntensity * projectileSpeed, _minShakeValue);

        var targetRotation = _baseRotation * Quaternion.Euler(
            Random.Range(-intensity, intensity),
            Random.Range(-intensity, intensity),
            Random.Range(-intensity, intensity)
        );

        while (elapsed < _animationDuration)
        {
            elapsed += Time.deltaTime;
            var t = elapsed / _animationDuration;
            
            var shakeProgress = t <= 0.5f 
                ? t * 2f 
                : (1f - t) * 2f;

            transform.localRotation = Quaternion.Lerp(_baseRotation, targetRotation, shakeProgress);
            yield return null;
        }

        transform.localRotation = _baseRotation;
    }
    
    private void OnDisable()
    {
        _mortarShellLauncher.ShellLaunched -= OnShellLaunched;
    }
}
