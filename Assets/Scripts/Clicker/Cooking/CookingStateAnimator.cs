using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CookingStateAnimator : MonoBehaviour
{
    [SerializeField] private int _minimalStateToActivate;

    const string IntName = "State";

    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Play(int state, float timeToDeactivateGameObject = 0)
    {
        if (state < _minimalStateToActivate)
            return;

        gameObject.SetActive(true);
        _animator.SetInteger(IntName, state);

        if (timeToDeactivateGameObject != 0)
            StartCoroutine(DeactivateAfterTime(timeToDeactivateGameObject));
    }

    private IEnumerator DeactivateAfterTime(float time)
    {
        if (time == 0)
            yield return null;

        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
