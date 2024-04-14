using UnityEngine;
using Zenject;

namespace Sources.Modules.Level.Scripts
{
    public class LevelRoot : MonoBehaviour
    {
        private LevelHandler _levelHandler;

        [Inject]
        public void Construct(LevelHandler levelHandler)
        {
            _levelHandler = levelHandler;
        }

        private void Start()
        {
            _levelHandler.Init();
        }
    }
}
