using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Shared.Views;

namespace Demo.Scripts
{
    public class RootPageManager : MonoBehaviour
    {
        [SerializeField] private PageContainer _pageContainer;

        private void Start()
        {
            var option = new PushWindowOption(ResourceKey.TopPagePrefab(), false, loadAsync: false);
            _pageContainer.Push(option);
        }
    }
}