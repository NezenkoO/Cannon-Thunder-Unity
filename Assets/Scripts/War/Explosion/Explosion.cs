using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Explosion : WarEntity
{
    [SerializeField] private ParticleSystemStoppedHandler _particleSystemStoppedHandler;

    private void OnEnable()
    {
        _particleSystemStoppedHandler.ParticleSystemStopped += Recycle;
    }

    private void OnDisable()
    {
        _particleSystemStoppedHandler.ParticleSystemStopped -= Recycle;
    }

    public override void Recycle()
    {
        WarEntityReclaim.Recycle(this);
    }
}
