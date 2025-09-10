using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Vector3 LookDirection => Transform.up;
    public Transform Transform
    {
        get
        {
            if (_cachedTransform == null)
                _cachedTransform = transform;
            
            return _cachedTransform;
        }
    }
    
    private Transform _cachedTransform;
}