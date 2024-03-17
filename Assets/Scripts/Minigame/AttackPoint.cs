using NSFWMiniJam3.Manager;
using UnityEngine;

namespace NSFWMiniJam3.Combat
{
    public class AttackPoint : MonoBehaviour
    {
        [SerializeField] Transform innerCircle;
        public float AttackSpeed { set; private get; }

        private float _timer = 0f;

        private RectTransform _circleRt;

        private void Awake()
        {
            _circleRt = innerCircle.GetComponent<RectTransform>();
            _circleRt.localScale = Vector2.zero;
        }

        private void Update()
        {
            _timer += Time.deltaTime * AttackSpeed;

            if (_timer >= 1f)
            {
                MinigameManager.Instance.UpdateScore(-1);
                Destroy(gameObject);
            }
            else
            {
                _circleRt.localScale = Vector2.one * _timer;
            }
        }

        public void MouseClicked()
        {
            MinigameManager.Instance.UpdateScore(1f * _circleRt.localScale.x);
            Destroy(gameObject);
        }
    }
}