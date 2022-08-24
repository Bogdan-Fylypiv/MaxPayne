using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float delay;
    bool canPlay;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        if (!canPlay)
            return;

        Manager.ManagerInstance.Timer.Add(() => { canPlay = true; }, delay);

        canPlay = false;

        AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}
