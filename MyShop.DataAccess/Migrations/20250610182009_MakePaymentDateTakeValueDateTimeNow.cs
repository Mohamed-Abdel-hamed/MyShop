using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakePaymentDateTakeValueDateTimeNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE OrderHeaders SET PaymentDate = GETDATE() WHERE PaymentDate IS NULL");
            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "OrderHeaders",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
