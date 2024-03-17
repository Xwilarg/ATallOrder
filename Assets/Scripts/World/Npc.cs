using NSFWMiniJam3.Manager;
using NSFWMiniJam3.SO;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Npc : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private NpcInfo _info;
        public NpcInfo Info => _info;

        private Animator _anim;

        public bool IsDowned { set; get; }

        // Target data
        private Vector2 _iniPos;
        private float _movTimer;
        private Door _target;

        private bool _naked;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _iniPos = transform.position;

            if (_info != null)
            {
                _anim.runtimeAnimatorController = _info.WorldAnimator;
            }
        }

        public void SetInfo(NpcInfo info)
        {
            _info = info;
            _anim.runtimeAnimatorController = _info.WorldAnimator;
        }

        private void Update()
        {
            _anim.SetBool("GetNaked", _naked); // Unity doing weird shit

            if (_target != null)
            {
                _movTimer += Time.deltaTime * .5f;

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

        public void DownAnim()
        {
            _anim.SetTrigger("GetDown");
        }

        public void NakedAnim()
        {
            _naked = true;
        }

        public void RunAnim()
        {
            _anim.SetTrigger("Run");
        }

        public void SetDestination(Door d)
        {
            _target = d;
            if (_info.PosOverrides.IsSpriteInverted)
            {
                GetComponent<SpriteRenderer>().flipX = d.transform.position.x > transform.position.x;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = d.transform.position.x < transform.position.x;
            }
        }

        public string InteractionKey => "speak";

        public bool CanInteract => IsDowned;

        public void Interact(PlayerController pc)
        {
            DialogueManager.Instance.ShowStory(transform.position, _info.Garden);
        }
    }
}
