using System;
#if USN_USE_ASYNC_METHODS
using Cysharp.Threading.Tasks;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Sheet;
using UnityScreenNavigator.Runtime.Foundation.Coroutine;
using Screen = UnityScreenNavigator.Runtime.Core.Screen.Screen;

namespace Demo.Scripts
{
    public class ShopScreen : UnityScreenNavigator.Runtime.Core.Screen.Screen
    {
        private const int ItemGridSheetCount = 3;

        [SerializeField] private SheetContainer _itemGridContainer;
        [SerializeField] private Button[] _itemGridButtons;

        private readonly int[] _itemGridSheetIds = new int[ItemGridSheetCount];
#if USN_USE_ASYNC_METHODS
        public override async UniTask Initialize()
        {
            var registerHandles = new AsyncProcessHandle[ItemGridSheetCount];
            for (var i = 0; i < ItemGridSheetCount; i++)
            {
                var index = i;
                registerHandles[i] = _itemGridContainer.Register(ResourceKey.ShopItemGridSheetPrefab(), x =>
                {
                    var id = x.sheetId;
                    _itemGridSheetIds[index] = id;
                    var shopItemGrid = (ShopItemGridSheet)x.instance;
                    shopItemGrid.Setup(index, GetCharacterId(index));
                });
            }

            for (var i = 0; i < ItemGridSheetCount; i++)
            {
                var handle = registerHandles[i];
                while (!handle.IsTerminated)
                {
                    await UniTask.Yield();
                }
                
                var sheetId = _itemGridSheetIds[i];

                async void ShowSheet()
                {
                    if (_itemGridContainer.IsInTransition)
                    {
                        return;
                    }

                    if (_itemGridContainer.ActiveSheetId == sheetId)
                    {
                        // This sheet is already displayed.
                        return;
                    }

                    await _itemGridContainer.Show(sheetId, true);
                }

                _itemGridButtons[i].onClick.AddListener( ShowSheet);
            }

            await _itemGridContainer.Show(_itemGridSheetIds[0], false);
        }
#else
        public override IEnumerator Initialize()
        {
            var registerHandles = new AsyncProcessHandle[ItemGridSheetCount];
            for (var i = 0; i < ItemGridSheetCount; i++)
            {
                var index = i;
                registerHandles[i] = _itemGridContainer.Register(ResourceKey.ShopItemGridSheetPrefab(), x =>
                {
                    var id = x.sheetId;
                    _itemGridSheetIds[index] = id;
                    var shopItemGrid = (ShopItemGridSheet)x.instance;
                    shopItemGrid.Setup(index, GetCharacterId(index));
                });
            }

            for (var i = 0; i < ItemGridSheetCount; i++)
            {
                var handle = registerHandles[i];
                while (!handle.IsTerminated)
                {
                    yield return null;
                }
                
                var sheetId = _itemGridSheetIds[i];
                _itemGridButtons[i].onClick.AddListener(() =>
                {
                    if (_itemGridContainer.IsInTransition)
                    {
                        return;
                    }
                    if (_itemGridContainer.ActiveSheetId == sheetId)
                    {
                        // This sheet is already displayed.
                        return;
                    }
                    _itemGridContainer.Show(sheetId, true);
                });
            }

            _itemGridContainer.Show(_itemGridSheetIds[0], false);
        }
#endif
        private int GetCharacterId(int index)
        {
            switch (index)
            {
                case 0:
                    return 3;
                case 1:
                    return 4;
                case 2:
                    return 5;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}