using System.Collections;
using UnityEngine;

public class CameraShellLaunchAnimation : MonoBehaviour
{
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _shakeIntensity;
    [SerializeField] private float _minShakeValue;

    private Quaternion originalRotation;
    private Coroutine _coroutine;


    private void OnEnable()
    {
        _mortarShellLauncher.ShellLaunched += ShakeCamera;
        originalRotation = transform.localRotation;
    }

    private void OnDisable()
    {
        _mortarShellLauncher.ShellLaunched -= ShakeCamera;
    }

    private void ShakeCamera(ProjectileProperties projectileProperties)
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(ShakeCameraAnimation2(projectileProperties));
    }

    private IEnumerator ShakeCameraAnimation2(ProjectileProperties projectileProperties)
    {
        float time = 0;
        float scaledShakeIntensity = Mathf.Max(_shakeIntensity * projectileProperties.InitialSpeed, _minShakeValue);
        Quaternion targetRotation = originalRotation * Quaternion.Euler(
              Random.Range(-scaledShakeIntensity, scaledShakeIntensity),
              Random.Range(-scaledShakeIntensity, scaledShakeIntensity),
              Random.Range(-scaledShakeIntensity, scaledShakeIntensity)
        );

        while (time < _animationDuration / 2)
        {
            time += Time.deltaTime;
            float shakeProgress = time / _animationDuration;
            transform.localRotation = Quaternion.Lerp(originalRotation, targetRotation, shakeProgress);
            yield return null;
        }

        while (time < _animationDuration)
        {
            time += Time.deltaTime;
            float shakeProgress = time / _animationDuration;
            transform.localRotation = Quaternion.Lerp(targetRotation, originalRotation, shakeProgress);
            yield return null;
        }

        transform.localRotation = originalRotation;
    }
}
