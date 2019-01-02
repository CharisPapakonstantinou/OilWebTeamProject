using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OilTeamProject.Models.Customers;
using System;

namespace OilTeamProject.Dtos
{
    public class OrderDto
    {
        public string OrderNumber { get; set; }
        public DateTime CreationDate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }

        public bool PaidOff { get; set; }
        public DateTime PaymentDate { get; set; }
        public double TotalCost { get; set; }
    }
}