using Api.Controllers.Resources;
using Api.Core;
using Api.Core.Models;
using Api.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Api.Controllers;


[Route("[controller]")]
public class PhysicalPersonController : Controller
{
    private readonly IMapper mapper;

    private readonly IPhysicalPersonRepository repository;

    private readonly IUnitOfWork unitOfWork;

    private readonly IWebHostEnvironment host;


    public PhysicalPersonController(
        IMapper mapper,
        IPhysicalPersonRepository repository,
        IUnitOfWork unitOfWork,
        IWebHostEnvironment host
    )
    {
        this.mapper = mapper;
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.host = host;
    }


    [HttpGet]
    public async Task<IEnumerable<PhysicalPersonResource>> GetPhysicalPersons(PhysicalPersonQueryResource filterResource)
    {
        var filter = mapper.Map<PhysicalPersonQueryResource, PhysicalPersonQuery>(filterResource);
        var queryResult = await repository.GetPhysicalPersons(filter);

        return mapper.Map<IEnumerable<PhysicalPerson>, IEnumerable<PhysicalPersonResource>>(queryResult);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetPhysicalPerson(int id)
    {
        var physicalPerson = await repository.GetPhysicalPerson(id);

        if (physicalPerson == null)
            return NotFound();

        var physicalPersonResource = mapper.Map<PhysicalPerson, PhysicalPersonResource>(physicalPerson);

        return Ok(physicalPersonResource);
    }


    [HttpPost]
    public async Task<IActionResult> CreatePhysicalPerson([FromBody] SavePhysicalPersonResource physicalPersonResource)
    {
        var physicalPerson = mapper.Map<SavePhysicalPersonResource, PhysicalPerson>(physicalPersonResource);

        await repository.CreatePhysicalPerson(physicalPerson);
        await unitOfWork.Complete();

        physicalPerson = await repository.GetPhysicalPerson(physicalPerson.Id);
        var resource = mapper.Map<PhysicalPerson, PhysicalPersonResource>(physicalPerson);

        return Ok(resource);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePhysicalPerson(int id, [FromBody] SavePhysicalPersonResource physicalPersonResource)
    {
        var physicalPerson = await repository.GetPhysicalPerson(id);

        if (physicalPerson == null)
            return NotFound();

        mapper.Map(physicalPersonResource, physicalPerson);

        await unitOfWork.Complete();

        physicalPerson = await repository.GetPhysicalPerson(physicalPerson.Id);
        var vechileResource = mapper.Map<PhysicalPerson, SavePhysicalPersonResource>(physicalPerson);

        return Ok(vechileResource);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePhysicalPerson(int id)
    {
        var physicalPerson = await repository.GetPhysicalPerson(id, includeRelated: false);

        if (physicalPerson == null)
            return NotFound();

        repository.RemovePhysicalPerson(physicalPerson);
        await unitOfWork.Complete();

        return Ok(id);
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetRelationCount(int masterId, Relation relation)
    {
        var physicalPerson = await repository.GetPhysicalPerson(masterId);

        if (physicalPerson == null)
            return NotFound();

        var count = physicalPerson.PhysicalPersonRelations.Where(ppr => ppr.Relation == relation).Count();

        return Ok(count);
    }


    [HttpPost("related-physical-person")]
    public async Task<IActionResult> AddRelatedPhysicalPerson(int masterId, int relatedId, Relation relation)
    {
        var masterPhysicalPerson = await repository.GetPhysicalPerson(masterId);
        var relatedPhysicalPerson = await repository.GetPhysicalPerson(relatedId);

        if (masterPhysicalPerson == null || relatedPhysicalPerson == null)
            return NotFound();

        //TODO dont add master
        if (!masterPhysicalPerson.PhysicalPersonRelations.Any(mpp => mpp.RelatedId == relatedPhysicalPerson.Id))
        {
            masterPhysicalPerson.PhysicalPersonRelations.Add(
                new PhysicalPersonRelation
                {
                    Related = relatedPhysicalPerson,
                    Relation = relation
                });

            await unitOfWork.Complete();
        };

        return NoContent();
    }


    [HttpDelete("related-physical-person")]
    public async Task<IActionResult> RemoveRelatedPhysicalPerson(int masterId, int relatedId)
    {
        var masterPhysicalPerson = await repository.GetPhysicalPerson(masterId);
        var relatedPhysicalPerson = await repository.GetPhysicalPerson(relatedId);

        if (masterPhysicalPerson == null || relatedPhysicalPerson == null)
            return NotFound();

        var removableRelation = masterPhysicalPerson.PhysicalPersonRelations
            .FirstOrDefault(ppr => ppr.RelatedId == relatedPhysicalPerson.Id);
        masterPhysicalPerson.PhysicalPersonRelations.Remove(removableRelation);

        await unitOfWork.Complete();

        return NoContent();
    }


    [HttpPost("[action]/{physicalPersonId}")]
    public async Task<IActionResult> UploadPhoto(int physicalPersonId, IFormFile file)
    {
        var physicalPerson = await repository.GetPhysicalPerson(physicalPersonId, false);

        if (physicalPerson == null)
        {
            return NotFound();
        }

        if (file == null) return BadRequest("Null file");
        if (file.Length == 0) return BadRequest("Empty file");
        // TODO Check for Type

        var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolderPath))
            Directory.CreateDirectory(uploadsFolderPath);

        var filePath = Path.Combine(uploadsFolderPath, file.FileName);

        using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream);

        physicalPerson.PictureRelativePath = $"uploads/{file.FileName}";

        await unitOfWork.Complete();

        return Ok();
    }
}
