using UnityEngine;

namespace Assets.Script
{
    public class Bullect : MonoBehaviour
    {
        public float MoveSpeed = 10;

        public bool IsPlayer;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(transform.up * MoveSpeed * Time.deltaTime, Space.World);

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Tank":
                    if (!IsPlayer)
                    {
                        collision.SendMessage("Die");
                        Destroy(gameObject);
                    }
                    break;
                case "Wall":
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    break;
                case "base":
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                    break;
                case "IronWall":
                    if (IsPlayer)
                    {
                        collision.SendMessage("PlayAudio");
                    }
                    Destroy(gameObject);
                    break;
                case "Enemy":
                    if (IsPlayer)
                    {
                        collision.SendMessage("Die");
                        Destroy(gameObject);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
