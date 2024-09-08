using UnityEngine;

public struct ProjectileProperties
{
    public Vector3 Direction;
    public Vector3 InitialPosition;
    public float InitialSpeed;

    public ProjectileProperties(Vector3 direction, Vector3 initialPosition, float initialSpeed)
    {
        Direction = direction;
        InitialPosition = initialPosition;
        InitialSpeed = initialSpeed;
    }
}