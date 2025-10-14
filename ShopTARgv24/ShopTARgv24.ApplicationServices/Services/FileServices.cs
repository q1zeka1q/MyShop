﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly ShopTARgv24Context _context;
        private readonly IHostEnvironment _webHost;

        public FileServices
            (
                ShopTARgv24Context context,
                IHostEnvironment webHost
            )
        {
            _context = context;
            _webHost = webHost;
        }

        public void FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    //muutuja string uploadsFolder ja sinna laetakse failid
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                    //muutuja string uniqueFileName ja siin genereeritakse uus Guid ja lisatakse see faili ette
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.Name;
                    //muutuja string filePath kombineeritakse ja lisatakse koos kausta unikaalse nimega
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            SpaceshipId = spaceship.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
            }
        }

public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
{
    //meil on vaja leida file andmebaasist läbi id ülesse
    var imageId = await _context.FileToApis
        .FirstOrDefaultAsync(x => x.Id == dto.Id);

    var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"
        + imageId.ExistingFilePath;

    //kui fail on olemas, siis kustuta ära
    if (File.Exists(filePath))
    {
        File.Delete(filePath);
    }

    _context.FileToApis.Remove(imageId);
    await _context.SaveChangesAsync();

    return imageId;
}

public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
{
    //foreach, mille sees toimub failide kustutamine
    foreach (var dto in dtos)
    {
        var imageId = await _context.FileToApis
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"
            + imageId.ExistingFilePath;

        //kui fail on olemas, siis kustuta ära
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        _context.FileToApis.Remove(imageId);
        await _context.SaveChangesAsync();
    }

    return null;
}

public void UploadFilesToDatabase(KindergardenDto dto, Kindergarden domain)
{
    //tuleb ära kontrollida, kas on üks fail või mitu
    if (dto.Files != null && dto.Files.Count > 0)
    {
        //kui tuleb mitu faili, siis igaks juhuks tuleks kasutada foreachi
        foreach (var file in dto.Files)
        {
            //foreachi sees kasutada using-t ja ära mappida
            using (var target = new MemoryStream())
            {
                FileToDatabase files = new FileToDatabase()
                {
                    Id = Guid.NewGuid(),
                    ImageTitle = file.FileName,
                    KindergardenId = domain.Id
                };
                //salvestada andmed andmebaasi
                file.CopyTo(target);
                files.ImageData = target.ToArray();

                _context.FileToDatabase.Add(files);
                }
            }
        }
        }
      public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            var imageId = await _context.FileToDatabase
                .FirstOrDefaultAsync(x => x.Id == dto.ImageId);

            if (imageId != null)
            {
                _context.FileToDatabase.Remove(imageId);
                await _context.SaveChangesAsync();

                return imageId;
            }

            return null;
        }

        // Eemaldab kõik faile andmebaasist
        public async Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var imageId = await _context.FileToDatabase
                    .FirstOrDefaultAsync(x => x.Id == dto.ImageId);

                if (imageId != null)
                {
                    _context.FileToDatabase.Remove(imageId);
                    await _context.SaveChangesAsync();
                }
            }
            return null;
        }
    }
}