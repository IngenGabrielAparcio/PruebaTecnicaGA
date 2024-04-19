
using Prueba.Core.DTOs;
using Prueba.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Core.Interfaces
{
    public interface IProductsServices
    {        

        public ResponseQuery<ProductsDto> CreateProducts(ProductsDto request, ResponseQuery<ProductsDto> response);

        public ResponseQuery<ProductsDto> EditProducts(ProductsDto request, ResponseQuery<ProductsDto> response);

        public ResponseQuery<ProductsDto> DeleteProducts(int id, ResponseQuery<ProductsDto> response);

    }
}
