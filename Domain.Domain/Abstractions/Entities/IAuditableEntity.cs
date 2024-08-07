﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Abstractions.Entities;
public abstract class AuditableEntity<T>: Entity<T>
{
    public DateTimeOffset? CreatedOnUtc { get; set; }

    public DateTimeOffset? ModifiedOnUtc { get; set; }
    public T? CreateBy { get; set; }
    public T? UpdateBy { get; set; }
}
