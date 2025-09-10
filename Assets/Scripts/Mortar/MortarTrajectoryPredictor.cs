using UnityEngine;


public class MortarTrajectoryPredictor : TrajectoryPredictor
{
    [SerializeField] private Barrel _barrel;
    [SerializeField] private MortarShellLauncher _mortarShellLauncher;
    [SerializeField] private SliderWithLabel _powerSlide;

    private void Update()
    {
        PredictTrajectory(new ProjectileProperties(_barrel.LookDirection,
            _mortarShellLauncher.LaunchPoint, _powerSlide.Value));
    }
}
