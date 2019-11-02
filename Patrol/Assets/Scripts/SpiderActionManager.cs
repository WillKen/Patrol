using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderActionManager : SSActionManager
{
    private GoSpiderAction go_spider;                            //巡逻兵巡逻

    public void GoSpider(GameObject spider)
    {
        go_spider = GoSpiderAction.GetSSAction(spider.transform.position);
        this.RunAction(spider, go_spider, this);
    }
    //停止所有动作
    public void DestroyAllAction()
    {
        DestroyAll();
    }
}
