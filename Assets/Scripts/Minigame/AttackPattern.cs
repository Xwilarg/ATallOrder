using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSFWMiniJam3.Combat
{
    public class AttackPattern : MonoBehaviour
    {
        [SerializeField] GameObject attackPoint_ref;

        [SerializeField] float attackDelay;
        [SerializeField] SO.PointSpawns[] attackPointArray;

        // Start is called before the first frame update
        void OnEnable()
        {
            StartCoroutine("AttackSequence");
        }

        IEnumerator AttackSequence()
        {
            foreach (SO.PointSpawns ap in attackPointArray)
            {
                yield return new WaitForSeconds(attackDelay);
                GameObject newAP = Instantiate(attackPoint_ref, this.transform);

                newAP.SetActive(true);
                newAP.transform.position = new Vector2(ap.x * Screen.width, ap.y * Screen.height);
            }

            StartCoroutine("AttackSequence");

        }
    }
}
