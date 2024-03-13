using UnityEngine;

namespace NSFWMiniJam3.Manager
{
    public class AssetManager : MonoBehaviour
    {
        public static AssetManager Instance { private set; get; }

        [SerializeField]
        private GameObject _npcPrefab;
        public GameObject NpcPrefab => _npcPrefab;

        [SerializeField]
        private TextAsset _doorLocked;
        public TextAsset DoorLocked => _doorLocked;

        [SerializeField]
        private TextAsset _propEmpty;
        public TextAsset PropEmpty => _propEmpty;

        private void Awake()
        {
            Instance = this;
        }
    }
}
