﻿using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.DTOs.Supply;
using DiyorMarket.Domain.Entities;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DiyorMarket.Controllers
{
    [Route("api/supplies")]
    [ApiController]
    [Authorize]
    public class SuppliesController : Controller
    {      
        private readonly ISupplyService _supplyService;
        public SuppliesController(ISupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SupplyDto>> GetSuppliesAsync(
            [FromQuery] SupplyResourceParameters supplyResourceParameters)
        {
            var supplies = _supplyService.GetSupplies(supplyResourceParameters);

            return Ok(supplies);
        }

        [HttpGet("{id}", Name = "GetSupplyById")]
        public ActionResult<SupplyDto> Get(int id)
        {
            var supply = _supplyService.GetSupplyById(id);

            if (supply is null)
            {
                return NotFound($"Supply with id: {id} does not exist.");
            }

            return Ok(supply);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplyForCreateDto supply)
        {
            _supplyService.CreateSupply(supply);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplyForUpdateDto supply)
        {
            if (id != supply.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supply.Id}.");
            }

            _supplyService.UpdateSupply(supply);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _supplyService.DeleteSupply(id);

            return NoContent();
        }
    }
}
