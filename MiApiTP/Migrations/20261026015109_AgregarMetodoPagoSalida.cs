using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiApiTP.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMetodoPagoSalida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngresoDetalles_Productos_ProductoId",
                table: "IngresoDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_SalidaDetalles_Productos_ProductoId",
                table: "SalidaDetalles");

            migrationBuilder.AddColumn<string>(
                name: "MetodoPago",
                table: "Salidas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Efectivo");

            migrationBuilder.AddForeignKey(
                name: "FK_IngresoDetalles_Productos_ProductoId",
                table: "IngresoDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalidaDetalles_Productos_ProductoId",
                table: "SalidaDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngresoDetalles_Productos_ProductoId",
                table: "IngresoDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_SalidaDetalles_Productos_ProductoId",
                table: "SalidaDetalles");

            migrationBuilder.DropColumn(
                name: "MetodoPago",
                table: "Salidas");

            migrationBuilder.AddForeignKey(
                name: "FK_IngresoDetalles_Productos_ProductoId",
                table: "IngresoDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalidaDetalles_Productos_ProductoId",
                table: "SalidaDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
