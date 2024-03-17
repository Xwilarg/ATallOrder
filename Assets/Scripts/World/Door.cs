using Cinemachine;
using NSFWMiniJam3.Manager;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Node _destination;

        [SerializeField]
        private bool _requireKey;

        public Room Destination => _destination.ParentRoom;

        public string InteractionKey => "enter";

        public void Interact(PlayerController pc)
        {
            if (_requireKey && !pc.HasKey)
            {
                DialogueManager.Instance.ShowStory(pc.transform.position, AssetManager.Instance.DoorLocked);
            }
            else
            {
                TransitionManager.Instance.StartTransition((CinemachineVirtualCamera vCam) =>
                {
                    RoomsManager.Instance.ShowRoom(_destination.ParentRoom);

                    var localPos = pc.transform.localPosition;
                    pc.transform.parent = _destination.ParentRoom.transform;
                    pc.transform.position = _destination.transform.position;
                    pc.transform.localPosition = new(pc.transform.localPosition.x, localPos.y, 0f);

                    var composer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
                    vCam.ForceCameraPosition(new(pc.transform.position.x, pc.transform.position.y + composer.m_TrackedObjectOffset.y, vCam.transform.position.z), vCam.transform.rotation);
                    pc.SetCamCollider(Destination.RoomColl);
                });
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _destination.transform.position);
        }
    }
}
