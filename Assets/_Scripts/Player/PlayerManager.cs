using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LP.FDG.InputManager;

namespace LP.FDG.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;

        public Transform playerUnits;
        public Transform enemyUnits;

        public Transform playerBuildings;

        private void Awake()
        {
            instance = this;
            SetBasicStats(playerUnits);
            SetBasicStats(enemyUnits);
            SetBasicStats(playerBuildings);
        }

        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }

        public void SetBasicStats(Transform type)
        {
            foreach (Transform child in type)
            {
                foreach (Transform tf in child)
                {
                    string name = child.name.Substring(0, child.name.Length - 1).ToLower();
                    //var stats = Units.UnitHandler.instance.GetBasicUnitStats(unitName);

                    if (type == playerUnits)
                    {
                        Units.Player.PlayerUnit pU = tf.GetComponent<Units.Player.PlayerUnit>();
                        pU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }
                    else if (type == enemyUnits)
                    {
                        // set enemy stats
                        Units.Enemy.EnemyUnit eU = tf.GetComponent<Units.Enemy.EnemyUnit>();
                        eU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }
                    else if(type == playerBuildings)
                    {
                        Buildings.Player.PlayerBuilding pB = tf.GetComponent<Buildings.Player.PlayerBuilding>();
                        pB.baseStats = Buildings.BuildingHandler.instance.GetBasicBuildingStats(name);
                    }

                    // if we have any upgrades add them now
                    // add upgrages to unit stats
                }
            }
        }
    }
}

