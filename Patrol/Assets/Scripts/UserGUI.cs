﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private IUserAction action;
    private GUIStyle score_style = new GUIStyle();
    private GUIStyle text_style = new GUIStyle();
    private GUIStyle over_style = new GUIStyle();
    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        text_style.normal.textColor = new Color(0, 0, 0, 1);
        text_style.fontSize = 16;
        score_style.normal.textColor = new Color(1,0.92f,0.016f,1);
        score_style.fontSize = 16;
        over_style.fontSize = 25;
    }

    void Update()
    {
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        action.MovePlayer(translationX, translationZ);
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 5, 200, 50), "分数:", text_style);
        GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), score_style);
        GUI.Label(new Rect(Screen.width / 2-80 ,10, 100, 100), "使用WSAD或方向键移动", text_style);
        GUI.Label(new Rect(Screen.width / 2 - 87, 30, 100, 100), "成功逃离蜘蛛的追击得1分", text_style);
        if(action.GetGameover() )
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "游戏结束", over_style);
        }
        
    }
}
