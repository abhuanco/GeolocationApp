using GeolocationApp.Application.Interfaces;
using GeolocationApp.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GeolocationApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitController(IVisitService visitService, IGeolocationService geolocationService, ICurrencyService currencyService) : ControllerBase
    {
        [EndpointName("List")]
        [EndpointSummary("List")]
        [EndpointDescription("Get list of visits with pagination")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string? filter = null)
        {
            var (data, totalCount) = await visitService.GetPagedVisitsAsync(
                pageIndex,
                pageSize,
                filter: v => string.IsNullOrEmpty(filter) || v.Country.Contains(filter)
            );

            return Ok(new
            {
                data,
                totalCount
            });
        }

        [EndpointName("Find")]
        [EndpointSummary("Find")]
        [EndpointDescription("Obtain visit by id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var visit = await visitService.GetVisitByIdAsync(id);
            if (visit == null)
                return NotFound();

            return Ok(visit);
        }

        [EndpointName("Geolocation")]
        [EndpointSummary("Geolocation")]
        [EndpointDescription(
            "The endpoint is responsible for recording the geolocation of the user who enters the page. If your geolocation is not found, it will give a 404 error saying that it did not find your country code.")]
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var geolocation = await geolocationService.GetGeoLocationAsync();
            await currencyService.LoadCurrenciesAsync();
            var currency = currencyService.GetCurrencyByCodeAsync(geolocation.Currency);
            var visitRequest = new UpdateVisit
            {
                Country = geolocation.Name,
                Emoji = geolocation.Emoji,
                Latitude = geolocation.Latitude,
                Longitude = geolocation.Longitude,
                VisitDate = DateTime.UtcNow,
                Ip = geolocation.Ip
            };
            if (currency != null)
            {
                visitRequest.Currency = geolocation.Currency;
                visitRequest.CurrencyName = currency.Name;
                visitRequest.Symbol = currency.Symbol;
            }

            var visit = await visitService.CreateVisitAsync(visitRequest);

            return CreatedAtAction(nameof(Get), new { id = visit.Id }, visit);
        }

        [EndpointName("Update")]
        [EndpointSummary("Update")]
        [EndpointDescription("Update visit by id  and data visit")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisit(Guid id, [FromBody] UpdateVisit updateVisitUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var updatedVisit = await visitService.UpdateVisitAsync(id, updateVisitUpdate);
            return Ok(updatedVisit);
        }

        [EndpointName("Delete")]
        [EndpointSummary("Delete")]
        [EndpointDescription("Delete visit by id uuid")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisit(Guid id)
        {
            var result = await visitService.DeleteVisitAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
