using UnityEngine;

public class EnemyTruck : MonoBehaviour
{
    private float _speed;

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarCatcher>() != null)
            gameObject.SetActive(false);
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * _speed);
    }
}
