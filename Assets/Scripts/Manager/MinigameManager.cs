﻿using NSFWMiniJam3.Combat;
using NSFWMiniJam3.SO;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NSFWMiniJam3.Manager
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager Instance { private set; get; }

        [SerializeField]
        private GameObject _fightContainer;

        [SerializeField] private Image backgroundSprite;

        [SerializeField] private GameObject npcGameObject;

        [SerializeField] private Transform attackHolder;
        [SerializeField] private GameObject attackPointRef;

        [SerializeField]
        private RectTransform _playerProg, _bossProg;

        [SerializeField]
        private GameObject _spamInstruction;

        [SerializeField]
        private Animator _anim;

        [SerializeField]
        private GameObject _endButton;

        private const float _barMult = 20f;

        private NpcInfo npcInfo;

        private float _score;

        private void Awake()
        {
            Instance = this;
        }

        public bool IsPlaying { set; get; }
        public bool IsNPCAttacking { get; private set; }

        int _atcksLeft;

        private Action _onLoose, _onWin;

        public bool IsStealingClothes { private set; get; }
        private float _struggleTimer = 0f;
        private int _struggleCount = 0;

        private bool _gotDamageYet;

        public void Play(NpcInfo info, Action onWin, Action onLoose)
        {
            IsPlaying = true;

            _anim.SetTrigger("Reset");

            AudioManager.Instance.PlayMusic(AudioManager.SongName.Combat);

            IsStealingClothes = false;
            _spamInstruction.SetActive(false);
            _endButton.SetActive(false);

            _onLoose = onLoose;
            _onWin = onWin;

            npcInfo = info;
            npcGameObject.GetComponent<Image>().sprite = info.GameSprite;
            npcGameObject.GetComponent<Animator>().runtimeAnimatorController = info.FightAnimator;

            _fightContainer.SetActive(true);

            _score = 0;

            int atckCount = 3;
            var atcks = Enumerable.Repeat(new object(), atckCount).Select(x => npcInfo.attackPatterns[UnityEngine.Random.Range(0, npcInfo.attackPatterns.Length)]).ToArray();

            _atcksLeft = atcks.Sum(x => x.attackPointArray.Length);

            UpdateScoreUI();

            StartCoroutine(NPCAttack(atcks));
        }

        private void Update()
        {
            if (IsPlaying && IsStealingClothes)
            {
                _struggleTimer -= Time.deltaTime;

                if (_struggleTimer <= 0f)
                {
                    _score--;

                    UpdateScoreUI();

                    if (_score <= -_barMult / 2f)
                    {
                        EndGame();
                        _onLoose?.Invoke();
                    }

                    _struggleCount++;
                    _struggleTimer = npcInfo.StatBlock.ClothFightbackInitialValue - (0.05f * _struggleCount);
                    if (_struggleTimer < .1f) _struggleTimer = .1f;
                }
            }
        }

        public void StruggleForCloth()
        {
            _score++;

            UpdateScoreUI();

            if (_score >= _barMult / 2f)
            {
                IsStealingClothes = false;
                _spamInstruction.SetActive(false);
                _anim.SetTrigger("Defeat");

                _spamInstruction.SetActive(false);
                _endButton.SetActive(true);
            }
        }

        public void Win()
        {
            EndGame();
            _onWin?.Invoke();
        }

        private void EndGame()
        {
            _fightContainer.SetActive(false);
            IsPlaying = false;
            AudioManager.Instance.PlayMusic(AudioManager.SongName.Exploration);
        }

        private void UpdateScoreUI()
        {
            var p = (_score + _barMult / 2f) / _barMult;
            _playerProg.localScale = new(Mathf.Clamp01(p), 1f, 1f);
            _bossProg.localScale = new(Mathf.Clamp01(1f - p), 1f, 1f);
        }

        public void UpdateScore(float val)
        {
            if (!_gotDamageYet && val < 0)
            {
                _gotDamageYet = true;
                _anim.SetTrigger("Attack");
            }

            _score += val;

            _atcksLeft--;
            UpdateScoreUI();

            if (_atcksLeft == 0)
            {
                if (_score >= 0)
                {
                    _spamInstruction.SetActive(true);
                    _score -= npcInfo.StatBlock.ClothFightbackOffset;
                    UpdateScoreUI();

                    _struggleCount = 0;
                    _struggleTimer = npcInfo.StatBlock.ClothFightbackInitialValue;

                    StartCoroutine(WaitAndDo(1f, () =>
                    {
                        _anim.SetTrigger("Struggle");
                        IsStealingClothes = true;
                    }));
                }
                else
                {
                    EndGame();
                    _onLoose?.Invoke();
                }
            }
        }

        IEnumerator WaitAndDo(float timer, Action callback)
        {
            yield return new WaitForSeconds(timer);
            callback();
        }

        IEnumerator NPCAttack(PatternInfo[] patterns)
        {
            foreach (var p in patterns)
            {
                IsNPCAttacking = true;

                //get a pattern, cycle through it, instantiate attack points (with offset)

                _gotDamageYet = false;

                foreach (PointSpawns point in p.attackPointArray)
                {
                    yield return new WaitForSeconds(p.attackDelay);
                    GameObject newAP = Instantiate(attackPointRef, attackHolder);

                    newAP.SetActive(true);
                    newAP.GetComponent<AttackPoint>().AttackSpeed = npcInfo.StatBlock.AttackSpeed;
                    newAP.transform.position = new Vector2(point.x * Screen.width, point.y * Screen.height);
                }

                yield return new WaitForSeconds(1f); // TODO
            }
        }
    }
}
