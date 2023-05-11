using Bithumb.Net.Clients;
using Bithumb.Net.Enums;
using Bithumb.Net.Objects.Models;
using Bithumb.Net.Objects.Models.ResponseModels;

using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace Bithumb.Net.Examples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BithumbClient client = default!;
        BithumbSocketClient socketClient = default!;

        public MainWindow()
        {
            InitializeComponent();

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Gaten", "bithumb_api.txt");
            var keyData = File.ReadAllLines(path);
            var connectKey = keyData[0];
            var secretKey = keyData[1];

            client = new BithumbClient(connectKey, secretKey);
            socketClient = new BithumbSocketClient();
        }

        private async void SocketTickerSubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            await socketClient.Streams.SubscribeToTickerAsync( "BTC_KRW", BithumbSocketTickInterval.OneHour, OnMessage).ConfigureAwait(false);
        }

        private void SocketTickerUnsubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            socketClient.Streams.UnsubscribeToTicker();
        }

        private void OnMessage(BithumbWebSocketResponse<BithumbWebSocketTicker> obj)
        {
            var symbol = obj.content.symbol;
            var price = obj.content.closePrice;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, () =>
            {
                SocketTickerText.Text = $"{symbol} \\{price:#,###}";
            });
        }

        private void TickerGetButton_Click(object sender, RoutedEventArgs e)
        {
            var result = client.Public.GetTickerAsync(BithumbPaymentCurrency.KRW, "BTC");
            result.Wait();

            var price = result.Result.data?.closing_price;

            TickerText.Text = $"BTC : \\{price:#,###}";
        }
    }
}
