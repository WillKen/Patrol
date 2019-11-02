using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFactory : MonoBehaviour
{
    private GameObject spider = null;                              //巡逻兵
    private List<GameObject> used = new List<GameObject>();        //正在被使用的巡逻兵
    private Vector3[] vec = new Vector3[9];                        //保存每个巡逻兵的初始位置
    public FirstSceneController sceneControler;                    //场景控制器

    //返回巡逻兵列表
    public List<GameObject> GetSpiders()
    {
        int[] pos_x = { -8, 4, 13 };
        int[] pos_z = { 8, -3, -13 };
        int index = 0;
        //生成不同的巡逻兵初始位置
        for(int i=0;i < 3;i++)
        {
            for(int j=0;j < 3;j++)
            {
                vec[index] = new Vector3(pos_x[j], 0, pos_z[i]);
                index++;
            }
        }
        //加载巡逻兵
        for(int i=0; i < 9; i++)
        {
            spider = Instantiate(Resources.Load<GameObject>("Prefabs/Spider"));
            spider.transform.position = vec[i];
            spider.GetComponent<SpiderData>().sign = i + 1;
            spider.GetComponent<SpiderData>().start_position = vec[i];
            used.Add(spider);
        }   
        return used;
    }


    //停止巡逻
    public void StopSpider()
    {
        //切换所有侦查兵的动画
        for (int i = 0; i < used.Count; i++)
        {
            used[i].gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
