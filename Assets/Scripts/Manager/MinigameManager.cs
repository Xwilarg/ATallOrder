using NSFWMiniJam3.Combat;
using NSFWMiniJam3.SO;
using System.Collections;
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

        private void Awake()
        {
            Instance = this;
        }

        public bool IsPlaying { set; get; }
        public bool IsNPCAttacking { get; private set; }


        public void Play(NpcInfo info)
        {
            IsPlaying = true; // Set that back to false once it's done!

            npcInfo = info;
            npcGameObject.GetComponent<Image>().sprite = info.GameSprite;
            npcGameObject.GetComponent<Animator>().runtimeAnimatorController = info.CharacterAnimator;

            _fightContainer.SetActive(true);

            StartCoroutine("NPCAttack");
        }

        IEnumerator NPCAttack()
        {
            yield return new WaitForSeconds(npcInfo.StatBlock.AttackSpeed);

            IsNPCAttacking = true;

            //get a pattern, cycle through it, instantiate attack points (with offset)
            PatternInfo patternInfo = npcInfo.attackPatterns[Random.Range(0, npcInfo.attackPatterns.Length)];

            foreach(PointSpawns point in patternInfo.attackPointArray)
            {
                yield return new WaitForSeconds(patternInfo.attackDelay);
                GameObject newAP = Instantiate(attackPointRef, attackHolder);

                newAP.SetActive(true);
                newAP.transform.position = new Vector2(point.x * Screen.width, point.y * Screen.height);
            }

            StartCoroutine("NPCAttack");
        }
    }
}
