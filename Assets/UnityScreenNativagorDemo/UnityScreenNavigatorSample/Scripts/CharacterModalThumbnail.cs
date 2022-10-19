using System;
using AddressableAssets.Loaders;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Demo.Scripts
{
    public class CharacterModalThumbnail : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        
        private readonly IAssetsKeyLoader<Sprite> _loader = new AssetsKeyLoader<Sprite>();

        private void Awake()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClicked);
            _loader.UnloadAllAssets();
        }

        public event Action Clicked;

        public async UniTask Setup(int id, int rank)
        {
            var sprite =
                await _loader.LoadAssetAsync(ResourceKey.CharacterThumbnailSprite(id, rank));
            _image.sprite = sprite;
        }

        private void OnClicked()
        {
            Clicked?.Invoke();
        }
    }
}