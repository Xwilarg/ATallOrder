using NSFWMiniJam3.SO;
using UnityEngine;
using UnityEngine.UI;

namespace NSFWMiniJam3.Manager
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager Instance { private set; get; }

        [SerializeField]
        private GameObject _fightContainer;

        [SerializeField] private Image backgroundSprite;

        [SerializeField] private GameObject npcGameObject;

        [SerializeField] private Transform AttackHolder;

        private void Awake()
        {
            Instance = this;
        }

        public bool IsPlaying { set; get; }


        public void Play(NpcInfo info)
        {
            IsPlaying = true; // Set that back to false once it's done!

            npcGameObject.GetComponent<Image>().sprite = info.GameSprite;
            npcGameObject.GetComponent<Animator>().runtimeAnimatorController = info.CharacterAnimator;

            GameObject ago = Instantiate(info.AttackPatterns[0].gameObject, AttackHolder);

            ago.transform.position = npcGameObject.transform.position;
            ago.SetActive(true);

            _fightContainer.SetActive(true);
        }
    }
}
