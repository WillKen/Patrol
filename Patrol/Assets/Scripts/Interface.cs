﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{

}

public interface IUserAction                          
{
    void MovePlayer(float translationX, float translationZ);
    int GetScore();
    bool GetGameover();
}

public interface ISSActionCallback
{
    void SSActionEvent(SSAction source,int intParam = 0,GameObject objectParam = null);
}

public interface IGameStatusOp
{
    void PlayerEscape();
    void PlayerGameover();
}
