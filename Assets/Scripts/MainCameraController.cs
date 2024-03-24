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
    public class MainCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _lookSpeed;
        private Vector3 _offset;
        private void Start()
        {
            _offset = transform.position - _target.position;
        }
        private void Update()
        {
            FollowTarget();
            MoveAroundTarget();
        }
        private void FollowTarget()
        {
            transform.position = _target.position + _offset;
        }
        private void MoveAroundTarget()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            if (mouseX != 0 || mouseY != 0)
            {
                transform.LookAt(_target);
                transform.RotateAround(_target.position, transform.up, mouseX * _lookSpeed);
                transform.RotateAround(_target.position, transform.right, -mouseY * _lookSpeed);
                _offset = transform.position - _target.position;
            }
        }
    }
}
