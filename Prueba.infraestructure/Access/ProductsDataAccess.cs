using AutoMapper;
using Prueba.Core.DTOs;
using Prueba.Core.Entities;
using Prueba.Core.Interfaces;
using Prueba.Core.Utilities;
using Prueba.infraestructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace Prueba.infraestructure.Access
{
    public class ProductsDataAccess : IProductsDataAccess
    {
        protected DBStoreTestContext context;
        private readonly IMapper mapper;

        public ProductsDataAccess(DBStoreTestContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public ProductsDto CreateProducts(ProductsDto request)
        {
            var products = mapper.Map<Products>(request);
            context.Products.Add(products);
            context.SaveChanges();
            var Result = mapper.Map<ProductsDto>(products);
            return Result;
        }

        public ProductsDto EditProducts(ProductsDto request)
        {

            var products = context.Products.FirstOrDefault(x => x.Id == request.Id);
            if (products != null)
            {
                // Campos a actualizar
                FrameworkTypeUtility.SetProperties(request, products);

                // Guardar cambios
                context.SaveChanges();
                var Result = mapper.Map<ProductsDto>(request);
                return Result;
            }
            else
            {
                return new ProductsDto();
            }
            
            
        }

        public ProductsDto DeleteProducts(int id)
        {

            var products = context.Products.FirstOrDefault(x => x.Id == id);                       
            if (products != null)
            {
                context.Products.Remove(products);
                context.SaveChanges();
                return new ProductsDto();
            }
            else
            {
                return new ProductsDto();
            }


        }

        public List<ProductsDto> GetProductsByOrder(int orderid)
        {
            List<Products> entidad = context.Products.Where(x => x.OrderId == orderid).ToList();
            return mapper.Map<List<ProductsDto>>(entidad);            

        }

        public ProductsDto GetProductsById(int id)
        {
            Products entidad = context.Products.FirstOrDefault(x => x.Id == id);
            return mapper.Map<ProductsDto>(entidad);

        }

    }
}
