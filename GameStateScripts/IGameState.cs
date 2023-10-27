using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    public void OnStart();
    public void OnPlay();
    public void OnPause();
    public void OnFail();
}
