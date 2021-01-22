using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bariar : MonoBehaviour
{
    public AudioClip hitaudio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(hitaudio, transform.position);
    }
}
