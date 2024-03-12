using NSFWMiniJam3.Manager;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class NpcIntro : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private TextAsset _introText, _introTwiceText;

        public string InteractionKey => "speak";

        public void Interact(PlayerController pc)
        {
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
