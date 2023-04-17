using Hotel.Application.Dto;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Helper
{
    public class Helper
    {
        public static class EmailServices
        {
            public static bool IsValid(string email)
            {
                return email.Contains("@");
            }

            public static void Enviar(string de, string para, string assunto, string mensagem)
            {
                var mail = new MailMessage(de, para);
                var client = new SmtpClient
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.google.com"
                };

                mail.Subject = assunto;
                mail.Body = mensagem;
                client.Send(mail);
            }
        }
        public static class NameServices
        {
            public static bool IsValid(string name)
            {
                return !string.IsNullOrEmpty(name);
            }
        }
        public static class validarEmailServices
        {
            public static bool IsValidEmail(string email)
            {
                return email.Contains("@");
            }
        }

        public static string GetHashMD5(string input)
        {
            MD5 md5Hasher = MD5.Create();

            if (input == null)
                return null;

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        public static LogDto Log(string message, string method)
        {
            var log = new LogDto();
            log.Error = message;
            log.Method = method;
            log.CreatedAt = DateTime.UtcNow;
            return log;
        }

        public static PurseHistoryDto DataPurse(PurseRequestDto purse, PurseDto purseActual, bool addPurse)
        {
            var result = new PurseHistoryDto();

            if (!!addPurse)
            {
                result.Id = purseActual.Id;
                result.IdPurse = purseActual.Id;
                result.PreviousValue = purseActual.Value;
                result.Value= purse.Value + purseActual.Value;
                result.ValueAdded = purse.Value;
                result.WithDrawnAmont = 0;
            }
            else
            {
                result.Id = purseActual.Id;
                result.IdPurse = purseActual.Id;
                result.PreviousValue = purseActual.Value;
                result.Value = purseActual.Value - purse.Value; // verificar validação fica não ficar negativo o saldo
                result.ValueAdded = 0;
                result.WithDrawnAmont = purse.Value;
            }

            return result;
        }

        public static void CreateStayPdf(StayHotelUserDto stayUser, StayHotelDto stayHotel)
        {
            using(var doc = new PdfSharp.Pdf.PdfDocument())
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var page = doc.AddPage();
                var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                XFont fontTit = new XFont("Helvetica Neue Condensed Bold", 36, XFontStyle.Regular);
                XFont font = new XFont("Helvetica Neue Condensed Bold", 20, XFontStyle.Regular);

                textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Center;
                textFormatter.DrawString("Reserva Confirmada " + stayHotel.Name, fontTit,
                    PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, 50, page.Width, page.Height));

                textFormatter.DrawString("Check-in dia" + stayUser.Checkin.ToString("dd/MM/yyyy") + " das 15hs. as 22hs.", font,
                    PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(20, 150, page.Width, page.Height));

                textFormatter.DrawString("Check-out dia " + stayUser.Checkout.ToString("dd/MM/yyyy") + " até as 11hs.", font,
                    PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, 185, page.Width, page.Height));

                doc.Save(@"C:\Temp\" + stayHotel.Id +".pdf");
                doc.Close();
            }
            CreatePayPdf(stayHotel);
        }

        public static void CreatePayPdf(StayHotelDto stayHotel)
        {
            using (var doc = new PdfSharp.Pdf.PdfDocument())
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var page = doc.AddPage();
                var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                //var fontTit = new PdfSharp.Drawing.XFont("Arial", 14, PdfSharp.Drawing.XFontStyle.BoldItalic);
                XFont fontTit = new XFont("Helvetica Neue Condensed Bold", 36, XFontStyle.Regular);
                XFont font = new XFont("Helvetica Neue Condensed Bold", 20, XFontStyle.Regular);

                textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Center;
                textFormatter.DrawString("Pagamento Realizado com sucesso para a Reserva no (a) " + stayHotel.Name, fontTit,
                    PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(0, 50, page.Width, page.Height));

                doc.Save(@"C:\Temp\" + stayHotel.Id + "_Pay.pdf");
            }
        }
    }
}
