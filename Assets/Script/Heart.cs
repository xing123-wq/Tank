using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer _sprite;
    public Sprite BrokenSprite;
    public GameObject ExplosinPrefab;
    public AudioClip dieAudio;


    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        _sprite.sprite = BrokenSprite;
        Instantiate(ExplosinPrefab, transform.position, transform.rotation);
        PlayerMananger.Instance.IsDefeat = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
