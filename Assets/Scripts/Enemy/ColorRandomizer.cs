using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    [SerializeField] private Material[] _material;

    private bool _isReady = false;
    private MeshRenderer[] _targetRenderers;

    private void OnEnable()
    {
        if (_isReady == false)
            GetReferences();

        PickColor();
    }

    private void GetReferences()
    {
        ColorTarget[] _targets = GetComponentsInChildren<ColorTarget>();

        _targetRenderers = new MeshRenderer[_targets.Length];

        for (int i = 0; i < _targets.Length; i++)
        {
            _targetRenderers[i] = _targets[i].GetComponent<MeshRenderer>();
        }

        _isReady = true;
    }

    private void PickColor()
    {
        Material material = _material[Random.Range(0, _material.Length)];

        foreach (var target in _targetRenderers)
            target.material = material;
    }
}
