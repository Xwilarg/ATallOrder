using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        private bool _startingRoom;

        public bool IsStartingRoom => _startingRoom;

        private void Awake()
        {
            foreach (var door in GetComponentsInChildren<IRoomOwned>())
            {
                door.ParentRoom = this;
            }
        }
    }
}
