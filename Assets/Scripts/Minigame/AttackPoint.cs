using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] Transform innerCircle;
    [SerializeField] float attackSpeed = 1;

    private void Start()
    {
        innerCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }

    private void Update()
    {
        innerCircle.GetComponent<RectTransform>().sizeDelta += new Vector2(10, 10) * Time.deltaTime * attackSpeed;
    }

    public void MouseClicked()
    {
        innerCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }
}
