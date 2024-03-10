using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour
{
    [SerializeField] float attackDelay;
    [SerializeField] GameObject[] attackPointArray;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AttackSequence");
    }

    IEnumerator AttackSequence()
    {
        foreach(GameObject ap in attackPointArray)
        {
            yield return new WaitForSeconds(attackDelay);
            GameObject newAP = Instantiate(ap, this.transform);

            newAP.SetActive(true);
        }

        StartCoroutine("AttackSequence");

    }
}
