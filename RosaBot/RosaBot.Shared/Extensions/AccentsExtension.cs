using System.Globalization;
using System.Text;

namespace RosaBot.Shared.Extensions
{
    public static class AccentsExtension
    {
        public static string RemoveAccents(this string text)
        {
            text = new string(
                text
                    .Normalize(NormalizationForm.FormD)
                    .ToCharArray()
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    .ToArray())
            .Normalize(NormalizationForm.FormC);

            return text.ToLower();
        }
    }
}
