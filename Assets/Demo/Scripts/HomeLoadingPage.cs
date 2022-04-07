using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Shared.Views;

namespace Demo.Scripts
{
    public class HomeLoadingPage : Page
    {
        public override void DidPushEnter()
        {
            var pushOption = new PushWindowOption(ResourceKey.HomePagePrefab(), true);
            // Transition to "Home".
            PageContainer.Of(transform).Push(pushOption);
        }
    }
}