using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppSuite.Data
{
    [Table(nameof(WorkOrder))]
    [PrimaryKey(nameof(OrderId))]
    public class WorkOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public string OrderNum { get; set; }
        public DateTime OrderDate { get; set; }
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

    // WorkOrderEquip
    // WorkOrderTask

    public class WorkOrderLine
    {
        public int OrderId { get; set; }
        public int OrderLine { get; set; }
        public DateTime OrderDate { get; set; }
        public string PartNum { get; set; }

    }

}
