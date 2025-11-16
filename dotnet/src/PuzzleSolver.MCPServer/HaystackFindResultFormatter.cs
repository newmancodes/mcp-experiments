using System.Text;
using Humanizer;
using PuzzleSolver.Common;
using PuzzleSolver.Haystack;

namespace PuzzleSolver.MCPServer;

internal class HaystackFindResultFormatter : IFormatter<HaystackFindResult>
{
    public string Format(HaystackFindResult value)
    {
        var sb = new StringBuilder();
        sb.Append("I have searched for the value '");
        sb.Append(value.Needle);
        sb.Append("' in the value '");
        sb.Append(value.Haystack);
        sb.Append("'. I do not count overlapping occurrences, so if the searched for value is a palindrome, the end of one match will not be counted as the start of another potential match. ");
        sb.Append("I was ");
        if (value.Instances == 0)
        {
            sb.Append("not ");
        }
        sb.Append("able to find the requested value");
        if (value.Instances > 0)
        {
            sb.Append(' ');
            sb.Append(value.Instances.ToWords());
            sb.Append("time".ToQuantity(value.Instances, ShowQuantityAs.None));
        }
        sb.AppendLine(".");
        sb.AppendLine();

        if (value.Instances > 0)
        {
            sb.AppendLine("The value was found at the following locations show below.");
            sb.AppendLine();
            sb.AppendLine("| Location | Highlighted Value |");
            sb.AppendLine("|-|-|");

            foreach (var location in value.Locations)
            {
                sb.Append("| ");
                sb.Append(location);
                sb.Append(" | ");
                sb.Append(value.Haystack.Insert(location + value.Needle.Length, "**").Insert(location, "**"));
                sb.AppendLine("|");
            }

            sb.AppendLine();
            sb.AppendLine("**Note:** The locations are zero-based, so the first character in the string is at location 0.");
        }

        return sb.ToString();
    }
}