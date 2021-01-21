using UnityEngine;

namespace Assets.Script
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        public float MoveSpeed = 3;

        public GameObject ExplosionPrefab;

        private Vector3 _eulerAngles;

        public GameObject DefendEffectPrefab;

        private float _timeVal;

        private bool _isDefended = true;

        private float _defendedTimeVal;

        private SpriteRenderer _sprite;

        public GameObject Bullect;

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
            //是否处于无敌状态
            if (_isDefended)
            {
                DefendEffectPrefab.SetActive(true);
                _defendedTimeVal -= Time.deltaTime;
                if (_defendedTimeVal <= 0)
                {
                    _isDefended = false;
                    DefendEffectPrefab.SetActive(false);
                }
            }

        }

        /// <summary>
        /// 在Update之后运行，固定物理帧，每一秒都是是匀称的，
        /// </summary>
        private void FixedUpdate()
        {
            if (PlayerMananger.Instance.IsDefeat)
            {
                return;
            }
            Move();
            //攻击CD
            if (_timeVal > 0.4f)
            {
                Attack();
            }
            else
            {
                _timeVal += Time.fixedDeltaTime;
            }
        }

        private void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(Bullect, transform.position, Quaternion.Euler(transform.eulerAngles + _eulerAngles));
                _timeVal = 0;
            }
        }

        /// <summary>
        /// 坦克移动的方法
        /// </summary>
        private void Move()
        {
            var horizontal = Input.GetAxisRaw(Const.Horizontal);
            transform.Translate(Vector3.right * horizontal * MoveSpeed * Time.fixedDeltaTime, Space.World);

            if (horizontal < 0)
            {
                _sprite.sprite = TankSprites[3];
                _eulerAngles = new Vector3(0, 0, 90);
            }
            else if (horizontal > 0)
            {
                _sprite.sprite = TankSprites[1];
                _eulerAngles = new Vector3(0, 0, -90);
            }

            if (horizontal != 0)
            {
                return;
            }

            var vertical = Input.GetAxisRaw(Const.Vertical);
            transform.Translate(Vector3.up * vertical * MoveSpeed * Time.fixedDeltaTime, Space.World);

            if (vertical < 0)
            {
                _sprite.sprite = TankSprites[2];
                _eulerAngles = new Vector3(0, 0, -180);
            }
            else if (vertical > 0)
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
            if (_isDefended)
            {
                return;
            }
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            PlayerMananger.Instance.IsDead = true;
            Destroy(gameObject);
        }


    }
}
