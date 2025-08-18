using System;
using System.Collections;
using UnityEngine;

public class Releaser : MonoBehaviour
{
    private Coroutine _releaseCoroutine;

    public event Action<Cube> ReleaseTimeCome;

    public void StartReleaseCoroutine(Cube cube)
    {
        StartCoroutine(ReleaseCoroutine(cube));
    }

    private IEnumerator ReleaseCoroutine(Cube cube)
    {
        float delay = UnityEngine.Random.Range(cube.MinReleaseValue, cube.MaxReleaseValue);
        yield return new WaitForSeconds(delay);
        ReleaseTimeCome?.Invoke(cube);
    }
}
