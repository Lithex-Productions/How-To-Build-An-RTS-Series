using UnityEngine;

namespace LP.FDG.Buildings
{
    public class BuildingCreator : MonoBehaviour
    {
        [SerializeField]
<<<<<<< Updated upstream
        private GameObject Building;

        [SerializeField]
        private Ray BuildingRayCast;
        //bool BuildingHasNotBeenPlaced;
        //bool isMouseDown;

=======
        private Ray BuildingRayCast;

        [SerializeField]
        private RaycastHit location;

        [SerializeField]
        private int layersBuildingCanBePlacedOn = 1 << 10;

        [SerializeField]
        private float rayLength = 100.0f;

        [SerializeField]
        private float buildingOffset = 1.0f;

        [SerializeField]
        private GameObject building;

        [SerializeField]
        private GameObject barraks;

        [SerializeField]
        private string whereToFindParent = "/PlayerBuildings/Barraks";

        [SerializeField]
        private string buildingLayer = "Interactables";

        public void Awake()
        {
            barraks = GameObject.Find(whereToFindParent);
            layersBuildingCanBePlacedOn = ~layersBuildingCanBePlacedOn;
            Ray BuildingRayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Ray may be used");
        }
>>>>>>> Stashed changes

        public void Update()
        {
            Ray BuildingRayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
<<<<<<< Updated upstream
            RaycastHit Location;

            if (Physics.Raycast(BuildingRayCast, out Location, 1000))
            {
                Instantiate(Building, new Vector3(Location.point.x, Location.point.y + Building.transform.position.y, Location.point.z), Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
=======
            if(Physics.Raycast(BuildingRayCast, out location, rayLength, layersBuildingCanBePlacedOn))
            {
                Debug.DrawLine(Camera.main.transform.position, location.point, Color.red);
                transform.position = new Vector3(location.point.x, buildingOffset, location.point.z);
            }

            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("Button Pressed");

                Instantiate(building, new Vector3(location.point.x, buildingOffset, location.point.z), Quaternion.identity, barraks.transform);
                building.layer = LayerMask.NameToLayer(buildingLayer);
                Destroy(gameObject);
            }

        }
    }
}

>>>>>>> Stashed changes
