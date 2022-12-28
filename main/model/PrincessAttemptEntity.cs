using System.ComponentModel.DataAnnotations;
using lab2;

namespace lab4_db.model;

public class PrincessAttemptEntity: IEntity
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Contender> Сontenders { get; set; } = new ();
}