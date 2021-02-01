using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    bullet0,
    bullet1,
    bullet2
}

public class Bullet : MonoBehaviour
{
    public BulletType bulletType;
    public Rigidbody2D rg2d;
    private float bulletSpeed = 5.0f;
    private float arrowDistance = 50.0f;
    public Vector3 startPos;
   

    // Start is called before the first frame update
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        rg2d.velocity = transform.right * bulletSpeed;
        startPos = transform.position;
        transform.rotation = new Quaternion(0, 0, 90, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if(distance>arrowDistance)
        {
            Destroy(gameObject);
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            if (monster.type == GameManager.Instance.BulletTargetDic[bulletType])
            {
                monster.Dead();

            }
            Destroy(gameObject);
            //other.GetComponent<Monster>();
        }
    }

}
