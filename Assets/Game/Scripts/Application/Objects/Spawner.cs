using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 回合之间的间隔时间
    public float roundInterval = 5f;
    // 回合内出怪的间隔时间
    public float spawnInterval = 1f;

    private List<Round> m_rounds = new List<Round>();
    private WaitForSeconds roundSeconds;
    private WaitForSeconds spawnSeconds;

    public void InitializeSpawner()
    {
        m_rounds = GetComponent<Map>().Level.rounds;
        roundSeconds = new WaitForSeconds(roundInterval);
        spawnSeconds = new WaitForSeconds(spawnInterval);
    }

    public void StartSpawn() => StartCoroutine(Cou_Spawn());

    public void StopSpawn() => StopCoroutine(Cou_Spawn());

    IEnumerator Cou_Spawn()
    {
        for (int i = 0; i < m_rounds.Count; i++)
        {
            GameFacade.Instance.SendNotification(MVCNotification.RUN_ROUND);
            for (int j = 0; j  < m_rounds[i].monsterNum; j++)
            {
                print("出怪: " + m_rounds[i].monsterType);
                GameMgr.Instance.SpawnMonster();
                yield return spawnSeconds;
            }
            yield return roundSeconds;
        }
    }
}
