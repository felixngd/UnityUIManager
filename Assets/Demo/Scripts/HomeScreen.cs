using System;
#if USN_USE_ASYNC_METHODS
using Cysharp.Threading.Tasks;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Screen;
using UnityScreenNavigator.Runtime.Core.Shared.Views;
namespace Demo.Scripts
{
    public class HomeScreen : UnityScreenNavigator.Runtime.Core.Screen.Screen
    {
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _shopButton;
#if USN_USE_ASYNC_METHODS
        public override async UniTask Initialize()
        {
            _settingButton.onClick.AddListener(OnSettingButtonClicked);
            _shopButton.onClick.AddListener(OnShopButtonClicked);

            // Preload the "Shop" page prefab.
            await ScreenContainer.Of(transform).Preload(ResourceKey.ShopPagePrefab());
            // Simulate loading time.
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
        #else
        public override IEnumerator Initialize()
        {
            _settingButton.onClick.AddListener(OnSettingButtonClicked);
            _shopButton.onClick.AddListener(OnShopButtonClicked);

            // Preload the "Shop" page prefab.
            yield return PageContainer.Of(transform).Preload(ResourceKey.ShopPagePrefab());
            // Simulate loading time.
            yield return new WaitForSeconds(1.0f);
        }
#endif
#if USN_USE_ASYNC_METHODS
        public override UniTask Cleanup()
        {
            _settingButton.onClick.RemoveListener(OnSettingButtonClicked);
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
            ScreenContainer.Of(transform).ReleasePreloaded(ResourceKey.ShopPagePrefab());
            return UniTask.CompletedTask;
        }
#else
        public override IEnumerator Cleanup()
        {
            _settingButton.onClick.RemoveListener(OnSettingButtonClicked);
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
            PageContainer.Of(transform).ReleasePreloaded(ResourceKey.ShopPagePrefab());
            yield break;
        }
#endif
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