using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ReverseComparer
/// </summary>
public sealed class ReverseComparer<T> : IComparer<T>
{
    private readonly IComparer<T> original;

    public ReverseComparer(IComparer<T> original)
    {
        // TODO: Validation
        this.original = original;
    }

    public int Compare(T left, T right)
    {
        return original.Compare(right, left);
    }
}
