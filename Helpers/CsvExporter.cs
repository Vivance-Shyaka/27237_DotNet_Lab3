using System.Data;
using System.Text;

namespace WLAMS.Helpers;

public static class CsvExporter
{
    // Saves a table as a CSV file that opens in Excel
    public static void Save(DataTable table, string path)
    {
        var sb = new StringBuilder();

        sb.AppendLine(string.Join(",",
            table.Columns.Cast<DataColumn>().Select(c => Escape(c.ColumnName))));

        foreach (DataRow row in table.Rows)
            sb.AppendLine(string.Join(",",
                row.ItemArray.Select(v => Escape(Convert.ToString(v) ?? ""))));

        File.WriteAllText(path, sb.ToString(), new UTF8Encoding(true));
    }

    private static string Escape(string value)
    {
        if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        return value;
    }
}