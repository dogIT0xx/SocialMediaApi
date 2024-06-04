namespace SocialMediaApi.Share.ResponseModels
{
    public sealed class GetPostByIdRes
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? AuthorId { get; set; }
        public string? MediaUrl { get; set; }
    }
}
