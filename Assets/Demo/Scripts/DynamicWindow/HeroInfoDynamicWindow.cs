using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Demo.Scripts.DynamicWindow
{
    public class HeroInfoDynamicWindow : UnityScreenNavigator.Runtime.Core.DynamicWindow.DynamicWindow
    {
        [SerializeField] private Image heroImage;
        [SerializeField] private TMP_Text descriptionText;

        private readonly int _characterId = 3;

        public override async UniTask Initialize()
        {
            var key = ResourceKey.CharacterThumbnailSprite(_characterId, 1);
            heroImage.sprite = await AddressablesManager.LoadAssetAsync<Sprite>(key);
            heroImage.SetNativeSize();
            //lorem ipsum text
            descriptionText.text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam euismod, nisi vel consectetur interdum, nisl nunc egestas nisi, euismod aliquam nisl nunc euismod aliquam nisl nunc egestas nisi";
        }
    }
}