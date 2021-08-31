using UnityEngine;

namespace LP.FDG.Buildings
{

    public class BuildingEnabler : MonoBehaviour
    {
        [SerializeField]
        private GameObject PotentionalBuilding;

        public void SeePotentional()
        {
            Instantiate(PotentionalBuilding);
        }
    }
}
