using UnityEngine;

namespace NSFWMiniJam3.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/NpcInfo", fileName = "NpcInfo")]
    public class NpcInfo : ScriptableObject
    {
        [SerializeField]
        private Sprite GameSprite;
    }
}