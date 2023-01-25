using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private GameObject _roadTemplate;
    [SerializeField] private int _count = 5;
    [SerializeField] private int _segmentLength = 40;

    private readonly List<GameObject> _segments = new List<GameObject>();

    private void OnEnable()
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject newSegment = Instantiate(_roadTemplate, transform);

            newSegment.transform.localPosition = new Vector3(0, 0, i * _segmentLength);
            _segments.Add(newSegment);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject segment in _segments)
            Destroy(segment);
    }
}
