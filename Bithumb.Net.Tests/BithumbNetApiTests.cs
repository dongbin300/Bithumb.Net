using Bithumb.Net.Clients;
using Bithumb.Net.Enums;

namespace Bithumb.Net.Tests
{
    public class Tests
    {
        BithumbClient client;

        [SetUp]
        public void Setup()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Gaten", "bithumb_api.txt");
            var keyData = File.ReadAllLines(path);
            var connectKey = keyData[0];
            var secretKey = keyData[1];

            client = new BithumbClient(connectKey, secretKey);
        }

        #region Candlestick API
        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task GetCandlesticksAsync(string orderCurrency)
        {
            try
            {
                Assert.That(await client.Candlestick.GetCandlesticksAsync(orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion

        #region Info API
        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task GetAccountAsync(string orderCurrency)
        {
            try
            {
                Assert.That(await client.Info.GetAccountAsync(orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task GetBalanceAsync(string currency)
        {
            try
            {
                Assert.That(await client.Info.GetBalanceAsync(currency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task GetAllBalancesAsync()
        {
            try
            {
                Assert.That(await client.Info.GetAllBalancesAsync(), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task GetWalletAddressAsync(string currency)
        {
            try
            {
                Assert.That(await client.Info.GetWalletAddressAsync(currency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task GetTradeAsync(string orderCurrency)
        {
            try
            {
                Assert.That(await client.Info.GetTradeAsync(orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task GetAllOrdersAsync(string orderCurrency)
        {
            try
            {
                Assert.That(await client.Info.GetAllOrdersAsync(orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("0123", "BTC")]
        [TestCase("0234", "ETH")]
        [TestCase("0789", "ADA")]
        public async Task GetOrderDetailAsync(string orderId, string orderCurrency)
        {
            try
            {
                Assert.That(await client.Info.GetOrderDetailAsync(orderId, orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task GetAllTransactionsAsync(string orderCurrency)
        {
            try
            {
                Assert.That(await client.Info.GetAllTransactionsAsync(orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion

        #region Trade API
        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task OrderAsync(string paymentCurrency)
        {
            try
            {
                Assert.That(await client.Trade.OrderAsync(paymentCurrency, BithumbPaymentCurrency.KRW, BithumbTransactionType.ask, 0.1m, 0.2m), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task OrderMarketBuyAsync(string orderCurrency)
        {
            try
            {
                Assert.That(await client.Trade.OrderMarketBuyAsync(orderCurrency, BithumbPaymentCurrency.KRW, 0.2m), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task OrderMarketSellAsync(string orderCurrency)
        {
            try
            {
                Assert.That(await client.Trade.OrderMarketSellAsync(orderCurrency, BithumbPaymentCurrency.KRW, 0.2m), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task OrderStopLimitAsync(string orderCurrency)
        {
            try
            {
                Assert.That(await client.Trade.OrderStopLimitAsync(orderCurrency, BithumbPaymentCurrency.KRW, BithumbTransactionType.ask, 0.1m, 0.1m, 0.2m), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC", "012345")]
        [TestCase("ETH", "485764")]
        [TestCase("ADA", "393785")]
        public async Task OrderCancelAsync(string orderCurrency, string orderId)
        {
            try
            {
                Assert.That(await client.Trade.OrderCancelAsync(orderCurrency, BithumbPaymentCurrency.KRW, Enums.BithumbTransactionType.ask, orderId), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task WithdrawalIndividualAsync(string currnecy)
        {
            try
            {
                Assert.That(await client.Trade.WithdrawalIndividualAsync("Binance", currnecy, "wallet_address", 0.2m, "È«±æµ¿", "HONG GILDONG"), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task WithdrawalCorporateAsync(string currnecy)
        {
            try
            {
                Assert.That(await client.Trade.WithdrawalCorporateAsync("api_key", "secret_key", "binance", currnecy, "wallet_address", 0.2m, "È«±æµ¿", "HONG GILDONG", "±è´ëÇ¥", "KIM DAEPYO"), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion

        #region Public API

        [TestCase(BithumbPaymentCurrency.KRW)]
        [TestCase(BithumbPaymentCurrency.BTC)]
        public async Task OrderAsync(BithumbPaymentCurrency paymentCurrency)
        {
            try
            {
                Assert.That(await client.Public.GetAllTickersAsync(paymentCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase(BithumbPaymentCurrency.KRW, "BTC")]
        [TestCase(BithumbPaymentCurrency.KRW, "ETH")]
        [TestCase(BithumbPaymentCurrency.BTC, "ADA")]
        public async Task GetTickerAsync(BithumbPaymentCurrency paymentCurrency, string orderCurrency)
        {
            try
            {
                Assert.That(await client.Public.GetTickerAsync(paymentCurrency, orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase(BithumbPaymentCurrency.KRW, 2)]
        [TestCase(BithumbPaymentCurrency.BTC, 3)]
        public async Task GetAllOrderbooksAsync(BithumbPaymentCurrency paymentCurrency, int count)
        {
            try
            {
                Assert.That(await client.Public.GetAllOrderbooksAsync(paymentCurrency, count), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase(BithumbPaymentCurrency.KRW, "BTC")]
        [TestCase(BithumbPaymentCurrency.KRW, "ETH")]
        [TestCase(BithumbPaymentCurrency.BTC, "ADA")]
        public async Task GetOrderbookAsync(BithumbPaymentCurrency paymentCurrency, string orderCurrency)
        {
            try
            {
                Assert.That(await client.Public.GetOrderbookAsync(paymentCurrency, orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase(BithumbPaymentCurrency.KRW, "BTC")]
        [TestCase(BithumbPaymentCurrency.KRW, "ETH")]
        [TestCase(BithumbPaymentCurrency.BTC, "ADA")]
        public async Task GetTransactionHistoryAsync(BithumbPaymentCurrency paymentCurrency, string orderCurrency)
        {
            try
            {
                Assert.That(await client.Public.GetTransactionHistoryAsync(paymentCurrency, orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task GetAllAssetStatusAsync()
        {
            try
            {
                Assert.That(await client.Public.GetAllAssetStatusAsync(), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("BTC")]
        [TestCase("ETH")]
        [TestCase("ADA")]
        public async Task GetAssetStatusAsync(string orderCurrency)
        {
            try
            {
                Assert.That(await client.Public.GetAssetStatusAsync(orderCurrency), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task GetBtciAsync()
        {
            try
            {
                Assert.That(await client.Public.GetBtciAsync(), Is.Not.Null);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion
    }
}