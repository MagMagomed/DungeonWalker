using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Assets.Scripts.Player.RigProceduralAnimation.LegAnimations
{
    internal class LegTarget : MonoBehaviour
    {
        [SerializeField] private float _stepSpeed = 5.0f;
        [SerializeField] private float _high = 5.0f;
        [SerializeField] private AnimationCurve _stepCurve;

        private Vector3 _position;
        private Movement? _movement;
        private Rotation? _rotation;
        private Transform _transform;

        public Vector3 Position => _position;
        public bool IsMoving => _movement is not null;
        private void Awake()
        {
            _transform = base.transform;
            _position = _transform.position;
        }
        private void Update()
        {
            if (_movement is not null)
            {
                transform.position = _movement.Value.ToPosition;
            }
            if(_rotation is not null)
            {
                transform.LookAt(_rotation.Value.ToRotation);
            }
        }
        public void MoveTo(Vector3 targetPostion)
        {
            if(_movement is null)
            {
                _movement = new Movement()
                {
                    Progress = 0f,
                    FromPosition = _position,
                    ToPosition = targetPostion
                };
            }
            else
            {
                _movement = new Movement()
                {
                    Progress = _movement.Value.Progress,
                    FromPosition = _movement.Value.FromPosition,
                    ToPosition = targetPostion
                };
            }
        }
        public void RotateOn(Vector3 targetRotate)
        {
            if (_rotation is null)
            {
                _rotation = new Rotation()
                {
                    Progress = 0f,
                    FromRotation = _transform.rotation.eulerAngles,
                    ToRotation = targetRotate
                };
            }
            else
            {
                _rotation = new Rotation()
                {
                    Progress = _rotation.Value.Progress,
                    FromRotation = _rotation.Value.FromRotation,
                    ToRotation = targetRotate
                };
            }
        }
        private struct Rotation
        {
            public float Progress;
            public Vector3 FromRotation;
            public Vector3 ToRotation;
        }
        private struct Movement
        {
            public float Progress;
            public Vector3 FromPosition;
            public Vector3 ToPosition;

            public Vector3 Evaluate(in Vector3 up, AnimationCurve stepCurve)
            {
                return Vector3.Lerp(FromPosition, ToPosition, Progress) + up * stepCurve.Evaluate(Progress);
            }
        }
    }
}
