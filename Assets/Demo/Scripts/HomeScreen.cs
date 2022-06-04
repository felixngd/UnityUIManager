using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Screen;
using UnityScreenNavigator.Runtime.Core.Shared;
using Screen = UnityScreenNavigator.Runtime.Core.Screen.Screen;

namespace Demo.Scripts
{
    public class HomeScreen : Screen
    {
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _shopButton;

        public override async UniTask Initialize()
        {
            _settingButton.onClick.AddListener(OnSettingButtonClicked);
            _shopButton.onClick.AddListener(OnShopButtonClicked);

            // Preload the "Shop" page prefab.
            await ScreenContainer.Of(transform).Preload(ResourceKey.ShopPagePrefab());
            // Simulate loading time.
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        public override UniTask Cleanup()
        {
            _settingButton.onClick.RemoveListener(OnSettingButtonClicked);
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
            ScreenContainer.Of(transform).ReleasePreloaded(ResourceKey.ShopPagePrefab());
            return UniTask.CompletedTask;
        }

        private void OnSettingButtonClicked()
        {
            var pushOption = new WindowOption(ResourceKey.SettingsModalPrefab(), true);
            ModalContainer.Find(ContainerKey.ModalContainerLayer).Push(pushOption);
        }

        private void OnShopButtonClicked()
        {
            var pushOption = new WindowOption(ResourceKey.ShopPagePrefab(), true);
            ScreenContainer.Of(transform).Push(pushOption);
        }
    }
}