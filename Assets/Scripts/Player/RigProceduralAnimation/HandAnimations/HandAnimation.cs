using Assets.Scripts.Player.RigProceduralAnimation.HandAnimations;
using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering.UI;

namespace Assets.Scripts.Player.RigProceduralAnimation.HandAnimation
{
    public class HandAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _handRig;

        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private float _maxDistance;
        [SerializeField] private Vector3 _rayOffset;
        [SerializeField] private Vector3 _rayDirection;

        [SerializeField] private TwoBoneIKAnimation _twoBoneIKanimation;

        private Ray _ray;
        private void Update()
        {
            var rayTouchWall = false;
            _ray = new Ray(_handRig.position + _handRig.rotation * _rayOffset, _handRig.rotation * _rayDirection);
            rayTouchWall = Physics.Raycast(_ray, out RaycastHit hitInfo, _maxDistance, _wallLayer, QueryTriggerInteraction.UseGlobal);

            if (rayTouchWall) _twoBoneIKanimation.PlayAnimation(hitInfo.point);
            else _twoBoneIKanimation.RevertAnimation();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_ray);
        }
    }
}
