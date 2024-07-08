using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Abstractions.Entities;

namespace Domain.Domain.Entities;

public class BaseEntities<T>: AggregateRoot,IAuditableEntity<T>
{
    [Key]
    [Column("Id")]
    public T Id { get; set; }
    public DateTimeOffset? CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
    public T? CreateBy { get; set; }
    public T? UpdateBy { get; set; }
}

