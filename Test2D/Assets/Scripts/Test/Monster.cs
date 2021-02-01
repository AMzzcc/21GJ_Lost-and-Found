using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    monster0,
    monster1,
    monster2
}
public class Monster : MonoBehaviour
{
    public int lifeValue = 100;//怪物血量
    private float moveSpeed=2.5f;
    private SpriteRenderer sr;
    public MonsterType type;
    private Rigidbody2D rg2d;
    private AudioSource audio;



    private void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        int monsterNum = UnityEngine.Random.Range(0, 3);
        MusicMgr.GetInstance().PlaySoundMusic("monster"+monsterNum, false, (source) =>
            {
                audio = source;
            });

    }
    void Update()
    {
        if (lifeValue == 0)
        {
            Dead();
        }
        else
        {
            MoveToTarget(GameManager.Instance.player.transform);
        }

    }
    public void Dead()
    {
        Debug.Log("Monster has Dead");
        gameObject.SetActive(false);
        //EventCenter.GetInstance().EventTrigger<Monster>("MonsterDead",this);
    }

    public void MoveToTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle - 120);
        if(direction.x>0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        rg2d.velocity = direction.normalized * moveSpeed;
    }


}
