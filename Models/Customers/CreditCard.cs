using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilTeamProject.Models.Customers
{
    public enum CardType
    {
        Visa = 1,
        MasterCard = 2,
        AmercicanExpress = 3
    }
    public class CreditCard
    {
        [Key]
        [ForeignKey("Customer")]
        public int CustomerID { get; private set; }

        public int CreditCardNumber { get; private set; }

        public CardType Type { get; private set; }

        public DateTime ExpireDate { get; private set; }

        public virtual Customer Customer { get; set; }

        private CreditCard()
        {

        }

        public CreditCard(CardType type, int creditCardNumber, DateTime expireDate, int? customerID)
        {
            if (customerID == null)
            {
                throw new ArgumentException("CustomerID");
            }

            Type = type;
            CreditCardNumber = creditCardNumber;
            ExpireDate = expireDate;
            CustomerID = customerID.Value;
        }
    }
}