using Bithumb.Net.Enums;
using Bithumb.Net.Objects.Models.ResponseModels;

namespace Bithumb.Net.Clients.PrivateApis
{
    public class BithumbTradeApi : BaseClient
    {
        public BithumbTradeApi(HttpClient client, string connectKey, string secretKey) : base(client, connectKey, secretKey)
        {
        }

        /// <summary>
        /// 지정가 매수/매도 등록 기능을 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="type">거래유형(bid : 매수 ask : 매도)</param>
        /// <param name="price">Currency 거래가</param>
        /// <param name="units">주문 수량, [최대 주문 금액] 50억 원</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%A7%80%EC%A0%95%EA%B0%80-%EC%A3%BC%EB%AC%B8%ED%95%98%EA%B8%B0"/>
        public async Task<BithumbTradeResponse> OrderAsync(string orderCurrency, BithumbPaymentCurrency paymentCurrency, BithumbTransactionType type, decimal price, decimal units)
        {
            var endpoint = "/trade/place";
            var parameters = new Dictionary<string, string>
            {
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() },
                { "units", units.ToString() },
                { "price", price.ToString() },
                { "type", type.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbTradeResponse>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 시장가 매수 기능을 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="units">코인 매수 수량, [최대 주문 금액] 10억 원</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%8B%9C%EC%9E%A5%EA%B0%80-%EB%A7%A4%EC%88%98%ED%95%98%EA%B8%B0"/>
        public async Task<BithumbTradeResponse> OrderMarketBuyAsync(string orderCurrency, BithumbPaymentCurrency paymentCurrency, decimal units)
        {
            var endpoint = "/trade/market_buy";
            var parameters = new Dictionary<string, string>
            {
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() },
                { "units", units.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbTradeResponse>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 시장가 매도 기능을 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="units">코인 매수 수량, [최대 주문 금액] 10억 원</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%8B%9C%EC%9E%A5%EA%B0%80-%EB%A7%A4%EB%8F%84%ED%95%98%EA%B8%B0"/>
        public async Task<BithumbTradeResponse> OrderMarketSellAsync(string orderCurrency, BithumbPaymentCurrency paymentCurrency, decimal units)
        {
            var endpoint = "/trade/market_sell";
            var parameters = new Dictionary<string, string>
            {
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() },
                { "units", units.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbTradeResponse>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 자동주문 매수 / 매도 등록 기능을 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="type">거래유형(bid : 매수 ask : 매도)</param>
        /// <param name="watchPrice">주문 접수가 진행되는 가격 (자동주문시)</param>
        /// <param name="price">Currency 거래가</param>
        /// <param name="units">주문 수량, [최대 주문 금액] 50억 원</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%9E%90%EB%8F%99-%EC%A3%BC%EB%AC%B8%ED%95%98%EA%B8%B0"/>
        public async Task<BithumbTradeResponse> OrderStopLimitAsync(string orderCurrency, BithumbPaymentCurrency paymentCurrency, BithumbTransactionType type, decimal watchPrice, decimal price, decimal units)
        {
            var endpoint = "/trade/stop_limit";
            var parameters = new Dictionary<string, string>
            {
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() },
                { "watch_price", watchPrice.ToString() },
                { "price", price.ToString() },
                { "units", units.ToString() },
                { "type", type.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbTradeResponse>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 등록된 매수/매도 주문 취소 기능을 제공합니다.
        /// <para>
        /// 주문 취소 결과에 따른 취소 완료 개수는 [거래 주문내역 상세 조회<see cref="BithumbInfoApi.GetOrderDetailAsync" />]를 통해 확인할 수 있습니다.
        /// </para>
        /// </summary>
        /// <param name="orderCurrency">주문 통화 (코인)</param>
        /// <param name="paymentCurrency">결제 통화 (마켓), 입력값 : KRW 혹은 BTC</param>
        /// <param name="type">거래유형(bid : 매수 ask : 매도)</param>
        /// <param name="orderId">매수/매도 주문 등록된 주문번호</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%A3%BC%EB%AC%B8-%EC%B7%A8%EC%86%8C%ED%95%98%EA%B8%B0"/>
        public async Task<BithumbTradeResponse> OrderCancelAsync(string orderCurrency, BithumbPaymentCurrency paymentCurrency, BithumbTransactionType type, string orderId)
        {
            var endpoint = "/trade/cancel";
            var parameters = new Dictionary<string, string>
            {
                { "type", type.ToString() },
                { "order_id", orderId },
                { "order_currency", orderCurrency },
                { "payment_currency", paymentCurrency.ToString() }
            };

            return await PostBithumbAuthorizationAsync<BithumbTradeResponse>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// ※ NEED TO MORE TEST
        /// <para>
        /// 개인 가상자산 출금 신청 기능을 제공합니다. (원화 출금은 API를 제공하지 않습니다.)
        /// </para>
        /// <para>
        /// 출금 거래소명은 영문으로 입력하시기 바랍니다. (대소문자 구분 없음)
        /// </para>
        /// </summary>
        /// <param name="exchangeName">출금 거래소명</param>
        /// <param name="currency">가상자산 영문 코드, 기본값 : BTC</param>
        /// <param name="address">코인 별 출금 주소(1차 주소)</param>
        /// <param name="units">출금하고자 하는 코인 수량
        /// <para><see href="https://apidocs.bithumb.com/reference/%EC%BD%94%EC%9D%B8-%EC%B6%9C%EA%B8%88-%EC%B5%9C%EC%86%8C%EC%88%98%EB%9F%89-%EC%95%88%EB%82%B4"/></para>
        /// </param>
        /// <param name="koreanName">개인 수취 정보_국문 성명</param>
        /// <param name="englishName">개인 수취 정보_영문 성명</param>
        /// <param name="destination">XRP 출금 시 Destination Tag, STEEM 출금 시 입금 메모</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%BD%94%EC%9D%B8-%EC%B6%9C%EA%B8%88%ED%95%98%EA%B8%B0-%EA%B0%9C%EC%9D%B8"/>
        public async Task<BithumbTradeResponse> WithdrawalIndividualAsync(string exchangeName, string currency, string address, decimal units, string koreanName, string englishName, string destination = "")
        {
            var endpoint = "/trade/btc_withdrawal";
            var parameters = new Dictionary<string, string>
            {
                { "units", units.ToString() },
                { "address", address.ToString() },
                { "destination", destination },
                { "currency", currency },
                { "exchange_name", exchangeName },
                { "cust_type_cd", "01" },
                { "ko_name", koreanName },
                { "en_name", englishName }
            };

            return await PostBithumbAuthorizationAsync<BithumbTradeResponse>(Client, endpoint, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// ※ NEED TO MORE TEST
        /// <para>
        /// 법인 가상자산 출금 신청 기능을 제공합니다. (원화 출금은 API를 제공하지 않습니다.)
        /// </para>
        /// <para>
        /// 출금 거래소명은 영문으로 입력하시기 바랍니다. (대소문자 구분 없음)
        /// </para>
        /// </summary>
        /// <param name="apiKey">사용자 API Key</param>
        /// <param name="secretKey">사용자 Secret Key</param>
        /// <param name="exchangeName">출금 거래소명</param>
        /// <param name="currency">가상자산 영문 코드, 기본값 : BTC</param>
        /// <param name="address">코인 별 출금 주소(1차 주소)</param>
        /// <param name="units">출금하고자 하는 코인 수량
        /// <para><see href="https://apidocs.bithumb.com/reference/%EC%BD%94%EC%9D%B8-%EC%B6%9C%EA%B8%88-%EC%B5%9C%EC%86%8C%EC%88%98%EB%9F%89-%EC%95%88%EB%82%B4"/></para>
        /// </param>
        /// <param name="koreanName">법인 수취 정보_국문 법인명</param>
        /// <param name="englishName">법인 수취 정보_영문 법인명</param>
        /// <param name="corporateRepresentativeKoreanName">법인 수취 정보_국문 법인 대표자명</param>
        /// <param name="corporateRepresentativeEnglishName">법인 수취 정보_영문 법인 대표자명</param>
        /// <param name="destination">XRP 출금 시 Destination Tag, STEEM 출금 시 입금 메모</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EC%BD%94%EC%9D%B8-%EC%B6%9C%EA%B8%88%ED%95%98%EA%B8%B0-%EB%B2%95%EC%9D%B8"/>
        public async Task<BithumbTradeResponse> WithdrawalCorporateAsync(string apiKey, string secretKey, string exchangeName, string currency, string address, decimal units, string koreanName, string englishName, string corporateRepresentativeKoreanName, string corporateRepresentativeEnglishName, string destination = "")
        {
            var endpoint = "/trade/btc_withdrawal";
            var parameters = new Dictionary<string, string>
            {
                { "apiKey", apiKey },
                { "secretKey", secretKey },
                { "units", units.ToString() },
                { "address", address.ToString() },
                { "destination", destination },
                { "currency", currency },
                { "exchange_name", exchangeName },
                { "cust_type_cd", "02" },
                { "corp_ko_name", koreanName },
                { "corp_en_name", englishName },
                { "corp_rep_ko_name", corporateRepresentativeKoreanName },
                { "corp_rep_en_name", corporateRepresentativeEnglishName }
            };

            return await PostBithumbAuthorizationAsync<BithumbTradeResponse>(Client, endpoint, parameters).ConfigureAwait(false);
        }
    }
}
