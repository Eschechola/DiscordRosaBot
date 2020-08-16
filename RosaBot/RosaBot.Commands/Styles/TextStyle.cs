using System;

namespace RosaBot.Commands.Styles
{
    public static class TextStyle
    {
        public static string BlockedText(string text)
        {
            return String.Format("```{0}```", text);
        }
    }
}
