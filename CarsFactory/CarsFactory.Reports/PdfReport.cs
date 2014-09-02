namespace CarsFactory.Reports
{
    using System;
    using System.Linq;
    using System.Drawing;

    using Spire.Pdf;
    using Spire.Pdf.Graphics;
    using Spire.Pdf.Tables;
    using CarsFactory.Data;

    public static class PdfReport
    {
        // verticalDistance keeps the current vertical position in the page
        private static float verticalDistanceToTheTop = 0;
        private const string TITLE = "Aggregated Sales Report";
        private const int HEIGHT_SHIFT = 5;

        public static void GeneratePdfReport(CarsFactoryContext carsFactoryContext)
        {

            PdfDocument pdfReport = InitializePdfDocument();

            //get agragate data about the sales --var productsList = carsFactoryContext.Products.SqlQuery("SELECT * FROM PRODUCTS").ToList();
            //foreach day ...
            String[] a = new String[100];
            String[][] data = FormatData(a);
            PdfTable table = GenerateTable(data);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            AddTableToPage(table, pdfReport);
            SaveAndClose(pdfReport);
        }

        private static PdfDocument InitializePdfDocument()
        {
            PdfDocument doc = new PdfDocument();
            SetMargins();

            PdfSection section = doc.Sections.Add();
            section.PageSettings.Size = PdfPageSize.A4;
            PdfPageBase page = section.Pages.Add();

            DrawTitle(page);

            return doc;
        }

        private static String[][] FormatData(String[] data)
        {
            String[] tmpData = {
                            "Name;Capital;Continent;Area;Population",
                            "Argentina;Buenos Aires;South America;2777815;32300003",
                            "Bolivia;La Paz;South America;1098575;7300000",
                            "Brazil;Brasilia;South America;8511196;150400000",
                            "Canada;Ottawa;North America;9976147;26500000",
                            };
            data = tmpData;

            String[][] dataSource = new String[data.Length][];

            for (int i = 0; i < data.Length; i++)
            {
                dataSource[i] = data[i].Split(';');
            }

            return dataSource;
        }

        private static PdfTable GenerateTable(String[][] dataSource)
        {
            PdfTable table = new PdfTable();
            table.Style.CellPadding = 2;
            table.Style.HeaderSource = PdfHeaderSource.Rows;
            table.Style.HeaderRowCount = 1;
            table.Style.ShowHeader = true;
            table.DataSource = dataSource;
            table.AllowCrossPages = true;

            return table;
        }

        private static void AddTableToPage(PdfTable table, PdfDocument doc)
        {
            PdfPageBase page = doc.Sections[0].Pages[doc.Sections[0].Pages.Count - 1];

            float tableHeight = table.Rows.Count * table.Style.DefaultStyle.Font.Height * 1.5f;

            if (verticalDistanceToTheTop > (page.Canvas.ClientSize.Height - doc.PageSettings.Margins.Bottom - tableHeight))
            {
                page = doc.Sections[0].Pages.Add();
                verticalDistanceToTheTop = doc.PageSettings.Margins.Top;
            }

            PdfLayoutResult result = table.Draw(page, new PointF(2, verticalDistanceToTheTop));

            verticalDistanceToTheTop = verticalDistanceToTheTop + result.Bounds.Height + HEIGHT_SHIFT;
        }

        private static void SaveAndClose(PdfDocument doc)
        {
            doc.SaveToFile(@"..\..\..\PDF-Reports\SampleReport.pdf");
            doc.Close();
        }

        private static void SetMargins()
        {
            PdfMargins margin = new PdfMargins();

            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();

            margin.Top = unitCvtr.ConvertUnits(2f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Bottom = margin.Top;
            margin.Left = unitCvtr.ConvertUnits(2f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Right = margin.Left;
        }

        private static void DrawTitle(PdfPageBase page)
        {
            PdfStringFormat format;
            PdfTrueTypeFont font;

            PdfBrush brush = PdfBrushes.Black;
            font = new PdfTrueTypeFont(new Font("Arial", 14f, FontStyle.Bold));
            format = new PdfStringFormat(PdfTextAlignment.Center);
            page.Canvas.DrawString(TITLE, font, brush, page.Canvas.ClientSize.Width / 2, 0, format);

            verticalDistanceToTheTop += font.MeasureString(TITLE, format).Height + HEIGHT_SHIFT;
        }
    }
}