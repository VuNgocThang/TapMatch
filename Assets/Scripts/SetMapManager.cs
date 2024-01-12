using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThangVN
{
    public class SetMapManager : MonoBehaviour
    {
        public GameObject squarePrefab;
        public List<int> zAxis = new List<int>();
        public float spacing;
        public int rows;
        public int cols;

        private void Start()
        {
            Spawn();
        }

        void Spawn()
        {
            for (int i = 0; i < zAxis.Count; i++)
            {
                SpawnEachZ(zAxis[i]);
            }
        }

        void SpawnEachZ(int z)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    float xPos = i * spacing;
                    float yPos = j * spacing;

                    GameObject square = Instantiate(squarePrefab, transform);
                    square.transform.localPosition = new Vector3(xPos, yPos, z);
                }
            }
        }

    }
}
