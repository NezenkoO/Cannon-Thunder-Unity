using UnityEngine;
using System;

public class TrajectoryPredictor : MonoBehaviour
{
    [SerializeField] private HitMarker _hitMarker;
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private TrajectoryPredictorSettings _settings;
    
    private Vector3[] _pointsBuffer;

    private void Awake()
    {
        _trajectoryLine.enabled = true;

        _pointsBuffer = new Vector3[_settings.MaxPoints];
    }

    public void PredictTrajectory(ProjectileProperties projectile)
    {
        var velocity = projectile.Direction * projectile.InitialSpeed;
        var position = projectile.InitialPosition;

        _pointsBuffer[0] = position;
        var actualPoints = 1;

        for (var i = 1; i < _settings.MaxPoints; i++)
        {
            velocity += Physics.gravity * _settings.Increment;
            var nextPosition = position + velocity * _settings.Increment;

            var overlap = velocity.magnitude * _settings.Increment * _settings.RayOverlap;

            if (Physics.Raycast(position, velocity.normalized, out var hit, overlap))
            {
                _pointsBuffer[i] = hit.point;
                actualPoints = i + 1;
                _hitMarker.Enable(hit.point, hit.normal);
                break;
            }

            _hitMarker.Disable();

            _pointsBuffer[i] = nextPosition;
            position = nextPosition;
            actualPoints = i + 1;
        }

        _trajectoryLine.positionCount = actualPoints;
        _trajectoryLine.SetPositions(_pointsBuffer);
    }
}

[Serializable]
public class TrajectoryPredictorSettings
{
    [field: SerializeField, Range(10, 100)] public int MaxPoints { get; private set; }
    [field: SerializeField, Range(0.01f, 0.5f)] public float Increment { get; private set; }
    [field: SerializeField, Range(1.05f, 2f)] public float RayOverlap { get; private set; }
}