using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Assets.Scripts.Player.RigProceduralAnimation.LegAnimations
{
    public class Mech : MonoBehaviour
    {
        [SerializeField] private LegData[] _legs;
        [SerializeField] private float _minDistance;
        [SerializeField] private float _stepLength = 0.75f;
        [SerializeField] private float _up = 0.75f;
        [SerializeField] private Vector3 _fixNormal;
        private void Update()
        {
            for(int i = 0; i < _legs.Length; i++)
            {
                ref var legData = ref _legs[i];
                legData.Leg.MoveTo(legData.Raycast.Position + Vector3.up * _up);
                legData.Leg.RotateOn(legData.Raycast.Normal);
            }
        }
        private bool CanMove(int legIndex)
        {
            var legsCount = _legs.Length;
            var n1 = _legs[(legIndex + legsCount - 1) % legsCount];
            var n2 = _legs[(legIndex + 1) % legsCount];
            return !n1.Leg.IsMoving && !n2.Leg.IsMoving;
        }
        [Serializable]
        private struct LegData
        {
            [SerializeField] public LegTarget Leg;
            [SerializeField] public LegRaycast Raycast;
        }
    }
}
