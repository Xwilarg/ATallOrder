using NSFWMiniJam3.Manager;
using UnityEngine;

namespace NSFWMiniJam3.Combat
{
    public class AttackPoint : MonoBehaviour
    {
        [SerializeField] Transform innerCircle;
        [SerializeField] float attackSpeed = 1f;

        private float _timer = 0f;

        private void Start()
        {
            innerCircle.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        }

        private void Update()
        {
            _timer += Time.deltaTime * attackSpeed;

            if (_timer >= 1f)
            {
                MinigameManager.Instance.UpdateScore(-1);
                Destroy(gameObject);
            }
            else
            {
                innerCircle.GetComponent<RectTransform>().sizeDelta = Vector2.one * _timer;
            }
        }

        public void MouseClicked()
        {
            MinigameManager.Instance.UpdateScore(_timer >= .75f ? 1 : -1);
            Destroy(gameObject);
        }
    }
}