using AddressableAssets.Loaders;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Sheet;
using Cysharp.Threading.Tasks;

namespace Demo.Scripts
{
    public class CharacterModalImageSheet : Sheet
    {
        [SerializeField] private Image _image;
        private readonly IAssetsKeyLoader<Sprite> _loader = new AssetsKeyLoader<Sprite>();
        private int _characterId;
        private int _rank;

        public void Setup(int characterId, int rank)
        {
            _characterId = characterId;
            _rank = rank;
        }

        public override async UniTask WillEnter()
        {
            var handle = await _loader.LoadAssetAsync(ResourceKey.CharacterSprite(_characterId, _rank));
            _image.sprite = handle;
        }

        public override UniTask Cleanup()
        {
            _loader.UnloadAllAssets();
            return base.Cleanup();
        }
    }
}