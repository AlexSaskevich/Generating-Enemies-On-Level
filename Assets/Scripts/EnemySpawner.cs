using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _secondsBetweenSpawn = 2.0f;

    private Transform _spawnPoints;
    private Transform[] _points;

    private void Awake()
    {
        _spawnPoints = GetComponent<Transform>();
    }

    private void Start()
    {
        Initialize();

        ShuffleArray();

        StartCoroutine(Spawn());
    }

    private void Initialize()
    {
        _points = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            _points[i] = _spawnPoints.GetChild(i);
        }
    }

    private void ShuffleArray()
    {
        int minRandomIndex = 0;

        for (int i = _points.Length - 1; i >= 1; i--)
        {
            int randomIndex = Random.Range(minRandomIndex, i + 1);

            var temp = _points[randomIndex];

            _points[randomIndex] = _points[i];
            _points[i] = temp;
        }
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_secondsBetweenSpawn);

        for (int i = 0; i < _points.Length; i++)
        {
            Instantiate(_template, new Vector2(_points[i].transform.position.x, _points[i].transform.position.y), Quaternion.identity);

            yield return waitForSeconds;
        }
    }
}