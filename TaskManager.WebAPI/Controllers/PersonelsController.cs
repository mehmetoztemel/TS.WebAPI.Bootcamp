using Microsoft.AspNetCore.Mvc;
using TaskManager.WebAPI.Dtos;
using TaskManager.WebAPI.Extensions;
using TaskManager.WebAPI.Models;

namespace TaskManager.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class PersonelsController : ControllerBase
{
    public static List<Personel> Personels = new List<Personel>();

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Personels);
    }

    [HttpPost]
    public IActionResult Create([FromForm] CreatePersonelDto request)
    {

        string fileName = request.File.FileName.CreateFileName();
        using (var stream = System.IO.File.Create($"wwwroot/avatars/{fileName}"))
        {
            request.File.CopyTo(stream);
        }

        Personel personel = new Personel()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.Now,
            AvatarUrl = fileName
        };
        Personels.Add(personel);

        return Created();
    }


    [HttpDelete]
    public IActionResult DeleteById(Guid id)
    {
        Personel? personel = Personels.FirstOrDefault(x => x.Id == id);
        if (personel is null)
        {
            return NotFound();
        }
        System.IO.File.Delete($"wwwroot/avatars/{personel.AvatarUrl}");
        Personels.Remove(personel);
        return Ok(new { Message = "Personel kaydı başarıyla silindi" });

    }


    [HttpPut]
    public IActionResult Update([FromForm] UpdatePersonelDto request)
    {
        Personel? personel = Personels.FirstOrDefault(x => x.Id == request.Id);

        if (personel is null)
        {
            return BadRequest(new { Message = "Personel bulunamadı" });
        }
        personel.FirstName = request.FirstName;
        personel.LastName = request.LastName;
        if (request.File is not null)
        {
            // değiştirdiğimiz dosyaların yer kaplamasını önlemek amacıyla eski dosyayı siliyoruz
            System.IO.File.Delete($"wwwroot/avatars/{personel.AvatarUrl}");
            string fileName = request.File.FileName.CreateFileName();
            using (var stream = System.IO.File.Create($"wwwroot/avatars/{fileName}"))
            {
                request.File.CopyTo(stream);
            }
            personel.AvatarUrl = fileName;
        }
        return Ok(new { Message = "Personel kaydı başarıyla güncellendi" });
    }
}