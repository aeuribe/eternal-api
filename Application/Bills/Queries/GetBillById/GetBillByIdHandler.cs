using eternal_api.Application.Common.DTOs;
using eternal_api.Application.Common.Interfaces;

namespace eternal_api.Application.Bills.Queries.GetBillById
{
    // 🔹 Handler: encargado de procesar una Query
    public class GetBillByIdHandler
    {
        private readonly IBillRepository _billRepository;

        // Inyección de dependencias: el handler no accede directamente a la base de datos,
        // sino que depende de un contrato (IBillRepository).
        // Así mantenemos el principio de inversión de dependencias de Clean Architecture.
        public GetBillByIdHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        // Método que procesa la Query.
        public async Task<BillDto?> Handle(GetBillByIdQuery query)
        {
            // Buscar la factura en la base de datos mediante el repositorio.
            var bill = await _billRepository.GetByIdAsync(query.Id);

            // Si no existe ninguna factura con ese Id → devolvemos null.
            if (bill == null) return null;

            /*
             Si existe, convertimos la entidad de dominio (Bill) en un DTO (BillDto).
             
             No debemos devolver directamente la entidad de dominio (Bill),
                porque esta contiene información interna que no debería ser expuesta.
             
             En su lugar, usamos un DTO (Data Transfer Object):
                - Simplifica la respuesta.
                - Expone solo los datos necesarios.
                - Protege la capa de dominio de modificaciones externas.
             */

            return new BillDto
            {
                Id = bill.Id,
                Total = bill.Total,
                CreatedAt = bill.CreatedAt
            };
        }
    }
}
