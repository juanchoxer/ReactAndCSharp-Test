using System;
using System.ComponentModel.DataAnnotations;

namespace GyFProducts.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public DateTime FechaCarga { get; set; }
        [Required]
        public CategoriaProducto Categoria { get; set; }
    }

    public enum CategoriaProducto
    {
        PRODUNO,
        PRODDOS
    }
}
