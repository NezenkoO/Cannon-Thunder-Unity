using UnityEngine;

public class MortarMovement : MonoBehaviour
{
    public Vector3 LookDirection => _barrel.up;

    [SerializeField] private Transform _barrel;
    [SerializeField] private Transform _mortar;
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
        _barrel.localEulerAngles = new Vector3(_currentTiltAngle, 0, 0);
    }
}