using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Abstractions.Entities;
public interface IDateTracking
{

    public DateTimeOffset? CreatedOnUtc { get; set; }

    public DateTimeOffset? ModifiedOnUtc { get; set; }
}
