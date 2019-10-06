using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] allGameAudio = null;
    float MasterVolume = 0f;
    float MusicVolume = 0f;
    float SoundFXVolume = 0f;
    private void Start()
    {
        allGameAudio = new AudioSource[34];
        for (int i = 0; i < 34; i++)
        {
            allGameAudio[i] = GetComponents<AudioSource>()[i];

        }
        MasterVolume = PlayerPrefs.GetInt("Master", 70);
        MusicVolume = PlayerPrefs.GetInt("Music", 50);
        SoundFXVolume = PlayerPrefs.GetInt("Sound", 50);
        SetAll();
    }

    public void SetMaster(int master)
    {
        MasterVolume = master;
        PlayerPrefs.SetInt("Master", (int)master);
    }

    public void SetMusic(int music)
    {
        MusicVolume = music;
        PlayerPrefs.SetInt("Music", (int)music);
    }

    public void SetSound(int sound)
    {
        SoundFXVolume = sound;
        PlayerPrefs.SetInt("Sound", (int)sound);
    }

    public void SetAll()
    {
        PlayerPrefs.SetInt("Master", (int)MasterVolume);
        PlayerPrefs.SetInt("Music", (int)MusicVolume);
        PlayerPrefs.SetInt("Sound", (int)SoundFXVolume);
        float MasterMultiplier = MasterVolume / 100;
        allGameAudio[7].volume = ((MusicVolume / 100) / 2) * MasterMultiplier;
        allGameAudio[8].volume = ((MusicVolume / 100) / 2) * MasterMultiplier;
        allGameAudio[19].volume = ((MusicVolume / 100) / 2) * MasterMultiplier;
        allGameAudio[22].volume = ((MusicVolume / 100) * 2) * MasterMultiplier;
        allGameAudio[28].volume = ((MusicVolume / 100) / 2) * MasterMultiplier;
        allGameAudio[29].volume = ((MusicVolume / 100) / 2) * MasterMultiplier;
        allGameAudio[30].volume = ((MusicVolume / 100) / 2) * MasterMultiplier;

        allGameAudio[0].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[1].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[3].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[6].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[10].volume = (SoundFXVolume / 100) * (MasterMultiplier / 3);
        allGameAudio[11].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[12].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[13].volume = (SoundFXVolume / 100) * (MasterMultiplier / 4);
        allGameAudio[14].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[15].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[16].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[17].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[18].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[20].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[20].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[21].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[23].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[24].volume = (SoundFXVolume / 100) * MasterMultiplier / 6;
        allGameAudio[25].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[26].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[27].volume = (SoundFXVolume / 100) * MasterMultiplier / 5;
        allGameAudio[32].volume = (SoundFXVolume / 100) * MasterMultiplier;
        allGameAudio[33].volume = (SoundFXVolume / 100) * MasterMultiplier;
    }


    public void PlayPew()
    {
        GetComponents<AudioSource>()[0]?.Play();
    }
    public void PlayPlayerDamage()
    {
        GetComponents<AudioSource>()[1]?.Play();
    }

    public void PlayPhoneRing()
    {
        GetComponents<AudioSource>()[2]?.Play();
    }
    public void PlayElevatorRing()
    {
        GetComponents<AudioSource>()[3]?.Play();
    }

    public void PlayFilingCabinet()
    {
        GetComponents<AudioSource>()[4]?.Play();
    }

    public void PlayPrinter()
    {
        GetComponents<AudioSource>()[5]?.Play();
    }

    public void PlayJumpSparkle()
    {
        GetComponents<AudioSource>()[6]?.Play();
    }

    public void PlayPauseMusic()
    {
        GetComponents<AudioSource>()[7].Play();
    }

    public void StopPauseMusic()
    {
        GetComponents<AudioSource>()[7].Stop();
    }

    public void PauseLevel1Music()
    {
        GetComponents<AudioSource>()[8].Pause();
    }

    public void UnpauseLevel1Music()
    {
        GetComponents<AudioSource>()[8].UnPause();
    }

    public void Explosion()
    {
        GetComponents<AudioSource>()[10].Play();
    }

    public void Celebrate()
    {
        GetComponents<AudioSource>()[11].Play();
    }
    
    public void EnemyShooting()
    {
        GetComponents<AudioSource>()[12].Play();
    }
    public void DeathSound()
    {
        GetComponents<AudioSource>()[13].Play();
    }

    public void ChargerRunning()
    {
        GetComponents<AudioSource>()[14].loop = true;
        GetComponents<AudioSource>()[14].Play();
    }

    public void ChargerStop()
    {
        GetComponents<AudioSource>()[14].loop = false;
    }

    public void SodaLauncher()
    {
        GetComponents<AudioSource>()[15].Play();
    }

    public void Pencil()
    {
        GetComponents<AudioSource>()[16].Play();
    }

    public void PencilHitWall()
    {
        GetComponents<AudioSource>()[17].Play();
    }

    public void Pickup()
    {
        GetComponents<AudioSource>()[18].Play();
    }
    public void Button()
    {
        GetComponents<AudioSource>()[20].Play();
    }

    public void CoffeeBreak()
    {
        GetComponents<AudioSource>()[21].Play();
    }

    public void GameOver()
    {
        GetComponents<AudioSource>()[22].Play();
    }

    public void Cry()
    {
        GetComponents<AudioSource>()[23].Play();
    }

    public void StopCrying()
    {
        GetComponents<AudioSource>()[23].Stop();
    }

    public void JumpInitial()
    {
        GetComponents<AudioSource>()[24].Play();
    }

    public void NewOrLoadGame()
    {
        GetComponents<AudioSource>()[25].Play();
    }

    public void Sizzle()
    {
        GetComponents<AudioSource>()[26].Play();
    }

    public void WeaponSwap()
    {
        GetComponents<AudioSource>()[27].Play();
    }

    public void PauseLevel2Music()
    {
        GetComponents<AudioSource>()[28].Pause();
    }

    public void UnpauseLevel2Music()
    {
        GetComponents<AudioSource>()[28].UnPause();
    }

    public void PauseLevel3Music()
    {
        GetComponents<AudioSource>()[29].Pause();
    }

    public void UnpauseLevel3Music()
    {
        GetComponents<AudioSource>()[29].UnPause();
    }

    public void PauseLevel4Music()
    {
        GetComponents<AudioSource>()[30].Pause();
    }

    public void UnpauseLevel4Music()
    {
        GetComponents<AudioSource>()[30].UnPause();
    }

    public void DoorUnlock()
    {
        GetComponents<AudioSource>()[31].Play();
    }

    public void GlassChip()
    {
        GetComponents<AudioSource>()[32].Play();
    }

    public void GlassBreak()
    {
        GetComponents<AudioSource>()[33].Play();
    }
}
