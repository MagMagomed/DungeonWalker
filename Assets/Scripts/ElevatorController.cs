using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class ElevatorController : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _yTargetPosition;
        [SerializeField] private LayerMask _layerMask;
        private void Update()
        {
            if(Physics.CheckBox(transform.position + Vector3.up, transform.localScale, transform.rotation, _layerMask))
            {
                if(transform.position.y < _yTargetPosition)
                {
                    GetUp();
                }
            }
        }
        private void GetUp()
        {
            StartCoroutine(AnimationOnTime(_duration));
        }
        private void GetDown()
        {
            StartCoroutine(AnimationOnTimeRevert(_duration));
        }
        public IEnumerator AnimationOnTime(float duration)
        {
            float progress = 0f;
            float exprideSeconds = 0f;
            var startPostion = transform.position;
            while (progress < 1f)
            {
                exprideSeconds += Time.deltaTime;
                progress = exprideSeconds / duration;
                var evaluated = _curve.Evaluate(progress);
                transform.position = startPostion + new Vector3(0f, _yTargetPosition * evaluated, 0f);
                yield return null;
            }
        }
        public IEnumerator AnimationOnTimeRevert(float duration)
        {
            float progress = 0f;
            float exprideSeconds = 0f;
            var startPostion = transform.position;
            while (progress < 1f)
            {
                exprideSeconds += Time.deltaTime;
                progress = exprideSeconds / duration;
                var evaluated = _curve.Evaluate(progress);
                transform.position = startPostion - new Vector3(0f, startPostion.y * evaluated, 0f);
                yield return null;
            }
        }
        public void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position + Vector3.up, transform.localScale);
        }
    }
}
