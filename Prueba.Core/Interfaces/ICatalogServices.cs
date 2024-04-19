
using Prueba.Core.DTOs;
using Prueba.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Core.Interfaces
{
    public interface ICatalogServices
    {
        public ResponseQuery<CatalogDto> GetCatalog(int id, ResponseQuery<CatalogDto> response);

        public ResponseQuery<List<CatalogDto>> GetListCatalog(ResponseQuery<List<CatalogDto>> response);

        public ResponseQuery<CatalogDto> CreateCatalog(CatalogDto request, ResponseQuery<CatalogDto> response);

        public ResponseQuery<CatalogDto> UpdateCatalog(CatalogDto request, ResponseQuery<CatalogDto> response);

        public ResponseQuery<CatalogDto> DeleteCatalog(int id, ResponseQuery<CatalogDto> response);

    }
}
