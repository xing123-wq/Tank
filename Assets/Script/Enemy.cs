using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed = 3;

    public GameObject ExplosionPrefab;

    private Vector3 _eulerAngles;

    private float TimeValChangeDirection;

    private float _timeVal;

    private SpriteRenderer _sprite;

    public GameObject Bullect;

    private float v = -1;
    private float h;

    /// <summary>
    ///玩家的 上 右 下 左
    /// </summary>
    public Sprite[] TankSprites;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //攻击的时间间隔
        if (_timeVal >= 3)
        {
            Attack();
        }
        else
        {
            _timeVal += Time.deltaTime;
        }
    }

    /// <summary>
    /// 在Update之后运行，固定物理帧，每一秒都是是匀称的，
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    private void Attack()
    {
        Instantiate(Bullect, transform.position, Quaternion.Euler(transform.eulerAngles + _eulerAngles));
        _timeVal = 0;
    }

    /// <summary>
    /// 坦克移动的方法
    /// </summary>
    private void Move()
    {
        if (TimeValChangeDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 2 && num <= 4)
            {
                h = -1;
                v = 0;
            }
            TimeValChangeDirection = 0;
        }
        else
        {
            TimeValChangeDirection += Time.fixedDeltaTime;
        }
        transform.Translate(Vector3.right * h * MoveSpeed * Time.fixedDeltaTime, Space.World);

        if (h < 0)
        {
            _sprite.sprite = TankSprites[3];
            _eulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            _sprite.sprite = TankSprites[1];
            _eulerAngles = new Vector3(0, 0, -90);
        }

        if (h != 0)
        {
            return;
        }

        transform.Translate(Vector3.up * v * MoveSpeed * Time.fixedDeltaTime, Space.World);

        if (v < 0)
        {
            _sprite.sprite = TankSprites[2];
            _eulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            _sprite.sprite = TankSprites[0];
            _eulerAngles = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// 坦克死亡
    /// </summary>
    private void Die()
    {
        PlayerMananger.Instance.playerscore++;
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TimeValChangeDirection = 4;
        }
    }
}
