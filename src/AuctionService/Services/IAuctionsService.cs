using AuctionService.DTOs;
using AuctionService.Entities;

namespace AuctionService.Services;

public interface IAuctionsService
{
    public Task<List<Auction>> GetAuctionsAsync();
    public Task<Auction> GetAuctionByIdAsync(Guid id);
    public Task<bool> CreateAuctionAsync(Auction auction);
    public Task<(bool success, bool notFound)> UpdateAuctionAsync(Guid id, UpdateAuctionDto updatedAuction);
    public Task<(bool success, bool notFound)> DeleteAuctionAsync(Guid id);
}
