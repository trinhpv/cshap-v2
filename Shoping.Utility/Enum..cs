namespace Shopping.Utility
{
    public enum RoleType
    {
        User,
        Admin
    }
    public enum PaymentStatus
    {
        Pending,
        Paid
    }

    public enum TokenType
    {
        AccessToken,
        RefreshToken
    }

    public enum OrderType
    {
        Ascending,
        Descending
    }


    public enum ReportTimeRange
    {
        Daily,
        Monthly,
        Yearly
    }
}