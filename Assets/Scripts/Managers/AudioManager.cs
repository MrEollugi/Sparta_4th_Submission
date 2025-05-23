using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    [Header("SFX Clips")]
    public AudioClip jumpClip;
    public AudioClip pickupClip;
    public AudioClip uiClickClip;
    public AudioClip useItemClip;
    public AudioClip switchSlotClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[AudioManager] Tried to play null AudioClip!");
            return;
        }
        sfxSource.PlayOneShot(clip);
    }

    public void PlayJump()
    {
        PlaySFX(jumpClip);
    }
    public void PlayUIClick() => PlaySFX(uiClickClip);
    public void PlayPickup() => PlaySFX(pickupClip);
    public void PlayUseItem() => PlaySFX(useItemClip);
    public void PlaySwitchSlot() => PlaySFX(switchSlotClip);
}
