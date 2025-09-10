using UnityEngine;

public class Shell : WarEntity
{
    private ShellConfig _shellConfig;
    private ExplosionsSpawner _explosionsSpawner;
    
    private Vector3 _launchPoint, _launchVelocity, _hitNormalInfo;
    private int _currentBounceCount;
    private float _age;

    public void SetConfig(ShellConfig shellConfig)
    {
        _shellConfig = shellConfig;
    }

    public void SetExplosionsSpawner(ExplosionsSpawner explosionObjectPool)
    {
        _explosionsSpawner = explosionObjectPool;
    }

    public void Launch(Vector3 launchPoint, Vector3 launchVelocity)
    {
        _launchPoint = launchPoint;
        _launchVelocity = launchVelocity;

        _currentBounceCount = 0;
        _age = 0;
    }

    public override bool GameUpdate()
    {
        _age += Time.deltaTime;

        if (_age >= _shellConfig.MaxAge)
        {
            _hitNormalInfo = Vector3.one;
            Recycle();
            return false;
        }

        var currentPosition = _launchPoint + _launchVelocity * _age;
        currentPosition.y -= 0.5f * _shellConfig.Gravity * _age * _age;
        var direction = currentPosition - transform.position;

        if (Physics.Raycast(transform.position, direction.normalized, out var hitInfo, direction.magnitude))
        {
            _hitNormalInfo = hitInfo.normal;

            if (hitInfo.transform.TryGetComponent(out IShallInteractable shallInteractable))
            {
                shallInteractable.ShellTouch(hitInfo);
            }
            if (++_currentBounceCount > _shellConfig.MaxBounceCount)
            {
                Recycle();
                return false;
            }

            var currentVelocity = _launchVelocity;
            currentVelocity.y -= 0.5f * _shellConfig.Gravity * _age * _age;
            _launchVelocity = CalculateBounce(currentVelocity, hitInfo.normal);
            _launchPoint = transform.position;
            _age = 0f;
        }
        else
        {
            transform.position = currentPosition;
        }

        return true;
    }

    private Vector3 CalculateBounce(Vector3 incomingVelocity, Vector3 surfaceNormal)
    {
        var normal = surfaceNormal;
        var dotProduct = Vector3.Dot(incomingVelocity, normal);
        var absoluteNormalizedDotProduct = Mathf.Abs(Vector3.Dot(incomingVelocity.normalized, normal.normalized));
        var bounceDampingFactor = Mathf.Clamp01(_shellConfig.BounceDampingFactor - absoluteNormalizedDotProduct);
        var reflectedVelocity = incomingVelocity - 2 * dotProduct * normal;

        reflectedVelocity *= _shellConfig.Bounciness * bounceDampingFactor;
        reflectedVelocity.y -= _shellConfig.Gravity * Time.deltaTime;

        return reflectedVelocity;
    }
    
    public override void Recycle()
    {
        if(_explosionsSpawner != null)
        {
            _explosionsSpawner.AddExplosion(transform.position, Quaternion.LookRotation(_hitNormalInfo));
        }

        WarEntityReclaim.Recycle(this);
    }
}
