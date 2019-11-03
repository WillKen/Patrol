using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderActionManager : SSActionManager
{
    private GoSpiderAction go_spider; 

    public void GoSpider(GameObject spider)
    {
        go_spider = GoSpiderAction.GetSSAction(spider.transform.position);
        this.RunAction(spider, go_spider, this);
    }

    public void DestroyAllAction()
    {
        DestroyAll();
    }
}
