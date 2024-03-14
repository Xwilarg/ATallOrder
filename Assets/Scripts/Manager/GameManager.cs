using UnityEngine;

namespace NSFWMiniJam3.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField]
        private Transform[] _spots;

        private int _spotIndex;

        public Transform NextSpot
        {
            get
            {
                _spotIndex++;
                return _spots[_spotIndex];
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        public bool CanMove => RoomsManager.Instance.EnemyRunningAway == 0 && !MinigameManager.Instance.IsPlaying && !DialogueManager.Instance.IsPlayingStory;
    }
}
