using eternal_api.Application.Common.Interfaces;
using eternal_api.Domain.Entities;

namespace eternal_api.Application.Bills.Commands.CreateBill
{
    public class CreateBillHandler
    {
        private readonly IBillRepository _billRepository;

        public CreateBillHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task<Guid> Handle(CreateBillCommand command)
        {
            var bill = new Bill(command.OrderId, command.Total);

            await _billRepository.AddAsync(bill);

            /*
             El ID se crea en el constructor con el uso de Guid,
             por eso existe un Id que se puede retornar sin esperar
             la respuesta de la inserción en el repositorio
            */
            return bill.Id;
        }
    }
}
