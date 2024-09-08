using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemStoppedHandler : MonoBehaviour
{
    public event Action ParticleSystemStopped;

    private void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        ParticleSystemStopped?.Invoke();
    }
}
