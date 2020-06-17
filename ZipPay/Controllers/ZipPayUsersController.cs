using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZipPay.Context;
using ZipPay.Exceptions;
using ZipPay.Models;

namespace ZipPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipPayUsersController : ControllerBase
    {
        private readonly ZipPayContext _context;

        public ZipPayUsersController(ZipPayContext context)
        {
            _context = context;
        }

        // GET: api/ZipPayUsers
        [HttpGet]
        public IEnumerable<ZipPayUser> GetZipPayUser()
        {
            var UserList = _context.ZipPayUser;
            if (UserList.ToList().Count > 0)
            {
                return _context.ZipPayUser;
            }
            else
            {
                throw new NotFoundCustomException("No data found", $"Records not found");
            }
        }

        // GET: api/ZipPayUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetZipPayUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zipPayUser = await _context.ZipPayUser.FindAsync(id);

            if (zipPayUser == null)
            {
                throw new NotFoundCustomException("No data found", $"Please check your parameters id: {id}");
            }

            return Ok(zipPayUser);
        }

        // POST: api/ZipPayUsers
        [HttpPost]
        public async Task<IActionResult> PostZipPayUser([FromBody] ZipPayUser zipPayUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _context.ZipPayUser.Where(o => o.Email == zipPayUser.Email).ToList();
            if (existingUser.Count == 0)
            {
                _context.ZipPayUser.Add(zipPayUser);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetZipPayUser", new { id = zipPayUser.ZipPayId }, zipPayUser);
            }
            else
            {
                throw new Exception("This email Id is already registered, Please try with different email id"); 
            }

        }
    }
}