using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneBtn : MonoBehaviour
{
    public SceneName sceneName;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ChangeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        SoftWareMgr.GetInstance().LoadScene(sceneName);
    }
}
