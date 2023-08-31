using System;
using System.Reactive.Disposables;
using Resto.Front.Api;
using Resto.Front.Api.Attributes;
using Resto.Front.Api.Attributes.JetBrains;
using Resto.Front.Api.Data.Common;
using Resto.Front.Api.Data.Orders;
using Resto.Front.Api.UI;

namespace Plugin
{
    [UsedImplicitly]
    [PluginLicenseModuleId(21016318)]
    public sealed class TestPlugin : IFrontPlugin
    {
        private readonly CompositeDisposable _subscriptions;

        public TestPlugin()
        {
            _subscriptions = new CompositeDisposable();
            PluginContext.Notifications.CafeSessionOpening.Subscribe(PluginStartingHandler);

            InitializePlugin();
        }

        private void PluginStartingHandler((IReceiptPrinter printer, IViewManager vm) obj)
        {
            obj.vm.ShowOkPopup("Плагин успешно запущен", "Запуск плагина");
        }

        private void InitializePlugin()
        {
            PluginContext.Operations.AddButtonToOrderEditScreen(
            "Количество добавленных позиций",
            OrderSectionButtonClick);

        }

        private void OrderSectionButtonClick((IOrder order, IOperationService os, IViewManager vm) obj)
        {
            obj.vm.ShowOkPopup($"Общее количество добавленных позиций {obj.order.Number}", "Количество позиций");
        }


        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}