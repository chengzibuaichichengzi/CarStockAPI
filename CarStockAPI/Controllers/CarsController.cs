using AutoMapper;
using CarStockAPI.Data;
using CarStockAPI.DTOs;
using CarStockAPI.Models;
using CarStockAPI.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using CarStockAPI.QueryFilters;
using Microsoft.AspNetCore.Authentication;
using CarStockAPI.Auth;
using Microsoft.Net.Http.Headers;

namespace CarStockAPI.Controllers
{
    [Route("api/cars")]
    [ApiController]
    //[APIKeyAuthFilter]
    [CustomTokenAuthFilter]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICustomTokenManager _tokenManager;

        public CarsController(ICarRepo repository, IMapper mapper, ICustomTokenManager tokenManager)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        //GET api/cars
        [HttpGet]
        public ActionResult<IEnumerable<CarReadDto>> GetAllCars([FromQuery] CarQueryFilter carQueryFilter)
        {
            // Get dealer client Id from token
            var token = Request.Headers["tokenheader"].ToString();
            string dealerClientId = _tokenManager.GetUserInfoByToken(token);

            // Each dealer can only view own cars
            var cars = _repository.GetAllCars(dealerClientId);

            // Search cars by Make and Model from request query
            if (carQueryFilter != null)
            {
                if (!string.IsNullOrWhiteSpace(carQueryFilter.Make))
                {
                    // Ignore case when search car by make
                    cars = cars.Where(c => c.Make.Contains(carQueryFilter.Make, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(carQueryFilter.Model))
                {
                    // Ignore case when search car by model
                    cars = cars.Where(c => c.Model.Contains(carQueryFilter.Model, StringComparison.OrdinalIgnoreCase));
                }
            }

            return Ok(_mapper.Map<IEnumerable<CarReadDto>>(cars));
        }

        //GET api/car/{id}
        [HttpGet("{id}", Name = "GetCarById")]
        public ActionResult <CarReadDto> GetCarById(Guid id) 
        {
            // Get dealer client Id from token
            var token = Request.Headers["tokenheader"].ToString();
            string dealerClientId = _tokenManager.GetUserInfoByToken(token);

            // Each dealer can only view own cars
            var car = _repository.GetCarById(id, dealerClientId);

            if (car != null)
            {
                return Ok(_mapper.Map<CarReadDto>(car));
            }
            else
            {
                return NotFound();
            }
        }

        //POST api/cars 
        [HttpPost]
        public ActionResult <CarReadDto> AddCar(CarAddDto carAddDto) {
            // Get dealer client Id from token
            var token = Request.Headers["tokenheader"].ToString();
            string dealerClientId = _tokenManager.GetUserInfoByToken(token);

            var carModel = _mapper.Map<Car>(carAddDto);

            // When add new car, auto assign current dealer's client id
            _repository.AddCar(carModel, dealerClientId);
            _repository.SaveChanges();

            var carReadDto = _mapper.Map<CarReadDto>(carModel);

            // Include the URI of the new created resource
            return CreatedAtRoute(nameof(GetCarById), new { Id = carReadDto.Id }, carReadDto);
        }

        //PUT api/cars/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCarStockLevel(Guid id, CarUpdateDto carUpdateDto)
        { 
            // Get dealer client Id from token
            var token = Request.Headers["tokenheader"].ToString();
            string dealerClientId = _tokenManager.GetUserInfoByToken(token);

            // Each dealer can only update own cars' stock level
            var carModel = _repository.GetCarById(id, dealerClientId);
            if (carModel == null)
            {
                return NotFound();
            }
            _mapper.Map(carUpdateDto, carModel);

            _repository.UpdateCarStock(carModel, dealerClientId);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/cars/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCar(Guid id)
        {
            // Get dealer client Id from token
            var token = Request.Headers["tokenheader"].ToString();
            string dealerClientId = _tokenManager.GetUserInfoByToken(token);

            // Each dealer can only delete own cars
            var carModel = _repository.GetCarById(id, dealerClientId);
            if(carModel == null)
            {
                return NotFound();
            }

            _repository.DeleteCar(carModel);
            _repository.SaveChanges();

            var carReadDto = _mapper.Map<CarReadDto>(carModel);

            return Ok(carReadDto);
        }
    }
}
