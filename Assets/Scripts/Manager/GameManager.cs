﻿using UnityEngine;

namespace NSFWMiniJam3.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }

        public bool CanMove => !RoomsManager.Instance.IsEnemyRunningAway && !MinigameManager.Instance.IsPlaying;
    }
}
