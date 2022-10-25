using Api.Controllers.Resources;
using Api.Core;
using Api.Core.Models;
using Api.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[Route("[controller]")]
public class PhysicalPersonController : Controller
{
    private readonly IMapper mapper;

    private readonly IPhysicalPersonRepository repository;

    private readonly IUnitOfWork unitOfWork;


    public PhysicalPersonController(IMapper mapper, IPhysicalPersonRepository repository, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.unitOfWork = unitOfWork;
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


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePhysicalPerson(int id)
    {
        var physicalPerson = await repository.GetPhysicalPerson(id);

        if (physicalPerson == null)
            return NotFound();

        repository.RemovePhysicalPerson(physicalPerson);
        await unitOfWork.Complete();

        return Ok(id);
    }
}
