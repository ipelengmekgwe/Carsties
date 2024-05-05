using AuctionService.DTOs;
using AuctionService.Entities;
using AuctionService.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuctionsController(IAuctionsService auctionsService, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        var auctions = await auctionsService.GetAuctionsAsync();

        return mapper.Map<List<AuctionDto>>(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await auctionsService.GetAuctionByIdAsync(id);

        if (auction == null) return NotFound();

        return mapper.Map<AuctionDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        var auction = mapper.Map<Auction>(auctionDto);
        // TODO: add current user as seller
        auction.Seller = "test";

        var saved = await auctionsService.CreateAuctionAsync(auction);

        if (!saved) return BadRequest("Could not save changes to the DB");

        return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, mapper.Map<AuctionDto>(auction));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        // TODO: check seller == username
        (bool success, bool notFound) = await auctionsService.UpdateAuctionAsync(id, updateAuctionDto);

        if (notFound) return NotFound();

        if (!success) return BadRequest("Problem saving changes");

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        (bool success, bool notFound) = await auctionsService.DeleteAuctionAsync(id);

        if (notFound) return NotFound();

        if (!success) return BadRequest("Could not update DB");

        return Ok();
    }
}
