using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LP.FDG.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit/Basic")]
    public class BasicUnit : ScriptableObject
    {
        public enum unitType
        {
            Worker,
            Warrior,
            Healer
        }

        [Space(15)]
        [Header("Unit Settings")]

        public unitType type;
        public new string name;
        public GameObject humanPrefab;
        public GameObject infectedPrefab;
        public GameObject icon;
        public float spawnTime;

        [Space(15)]
        [Header("Unit Base Stats")]
        [Space(40)]

        public UnitStatTypes.Base baseStats;
    }
}

