using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public Image[] bullets;
    public int curBulletNum = 0;
    void Start()
    {
        EventCenter.GetInstance().AddEventListener<int>("ChangeBullet", ChangeBullet);
        ChangeBullet(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBullet(int newBulletNum)
    {
        bullets[curBulletNum].color = new Color(180/255, 180/255, 180/255);
        bullets[newBulletNum].color = new Color(1, 1, 1);
        curBulletNum = newBulletNum;
    } 
}
