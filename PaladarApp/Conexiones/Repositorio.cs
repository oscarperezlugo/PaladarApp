﻿using Newtonsoft.Json;
using PaladarApp.Datos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaladarApp.Conexiones
{
    class Repositorio
    {
        public Producto[] getProducto()
        {
            try
            {
                Producto[] productos;
                var URLWebAPI = "https://paladarweb.somee.com/Api/api/Productoes";
                using (var Client = new System.Net.Http.HttpClient())
                {
                    var JSON = Client.GetStringAsync(URLWebAPI);
                    productos = Newtonsoft.Json.JsonConvert.DeserializeObject<Producto[]>(JSON.Result);
                }

                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Categorias[] getCategoria()
        {
            try
            {
                Categorias[] categoria;
                var URLWebAPI = "https://paladarweb.somee.com/Api/api/Categorias";
                using (var Client = new System.Net.Http.HttpClient())
                {
                    var JSON = Client.GetStringAsync(URLWebAPI);
                    categoria = Newtonsoft.Json.JsonConvert.DeserializeObject<Categorias[]>(JSON.Result);
                }

                return categoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Login> postLogin(Login login)
        {
            Login loginr = new Login();
            loginr.Correo = login.Correo;
            loginr.Contrasena = login.Contrasena;
            var jsonObj = JsonConvert.SerializeObject(loginr);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://paladarweb.somee.com/Api/api/Login"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Login result = JsonConvert.DeserializeObject<Login>(dataResult);
                return result;
            }
        }
        public async Task<Clientes> postRegistro(Clientes clientes)
        {
            Clientes clienter = new Clientes();
            clienter.Nombre = clientes.Nombre;
            clienter.Apellido = clientes.Apellido;
            clienter.Correo = clientes.Correo;
            clienter.Telefono = clientes.Telefono;
            clienter.Direccion = clientes.Direccion;
            clienter.FechaRegistro = DateTime.Now;
            clienter.Contrasena = clientes.Contrasena;
            clienter.iDCliente = System.Guid.NewGuid();
            var jsonObj = JsonConvert.SerializeObject(clienter);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://paladarweb.somee.com/Api/api/Clientes"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Clientes result = JsonConvert.DeserializeObject<Clientes>(dataResult);
                return result;
            }
        }
        public async Task<Lineas> postLinea(Lineas linea)
        {
            Lineas linear = new Lineas();
            linear.Producto = linea.Producto;
            linear.Cantidad = linea.Cantidad;
            linear.Precio = linea.Precio;            
            linear.iDVenta = linea.iDVenta;
            var jsonObj = JsonConvert.SerializeObject(linear);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://paladarweb.somee.com/Api/api/Lineas"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Lineas result = JsonConvert.DeserializeObject<Lineas>(dataResult);
                return result;
            }
        }
        public async Task<Cabecera> postCabecera(Cabecera cabecera)
        {
            Cabecera cabecerar = new Cabecera();
            cabecerar.iDCliente = cabecera.iDCliente;
            cabecerar.iDVenta = cabecera.iDVenta;
            cabecerar.FechaVenta = cabecera.FechaVenta;
            cabecerar.SubTotal = cabecera.SubTotal;
            cabecerar.Monto = cabecera.Monto;
            cabecerar.Lineas = cabecera.Lineas;
            cabecerar.Status = cabecera.Status;
            cabecerar.Metodo = cabecera.Metodo;
            cabecerar.TipoVenta = cabecera.TipoVenta;
            cabecerar.Direccion = cabecera.Direccion;
            var jsonObj = JsonConvert.SerializeObject(cabecerar);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://paladarweb.somee.com/Api/api/Cabeceras"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Cabecera result = JsonConvert.DeserializeObject<Cabecera>(dataResult);
                return result;
            }
        }        
        public async Task<Lineas> deleteLinea(Lineas linea)
        {
            string iD = linea.Row.ToString();
            Lineas linear = new Lineas();
            var jsonObj = JsonConvert.SerializeObject(linear);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://paladarweb.somee.com/Api/api/Lineas/" + iD+""),
                    Method = HttpMethod.Delete,
                    Content = content

                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Lineas result = linear;
                return result;
            }
        }
        public async Task<Pagos> postPagos(Pagos pagos)
        {
            Pagos pagosr = new Pagos();
            pagosr.Metodo = pagos.Metodo;
            pagosr.iDVenta = pagos.iDVenta;
            pagosr.Correo = pagos.Correo;
            var jsonObj = JsonConvert.SerializeObject(pagosr);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://paladarweb.somee.com/Api/api/Pagos"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Pagos result = JsonConvert.DeserializeObject<Pagos>(dataResult);
                return result;
            }
        }
        public async Task<Cantidad> postCantidad(Cantidad cantidad)
        {
            Cantidad cantidadr = new Cantidad();
            cantidadr.TagDesc = cantidad.TagDesc;
            cantidadr.CantidadDesc = cantidad.CantidadDesc;
            var jsonObj = JsonConvert.SerializeObject(cantidadr);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://paladarweb.somee.com/Api/api/Cantidad"),
                    Method = HttpMethod.Post,
                    Content = content
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                Cantidad result = JsonConvert.DeserializeObject<Cantidad>(dataResult);
                return result;
            }
        }
    }
}
