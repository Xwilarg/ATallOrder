﻿using NSFWMiniJam3.Manager;
using NSFWMiniJam3.SO;
using UnityEngine;

namespace NSFWMiniJam3.World
{
    public class Npc : MonoBehaviour//, IInteractable
    {
        [SerializeField]
        private NpcInfo _info;

        private Animator _anim;

        public bool IsDowned { private set; get; }

        // Target data
        private Vector2 _iniPos;
        private float _movTimer;
        private Door _target;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _anim.runtimeAnimatorController = _info.WorldAnimator;
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

        public void DownAnim()
        {
            _anim.SetTrigger("GetDown");
        }

        public void NakedAnim()
        {
            _anim.SetTrigger("GetNaked");
        }

        public void RunAnim()
        {
            _anim.SetTrigger("Run");
        }

        public void SetDestination(Door d)
        {
            _target = d;
            GetComponent<SpriteRenderer>().flipX = d.transform.position.x < transform.position.x;
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
