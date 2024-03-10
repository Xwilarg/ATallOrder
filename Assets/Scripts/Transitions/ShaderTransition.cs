using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class ShaderTransition : MonoBehaviour
    {
        [SerializeField][Range(0, 0.99f)] private float holdBlack = 0.2f;
        [SerializeField] private Material _shader;

        private float _progressMultiplier => 1f / (1f - holdBlack);

        public void OnTransitionProgress(float veilProgress) {
            SetProgress(veilProgress);
        }

        public void TransitionEnd() {
            SetProgress(0f);
        }

        private void SetProgress(float veilProgress) {
            _shader.SetFloat("_Progress", veilProgress * _progressMultiplier);
        }

        private void OnDestroy()
        {
            SetProgress(0f);
        }
    }
}
