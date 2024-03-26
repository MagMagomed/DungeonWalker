using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Debugger 
    {
        private Vector3 lastpoint = new Vector3(0, 0f, 0f);
        public void DrowFunction(Vector3 newPoint)
        {
            Debug.DrawLine(lastpoint, newPoint, new Color(newPoint.x, newPoint.y, newPoint.z), float.MaxValue);
            lastpoint = newPoint;
        }
    }
}
