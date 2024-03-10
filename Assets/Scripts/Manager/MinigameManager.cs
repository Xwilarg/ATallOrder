using NSFWMiniJam3.SO;
using UnityEngine;
using UnityEngine.UI;

namespace NSFWMiniJam3.Manager
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager Instance { private set; get; }

        public NpcInfo CombatNPC;

        [SerializeField] private Image backgroundSprite;

        [SerializeField] private GameObject npcGameObject;

        [SerializeField] private Transform AttackHolder;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Play(CombatNPC);
        }

        public bool IsPlaying { set; get; }


        public void Play(NpcInfo info)
        {
            IsPlaying = true; // Set that back to false once it's done!

            CombatNPC = info;
            npcGameObject.GetComponent<Image>().sprite = CombatNPC.GameSprite;
            npcGameObject.GetComponent<Animator>().runtimeAnimatorController = CombatNPC.CharacterAnimator;

            GameObject ago = Instantiate(CombatNPC.AttackPatterns[0].gameObject, AttackHolder);

            ago.transform.position = npcGameObject.transform.position;
            ago.SetActive(true);
        }
    }
}
