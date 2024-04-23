using Prueba.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Core.Interfaces
{
    public interface IProductsDataAccess
    {

        public ProductsDto CreateProducts(ProductsDto request);

        public string CreateRange(List<ProductsDto> request);

        public ProductsDto EditProducts(ProductsDto request);

        public ProductsDto DeleteProducts(int id);

        public List<ProductsDto> GetProductsByOrder(int orderid);

        public ProductsDto GetProductsById(int id);

    }
}
