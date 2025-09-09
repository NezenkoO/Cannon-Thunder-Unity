using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    [SerializeField] private HitMarker _hitMarker;
    [Header("Settings")]
    [SerializeField, Range(10, 100)] private int maxPoints = 50;
    [SerializeField, Range(0.01f, 0.5f)] private float increment = 0.025f;
    [SerializeField, Range(1.05f, 2f)] private float rayOverlap = 1.1f;

    private LineRenderer trajectoryLine;
    private Vector3[] pointsBuffer;

    private void Awake()
    {
        trajectoryLine = GetComponent<LineRenderer>();
        trajectoryLine.enabled = true;

        pointsBuffer = new Vector3[maxPoints];
    }

    public void PredictTrajectory(ProjectileProperties projectile)
    {
        var velocity = projectile.Direction * projectile.InitialSpeed;
        var position = projectile.InitialPosition;

        pointsBuffer[0] = position;

        var actualPoints = 1;

        for (var i = 1; i < maxPoints; i++)
        {
            velocity += Physics.gravity * increment;
            var nextPosition = position + velocity * increment;

            var overlap = velocity.magnitude * increment * rayOverlap;

            if (Physics.Raycast(position, velocity.normalized, out var hit, overlap))
            {
                pointsBuffer[i] = hit.point;
                actualPoints = i + 1;
                
                _hitMarker.Enable(hit.point, hit.normal);
                
                break;
            }

            _hitMarker.Disable();
            
            pointsBuffer[i] = nextPosition;
            position = nextPosition;
            actualPoints = i + 1;
        }

        trajectoryLine.positionCount = actualPoints;
        trajectoryLine.SetPositions(pointsBuffer);
    }
}