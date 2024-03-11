using UnityEngine;

namespace NSFWMiniJam3.SO
{
    /// <summary>
    /// NPC INFO - All pertinant information for a single character in combat
    /// Animators, sprites, 
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/NpcInfo", fileName = "NpcInfo")]
    public class NpcInfo : ScriptableObject
    {

        public string CharacterName;

        public Sprite GameSprite;
        public RuntimeAnimatorController CharacterAnimator;

        public PatternInfo[] attackPatterns;
        
        public Stats StatBlock;

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