using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;

    private void Start()
    {
        if (Saver.GetBool("Sounds", true))
        {
            _musicSource.Play();
        }
        else
        {
            _musicSource.Stop();
        }
    }
}
