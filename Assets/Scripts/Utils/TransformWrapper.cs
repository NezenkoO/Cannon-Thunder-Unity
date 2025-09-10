using UnityEngine;

public class TransformWrapper : MonoBehaviour
{
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