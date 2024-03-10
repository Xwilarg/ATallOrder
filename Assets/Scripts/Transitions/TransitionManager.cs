using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class TransitionManager : MonoBehaviour
    {
        public static TransitionManager Instance { private set; get; }

        private ShaderTransition _shaderTransition;

        [Range(0.0f, 2)] [SerializeField] private float transitionDuration = 0.6f;

        private System.Action _transitionEndCallback;

        private float midDuration => transitionDuration / 2f;
        private bool transitionHappening = false;
        private bool timeIsBeforeRoomChange = true;
        private float transitionTime = 0;

        // from range [0, 1]
        // 0 - screen is visible
        // 1 - screen is not visible
        private float veilProgress {
            get {
                if (timeIsBeforeRoomChange) {
                    return transitionTime / midDuration;
                }
                return 1 - ((transitionTime - midDuration) / midDuration);
            }
        }

        // function called to start transition
        public void StartTransition(System.Action callback) {
            _transitionEndCallback = callback;

            transitionHappening = true;
        }

        private void Awake()
        {
            Instance = this;

            _shaderTransition = GetComponent<ShaderTransition>();
        }

        private void Update()
        {
            HandleHappeningTransition();
        }

        private void HandleHappeningTransition() {
            // returning if there is no transition happening
            if (!transitionHappening) {
                return;
            }

            _shaderTransition.OnTransitionProgress(veilProgress);

            transitionTime += Time.deltaTime;

            if (transitionTime > midDuration && timeIsBeforeRoomChange) {
                _transitionEndCallback?.Invoke();
                timeIsBeforeRoomChange = false;
            }

            // checking if transition is over
            if (transitionTime > transitionDuration) {
                _shaderTransition.TransitionEnd();
                transitionHappening = false;
                timeIsBeforeRoomChange = true;
                transitionTime = 0;
                _transitionEndCallback = null;
            }
        }
    }
}
