using UnityEngine;

public class Colorer : MonoBehaviour
{
    [SerializeField] Spawner _spawner;

    public void SetRandomColor(Cube cube)
    {
        if (cube.TryGetComponent(out MeshRenderer meshRenderer))
            meshRenderer.material.color = Random.ColorHSV();
    }
}
