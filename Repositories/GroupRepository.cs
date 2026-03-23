// ==================== Repositories/GroupRepository.cs ====================
// «База» групп — Dictionary для быстрого поиска по id (collection/03_Dictionary).

namespace UniversityApp.Repositories;

using UniversityApp.Entities;

public class GroupRepository : IGroupRepository
{
    /// <summary>Словарь: id группы → объект Group (collection/03_Dictionary).</summary>
    private readonly Dictionary<string, Group> _groups = [];

    public void Add(Group group) => _groups[group.Id] = group;

    public Group? GetById(string id) => _groups.TryGetValue(id, out var g) ? g : null;

    public IReadOnlyList<Group> GetAll() => _groups.Values.ToList();
}