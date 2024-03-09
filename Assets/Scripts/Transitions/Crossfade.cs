using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Crossfade : MonoBehaviour
    {
        [SerializeField][Range(0, 0.99f)] private float holdBlack = 0.2f;
        [SerializeField] private Material crossfade;

        private float _progressMultiplier => 1f / (1 - holdBlack);

        public void OnTransitionProgress(float veilProgress) {
            SetProgress(veilProgress);
        }

        public void TransitionEnd() {
            SetProgress(0);
        }

        private void SetProgress(float veilProgress) {
            crossfade.SetFloat("_Progress", veilProgress * _progressMultiplier);
        }
    }
}
