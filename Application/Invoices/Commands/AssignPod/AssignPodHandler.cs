using eternal_api.Application.Common.Exceptions;
using eternal_api.Application.Invoices.Interfaces;
using MediatR;

namespace eternal_api.Application.Invoices.Commands.AssignPOD
{
    public class AssignPODHandler : IRequestHandler<AssignPODCommand, bool>
    {
        private readonly IInvoiceUnitOfWork _unitOfWork;
        public AssignPODHandler(IInvoiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AssignPODCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscamos la factura existente
            var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(request.Id);

            if (invoice == null)
            {
                // Usamos la excepción de dominio para que el middleware devuelva un 404
                throw new NotFoundException("Invoice", request.Id);
            }

            // 2. Usamos el método de negocio de tu entidad (Encapsulamiento)
            invoice.AssignPOD(request.POD);

            // 3. Le decimos a Entity Framework que solo actualice la columna POD
            await _unitOfWork.InvoiceRepository.AddPodAsync(invoice);

            // 4. Confirmamos la transacción en la base de datos
            await _unitOfWork.CompleteAsync(cancellationToken);

            return true;
        }
    }
}
