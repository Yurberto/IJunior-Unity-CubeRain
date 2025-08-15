using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _inputHandler.SpaceClicked += ChangeStateCorutine;
    }

    private void ChangeStateCorutine()
    {
        _spawner.ChangeCorutineState();
    }
}
