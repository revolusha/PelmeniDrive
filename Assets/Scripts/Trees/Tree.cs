using UnityEngine;

public class Tree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TreeCatcher>() != null)
        {
            gameObject.SetActive(false);
        }
    }
}
