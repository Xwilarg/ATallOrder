using NSFWMiniJam3.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NSFWMiniJam3.World
{
    public class NpcIntro : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private TextAsset _introText, _introTwiceText, _gameEndText;

        public string InteractionKey => "speak";

        public void Interact(PlayerController pc)
        {
            if (GameManager.Instance.MinigameWon == 3)
            {
                var sr = GetComponent<SpriteRenderer>();
                sr.sortingLayerName = "Player";
                sr.sortingOrder = -1;
                transform.position = new(transform.position.x, 1.17f);
                DialogueManager.Instance.ShowStory(transform.position, _gameEndText, () =>
                {
                    SceneManager.LoadScene("Menu");
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
    }
}
