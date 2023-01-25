using UnityEngine;

public class PlateRotating : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 30;
    [SerializeField] private Vector3 _rotationValue = new Vector3(0, 1, 0);

    private void Update()
    {
        transform.Rotate(_rotationValue, _rotationSpeed * Time.deltaTime);
    }
}
