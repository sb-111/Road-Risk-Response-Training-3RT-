using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] carPrefabs; // ������ ���� ������ �迭
    [SerializeField] private Transform[] spawnPoints; // ������ ��Ÿ�� ������ ���� �迭
    [SerializeField] private float minSpawnInterval = 1.0f; // �ּ� ���� ����
    [SerializeField] private float maxSpawnInterval = 3.0f; // �ִ� ���� ����

    private float[] nextSpawnTime; // �� ������ ���� ���� �ð��� ������ �迭

    private void Start()
    {
        nextSpawnTime = new float[spawnPoints.Length];
        for (int i = 0; i < nextSpawnTime.Length; i++)
        {
            nextSpawnTime[i] = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    private void Update()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Time.time >= nextSpawnTime[i])
            {
                SpawnCar(i);
                nextSpawnTime[i] = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
    }

    private void SpawnCar(int laneIndex)
    {
        int carIndex = Random.Range(0, carPrefabs.Length);
        GameObject newCar = Instantiate(carPrefabs[carIndex], spawnPoints[laneIndex].position, spawnPoints[laneIndex].rotation);
    }
}
