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

        [SerializeField] private Transform attackHolder;
        [SerializeField] private GameObject attackPointRef;

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

            _fightContainer.SetActive(true);
        }
    }
}
