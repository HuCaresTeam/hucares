using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection;
using System.Data;
using System;
using System.Globalization;
using Utils;

namespace SqliteManipulation
{
    public static class AutoMapper
    {
        public static IEnumerable<Target> MapToObjectEnumerable <Target>(this DataSet dataSet) where Target : new()
        {
            var result = new List<Target>();

            var table = dataSet.Tables[0];
            var targetType = typeof(Target);
            var mapNamesToProperties = new Dictionary<string, PropertyInfo>();
            foreach (DataColumn column in table.Columns)
            {
                var propertyInfo = targetType.GetProperty(column.ColumnName, BindingFlags.Instance | BindingFlags.Public);
                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    mapNamesToProperties.Add(column.ColumnName, propertyInfo);
                }
            }

            foreach (DataRow row in table.Rows)
            {
                var targetObj = new Target();
                foreach (var namePropertyPair in mapNamesToProperties)
                {
                    var columnValue = row[namePropertyPair.Key];
                    if (typeof(DateTime).Name == namePropertyPair.Value.PropertyType.Name)
                    {
                        var convertedTime = columnValue.ToString().ToIsoDateTime();
                        namePropertyPair.Value.SetValue(targetObj, convertedTime);
                    }
                    else
                    {
                        namePropertyPair.Value.SetValue(targetObj, columnValue);
                    }
                }
                result.Add(targetObj);
            }

            return result;
        }
    }
}
