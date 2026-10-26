using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiApiTP.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaAuditoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true),
                    UsuarioEmail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Accion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Entidad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EntidadId = table.Column<int>(type: "integer", nullable: true),
                    Detalle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Ip = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorias");
        }
    }
}
