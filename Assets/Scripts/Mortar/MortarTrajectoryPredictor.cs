using UnityEngine;


public class MortarTrajectoryPredictor : TrajectoryPredictor
{
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [SerializeField] private MortarMovement _mortarMovement;
    [SerializeField] private SliderWithLabel _powerSlide;

    private void Update()
    {
        PredictTrajectory(new ProjectileProperties(_mortarMovement.LookDirection,
            _mortarShellLauncher.LaunchPoint, _powerSlide.Value));
    }
}
