using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.DynamicWindow;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Screen;
using UnityScreenNavigator.Runtime.Core.Shared;
using UnityScreenNavigator.Runtime.Core.Shared.Views;

namespace Demo.Scripts
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private ContainerLayerSettings containerLayerSettings;
        private GlobalContainerLayerManager _globalContainerLayerManager;
        
        private void Awake()
        {
            if(containerLayerSettings == null)
                throw new ArgumentNullException(nameof(containerLayerSettings));
            
            _globalContainerLayerManager = FindObjectOfType<GlobalContainerLayerManager>();
        }

        private IEnumerator Start()
        {
            var layers = containerLayerSettings.GetContainerLayers();
            for (var i = 0; i < layers.Length; i++)
            {
                switch (layers[i].layerType)
                {
                    case ContainerLayerType.Modal:
                        ModalContainer.Create(layers[i].name, layers[i].layer, layers[i].layerType);
                        break;
                    case ContainerLayerType.Screen:
                        ScreenContainer.Create(layers[i].name, layers[i].layer, layers[i].layerType);
                        break;
                    case ContainerLayerType.Dynamic:
                        DynamicWindowContainer.Create(layers[i].name, layers[i].layer, layers[i].layerType);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            yield return null;
            var option = new WindowOption(ResourceKey.TopPagePrefab(), false, loadAsync: false);
            _globalContainerLayerManager.Find<ScreenContainer>(ContainerKey.MainContainerLayer).Push(option);
            
        }
        [Button]
        private void TestOpenDynamicWindow()
        {
            var option = new WindowOption("Prefabs/prefab_demo_hero_info.prefab", false, loadAsync: false);
            _globalContainerLayerManager.Find<DynamicWindowContainer>(ContainerKey.TutorialContainerLayer).Show(option);
        }
    }
}