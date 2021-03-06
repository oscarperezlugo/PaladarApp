﻿using Acr.UserDialogs;
using PaladarApp.Conexiones;
using PaladarApp.Datos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;

namespace PaladarApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : CarouselPage
    {
        int N;
        int M;
        int L;
        double totalfinal;
        double totalfinal2;
        IUserDialogs Dialogs = UserDialogs.Instance;        
        System.Guid? VENTA;
        System.Guid? IDCLIENTE;
        System.DateTime FECHA;
        string RESULTADO;        
        string STATUS;
        string METODO;
        string DIRECCION;
        string DIRECCIONLOG;
        string TIPOVENTA;
        int lineaid;
        decimal totalrestar;
        decimal totalrestarD;
        int rowsend;
        string NOMBRE;
        public MainPage()
        {
            InitializeComponent();
            Repositorio repo = new Repositorio();
            Producto[] listaproducto = repo.getProducto();
            Categorias[] listacategoria = repo.getCategoria();
            ListaProductos.ItemsSource = listaproducto;
            ListaProductos2.ItemsSource = listaproducto;
            ListaCategorias.ItemsSource = listacategoria;
            fechafactura.Text = DateTime.Now.ToString();
            

        }
        protected async override void OnAppearing() 
        {
            
            try 
            { 
                var verif = await SecureStorage.GetAsync("id");
                if (verif != null)
                {
                    await SecureStorage.GetAsync("id");
                    var idcliente = await SecureStorage.GetAsync("idcliente");
                    IDCLIENTE = Guid.Parse(idcliente);
                    DIRECCIONLOG = await SecureStorage.GetAsync("direccionlog");
                    NOMBRE = await SecureStorage.GetAsync("nombre");
                    string usuariodef = await SecureStorage.GetAsync("usuario");
                    FECHA = DateTime.Now;
                    usuario.Text = usuariodef;
                    usuario2.Text = usuariodef;
                    usuario3.Text = usuariodef;
                    clientefactura.Text = usuariodef;
                }
            }
            catch { };
            
            
                
            
        }
        private void DolarClicked(object sender, EventArgs e)
        {
            CurrentPage = ListaDolares;
        }
        private void BolivarClicked(object sender, EventArgs e)
        {
            CurrentPage = ListaBolivares;
        }
        private void LoginClicked(object sender, EventArgs e)
        {
            CurrentPage = Login;
        }
        private void BodegonClicked(object sender, EventArgs e)
        {
            CurrentPage = Categorias;
        }
        private void RegistroClicked(object sender, EventArgs e)
        {
            CurrentPage = Registro;
        }
        private void CarritoClicked(object sender, EventArgs e)
        {
            CurrentPage = Carrito;
        }
        private void mapaClicked(object sender, EventArgs e)
        {
            CurrentPage = Mapa;
        }
        private async void OpenWhatsApp(object sender, EventArgs e)
        {
            try
            {
                Chat.Open("+584140456431", "Hola, ¿se encuentran disponibles?");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        void Handle_SearchButtonPressed(object sender, System.EventArgs e)
        {
            Repositorio repo = new Repositorio();
            Producto[] listaprod = repo.getProducto();
            List<Producto> lst = listaprod.OfType<Producto>().ToList();
            var ProdSearched = lst.Where(l => l.Producto1.Contains(SearchBar.Text));
            ListaProductos.ItemsSource = ProdSearched;
        }
        void Handle_SearchButtonPressedDos(object sender, System.EventArgs e)
        {
            Repositorio repo = new Repositorio();
            Producto[] listaprod = repo.getProducto();
            List<Producto> lst = listaprod.OfType<Producto>().ToList();
            var ProdSearched = lst.Where(l => l.Producto1.Contains(SearchBar2.Text));
            ListaProductos2.ItemsSource = ProdSearched;
        }
        void Handle_SearchButtonPressedtres(object sender, System.EventArgs e)
        {
            Repositorio repo = new Repositorio();
            Categorias[] listacat = repo.getCategoria();
            List<Categorias> lst = listacat.OfType<Categorias>().ToList();
            var CatSearched = lst.Where(l => l.Categoria.Contains(SearchBar3.Text));
            ListaCategorias.ItemsSource = CatSearched;
        }
        private void CategoriaPressed(object sender, SelectedItemChangedEventArgs e)
        {
            Repositorio repo = new Repositorio();
            Producto[] listaproducto = repo.getProducto();
            List<Producto> lst = listaproducto.OfType<Producto>().ToList();
            var obj = (Categorias)e.SelectedItem;
            var ArticuloSearched = lst.Where(l => l.Categoria == obj.Categoria);            
            ListaProductos.ItemsSource = ArticuloSearched;
            ListaProductos2.ItemsSource = ArticuloSearched;
            CurrentPage = ListaDolares;
        }
        private async void Login_Clicked(object sender, EventArgs e)
        {
            if (correos.Text != null && contrasenas.Text != null)
            {
                Login usuariologin = new Login();
                usuariologin.Correo = correos.Text;
                usuariologin.Contrasena = contrasenas.Text;


                try
                {

                    Repositorio repositorio = new Repositorio();
                    Login userlogin = repositorio.postLogin(usuariologin).Result;
                    if (userlogin.iD != 0)
                    {
                        L = 1;
                        Dialogs.ShowLoading("Bienvenido " + userlogin.nombre + "");                        
                        await Task.Delay(2000);
                        Dialogs.HideLoading();
                        usuario.Text = ""+userlogin.nombre+" "+userlogin.apellido+"";
                        usuario2.Text = "" + userlogin.nombre + " " + userlogin.apellido + "";
                        usuario3.Text = "" + userlogin.nombre + " " + userlogin.apellido + "";
                        usuario4.Text = "" + userlogin.nombre + " " + userlogin.apellido + "";
                        clientefactura.Text = "" + userlogin.nombre + " " + userlogin.apellido + "";
                        IDCLIENTE = userlogin.guid;
                        DIRECCIONLOG = userlogin.direccion;
                        FECHA = DateTime.Now;
                        NOMBRE = userlogin.nombre;
                        try
                        {

                            await SecureStorage.SetAsync("id", userlogin.iD.ToString());
                            await SecureStorage.SetAsync("idcliente", userlogin.guid.ToString());
                            await SecureStorage.SetAsync("direccionlog", userlogin.direccion.ToString());
                            await SecureStorage.SetAsync("nombre", userlogin.nombre.ToString());
                            await SecureStorage.SetAsync("usuario", ""+userlogin.nombre+" "+userlogin.apellido+"");


                        }
                        catch (Exception ex)
                        {
                            
                        }
                        CurrentPage = ListaDolares;
                    }
                    else
                    {
                        Dialogs.ShowLoading("Correo o Contraseña incorrectas");
                        await Task.Delay(2000);
                        Dialogs.HideLoading();
                    }
                    
                }

                catch (Exception ex)
                {
                    

                }
            }
            else 
            {
                Dialogs.ShowLoading("Rellene los campos de Correo y Contraseña");
                await Task.Delay(2000);
                Dialogs.HideLoading();
            }

        }
        private async void Registro_Clicked(object sender, EventArgs e)
        {
            if (RegCorreo.Text != null && RegNombre.Text != null && RegApellido.Text != null && RegCalle.Text != null && RegTel.Text != null && RegContra.Text == ConContra.Text)
            {
                Clientes clientes = new Clientes();
                clientes.Nombre = RegNombre.Text;
                clientes.Apellido = RegApellido.Text;
                clientes.Contrasena = RegContra.Text;
                clientes.Telefono = ""+RegTelefono.SelectedItem.ToString()+" "+RegTel.Text+"";
                clientes.Correo = RegCorreo.Text;
                clientes.Direccion = ""+RegMunicipio.SelectedItem.ToString()+" "+RegCalle.Text+"";
                try
                {

                    Repositorio repositorio = new Repositorio();
                    Clientes ClienteR = repositorio.postRegistro(clientes).Result;
                    if (ClienteR.Row != 0)
                    {
                        L = 1;
                        Dialogs.ShowLoading("Bienvenido a Bodegón Paladar " + ClienteR.Nombre + "");
                        await Task.Delay(2000);
                        Dialogs.HideLoading();
                        usuario.Text = "" + ClienteR.Nombre + " " + ClienteR.Apellido + "";
                        usuario2.Text = "" + ClienteR.Nombre + " " + ClienteR.Apellido + "";
                        usuario3.Text = "" + ClienteR.Nombre + " " + ClienteR.Apellido + "";
                        clientefactura.Text = "" + ClienteR.Nombre + " " + ClienteR.Apellido + "";
                        try
                        {

                            await SecureStorage.SetAsync("id", ClienteR.Row.ToString());
                            CurrentPage = ListaDolares;
                        }
                        catch (Exception ex)
                        {
                            Dialogs.ShowLoading("Error " + ex + "");
                            await Task.Delay(2000);
                            Dialogs.HideLoading();
                        }
                    }
                    else
                    {
                        Dialogs.ShowLoading("Datos Incorrectos");
                        await Task.Delay(2000);
                        Dialogs.HideLoading();
                    }

                }

                catch (Exception ex)
                {
                    await DisplayAlert("Registro Error", ex.Message, "Intente de nuevo mas tarde");

                }
            }
            else
            {
                Dialogs.ShowLoading("Rellene los campos para registrarse");
                await Task.Delay(2000);
                Dialogs.HideLoading();
            }

        }
        private ObservableCollection<Lineas> _linea;
        public ObservableCollection<Lineas> Lineas
        {
            get
            {
                return _linea ?? (_linea = new ObservableCollection<Lineas>());
                                                                                                                                                   
            }
        }
        private async void ItemClicked(object sender, SelectedItemChangedEventArgs e)
        {
            if (await  SecureStorage.GetAsync("id") != null)
            {
                string cantfin;
                int cant;
                var obj = (Producto)e.SelectedItem;
                cantfin = obj.SelectItem;
                if (cantfin == null) 
                {
                    string cantmed = await DisplayActionSheet("SELECCIONE LA CANTIDAD", null, null, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10");
                    cant = int.Parse(cantmed); 
                }
                else 
                { 
                    cant = Int16.Parse(cantfin); 
                }
                string id = obj.Producto1.ToString();
                decimal? precio = obj.Precio * cant;
                decimal? preciod = obj.PrecioD * cant;
                double totalsum = double.Parse(precio.ToString());
                double totalsum2 = double.Parse(preciod.ToString());
                totalfinal = totalfinal + totalsum;
                totalfinal2 = totalfinal2 + totalsum2;
                string montoind = totalfinal.ToString("0.00");
                string montoind2 = totalfinal2.ToString("0.00");
                totalcuatro.Text = "" + montoind + "";
                totalcinco.Text = montoind2.ToString();
                N = N + 1;                
                if (N == 1)
                {
                    Cabecera cabecera = new Cabecera();
                    cabecera.iDVenta = System.Guid.NewGuid();
                    VENTA = cabecera.iDVenta;
                }                               
                var linea = new Lineas()
                {
                    Dolares = preciod,
                    Row = lineaid,
                    Cantidad = cant,
                    Producto = id,
                    Precio = precio
                };
                var item = Lineas.FirstOrDefault(i => i.Producto == linea.Producto);
                if (item != null)
                {
                    item.Cantidad = item.Cantidad + linea.Cantidad;
                    item.Precio = item.Precio + linea.Precio;
                    item.Dolares = linea.Dolares + item.Dolares;
                    linea.Dolares = item.Dolares;
                    linea.Cantidad = item.Cantidad;
                    linea.Precio = item.Precio;
                    linea.Producto = item.Producto;
                    linea.Row = linea.Row;
                    Lineas.Add(linea);
                    Lineas.Remove(item);
                }
                else
                {
                    M = M + 1;
                    Lineas.Add(linea);
                }
                indicador.Text = " " + M + " ";
                indicador2.Text = " " + M + " ";
                indicador3.Text = " " + M + " ";
                indicador4.Text = " " + M + " ";
                FacturaFinal.ItemsSource = Lineas;
                Dialogs.ShowLoading("Producto Agregado");
                await Task.Delay(1000);
                Dialogs.HideLoading();
            }
            else
            {
                await DisplayAlert("Gracias por preferirnos", "Debe Realizar su LOGIN", "OK");
                CurrentPage = Login;
            }
            
        }
        private async void FinalizarClicked(object sender, EventArgs e)
        {
            Repositorio repositorio = new Repositorio();
            string metodo = await DisplayActionSheet("SELECCIONES METODO DE PAGO", "Cancelar", null, "PAGO MOVIL", "ZELLE", "TRANSFERENCIA", "EN LOCAL");
            if (metodo == "ZELLE") 
            {
                string resultado = await DisplayPromptAsync("Zelle, bodegonpaladarvalencia@gmail.com, Jose Brito", "CORREO Y NOMBRE AFILIADO");
                RESULTADO = resultado;
                if (resultado != null)
                {
                    await DisplayAlert("Gracias por su Compra", "Verificaremos su pago y procesaremos su pedido", "OK");
                    STATUS = "PAGO POR VERIFICAR";
                    METODO = metodo;
                    string tipoventa = await DisplayActionSheet("TIPO DE VENTA", null, null, "DELIVERY", "PICKUP");
                    TIPOVENTA = tipoventa;
                    if (tipoventa == "DELIVERY" )
                    {
                        bool answer = await DisplayAlert("¿Esta es la direccion de entrega?", "" + DIRECCIONLOG + "", "SI", "NO");
                        if (answer == true) 
                        {
                            DIRECCION = DIRECCIONLOG;
                            
                        }
                        else
                        {
                            DIRECCION = await DisplayPromptAsync("INSERTE LA DIRECCION DE ENTREGA", "DIRECCION");
                        }
                    }
                    else 
                    {
                        await DisplayAlert("Gracias por su Compra", "lo estamos esperando, procesaremos su pedido", "OK");
                    }
                    Cabecera cabecera = new Cabecera();
                    cabecera.iDCliente = IDCLIENTE;
                    cabecera.iDVenta = VENTA;
                    cabecera.FechaVenta = FECHA;
                    Decimal totaldolares = Decimal.Parse(totalcinco.Text);
                    cabecera.SubTotal = totaldolares;
                    Decimal total = Decimal.Parse(totalcuatro.Text);
                    cabecera.Monto = total;
                    cabecera.Lineas = N;
                    cabecera.Status = STATUS;
                    cabecera.Metodo = METODO;
                    cabecera.TipoVenta = TIPOVENTA;
                    cabecera.Direccion = DIRECCION;
                    try
                    {

                        Cabecera cabecerar = repositorio.postCabecera(cabecera).Result;
                    }
                    catch { }
                    Pagos pagos = new Pagos();
                    pagos.iDVenta = VENTA;
                    pagos.Metodo = METODO;
                    pagos.Correo = RESULTADO;
                    try
                    {

                        Pagos pagosr = repositorio.postPagos(pagos).Result;

                    }
                    catch { }

                    for (int i = 0; i < Lineas.Count; i++)
                    {
                        Cantidad cantidad = new Cantidad();
                        string Productotest = Lineas[i].Producto;
                        cantidad.CantidadDesc = Int16.Parse(Lineas[i].Cantidad.ToString());
                        cantidad.TagDesc = Productotest.Replace(" ", string.Empty).ToLower().ToString();
                        try
                        {
                            Cantidad cantidadr = repositorio.postCantidad(cantidad).Result;

                        }
                        catch { }
                    }
                    for (int j = 0; j < Lineas.Count; j++)
                    {
                        Lineas lineas = new Lineas();
                        lineas.Producto = Lineas[j].Producto;
                        lineas.Precio = Decimal.Parse(Lineas[j].Precio.ToString());
                        lineas.Cantidad = Double.Parse(Lineas[j].Cantidad.ToString());
                        lineas.iDVenta = VENTA;
                        try
                        {

                            Lineas lienar = repositorio.postLinea(lineas).Result;


                        }
                        catch
                        {
                        }
                    }


                    VENTA = null;
                    totaldolares = 0;
                    total = 0;
                    N = 0;
                    M = 0;
                    totalcuatro.Text = "0";
                    totalcinco.Text = "0";
                    indicador.Text = " " + M + " ";
                    indicador2.Text = " " + M + " ";
                    indicador3.Text = " " + M + " ";
                    indicador4.Text = " " + M + " ";
                    STATUS = null;
                    METODO = null;
                    TIPOVENTA = null;
                    DIRECCION = null;
                    Lineas.Clear();
                    Dialogs.ShowLoading("Gracias " + NOMBRE + ", por preferirnos");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = ListaDolares;

                }
                else if (resultado == "cancel")
                {
                    CurrentPage = Carrito;
                }
                else
                {
                    await DisplayAlert("POR FAVOR", "Recuerde ingresar los datos para procesar su pedido", "OK");
                    CurrentPage = Carrito;
                }
                
            }
            else if (metodo == "TRANSFERENCIA")
            {
                string resultado = await DisplayPromptAsync("Banesco, Inversiones MGJA CA, J-412143620, 01340467474671099991", "DATOS TRANSFERENCIA");
                RESULTADO = resultado;
                if (resultado != null)
                {
                    await DisplayAlert("Gracias por su Compra", "Verificaremos su pago y procesaremos su pedido", "OK");
                    STATUS = "PAGO POR VERIFICAR";
                    METODO = metodo;
                    string tipoventa = await DisplayActionSheet("TIPO DE VENTA", null, null, "DELIVERY", "PICKUP");
                    TIPOVENTA = tipoventa;
                    if (tipoventa == "DELIVERY")
                    {
                        bool answer = await DisplayAlert("¿Esta es la direccion de entrega?", "" + DIRECCIONLOG + "", "SI", "NO");
                        if (answer == true)
                        {
                            DIRECCION = DIRECCIONLOG;

                        }
                        else
                        {
                            DIRECCION = await DisplayPromptAsync("INSERTE LA DIRECCION DE ENTREGA", "DIRECCION");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Gracias por su Compra", "lo estamos esperando, procesaremos su pedido", "OK");
                    }
                    Cabecera cabecera = new Cabecera();
                    cabecera.iDCliente = IDCLIENTE;
                    cabecera.iDVenta = VENTA;
                    cabecera.FechaVenta = FECHA;
                    Decimal totaldolares = Decimal.Parse(totalcinco.Text);
                    cabecera.SubTotal = totaldolares;
                    Decimal total = Decimal.Parse(totalcuatro.Text);
                    cabecera.Monto = total;
                    cabecera.Lineas = N;
                    cabecera.Status = STATUS;
                    cabecera.Metodo = METODO;
                    cabecera.TipoVenta = TIPOVENTA;
                    cabecera.Direccion = DIRECCION;
                    try
                    {

                        Cabecera cabecerar = repositorio.postCabecera(cabecera).Result;
                    }
                    catch { }
                    Pagos pagos = new Pagos();
                    pagos.iDVenta = VENTA;
                    pagos.Metodo = METODO;
                    pagos.Correo = RESULTADO;
                    try
                    {

                        Pagos pagosr = repositorio.postPagos(pagos).Result;

                    }
                    catch { }

                    for (int i = 0; i < Lineas.Count; i++)
                    {
                        Cantidad cantidad = new Cantidad();
                        string Productotest = Lineas[i].Producto;
                        cantidad.CantidadDesc = Int16.Parse(Lineas[i].Cantidad.ToString());
                        cantidad.TagDesc = Productotest.Replace(" ", string.Empty).ToLower().ToString();
                        try
                        {
                            Cantidad cantidadr = repositorio.postCantidad(cantidad).Result;

                        }
                        catch { }
                    }
                    for (int j = 0; j < Lineas.Count; j++)
                    {
                        Lineas lineas = new Lineas();
                        lineas.Producto = Lineas[j].Producto;
                        lineas.Precio = Decimal.Parse(Lineas[j].Precio.ToString());
                        lineas.Cantidad = Double.Parse(Lineas[j].Cantidad.ToString());
                        lineas.iDVenta = VENTA;
                        try
                        {

                            Lineas lienar = repositorio.postLinea(lineas).Result;


                        }
                        catch
                        {
                        }
                    }


                    VENTA = null;
                    totaldolares = 0;
                    total = 0;
                    N = 0;
                    M = 0;
                    totalcuatro.Text = "0";
                    totalcinco.Text = "0";
                    indicador.Text = " " + M + " ";
                    indicador2.Text = " " + M + " ";
                    indicador3.Text = " " + M + " ";
                    indicador4.Text = " " + M + " ";
                    STATUS = null;
                    METODO = null;
                    TIPOVENTA = null;
                    DIRECCION = null;
                    Lineas.Clear();
                    Dialogs.ShowLoading("Gracias " + NOMBRE + ", por preferirnos");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = ListaDolares;
                }
                else if (resultado == "cancel")
                {
                    CurrentPage = Carrito;
                }
                else
                {
                    await DisplayAlert("POR FAVOR", "Recuerde ingresar los datos para procesar su pedido", "OK");
                    CurrentPage = Carrito;
                }
            }
            else if (metodo == "PAGO MOVIL")
            {
                string resultado = await DisplayPromptAsync("PagoMovil Bodegon Paladar, Banesco, J-412143620, 0412-4108175", "NUMERO DE PAGOMOVIL");
                RESULTADO = resultado;
                if (resultado != null)
                {
                    await DisplayAlert("Gracias por su Compra", "Verificaremos su pago y procesaremos su pedido", "OK");
                    STATUS = "PAGO POR VERIFICAR";
                    METODO = metodo;
                    string tipoventa = await DisplayActionSheet("TIPO DE VENTA", null, null, "DELIVERY", "PICKUP");
                    TIPOVENTA = tipoventa;
                    if (tipoventa == "DELIVERY")
                    {
                        bool answer = await DisplayAlert("¿Esta es la direccion de entrega?", "" + DIRECCIONLOG + "", "SI", "NO");
                        if (answer == true)
                        {
                            DIRECCION = DIRECCIONLOG;

                        }
                        else
                        {
                            DIRECCION = await DisplayPromptAsync("INSERTE LA DIRECCION DE ENTREGA", "DIRECCION");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Gracias por su Compra", "lo estamos esperando, procesaremos su pedido", "OK");
                    }
                    Cabecera cabecera = new Cabecera();
                    cabecera.iDCliente = IDCLIENTE;
                    cabecera.iDVenta = VENTA;
                    cabecera.FechaVenta = FECHA;
                    Decimal totaldolares = Decimal.Parse(totalcinco.Text);
                    cabecera.SubTotal = totaldolares;
                    Decimal total = Decimal.Parse(totalcuatro.Text);
                    cabecera.Monto = total;
                    cabecera.Lineas = N;
                    cabecera.Status = STATUS;
                    cabecera.Metodo = METODO;
                    cabecera.TipoVenta = TIPOVENTA;
                    cabecera.Direccion = DIRECCION;
                    try
                    {

                        Cabecera cabecerar = repositorio.postCabecera(cabecera).Result;
                    }
                    catch { }
                    Pagos pagos = new Pagos();
                    pagos.iDVenta = VENTA;
                    pagos.Metodo = METODO;
                    pagos.Correo = RESULTADO;
                    try
                    {

                        Pagos pagosr = repositorio.postPagos(pagos).Result;

                    }
                    catch { }

                    for (int i = 0; i < Lineas.Count; i++)
                    {
                        Cantidad cantidad = new Cantidad();
                        string Productotest = Lineas[i].Producto;
                        cantidad.CantidadDesc = Int16.Parse(Lineas[i].Cantidad.ToString());
                        cantidad.TagDesc = Productotest.Replace(" ", string.Empty).ToLower().ToString();
                        try
                        {
                            Cantidad cantidadr = repositorio.postCantidad(cantidad).Result;

                        }
                        catch { }
                    }
                    for (int j = 0; j < Lineas.Count; j++)
                    {
                        Lineas lineas = new Lineas();
                        lineas.Producto = Lineas[j].Producto;
                        lineas.Precio = Decimal.Parse(Lineas[j].Precio.ToString());
                        lineas.Cantidad = Double.Parse(Lineas[j].Cantidad.ToString());
                        lineas.iDVenta = VENTA;
                        try
                        {

                            Lineas lienar = repositorio.postLinea(lineas).Result;


                        }
                        catch
                        {
                        }
                    }


                    VENTA = null;
                    totaldolares = 0;
                    total = 0;
                    N = 0;
                    M = 0;
                    totalcuatro.Text = "0";
                    totalcinco.Text = "0";
                    indicador.Text = " " + M + " ";
                    indicador2.Text = " " + M + " ";
                    indicador3.Text = " " + M + " ";
                    indicador4.Text = " " + M + " ";
                    STATUS = null;
                    METODO = null;
                    TIPOVENTA = null;
                    DIRECCION = null;
                    Lineas.Clear();
                    Dialogs.ShowLoading("Gracias " + NOMBRE + ", por preferirnos");
                    await Task.Delay(2000);
                    Dialogs.HideLoading();
                    CurrentPage = ListaDolares;
                }
                else if (resultado == "cancel")
                {
                    CurrentPage = Carrito;
                }
                else
                {
                    await DisplayAlert("POR FAVOR", "Recuerde ingresar los datos para procesar su pedido", "OK");
                    CurrentPage = Carrito;
                }
            }
            else if (metodo == "EN LOCAL")
            {
                
                await DisplayAlert("Gracias por su Compra", "lo estamos esperando, procesaremos su pedido", "OK");
                STATUS = "Pago por cobrar en local";
                METODO = metodo;
                TIPOVENTA = "PICKUP";
                DIRECCION = "EN LOCAL";
                RESULTADO = "EFECTIVO TARJETA EN EL LOCAL";
                Cabecera cabecera = new Cabecera();
                cabecera.iDCliente = IDCLIENTE;
                cabecera.iDVenta = VENTA;
                cabecera.FechaVenta = FECHA;
                Decimal totaldolares = Decimal.Parse(totalcinco.Text);
                cabecera.SubTotal = totaldolares;
                Decimal total = Decimal.Parse(totalcuatro.Text);
                cabecera.Monto = total;
                cabecera.Lineas = N;
                cabecera.Status = STATUS;
                cabecera.Metodo = METODO;
                cabecera.TipoVenta = TIPOVENTA;
                cabecera.Direccion = DIRECCION;
                try
                {

                    Cabecera cabecerar = repositorio.postCabecera(cabecera).Result;
                }
                catch { }
                Pagos pagos = new Pagos();
                pagos.iDVenta = VENTA;
                pagos.Metodo = METODO;
                pagos.Correo = RESULTADO;
                try
                {

                    Pagos pagosr = repositorio.postPagos(pagos).Result;

                }
                catch { }

                for (int i = 0; i < Lineas.Count; i++)
                {
                    Cantidad cantidad = new Cantidad();
                    string Productotest = Lineas[i].Producto;
                    cantidad.CantidadDesc = Int16.Parse(Lineas[i].Cantidad.ToString());
                    cantidad.TagDesc = Productotest.Replace(" ", string.Empty).ToLower().ToString();
                    try
                    {
                        Cantidad cantidadr = repositorio.postCantidad(cantidad).Result;

                    }
                    catch { }
                }
                for (int j = 0; j < Lineas.Count; j++)
                {
                    Lineas lineas = new Lineas();
                    lineas.Producto = Lineas[j].Producto;
                    lineas.Precio = Decimal.Parse(Lineas[j].Precio.ToString());
                    lineas.Cantidad = Double.Parse(Lineas[j].Cantidad.ToString());
                    lineas.iDVenta = VENTA;
                    try
                    {

                        Lineas lienar = repositorio.postLinea(lineas).Result;


                    }
                    catch
                    {
                    }
                }


                VENTA = null;
                totaldolares = 0;
                total = 0;
                N = 0;
                M = 0;
                totalcuatro.Text = "0";
                totalcinco.Text = "0";
                indicador.Text = " " + M + " ";
                indicador2.Text = " " + M + " ";
                indicador3.Text = " " + M + " ";
                indicador4.Text = " " + M + " ";
                STATUS = null;
                METODO = null;
                TIPOVENTA = null;
                DIRECCION = null;
                Lineas.Clear();
                Dialogs.ShowLoading("Gracias " + NOMBRE + ", por preferirnos");
                await Task.Delay(2000);
                Dialogs.HideLoading();
                CurrentPage = ListaDolares;

            }
            
        }
        private async void FacturaFinal_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string pregunta = await DisplayActionSheet("¿ESTA SEGURO DE ELIMINAR ESTE ITEM?", "SI", "NO");
            if (pregunta == "SI")
            {
                var obj = (Lineas)e.SelectedItem;
                decimal preciomedio = Decimal.Parse(obj.Precio.ToString());
                decimal preciofinal = Decimal.Parse(totalcuatro.Text);
                decimal preciomedioD = Decimal.Parse(obj.Dolares.ToString());
                decimal preciofinalD = Decimal.Parse(totalcinco.Text);
                decimal resta = preciofinal - preciomedio;
                decimal restaD = preciofinalD - preciomedioD;
                totalrestar = resta;
                totalrestarD = restaD;
                rowsend = obj.Row;
                DeleteClicked();
            }
            else
            {
                CurrentPage = Carrito;
            }
            

        }

        

        private async void DeleteClicked()
        {
            var linea = FacturaFinal.SelectedItem;
            Lineas.Remove((Lineas)linea);                                                          
            totalcuatro.Text = "" + totalrestar + "";
            totalcinco.Text = totalrestarD.ToString();            
            M = M - 1;
            N = N - 1;
            indicador.Text = " " + M + " ";
            indicador2.Text = " " + M + " ";
            indicador3.Text = " " + M + " ";
            indicador4.Text = " " + M + " ";
            Dialogs.ShowLoading("Producto Eliminado");
            await Task.Delay(1000);
            FacturaFinal.ItemsSource = Lineas;
            Dialogs.HideLoading();
            

        }
    }
    
}
