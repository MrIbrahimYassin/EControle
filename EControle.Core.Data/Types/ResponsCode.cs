namespace EControle.Core.Data.Types
{
    public enum ResponsCode
    {
        TheRequestWasBeSuccessfulToReply = 0,
        CannotConnectToModuleServiceApplication = -2000,
        HaveNotConnectedToModuleServiceApplicationYet = -2001,
        ReceiveDataFailed = -2004,
        TheReceivedDataIsEmpty = -2005,
        TheMeterIsNewAccount, TheFirstVendingMustBeInVendingOfficeOfSedc = -5001,
        TheCurrentTimeIsLessThanTheLastTimeOfGeneration = -5003,
        TheSgcOfCurrentMeterCannotBeFound = -5009,
        InvalidTheMeterNumberOrTheMeterCannotBeFound = -10006,
        TheTariffOfCustomerCannotBeFound = -10007,
        TheCustomerHasBeenCancelled = -10009,
        TheDetailsOfOperatorCannotBeFound = -10013,
        TheVendingServerCannotBeFoundOrTheVendingOfficeIsNotExtends = -10015,
        TheVendingServerIsInactiveInSedeSystem = -10016,
        TheMinTransactionAmountIsFoundTheAmountOfThisTransactionIsLessThanThisValue = -10020,
        InvalidChecksumVerificationOfInformation = -10023,
        TheLastVendingLineNeedToConfirmReversedOrRestored = -10024,
        TheLastVendingLineNeedToConfirmRefundOrRestored = -10025,
        TheBalanceOfAccountIsNotEnough = -10032,
        ThePaymentTypeIsNotFound = -10034,
        TheCustomerHasBeenBlockedToDoAnyTransactions = -10071,
        TheTransactionCannotBeFoundOrTheTransactionIsBelongToTheMeterNumberYouSent = -10078,
        TheTransactionCannotBeAllowedWithinTheIntervalTime = -10080,
        ConnectToVendingServerFailed = -10081,
        ChecksumFailed = -10082,
        DuplicatedTheTransactionIdInTheSystem = -10084,
        InvalidTheDataFromTheVendingServer = -10085,
        TheLastTransactionIsPurchasedByTp, ButNotCheckedByTp = -11001,
        TheCustomerHasMpu, CannotVendByTp = -30029,
        TheCustomerIsAutoClosed = -40000,
        TheLengthOfTransactionIdCannotBeMoreThan30 = -80000,
        NoPermissionToFinishThisFunction = -90000,
        YouHaveNoPermissionToVisitThisFunctionIfYouWantToUseThis, PleaseContactTheAdministrator = -90002,
        ValidateUserFailed = -90005


    }
}