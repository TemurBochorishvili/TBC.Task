﻿using Api.Core.Models;

namespace Api.Core;

public interface IPhysicalPersonRepository
{
    Task CreatePhysicalPerson(PhysicalPerson physicalPerson);

    void UploadPhysicalPersonPhoto();

    void AddPhysicalPersonRelation();

    void RemovePhysicalPersonRelation();

    void RemovePhysicalPerson(PhysicalPerson physicalPerson);

    Task<PhysicalPerson> GetPhysicalPerson(int id, bool includeRelated = true);

    void GetPhysicalPersons();

    void CountRelatedPhysicalPersonsByRelationType();
}
