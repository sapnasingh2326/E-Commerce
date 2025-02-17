using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Model;

public partial class DemoProjectContext : DbContext
{
    public DemoProjectContext()
    {
    }

    public DemoProjectContext(DbContextOptions<DemoProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressBook> AddressBooks { get; set; }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<AppInfo> AppInfos { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ContactU> ContactUs { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DynamicPage> DynamicPages { get; set; }

    public virtual DbSet<EmailTemp> EmailTemps { get; set; }

    public virtual DbSet<MailItem> MailItems { get; set; }

    public virtual DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderList> OrderLists { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Smtpconfig> Smtpconfigs { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-DH1O5F3;Initial Catalog=DemoProject;TrustServerCertificate=True; user id=sa; password=Nimda123 ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AddressB__3214EC07951E0949");

            entity.ToTable("AddressBook");

            entity.Property(e => e.AddressLine1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Registration).WithMany(p => p.AddressBooks)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__AddressBo__Regis__4F7CD00D");
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AddressT__3214EC0718BB409C");

            entity.ToTable("AddressType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppInfo__3214EC070EB9C451");

            entity.ToTable("AppInfo");

            entity.Property(e => e.FavIcon)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Logo).IsUnicode(false);
            entity.Property(e => e.LogoBackGroundImage)
                .IsUnicode(false)
                .HasColumnName("logoBackGroundImage");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Banners__3213E83FECDC3DBE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BannerImg).HasMaxLength(500);
            entity.Property(e => e.BannerName).HasMaxLength(100);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brand__3214EC07E9E649F8");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandLogo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC07BE7F24AE");

            entity.ToTable("Cart");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Cart__ProductId__619B8048");

            entity.HasOne(d => d.Registration).WithMany(p => p.Carts)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__Cart__Registrati__628FA481");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07C7F7D86F");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryImageUrl).IsUnicode(false);
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3214EC0781F84FF7");

            entity.ToTable("City");

            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__City__StateID__49C3F6B7");
        });

        modelBuilder.Entity<ContactU>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContactU__3214EC0788F07267");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC07AF5F7CB0");

            entity.ToTable("Country");

            entity.HasIndex(e => e.CountryName, "UQ__Country__E056F2017A55E6EB").IsUnique();

            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DynamicPage>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PK__DynamicP__C565B1045ACBC6AA");

            entity.ToTable("DynamicPage");

            entity.Property(e => e.PageName).HasMaxLength(100);
            entity.Property(e => e.PageUrl)
                .HasMaxLength(250)
                .HasColumnName("PageURL");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<EmailTemp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailTem__3214EC07520DC27A");

            entity.ToTable("EmailTemp");

            entity.Property(e => e.EmailUrl)
                .HasMaxLength(250)
                .HasColumnName("EmailURL");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Subject).HasMaxLength(250);
        });

        modelBuilder.Entity<MailItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MailItem__3214EC07B0C5AC76");

            entity.ToTable("MailItem");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ReceivedDate).HasColumnType("datetime");
            entity.Property(e => e.Sender).HasMaxLength(255);
            entity.Property(e => e.Subject).HasMaxLength(255);
        });

        modelBuilder.Entity<NewsletterSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Newslett__3214EC071A5B92D2");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3213E83F7DCC67F2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber).HasMaxLength(20);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__0E6E26BF");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC075DA4FECE");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__6D0D32F4");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__6E01572D");

            entity.HasOne(d => d.Registration).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__OrderDeta__Regis__6EF57B66");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3213E83FF9906C80");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderItem__Order__14270015");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderItem__Produ__151B244E");
        });

        modelBuilder.Entity<OrderList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderLis__3214EC076D5359DB");

            entity.ToTable("OrderList");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNo).IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Address).WithMany(p => p.OrderLists)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__OrderList__Addre__68487DD7");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.OrderLists)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__OrderList__Payme__6A30C649");

            entity.HasOne(d => d.Registration).WithMany(p => p.OrderLists)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__OrderList__Regis__6754599E");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07445C4C1C");

            entity.ToTable("Payment");

            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.CardNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExpiryDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Upiid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UPIId");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderDetailId)
                .HasConstraintName("FK__Payment__OrderDe__73BA3083");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payment__OrderId__72C60C4A");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC07CCBEAD35");

            entity.Property(e => e.MethodName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC0735AF1AAF");

            entity.ToTable("Product");

            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsNewarrivals).HasColumnName("IsNEWARRIVALS");
            entity.Property(e => e.IsThreeitems).HasColumnName("IsTHREEITEMS");
            entity.Property(e => e.IsTwoitems).HasColumnName("IsTWOITEMS");
            entity.Property(e => e.LongDescription).IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductDisplayImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("productDisplayImage");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductColor).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductColorId)
                .HasConstraintName("FK__Product__Product__5441852A");

            entity.HasOne(d => d.ProductSize).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductSizeId)
                .HasConstraintName("FK__Product__Product__534D60F1");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK__Product__SubCate__52593CB8");
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductC__3214EC07C153FDF2");

            entity.ToTable("ProductColor");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC076559358F");

            entity.ToTable("ProductImage");

            entity.Property(e => e.ImageUrl).IsUnicode(false);

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.Product)
                .HasConstraintName("FK__ProductIm__Produ__571DF1D5");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductR__3214EC07C194FA77");

            entity.ToTable("ProductReview");

            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ImageOne)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ImageTwo)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductRe__Produ__59FA5E80");

            entity.HasOne(d => d.Registration).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__ProductRe__Regis__5AEE82B9");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductS__3214EC0744A58F16");

            entity.ToTable("ProductSize");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registra__3214EC076BAC621F");

            entity.ToTable("Registration");

            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl).IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PostCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Registrat__RoleI__07C12930");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83F7A5B4903");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Smtpconfig>(entity =>
        {
            entity.HasKey(e => e.SmtpId).HasName("PK__SMTPConf__F70729123F6FA71C");

            entity.ToTable("SMTPConfig");

            entity.Property(e => e.Host).HasMaxLength(255);
            entity.Property(e => e.IsEnableSsl).HasColumnName("IsEnableSSL");
            entity.Property(e => e.SmtpPassword).HasMaxLength(255);
            entity.Property(e => e.SmtpUser).HasMaxLength(255);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__State__3214EC07534E0CFC");

            entity.ToTable("State");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__State__CountryID__46E78A0C");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubCateg__3214EC077C3DF9A4");

            entity.ToTable("SubCategory");

            entity.Property(e => e.SubCategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SubCatego__Categ__398D8EEE");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Wishlist__3214EC07185D07A6");

            entity.ToTable("Wishlist");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Wishlist__Produc__5DCAEF64");

            entity.HasOne(d => d.Registration).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__Wishlist__Regist__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
