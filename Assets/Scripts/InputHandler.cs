using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    public event UnityAction SpaceClicked;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpaceClicked?.Invoke();
    }
}
