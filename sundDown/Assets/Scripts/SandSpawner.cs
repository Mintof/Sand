using UnityEngine;
using System.Collections.Generic;

public class SandSpawner : MonoBehaviour
{
    [Header("Настройки")]
    public SandGrain grainPrefab;
    public Transform spawnPoint;
    public int poolSize = 400;
    public float spawnInterval = 0.03f;
    public float spawnSpread = 0.02f;

    private Queue<SandGrain> pool = new Queue<SandGrain>();
    private float timer = 0f;
    private bool isSpawning = false;
    private Transform poolRoot;

    void Start()
    {
        poolRoot = new GameObject("SandPool").transform;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var grain = Instantiate(grainPrefab, poolRoot);
            grain.gameObject.SetActive(false);
            pool.Enqueue(grain);
        }
    }

    void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        Debug.Log($" TrySpawn вызван | В пуле: {pool.Count}");

        if (pool.Count == 0)
        {
            Debug.LogWarning(" ПУЛ ПУСТ! Увеличь poolSize или подожди пока песок осядет");
            return;
        }

        var grain = pool.Dequeue();
        Vector3 offset = Random.insideUnitSphere * spawnSpread;
        offset.y = Mathf.Abs(offset.y);
        Vector3 spawnPos = spawnPoint.position + offset;

        Debug.Log($" Спавним песчинку в позиции: {spawnPos}");
        Debug.Log($" Spawn Point существует: {spawnPoint != null}");
        Debug.Log($" Префаб песчинки: {grainPrefab != null}");

        grain.WakeUp(spawnPos);
    }

    public void ToggleSpawn()
    {
        isSpawning = !isSpawning;
        Debug.Log($" Спавн песка: {(isSpawning ? "ВКЛ" : "ВЫКЛ")} | В пуле: {pool.Count}");
    }
    public void StopSpawn() => isSpawning = false;
}