using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using Teste.Domain.Entities;
using Teste.Infra.Data.Mappings;

namespace Teste.Infra.Data.Contexts
{
    public class Context : IdentityDbContext<AspNetUsers>
    {
        public IDbContextTransaction Transaction { get; private set; }
        public virtual DbSet<Aluno> Alunos { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
            try
            {
                ChangeTracker.LazyLoadingEnabled = false;
                //if (Database.GetPendingMigrations().Any())
                //{

                //Exclui a base de dados se existir.
                //Database.EnsureDeleted();

                //Cria a base de dados e suas respectivas tabelas se não existir.
                Database.EnsureCreated();
                Database.Migrate();

                //}
            }
            catch (Exception)
            {
                
            }

        }

     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public IDbContextTransaction InitTransacao()
        {
            if (Transaction == null) Transaction = this.Database.BeginTransaction();
            return Transaction;
        }
        private void RollBack()
        {
            Transaction?.Rollback();
        }
        private void Save()
        {
            try
            {
                ChangeTracker.DetectChanges();
                SaveChanges();
            }
            catch (Exception ex)
            {
                RollBack();
                throw new Exception(ex.Message);
            }
        }
        private void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Transaction.Dispose();
                Transaction = null;
            }
        }
        public void SendChanges()
        {
            Save();
            Commit();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AlunoMap());
        }
    }
}