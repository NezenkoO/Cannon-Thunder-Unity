using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class WarEntity : GameBehavior
{
    public IWarEntityRecycler WarEntityReclaim { get; set; }
}
