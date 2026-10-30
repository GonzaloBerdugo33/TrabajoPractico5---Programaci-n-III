using PuntoDeVenta.Movil.DTOs;

namespace PuntoDeVenta.Movil.Servicios;

// Un renglón del carrito: el producto y cuántas unidades lleva
public class CarritoItem
{
    public ProductoDTO Producto { get; set; } = default!;
    public int Cantidad { get; set; }
    public decimal Subtotal => Producto.Precio * Cantidad;
}

// El carrito vive en memoria mientras la app está abierta.
// Se registra una sola vez (AddSingleton) para que no se pierda al cambiar de pantalla.
public class CarritoService
{
    private readonly List<CarritoItem> _items = new();

    public IReadOnlyList<CarritoItem> Items => _items;
    public decimal Total => _items.Sum(i => i.Subtotal);
    public int CantidadTotal => _items.Sum(i => i.Cantidad);

    public void Agregar(ProductoDTO producto)
    {
        var item = _items.FirstOrDefault(i => i.Producto.Id == producto.Id);

        if (item is null)
        {
            if (producto.Stock > 0)
                _items.Add(new CarritoItem { Producto = producto, Cantidad = 1 });
        }
        else
        {
            Aumentar(producto.Id);
        }
    }

    public void Aumentar(int productoId)
    {
        var item = _items.FirstOrDefault(i => i.Producto.Id == productoId);
        if (item is not null && item.Cantidad < item.Producto.Stock)
            item.Cantidad++;
    }

    public void Disminuir(int productoId)
    {
        var item = _items.FirstOrDefault(i => i.Producto.Id == productoId);
        if (item is not null && item.Cantidad > 1)
            item.Cantidad--;
    }

    public void Quitar(int productoId)
    {
        _items.RemoveAll(i => i.Producto.Id == productoId);
    }

    public void Vaciar()
    {
        _items.Clear();
    }
}