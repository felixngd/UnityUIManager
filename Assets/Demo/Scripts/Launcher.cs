using System;
using System.Collections;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Shared;
using UnityScreenNavigator.Runtime.Core.Shared.Views;
using UnityScreenNavigator.Runtime.Core.UnorderedModal;

namespace Demo.Scripts
{
    public class Launcher : MonoBehaviour
    {
        private GlobalContainerLayerManager _globalContainerLayerManager;

        private void Awake()
        {
            _globalContainerLayerManager = FindObjectOfType<GlobalContainerLayerManager>();
        }

        private IEnumerator Start()
        {
            var layers = UnityScreenNavigatorSettings.Instance.GetContainerLayers();
            for (var i = 0; i < layers.Length; i++)
            {
                switch (layers[i].layerType)
                {
                    case ContainerLayerType.Modal:
                        ModalContainer.Create(layers[i].name, layers[i].layer, layers[i].layerType);
                        break;
                    case ContainerLayerType.Page:
                        PageContainer.Create(layers[i].name, layers[i].layer, layers[i].layerType);
                        break;
                    case ContainerLayerType.UnorderedModal:
                        UnorderedModalContainer.Create(layers[i].name, layers[i].layer, layers[i].layerType);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            yield return null;
            var option = new PushWindowOption(ResourceKey.TopPagePrefab(), false, loadAsync: false);
            _globalContainerLayerManager.Find<PageContainer>("MAIN_CONTAINER").Push(option);
        }
    }
}