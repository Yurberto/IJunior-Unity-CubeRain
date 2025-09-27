using UnityEngine;

public class RainSpawnSystem : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeReleased += ChangeCubeToBomb;
    }

    private void ChangeCubeToBomb(Cube cube)
    {
        _bombSpawner.SpawnOnPosition(cube.transform.position);
    }
}
