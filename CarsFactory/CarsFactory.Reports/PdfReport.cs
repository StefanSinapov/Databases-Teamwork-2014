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
        private static float height = 10;

        public static void GenerateJsonReports(CarsFactoryContext carsFactoryContext)
        {
           
            PdfDocument pdfReport = InitializePdfDocument();

            //get agragate data about the sales --var productsList = carsFactoryContext.Products.SqlQuery("SELECT * FROM PRODUCTS").ToList();
            //foreach day ...
            String[] a = new String[100];
            String[][] data = FormatData( a);
            CreateTable(data, pdfReport);
            SaveAndClose(pdfReport);

        }

        public static PdfDocument InitializePdfDocument()
        {
            //Create a pdf document.<br>
            PdfDocument doc = new PdfDocument();

            //margin
            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            PdfMargins margin = new PdfMargins();
            margin.Top = unitCvtr.ConvertUnits(2.54f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Bottom = margin.Top;
            margin.Left = unitCvtr.ConvertUnits(3.17f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Right = margin.Left;
            // Create new page
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin);
            float height = 10;
            //title
            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
            page.Canvas.DrawString("Country List", font1, brush1, page.Canvas.ClientSize.Width / 2, height, format1);
            height = height + font1.MeasureString("Country List", format1).Height;
            height = height + 5;

            return doc;
        }

        public static String[][] FormatData(String[] data)
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

        public static void CreateTable(String[][] dataSource, PdfDocument doc)
        {
            PdfTable table = new PdfTable();
            table.Style.CellPadding = 2;
            table.Style.HeaderSource = PdfHeaderSource.Rows;
            table.Style.HeaderRowCount = 1;
            table.Style.ShowHeader = true;
            table.DataSource = dataSource;

           // PdfPageBase page = doc.Pages[doc.Pages.Count - 1];
                   PdfPageBase page =doc.Pages.Add();
            PdfLayoutResult result = table.Draw(page, new PointF(0, height));
            height = height + result.Bounds.Height + 5;
            PdfBrush brush2 = PdfBrushes.Gray;
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 9f));
            page.Canvas.DrawString(String.Format("* {0} countries in the list.", dataSource.GetLength(0) - 1), font2, brush2, 5, height);
        }

        public static void SaveAndClose(PdfDocument doc)
        {
            doc.SaveToFile("SimpleTable.pdf");
            doc.Close();
        }
    }
}