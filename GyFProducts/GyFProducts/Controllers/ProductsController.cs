using GyFProducts.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace GyFProducts.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]

    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _productsInMemoryStore = new List<Product>();

        #region publics

        public ProductsController()
        {
            if (_productsInMemoryStore.Count == 0)
                _productsInMemoryStore.AddRange(CreateListOfSamples2());
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Product> Create(Product product)
        {
            BadRequestObjectResult badRequest = ValidateProduct(product);
            if (badRequest != null) return badRequest;

            product.Id = _productsInMemoryStore.Any() ? _productsInMemoryStore.Max(c => c.Id) + 1 : 1;

            _productsInMemoryStore.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }


        [HttpGet]
        public ActionResult<List<Product>> GetAll() => _productsInMemoryStore;

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetById(int id)
        {
            var record = GetProduct(id);

            if (record == null) return NotFound();

            return record;
        }

        [HttpGet("precio/{maxPrecio}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Product> GetBestByPrecio(int maxPrecio)
        {
            if (maxPrecio < 0 || maxPrecio > 1000000) return BadRequest("El precio máximo debe ser entre 0 y 1.000.000");

            List<Product> records = GetBestProductsByPrecio(maxPrecio);

            if (records.Count == 2) return Ok(records);

            return Ok();
        }

        #endregion

        #region privates

        private List<Product> GetBestProductsByPrecio(int maxPrecio)
        {
            List<Product> productos = new List<Product>();

            List<Product> products1 = _productsInMemoryStore.Where(p => p.Categoria == CategoriaProducto.PRODUNO).ToList();
            List<Product> products2 = _productsInMemoryStore.Where(p => p.Categoria == CategoriaProducto.PRODDOS).ToList();

            Product bestProduct1 = null;
            Product bestProduct2 = null;
            double bestPrecio = 0;
            double precioActual;

            foreach (Product product1 in products1)
            {
                foreach (Product product2 in products2)
                {
                    precioActual = product1.Precio + product2.Precio;

                    if (precioActual <= maxPrecio && precioActual > bestPrecio)
                    {
                        bestPrecio = precioActual;
                        bestProduct1 = product1;
                        bestProduct2 = product2;
                    }

                }
            }

            if (bestProduct1 != null) productos.Add(bestProduct1);
            if (bestProduct2 != null) productos.Add(bestProduct2);

            return productos;
        }

        private Product GetProduct(int id)
        {
            return _productsInMemoryStore.FirstOrDefault(c => c.Id == id);
        }

        private BadRequestObjectResult ValidateProduct(Product product)
        {
            string badRequestError = string.Empty;

            if (product.Precio < 0) badRequestError += "El precio debe ser mayor a 0. ";

            if (product.FechaCarga == DateTime.MinValue) badRequestError += "Fecha invalida. ";

            if (badRequestError.Length > 1) return BadRequest(badRequestError);

            return null;
        }

        private List<Product> CreateListOfSamples()
        {
            List<Product> productsSamples = new List<Product>();

            Product product1 = new Product
            {
                Id = 1,
                Precio = 10,
                FechaCarga = new DateTime(2019, 10, 25),
                Categoria = CategoriaProducto.PRODDOS
            };

            Product product2 = new Product
            {
                Id = 2,
                Precio = 60,
                FechaCarga = new DateTime(2019, 10, 21),
                Categoria = CategoriaProducto.PRODUNO
            };

            Product product3 = new Product
            {
                Id = 3,
                Precio = 5,
                FechaCarga = new DateTime(2019, 10, 22),
                Categoria = CategoriaProducto.PRODDOS
            };

            Product product4 = new Product
            {
                Id = 4,
                Precio = 5,
                FechaCarga = new DateTime(2019, 10, 29),
                Categoria = CategoriaProducto.PRODUNO
            };

            Product product5 = new Product
            {
                Id = 5,
                Precio = 15,
                FechaCarga = new DateTime(2019, 9, 11),
                Categoria = CategoriaProducto.PRODDOS
            };

            productsSamples.Add(product1);
            productsSamples.Add(product2);
            productsSamples.Add(product3);
            productsSamples.Add(product4);
            productsSamples.Add(product5);

            return productsSamples;
        }

        private List<Product> CreateListOfSamples2()
        {
            List<Product> productsSamples = new List<Product>();

            Product product1 = new Product
            {
                Id = 1,
                Precio = 50,
                FechaCarga = new DateTime(2019, 10, 25),
                Categoria = CategoriaProducto.PRODDOS
            };

            Product product2 = new Product
            {
                Id = 2,
                Precio = 44,
                FechaCarga = new DateTime(2019, 10, 21),
                Categoria = CategoriaProducto.PRODUNO
            };

            Product product3 = new Product
            {
                Id = 3,
                Precio = 40,
                FechaCarga = new DateTime(2019, 10, 22),
                Categoria = CategoriaProducto.PRODDOS
            };

            Product product4 = new Product
            {
                Id = 4,
                Precio = 5,
                FechaCarga = new DateTime(2019, 10, 29),
                Categoria = CategoriaProducto.PRODUNO
            };

            Product product5 = new Product
            {
                Id = 5,
                Precio = 15,
                FechaCarga = new DateTime(2019, 9, 11),
                Categoria = CategoriaProducto.PRODDOS
            };

            productsSamples.Add(product1);
            productsSamples.Add(product2);
            productsSamples.Add(product3);
            productsSamples.Add(product4);
            productsSamples.Add(product5);

            return productsSamples;
        }
        #endregion

    }
}
