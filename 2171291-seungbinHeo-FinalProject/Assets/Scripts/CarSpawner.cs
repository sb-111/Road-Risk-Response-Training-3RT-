using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] carPrefabs; // 생성할 차량 프리팹 배열
    [SerializeField] private Transform[] spawnPoints; // 차량이 나타날 차선별 지점 배열
    [SerializeField] private float minSpawnInterval = 1.0f; // 최소 생성 간격
    [SerializeField] private float maxSpawnInterval = 3.0f; // 최대 생성 간격

    private float[] nextSpawnTime; // 각 차선의 다음 생성 시간을 저장할 배열

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
