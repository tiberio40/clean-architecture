using Core.Entities;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        Task<IDbContextTransaction> BeginTransactionAsync();

        IRepository<MarketingStatusEntity> MarketingStatusRepository { get; }

        Task<int> Save();
    }
}
