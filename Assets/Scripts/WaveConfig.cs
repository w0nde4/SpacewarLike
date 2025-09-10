using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float randomSpawnCoefficient = 0.3f;
    [SerializeField] int enemiesAmount = 5;
    [SerializeField] float moveSpeed = 2f;

    public List<Transform> GetWaypoints()
    {   
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints; 
    } 
    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public float GetTimeBetweenSpawn() { return timeBetweenSpawn; }
    public float GetRandomSpawnCoefficient() { return randomSpawnCoefficient; }
    public int GetEnemiesAmount() { return enemiesAmount; }
    public float GetEnemyMoveSpeed() { return moveSpeed; }
}
