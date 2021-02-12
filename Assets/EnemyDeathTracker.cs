using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathTracker : MonoBehaviour
{
    public TempKillGuy[] enemies;
    [SerializeField] private GameObject enemyReward = null;

    public collectableKiller[] collectables;
    [SerializeField] private GameObject collectableReward = null;

    private bool allKilled = false;
    private bool allCollected = false;

    void Update()
    {
        if (!allKilled)
        {
            UpdateEnemies();
        }

       if (!allCollected)
        {
            UpdateCollectables();
        }
    }

    private void UpdateEnemies()
    {
        var killedOnce = 0;
        var i = 0;

        foreach (TempKillGuy enemy in enemies)
        {
            i++;
            if (enemy.hasDiedBefore)
            {
                killedOnce++;
            }
        }

        if (killedOnce == i)
        {
            enemyReward.SetActive(true);
        }
    }

    private void UpdateCollectables()
    {
        var CollectedTotal = 0;
        var ic = 0;

        foreach (collectableKiller collectable in collectables)
        {
            ic++;
            if (collectable.collected)
            {
                CollectedTotal++;
            }
        }

        if (CollectedTotal == ic)
        {
            collectableReward.SetActive(true);
        }
    }
}
