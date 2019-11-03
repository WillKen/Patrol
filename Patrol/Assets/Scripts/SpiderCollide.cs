using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCollide : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.gameObject.transform.parent.GetComponent<SpiderData>().follow_player = true;
            this.gameObject.transform.parent.GetComponent<SpiderData>().player = collider.gameObject;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.gameObject.transform.parent.GetComponent<SpiderData>().follow_player = false;
            this.gameObject.transform.parent.GetComponent<SpiderData>().player = null;
        }
    }
}
