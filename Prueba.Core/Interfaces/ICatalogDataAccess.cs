using Prueba.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Core.Interfaces
{
    public interface ICatalogDataAccess
    {
        public CatalogDto GetCatalog(int id);

        public CatalogDto GetCatalogByName(string name);

        public List<CatalogDto> GetListCatalog();

        public CatalogDto CreateCatalog(CatalogDto request);

        public CatalogDto UpdateCatalog(CatalogDto request);

        public CatalogDto DeleteCatalog(int id);

    }
}
