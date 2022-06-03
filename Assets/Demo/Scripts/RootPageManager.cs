using UnityEngine;
using UnityEngine.Serialization;
using UnityScreenNavigator.Runtime.Core.DynamicWindow;
using UnityScreenNavigator.Runtime.Core.Screen;

namespace Demo.Scripts
{
    public class RootPageManager : MonoBehaviour
    {
        [FormerlySerializedAs("_pageContainer")] [SerializeField]
        private ScreenContainer screenContainer;

        private void Start()
        {
            var option = new WindowOption(ResourceKey.TopPagePrefab(), false, loadAsync: false);
            screenContainer.Push(option);
        }
    }
}