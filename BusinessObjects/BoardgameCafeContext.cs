using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects;

public partial class BoardgameCafeContext : DbContext
{
    public BoardgameCafeContext()
    {
    }

    public BoardgameCafeContext(DbContextOptions<BoardgameCafeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillDetail> BillDetails { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<DrinkCategory> DrinkCategories { get; set; }

    public virtual DbSet<DrinkVariation> DrinkVariations { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameType> GameTypes { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = BoardgameCafe;uid=sa;pwd=123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bill__3213E83F89CAE50A");

            entity.ToTable("bill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(128)
                .HasColumnName("customer_name");
            entity.Property(e => e.NumberOfGames).HasColumnName("number_of_games");
            entity.Property(e => e.PaidAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("paid_at");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Table).WithMany(p => p.Bills)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bill__table_id__52593CB8");
        });

        modelBuilder.Entity<BillDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bill_det__3213E83FB7291F71");

            entity.ToTable("bill_detail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BillId).HasColumnName("bill_id");
            entity.Property(e => e.DrinkVariationId).HasColumnName("drink_variation_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Bill).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.BillId)
                .HasConstraintName("FK__bill_deta__bill___571DF1D5");

            entity.HasOne(d => d.DrinkVariation).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.DrinkVariationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bill_deta__drink__5629CD9C");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__drink__3213E83F86733981");

            entity.ToTable("drink");

            entity.HasIndex(e => e.Name, "UQ__drink__72E12F1B6383B71E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Category).WithMany(p => p.Drinks)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__drink__category___403A8C7D");
        });

        modelBuilder.Entity<DrinkCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__drink_ca__3213E83F84C67B62");

            entity.ToTable("drink_category");

            entity.HasIndex(e => e.Name, "UQ__drink_ca__72E12F1B621AC90B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DrinkVariation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__drink_va__3213E83FF0ED0726");

            entity.ToTable("drink_variation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DrinkId).HasColumnName("drink_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.VariationName)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("variation_name");

            entity.HasOne(d => d.Drink).WithMany(p => p.DrinkVariations)
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("FK__drink_var__drink__440B1D61");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__game__3213E83F5B6CB0B1");

            entity.ToTable("game");

            entity.HasIndex(e => e.Name, "UQ__game__72E12F1BCACC1B25").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.MaxPlayerNumber).HasColumnName("max_player_number");
            entity.Property(e => e.MinPlayerNumber).HasColumnName("min_player_number");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TypeId)
                .HasDefaultValue(1)
                .HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Games)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__game__type_id__4CA06362");
        });

        modelBuilder.Entity<GameType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__game_typ__3213E83FA4546938");

            entity.ToTable("game_type");

            entity.HasIndex(e => e.Name, "UQ__game_typ__72E12F1BC7553DB8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__staff__3213E83FDDF9A509");

            entity.ToTable("staff");

            entity.HasIndex(e => e.Email, "UQ__staff__AB6E61644436A811").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__staff__B43B145F0DA41C1C").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__staff__F3DBC572F0B9DE95").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(64)
                .HasColumnName("fullname");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__table__3213E83FF81DE9F8");

            entity.ToTable("table");

            entity.HasIndex(e => e.TableNumber, "UQ__table__21B232CEDD1DC9CA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TableNumber)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("table_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
