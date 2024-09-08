using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ProjectileConfig
{
    public float Bounciness;
    public float BounceDampingFactor;
    public float MaxBounceCount;
    public float MaxAge;
    public float Gravity;
}
