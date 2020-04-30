﻿using Core.Interfaces;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private IGenricRepository<T> _entity;
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IGenricRepository<T> Entity 
        {
            get 
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
