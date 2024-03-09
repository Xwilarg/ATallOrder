using NSFWMiniJam3.World;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Node : MonoBehaviour, IRoomOwned
    {
        public Room ParentRoom { set; get; }
    }
}
