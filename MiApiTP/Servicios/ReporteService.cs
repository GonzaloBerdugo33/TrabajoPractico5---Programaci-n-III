using System.Globalization;
using MiApiTP.Modelos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MiApiTP.Servicios
{
    public interface IReporteService
    {
        byte[] GenerarListadoStock(IReadOnlyCollection<Producto> productos);
        byte[] GenerarListadoEntradas(IReadOnlyCollection<IngresoProducto> ingresos);
        byte[] GenerarListadoSalidas(IReadOnlyCollection<SalidaProducto> salidas);
    }

    public class ReporteService : IReporteService
    {
        private static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-AR");

        public byte[] GenerarListadoStock(IReadOnlyCollection<Producto> productos)
        {
            var ahora = DateTime.UtcNow.AddHours(-3);
            var totalUnidades = 0;
            foreach (var p in productos)
            {
                totalUnidades += p.Stock;
            }

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(36);
                    page.Header().Text("Reporte de stock").FontSize(20).SemiBold();
                    page.Content().PaddingVertical(16).Column(column =>
                    {
                        column.Item().Text($"Generado el {ahora:dd/MM/yyyy HH:mm}")
                            .FontSize(10).FontColor(Colors.Grey.Darken1);
                        column.Item().PaddingTop(12).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1.5f);
                                columns.RelativeColumn(1.2f);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(EstiloEncabezado).Text("ID");
                                header.Cell().Element(EstiloEncabezado).Text("Producto");
                                header.Cell().Element(EstiloEncabezado).Text("Categoría");
                                header.Cell().Element(EstiloEncabezado).Text("Proveedor");
                                header.Cell().Element(EstiloEncabezado).AlignRight().Text("Precio");
                                header.Cell().Element(EstiloEncabezado).AlignRight().Text("Stock");
                            });

                            foreach (var producto in productos)
                            {
                                table.Cell().Element(EstiloCelda).Text(producto.Id.ToString());
                                table.Cell().Element(EstiloCelda).Text(producto.Nombre);
                                table.Cell().Element(EstiloCelda).Text(producto.Categoria?.Nombre ?? "-");
                                table.Cell().Element(EstiloCelda).Text(producto.Proveedor?.RazonSocial ?? "-");
                                table.Cell().Element(EstiloCelda).AlignRight().Text(producto.Precio.ToString("C2", Cultura));
                                table.Cell().Element(EstiloCelda).AlignRight().Text(producto.Stock.ToString());
                            }
                        });
                    });
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span($"Total de productos: {productos.Count} | Unidades en stock: ");
                        text.Span(totalUnidades.ToString()).SemiBold();
                    });
                });
            }).GeneratePdf();
        }

        public byte[] GenerarListadoEntradas(IReadOnlyCollection<IngresoProducto> ingresos)
        {
            var ahora = DateTime.UtcNow.AddHours(-3);
            var totalUnidades = 0;
            foreach (var i in ingresos)
            {
                totalUnidades += UnidadesDe(i);
            }

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(36);
                    page.Header().Text("Reporte de entradas").FontSize(20).SemiBold();
                    page.Content().PaddingVertical(16).Column(column =>
                    {
                        column.Item().Text($"Generado el {ahora:dd/MM/yyyy HH:mm}")
                            .FontSize(10).FontColor(Colors.Grey.Darken1);
                        column.Item().PaddingTop(12).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1.2f);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(EstiloEncabezado).Text("Nº");
                                header.Cell().Element(EstiloEncabezado).Text("Fecha");
                                header.Cell().Element(EstiloEncabezado).Text("Proveedor");
                                header.Cell().Element(EstiloEncabezado).Text("Registrado por");
                                header.Cell().Element(EstiloEncabezado).AlignRight().Text("Unidades");
                            });

                            foreach (var ingreso in ingresos)
                            {
                                table.Cell().Element(EstiloCelda).Text(ingreso.Id.ToString());
                                table.Cell().Element(EstiloCelda).Text(ingreso.Fecha.AddHours(-3).ToString("dd/MM/yyyy HH:mm"));
                                table.Cell().Element(EstiloCelda).Text(ingreso.Proveedor?.RazonSocial ?? "-");
                                table.Cell().Element(EstiloCelda).Text(ingreso.Usuario?.Email ?? "-");
                                table.Cell().Element(EstiloCelda).AlignRight().Text(UnidadesDe(ingreso).ToString());
                            }
                        });
                    });
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span($"Total de ingresos: {ingresos.Count} | Unidades ingresadas: ");
                        text.Span(totalUnidades.ToString()).SemiBold();
                    });
                });
            }).GeneratePdf();
        }

        public byte[] GenerarListadoSalidas(IReadOnlyCollection<SalidaProducto> salidas)
        {
            var ahora = DateTime.UtcNow.AddHours(-3);
            decimal totalGeneral = 0;
            foreach (var s in salidas)
            {
                totalGeneral += TotalDe(s);
            }

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(36);
                    page.Header().Text("Reporte de salidas (ventas)").FontSize(20).SemiBold();
                    page.Content().PaddingVertical(16).Column(column =>
                    {
                        column.Item().Text($"Generado el {ahora:dd/MM/yyyy HH:mm}")
                            .FontSize(10).FontColor(Colors.Grey.Darken1);
                        column.Item().PaddingTop(12).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2.5f);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1.2f);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(EstiloEncabezado).Text("Nº");
                                header.Cell().Element(EstiloEncabezado).Text("Fecha");
                                header.Cell().Element(EstiloEncabezado).Text("Cliente");
                                header.Cell().Element(EstiloEncabezado).Text("Forma de pago");
                                header.Cell().Element(EstiloEncabezado).AlignRight().Text("Unidades");
                                header.Cell().Element(EstiloEncabezado).AlignRight().Text("Total");
                            });

                            foreach (var salida in salidas)
                            {
                                table.Cell().Element(EstiloCelda).Text(salida.Id.ToString());
                                table.Cell().Element(EstiloCelda).Text(salida.Fecha.AddHours(-3).ToString("dd/MM/yyyy HH:mm"));
                                table.Cell().Element(EstiloCelda).Text(salida.Cliente?.Nombre ?? "-");
                                table.Cell().Element(EstiloCelda).Text(salida.MetodoPago);
                                table.Cell().Element(EstiloCelda).AlignRight().Text(UnidadesDe(salida).ToString());
                                table.Cell().Element(EstiloCelda).AlignRight().Text(TotalDe(salida).ToString("C2", Cultura));
                            }
                        });
                    });
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span($"Total de ventas: {salidas.Count} | Total vendido: ");
                        text.Span(totalGeneral.ToString("C2", Cultura)).SemiBold();
                    });
                });
            }).GeneratePdf();
        }

        private static int UnidadesDe(IngresoProducto ingreso)
        {
            var unidades = 0;
            foreach (var d in ingreso.DetallesIngresos)
            {
                unidades += d.Cantidad;
            }
            return unidades;
        }

        private static int UnidadesDe(SalidaProducto salida)
        {
            var unidades = 0;
            foreach (var d in salida.DetallesProductos)
            {
                unidades += d.Cantidad;
            }
            return unidades;
        }

        private static decimal TotalDe(SalidaProducto salida)
        {
            decimal total = 0;
            foreach (var d in salida.DetallesProductos)
            {
                total += d.Cantidad * d.PrecioUnitario;
            }
            return total;
        }

        private static IContainer EstiloEncabezado(IContainer container)
        {
            return container.Background(Colors.Blue.Darken2).Padding(6)
                .DefaultTextStyle(x => x.FontColor(Colors.White).SemiBold());
        }

        private static IContainer EstiloCelda(IContainer container)
        {
            return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(6);
        }
    }
}