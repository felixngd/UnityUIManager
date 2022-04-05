#if USN_USE_ASYNC_METHODS
using Cysharp.Threading.Tasks;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Demo.Scripts
{
    public class CharacterModalImageSheet : Sheet
    {
        [SerializeField] private Image _image;

        private int _characterId;
        private int _rank;

        public void Setup(int characterId, int rank)
        {
            _characterId = characterId;
            _rank = rank;
        }
#if USN_USE_ASYNC_METHODS
        public override async UniTask WillEnter()
        {
            var handle = Resources.LoadAsync<Sprite>(ResourceKey.CharacterSprite(_characterId, _rank));
            await handle;
            _image.sprite = (Sprite) handle.asset;
        }
#else
        public override IEnumerator WillEnter()
        {
            var handle = Resources.LoadAsync<Sprite>(ResourceKey.CharacterSprite(_characterId, _rank));
            yield return handle;
            _image.sprite = (Sprite) handle.asset;
        }
#endif
    }

}