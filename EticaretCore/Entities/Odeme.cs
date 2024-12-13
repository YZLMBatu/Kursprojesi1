﻿using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class Odeme : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Sipariş No"),StringLength(50)]
        public string OrderNo { get; set; }
        [Display(Name = "Toplam Tutar")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Müşteri No")]
        public int AppUserId { get; set; }
        [Display(Name = "Müşteri "), StringLength(50)]
        public string CustomerId { get; set; }
        [Display(Name = "Fatura Adresi "), StringLength(50)]
        public string BillingAddress { get; set; }
        [Display(Name = "Teslimat Adresi"), StringLength(50)]
        public string DeliveryAddress { get; set; }
        [Display(Name = "Teslimat Tarihi")]
        public DateTime OrderDate { get; set; }
        public List<OdemeLine>? OdemeLines { get; set; }
        [Display(Name = "Müşteri")]
        public AppUser? AppUser { get; set; }
    }
}