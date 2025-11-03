using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemySystem
{
    public class TakeDamageEffect : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private List<Renderer> _targetRenderers = new();
        [SerializeField] private string _parameterName = "_Alpha";
        [SerializeField] private float _parameterValue = 0f;
        [SerializeField] private float _redDuration;
        private Coroutine _coroutine;
        private List<ShaderChanger> _shaderChangers = new();

        public class ShaderChanger
        {
            private MaterialPropertyBlock PropertyBlock { get; }
            private int ParameterId { get; }
            private Renderer Renderer { get; }

            public ShaderChanger(string parameterName, Renderer renderer)
            {
                PropertyBlock = new MaterialPropertyBlock();
                ParameterId = Shader.PropertyToID(parameterName);
                Renderer = renderer;
            }

            public void SetShaderParameter(float value)
            {
                if (Renderer == null)
                    return;
                Renderer.GetPropertyBlock(PropertyBlock);
                PropertyBlock.SetFloat(ParameterId, value);
                Renderer.SetPropertyBlock(PropertyBlock);
            }
        }

        private void OnEnable()
        {
            _health._current.Changed += DoEffect;
        }

        private void OnDisable()
        {
            _health._current.Changed -= DoEffect;
        }

        private void Start()
        {
            for (var i = 0; i < _targetRenderers.Count; i++)
            {
                _shaderChangers.Add(new ShaderChanger(_parameterName, _targetRenderers[i]));
                _shaderChangers.Last().SetShaderParameter(_parameterValue);
            }
        }

        private void DoEffect(int old, int newValue)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(DamageEffect());
        }

        private IEnumerator DamageEffect()
        {
            foreach (var item in _shaderChangers)
                item.SetShaderParameter(1f);
            yield return new WaitForSeconds(_redDuration);
            foreach (var item in _shaderChangers)
                item.SetShaderParameter(0f);
        }

    }
}