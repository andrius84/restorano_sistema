using RestoranoSistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestoranoSistema.Repositories.Interfaces;
using RestoranoSistema.Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;


namespace RestoranoSistema.Services
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly IReceiptRepository _receiptRepository;
        public ReceiptsService(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }
        public List<string> GenerateClientReceipt(Order order)
        {
            var lines = new List<string>
            {
                "---------------------------------------------------",
                "                       Kvitas                         ",
                "---------------------------------------------------",
                $"Užsakymo Nr.: {order.Id}",
                $"Staliukas: {order.Table.Id} (Seats: {order.Table.Seats})",
                $"Užsakymo laikas: {order.OrderTime.ToShortTimeString()}",
                "---------------------------------------------------",
                "Patiekalai ir gėrimai:"
            };
            if (order.Dishes != null)
            {
                foreach (var dish in order.Dishes)
                {
                    var line = $"- {dish.Name}";
                    var priceGap = new string(' ', 45 - line.Length);
                    line += $"{priceGap}${dish.Price:F2}";
                    lines.Add(line);
                }
            }
            if (order.Beverages != null)
            {
                foreach (var beverage in order.Beverages)
                {
                    var line = $"- {beverage.Name}";
                    var priceGap = new string(' ', 45 - line.Length);
                    line += $"{priceGap}${beverage.Price:F2}";
                    lines.Add(line);
                }
            }
            lines.AddRange(new[]
            {
                "---------------------------------------------------",
                $"Visa kaina:                                  ${order.TotalPrice:F2}",
                "---------------------------------------------------",
                "Ačiū, kad renkatės mūsų restoraną!"
            });
            return lines;
        }
        public List<string> GenerateRestaurantReceipt(Order order)
        {
            var lines = new List<string>
            {
                "---------------------------------------------------",
                "                   Restorano kvitas                   ",
                "---------------------------------------------------",
                $"Užsakymo numeris: {order.Id}",
                $"Staliukas: {order.Table.Id} (Seats: {order.Table.Seats})",
                $"Užsakymo laikas: {order.OrderTime.ToShortTimeString()}",
                "---------------------------------------------------",
                "Patiekalai ir gėrimai:"
            };
            if (order.Dishes != null)
            {
                foreach (var dish in order.Dishes)
                {
                    var line = $"- {dish.Name}";
                    var priceGap = new string(' ', 45 - line.Length);
                    line += $"{priceGap}${dish.Price:F2}";
                    lines.Add(line);
                }
            }
            if (order.Beverages != null)
            {
                foreach (var beverage in order.Beverages)
                {
                    var line = $"- {beverage.Name}";
                    var priceGap = new string(' ', 45 - line.Length);
                    line += $"{priceGap}${beverage.Price:F2}";
                    lines.Add(line);
                }
            }
            lines.AddRange(new[]
            {
                "---------------------------------------------------",
                $"Galutinė kaina:                              ${order.TotalPrice:F2}",
                "---------------------------------------------------"
            });
            _receiptRepository.SaveRestaurantReceiptToFile(lines);
            return lines;
        }
        public void SendClientReceiptToEmail(List<string> receipt, string? clientEmail)
        {
            try
            {
                // Basic validation for clientEmail
                if (string.IsNullOrWhiteSpace(clientEmail))
                {
                    throw new ArgumentException("Client email address cannot be empty.");
                }

                using (var email = new MimeMessage())
                {
                    email.From.Add(new MailboxAddress("Sender Name", "test@email.com"));
                    email.To.Add(new MailboxAddress("Client", clientEmail));
                    email.Subject = "Receipt for Your Order";
                    email.Body = new TextPart("plain")
                    {
                        Text = string.Join(Environment.NewLine, receipt)
                    };

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Connect("localhost", 2525, false);
                        //smtp.Authenticate("user", "pass"); 
                        smtp.Send(email);
                        smtp.Disconnect(true);
                    }
                }
            }
            catch (SmtpCommandException ex)
            {
                Console.WriteLine($"SMTP Command error: {ex.Message}");
                Console.WriteLine($"StatusCode: {ex.StatusCode}");
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine($"Authentication failed: {ex.Message}");
            }
            catch (SmtpProtocolException ex)
            {
                Console.WriteLine($"Protocol error while communicating with SMTP server: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
