using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Services;

public class AuctionsService(AuctionDbContext context) : IAuctionsService
{
    public async Task<bool> CreateAuctionAsync(Auction auction)
    {
        context.Auctions.Add(auction);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Auction> GetAuctionByIdAsync(Guid id)
    {
        return await context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Auction>> GetAuctionsAsync()
    {
        return await context.Auctions
            .Include(x => x.Item)
            .OrderBy(x => x.Item.Make)
            .ToListAsync();
    }

    public async Task<(bool success, bool notFound)> DeleteAuctionAsync(Guid id)
    {
        var auction = await context.Auctions.FindAsync(id);

        if (auction == null) return (false, true);

        //TODO: check seller == username

        context.Auctions.Remove(auction);

        var result = await context.SaveChangesAsync() > 0;

        return (result, false);
    }

    public async Task<(bool success, bool notFound)> UpdateAuctionAsync(Guid id, UpdateAuctionDto updatedAuction)
    {
        var auction = await context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (auction == null) return (false, true);

        // TODO: check seller == username
        auction.Item.Make = updatedAuction.Make ?? auction.Item.Make;
        auction.Item.Model = updatedAuction.Model ?? auction.Item.Model;
        auction.Item.Color = updatedAuction.Color ?? auction.Item.Color;
        auction.Item.Mileage = updatedAuction.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updatedAuction.Year ?? auction.Item.Year;

        var result = await context.SaveChangesAsync() > 0;

        return (result, false);
    }
}
