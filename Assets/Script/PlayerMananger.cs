using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMananger : MonoBehaviour
{
    public int lifevalue = 3;
    public int playerscore = 0;
    public bool IsDead;
    public bool IsDefeat;

    public Text PlayerScoreText;
    public Text PlayerLifeValueText;

    public GameObject Bron;
    public static PlayerMananger Instance { get; set; }
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            Recover();
        }
        PlayerScoreText.text = playerscore.ToString();
        PlayerLifeValueText.text = lifevalue.ToString();
    }

    private void Recover()
    {
        if (lifevalue <= 0)
        {
            //游戏失败，返回主界面


        }
        else
        {
            lifevalue--;
            GameObject game = Instantiate(Bron, new Vector3(-2, -8, 0), Quaternion.identity);
            game.GetComponent<Borm>().CreatePlayer = true;
            IsDead = false;
        }
    }
}
