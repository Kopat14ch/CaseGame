using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Sound.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundRoot : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _audioClips;
        
        private SoundHandler _soundHandler;

        private void Awake()
        {
            _soundHandler = new SoundHandler(_audioClips, GetComponent<AudioSource>());
        }

        private void OnDestroy()
        {
            _soundHandler.Disable();
        }
    }
}
