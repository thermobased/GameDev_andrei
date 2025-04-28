using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemies/New Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Stats")]
    public string enemyName = "Enemy";
    public float maxHealth = 100f;
    
    [Header("Visuals")]
    public GameObject enemyPrefab;
    
    [Header("Dropped Rewards")]
    public int currencyReward = 5;
}
