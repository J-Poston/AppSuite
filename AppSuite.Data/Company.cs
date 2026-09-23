using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppSuite.Data
{

    [Table(nameof(Company))]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(10)]
        public string CompanyCode { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        public ICollection<CompanySite> CompanySites { get; set; } = new List<CompanySite>();
    }

    [Table(nameof(CompanySite))]
    public class CompanySite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(10)]
        public string SiteCode { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(40)] 
        public string Address1 { get; set; }
        [MaxLength(40)] 
        public string Address2 { get; set; }
        [MaxLength(40)] 
        public string Addres3 { get; set; }
        [MaxLength(40)] 
        public string City { get; set; }
        [MaxLength(40)] 
        public string State { get; set; }
        [MaxLength(40)] 
        public string ZipCode { get; set; }
        [MaxLength(40)] 
        public string Country { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }

    }
}
