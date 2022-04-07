using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Shared.Views;

namespace DemoDevelopment
{
    public class PageDevelopmentManager : MonoBehaviour
    {
        [SerializeField] private string _resourceKey;
        [SerializeField] private PageContainer _pageContainer;

        private void Start()
        {
            var pushOption = new PushWindowOption(_resourceKey, false);
            _pageContainer.Push(pushOption);
        }
    }
}
