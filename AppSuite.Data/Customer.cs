using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppSuite.Data
{
    [Table(nameof(Customer))]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(20)]
        public string CustId { get; set; }
        public string Name { get; set; }
        public ICollection<CustEquip> CustEquips { get; set; } = new List<CustEquip>();
        public bool ValidSoldTo { get; set; }
        public bool ValidBillTo { get; set; }
        public bool ValidPayer { get; set; }
        public bool ValidShipTo { get; set; }
    }
    [Table(nameof(CustEquip))]
    [PrimaryKey(nameof(CustId),nameof(EquipId))]
    public class CustEquip
    {
        public int CustId { get; set; }
        [ForeignKey(nameof(Customer))]
        public Customer Customer { get; set; }
        public int EquipId { get; set; }
        [ForeignKey(nameof(EquipId))]
        public Equipment Equipment { get; set; }

    }
}
