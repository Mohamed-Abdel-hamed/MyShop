﻿
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MyShop.Web.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}
