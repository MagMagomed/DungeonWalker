using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Assets.Scripts.Player.RigProceduralAnimation.HandAnimations
{
    public class TwoBoneIKAnimation : MonoBehaviour
    {
        [SerializeField] private TwoBoneIKConstraint _twoBoneIKContraint;
        [SerializeField] private MultiRotationConstraint _multiRotationConstraint;

        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _duration;
        private bool _animationInProgress;

        public void PlayAnimation(Vector3 point)
        {
            _twoBoneIKContraint.transform.position = point;
            if (!_animationInProgress && !IsAnimationEnded)
            {
                StartCoroutine(AnimationOnTime(_duration));
            }
        }
        public void RevertAnimation()
        {
            if (!_animationInProgress && IsAnimationEnded)
            {
                StartCoroutine(AnimationOnTimeRevert(_duration));
            }
        }
        private bool IsAnimationEnded => _twoBoneIKContraint.weight == 1 && _multiRotationConstraint.weight == 1;
        private IEnumerator AnimationOnTime(float duration)
        {
            float exprideSeconds = 0;
            float progress = 0;
            float evaluted = 0f;
            while (progress < 1f)
            {
                _animationInProgress = true;
                exprideSeconds += Time.deltaTime;
                progress = exprideSeconds / duration;
                evaluted = _animationCurve.Evaluate(progress);

                _twoBoneIKContraint.weight = evaluted;
                _multiRotationConstraint.weight = evaluted;
                yield return null;
            }
            _animationInProgress = false;
        }
        private IEnumerator AnimationOnTimeRevert(float duration)
        {
            float exprideSeconds = 0;
            float progress = 0;
            float evaluted = 0f;

            float twoBoneIKContraintStartWeight = _twoBoneIKContraint.weight;
            float multiRotationConstraintWeight = _multiRotationConstraint.weight;
            while (progress < 1f)
            {
                _animationInProgress = true;
                exprideSeconds += Time.deltaTime;
                progress = exprideSeconds / duration;
                evaluted = _animationCurve.Evaluate(progress);

                _multiRotationConstraint.weight = multiRotationConstraintWeight - evaluted;
                _twoBoneIKContraint.weight = twoBoneIKContraintStartWeight - evaluted;
                yield return null;
            }
            _animationInProgress = false;
        }
    }
}
