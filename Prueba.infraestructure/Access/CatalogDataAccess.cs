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
    public class CatalogDataAccess : ICatalogDataAccess
    {
        protected DBStoreTestContext context;
        private readonly IMapper mapper;

        public CatalogDataAccess(DBStoreTestContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public CatalogDto GetCatalog(int id)
        {
            Catalog catalog = new Catalog();
            catalog = context.Catalog.FirstOrDefault(x => x.Id == id);           
            return mapper.Map<CatalogDto>(catalog);

        }

        public CatalogDto GetCatalogByName(string name)
        {
            Catalog catalog = new Catalog();
            catalog = context.Catalog.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            return mapper.Map<CatalogDto>(catalog);

        }

        public List<CatalogDto> GetListCatalog()
        {
            
            List<Catalog> entidad = context.Catalog.ToList();
            return mapper.Map<List<CatalogDto>>(entidad);

        }

        public CatalogDto CreateCatalog(CatalogDto request)
        {
            var catalog = mapper.Map<Catalog>(request);
            context.Catalog.Add(catalog);
            context.SaveChanges();
            var Result = mapper.Map<CatalogDto>(catalog);
            return Result;
        }

        public CatalogDto UpdateCatalog(CatalogDto request)
        {

            var catalog = context.Catalog.FirstOrDefault(x => x.Id == request.Id);
            if (catalog != null)
            {
                // Campos a actualizar
                FrameworkTypeUtility.SetProperties(request, catalog);

                // Guardar cambios
                context.SaveChanges();
                var Result = mapper.Map<CatalogDto>(request);
                return Result;
            }
            else
            {
                return new CatalogDto();
            }
                        
        }

        public CatalogDto DeleteCatalog(int id)
        {

            var catalog = context.Catalog.FirstOrDefault(x => x.Id == id);            
            if (catalog != null)
            {
                context.Catalog.Remove(catalog);
                context.SaveChanges();
                return new CatalogDto();
            }
            else
            {
                return new CatalogDto();
            }


        }

    }
}
