using NSFWMiniJam3.SO;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Npc : MonoBehaviour//, IInteractable
    {
        [SerializeField]
        private NpcInfo _info;

        public bool IsDowned { private set; get; }

        /*public void Interact(PlayerController pc)
        {
            if (IsDowned)
            {

            }
            else
            {
            }
        }*/
    }
}
