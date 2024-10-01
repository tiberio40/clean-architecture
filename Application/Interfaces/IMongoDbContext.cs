using Core.DTOs.TmetricDtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMongoDbContext
    {
        public IMongoCollection<LogDto> Logs { get; }
    }
}
