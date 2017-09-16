using System.Collections.Generic;

namespace InsuranceEngine.DTO.Utility.GridData
{
    public class GridDataResponseDTO<T>
    {

        public IEnumerable<T> GridRows { get; set; }

        public int ItemCount { get; set; }

    }
}
