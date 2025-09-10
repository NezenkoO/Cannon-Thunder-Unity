using UnityEngine;

public class MortarMovement : MonoBehaviour
{
    [SerializeField] private Barrel _barrel;

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _barrelRotationSpeed;
    [SerializeField] private float _maxTiltAngle = 120;
    [SerializeField] private float _minTiltAngle = 60;

    private float _currentTiltAngle = 0f;

    private void OnValidate()
    {
        if (_maxTiltAngle < _minTiltAngle)
        {
            _maxTiltAngle = _minTiltAngle;
        }
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up, _rotationSpeed * horizontal * Time.deltaTime);

        _currentTiltAngle += vertical * -1 * _barrelRotationSpeed * Time.deltaTime;
        _currentTiltAngle = Mathf.Clamp(_currentTiltAngle, _minTiltAngle, _maxTiltAngle);

        _barrel.Transform.localEulerAngles = new Vector3(_currentTiltAngle, 0, 0);
    }
}