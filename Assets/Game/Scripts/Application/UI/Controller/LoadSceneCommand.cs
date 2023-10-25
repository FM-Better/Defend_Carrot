using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        LoadSceneArgs loadSceneArgs = notification.Body as LoadSceneArgs;
        SceneMgr.LoadSceneAsync(loadSceneArgs.sceneIndex, loadSceneArgs.callback);
    }
}
