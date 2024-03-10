using NSFWMiniJam3.SO;
using UnityEngine;

namespace NSFWMiniJam3.Manager
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager Instance { private set; get; }

        public NpcInfo CombatNPC;

        [SerializeField] private SpriteRenderer backgroundSprite;

        [SerializeField] private GameObject npcGameObject;

        private void Awake()
        {
            Instance = this;
        }

        public bool IsPlaying { set; get; }


        public void Play(NpcInfo info)
        {
            IsPlaying = true; // Set that back to false once it's done!
            Debug.LogWarning("TODO: Make actual code");
        }
    }
}
