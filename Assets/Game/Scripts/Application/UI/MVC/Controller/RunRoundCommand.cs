using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunRoundCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).RunRound();
    }
}
