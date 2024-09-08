using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IShallInteractable
{
    public void ShellTouch(RaycastHit hitInfo);
}
