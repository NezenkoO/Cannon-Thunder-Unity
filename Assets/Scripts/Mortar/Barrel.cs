using UnityEngine;

public class Barrel : TransformWrapper
{
    public Vector3 DefaultLocalPosition { get; private set; }
    public Vector3 LookDirection => Transform.up;

    private void Awake()
    {
        DefaultLocalPosition = Transform.localPosition;
    }
}