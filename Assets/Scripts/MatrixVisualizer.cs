using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class MatrixVisualizer
    {
        [SerializeField] private GameObject modelPrefab;
        [SerializeField] private Transform modelParent;
        [SerializeField] private GameObject spacePrefab;
        [SerializeField] private Transform spaceParent;
        [SerializeField] private GameObject offsetPrefab;
        [SerializeField] private Transform offsetParent;

        public void VisualizeMatrices(List<Matrix4x4> modelsMatrices, List<Matrix4x4> spaceMatrices, List<Matrix4x4> offsetsMatrices)
        {
            InitObjects(modelsMatrices, modelPrefab, modelParent);
            InitObjects(spaceMatrices, spacePrefab, spaceParent);
            InitObjects(offsetsMatrices, offsetPrefab, offsetParent);
        }

        private void InitObjects(List<Matrix4x4> matrices, GameObject prefab, Transform parent)
        {
            foreach (var matrix in matrices)
            {
                var position = matrix.GetColumn(3);
                var rotation = Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
                var scale = new Vector3(
                    matrix.GetColumn(0).magnitude, 
                    matrix.GetColumn(1).magnitude, 
                    matrix.GetColumn(2).magnitude
                );

                var obj = MonoBehaviour.Instantiate(prefab, position, rotation, parent);
                obj.transform.localScale = scale;
            }
        }
    }
}