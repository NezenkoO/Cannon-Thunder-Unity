using UnityEngine;

public class HitMarker : MonoBehaviour
{
    public void Enable(Vector3 position, Vector3 normal)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
        gameObject.transform.rotation = Quaternion.LookRotation(normal);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}