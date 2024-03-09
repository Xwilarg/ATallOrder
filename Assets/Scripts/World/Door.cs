using NSFWMiniJam3.Manager;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Transform _destination;

        public Room ParentRoom { set; private get; }

        public void Interact()
        {
            RoomsManager.Instance.ShowRoom(ParentRoom);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _destination.position);
        }
    }
}
