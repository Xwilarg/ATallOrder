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
                DialogueManager.Instance.ShowStory(transform.position, _gameEndText, () =>
                {
                    SceneManager.LoadScene("Menu");
                });
            }
            if (pc.HasKey)
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
