using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.RigProceduralAnimation.LegAnimations
{
    internal class LegTarget : MonoBehaviour
    {
        [SerializeField] private float _stepSpeed = 5.0f;
        [SerializeField] private float _high = 5.0f;
        [SerializeField] private AnimationCurve _stepCurve;

        private Vector3 _position;
        private Movement? _movement;
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
            if(IsMoving)
            {
                var movement = _movement.Value;
                movement.Progress = Mathf.Clamp01(movement.Progress + Time.deltaTime * _stepSpeed);
                _position = movement.Evaluate(Vector3.up * _high, _stepCurve);
                _movement = movement.Progress < 1 ? movement : null;
            }

            _transform.position = _position;
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
