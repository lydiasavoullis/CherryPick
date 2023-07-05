using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.group;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound: " + name +" not found!");
            return;
        }
        if (!s.source.isPlaying)
        {
            Debug.Log(name);
            s.source.Play();
        }  
    }
    public void PlayAtRandomTime(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        float delayTime = UnityEngine.Random.Range(0f, s.source.clip.length);
        s.source.time = delayTime;
        if (!s.source.isPlaying)
        {
            Debug.Log(name);

            s.source.Play();
        }
        else {
            s.source.Stop();
            s.source.Play();
        }
        
    }

    public void TypeSound(string name, float maxPitch, float minPitch)
    {
        Debug.Log(name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = (UnityEngine.Random.Range(minPitch, maxPitch));
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        if (!s.source.isPlaying)
        {
            //Debug.Log();
            s.source.Play();
        }
    }
    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            //Debug.Log("Sound: " + name + " not found!");
            return;
        }
        if (s.source.isPlaying) {
            Debug.Log(name + " stopped playing");
            StartCoroutine(StartFade(s.source, 4f, 0f));
        }
        
    }
    public void StopImmediate(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            //Debug.Log("Sound: " + name + " not found!");
            return;
        }
        if (s.source.isPlaying)
        {
            Debug.Log(name + " stopped playing");
            s.source.Stop();
        }

    }
    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        Debug.Log("Fade music");
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
        yield break;
    }

}
