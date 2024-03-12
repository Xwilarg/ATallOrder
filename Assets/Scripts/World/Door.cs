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

        public string InteractionKey => "enter";

        public void Interact(PlayerController pc)
        {
            TransitionManager.Instance.StartTransition((CinemachineVirtualCamera vCam) =>
            {
                RoomsManager.Instance.ShowRoom(_destination.ParentRoom);

                var localPos = pc.transform.localPosition;
                pc.transform.parent = _destination.ParentRoom.transform;
                pc.transform.position = _destination.transform.position;
                pc.transform.localPosition = new(pc.transform.localPosition.x, localPos.y, 0f);

                var composer = vCam.GetCinemachineComponent<CinemachineTransposer>();
                vCam.ForceCameraPosition(new(pc.transform.position.x, pc.transform.position.y + composer.m_FollowOffset.y, vCam.transform.position.z), vCam.transform.rotation);
            });
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _destination.transform.position);
        }
    }
}
