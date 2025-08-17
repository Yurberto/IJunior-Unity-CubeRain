using UnityEngine;

public class Colorer : MonoBehaviour
{
    public void SetRandomColor(Cube cube)
    {
        if (cube == null || cube.ColorChanged)
            return;

        if (cube.gameObject.TryGetComponent(out MeshRenderer meshRenderer) && meshRenderer.enabled)
            meshRenderer.material.color = Random.ColorHSV();
    }
}
