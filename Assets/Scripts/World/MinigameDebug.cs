using NSFWMiniJam3.Manager;
using NSFWMiniJam3.SO;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class MinigameDebug : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private NpcInfo _info;

        public void Interact(PlayerController pc)
        {
            MinigameManager.Instance.Play(_info, null, null);
        }
        public string InteractionKey => "speak";
    }
}
