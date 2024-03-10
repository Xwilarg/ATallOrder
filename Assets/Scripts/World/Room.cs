using NSFWMiniJam3.Manager;
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

        public bool CanHideInRoom => _props.Any() && _props.All(x => x.HiddenNpc == null);

        public Prop RandomProp => _props[Random.Range(0, _props.Length)];

        public bool IsStartingRoom => _startingRoom;

        private void Awake()
        {
            foreach (var ro in GetComponentsInChildren<IRoomOwned>())
            {
                ro.ParentRoom = this;
            }

            Doors = GetComponentsInChildren<Door>();
            _props = GetComponentsInChildren<Prop>();
        }

        /// <summary>
        /// Code executed when the player look inside an empty object
        /// </summary>
        public void FailHideCheck(PlayerController pc)
        {
            foreach (Prop p in _props)
            {
                if (p.HiddenNpc != null)
                {
                    var info = p.HiddenNpc;

                    // Get another free room
                    var freeRoom = RoomsManager.Instance.GetRandomAvailableRoom(this);
                    if (freeRoom == null)
                    {
                        Debug.LogWarning($"No other room found, staying hidden...");
                        continue;
                    }

                    // Get path to the room
                    var possibleDoors = RoomsManager.Instance.GetDoorTo(this, freeRoom);

                    if (!possibleDoors.Any())
                    {
                        Debug.LogWarning($"No path found to go from {name} to {freeRoom.name}, staying hidden...");
                        continue;
                    }

                    // Hide NPC
                    freeRoom.RandomProp.SetHide(info);

                    // Make NPC run away
                    RoomsManager.Instance.EnemyRunningAway++;
                    var npc = p.SpawnNpc(pc);
                    npc.SetDestination(possibleDoors[Random.Range(0, possibleDoors.Length)]);

                    // Remove NPC from prop
                    p.SetHide(null);
                }
            }
        }
    }
}
