﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Shared;
using UnityScreenNavigator.Runtime.Core.Shared.Views;
using UnityScreenNavigator.Runtime.Foundation;
using UnityScreenNavigator.Runtime.Foundation.Animation;
using UnityScreenNavigator.Runtime.Foundation.Coroutine;
using UnityScreenNavigator.Runtime.Foundation.PriorityCollection;

namespace UnityScreenNavigator.Runtime.Core.UnorderedModal
{
    public class UnorderedModal : Window, IWindowLifeCycleEvent
    {
        [SerializeField] private bool _usePrefabNameAsIdentifier = true;

        [SerializeField] [EnabledIf(nameof(_usePrefabNameAsIdentifier), false)]
        private string _identifier;

        private readonly PriorityList<IWindowLifeCycleEvent> _lifecycleEvents =
            new PriorityList<IWindowLifeCycleEvent>();

        [SerializeField]
        private ModalTransitionAnimationContainer _animationContainer = new ModalTransitionAnimationContainer();

        public ModalTransitionAnimationContainer AnimationContainer => _animationContainer;

#if USN_USE_ASYNC_METHODS
        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }
#else
        public virtual IEnumerator Initialize()
        {
            yield break;
        }
#endif
#if USN_USE_ASYNC_METHODS
        public virtual UniTask WillShowEnter()
        {
            return UniTask.CompletedTask;
        }
#else
        public virtual IEnumerator WillShowEnter()
        {
            yield break;
        }
#endif

        public void DidShowEnter()
        {
        }
#if USN_USE_ASYNC_METHODS
        public UniTask WillShowExit()
        {
            return UniTask.CompletedTask;
        }
#else
        public virtual IEnumerator WillShowExit()
        {
            yield break;
        }
#endif

        public void DidShowExit()
        {
        }
#if USN_USE_ASYNC_METHODS
        public UniTask WillHideEnter()
        {
            return UniTask.CompletedTask;
        }
#else
        public virtual IEnumerator WillHideEnter()
        {
            yield break;
        }
#endif
        public void DidHideEnter()
        {
        }
#if USN_USE_ASYNC_METHODS
        public UniTask WillHideExit()
        {
            return UniTask.CompletedTask;
        }
#else
        public virtual IEnumerator WillHideExit()
        {
            yield break;
        }
#endif
        public void DidHideExit()
        {
        }
#if USN_USE_ASYNC_METHODS
        public UniTask Cleanup()
        {
            return UniTask.CompletedTask;
        }
#else
        public virtual IEnumerator Cleanup()
        {
            yield break;
        }
#endif
        protected override void OnCreate(IBundle bundle)
        {
        }

        internal AsyncProcessHandle AfterLoad(RectTransform parentTransform)
        {
            _lifecycleEvents.Add(this, 0);
            _identifier = _usePrefabNameAsIdentifier ? gameObject.name.Replace("(Clone)", string.Empty) : _identifier;
            Parent = parentTransform;
            RectTransform.FillParent((RectTransform) Parent);
            Alpha = 0.0f;

            return CoroutineManager.Instance.Run(CreateCoroutine(_lifecycleEvents.Select(x => x.Initialize())));
        }
        internal AsyncProcessHandle BeforeEnter(bool push, UnorderedModal partnerModal)
        {
            return CoroutineManager.Instance.Run(BeforeEnterRoutine(push, partnerModal));
        }

        private IEnumerator BeforeEnterRoutine(bool push, UnorderedModal partnerModal)
        {
            if (push)
            {
                gameObject.SetActive(true);
                RectTransform.FillParent((RectTransform)Parent);
                //_canvasGroup.alpha = 0.0f;
                Alpha = 0.0f;
            }

            if (!UnityScreenNavigatorSettings.Instance.EnableInteractionInTransition)
            {
                //_canvasGroup.interactable = false;
                Interactable = false;
            }

            var routines = push
                ? _lifecycleEvents.Select(x => x.WillShowEnter())
                : _lifecycleEvents.Select(x => x.WillHideEnter());
            var handle = CoroutineManager.Instance.Run(CreateCoroutine(routines));

            while (!handle.IsTerminated)
            {
                yield return null;
            }
        }
        internal AsyncProcessHandle Enter(bool push, bool playAnimation, UnorderedModal partnerModal)
        {
            return CoroutineManager.Instance.Run(EnterRoutine(push, playAnimation, partnerModal));
        }

        private IEnumerator EnterRoutine(bool push, bool playAnimation, UnorderedModal partnerModal)
        {
            if (push)
            {
                Alpha = 1.0f;

                if (playAnimation)
                {
                    var anim = _animationContainer.GetAnimation(true, partnerModal?.Identifier);
                    if (anim == null)
                    {
                        anim = UnityScreenNavigatorSettings.Instance.GetDefaultModalTransitionAnimation(true);
                    }

                    anim.SetPartner(partnerModal?.transform as RectTransform);
                    anim.Setup(RectTransform);
                    yield return CoroutineManager.Instance.Run(anim.CreatePlayRoutine());
                }

                RectTransform.FillParent((RectTransform)Parent);
            }
        }
        internal void AfterEnter(bool show, UnorderedModal partnerModal)
        {
            if (show)
            {
                foreach (var lifecycleEvent in _lifecycleEvents)
                {
                    lifecycleEvent.DidShowEnter();
                }
            }
            else
            {
                foreach (var lifecycleEvent in _lifecycleEvents)
                {
                    lifecycleEvent.DidHideEnter();
                }
            }

            if (!UnityScreenNavigatorSettings.Instance.EnableInteractionInTransition)
            {
                Interactable = true;
            }
        }
        internal AsyncProcessHandle BeforeExit(bool push, UnorderedModal partnerModal)
        {
            return CoroutineManager.Instance.Run(BeforeExitRoutine(push, partnerModal));
        }
        private IEnumerator BeforeExitRoutine(bool push, UnorderedModal partnerModal)
        {
            if (!push)
            {
                gameObject.SetActive(true);
                RectTransform.FillParent((RectTransform)Parent);
                //_canvasGroup.alpha = 1.0f;
                Alpha = 1.0f;
            }

            if (!UnityScreenNavigatorSettings.Instance.EnableInteractionInTransition)
            {
                //_canvasGroup.interactable = false;
                Interactable = false;
            }

            var routines = push
                ? _lifecycleEvents.Select(x => x.WillShowExit())
                : _lifecycleEvents.Select(x => x.WillHideExit());
            var handle = CoroutineManager.Instance.Run(CreateCoroutine(routines));

            while (!handle.IsTerminated)
            {
                yield return null;
            }
        }
        internal AsyncProcessHandle Exit(bool push, bool playAnimation, UnorderedModal partnerModal)
        {
            return CoroutineManager.Instance.Run(ExitRoutine(push, playAnimation, partnerModal));
        }

        private IEnumerator ExitRoutine(bool push, bool playAnimation, UnorderedModal partnerModal)
        {
            if (!push)
            {
                if (playAnimation)
                {
                    var anim = _animationContainer.GetAnimation(false, partnerModal?._identifier);
                    if (anim == null)
                    {
                        anim = UnityScreenNavigatorSettings.Instance.GetDefaultModalTransitionAnimation(false);
                    }

                    anim.SetPartner(partnerModal?.transform as RectTransform);
                    anim.Setup(RectTransform);
                    yield return CoroutineManager.Instance.Run(anim.CreatePlayRoutine());
                }

                //_canvasGroup.alpha = 0.0f;
                Alpha = 0.0f;
            }
        }

        internal void AfterExit(bool push, UnorderedModal partnerModal)
        {
            if (push)
            {
                foreach (var lifecycleEvent in _lifecycleEvents)
                {
                    lifecycleEvent.DidShowExit();
                }
            }
            else
            {
                foreach (var lifecycleEvent in _lifecycleEvents)
                {
                    lifecycleEvent.DidHideExit();
                }
            }
        }

        internal AsyncProcessHandle BeforeRelease()
        {
            return CoroutineManager.Instance.Run(CreateCoroutine(_lifecycleEvents.Select(x => x.Cleanup())));
        }
#if USN_USE_ASYNC_METHODS
        private IEnumerator CreateCoroutine(IEnumerable<UniTask> targets)
#else
        private IEnumerator CreateCoroutine(IEnumerable<IEnumerator> targets)
#endif
        {
            foreach (var target in targets)
            {
                var handle = CoroutineManager.Instance.Run(CreateCoroutine(target));
                if (!handle.IsTerminated)
                {
                    yield return handle;
                }
            }
        }

#if USN_USE_ASYNC_METHODS
        private IEnumerator CreateCoroutine(UniTask target)
#else
        private IEnumerator CreateCoroutine(IEnumerator target)
#endif
        {
#if USN_USE_ASYNC_METHODS
            async void WaitTaskAndCallback(UniTask task, Action callback)
            {
                await task;
                callback?.Invoke();
            }

            var isCompleted = false;
            WaitTaskAndCallback(target, () => { isCompleted = true; });
            return new WaitUntil(() => isCompleted);
#else
            return target;
#endif
        }
    }
}