namespace Bithumb.Net.Enums
{
    public enum BithumbSearchGbType
    {
        /// <summary>
        /// 전체
        /// </summary>
        All = 0,

        /// <summary>
        /// 매수 완료
        /// </summary>
        BidComplete,

        /// <summary>
        /// 매도 완료
        /// </summary>
        AskComplete,

        /// <summary>
        /// 출금 중
        /// </summary>
        PendingWithdrawal,

        /// <summary>
        /// 입금
        /// </summary>
        Deposit,

        /// <summary>
        /// 출금
        /// </summary>
        Withdrawal,

        /// <summary>
        /// KRW 입금 중
        /// </summary>
        PendingDepositKrw = 9
    }
}
