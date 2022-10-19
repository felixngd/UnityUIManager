using AddressableAssets.Loaders;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;
using Cysharp.Threading.Tasks;

namespace Demo.Scripts
{
    public class CharacterImageModal : Modal
    {
        [SerializeField] private Image _image;
        private readonly IAssetsKeyLoader<Sprite> _loader = new AssetsKeyLoader<Sprite>();

        private int _characterId;
        private int _rank;

        public RectTransform ImageTransform => (RectTransform) _image.transform;

        public void Setup(int characterId, int rank)
        {
            _characterId = characterId;
            _rank = rank;
        }

        public override async UniTask WillPushEnter()
        {
            var resourceKey = ResourceKey.CharacterSprite(_characterId, _rank);
            var handle = await _loader.LoadAssetAsync(resourceKey);
            var sprite = handle;
            _image.sprite = sprite;
        }

        public override UniTask Cleanup()
        {
            _loader.UnloadAllAssets();
            return base.Cleanup();
        }
    }
}