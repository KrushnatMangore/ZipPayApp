using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZipPay.Context;
using ZipPay.Exceptions;
using ZipPay.Models;

namespace ZipPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipPayAccountController : ControllerBase
    {
        private readonly ZipPayContext _context;

        public ZipPayAccountController(ZipPayContext context)
        {
            _context = context;
        }

        // GET: api/ZipPayAccounts
        [HttpGet]
        public IEnumerable<ZipPayAccount> GetZipPayAccount()
        {
            var AccountList = _context.ZipPayAccount;
            if (AccountList.ToList().Count > 0)
            {
                return _context.ZipPayAccount;
            }
            else
            {
                throw new NotFoundCustomException("No data found", $"Records not found");
            }
        }

        // GET: api/ZipPayAccount/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetZipPayAccount([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zipPayAccount = await _context.ZipPayAccount.FindAsync(id);

            if (zipPayAccount == null)
            {
                throw new NotFoundCustomException("No data found", $"Please check your parameters id: {id}");
            }

            return Ok(zipPayAccount);
        }

        // POST: api/ZipPayUsers
        [HttpPost]
        public async Task<IActionResult> PostZipPayAccount([FromBody] ZipPayAccount zipPayAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkSaving = _context.ZipPayUser.ToList().FirstOrDefault(p => p.ZipPayId == zipPayAccount.ZipPayId);
            if(Convert.ToUInt32(checkSaving.Salary - checkSaving.Expenses) < 100)
            {
                throw new Exception("You are not allowed to create account");
            }
            else
            {
                _context.ZipPayAccount.Add(zipPayAccount);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetZipPayAccount", new { id = zipPayAccount.AccountId }, zipPayAccount);
            }

        }
    }
}
