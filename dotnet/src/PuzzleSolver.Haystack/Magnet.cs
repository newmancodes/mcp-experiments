namespace PuzzleSolver.Haystack;

public class Magnet
{
    public HaystackFindResult Find(string needle, string haystack)
    {
        if (haystack.Length == 0)
        {
            // Needle cannot be found in an empty haystack
            return HaystackFindResult.NotFound(needle, haystack);
        }
        
        var locations = new List<int>();
        var index = 0;
        while ((index = haystack.IndexOf(needle, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            locations.Add(index);
            index += needle.Length; // Move past the current match, prevent overlapping matches
        }

        return new HaystackFindResult(needle, haystack, locations);
    }
}