using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.RigProceduralAnimation.LegAnimations
{
    public class Mech : MonoBehaviour
    {
        [SerializeField] private LegData[] _legs;
        [SerializeField] private float _stepLength = 0.75f;
        private void Update()
        {
            for(int i = 0; i < _legs.Length; i++)
            {
                ref var legData = ref _legs[i];
                if(!CanMove(i)) continue;
                if(!legData.Leg.IsMoving 
                    && !(Vector3.Distance(legData.Leg.Position, legData.Raycast.Position) > _stepLength)
                    )
                {
                    continue;
                }
                legData.Leg.MoveTo(legData.Raycast.Position);
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
