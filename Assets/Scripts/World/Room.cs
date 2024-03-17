using NSFWMiniJam3.Manager;
using NSFWMiniJam3.SO;
using System.Linq;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        private bool _startingRoom;

        private Prop[] _props;
        public Door[] Doors { private set; get; }

        private Collider2D _coll;
        public Collider2D RoomColl => _coll;

        public bool CanHideInRoom => _props.Any() && _props.All(x => x.HiddenNpc == null);

        public Prop RandomProp => _props[Random.Range(0, _props.Length)];

        public bool IsStartingRoom => _startingRoom;

        private void Awake()
        {
            _coll = GetComponent<Collider2D>();

            foreach (var ro in GetComponentsInChildren<IRoomOwned>())
            {
                ro.ParentRoom = this;
            }

            Doors = GetComponentsInChildren<Door>();
            _props = GetComponentsInChildren<Prop>();
        }

        public bool RunAway(PlayerController pc, NpcInfo info, System.Func<PlayerController, Npc> spawn)
        {
            // Get another free room
            var freeRoom = RoomsManager.Instance.GetRandomAvailableRoom(this);
            if (freeRoom == null)
            {
                Debug.LogWarning($"No other room found, staying hidden...");
                return false;
            }

            // Get path to the room
            var possibleDoors = RoomsManager.Instance.GetDoorTo(this, freeRoom);

            if (!possibleDoors.Any())
            {
                Debug.LogWarning($"No path found to go from {name} to {freeRoom.name}, staying hidden...");
                return false;
            }


            Debug.Log($"{info.CharacterName} is going to hide in {freeRoom.name} (Possible doors: {string.Join(", ", possibleDoors.Select(x => x.name))})");

            // Hide NPC
            freeRoom.RandomProp.SetHide(info);

            // Make NPC run away
            RoomsManager.Instance.EnemyRunningAway++;
            var npc = spawn(pc);
            npc.RunAnim();
            npc.SetDestination(possibleDoors[Random.Range(0, possibleDoors.Length)]);

            return true;
        }

        /// <summary>
        /// Code executed when the player look inside an empty object
        /// </summary>
        public void FailHideCheck(PlayerController pc)
        {
            foreach (Prop p in _props)
            {
                if (p.HiddenNpc != null && RunAway(pc, p.HiddenNpc, p.SpawnNpc))
                {
                    // Remove NPC from prop
                    p.SetHide(null);
                }
            }
        }
    }
}
