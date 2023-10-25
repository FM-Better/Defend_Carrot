using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadSceneArgs
{
    // ³¡¾°Ë÷Òý
    public int sceneIndex;
    public UnityAction callback = null;

    public LoadSceneArgs(int sceneIndex, UnityAction callback)
    {
        this.sceneIndex = sceneIndex;
        this.callback = callback;
    }
}
