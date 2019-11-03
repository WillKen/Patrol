using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFactory : MonoBehaviour
{
    private GameObject spider = null;                              
    private List<GameObject> used = new List<GameObject>();       
    private Vector3[] vec = new Vector3[9];                        
    public FirstSceneController sceneControler;                 

    //返回巡逻兵列表
    public List<GameObject> GetSpiders()
    {
        int[] pos_x = { -8, 4, 13 };
        int[] pos_z = { 8, -3, -13 };
        int index = 0;
        for(int i=0;i < 3;i++)
        {
            for(int j=0;j < 3;j++)
            {
                vec[index] = new Vector3(pos_x[j], 0, pos_z[i]);
                index++;
            }
        }

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


    public void StopSpider()
    {
        for (int i = 0; i < used.Count; i++)
        {
            used[i].gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
