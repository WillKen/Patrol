using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    public SpiderFactory spider_factory;                               
    public ScoreRecorder recorder;                                  
    public SpiderActionManager action_manager;                   
    public int wall_sign = -1;
    public GameObject player;
    public Camera main_camera;                                       
    public float player_speed = 10;                                  
    private List<GameObject> spiders;                               
    private bool game_over = false;                              

    
    void Start()
    {
		Debug.Log ("start");
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        spider_factory = Singleton<SpiderFactory>.Instance;
        action_manager = gameObject.AddComponent<SpiderActionManager>() as SpiderActionManager;
		Instantiate(Resources.Load<GameObject>("Prefabs/Maze"));
		player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 9, 0), Quaternion.identity) as GameObject;
		spiders = spider_factory.GetSpiders();
		for (int i = 0; i < spiders.Count; i++)
		{
			action_manager.GoSpider(spiders[i]);
		}
        main_camera.GetComponent<CameraFlow>().follow = player;
		main_camera.GetComponent<CameraFlow>().enabled = true;
		Debug.Log ("camera");
        recorder = Singleton<ScoreRecorder>.Instance;
    }

    void Update()
    {
        for (int i = 0; i < spiders.Count; i++)
        {
            spiders[i].gameObject.GetComponent<SpiderData>().wall_sign = wall_sign;
        }

    }
		
    public void MovePlayer(float translationX, float translationZ)
    {
        if(!game_over)
        {
            if (translationX != 0 || translationZ != 0)
            {
                player.GetComponent<Animator>().SetBool("run", true);
            }
            else
            {
                player.GetComponent<Animator>().SetBool("run", false);
            }
			if (translationZ > 0) {
				player.transform.rotation =  Quaternion.LookRotation(new Vector3(0, 0, 100));
				player.transform.Translate(0, 0, translationZ * player_speed * Time.deltaTime);
			} 
			else if(translationZ < 0) {
				player.transform.rotation =  Quaternion.LookRotation(new Vector3(0,0,-100));
				player.transform.Translate(0, 0, -translationZ * player_speed * Time.deltaTime);
			}

			if (translationX > 0) {
				player.transform.rotation =  Quaternion.LookRotation(new Vector3(100, 0, 0));
				player.transform.Translate(0, 0, translationX * player_speed * Time.deltaTime);
			} 
			else if(translationX < 0) {
				player.transform.rotation =  Quaternion.LookRotation(new Vector3(-100,0,0));
				player.transform.Translate(0, 0, -translationX * player_speed * Time.deltaTime);
			}

            
            if (player.transform.localEulerAngles.x != 0 || player.transform.localEulerAngles.z != 0)
            {
                player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
            }
            if (player.transform.position.y != 0)
            {
                player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            }     
        }
    }

    public int GetScore()
    {
        return recorder.GetScore();
    }

    
    public bool GetGameover()
    {
        return game_over;
    }


    void OnEnable()
    {
		Debug.Log ("en");
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameoverChange += Gameover;
        
    }

    void AddScore()
    {
        recorder.AddScore();
    }
    
    void Gameover()
    {
        game_over = true;
        spider_factory.StopSpider();
        action_manager.DestroyAllAction();
    }
}
