using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LP.FDG.Grid
{
    [RequireComponent(typeof(MeshFilter))]
    public class GridGenerator : MonoBehaviour
    {
        #region Mesh Variables
        [SerializeField]
        private Mesh mesh;

        [SerializeField]
        private Vector3[] tileVertices;

        [SerializeField]
        private Vector2[] tileUV;

        [SerializeField]
        private int[] trianglesOnMap;
        #endregion

        #region Grid Variables
        [SerializeField]
        private float floorLevel;

        [SerializeField]
        private int xGridSize = 10;

        [SerializeField]
        private int zGridSize = 20;

        [SerializeField]
        private int gridScale = 20;



        #endregion

        void Start()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            CreateTriangleGrid();
            UpdateGrid();
        }

        /*
        //Create Hexagon Shape!
        void CreateHexagonShape()
        {
            float floorLevel = 1;
            tileVertices = new Vector3[]
            {
                new Vector3(-1f , floorLevel, -.5f),
                new Vector3(-1f, floorLevel, .5f),
                new Vector3(0f, floorLevel, 1f),
                new Vector3(1f, floorLevel, .5f),
                new Vector3(1f, floorLevel, -.5f),
                new Vector3(0f, floorLevel, -1f)
            };

            triangles = new int[]
            {
                1,5,0,
                1,4,5,
                1,2,4,
                2,3,4
            };

            tileUV = new Vector2[]
            {
                new Vector2(0,0.25f),
                new Vector2(0,0.75f),
                new Vector2(0.5f,1),
                new Vector2(1,0.75f),
                new Vector2(1,0.25f),
                new Vector2(0.5f,0),
            };
        }
        */

        // CreateTriangleShape()
        private void CreateTriangleGrid()
        {
            float floorLevel = 1;
            tileVertices = new Vector3[(xGridSize + 1) * (zGridSize + 1)];

            for (int i = 0, zPos = 0; zPos < zGridSize; zPos++)
            {
                for (int xPos = 0; xPos <= xGridSize; xPos++)
                {
                    tileVertices[i] = new Vector3(xPos, floorLevel, zPos);
                    i++;
                }
            }

            trianglesOnMap = new int[xGridSize * zGridSize * 6];
            int vertexOffset = 0;
            int triangleOffset = 0;

            for (int z = 0; z < zGridSize; z++)
            {
                for (int x = 0; x < xGridSize; x++)
                {

                    trianglesOnMap[triangleOffset + 0] = vertexOffset + 0;
                    trianglesOnMap[triangleOffset + 1] = vertexOffset + xGridSize + 1;
                    trianglesOnMap[triangleOffset + 2] = vertexOffset + 1;
                    trianglesOnMap[triangleOffset + 3] = vertexOffset + 1;
                    trianglesOnMap[triangleOffset + 4] = vertexOffset + xGridSize + 1;
                    trianglesOnMap[triangleOffset + 5] = vertexOffset + xGridSize + 2;

                    vertexOffset++;
                    triangleOffset += 6;
                }
                vertexOffset++;
            }
            

            

            

        }

        private void UpdateGrid()
        {
            mesh.Clear();

            mesh.vertices = tileVertices;
            mesh.triangles = trianglesOnMap;

            mesh.RecalculateNormals();

        }

        public Vector3 GetNearestPointOnGrid(Vector3 position)
        {
            position -= transform.position;

            int xCount = Mathf.RoundToInt(position.x / gridScale);
            int zCount = Mathf.RoundToInt(position.z / gridScale);

            Vector3 result = new Vector3(
                (float)xCount * gridScale,
                (float)zCount * gridScale);

            result += transform.position;

            return result;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (tileVertices == null)
            {
                return;
            }

            for(int i = 0; i < tileVertices.Length; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(tileVertices[i], 0.1f);
            }
            Debug.Log("END OF LOOP");
        }


        /*
         * 
         * 
         *             tileVertices = new Vector3[]
            {
                new Vector3(0, floorLevel, 0),
                new Vector3(0, floorLevel, 1),
                new Vector3(1, floorLevel, 0)
            };

            triangles = new int[]
            {
                0, 1, 2
            };
         * 
        void UpdateHexagonalMesh()
        {
            mesh.Clear();

            mesh.vertices = tileVertices;
            mesh.triangles = triangles;
            mesh.uv = tileUV;

            mesh.RecalculateNormals();

        }
        */
    }
}
