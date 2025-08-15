using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))] 
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_rigidbody == null)
        {
            string errorMessage = $"Объект {name} не имеет \"Rigidbody\"";
            Debug.LogError(errorMessage);
        }
    }    
}
