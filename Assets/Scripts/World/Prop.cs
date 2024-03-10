﻿using NSFWMiniJam3.Manager;
using NSFWMiniJam3.SO;
using UnityEngine;
using UnityEngine.Assertions;

namespace NSFWMiniJam3.World
{
    public class Prop : MonoBehaviour, IInteractable, IRoomOwned
    {
        public NpcInfo HiddenNpc { private set; get; }
        public Room ParentRoom { set; private get; }

        [SerializeField]
        private Sprite _free, _busy;

        private SpriteRenderer _sr;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        public void SetHide(NpcInfo info)
        {
            HiddenNpc = info;
            _sr.sprite = info == null ? _free : _busy;
        }

        public void SpawnNpc()
        {
            Assert.IsNotNull(HiddenNpc);

            var go = Instantiate(AssetManager.Instance.NpcPrefab, transform.position, Quaternion.identity);
            go.GetComponent<SpriteRenderer>().sprite = HiddenNpc.GameSprite;
        }

        public void Interact(PlayerController pc)
        {
            if (HiddenNpc != null)
            {
                SpawnNpc();

                MinigameManager.Instance.Play(HiddenNpc);
            }
            else
            {
                ParentRoom.FailHideCheck();
            }
        }
    }
}