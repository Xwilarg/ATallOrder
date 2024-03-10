using UnityEngine;

namespace NSFWMiniJam3.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/NpcInfo", fileName = "NpcInfo")]
    public class NpcInfo : ScriptableObject
    {

        public string characterName;
        public Sprite GameSprite;

        public Attack[] attackPatterns;
        public Stats stats;

    }

    [System.Serializable]
    public struct Stats
    {
        public int Health;
        public float AttackSpeed;
        public float Defense;
        public float Power;
    }
}