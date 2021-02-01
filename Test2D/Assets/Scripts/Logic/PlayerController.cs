using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //玩家按键
    private KeyCode MoveUP;
    private KeyCode MoveDown;
    private KeyCode MoveLeft;
    private KeyCode MoveRight;
    private KeyCode changeBullet;

    //玩家组件
    private Rigidbody2D rb;
    private Collider2D cd;
    private SpriteRenderer sr;
    public Sprite[] humanSp;
    public GunController gun;

    //玩家数值
    private float moveSpeed=5.0f;
    private AudioSource audio;



    //移动中间值
    private float Dup;
    private float Dright;
    private float DupTarget;
    private float DrightTarget;
    private float VelocityDup;
    private float VelocityDright;

   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        gun = GetComponentInChildren<GunController>();
        MoveUP = KeyCode.W;
        MoveDown = KeyCode.S;
        MoveLeft = KeyCode.A;
        MoveRight = KeyCode.D;
        changeBullet = KeyCode.R;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeBullet();
    }


    private void Move()
    {

        //float x = Input.GetAxisRaw("Horizontal");
        //transform.Translate(Vector3.right * x * moveSpeed * Time.deltaTime, Space.World);

        //float y = Input.GetAxisRaw("Vertical");
        //transform.Translate(Vector3.up * y * moveSpeed * Time.deltaTime, Space.World);

        DupTarget = (Input.GetKey(MoveUP) ? 1.0f : 0) - (Input.GetKey(MoveDown) ? 1.0f : 0);
        DrightTarget = (Input.GetKey(MoveRight) ? 1.0f : 0) - (Input.GetKey(MoveLeft) ? 1.0f : 0);

        Dup = Mathf.SmoothDamp(Dup, DupTarget, ref VelocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, DrightTarget, ref VelocityDright, 0.1f);

        if(DupTarget > 0)
        {
            sr.sprite = humanSp[1];
        }
        else
        {
            sr.sprite = humanSp[0];
        }
        if (Dright > 0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }




        rb.velocity = new Vector2(Dright*moveSpeed, Dup*moveSpeed);
        
    }

    public void ChangeBullet()
    {
        if(Input.GetKeyDown(changeBullet))
        {
            gun.ChangeBullet();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            SoftWareMgr.GetInstance().LoadScene(SceneName.EndScene);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            MusicMgr.GetInstance().PlaySoundMusic("hurt", false, (source) =>
            {
                audio = source;
            });
        }
    }
}
