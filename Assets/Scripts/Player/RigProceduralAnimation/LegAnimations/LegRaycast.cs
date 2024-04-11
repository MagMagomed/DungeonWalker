using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.RigProceduralAnimation.LegAnimations
{
    internal class LegRaycast : MonoBehaviour
    {
        private RaycastHit _hit;
        private Transform _transform;
        private Ray _ray;
        [SerializeField] private Vector3 _rayDirection;
        public Vector3 Position => _hit.point;
        public Vector3 Normal => _hit.normal;

        private void Awake()
        {
            _transform = base.transform;
        }
        private void Update()
        {
            _ray = new Ray(_transform.position, -(_transform.rotation * _rayDirection));
            Physics.Raycast(_ray, out _hit);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_ray);
            Gizmos.DrawSphere(_hit.point, 0.1f);
        }
    }
}
