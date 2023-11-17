using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : BaseManagerMono<GameMgr>
{
    private int m_aliveMonster;

    void Start()
    {
        // Æô¶¯MVCµÄFacade
        GameFacade.Instance.StartUp();
        m_aliveMonster = 0;
    }

    public void ClearMonster() => m_aliveMonster = 0;

    public void SpawnMonster() => m_aliveMonster++;

    public void KillMonster() => m_aliveMonster--;
}
