namespace RosaBot.Core.Styles
{
    public static class TextStyle
    {
        public static string BlockedText(string text)
            => string.Format("```{0}```", text);
    }
}
