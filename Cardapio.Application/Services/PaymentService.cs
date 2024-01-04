using AutoMapper;
using Cardapio.Application.DTOs;
using Cardapio.Application.Services.Interface;
using Cardapio.Application.Validators;
using Cardapio.DB.Entiites;
using Cardapio.DB.Notifiers;
using Cardapio.DB.Notifiers.Interface;
using Cardapio.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cardapio.Application.Services
{
    public class PaymentService : IPaymentService
    {
        public IResponseMessage responseMessage { get; }
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            this._paymentRepository = paymentRepository;
            this._mapper = mapper;
            responseMessage = new ResponseMessage();
        }

        public async Task CreateAsync(AddPaymentDTO entity)
        {
            if (!Validate(entity))
            {
                return;
            }
            var temp = _mapper.Map<Payment>(entity);
            await _paymentRepository.CreateAsync(temp);
            if (!_paymentRepository.responseMessage.IsValid)
            {
                foreach (var item in _paymentRepository.responseMessage.Erros)
                {
                    responseMessage.AddErros(item);
                }
            }
        }

        public async Task<IEnumerable<ReadPaymentDTO>> GetAllAsync(Expression<Func<Payment, bool>> predicate)
        {
            var temp = await _paymentRepository.GetAllAsync(predicate);
            return _mapper.Map<IEnumerable<ReadPaymentDTO>>(temp);
        }

        public async Task<ReadPaymentDTO> GetAsync(Expression<Func<Payment, bool>> predicate)
        {
            var temp = await _paymentRepository.GetAsync(predicate);
            if (temp != null)
            {
                return _mapper.Map<ReadPaymentDTO>(temp);
            }
            return null;
        }

        public bool Validate(AddPaymentDTO entity)
        {
            var result = new PaymentValidator().Validate(entity);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    responseMessage.AddErros(item.ErrorMessage);
                }
                return false;
            }
            return true;
        }
    }
}
