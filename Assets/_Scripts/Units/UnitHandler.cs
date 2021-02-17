using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LP.FDG.Player;

namespace LP.FDG.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;

        [SerializeField]
        private BasicUnit worker, warrior, healer;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        public UnitStatTypes.Base GetBasicUnitStats(string type)
        {
            BasicUnit unit;
            switch (type)
            {
                case "worker":
                    unit = worker;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                case "healer":
                    unit = healer;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }
            return unit.baseStats;
        }
    }
}

