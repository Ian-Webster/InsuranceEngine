using System.Collections.Generic;

namespace InsuranceEngine.DTO.Utility.GridData
{
    public class GridDataRequestDTO
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public List<FilterRequestDTO> Filters { get; set; }

        public SortRequestDTO Sort { get; set; }

    }
}
