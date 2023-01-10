using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookietry.Models
{
    public class Book
    {

        [Key]
        public int Book_Id { get; set; }
        [Display(Name = "Book_Name")]
        public string Book_name { get; set; }
        [Display(Name = "Category")]
        public string Book_categ { get; set; }
        [Display(Name = "Description")]
        public string Desc { get; set; }
        [Display(Name = "Author")]
        public string Author_name { get; set; }
        [Display(Name = "Language")]
        public string lang { get; set; }


    }

    public class BooksSeller
    {
        [Key]
        public int Seller_id { get; set; }
        [Display(Name = "Seller Name")]
        public string Seller_Name { get; set; }
        [Display(Name = "Phone Number")]
        public string Seller_Number { get; set; }
        [Display(Name = "Email")]
        public string Seller_email { get; set; }
        [Display(Name = "Address")]
        public string Seller_address { get; set; }



    }


    public class Purchase
    {
        [Key]
        public int purchase_id { get; set; }

        [ForeignKey("forkey1")]
        [Display(Name = "Seller Name")]
        public int? Seller_id { get; set; }
        public BooksSeller forkey1 { get; set; }




        [ForeignKey("forkey2")]
        [Display(Name = "Book Name")]
        public int? Book_Id { get; set; }

        public string Book_name { get; set; }
        public Book forkey2 { get; set; }
        [Display(Name = "Quantity")]
        public int purchase_quantity { get; set; }
        [Display(Name = "Date")]
        public DateTime purchase_date { get; set; }
        [Display(Name = "Price")]
        public int purchase_cost { get; set; }
    }

    public class Invoice
    {
        [Key]
        public int invoice_id { get; set; }
        public string userID { get; set; } //httpcontext.current.user.identity.name

        [ForeignKey("forkey3")]
        [Display(Name = "Book Name")]
        public int Book_Id { get; set; }
        public Book forkey3 { get; set; }
        [Display(Name = "Quantity")]
        public int quantity { get; set; }
        [Display(Name = "Name")]
        public string invoice_name { get; set; }
        [Display(Name = "Phone")]
        public string invoice_phone { get; set; }
        [Display(Name = "Email")]
        public string invoice_mail { get; set; }
        [Display(Name = "Address")]
        public string invoice_address { get; set; }
    }

    public class Inventory
    {
        //[Key]
        //public int inv_id { get; set; }
        [Key]
        [ForeignKey("forkey4")]
        [Display(Name = "Book Name")]
        public int Book_Id { get; set; }
        public Book forkey4 { get; set; }
        [Display(Name = "Quantity")]

        public int quantity { get; set; }

    }




    public class BookieDbContext : DbContext
    {
        


        public BookieDbContext(DbContextOptions<BookieDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BooksSeller> Sellers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
    }
}


