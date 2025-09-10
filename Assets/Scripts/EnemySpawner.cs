using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startWave = 0;
    [SerializeField] bool looping = false;
    
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnWaves()
    {
        for (int waveNumber = startWave; waveNumber < waveConfigs.Count; waveNumber++)
        {
            var currentWave = waveConfigs[waveNumber];
            yield return StartCoroutine(SpawnEnemies(currentWave));
        }
    }

    IEnumerator SpawnEnemies(WaveConfig waveConfig)
    {
        for(int enemyCount = 0; enemyCount < waveConfig.GetEnemiesAmount(); enemyCount++)
        {
            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }
}
