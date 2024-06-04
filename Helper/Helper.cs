namespace SocialMediaApi
{
    public static class Helper
    {
        public static string ToSha256Hash(string input, string secretKey)
        {
            return string.Empty;
        }

        public static int ConvertToTimestamp(DateTime dateTime)
        {
            return dateTime.Subtract(DateTime.UnixEpoch).Seconds;
        }

        public static int GetUtcTimestampNow()
        {
            return Convert.ToInt32(DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds);
        }
    }
}
