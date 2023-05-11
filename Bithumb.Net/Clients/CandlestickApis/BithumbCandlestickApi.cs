using Bithumb.Net.Converters;
using Bithumb.Net.Enums;
using Bithumb.Net.Extensions;
using Bithumb.Net.Objects.Models.ResponseModels;

namespace Bithumb.Net.Clients.CandlestickApis
{
    public class BithumbCandlestickApi : BaseClient
    {
        public BithumbCandlestickApi(HttpClient client) : base(client, "", "")
        {
        }

        /// <summary>
        /// REST API를 이용한 방식으로, Candlestick API는 시간 및 구간 별 빗썸 거래소 가상자산 가격, 거래량 정보를 제공합니다.
        /// </summary>
        /// <param name="orderCurrency">주문 통화(코인), 기본값 : BTC</param>
        /// <param name="paymentCurrency">결제 통화(마켓), 기본값 : KRW</param>
        /// <param name="interval">차트 간격, 기본값 : 24h {1m, 3m, 5m, 10m, 30m, 1h, 6h, 12h, 24h 사용 가능}</param>
        /// <returns></returns>
        public async Task<BithumbCandlestickResponse> GetCandlesticksAsync(string orderCurrency = "BTC", BithumbPaymentCurrency paymentCurrency = BithumbPaymentCurrency.KRW, BithumbInterval interval = BithumbInterval.OneDay)
        {
            var endpoint = $"/public/candlestick/{orderCurrency}_{paymentCurrency}/{interval.EnumToString()}";

            return await GetBithumbAsync<BithumbCandlestickResponse>(Client, endpoint, null, new BithumbCandlesticksConverter()).ConfigureAwait(false);
        }
    }
}
