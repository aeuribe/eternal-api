using eternal_api.Application.Assignments.Interfaces;

namespace eternal_api.Application.Orders.Interfaces
{
    public interface IOrderUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }
        IAssignmentRepository AssignmentRepository { get;  }
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        
    }
}
