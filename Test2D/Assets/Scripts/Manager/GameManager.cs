using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : Singleton<GameManager>
{
    //[SerializeField]
    //private GameObject gameSettingPanel;
    //[SerializeField]
    //private Slider BKMusicSlider;//Slider 对象
    //[SerializeField]
    //private Slider SoundMusicSlider;

    public GameObject player;
    public GameObject[] monsters;
    public Transform[] createPoint;
    public Dictionary<BulletType,MonsterType> BulletTargetDic = new Dictionary<BulletType,MonsterType>();
    public float createRate=3.0f;
    public float nextCreate = 0f;
    public GameObject endPanel;

    void Start()
    {
        //gameSettingPanel = GameObject.Find("GameSettingPanel");
        //BKMusicSlider = GameObject.Find("BKMusicSlider").GetComponent<Slider>();
        //SoundMusicSlider = GameObject.Find("SoundMusicSlider").GetComponent<Slider>();
        BulletTargetDic.Add(BulletType.bullet0,MonsterType.monster0);
        BulletTargetDic.Add(BulletType.bullet1, MonsterType.monster1);
        BulletTargetDic.Add(BulletType.bullet2, MonsterType.monster2);
        player = GameObject.Find("Player");

    }

    private void FixedUpdate()
    {
        CreateMonster();
    }


    public void OpenGameSetting()
    {
        Time.timeScale = 0;
    }

    public void CloseGameSetting()
    {
        Time.timeScale = 1f;
    }

    public void OnRestart()//点击“重新开始”时执行此方法
    {
        //Loading Scene0
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }


    public void CreateMonster()
    {
        nextCreate += Time.fixedDeltaTime;
        if(nextCreate>createRate)
        {
            nextCreate = 0;
            foreach (Transform i in createPoint)
            {
                int ifCreate = Random.Range(0, 2);
                int monsterNum = Random.Range(0, 3);
                if (ifCreate > 0)
                {
                    Instantiate(monsters[monsterNum], i.position, Quaternion.identity);
                }
            }
        }
       

    }

    //public void BKMusicSetting()
    //{
    //    //Debug.Log(BKMusicSlider.value / 1);
    //    MusicMgr.GetInstance().changeValue(BKMusicSlider.value / 1);
    //}

    //public void SoundMusicSetting()
    //{
    //    //Debug.Log(SoundMusicSlider.value / 1);
    //    MusicMgr.GetInstance().changeSoundValue(SoundMusicSlider.value / 1);
   
    //}

}
