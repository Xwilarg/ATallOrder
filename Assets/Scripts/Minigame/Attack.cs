using UnityEngine;

namespace NSFWMiniJam3.Combat
{
    [System.Serializable]
    public class Attack
    {
        public AttackPoint[] AttackPoints;
        public int AttackPower;
        public float DelayBetweenPoints;
    }
}