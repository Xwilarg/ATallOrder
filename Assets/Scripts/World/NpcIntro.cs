using NSFWMiniJam3.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NSFWMiniJam3.World
{
    public class NpcIntro : MonoBehaviour, IInteractable
    {
        public static NpcIntro Instance { private set; get; }

        [SerializeField]
        private TextAsset _introText, _introTwiceText, _gameEndText;

        [SerializeField]
        private GameObject _endCG;

        [SerializeField]
        private Sprite _endSprite;

        public string InteractionKey => "speak";

        public bool CanInteract => true;

        private void Awake()
        {
            Instance = this;
        }

        public void Spe()
        {
            var sr = GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Player";
            sr.sortingOrder = -1;
            sr.sprite = _endSprite;
            transform.position = new(transform.position.x, 1.79f);
        }

        public void Interact(PlayerController pc)
        {
            if (GameManager.Instance.MinigameWon == 3)
            {
                DialogueManager.Instance.ShowStory(transform.position, _gameEndText, () =>
                {
                    _endCG.SetActive(true);

                    StartCoroutine(WaitAndMenu());
                });
            }
            else if (pc.HasKey)
            {
                DialogueManager.Instance.ShowStory(transform.position, _introTwiceText);
            }
            else
            {
                pc.HasKey = true;
                DialogueManager.Instance.ShowStory(transform.position, _introText);
            }
        }

        private IEnumerator WaitAndMenu()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Menu");
        }
    }
}
