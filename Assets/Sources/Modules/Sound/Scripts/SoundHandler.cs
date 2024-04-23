using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.Modules.Sound.Scripts
{
    public sealed class SoundHandler
    {
        private readonly List<AudioClip> _audioClips;
        private readonly AudioSource _audioSource;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public SoundHandler(List<AudioClip> audioClips, AudioSource audioSource)
        {
            _audioClips = audioClips;
            _audioSource = audioSource;
            _cancellationTokenSource = new CancellationTokenSource();
            
            Play(_cancellationTokenSource.Token).Forget();
        }
        
        public void Disable()
        {
            _cancellationTokenSource?.Cancel();
        }
        
        private async UniTask Play(CancellationToken token)
        {
            int currentIndexAudioClip = 0;
            
            while (token.IsCancellationRequested == false)
            {
                _audioSource.clip = _audioClips[currentIndexAudioClip];
                _audioSource.Play();
                currentIndexAudioClip++;
                currentIndexAudioClip %= _audioClips.Count;
                
                await UniTask.WaitUntil(() => _audioSource.isPlaying == false, cancellationToken: token);
            }
        }
    }
}