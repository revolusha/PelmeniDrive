using UnityEngine;

public class TreeDeath : MonoBehaviour
{
    [SerializeField] private Animator _tree;

    private Player _player;

    private void OnEnable()
    {
        _player = Game.Player;
        _tree = GetComponentInChildren<Animator>();
        _tree.gameObject.SetActive(false);
        _player.OnGettingDeadByTree += MakeTreePunch;
    }

    private void OnDisable()
    {
        _player.OnGettingDeadByTree -= MakeTreePunch;
    }

    private void MakeTreePunch()
    {
        _tree.gameObject.SetActive(true);
    }
}
