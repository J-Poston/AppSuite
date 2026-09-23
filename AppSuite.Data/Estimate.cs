using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppSuite.Data
{
    [Table(nameof(Estimate))]
    [PrimaryKey(nameof(EstimateId))]
    public class Estimate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstimateId { get; set; }
        public string EstimateNum { get; set; }
        public DateTime QuoteDate { get; set; }
        public int SoldToCustId { get; set; }
        public int BillToCustId { get; set; }
        public int PayerCustId { get; set; }
        public int ShipToCustId { get; set; }
        public decimal SubtotalAmt { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal DiscountPct { get; set; }
        public decimal MiscCharges { get; set; }
        public decimal OrderAmt { get; set; }

    }

    //EstimateEquip
    // EstimateTask

    public class EstimateLine
    {
        public int EstimateId { get; set; }
        public int LineNum { get; set; }
        public DateTime OrderDate { get; set; }
        public string PartNum { get; set; }

    }

}
