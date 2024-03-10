using UnityEngine;

namespace NSFWMiniJam3.Manager
{
    public class AssetManager : MonoBehaviour
    {
        public static AssetManager Instance { private set; get; }

        [SerializeField]
        private GameObject _npcPrefab;
        public GameObject NpcPrefab => _npcPrefab;
    }
}
