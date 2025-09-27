using TMPro;
using UnityEngine;

public class SpawnerInfoRenderer<T> : MonoBehaviour where T : MonoBehaviour
{
    private const string SpawnedTitle = "Total spawned: ";
    private const string CreatedTitle = "Total created: ";
    private const string ActiveTitle = "Active: ";

    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private TextMeshProUGUI _spawnedTotal;
    [SerializeField] private TextMeshProUGUI _createdTotal;
    [SerializeField] private TextMeshProUGUI _active;

    protected void Update()
    {
        _spawnedTotal.text = SpawnedTitle + _spawner.SpawnedCount.ToString();
        _createdTotal.text = CreatedTitle + _spawner.CreatedCount.ToString();
        _active.text = ActiveTitle + _spawner.ActiveCount.ToString();
    }
}
