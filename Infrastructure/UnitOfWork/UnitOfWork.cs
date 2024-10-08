﻿
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Attributes
        private readonly SqlDbContext _context;
        private bool disposed = false;
        #endregion Attributes

        #region builder
        public UnitOfWork(SqlDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Properties  
        private IRepository<MarketingStatusEntity> marketingStatusRepository;

        #endregion


        #region Members

        public IRepository<MarketingStatusEntity> MarketingStatusRepository
        {
            get
            {
                if (this.marketingStatusRepository == null)
                    this.marketingStatusRepository = new Repository<MarketingStatusEntity>(_context);

                return marketingStatusRepository;
            }
        }

        #endregion

        #region Base
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();
        #endregion
    }
}
