using DG.Tweening;
using UnityEngine;

namespace Sources.Modules.Case.Scripts
{
    public class CaseHandler
    {
        private const float Duration = .5f;
        
        private readonly Transform _imageTransform;
        private readonly Vector3 _currentRotate;
        private readonly Quaternion _imageBaseTransformRotation;

        private Tween _loopTween;

        public CaseHandler(Transform imageTransform)
        {
            _imageTransform = imageTransform;
            _imageBaseTransformRotation = imageTransform.rotation;
            _currentRotate = new Vector3(0,0,12);
        }
        
        public void MouseEnter()
        {
            _loopTween = _imageTransform.DOLocalRotate(_currentRotate, Duration)
                .OnComplete(() =>
                {
                    _loopTween = _imageTransform.DOLocalRotate(-_currentRotate, Duration).SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.InOutSine);
                });
        }

        public void MouseExit()
        {
            _imageTransform.localRotation = _imageBaseTransformRotation;
            _loopTween.Kill();
        }
    }
}