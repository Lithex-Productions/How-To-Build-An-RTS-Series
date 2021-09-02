using UnityEngine;

namespace LP.FDG.Buildings
{
    public class BuildingCreator : MonoBehaviour
    {
        [SerializeField]
        private Ray BuildingRayCast;

        [SerializeField]
        private RaycastHit location;

        [SerializeField]
        private int layersBuildingCanBePlacedOn = 1 << 10;

        [SerializeField]
        private float rayLength = 100.0f;

        [SerializeField]
        private float buildingOffSet = 1.0f;

        [SerializeField]
        private GameObject barraks;

        [SerializeField]
        private GameObject building;

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

        public void Update()
        {
            Ray BuildingRayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(BuildingRayCast, out location, rayLength, layersBuildingCanBePlacedOn))
            {
                Debug.DrawLine(Camera.main.transform.position, location.point, Color.red);
                transform.position = new Vector3(location.point.x, buildingOffSet, location.point.z);
                //Debug.Log(location.point);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Button Pressed");

                Instantiate(building, new Vector3(location.point.x, buildingOffSet, location.point.z), Quaternion.identity, barraks.transform);
                building.layer = LayerMask.NameToLayer(buildingLayer);
                Destroy(gameObject);
            }
        }
    }
}
