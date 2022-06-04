using UnityEngine;
using UnityEngine.Serialization;
using UnityScreenNavigator.Runtime.Core.Screen;
using UnityScreenNavigator.Runtime.Core.Shared;

namespace DemoDevelopment
{
    public class PageDevelopmentManager : MonoBehaviour
    {
        [SerializeField] private string _resourceKey;
        [FormerlySerializedAs("_pageContainer")] [SerializeField] private ScreenContainer screenContainer;

        private void Start()
        {
            var pushOption = new WindowOption(_resourceKey, false);
            screenContainer.Push(pushOption);
        }
    }
}
