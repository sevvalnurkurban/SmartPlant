using System.Net;
using System.Net.Mail;
using SmartPlant.Services.Interfaces;

namespace SmartPlant.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpHost = _configuration["SMTP:Host"];
                var smtpPort = int.Parse(_configuration["SMTP:Port"] ?? "587");
                var smtpUsername = _configuration["SMTP:Username"];
                var smtpPassword = _configuration["SMTP:Password"];
                var fromEmail = _configuration["SMTP:FromEmail"];
                var fromName = _configuration["SMTP:FromName"];

                using var client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail!, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Email send error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendPasswordResetEmailAsync(string toEmail, string resetToken)
        {
            var resetLink = $"https://localhost:7043/Account/ResetPassword?token={resetToken}";

            var body = $@"
                <html>
                <body>
                    <h2>SmartPlant - Password Reset</h2>
                    <p>You requested a password reset. Click the link below to reset your password:</p>
                    <p><a href='{resetLink}'>Reset Password</a></p>
                    <p>This link will expire in 24 hours.</p>
                    <p>If you didn't request this, please ignore this email.</p>
                    <br>
                    <p>SmartPlant Support</p>
                </body>
                </html>
            ";

            return await SendEmailAsync(toEmail, "SmartPlant - Password Reset", body);
        }

        public async Task<bool> SendEmailVerificationAsync(string toEmail, string verificationToken)
        {
            var verificationLink = $"https://localhost:7043/Account/VerifyEmail?token={verificationToken}";

            var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <div style='text-align: center; margin-bottom: 30px;'>
                            <h1 style='color: #8b9f7f;'>🌱 SmartPlant</h1>
                        </div>
                        <div style='background-color: #f9f9f9; padding: 30px; border-radius: 10px;'>
                            <h2 style='color: #8b9f7f; margin-top: 0;'>Welcome to SmartPlant!</h2>
                            <p>Thank you for registering with SmartPlant. Please verify your email address to activate your account.</p>
                            <p style='margin: 30px 0;'>
                                <a href='{verificationLink}' style='background-color: #8b9f7f; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>Verify Email</a>
                            </p>
                            <p style='color: #666; font-size: 14px;'>Or copy and paste this link into your browser:</p>
                            <p style='color: #666; font-size: 12px; word-break: break-all;'>{verificationLink}</p>
                            <p style='margin-top: 30px; color: #999; font-size: 12px;'>This link will expire in 24 hours.</p>
                            <p style='color: #999; font-size: 12px;'>If you didn't create this account, please ignore this email.</p>
                        </div>
                        <div style='text-align: center; margin-top: 20px; color: #999; font-size: 12px;'>
                            <p>© 2025 SmartPlant - All Rights Reserved</p>
                            <p>Intelligent Plant Care Assistant</p>
                        </div>
                    </div>
                </body>
                </html>
            ";

            return await SendEmailAsync(toEmail, "SmartPlant - Verify Your Email", body);
        }

        public async Task<bool> SendEmailVerificationOtpAsync(string toEmail, string otpCode)
        {
            var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <div style='text-align: center; margin-bottom: 30px;'>
                            <h1 style='color: #8b9f7f;'>🌱 SmartPlant</h1>
                        </div>
                        <div style='background-color: #f9f9f9; padding: 30px; border-radius: 10px;'>
                            <h2 style='color: #8b9f7f; margin-top: 0;'>Welcome to SmartPlant!</h2>
                            <p>Thank you for registering with SmartPlant. Please use the verification code below to activate your account.</p>
                            <div style='text-align: center; margin: 30px 0;'>
                                <div style='background-color: #8b9f7f; color: white; padding: 20px; border-radius: 10px; font-size: 32px; font-weight: bold; letter-spacing: 8px; display: inline-block;'>
                                    {otpCode}
                                </div>
                            </div>
                            <p style='text-align: center; color: #666; font-size: 14px;'>Enter this code on the verification page</p>
                            <p style='margin-top: 30px; color: #999; font-size: 12px;'>This code will expire in 15 minutes.</p>
                            <p style='color: #999; font-size: 12px;'>If you didn't create this account, please ignore this email.</p>
                        </div>
                        <div style='text-align: center; margin-top: 20px; color: #999; font-size: 12px;'>
                            <p>© 2025 SmartPlant - All Rights Reserved</p>
                            <p>Intelligent Plant Care Assistant</p>
                        </div>
                    </div>
                </body>
                </html>
            ";

            return await SendEmailAsync(toEmail, "SmartPlant - Email Verification Code", body);
        }
    }
}