using UnityEngine;

namespace NSFWMiniJam3.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/PatternInfo", fileName = "PatternInfo)")]
    public class PatternInfo : ScriptableObject
    {
        public float attackDelay;
        public PointSpawns[] attackPointArray;
    }

    [System.Serializable]
    public struct PointSpawns
    {
        public float x;
        public float y;
    }
}