using System.Globalization;
using System.Text;

namespace eMedSchedule.Domain.Extensions
{
    public static class StringExtension
    {
        public static string RemoveAccent(this string text)
        {
            return new string(text
                    .Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    .ToArray())
                    .Normalize(NormalizationForm.FormC);
        }
    }
}