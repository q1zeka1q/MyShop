using System.Xml;
using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopTARgv24Context _context;

        public RealEstateServices
            (
                ShopTARgv24Context context
            )
        {
            _context = context;
        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            RealEstate realestate = new RealEstate();

            realestate.Id = Guid.NewGuid();
            realestate.Area = dto.Area;
            realestate.Location = dto.Location;
            realestate.RoomNumber = dto.RoomNumber;
            realestate.BuildingType = dto.BuildingType;
            realestate.CreateAt = DateTime.Now;
            realestate.ModifiedAt = DateTime.Now;

            await _context.RealEstate.AddAsync(realestate);
            await _context.SaveChangesAsync();

            return realestate;
        }

        public async Task<RealEstate> DetailAsync(Guid id)
        {
            var result = await _context.RealEstate
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<RealEstate> Delete(Guid id)
        {
            var realestate = await _context.RealEstate
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.RealEstate.Remove(realestate);
            await _context.SaveChangesAsync();

            return realestate;
        }

        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            RealEstate domain = new();

            domain.Id = dto.Id;
            domain.Area = dto.Area;
            domain.Location = dto.Location;
            domain.RoomNumber = dto.RoomNumber;
            domain.BuildingType = dto.BuildingType;
            domain.CreateAt = dto.CreateAt;
            domain.ModifiedAt = DateTime.Now;

            _context.RealEstate.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
