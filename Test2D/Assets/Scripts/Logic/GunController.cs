using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint;
    private int totalBulletsTypeNum=3;//子弹类型总数
    private int curBulletNum=0;//现在的子弹类型的序号
    private float nextFire=0f; //下次发射子弹时间
    private float fireRate=0.3f;//发射频率
    private float bulletSpeed=3.0f;//子弹速度
    private List<GameObject> bulletList = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < totalBulletsTypeNum; i++)
        {
            bulletList.Add(Resources.Load("Prefabs/Bullets/bullet" + i) as GameObject);
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        FollowMouseRotate();
    }

    //物体跟随鼠标旋转
    private void FollowMouseRotate()
    {
        //获取鼠标的坐标，鼠标是屏幕坐标，Z轴为0，这里不做转换  
        Vector3 mouse = Input.mousePosition;
        //获取物体坐标，物体坐标是世界坐标，将其转换成屏幕坐标，和鼠标一直  
        Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);
        //屏幕坐标向量相减，得到指向鼠标点的目标向量，即黄色线段  
        Vector3 gunDirection = mouse - obj;

        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);

        //计时器，子弹发射有间隙
        nextFire += Time.fixedDeltaTime;
        //如果按下了鼠标左键且计时器大于发射间隙
        if (Input.GetMouseButton(0) && nextFire > fireRate)
        {


            //计时器归零
            nextFire = 0;

            ////生成子弹
            //GameObject b = Instantiate(bulletList[curBulletNum], gameObject.transform.position, Quaternion.identity) as GameObject;

            ////子弹速度由鼠标点击的位置减去屏幕的宽高的1/2得到
            ////主要就是坐标的转换
            //b.GetComponent<Rigidbody2D>().velocity = ((direction-gameObject.transform.position).normalized * bulletSpeed);
            ////将所有子弹放在一个父物体下，方便操作
            //b.transform.SetParent(GameObject.Find("Bullets").transform);


            //实例化子弹
            GameObject b = Instantiate(bulletList[curBulletNum], firePoint.transform.position, Quaternion.Euler(transform.eulerAngles));

            AudioSource audio;
            MusicMgr.GetInstance().PlaySoundMusic("hit", false, (source) =>
            {
                audio = source;
            });
            //初始化子弹的方向速度
            //b.GetComponent<Rigidbody2D>().velocity = target - gameObject.transform.position;
        }

    }

    //换子弹
    public void ChangeBullet()
    {
        curBulletNum++;
        curBulletNum = curBulletNum % totalBulletsTypeNum;
        EventCenter.GetInstance().EventTrigger<int>("ChangeBullet", curBulletNum);
        //UI
    }

}
