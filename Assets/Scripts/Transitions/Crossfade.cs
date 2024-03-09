using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Crossfade : MonoBehaviour
    {
        [Range(0, 0.99f)] public float holdBlack = 0.2f;
        public Material crossfade;

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
