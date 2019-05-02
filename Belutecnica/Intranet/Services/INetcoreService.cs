﻿using Intranet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Services
{
    public interface INetcoreService
    {
        Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email);

        Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager);

        Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL);

        Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env);

        Task UpdateRoles(ApplicationUser appUser, ApplicationUser currentUserLogin);

        Task CreateDefaultSuperAdmin();

        //VMStock GetStockByProductAndWarehouse(string productId, string warehouseId);

        //List<VMStock> GetStockPerWarehouse();

        Task InitCRM();
    }
}
