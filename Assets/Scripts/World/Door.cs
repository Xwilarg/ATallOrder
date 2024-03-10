using NSFWMiniJam3.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace NSFWMiniJam3.World
{
    public class Door : MonoBehaviour, IInteractable, IRoomOwned
    {
        [SerializeField]
        private Node _destination;
        [SerializeField]
        private UnityEvent enterDoorEvent;

        private PlayerController _lastSeenPlayerController;

        public Room ParentRoom { set; get; }

        public void Interact(PlayerController pc)
        {
            _lastSeenPlayerController = pc;
            enterDoorEvent?.Invoke();
        }

        public void ChangeRoom(int _) {
            RoomsManager.Instance.ShowRoom(_destination.ParentRoom);
            _lastSeenPlayerController.transform.position = _destination.transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _destination.transform.position);
        }
    }
}
