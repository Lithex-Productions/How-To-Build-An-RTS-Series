using UnityEngine;

namespace LP.FDG.Buildings
{
    public class BuildingCreator : MonoBehaviour
    {
        [SerializeField]
        private GameObject Building;

        [SerializeField]
        private Ray BuildingRayCast;
        //bool BuildingHasNotBeenPlaced;
        //bool isMouseDown;


        public void Update()
        {
            Ray BuildingRayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Location;

            if (Physics.Raycast(BuildingRayCast, out Location, 1000))
            {
                Instantiate(Building, new Vector3(Location.point.x, Location.point.y + Building.transform.position.y, Location.point.z), Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
