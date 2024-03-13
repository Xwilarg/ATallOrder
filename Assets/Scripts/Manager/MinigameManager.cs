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

        private const float _barMult = 10f;

        private NpcInfo npcInfo;

        private int _score;

        private void Awake()
        {
            Instance = this;
        }

        public bool IsPlaying { set; get; }
        public bool IsNPCAttacking { get; private set; }

        int _atcksLeft;

        private Action _onLoose, _onWin;

        public void Play(NpcInfo info, Action onWin, Action onLoose)
        {
            IsPlaying = true;

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

            StartCoroutine(NPCAttack(atcks));
        }

        public void UpdateScore(int val)
        {
            _score += val;

            _atcksLeft--;

            var p = (_score + _barMult / 2f) / _barMult;
            _playerProg.localScale = new(Mathf.Clamp01(p), 1f, 1f);
            _bossProg.localScale = new(Mathf.Clamp01(1f - p), 1f, 1f);

            if (_atcksLeft == 0)
            {
                _fightContainer.SetActive(false);
                IsPlaying = false;

                if (_score >= 0)
                {
                    _onWin?.Invoke();
                }
                else
                {
                    _onLoose?.Invoke();
                }
            }
        }

        IEnumerator NPCAttack(PatternInfo[] patterns)
        {
            foreach (var p in patterns)
            {
                IsNPCAttacking = true;

                //get a pattern, cycle through it, instantiate attack points (with offset)

                foreach (PointSpawns point in p.attackPointArray)
                {
                    yield return new WaitForSeconds(p.attackDelay);
                    GameObject newAP = Instantiate(attackPointRef, attackHolder);

                    newAP.SetActive(true);
                    newAP.transform.position = new Vector2(point.x * Screen.width, point.y * Screen.height);
                }

                yield return new WaitForSeconds(1f); // TODO
            }
        }
    }
}
