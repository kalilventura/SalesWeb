﻿using SalesWeb.Domain.DTO;
using SalesWeb.Domain.Entities;
using SalesWeb.Domain.Handlers.Interfaces;
using SalesWeb.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Domain.Handlers
{
    public class SellerHandler : ISellerHandler
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public SellerHandler(ISellerRepository sellerRepository,
                             IDepartmentRepository departmentRepository)
        {
            _sellerRepository = sellerRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IGenericResult> Add(SellerDto sellerDto)
        {
            var department = await _departmentRepository.FindById(new Guid(sellerDto.DepartmentId));
            if(department == null)
                return new GenericResult(false, "Departamento não existe.", null);

            var newSeller = new Seller
            {
                BaseSalary = sellerDto.BaseSalary,
                BirthDate = sellerDto.BirthDate,
                Email = sellerDto.Email,
                Name = sellerDto.Name,
                Department = department
            };
            return new GenericResult(true, "Vendedor inserido com sucesso.", await _sellerRepository.Add(newSeller));
        }

        public async Task<IGenericResult> Delete(Guid id)
        {
            var seller = await _sellerRepository.FindById(id);
            if (seller == null)
                return new GenericResult(false, "Vendedor não existe.");

            await _sellerRepository.Delete(seller);
            return new GenericResult(true, "Vendedor excluido com sucesso.", null);
        }

        public async Task<IGenericResult> FindAll()
        {
            var result = await _sellerRepository.FindAll();

            if (!result.Any())
                return new GenericResult(true, "Não existem vendedores cadastrados no sistema.");

            return new GenericResult(true, result);
        }

        public async Task<IGenericResult> FindAll(int currentPage, int pageSize)
        {
            var pagination = await _sellerRepository.FindAll(currentPage, pageSize);

            if (!pagination.Results.Any())
                return new GenericResult(true, "Não existem departamentos cadastrados no sistema.");

            return new GenericResult(true, pagination);
        }

        public async Task<IGenericResult> FindById(Guid id)
        {
            var result = await _sellerRepository.FindById(id);

            if (result == null)
                return new GenericResult(true, "Vendedor não encontrado.");

            return new GenericResult(true, result);
        }

        public async Task<IGenericResult> Update(SellerDto sellerDto)
        {

            var department = await _departmentRepository.FindById(new Guid(sellerDto.DepartmentId));
            if(department == null)
                return new GenericResult(true, "Departamento não encontrado.");

            var seller = new Seller 
            {
                Id = new Guid(sellerDto.Id),
                Name = sellerDto.Name,
                BaseSalary = sellerDto.BaseSalary,
                BirthDate = sellerDto.BirthDate,
                Email = sellerDto.Email,
                Department = department,
            };

            await _sellerRepository.Update(seller);

            return new GenericResult(true, "Vendedor excluido com sucesso.");
        }
    }
}