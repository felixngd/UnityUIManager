using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.DynamicWindow;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Screen;
using UnityScreenNavigator.Runtime.Core.Shared.Layers;

namespace Demo.Scripts
{
    public class LayerCreator : MonoBehaviour
    {
        [SerializeField] private ContainerLayerSettings containerLayerSettings;
        private void Awake()
        {
            InitializeLayers();
        }

        [Button]
        private void InitializeLayers()
        {
            var layers = containerLayerSettings.GetContainerLayers();
            for (var i = 0; i < layers.Length; i++)
            {
                switch (layers[i].layerType)
                {
                    case ContainerLayerType.Modal:
                        var modalContainer = ModalContainer.Create(layers[i].name, layers[i].layer, layers[i].layerType);
                        modalContainer.transform.SetParent(transform);
                        break;
                    case ContainerLayerType.Screen:
                        var screenContainer = ScreenContainer.Create(layers[i].name, layers[i].layer, layers[i].layerType);
                        screenContainer.transform.SetParent(transform);
                        break;
                    case ContainerLayerType.Dynamic:
                        var dynamicWindowContainer = DynamicWindowContainer.Create(layers[i].name, layers[i].layer, layers[i].layerType);
                        dynamicWindowContainer.transform.SetParent(transform);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}