using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Shared.Views;

namespace Demo.Scripts
{
    public class TopPage : Page
    {
        [SerializeField] private Button _button;

        protected override void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        protected override void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnClick);
            }
        }

        private void OnClick()
        {
            var option = new PushWindowOption(ResourceKey.HomeLoadingPagePrefab(), true, false);
            PageContainer.Of(transform).Push(option);
        }
    }
}
