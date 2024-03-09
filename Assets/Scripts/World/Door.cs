using NSFWMiniJam3.Manager;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Node _destination;

        public void Interact(PlayerController pc)
        {
            RoomsManager.Instance.ShowRoom(_destination.ParentRoom);
            pc.transform.position = _destination.transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _destination.transform.position);
        }
    }
}
