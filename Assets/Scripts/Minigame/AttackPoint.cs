using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] Transform innerCircle;
    [SerializeField] float attackSpeed = 1f;

    private void Start()
    {
        innerCircle.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
    }

    private void Update()
    {
        innerCircle.GetComponent<RectTransform>().sizeDelta += new Vector2(10, 10) * Time.deltaTime * attackSpeed;
    }

    public void MouseClicked()
    {
        Destroy(gameObject);
    }
}
