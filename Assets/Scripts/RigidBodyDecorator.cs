using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class RigidBodyDecorator : MonoBehaviour
    {
        [SerializeField] private Rigidbody body;
        public void SetUseGravity(bool active)
        {
            body.useGravity = active;
        }
    }
}
