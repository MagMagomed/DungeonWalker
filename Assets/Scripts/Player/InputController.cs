using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class InputController : MonoBehaviour
    {
        private Vector3 _moveDirection;
        private bool _sprint;
        private bool _jump;

        public bool Sprint { get { return _sprint; } }
        public bool Jump { get { return _jump; } }
        public Vector3 MoveDirection { get { return _moveDirection; } }

        private void LateUpdate()
        {
            _moveDirection = GetMoveDirection();
            _sprint = Input.GetButton("Fire3");
            _jump = Input.GetButtonDown("Jump");
        }

        private Vector3 GetMoveDirection()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            var moveDirection = new Vector3(moveHorizontal, 0f, moveVertical);
            return moveDirection.normalized;
        }
    }
}
