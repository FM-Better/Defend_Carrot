using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : BaseManagerMono<GameMgr>
{
    void Start()
    {
        // ����MVC��Facade
        GameFacade.Instance.StartUp();
    }
}
