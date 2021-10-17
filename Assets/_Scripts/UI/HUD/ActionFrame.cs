using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LP.FDG.UI.HUD
{
    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame instance = null;

        [SerializeField] private Button actionButton = null;
        [SerializeField] private Transform layoutGroup = null;

        private List<Button> buttons = new List<Button>();
        private PlayerActions actionsList = null;

        public List<float> spawnQueue = new List<float>();
        public List<GameObject> spawnOrder = new List<GameObject>();

        public GameObject spawnPoint = null;

        private void Awake()
        {
            instance = this;
        }

        public void SetActionButtons(PlayerActions actions, GameObject spawnLocation)
        {
            actionsList = actions;
            spawnPoint = spawnLocation;

            if (actions.basicUnits.Count > 0)
            {
                foreach(Units.BasicUnit unit in actions.basicUnits)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = unit.name;
                    GameObject icon = Instantiate(unit.icon, btn.transform);
                    //add text etc?...
                    buttons.Add(btn);
                }
            }

            if (actions.basicBuildings.Count > 0)
            {
                foreach(Buildings.BasicBuilding building in actions.basicBuildings)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = building.name;
                    GameObject icon = Instantiate(building.icon, btn.transform);
                    //add text etc?...
                    buttons.Add(btn);
                }
            }
        }

        public void ClearActions()
        {
            foreach (Button btn in buttons)
            {
                Destroy(btn.gameObject);
            }
            buttons.Clear();
        }

        public void StartSpawnTimer(string objectToSpawn)
        {
            if (IsUnit(objectToSpawn))
            {
                Units.BasicUnit unit = IsUnit(objectToSpawn);
                spawnQueue.Add(unit.spawnTime);
                spawnOrder.Add(unit.humanPrefab);
            }
            else if (IsBuilding(objectToSpawn))
            {
                Buildings.BasicBuilding building = IsBuilding(objectToSpawn);
                spawnQueue.Add(building.spawnTime);
                spawnOrder.Add(building.buildingPrefab);
            }
            else
            {
                Debug.Log($"{objectToSpawn} is not a spawnable object");
            }

            if (spawnQueue.Count == 1)
            {
                ActionTimer.instance.StartCoroutine(ActionTimer.instance.SpawnQueueTimer());
            }
            else if (spawnQueue.Count == 0)
            {
                ActionTimer.instance.StopAllCoroutines();
            }
        }

        private Units.BasicUnit IsUnit(string name)
        {
            if (actionsList.basicUnits.Count > 0)
            {
                foreach(Units.BasicUnit unit in actionsList.basicUnits)
                {
                    if (unit.name == name)
                    {
                        return unit;
                    }
                }
            }
            return null;
        }

        private Buildings.BasicBuilding IsBuilding(string name)
        {
            if (actionsList.basicBuildings.Count > 0)
            {
                foreach(Buildings.BasicBuilding building in actionsList.basicBuildings)
                {
                    if (building.name == name)
                    {
                        return building;
                    }
                }
            }
            return null;
        }

        public void SpawnObject()
        {
            GameObject spawnedObject = Instantiate(spawnOrder[0], new Vector3(spawnPoint.transform.position.x - 4,
                spawnPoint.transform.position.y, spawnPoint.transform.position.z), Quaternion.identity);

            spawnedObject.GetComponent<Units.Player.PlayerUnit>().baseStats.health = 50;

        }
    }
}

