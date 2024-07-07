using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Entities;

public class BaseEntities<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public T Id { get; set; }

        [Column("CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("UpdateDate")]
        public DateTime UpdateDate { get; set; }

        [Column("CreateBy")]
        public string? CreateBy { get; set; }

        [Column("UpdateBy")]
        public string? UpdateBy { get; set; }

    }

