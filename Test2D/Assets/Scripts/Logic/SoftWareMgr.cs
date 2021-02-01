using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneName
{
    StartScene,
    GameScene,
    EndScene
}

public class SoftWareMgr : SingletonBase<SoftWareMgr>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //加载游戏场景
    public void LoadScene(SceneName sceneName)
    {
        SceneMgr.GetInstance().LoadScene("Assets/Scenes/" + sceneName + ".unity");
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
         Application.Quit();
#endif

    }
}
