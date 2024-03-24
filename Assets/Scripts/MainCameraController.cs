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
        [SerializeField] Transform _target;
        [SerializeField] float _lookSpeed;
        private Vector3 _offset;
        private void Start()
        {
            _offset = transform.position - _target.position;
        }
        private void Update()
        {
            MoveForward();
            MoveAround();
        }
        private void MoveForward()
        {
            transform.position = _target.position + _offset;
        }
        private void MoveAround()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            if (mouseX != 0 || mouseY != 0)
            {
                transform.LookAt(_target);
                transform.RotateAround(_target.position, transform.up, mouseX);
                transform.RotateAround(_target.position, transform.right, -mouseY);
                _offset = transform.position - _target.position;
            }
        }
    }
}
