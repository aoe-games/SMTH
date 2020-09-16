using UnityEngine;
using UnityEngine.Serialization;

namespace Rendering
{
    public class HeatMaterialPropertyBlock : MonoBehaviour
    {
        [Range(0.0f, 0.95f)]
        public float heat = 0.0f;
        private Renderer _renderer;
        private MaterialPropertyBlock _propBlock;
        private static readonly int Heat = Shader.PropertyToID("_Heat");

        private void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat(Heat, heat);
            _renderer.SetPropertyBlock(_propBlock);
        }
    }
}