using NSFWMiniJam3.SO;
using NSFWMiniJam3.World;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace NSFWMiniJam3.Manager
{
    public class RoomsManager : MonoBehaviour
    {
        public static RoomsManager Instance { private set; get; }

        [SerializeField]
        private NpcInfo[] _npcs;

        private Room[] _rooms;

        public bool IsEnemyRunningAway { set; get; }

        private void Awake()
        {
            Instance = this;

            _rooms = FindObjectsOfType<Room>();

            Assert.AreEqual(1, _rooms.Count(x => x.IsStartingRoom), "There must be one and only one starting room");
        }

        private void Start()
        {
            ShowRoom(_rooms.First(x => x.IsStartingRoom));

            foreach (var npc in _npcs)
            {
                var r = GetRandomAvailableRoom();
                if (r != null)
                {
                    r.RandomProp.SetHide(npc);
                }
            }
        }

        private List<Door> GetDoorTo(Room current, Room destination)
        {
            return null; // TODO
        }

        public Room GetRandomAvailableRoom()
        {
            var availableRooms = _rooms.Where(x => x.CanHideInRoom).ToArray();
            if (availableRooms.Any())
            {
                return availableRooms[Random.Range(0, availableRooms.Length)];
            }
            return null;
        }

        public void ShowRoom(Room r)
        {
            foreach (var room in _rooms)
            {
                room.gameObject.SetActive(false);
            }
            r.gameObject.SetActive(true);
        }
    }
}
