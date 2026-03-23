namespace UniversityApp.Repositories;

using UniversityApp.Entities;

public interface IGroupRepository
{
    void Add(Group group);
    Group? GetById(string id);
    IReadOnlyList<Group> GetAll();
}