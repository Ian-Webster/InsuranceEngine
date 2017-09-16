namespace InsuranceEngine.DTO.Utility.GridData
{
    public enum FilterTypes
    {
        IsLessThan = 0,
        IsLessThanOrEqualTo = 1,
        IsEqualTo = 2,
        IsNotEqualTo = 3,
        IsGreaterThanOrEqualTo = 4,
        IsGreaterThan = 5,
        StartsWith = 6,
        EndsWith = 7,
        Contains = 8,
        IsContainedIn = 9,
        DoesNotContain = 10
    }

    public enum SortDirections
    {
        Ascending,
        Descending
    }
}