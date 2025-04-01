using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace DefaultNamespace
{
    public class OffsetsFinder : MonoBehaviour
    {
        [SerializeField] private MatrixVisualizer matrixVisualizer;
        
        private List<Matrix4x4> _modelMatrices = new();
        private List<Matrix4x4> _spaceMatrices = new();
        private List<Matrix4x4> _offsetsMatrices = new();

        private void Start()
        {
            LoadMatrices();
            FindOffsets();
            SaveOffsets();
        }

        private void LoadMatrices()
        {
            _modelMatrices = GetMatrices(Resources.Load<TextAsset>("model"));
            Debug.Log("Deserialized model : " + _modelMatrices.Count);

            _spaceMatrices = GetMatrices(Resources.Load<TextAsset>("space"));
            Debug.Log("Deserialized space : " + _spaceMatrices.Count);
        }

        private void FindOffsets()
        {
            var spaceSet = new HashSet<Matrix4x4>(_spaceMatrices);
            var inversedModelsDict = _modelMatrices.ToDictionary(m => m, mI => mI.inverse);

            foreach (var spaceMatrix in _spaceMatrices)
            {
                foreach (var modelMatrix in _modelMatrices)
                {
                    var offset = spaceMatrix * inversedModelsDict[modelMatrix];

                    if (_modelMatrices.All(testMatrix => spaceSet.Contains(offset * testMatrix)))
                    {
                        _offsetsMatrices.Add(offset);
                    }
                }
            }

            Debug.Log($"offsets found: {_offsetsMatrices.Count}");
        }
        
        private void SaveOffsets()
        {
            var offsetDataList = _offsetsMatrices.Select(MatrixData.FromMatrix4x4).ToList();
            var json = JsonConvert.SerializeObject(offsetDataList, Formatting.Indented);
            File.WriteAllText(Application.dataPath + "/offsets.json", json);

            matrixVisualizer.VisualizeMatrices(_modelMatrices, _spaceMatrices, _offsetsMatrices);
        }
        
        private List<Matrix4x4> GetMatrices(TextAsset text)
        {
            var modelData = JsonConvert.DeserializeObject<List<MatrixData>>(text.text);
            return modelData.Select(MatrixData.ToMatrix4x4).ToList();
        }
    }
}