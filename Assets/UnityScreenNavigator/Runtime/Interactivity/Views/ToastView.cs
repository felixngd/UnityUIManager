using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Shared.Views;
using UnityScreenNavigator.Runtime.Interactivity.Animation;

namespace UnityScreenNavigator.Runtime.Interactivity.Views
{
    public class ToastView : UIView
    {
#if USN_USE_TEXTMESHPRO
        [SerializeField] private TMPro.TextMeshProUGUI messageText;
        [SerializeField] private TMPro.TextMeshProUGUI hexCodeColorText;
#else
        [SerializeField] private Text messageText;
        [SerializeField] private Text hexCodeColorText;
#endif
        [SerializeField] private Image colorImage;
        [SerializeField] private InteractivityTransitionAnimationContainer transitionAnimationContainer;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMessage(string message)
        {
            messageText.text = message;
        }

        public void SetMessage(string message, Color color)
        {
            messageText.text = message;
            colorImage.color = color;
            hexCodeColorText.text = ColorUtility.ToHtmlStringRGB(color);
        }

        public UniTask PlayEnterAnimation(CancellationToken cancellationToken)
        {
            var enterAnimation = transitionAnimationContainer.GetAnimation(true);
            if (enterAnimation == null)
            {
                gameObject.SetActive(false);
                enterAnimation = SwitchTransitionAnimationObject.CreateInstance();
            }

            enterAnimation.Setup(RectTransform);
            return enterAnimation.Play(cancellationToken);
        }

        public UniTask PlayExitAnimation(CancellationToken cancellationToken)
        {
            var exitAnimation = transitionAnimationContainer.GetAnimation(false);
            if (exitAnimation == null)
            {
                gameObject.SetActive(false);
                exitAnimation = SwitchTransitionAnimationObject.CreateInstance();
            }

            exitAnimation.Setup(RectTransform);
            return exitAnimation.Play(cancellationToken);
        }

    }
}