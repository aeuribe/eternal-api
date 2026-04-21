using eternal_api.Application.Assignments.Interfaces;
using eternal_api.Application.Invoices.Interfaces;
using eternal_api.Application.Orders.Interfaces;
using eternal_api.Infraestructure.Persistence;

namespace eternal_api.Infraestructure.Repositories
{
    public class UnitOfWork : IOrderUnitOfWork, IInvoiceUnitOfWork
    {
        private readonly AppDbContext _context;

        public IOrderRepository OrderRepository { get; }

        public IInvoiceRepository InvoiceRepository { get; }

        public IAssignmentRepository AssignmentRepository { get; }

        public UnitOfWork(
            AppDbContext context,
            IOrderRepository orderRepository,
            IInvoiceRepository invoiceRepository,
            IAssignmentRepository assignmentRepository)
        {
            _context = context;
            OrderRepository = orderRepository;
            InvoiceRepository = invoiceRepository;
            AssignmentRepository = assignmentRepository;
        }
        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
