using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPlant.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailVerificationOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email_verification_otp",
                table: "users",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "email_verification_otp_expiry",
                table: "users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_verification_otp",
                table: "users");

            migrationBuilder.DropColumn(
                name: "email_verification_otp_expiry",
                table: "users");
        }
    }
}
