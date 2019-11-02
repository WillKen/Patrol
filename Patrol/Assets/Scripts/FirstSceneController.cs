using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    public SpiderFactory spider_factory;                               //巡逻者工厂
    public ScoreRecorder recorder;                                   //记录员
    public SpiderActionManager action_manager;                       //运动管理器
    public int wall_sign = -1;                                       //当前玩家所处哪个格子
    public GameObject player;                                        //玩家
    public Camera main_camera;                                       //主相机
    public float player_speed = 10;                                  //玩家移动速度
    private List<GameObject> spiders;                                //场景中巡逻者列表
    private bool game_over = false;                                  //游戏结束

    
    void Start()
    {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        spider_factory = Singleton<SpiderFactory>.Instance;
        action_manager = gameObject.AddComponent<SpiderActionManager>() as SpiderActionManager;
        LoadResources();
        main_camera.GetComponent<CameraFlow>().follow = player;
        recorder = Singleton<ScoreRecorder>.Instance;
    }

    void Update()
    {
        for (int i = 0; i < spiders.Count; i++)
        {
            spiders[i].gameObject.GetComponent<SpiderData>().wall_sign = wall_sign;
        }

    }

    public void LoadResources()
    {
        //Debug.Log("123");
        Instantiate(Resources.Load<GameObject>("Prefabs/Maze"));
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 9, 0), Quaternion.identity) as GameObject;
        spiders = spider_factory.GetSpiders();
        //所有侦察兵移动
        for (int i = 0; i < spiders.Count; i++)
        {
            action_manager.GoSpider(spiders[i]);
        }
    }
    //玩家移动
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
            //移动和旋转
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

            //防止碰撞带来的移动
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
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameoverChange += Gameover;
        
    }
    void OnDisable()
    {
        GameEventManager.ScoreChange -= AddScore;
        GameEventManager.GameoverChange -= Gameover;
      
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
