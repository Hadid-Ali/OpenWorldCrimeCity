using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    MELEE_PUNCH_KICK = 0,
    CLICK_SOUND = 1
}

[System.Serializable]
public class AudioSourceWithType
{
    public AudioClip clip;
    public SoundType soundType;
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource oneShotAudio;

    public List<AudioSourceWithType> sounds;
    
    void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        DontDestroyOnLoad(this);
        this.oneShotAudio = this.GetComponent<AudioSource>();
    }



    public void PlaySound(SoundType type)
    {
        this.oneShotAudio.PlayOneShot(this.sounds.Find(x => x.soundType.Equals(type)).clip);
    }

}
