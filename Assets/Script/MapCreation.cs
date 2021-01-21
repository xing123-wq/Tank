using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    /// <summary>
    /// 用来装饰初始化地图所需物体的数组
    /// 0.老家  1.墙  2.铁墙  3.出生效果 4.河流 5.草 6.空气墙
    /// </summary>
    public GameObject[] Item;

    /// <summary>
    /// 存在的位置已有对象列表
    /// </summary>
    private List<Vector3> _item = new List<Vector3>();

    private void Awake()
    {
        //实例化家
        CreateItem(Item[0], new Vector3(0, -8, 0), Quaternion.identity);

        //用墙把家围起来
        CreateItem(Item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(Item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(Item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        //实例化空气墙
        for (int i = -11; i < 12; i++)
        {
            CreateItem(Item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItem(Item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(Item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(Item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        //实例化地图
        for (int i = 0; i < 20; i++)
        {
            CreateItem(Item[1], CreateRandom(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(Item[2], CreateRandom(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(Item[4], CreateRandom(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(Item[5], CreateRandom(), Quaternion.identity);
        }

        //初始化玩家
        GameObject Player = Instantiate(Item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        Player.GetComponent<Borm>().CreatePlayer = true;

        CreateItem(Item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(Item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(Item[3], new Vector3(10, 8, 0), Quaternion.identity);

        InvokeRepeating("CreateEnemy", 4, 5);
    }

    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 enemypos = new Vector3();
        if (num == 0)
        {
            enemypos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            enemypos = new Vector3(0, 8, 0);
        }
        else
        {
            enemypos = new Vector3(10, 8, 0);
        }
        CreateItem(Item[3], enemypos, Quaternion.identity);
    }

    private void CreateItem(GameObject game, Vector3 vector3, Quaternion quaternion)
    {
        GameObject itemGo = Instantiate(game, vector3, quaternion);
        itemGo.transform.SetParent(gameObject.transform);
        _item.Add(vector3);
    }

    /// <summary>
    /// 产生随机位置
    /// </summary>
    private Vector3 CreateRandom()
    {
        //不生成x=-10，10的两列，y=-8,8正两行的位置
        while (true)
        {
            Vector3 Position = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(Position))
            {
                return Position;
            }
        }
    }

    private bool HasThePosition(Vector3 vector)
    {
        for (int i = 0; i < _item.Count; i++)
        {
            if (vector == _item[i])
            {
                return true;
            }
        }
        return false;
    }
}

