﻿using MaiDan.Accounting.Domain;
using MaiDan.Api.Services;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class SlipBookController : Controller
    {
        private IRepository<Order> orderBook;
        private IRepository<Bill> billBook;
        private IRepository<Slip> slipBook;
        private IRepository<PaymentMethod> paymentMethods;
        private readonly ICashRegister cashRegister;

        public SlipBookController(IRepository<Order> orderBook, IRepository<Bill> billBook, IRepository<Slip> slipBook, IRepository<PaymentMethod> paymentMethods, ICashRegister cashRegister)
        {
            this.orderBook = orderBook;
            this.billBook = billBook;
            this.slipBook = slipBook;
            this.paymentMethods = paymentMethods;
            this.cashRegister = cashRegister;
        }

        [HttpGet("{id}")]
        public ActionResult<DataContracts.Responses.Slip> Get(int id)
        {
            var slip = slipBook.Get(id);

            if (slip == null)
            {
                return NotFound();
            }

            var order = orderBook.Get(id);
            var bill = billBook.Get(id);

            return new DataContracts.Responses.Slip(order, bill, slip);
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.Order> Get()
        {
            return slipBook.GetAll().Select(s => new DataContracts.Responses.Slip(orderBook.Get(s.Id), billBook.Get(s.Id), s));
        }

        [HttpPost]
        public void PayBill([FromBody] DataContracts.Requests.Bill contract)
        {
            var bill = billBook.Get(contract.Id);
            cashRegister.Pay(bill);
        }

        [HttpPut]
        public IActionResult Update([FromBody] DataContracts.Requests.Slip contract)
        {
            var wrappedSlip = ModelFromContract(contract);
            if (wrappedSlip.HasError)
            {
                return BadRequest(wrappedSlip.ErrorMessage);
            }

            cashRegister.AddPayments(wrappedSlip.Model);

            return Ok();
        }

        private ValidationResult<Slip> ModelFromContract(DataContracts.Requests.Slip contract)
        {
            if (contract.Id <= 0)
            {
                return "The contract id of a slip to be updated cannot be 0 or negative";
            }

            if (contract.Payments == null)
            {
                return "The contract payments cannot be null";
            }

            if (!contract.Payments.Any())
            {
                return "The contract payments cannot be empty";
            }

            if (contract.Payments.Any(p => p.Amount <= 0))
            {
                return "Payment amounts cannot be 0 or negative";
            }

            if (contract.Payments.Any(p => p.Amount % 0.01m != 0))
            {
                return "Payment amounts cannot have a fractional part below the cent";
            }

            IList<Payment> payments = contract.Payments.Select(p => new Payment(p.Id, paymentMethods.Get(p.PaymentMethodId), p.Amount)).ToList();

            if (payments.Any(p => p.Method == null))
            {
                return "One or several payment method ids are unknown";
            }

            return new Slip(contract.Id, DateTime.Now, payments);
        }
    }
}
