using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsRefsSO AudioClipsRefsSO;
    public static SoundManager Instance { get; set; }
    private float volume = 1f;
    private const string PLAYER_PREFS_SFX_VOLUME = "sfxVolume";
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipieSuccess += DeliveryManager_OnRecipieSuccess;
        DeliveryManager.Instance.OnRecipieFailed += DeliveryManager_OnRecipieFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedUpSomething += Instance_OnPickedUpSomething;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced1;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter baseCounter = sender as TrashCounter;
        PlaySound(AudioClipsRefsSO.trash, baseCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced1(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(AudioClipsRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_OnPickedUpSomething()
    {
        PlaySound(AudioClipsRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(AudioClipsRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipieFailed()
    {
        PlaySound(AudioClipsRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipieSuccess()
    {
        PlaySound(AudioClipsRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume * volumeMultiplier);
    }

    public void PlayFootStepSound(Vector3 position, float volume)
    {
        PlaySound(AudioClipsRefsSO.footstep, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(AudioClipsRefsSO.warning, Vector3.zero);
    }
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(AudioClipsRefsSO.warning[0], position);
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1)
        {
            volume = 0;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
    }
    public float GetVolume()
    {
        return volume;
    }
}
