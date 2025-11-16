using System.ComponentModel;
using ModelContextProtocol.Server;
using PuzzleSolver.Haystack;

namespace PuzzleSolver.MCPServer.Tools;

[McpServerToolType]
public static class HaystackTool
{
    [McpServerTool(Name = "needle-in-haystack-finder", Destructive = false, Idempotent = true, OpenWorld = false, ReadOnly = true)]
    [Description("Locates occurrences of a specified value, known as the needle, within a larger string, known as the haystack. The search does not count overlapping occurrences, so if the searched value (needle) is a palindrome, the end of one match will not be counted as the start of another potential match.")]
    public static string FindNeedleOccurrencesInHaystack(string needle, string haystack)
    {
        var magnet = new Magnet();
        var result = magnet.Find(needle, haystack);

        var formatter = new HaystackFindResultFormatter();
        return formatter.Format(result);
    }
}