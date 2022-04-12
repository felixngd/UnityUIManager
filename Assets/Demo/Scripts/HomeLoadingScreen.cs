using UnityScreenNavigator.Runtime.Core.DynamicWindow;
using UnityScreenNavigator.Runtime.Core.Screen;
using UnityScreenNavigator.Runtime.Core.Shared.Views;

namespace Demo.Scripts
{
    public class HomeLoadingScreen : Screen
    {
        public override void DidPushEnter()
        {
            var pushOption = new WindowOption(ResourceKey.HomePagePrefab(), true);
            // Transition to "Home".
            ScreenContainer.Of(transform).Push(pushOption);
        }
    }
}