﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{
    //加载场景资源
    void LoadResources();
}

public interface IUserAction                          
{
    //移动玩家
    void MovePlayer(float translationX, float translationZ);
    //得到分数
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