using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace ZeroDay.DAL.Models.NatGeo
{
    [Table("Log")]
    public class Log
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Thread { get; set; }

        public string Level { get; set; }

        public string Logger { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }
    }
}