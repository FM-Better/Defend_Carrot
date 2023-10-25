using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : BaseManagerMono<GameMgr>
{
    void Start()
    {
        // Æô¶¯MVCµÄFacade
        GameFacade.Instance.StartUp();
    }
}
