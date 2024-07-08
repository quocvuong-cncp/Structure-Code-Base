using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Abstractions.Entities;
public interface IAuditableEntity<T>
{
    DateTimeOffset? CreatedOnUtc { get; set; }

    DateTimeOffset? ModifiedOnUtc { get; set; }
    T? CreateBy { get; set; }
    T? UpdateBy { get; set; }
}
