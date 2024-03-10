using NSFWMiniJam3.Manager;
using NSFWMiniJam3.SO;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Npc : MonoBehaviour//, IInteractable
    {
        [SerializeField]
        private NpcInfo _info;

        public bool IsDowned { private set; get; }

        // Target data
        private Vector2 _iniPos;
        private float _movTimer;
        private Door _target;

        private void Awake()
        {
            _iniPos = transform.position;
        }

        private void Update()
        {
            if (_target != null)
            {
                _movTimer += Time.deltaTime;

                if (_movTimer >= 1f)
                {
                    RoomsManager.Instance.EnemyRunningAway--;
                    Destroy(gameObject);
                }
                else
                {
                    Vector2 dest = new(_target.transform.position.x, transform.position.y); // Be sure our Y value doesn't change

                    transform.position = Vector2.Lerp(_iniPos, dest, _movTimer);
                }
            }
        }

        public void SetDestination(Door d)
        {
            _target = d;
        }

        /*public void Interact(PlayerController pc)
        {
            if (IsDowned)
            {

            }
            else
            {
            }
        }*/
    }
}
