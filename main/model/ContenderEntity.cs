using System.ComponentModel.DataAnnotations;
using lab4_db.model;

namespace lab2;

public class Contender: IEntity
{
    public long Id { get; set; }
    public string Name { set; get; }
    public int Attraction { set; get; }

    public long AttractionId { set; get; }
    public PrincessAttemptEntity PrincessAttemptEntity { get; set; }
}