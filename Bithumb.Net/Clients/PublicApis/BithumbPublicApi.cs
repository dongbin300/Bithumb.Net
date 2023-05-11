using Bithumb.Net.Converters;
using Bithumb.Net.Enums;
using Bithumb.Net.Objects.Models;
using Bithumb.Net.Objects.Models.ResponseModels;

namespace Bithumb.Net.Clients.PublicApis
{
    public class BithumbPublicApi : BaseClient
    {
        public BithumbPublicApi(HttpClient client) : base(client, "", "")
        {
        }

        /// <summary>
        /// 요청 당시 빗썸 거래소 가상자산 현재가 정보를 제공합니다.
        /// </summary>
        /// <param name="paymentCurrency">결제 통화(마켓), 입력값 : KRW 혹은 BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%ED%98%84%EC%9E%AC%EA%B0%80-%EC%A0%95%EB%B3%B4-%EC%A1%B0%ED%9A%8C-all"/>
        public async Task<BithumbResponse<BithumbTickers>> GetAllTickersAsync(BithumbPaymentCurrency paymentCurrency)
        {
            var endpoint = $"/public/ticker/ALL_{paymentCurrency}";
            return await GetBithumbAsync<BithumbResponse<BithumbTickers>>(Client, endpoint, null, new BithumbTickersConverter()).ConfigureAwait(false);
        }

        /// <summary>
        /// 요청 당시 빗썸 거래소 가상자산 현재가 정보를 제공합니다.
        /// </summary>
        /// <param name="paymentCurrency">결제 통화(마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="orderCurrency">주문 통화(코인), 기본값 : BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%ED%98%84%EC%9E%AC%EA%B0%80-%EC%A0%95%EB%B3%B4-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<BithumbCoin>> GetTickerAsync(BithumbPaymentCurrency paymentCurrency, string orderCurrency = "BTC")
        {
            var endpoint = $"/public/ticker/{orderCurrency}_{paymentCurrency}";
            return await GetBithumbAsync<BithumbResponse<BithumbCoin>>(Client, endpoint).ConfigureAwait(false);
        }

        /// <summary>
        /// 거래소 호가 정보를 제공합니다.
        /// </summary>
        /// <param name="paymentCurrency">결제 통화(마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="count">1~5 (기본값 : 5)</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%ED%98%B8%EA%B0%80-%EC%A0%95%EB%B3%B4-%EC%A1%B0%ED%9A%8C-all"/>
        public async Task<BithumbResponse<BithumbOrderbooks>> GetAllOrderbooksAsync(BithumbPaymentCurrency paymentCurrency, int count = 5)
        {
            var endpoint = $"/public/orderbook/ALL_{paymentCurrency}";
            var parameters = new Dictionary<string, string>()
            {
                { "count", count.ToString() }
            };

            return await GetBithumbAsync<BithumbResponse<BithumbOrderbooks>>(Client, endpoint, parameters, new BithumbOrderbooksConverter()).ConfigureAwait(false);
        }

        /// <summary>
        /// 거래소 호가 정보를 제공합니다.
        /// </summary>
        /// <param name="paymentCurrency">결제 통화(마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="orderCurrency">주문 통화(코인), 기본값 : BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%ED%98%B8%EA%B0%80-%EC%A0%95%EB%B3%B4-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<BithumbOrderbook>> GetOrderbookAsync(BithumbPaymentCurrency paymentCurrency, string orderCurrency = "BTC")
        {
            var endpoint = $"/public/orderbook/{orderCurrency}_{paymentCurrency}";
            return await GetBithumbAsync<BithumbResponse<BithumbOrderbook>>(Client, endpoint).ConfigureAwait(false);
        }

        /// <summary>
        /// 빗썸 거래소 가상자산 거래 체결 완료 내역을 제공합니다.
        /// </summary>
        /// <param name="paymentCurrency">결제 통화(마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="orderCurrency">주문 통화(코인), 기본값 : BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%B5%9C%EA%B7%BC-%EC%B2%B4%EA%B2%B0-%EB%82%B4%EC%97%AD"/>
        public async Task<BithumbResponse<IEnumerable<BithumbTransaction>>> GetTransactionHistoryAsync(BithumbPaymentCurrency paymentCurrency, string orderCurrency = "BTC")
        {
            var endpoint = $"/public/transaction_history/{orderCurrency}_{paymentCurrency}";
            return await GetBithumbAsync<BithumbResponse<IEnumerable<BithumbTransaction>>>(Client, endpoint).ConfigureAwait(false);
        }

        /// <summary>
        /// 가상 자산의 입/출금 현황 정보를 제공합니다.
        /// </summary>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%9E%85%EC%B6%9C%EA%B8%88-%EC%A7%80%EC%9B%90-%ED%98%84%ED%99%A9-all"/>
        public async Task<BithumbResponse<BithumbAssetStatuses>> GetAllAssetStatusAsync()
        {
            var endpoint = $"/public/assetsstatus/ALL";

            return await GetBithumbAsync<BithumbResponse<BithumbAssetStatuses>>(Client, endpoint, null, new BithumbAssetStatusesConverter()).ConfigureAwait(false);
        }

        /// <summary>
        /// 가상 자산의 입/출금 현황 정보를 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화(코인), 기본값 : BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%9E%85%EC%B6%9C%EA%B8%88-%EC%A7%80%EC%9B%90-%ED%98%84%ED%99%A9"/>
        public async Task<BithumbResponse<BithumbAssetStatus>> GetAssetStatusAsync(string orderCurrency = "BTC")
        {
            var endpoint = $"/public/assetsstatus/{orderCurrency}";
            return await GetBithumbAsync<BithumbResponse<BithumbAssetStatus>>(Client, endpoint).ConfigureAwait(false);
        }

        /// <summary>
        /// 빗썸 지수 (BTMI,BTAI) 정보를 제공합니다.
        /// </summary>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/btci-%EB%B9%97%EC%8D%B8%EC%A7%80%EC%88%98"/>
        public async Task<BithumbResponse<BithumbBtci>> GetBtciAsync()
        {
            var endpoint = $"/public/btci";
            return await GetBithumbAsync<BithumbResponse<BithumbBtci>>(Client, endpoint).ConfigureAwait(false);
        }
    }
}
