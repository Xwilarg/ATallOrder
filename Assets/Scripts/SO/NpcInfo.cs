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
        public RuntimeAnimatorController FightAnimator;
        public RuntimeAnimatorController WorldAnimator;

        public PatternInfo[] attackPatterns;
        
        public Stats StatBlock;

        public TextAsset Intro, OnPlayerWin, OnPlayerLoose;

        public WorldPosOverrides PosOverrides;

    }

    [System.Serializable]
    public struct Stats
    {
        public float AttackSpeed;
        public float ClothFightbackInitialValue;
        public int ClothFightbackOffset;
    }

    [System.Serializable]
    public struct WorldPosOverrides
    {
        public bool IsMovedDownWhenFound;
    }
}