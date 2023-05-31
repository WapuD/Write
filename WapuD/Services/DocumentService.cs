﻿using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Properties;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;

namespace WapuD.Services
{
    public class DocumentService
    {
        public async Task GetCheck(float OrderAmmount, float DiscountAmmount, Point PickupPoint, int OrderCode, int OrderNumber)
        {
            PdfWriter writer = new($"Товарный чек от {DateOnly.FromDateTime(DateTime.Now).ToString("D")}.pdf");
            PdfDocument pdf = new(writer);
            Document document = new(pdf);

            PdfFont comic = PdfFontFactory.CreateFont(@"C:\Windows\Fonts\comic.ttf", PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.PREFER_NOT_EMBEDDED);

            var content = new Paragraph($"ООО «Пиши Стирай»")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFont(comic)
                .SetFontSize(12);

            document.Add(content);

            content = new Paragraph($"Товарный чек № {string.Format("{0}", OrderNumber)} от {DateOnly.FromDateTime(DateTime.Now).ToString("D")}")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(16);

            document.Add(content);

            var table = new Table(5)
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetHeight(UnitValue.CreatePercentValue(100))
                .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

            table.AddCell(new Paragraph("Артикул")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(14));

            table.AddCell(new Paragraph("Наименование")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(14));

            table.AddCell(new Paragraph("Описание")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(14));

            table.AddCell(new Paragraph("Количество")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(14));

            table.AddCell(new Paragraph("Цена")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(14));

            document.Add(table);

            //content = new Paragraph(string.Format("Пункт выдачи: {0}, г. {1}, ул. {2}, д. {3}",
            //    PickupPoint.Index, PickupPoint.City, PickupPoint.Street, PickupPoint.House))
            //    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
            //    .SetFont(comic)
            //    .SetFontSize(14);

            document.Add(content);

            content = new Paragraph(string.Format("Сумма заказа: {0:C2}", OrderAmmount))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFont(comic)
                .SetFontSize(14);

            document.Add(content);

            content = new Paragraph(string.Format("Сумма скидки: {0:C2}", DiscountAmmount))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFont(comic)
                .SetFontSize(14);

            document.Add(content);

            content = new Paragraph(OrderCode.ToString())
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFont(comic)
                .SetFontSize(16);

            document.Add(content);

            document.Close();

            await Task.CompletedTask;
        }
    }
}
