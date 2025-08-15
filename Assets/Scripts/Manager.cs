using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Spawner _spawner;
    [SerializeField, Range(0, 50)] private float _spawnDelay = 3f;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _inputHandler.SpaceClicked += ChangeSpawnRoutineState;
    }

    private void OnDisable()
    {
        _inputHandler.SpaceClicked -= ChangeSpawnRoutineState;
    }

    private void ChangeSpawnRoutineState()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(SpawnRoutine());
            Debug.Log("Start");
        }
        else
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            Debug.Log("Stop");
        }
    }

    private IEnumerator SpawnRoutine()
    {
        float currentSpawnDelay = _spawnDelay;
        var wait = new WaitForSeconds(currentSpawnDelay);

        while (enabled)
        {
            _spawner.Spawn();
            currentSpawnDelay = _spawnDelay;
            yield return wait;
        }
    }
}
