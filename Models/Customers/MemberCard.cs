using OilTeamProject.Persistence;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;

namespace OilTeamProject.Models.Customers
{
    public enum MembershipType
    {
        Basic,
        Silver,
        Gold
    }

    public class MemberCard
    {
        [Key]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "Type")]
        public MembershipType Type { get; set; }

        [Display(Name = "Code")]
        public string MemberCardCode { get; set; }

        [Required]
        public int Credits { get; set; }

        [Required]
        public bool NewsLetter { get; set; }

        public virtual Customer Customer { get; set; }



        public static string GetMemberCardCode()
        {
            Thread.Sleep(100);
            return DateTime.Now.ToString("MMddHHmmss");
        }

        public static DateTime GetCreationDate()
        {
            MemberCard memberCard = new MemberCard();
            return memberCard.CreationDate = DateTime.Now;
        }

        private static MemberCard GetMemberCard(Customer customer, ApplicationDbContext _context)
        {
            var CustomerMemberCard = _context
                .MemberCards.SingleOrDefault(m => m.CustomerID == customer.CustomerID);

            return CustomerMemberCard;
        }

        public static int CalculateCredits(double totalCost, Customer customer, ApplicationDbContext _context)
        {
            if (GetMemberCard(customer, _context) != null)
            {

                GetMemberCard(customer, _context).Credits = (int)(totalCost * 10) + GetMemberCard(customer, _context).Credits;

                return GetMemberCard(customer, _context).Credits;
            }
            else
            {
                return 0;
            }
        }

        public static void CheckMembershipType(MemberCard memberCard, int credits, ApplicationDbContext _context)
        {
            if (credits >= 1000 && credits <= 5000)
            {
                memberCard.Type = MembershipType.Silver;

            }
            else if (credits > 5000)
            {
                memberCard.Type = MembershipType.Gold;
            }
            else
            {
                memberCard.Type = MembershipType.Basic;
            }
            _context.SaveChanges();

        }

        public static bool CheckBonus(double totalCost, Customer customer, ApplicationDbContext _context)
        {
            if (GetMemberCard(customer, _context) != null)
            {

                if (customer.MemberCard.Type == MembershipType.Silver)
                {
                    if (totalCost > 20)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (customer.MemberCard.Type == MembershipType.Gold)
                {
                    if (totalCost > 30)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void UpdateCredits(Customer customer, ApplicationDbContext _context, int newCredits)
        {
            if (GetMemberCard(customer, _context) != null)
            {
                GetMemberCard(customer, _context).Credits = newCredits;
                _context.SaveChanges();
            }
        }

        public static double BonusAmount(Customer customer, ApplicationDbContext _context)
        {
            if (GetMemberCard(customer, _context) != null)
            {
                var memberCard = (GetMemberCard(customer, _context));

                if (memberCard.Type == MembershipType.Basic)
                {
                    return -10;

                }
                if (memberCard.Type == MembershipType.Silver)
                {
                    return -20;
                }
                else
                {
                    return -30;
                }
            }
            else
                return 0;

        }

        public static double GetNewTotalCost(double totalcost, double bonusAmount)
        {


            return totalcost + bonusAmount;
        }

        public static void ZeroMemberPoints(Customer customer, ApplicationDbContext _context)
        {
            UpdateCredits(customer, _context, 0);
        }
    }
}