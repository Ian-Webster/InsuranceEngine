using System;

namespace InsuranceEngine.DTO.Utility.GridData
{
    public class FilterRequestDTO
    {
        public FilterTypes FilterType { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public Type DataType { get; set; }
    }
}
