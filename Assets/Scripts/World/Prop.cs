using NSFWMiniJam3.SO;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Prop : MonoBehaviour, IInteractable
    {
        public NpcInfo HiddenNpc { private set; get; }

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

        public void Interact(PlayerController pc)
        {
            throw new System.NotImplementedException();
        }
    }
}
