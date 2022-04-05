#if USN_USE_ASYNC_METHODS
using Cysharp.Threading.Tasks;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace Demo.Scripts
{
    public class CharacterImageModal : Modal
    {
        [SerializeField] private Image _image;

        private int _characterId;
        private int _rank;

        public RectTransform ImageTransform => (RectTransform)_image.transform;

        public void Setup(int characterId, int rank)
        {
            _characterId = characterId;
            _rank = rank;
        }
#if USN_USE_ASYNC_METHODS
        public override async UniTask WillPushEnter()
        {
            var resourceKey = ResourceKey.CharacterSprite(_characterId, _rank);
            var handle = Resources.LoadAsync<Sprite>(resourceKey);
            await handle;
            var sprite = (Sprite) handle.asset;
            _image.sprite = sprite;
        }
#else
        public override IEnumerator WillPushEnter()
        {
            var resourceKey = ResourceKey.CharacterSprite(_characterId, _rank);
            var handle = Resources.LoadAsync<Sprite>(resourceKey);
            yield return handle;
            var sprite = (Sprite) handle.asset;
            _image.sprite = sprite;
        }
#endif

    }
}
