using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWeb.Domain.Entities;
using SalesWeb.Domain.Repositories;
using SalesWeb.Infra.Database;

namespace SalesWeb.Infra.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly Context _context;
        public SellerRepository(Context context)
        {
            _context = context;
        }

        public async Task Add(Seller entity)
        {
            await _context.Seller.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Seller entity)
        {
            _context.Seller.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Seller>> FindAll()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task<Seller> FindById(Guid id)
        {
            return await _context.Seller.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Seller entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}