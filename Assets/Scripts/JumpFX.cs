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
        [SerializeField] private float _jumpDuration;

        List<Action> onJumpEnded = new List<Action>();
        List<Action> onJump = new List<Action>();
        public void PlayAnimation(Transform jumper)
        {
            StartCoroutine(AnimationByTime(jumper, _jumpDuration));
        }
        private IEnumerator AnimationByTime(Transform jumper, float duration)
        {
            foreach (var action in onJump)
            {
                action.Invoke();
            }
            float exprideSeconds = 0;
            float progress = 0;
            Vector3 startPosition = jumper.position;
            while (progress < 1f)
            {
                exprideSeconds += Time.deltaTime;
                progress = exprideSeconds / duration;
                float evaluted = Mathf.Approximately(_yAnimation.Evaluate(progress), 0f)? 0f : _yAnimation.Evaluate(progress);
                jumper.position = startPosition + new Vector3(0, evaluted * _jumpForce, 0);
                yield return null;
            }
            foreach (var action in onJumpEnded)
            {
                action.Invoke();
            }
        }
        public void AddOnJumpEnded(Action action)
        {
            if(onJumpEnded.Contains(action))
            {
                return;
            }
            onJumpEnded.Add(action);
        }
        public void RemoveOnJumpEnded(Action action)
        {
            if(!onJumpEnded.Contains(action))
            {
                return;
            }
            onJumpEnded.Remove(action);
        }
        public void AddOnJump(Action action)
        {
            if (onJump.Contains(action))
            {
                return;
            }
            onJump.Add(action);
        }
        public void RemoveOnJump(Action action)
        {
            if (!onJump.Contains(action))
            {
                return;
            }
            onJump.Remove(action);
        }

    }
}
