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
            transform.LookAt(_target);
            transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        }
    }
}
