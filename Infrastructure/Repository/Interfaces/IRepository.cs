﻿using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region IRepository<T> Members

        /// Retorna un objeto del tipo AsQueryable
        IQueryable<TEntity> AsQueryable();

        /// Retorna un objeto del tipo AsQueryable y acepta como parámetro las relaciones a incluir
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);

        /// Función que retorna una entidad, a partir de una consulta.
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        
        /// Retorna la primera entidad encontrada bajo una condición especificada o null sino encontrara registros
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        /// Registra una entidad
        void Insert(TEntity entity);

        /// Registra varias entidades
        void Insert(IEnumerable<TEntity> entities);

        /// Actualiza una entidad
        void Update(TEntity entity);

        /// Actualiza varias entidades
        void Update(IEnumerable<TEntity> entities);

        /// Elimina una entidad
        void Delete(TEntity entity);

        /// Elimina por Id
        void Delete(object id);

        /// Elimina un conjuto de entidades
        void Delete(IEnumerable<TEntity> entities);

        #endregion IRepository<T> Members
    }
}
