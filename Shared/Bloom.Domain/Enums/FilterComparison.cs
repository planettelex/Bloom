namespace Bloom.Domain.Enums
{
    /// <summary>
    /// Comparison expressions for filtering.
    /// </summary>
    public enum FilterComparison
    {
        None,
        Is,
        IsNot,
        BeginsWith,
        EndsWith,
        Contains,
        DoesNotContain, 
        IsBefore,
        IsAfter,
        IsInTheLast,
        IsNotInTheLast
    }
}
