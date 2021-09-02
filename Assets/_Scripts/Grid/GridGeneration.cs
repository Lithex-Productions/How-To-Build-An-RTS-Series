using UnityEngine;

namespace LP.FDG.Grid
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class GridGeneration : MonoBehaviour
    {
        #region Mesh Variables
        
        [SerializeField]
        private Mesh mesh;

        [SerializeField]
        private Vector3[] tileVertices;

        [SerializeField]
        private int[] trianglesOnMesh;
        #endregion

        #region Grid Settings

        [SerializeField]
        private float individualCellSize = 1.0f;
        
        [SerializeField]
        private Vector3 individualCellOffset;

        [SerializeField]
        private int xGridSize = 1;
        
        [SerializeField]
        private int zGridSize = 1;

        [SerializeField]
        private Vector3 gridLocationOffset;

        [SerializeField]
        private float vertexOffsetFactor = 0.5f;

        [SerializeField]
        private float vertexOffset = 0.5f;

        [SerializeField]
        private float floorLevel = 1;



        #endregion

        public void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
        }

        public void Start()
        {
            CreateGrid();
            UpdateGrid();
        }

        private void CreateGrid()
        {

            tileVertices = new Vector3[(xGridSize * zGridSize) *4];
            trianglesOnMesh = new int[(xGridSize * zGridSize) * 6];

            vertexOffset = individualCellSize * vertexOffsetFactor;

            for (int z = 0, vert = 0, tri = 0; z < zGridSize; z++)
            {
                for(int x = 0; x < xGridSize; x++)
                {
                    individualCellOffset = new Vector3(x * individualCellSize, floorLevel, z * individualCellSize);
                    tileVertices[vert + 0] = new Vector3(-vertexOffset, floorLevel, -vertexOffset) + individualCellOffset + gridLocationOffset;
                    tileVertices[vert + 1] = new Vector3(-vertexOffset, floorLevel, vertexOffset) + individualCellOffset + gridLocationOffset;
                    tileVertices[vert + 2] = new Vector3(vertexOffset, floorLevel, -vertexOffset) + individualCellOffset + gridLocationOffset;
                    tileVertices[vert + 3] = new Vector3(vertexOffset, floorLevel, vertexOffset) + individualCellOffset + gridLocationOffset;

                    trianglesOnMesh[tri + 0] = vert + 0;
                    trianglesOnMesh[tri + 1] = vert + 1;
                    trianglesOnMesh[tri + 2] = vert + 2;

                    trianglesOnMesh[tri + 3] = vert + 2;
                    trianglesOnMesh[tri + 4] = vert + 1;
                    trianglesOnMesh[tri + 5] = vert + 3;

                    vert += 4;
                    tri+=6;
                }
            }
            

        }

        private void UpdateGrid()
        {
            mesh.Clear();
            Debug.Log("Mesh has been resetted!");

            mesh.vertices = tileVertices;
            mesh.triangles = trianglesOnMesh;
            mesh.RecalculateNormals();

        }


    }
}

