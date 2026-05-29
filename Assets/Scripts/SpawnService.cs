using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Pool;
using Tools;
using Random = UnityEngine.Random;

public class SpawnService : MonoBehaviour
{
    [SerializeField] private TowerService towerService;
    
    [Header("Настройки спавна")]
    [SerializeField] private ComponentPool normalEnemyPool;
    [SerializeField] private ComponentPool bigEnemyPool;
    [SerializeField] private ComponentPool fastEnemyPool;
    [SerializeField] private EnemyConfig normalEnemyConfig;
    [SerializeField] private EnemyConfig bigEnemyConfig;
    [SerializeField] private EnemyConfig fastEnemyConfig;
    
    [SerializeField] private float spawnRadius = 15f; // Радиус окружности спавна
    [SerializeField] private float spawnInterval = 3f; // Как часто спавнятся враги
    
    private Dictionary<EnemyType, ComponentPool> enemyPools;
    private Dictionary<EnemyType, EnemyConfig> enemyConfigs;

    private void Awake()
    {
        enemyPools = new Dictionary<EnemyType, ComponentPool>
        {
            {EnemyType.Normal, normalEnemyPool},
            {EnemyType.Big, bigEnemyPool},
            {EnemyType.Fast, fastEnemyPool},
        };
        
        enemyConfigs = new Dictionary<EnemyType, EnemyConfig>
        {
            {EnemyType.Normal, normalEnemyConfig},
            {EnemyType.Big, bigEnemyConfig},
            {EnemyType.Fast, fastEnemyConfig},
        };
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var randomEnemyType = EnumHelper.GetRandomEnumValue<EnemyType>();
        var enemy = enemyPools[randomEnemyType].Get<Enemy>();

        enemy.transform.position = GenerateSpawnPosition();
        enemy.Target = towerService.TowerView;

        var config = enemyConfigs[randomEnemyType];
        enemy.Init(config.MaxHealth, config.MoveSpeed, config.AttackDamage, config.AttackDistance, config.AttackCooldown);
    }

    private Vector3 GenerateSpawnPosition()
    {
        var randomAngle = Random.Range(0f, Mathf.PI * 2f);
        var x = Mathf.Cos(randomAngle) * spawnRadius;
        var z = Mathf.Sin(randomAngle) * spawnRadius;
        
        return new Vector3(x, 0.0f, z);
    }
}