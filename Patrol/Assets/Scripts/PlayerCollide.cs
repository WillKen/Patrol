using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
			this.GetComponent<Animator>().SetTrigger("attack");
			other.gameObject.GetComponent<Animator>().SetTrigger("death");
            Singleton<GameEventManager>.Instance.PlayerGameover();
        }
    }
}
