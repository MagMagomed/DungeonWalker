using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class JumpFX : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _yAnimation;
        [SerializeField] private float _jumpForce;

        public void PlayAnimation(Transform jumper, float duration)
        {
            StartCoroutine(AnimationByTime(jumper, duration));
        }
        private IEnumerator AnimationByTime(Transform jumper, float duration)
        {
            float exprideSeconds = 0;
            float progress = 0;
            float startY = jumper.position.y;
            while (progress < 1)
            {
                exprideSeconds += Time.deltaTime;
                progress = exprideSeconds / duration;
                float evaluted = _yAnimation.Evaluate(progress);
                Debug.Log(evaluted);
                jumper.position = new Vector3(jumper.position.x, startY + evaluted * _jumpForce, jumper.position.z);

                yield return null;
            }
        }
    }
}
