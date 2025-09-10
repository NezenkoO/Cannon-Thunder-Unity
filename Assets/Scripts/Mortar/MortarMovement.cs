using UnityEngine;
using System;

public class MortarMovement : MonoBehaviour
{
    [SerializeField] private Barrel _barrel;
    [SerializeField] private Mortar _mortar;
    [SerializeField] private MortarMovementSettings _settings;

    private float _currentTiltAngle = 0f;

    private void OnValidate()
    {
        _settings.Validate();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        _mortar.Transform.Rotate(Vector3.up, _settings.RotationSpeed * horizontal * Time.deltaTime);

        _currentTiltAngle += vertical * -1 * _settings.BarrelRotationSpeed * Time.deltaTime;
        _currentTiltAngle = Mathf.Clamp(_currentTiltAngle, _settings.MinTiltAngle, _settings.MaxTiltAngle);

        _barrel.Transform.localEulerAngles = new Vector3(_currentTiltAngle, 0, 0);
    }
}

[Serializable]
public class MortarMovementSettings
{
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float BarrelRotationSpeed { get; private set; }
    [field: SerializeField] public float MaxTiltAngle { get; private set; }
    [field: SerializeField] public float MinTiltAngle { get; private set; }
    
    public void Validate()
    {
        MinTiltAngle = Mathf.Clamp(MinTiltAngle, 0f, 380f);
        MaxTiltAngle = Mathf.Clamp(MaxTiltAngle, 0f, 380f);
        
        if (MaxTiltAngle <= MinTiltAngle)
        {
            MaxTiltAngle = MinTiltAngle + 1f;
        }
    }
}