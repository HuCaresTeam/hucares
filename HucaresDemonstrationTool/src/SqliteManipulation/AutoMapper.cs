using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection;
using System.Data;

namespace SqliteManipulation
{
    public static class AutoMapper
    {
        public static IEnumerable<Target> MapToObjectEnumerable <Target>(this SQLiteDataReader dataReader) where Target : new()
        {
            var result = new List<Target>();

            var schemaTable = dataReader.GetSchemaTable();
            var targetType = typeof(Target);
            var mapNamesToProperties = new Dictionary<string, PropertyInfo>();
            foreach (DataRow row in schemaTable.Rows)
            {
                var columnName = row["ColumnName"].ToString();
                var propertyInfo = targetType.GetProperty(columnName, BindingFlags.Instance | BindingFlags.Public);
                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    mapNamesToProperties.Add(columnName, propertyInfo);
                }
            }

            while (dataReader.Read())
            {
                var targetObj = new Target();
                foreach (var namePropertyPair in mapNamesToProperties)
                {
                    namePropertyPair.Value.SetValue(targetObj, dataReader[namePropertyPair.Key]);
                }
                result.Add(targetObj);
            }

            return result;
        }
    }
}
