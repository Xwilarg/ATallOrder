using NSFWMiniJam3.SO;
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

        private NpcInfo npcInfo;

        private int _score;

        private void Awake()
        {
            Instance = this;
        }

        public bool IsPlaying { set; get; }
        public bool IsNPCAttacking { get; private set; }

        int _atcksLeft;


        public void Play(NpcInfo info)
        {
            IsPlaying = true; // Set that back to false once it's done!

            npcInfo = info;
            npcGameObject.GetComponent<Image>().sprite = info.GameSprite;
            npcGameObject.GetComponent<Animator>().runtimeAnimatorController = info.CharacterAnimator;

            _fightContainer.SetActive(true);

            _score = 0;

            int atckCount = 5;
            var atcks = Enumerable.Repeat(new object(), atckCount).Select(x => npcInfo.attackPatterns[Random.Range(0, npcInfo.attackPatterns.Length)]).ToArray();

            _atcksLeft = atcks.Sum(x => x.attackPointArray.Length);

            StartCoroutine(NPCAttack(atcks));
        }

        public void UpdateScore(int val)
        {
            _score += val;

            _atcksLeft--;
            if (_atcksLeft == 0)
            {
                IsPlaying = false;

                if (_score >= 0)
                {
                    // Win
                }
                else
                {
                    // Loose
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

                yield return new WaitForSeconds(npcInfo.StatBlock.AttackSpeed);
            }
        }
    }
}
