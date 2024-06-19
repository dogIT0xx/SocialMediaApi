namespace SocialMediaApi.Share.RequestModels
{
    public sealed record PaginationReq
    {
        public int PageSize { get; init; } = 10;
        public int PageIndex { get; init; } = 0;
    }
}
