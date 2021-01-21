using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borm : MonoBehaviour
{
    public GameObject PlayerPrefab;

    public GameObject[] EnemyPrefabs;

    public bool CreatePlayer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 0.8f);
        Destroy(gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void BornTank()
    {
        if (CreatePlayer)
        {
            Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int random = Random.Range(0, 1);
            Instantiate(EnemyPrefabs[random], transform.position, Quaternion.identity);
        }
    }
}
