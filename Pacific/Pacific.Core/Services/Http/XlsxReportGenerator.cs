using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Pacific.Core.Services.Orm;
using Pacific.ORM.HelpModels;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pacific.Core.Services.Http
{
	public class XlsxReportGenerator : IReportGenerator
	{
		private OrmService _ormService;

		public XlsxReportGenerator(OrmService ormService)
		{
			this._ormService = ormService;
		}


		public async Task<byte[]> GenerateReportAsync(ReportOptions options)
		{
            var orders = await this._ormService.SelectOrdersAsync(options);

            using (SpreadsheetDocument document = SpreadsheetDocument.Create("document.xlsx", SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Columns lstColumns = worksheetPart.Worksheet.GetFirstChild<Columns>();
                if (lstColumns == null)
                {
                    lstColumns = new Columns();
                }
                lstColumns.Append(new Column() { Min = 1, Max = 10, Width = 15, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 2, Max = 10, Width = 5, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 3, Max = 10, Width = 30, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 4, Max = 10, Width = 25, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 5, Max = 10, Width = 20, CustomWidth = true });
                worksheetPart.Worksheet.InsertAt(lstColumns, 0);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Somebody, help me pls...",
                };

                sheets.Append(sheet);

                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                var orderList = orders.ToList();
                Row row;
                for (int i = 0; i < orderList.Count; i++)
                {
                    row = new Row { RowIndex = (uint)(i + 1) };
                    InsertCell(row, 1, ReplaceHexadecimalSymbols(orderList[i].CustomerId), CellValues.String);
                    InsertCell(row, 2, orderList[i].EmployeeId.ToString(), CellValues.Number);
                    InsertCell(row, 3, ReplaceHexadecimalSymbols(orderList[i].OrderDate.ToLongDateString()), CellValues.String);
                    InsertCell(row, 4, ReplaceHexadecimalSymbols(orderList[i].ShipAddress), CellValues.String);
                    InsertCell(row, 5, ReplaceHexadecimalSymbols(orderList[i].ShipCity), CellValues.String);
                    sheetData.Append(row);
                }

                workbookPart.Workbook.Save();
                document.Close();
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var fileReader = new FileStream("document.xlsx", FileMode.Open))
                {
                    await fileReader.CopyToAsync(memoryStream);
                }

                memoryStream.Position = 0;

                return memoryStream.ToArray();
            }
        }
        private string ReplaceHexadecimalSymbols(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                return "";
            }

            string regex = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, regex, "", RegexOptions.Compiled);
        }

        private void InsertCell(Row row, int cellNum, dynamic val, CellValues type)
        {
            Cell refCell = null;
            Cell newCell = new Cell() { CellReference = cellNum.ToString() + ":" + row.RowIndex.ToString() };
            row.InsertBefore(newCell, refCell);

            newCell.CellValue = new CellValue(val);
            newCell.DataType = new EnumValue<CellValues>(type);
        }
    }
}
