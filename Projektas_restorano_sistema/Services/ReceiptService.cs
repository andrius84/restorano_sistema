﻿using RestoranoSistema.Models;
using RestoranoSistema.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestoranoSistema.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        public ReceiptService(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;

        }
        public List<string> GenerateClientReceipt(Order order)
        {
            var lines = new List<string>
        {
            "----------------------------------------",
            "                   Čekis                ",
            "----------------------------------------",
            $"Užsakymo Nr.: {order.Id}",
            $"Staliukas: {order.Table.Id} (Seats: {order.Table.Seats})",
            $"Užsakymo laikas: {order.OrderTime.ToShortTimeString()}",
            "----------------------------------------",
            "Patiekalai ir gėrimai:"
        };
            if (order.Dishes != null)
            {
                foreach (var dish in order.Dishes)
                {
                    lines.Add($"- {dish.Name}           ${dish.Price:F2}");
                }
            }
            if (order.Beverages != null)
            {
                foreach (var beverage in order.Beverages)
                {
                    lines.Add($"- {beverage.Name}           ${beverage.Price:F2}");
                }
            }
            lines.AddRange(new[]
            {
            "----------------------------------------",
            $"Visa kaina:       ${order.TotalPrice:F2}",
            "----------------------------------------",
            "Ačiū, kad renkatės mūsų restoraną!"
        });
            return lines;
        }
        public List<string> GenerateRestaurantReceipt(Order order)
        {
            var lines = new List<string>
        {
            "----------------------------------------",
            "            Kvitas restoranui           ",
            "----------------------------------------",
            $"Užsakymo numeris: {order.Id}",
            $"Staliukas: {order.Table.Id} (Seats: {order.Table.Seats})",
            $"Užsakymo laikas: {order.OrderTime.ToShortTimeString()}",
            "----------------------------------------",
            "Patiekalai ir gėrimai:"
        };
            if (order.Dishes != null)
            {
                foreach (var dish in order.Dishes)
                {
                    lines.Add($"- {dish.Name}           ${dish.Price:F2}");
                }
            }
            if (order.Beverages != null)
            {
                foreach (var beverage in order.Beverages)
                {
                    lines.Add($"- {beverage.Name}           ${beverage.Price:F2}");
                }
            }
            lines.AddRange(new[]
            {
            "----------------------------------------",
            $"Galutinė kaina:   ${order.TotalPrice:F2}",
            "----------------------------------------"
        });
            _receiptRepository.SaveRestaurantReceiptToFile(lines);
            return lines;
        }
        public void SendClientReceiptToEmail(Order order, string clientEmail)
        {
            // Generate the receipt
            var receiptLines = GenerateClientReceipt(order);
            var receiptBody = string.Join(Environment.NewLine, receiptLines);

            // Set up the email message
            var mailMessage = new MailMessage("andrius.asmanavicius@gmail.com", clientEmail)
            {
                Subject = "Your Receipt from Our Restaurant",
                Body = receiptBody
            };

            // Set up the SMTP client
            using (var smtpClient = new SmtpClient("smtp.example.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential("your-email@example.com", "your-email-password");
                smtpClient.EnableSsl = true;

                // Send the email
                smtpClient.Send(mailMessage);
            }
        }
    }
}
