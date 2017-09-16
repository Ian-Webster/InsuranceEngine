using InsuranceEngine.DTO.Utility.GridData;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceEngine.Business.BusinessManagers.Utility
{
    public class GridHelper : BusinessManagerBase<GridHelper>
    {

        public GridDataRequestDTO ConvertTelerikDataSourceRequestToGridDataRequestDTO(DataSourceRequest telerikRequest, bool linqDataSource)
        {
            FilterDescriptor filterDescriptor = null;
            string value = string.Empty;
            GridDataRequestDTO result = new GridDataRequestDTO()
            {
                Filters = null,
                PageNumber = 0,
                PageSize = 10,
                Sort = null
            };

            result.PageNumber = telerikRequest.Page;
            if (telerikRequest.PageSize > 0)
            {
                result.PageSize = telerikRequest.PageSize;
            }

            //filters
            if (telerikRequest.Filters != null && telerikRequest.Filters.Count > 0)
            {
                //List<FilterDescriptor> filterDescriptors = new List<FilterDescriptor>();
                //filterDescriptors = telerikRequest.Filters.OfType<FilterDescriptor>().ToList();
                ////get composite filter descriptors
                //foreach (CompositeFilterDescriptor compositeDescriptor in telerikRequest.Filters.OfType<CompositeFilterDescriptor>())
                //{
                //    if (compositeDescriptor.FilterDescriptors != null)
                //    {
                //        filterDescriptors.AddRange(compositeDescriptor.FilterDescriptors.Cast<FilterDescriptor>());
                //    }                    
                //}

                List<FilterDescriptor> filterDescriptors = GetFilterDescriptors(telerikRequest.Filters);

                result.Filters = new List<FilterRequestDTO>();
                foreach (IFilterDescriptor filter in filterDescriptors)
                {
                    filterDescriptor = filter as FilterDescriptor;
                    if (filterDescriptor.Value.GetType() == typeof(DateTime))
                    {
                        if (linqDataSource)
                        {
                            value = ((DateTime)filterDescriptor.Value).ToString("yyyy, MM, dd");
                        }
                        else
                        {
                            value = ((DateTime)filterDescriptor.Value).ToString("yyyy-MM-dd");
                        }
                    }
                    else
                    {
                        value = string.Concat(filterDescriptor.Value.ToString().Take(20));  //only take the first 20 characters - reduces risk of sql injection attack
                    }
                    result.Filters.Add(new FilterRequestDTO()
                    {
                        FilterType = (FilterTypes)filterDescriptor.Operator,
                        PropertyName = filterDescriptor.Member,
                        PropertyValue = value,
                        DataType = filterDescriptor.Value.GetType()
                    });
                }
            }
            else
            {
                //create a blank list of filters
                result.Filters = null;
            }

            //sort order - only one column sorting is current supported
            if (telerikRequest.Sorts != null && telerikRequest.Sorts.Count > 0)
            {
                SortDescriptor sort = telerikRequest.Sorts.First();
                result.Sort = new SortRequestDTO()
                {
                    SortDirection = sort.SortDirection == System.ComponentModel.ListSortDirection.Ascending ? SortDirections.Ascending : SortDirections.Descending,
                    SortProperty = sort.Member
                };
            }
            else
            {
                //create some a sort
                result.Sort = null;
            }

            return result;
        }


        private List<FilterDescriptor> GetFilterDescriptors(IList<IFilterDescriptor> descriptors)
        {
            List<FilterDescriptor> result = new List<FilterDescriptor>();
            foreach (IFilterDescriptor descriptor in descriptors)
            {
                if (descriptor.GetType() == typeof(FilterDescriptor))
                {
                    result.Add(descriptor as FilterDescriptor);
                }
                else if (descriptor.GetType() == typeof(CompositeFilterDescriptor))
                {
                    result.AddRange(GetFilterDescriptors(((CompositeFilterDescriptor)descriptor).FilterDescriptors));
                }
            }
            return result;
        }

    }
}
