using NSFWMiniJam3.SO;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Prop : MonoBehaviour, IInteractable
    {
        public NpcInfo HiddenNpc { set; get; }

        public void Interact(PlayerController pc)
        {
            throw new System.NotImplementedException();
        }
    }
}
