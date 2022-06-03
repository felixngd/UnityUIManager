using UnityScreenNavigator.Runtime.Core.DynamicWindow;
using UnityScreenNavigator.Runtime.Core.Screen;

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