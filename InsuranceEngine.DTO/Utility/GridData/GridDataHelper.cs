using InsuranceEngine.DTO.Utility.GridData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InsuranceEngine.Data.DTO.GridData
{
    public class GridDataHelper
    {

        private static List<string> sqlCheckList = new List<string>() 
                                      { "--",
                                        ";--",
                                        ";",
                                        "/*",
                                        "*/",
                                        "@@",
                                        "@",
                                        "char",
                                        "nchar",
                                        "varchar",
                                        "nvarchar",
                                        "alter",
                                        "begin",
                                        "cast",
                                        "create",
                                        "cursor",
                                        "declare",
                                        "delete",
                                        "drop",
                                        "end",
                                        "exec",
                                        "execute",
                                        "fetch",
                                        "insert",
                                        "kill",
                                        "select",
                                        "sys.",
                                        "sysobjects",
                                        "syscolumns",
                                        "table",
                                        "update",
                                        "sys.allocation_units",
                                        "sys.objects",
                                        "sys.assembly_modules",
                                        "sys.parameters",
                                        "sys.check_constraints",
                                        "sys.partitions",
                                        "sys.columns",
                                        "sys.procedures",
                                        "sys.computed_columns",
                                        "sys.sequences",
                                        "sys.default_constraints",
                                        "sys.service_queues",
                                        "sys.events",
                                        "sys.sql_dependencies",
                                        "sys.event_notifications",
                                        "sys.sql_expression_dependencies",
                                        "sys.extended_procedures",
                                        "sys.sql_modules",
                                        "sys.foreign_key_columns",
                                        "sys.stats",
                                        "sys.foreign_keys",
                                        "sys.stats_columns",
                                        "sys.function_order_columns",
                                        "sys.synonyms",
                                        "sys.hash_indexes",
                                        "sys.table_types",
                                        "sys.identity_columns",
                                        "sys.tables",
                                        "sys.index_columns",
                                        "sys.trigger_event_types",
                                        "sys.indexes",
                                        "sys.trigger_events",
                                        "sys.key_constraints",
                                        "sys.triggers",
                                        "sys.numbered_procedure_parameters",
                                        "sys.views",
                                        "sys.numbered_procedures"};

        public static string CleanSQLCommandsFromString(string stringToClean)
        {
            //start by replacing ' with ''
            stringToClean = stringToClean.Replace("'", "''");
            //now check if we have any sql commands in the text            
            stringToClean = Regex.Replace(stringToClean, @"\b(ALTER|CREATE|SET|DELETE|DROP|rename|truncate|backup|restore|from|table|union|end|case|DBCC|where|sysobjects|insert|or|database|declare|fetch|while|begin|EXEC(UTE){0,1}|INSERT( +INTO){0,1}|MERGE|SELECT|UPDATE|UNION( +ALL){0,1})\b", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase); // removes sql commands            
            stringToClean = Regex.Replace(stringToClean, @"sys\.[a-z,_]+", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase); // removes any sys. strings (e.g. sys.tables)            
            stringToClean = Regex.Replace(stringToClean, @"0x[a-z, 0-9]+", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase); // removes any hex statements
            stringToClean = Regex.Replace(stringToClean, "-{2,}", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase); // transforms multiple --- in - use to comment in sql scripts            
            stringToClean = Regex.Replace(stringToClean, @"[*]+", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase); // removes * used also to comment in sql scripts
            //now ditch any no alpha numeric fields - in case we missed anything
            stringToClean = CleanInvalidCharacters(stringToClean);

            return stringToClean;
        }

        public static string CleanInvalidCharacters(string stringToClean)
        {
            //remove all invalid characters
            char[] arr = stringToClean.ToCharArray();

            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c)
                                              || char.IsWhiteSpace(c)
                                              || c == '-'
                                              || c == '/'
                                              || c == '.'
                                              || c == '&'
                                              || c == ',')));
            stringToClean = new string(arr);


            return stringToClean;
        }

        public static string GetWhereClauseForRequest(GridDataRequestDTO requestDTO, bool forLinq)
        {
            StringBuilder result = new StringBuilder();
            if (requestDTO.Filters.Count > 0)
            {
                foreach (FilterRequestDTO filterDTO in requestDTO.Filters)
                {
                    filterDTO.PropertyValue = CleanSQLCommandsFromString(filterDTO.PropertyValue);
                    if (requestDTO.Filters.IndexOf(filterDTO) > 0)
                    {
                        filterDTO.PropertyValue = CleanSQLCommandsFromString(filterDTO.PropertyValue);
                        //need to add an "AND"
                        if (forLinq)
                        {
                            result.AppendLine("&& " + GetWhereClauseForFilter(filterDTO, forLinq));
                        }
                        else
                        {
                            result.AppendLine("AND " + GetWhereClauseForFilter(filterDTO, forLinq));
                        }
                    }
                    else
                    {
                        result.Append(GetWhereClauseForFilter(filterDTO, forLinq));
                    }
                }
            }


            return result.ToString();
        }

        private static StringBuilder GetWhereClauseForFilter(FilterRequestDTO filterDTO, bool forLinq)
        {
            StringBuilder current = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(filterDTO.PropertyName))
            {
                switch (filterDTO.FilterType)
                {
                    case FilterTypes.IsLessThan:
                        current.Append(filterDTO.PropertyName + " <");
                        if (forLinq)
                        {
                            current.Append(GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(GetPropertyValueForSQL(filterDTO, null, null));
                        }
                        break;
                    case FilterTypes.IsLessThanOrEqualTo:
                        current.Append(filterDTO.PropertyName + " <=");
                        if (forLinq)
                        {
                            current.Append(GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(GetPropertyValueForSQL(filterDTO, null, null));
                        }
                        break;
                    case FilterTypes.IsEqualTo:
                        if (forLinq)
                        {
                            current.Append(filterDTO.PropertyName + " ==");
                            current.Append(GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(filterDTO.PropertyName + " =");
                            current.Append(GetPropertyValueForSQL(filterDTO, null, null));
                        }
                        break;
                    case FilterTypes.IsNotEqualTo:
                        if (forLinq)
                        {
                            current.Append(filterDTO.PropertyName + " !=");
                            current.Append(GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(filterDTO.PropertyName + " <>");
                            current.Append(GetPropertyValueForSQL(filterDTO, null, null));
                        }
                        break;
                    case FilterTypes.IsGreaterThanOrEqualTo:
                        current.Append(filterDTO.PropertyName + " >=");
                        if (forLinq)
                        {
                            current.Append(GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(GetPropertyValueForSQL(filterDTO, null, null));
                        }
                        break;
                    case FilterTypes.IsGreaterThan:
                        current.Append(filterDTO.PropertyName + " >");
                        if (forLinq)
                        {
                            current.Append(GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(GetPropertyValueForSQL(filterDTO, null, null));
                        }
                        break;
                    case FilterTypes.StartsWith:
                        current.Append(filterDTO.PropertyName);
                        if (forLinq)
                        {
                            current.AppendFormat(".StartsWith({0})", GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(GetPropertyValueForSQL(filterDTO, " like '", "%'"));
                        }
                        break;
                    case FilterTypes.EndsWith:
                        current.Append(filterDTO.PropertyName);
                        if (forLinq)
                        {
                            current.AppendFormat(".EndsWith({0})", GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(GetPropertyValueForSQL(filterDTO, " like '%", "'"));
                        }
                        break;
                    case FilterTypes.Contains:
                        current.Append(filterDTO.PropertyName);
                        if (forLinq)
                        {
                            current.AppendFormat(".Contains({0})", GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(GetPropertyValueForSQL(filterDTO, " like '%", "%'"));
                        }
                        break;
                    case FilterTypes.IsContainedIn:
                        throw new Exception("unknown filter type"); //not sure how to do this one!!!
                    case FilterTypes.DoesNotContain:
                        if (forLinq)
                        {
                            current.Append("!" + filterDTO.PropertyName);
                            current.AppendFormat(".Contains({0})", GetPropertyValue(filterDTO));
                        }
                        else
                        {
                            current.Append(filterDTO.PropertyName);
                            current.Append(GetPropertyValueForSQL(filterDTO, " not like '%", "%'"));
                        }
                        break;
                    default:
                        throw new Exception("unknown filter type");
                }

                current.Append(" ");
            }

            return current;
        }

        private static StringBuilder GetPropertyValue(FilterRequestDTO filterDTO)
        {
            StringBuilder current = new StringBuilder();
            if (filterDTO.DataType == typeof(string))
            {
                current.Append(@"""" + filterDTO.PropertyValue + @""" ");
            }
            else if (filterDTO.DataType == typeof(DateTime))
            {
                current.Append(@"DateTime(" + filterDTO.PropertyValue + @") ");
            }
            else if (filterDTO.DataType == typeof(bool)
                     || filterDTO.DataType == typeof(int)
                     || filterDTO.DataType == typeof(byte)
                     || filterDTO.DataType == typeof(double)
                     || filterDTO.DataType == typeof(decimal))
            {
                current.Append(filterDTO.PropertyValue);
            }
            return current;
        }

        private static StringBuilder GetPropertyValueForSQL(FilterRequestDTO filterDTO, string prependValue, string postpendValue)
        {
            StringBuilder current = new StringBuilder();
            if (!string.IsNullOrEmpty(prependValue))
            {
                current.Append(prependValue);
            }
            else if (filterDTO.DataType == typeof(string) || filterDTO.DataType == typeof(DateTime))
            {
                current.Append(@"'");
            }
            if (filterDTO.DataType == typeof(bool))
            {
                if (filterDTO.PropertyValue.ToLower() == "true")
                {
                    current.Append('1');
                }
                else if (filterDTO.PropertyValue.ToLower() == "false")
                {
                    current.Append('0');
                }
                else
                {
                    current.Append(filterDTO.PropertyValue);
                }
            }
            else
            {
                current.Append(filterDTO.PropertyValue);
            }
            if (!string.IsNullOrEmpty(postpendValue))
            {
                current.Append(postpendValue + " ");
            }
            else if (filterDTO.DataType == typeof(string) || filterDTO.DataType == typeof(DateTime))
            {
                current.Append(@"' ");
            }
            return current;
        }

        public static string GetSortClauseForRequest(GridDataRequestDTO requestDTO)
        {
            StringBuilder result = new StringBuilder();
            if (requestDTO.Sort.SortDirection == SortDirections.Descending)
            {
                result.Append(requestDTO.Sort.SortProperty + " desc");
            }
            else
            {
                result.Append(requestDTO.Sort.SortProperty);
            }
            return result.ToString();
        }

    }
}
