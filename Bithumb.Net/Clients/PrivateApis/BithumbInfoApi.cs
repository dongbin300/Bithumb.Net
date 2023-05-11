using Bithumb.Net.Converters;
using Bithumb.Net.Enums;
using Bithumb.Net.Extensions;
using Bithumb.Net.Objects.Models;
using Bithumb.Net.Objects.Models.ResponseModels;

namespace Bithumb.Net.Clients.PrivateApis
{
    public class BithumbInfoApi : BaseClient
    {
        public BithumbInfoApi(HttpClient client, string connectKey, string secretKey) : base(client, connectKey, secretKey)
        {
        }

        /// <summary>
        /// 회원 정보 및 코인 거래 수수료 정보를 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%ED%9A%8C%EC%9B%90-%EC%A0%95%EB%B3%B4-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<BithumbAccount>> GetAccountAsync(string orderCurrency, BithumbPaymentCurrency paymentCurrency = BithumbPaymentCurrency.KRW)
        {
            var endpoint = "/info/account";
            var parameters = new Dictionary<string, string>
            {
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbResponse<BithumbAccount>>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 회원이 보유한 자산 정보를 제공합니다.
        /// </summary>
        /// <param name="currency">가상자산 영문 코드, 기본값 : BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B3%B4%EC%9C%A0%EC%9E%90%EC%82%B0-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<BithumbBalance>> GetBalanceAsync(string currency = "BTC")
        {
            var endpoint = "/info/balance";
            var parameters = new Dictionary<string, string>
            {
                { "currency", currency }
            };

            return await PostBithumbAuthorizationAsync<BithumbResponse<BithumbBalance>>(Client, endpoint, parameters, new BithumbBalanceConverter()).ConfigureAwait(false);
        }

        /// <summary>
        /// 회원이 보유한 자산 정보를 제공합니다.
        /// </summary>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B3%B4%EC%9C%A0%EC%9E%90%EC%82%B0-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<BithumbBalance>> GetAllBalancesAsync()
        {
            var endpoint = "/info/balance";
            var parameters = new Dictionary<string, string>
            {
                { "currency", "ALL" }
            };

            return await PostBithumbAuthorizationAsync<BithumbResponse<BithumbBalance>>(Client, endpoint, parameters, new BithumbBalanceConverter()).ConfigureAwait(false);
        }

        /// <summary>
        /// 회원의 코인 입금 지갑 주소를 제공합니다.
        /// </summary>
        /// <param name="currency">가상자산 영문 코드, 기본값 : BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%9E%85%EA%B8%88%EC%A7%80%EA%B0%91-%EC%A3%BC%EC%86%8C-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<BithumbWalletAddress>> GetWalletAddressAsync(string currency = "BTC")
        {
            var endpoint = "/info/wallet_address";
            var parameters = new Dictionary<string, string>
            {
                { "currency", currency }
            };

            return await PostBithumbAuthorizationAsync<BithumbResponse<BithumbWalletAddress>>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 회원의 가상자산 거래 정보를 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%B5%9C%EA%B7%BC-%EA%B1%B0%EB%9E%98%EC%A0%95%EB%B3%B4-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<BithumbTrade>> GetTradeAsync(string orderCurrency, BithumbPaymentCurrency paymentCurrency = BithumbPaymentCurrency.KRW)
        {
            var endpoint = "/info/ticker";
            var parameters = new Dictionary<string, string>
            {
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbResponse<BithumbTrade>>(Client, endpoint, parameters).ConfigureAwait(false);
        }


        /// <summary>
        /// 회원의 매수/매도 등록 대기 또는 거래 중 내역 정보를 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="orderId">매수/매도 주문 등록된 주문번호(입력 시 해당 데이터만 추출)</param>
        /// <param name="type">거래유형(bid : 매수 ask : 매도)</param>
        /// <param name="count">1~1000(기본값 : 100)</param>
        /// <param name="after">입력한 시간보다 나중의 데이터 추출</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EA%B1%B0%EB%9E%98-%EC%A3%BC%EB%AC%B8%EB%82%B4%EC%97%AD-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<IEnumerable<BithumbOrder>>> GetAllOrdersAsync(string orderCurrency, BithumbPaymentCurrency paymentCurrency = BithumbPaymentCurrency.KRW, string orderId = "", BithumbTransactionType? type = null, int count = 100, DateTime? after = null)
        {
            var endpoint = "/info/orders";
            var parameters = new Dictionary<string, string>
            {
                { "order_id", orderId },
                { "type", type.EnumToString()},
                { "count", count.ToString() },
                { "after", after.ToTimestampMs().ToString() },
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbResponse<IEnumerable<BithumbOrder>>>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 회원의 매수/매도 체결 내역 상세 정보를 제공합니다.
        /// </summary>
        /// <param name="orderId">매수/매도 주문 등록된 주문번호(입력 시 해당 데이터만 추출)</param>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EA%B1%B0%EB%9E%98-%EC%A3%BC%EB%AC%B8%EB%82%B4%EC%97%AD-%EC%83%81%EC%84%B8-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<BithumbOrderDetail>> GetOrderDetailAsync(string orderId, string orderCurrency, BithumbPaymentCurrency paymentCurrency = BithumbPaymentCurrency.KRW)
        {
            var endpoint = "/info/order_detail";
            var parameters = new Dictionary<string, string>
            {
                { "order_id", orderId },
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbResponse<BithumbOrderDetail>>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 회원의 거래 완료 내역 정보를 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="offset">0~(기본값 : 0)</param>
        /// <param name="count">1~50(기본값 : 20)</param>
        /// <param name="searchGbType">0 : 전체, 1 : 매수 완료, 2 : 매도 완료, 3 : 출금 중, 4 : 입금, 5 : 출금, 9 : KRW 입금 중</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EA%B1%B0%EB%9E%98-%EC%B2%B4%EA%B2%B0%EB%82%B4%EC%97%AD-%EC%A1%B0%ED%9A%8C"/>
        public async Task<BithumbResponse<IEnumerable<BithumbUserTransaction>>> GetAllTransactionsAsync(string orderCurrency, BithumbPaymentCurrency paymentCurrency = BithumbPaymentCurrency.KRW, int offset = 0, int count = 20, BithumbSearchGbType searchGbType = BithumbSearchGbType.All)
        {
            var endpoint = "/info/user_transactions";
            var parameters = new Dictionary<string, string>
            {
                { "offset", offset.ToString() },
                { "count", count.ToString() },
                { "searchGb" , ((int)searchGbType).ToString() },
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbResponse<IEnumerable<BithumbUserTransaction>>>(Client, endpoint, parameters).ConfigureAwait(false);
        }
    }
}
