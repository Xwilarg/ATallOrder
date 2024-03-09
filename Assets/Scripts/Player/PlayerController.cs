using NSFWMiniJam3.SO;
using NSFWMiniJam3.World;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NSFWMiniJam3
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        [SerializeField]
        private GameObject _interactionHint;

        private Rigidbody2D _rb;

        private Vector2 _mov;

        private InteractionTarget _interactionTarget;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = _mov * _info.Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IInteractable>(out var comp))
            {
                _interactionHint.SetActive(true);
                _interactionTarget = new()
                {
                    Interaction = comp,
                    ID = collision.gameObject.GetInstanceID()
                };
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_interactionTarget != null && _interactionTarget.ID == collision.gameObject.GetInstanceID())
            {
                _interactionHint.SetActive(false);
                _interactionTarget = null;
            }
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>();
        }

        public void OnUse(InputAction.CallbackContext value)
        {
            if (value.performed && _interactionTarget != null)
            {

            }
        }

        private record InteractionTarget
        {
            public IInteractable Interaction;
            public int ID;
        }
    }
}