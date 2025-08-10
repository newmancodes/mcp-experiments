namespace PuzzleSolver.Haystack;

public record HaystackFindResult(string Needle, string Haystack, IEnumerable<int> Locations)
{
    public int Instances => Locations.Count();

    public static HaystackFindResult NotFound(string needle, string haystack)
    {
        return new HaystackFindResult(needle, haystack, []);   
    }
}