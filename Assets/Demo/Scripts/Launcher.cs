using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityScreenNavigator.Runtime.Core.DynamicWindow;
using UnityScreenNavigator.Runtime.Core.Screen;
using UnityScreenNavigator.Runtime.Core.Shared.Layers;

namespace Demo.Scripts
{
    public class Launcher : MonoBehaviour
    {
        private GlobalContainerLayerManager _globalContainerLayerManager;
        private ScreenContainer _screenContainer;

        private async UniTaskVoid Start()
        {
            _globalContainerLayerManager = GetComponent<GlobalContainerLayerManager>();
            _screenContainer = _globalContainerLayerManager.Find<ScreenContainer>(ContainerKey.MainContainerLayer);
            await UniTask.Delay(1000);
            var option = new WindowOption(ResourceKey.TopPagePrefab(), true);
            _screenContainer.Push(option).Forget();
        }

        public void TestOpenDynamicWindow()
        {
            var option = new WindowOption("Prefabs/prefab_demo_hero_info.prefab", false, loadAsync: false);
            _globalContainerLayerManager.Find<DynamicWindowContainer>(ContainerKey.TutorialContainerLayer).Show(option);
        }

        public void HideDynamicWindow(string identifier)
        {
            _globalContainerLayerManager.Find<DynamicWindowContainer>(ContainerKey.TutorialContainerLayer)
                .Hide(identifier);
        }

        public void HideAllDynamicWindow()
        {
            _globalContainerLayerManager.Find<DynamicWindowContainer>(ContainerKey.TutorialContainerLayer)
                .HideAll(true);
        }

        public void DownloadAddressables()
        {
            var handle = Addressables.DownloadDependenciesAsync("", Addressables.MergeMode.None, true);
        }
    }
}