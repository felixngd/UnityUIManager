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
        private IEnumerator Start()
        {
            var layers = UnityScreenNavigatorSettings.Instance.GetContainerLayers();
            for (var i = 0; i < layers.Length; i++)
            {
                switch (layers[i].layerType)
                {
                    case ContainerLayerType.Modal:
                        ModalContainer.Create(layers[i].name, ResourceKey.SettingsModalPrefab());
                        break;
                    case ContainerLayerType.Page:
                        PageContainer.Create(layers[i].name, ResourceKey.HomePagePrefab());
                        break;
                    case ContainerLayerType.UnorderedModal:
                        UnorderedModalContainer.Create(ResourceKey.CharacterModalPrefab(), layers[i].name);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            yield return null;
        }
    }
}