using Cinemachine;
using NSFWMiniJam3.Manager;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Node _destination;

        public Room Destination => _destination.ParentRoom;

        public void Interact(PlayerController pc)
        {
            TransitionManager.Instance.StartTransition((CinemachineVirtualCamera vCam) =>
            {
                RoomsManager.Instance.ShowRoom(_destination.ParentRoom);
                pc.transform.position = _destination.transform.position;

                vCam.ForceCameraPosition(new(pc.transform.position.x, vCam.transform.position.y, vCam.transform.position.z), vCam.transform.rotation);
            });
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _destination.transform.position);
        }
    }
}
