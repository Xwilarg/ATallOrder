using NSFWMiniJam3.SO;
using UnityEngine;

namespace NSFWMiniJam3.Manager
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }


        public void Play(NpcInfo info)
        {
            Debug.LogWarning("TODO: Make actual code");
        }
    }
}
