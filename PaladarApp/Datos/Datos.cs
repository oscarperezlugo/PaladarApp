using System;
using System.Collections.Generic;
using System.Text;

namespace PaladarApp.Datos
{
    public class Cantidad
    {
        public string TagDesc { get; set; }
        public int CantidadDesc { get; set; }
    }
    public class Login
    {
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int iD { get; set; }
        public string Token { get; set; }        
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correolog { get; set; }
        public Nullable<System.Guid> guid { get; set; }
    }
    public partial class Cabecera
    {
        public Nullable<System.Guid> iDCliente { get; set; }
        public Nullable<System.DateTime> FechaVenta { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public Nullable<int> Lineas { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public Nullable<System.Guid> iDVenta { get; set; }
        public int Row { get; set; }
        public string Status { get; set; }
        public string TipoVenta { get; set; }
        public string Metodo { get; set; }
        public string Direccion { get; set; }
    }
    public partial class Clientes
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string Contrasena { get; set; }
        public Nullable<System.Guid> iDCliente { get; set; }
        public int Row { get; set; }
    }
    public partial class Lineas
    {
        public string Producto { get; set; }
        public Nullable<double> Cantidad { get; set; }
        
        public Nullable<decimal> Precio { get; set; }
        public Nullable<System.Guid> iDVenta { get; set; }
        public int Row { get; set; }
        public decimal? Dolares { get; set; }
        

    }
    public partial class Producto
    {
        public string Producto1 { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<System.Guid> iDProducto { get; set; }
        public int Row { get; set; }
        public byte[] Foto { get; set; }
        public Nullable<decimal> PrecioD { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public string Impuesto { get; set; }
        public string Descuento { get; set; }
        public string SelectItem { get; set; }
    }
    public partial class Categorias
    {
        public string Categoria { get; set; }
        public int Row { get; set; }
        public byte[] Foto { get; set; }
    }
    public partial class Descuentos
    {
        public string Descuento { get; set; }
        public Nullable<int> Porcentaje { get; set; }
        public int Row { get; set; }
    }
    public partial class Pagos
    {
        public Nullable<System.Guid> iDVenta { get; set; }
        public string Metodo { get; set; }
        public string Correo { get; set; }
        public int Row { get; set; }
    }
}
