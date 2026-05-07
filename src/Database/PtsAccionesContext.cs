using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoadVIS.Database;

public partial class PtsAccionesContext : DbContext
{
    public PtsAccionesContext(DbContextOptions<PtsAccionesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccionDgespecializadum> AccionDgespecializada { get; set; }

    public virtual DbSet<AccionEstadoHistorial> AccionEstadoHistorials { get; set; }

    public virtual DbSet<AccionMedidaCorrectiva> AccionMedidaCorrectivas { get; set; }

    public virtual DbSet<AccionSupervision> AccionSupervisions { get; set; }

    public virtual DbSet<BitacoraEstatus> BitacoraEstatuses { get; set; }

    public virtual DbSet<ConductasInfractora> ConductasInfractoras { get; set; }

    public virtual DbSet<EjecucionAccion> EjecucionAccions { get; set; }

    public virtual DbSet<EstadoFormulario> EstadoFormularios { get; set; }

    public virtual DbSet<EstadoFormularioTransicion> EstadoFormularioTransicions { get; set; }

    public virtual DbSet<EstatusAccion> EstatusAccions { get; set; }

    public virtual DbSet<EstatusSancion> EstatusSancions { get; set; }

    public virtual DbSet<IndexScript> IndexScripts { get; set; }

    public virtual DbSet<ObservacionRecomendacion> ObservacionRecomendacions { get; set; }

    public virtual DbSet<Sancion> Sancions { get; set; }

    public virtual DbSet<SancionControl> SancionControls { get; set; }

    public virtual DbSet<SolicitudCancelacion> SolicitudCancelacions { get; set; }

    public virtual DbSet<TipoAccion> TipoAccions { get; set; }

    public virtual DbSet<VwAccionMedidaCorrectiva> VwAccionMedidaCorrectivas { get; set; }

    public virtual DbSet<VwAccionSupervision> VwAccionSupervisions { get; set; }

    public virtual DbSet<VwDashboardAccionMedidaCorrectiva> VwDashboardAccionMedidaCorrectivas { get; set; }

    public virtual DbSet<VwDashboardDgespecializadum> VwDashboardDgespecializada { get; set; }

    public virtual DbSet<VwDashboardFactorRiesgo> VwDashboardFactorRiesgos { get; set; }

    public virtual DbSet<VwDashboardPresentacion> VwDashboardPresentacions { get; set; }

    public virtual DbSet<VwDashboardSancion> VwDashboardSancions { get; set; }

    public virtual DbSet<VwEjecucionAccion> VwEjecucionAccions { get; set; }

    public virtual DbSet<VwEtapaAccion> VwEtapaAccions { get; set; }

    public virtual DbSet<VwObservacionRecomendacion> VwObservacionRecomendacions { get; set; }

    public virtual DbSet<VwSancion> VwSancions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccionDgespecializadum>(entity =>
        {
            entity.HasKey(e => new { e.IdAccion, e.ClaveDge });

            entity.ToTable("AccionDGEspecializada", "Supervision");

            entity.Property(e => e.ClaveDge)
                .HasMaxLength(20)
                .HasDefaultValue("");
            entity.Property(e => e.NombreDge).HasMaxLength(150);

            entity.HasOne(d => d.IdAccionNavigation).WithMany(p => p.AccionDgespecializada)
                .HasForeignKey(d => d.IdAccion)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AccionEstadoHistorial>(entity =>
        {
            entity.ToTable("AccionEstadoHistorial", "Estado");

            entity.HasIndex(e => e.IdAccion, "IX_AccionEstadoHistorial_IdAccion");

            entity.HasIndex(e => e.IdEstado, "IX_AccionEstadoHistorial_IdEstado");

            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RegistradoEl)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RegistradoPor)
                .HasMaxLength(9)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAccionNavigation).WithMany(p => p.AccionEstadoHistorials)
                .HasForeignKey(d => d.IdAccion)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.AccionEstadoHistorials)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AccionMedidaCorrectiva>(entity =>
        {
            entity.HasKey(e => e.IdAymc);

            entity.ToTable("AccionMedidaCorrectiva", "Supervision");

            entity.HasIndex(e => e.IdAccion, "IX_AccionMedidaCorrectiva_IdAccion").IsUnique();

            entity.Property(e => e.JustificacionOmision).HasMaxLength(500);
            entity.Property(e => e.NumOficio).HasMaxLength(50);
            entity.Property(e => e.Omitido).HasDefaultValue(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);

            entity.HasOne(d => d.IdAccionNavigation).WithOne(p => p.AccionMedidaCorrectiva).HasForeignKey<AccionMedidaCorrectiva>(d => d.IdAccion);
        });

        modelBuilder.Entity<AccionSupervision>(entity =>
        {
            entity.HasKey(e => e.IdAccion);

            entity.ToTable("AccionSupervision", "Supervision");

            entity.HasIndex(e => e.IdEstado, "IX_AccionSupervision_IdEstado");

            entity.HasIndex(e => e.IdTipoAccion, "IX_AccionSupervision_IdTipoAccion");

            entity.Property(e => e.Casfim).HasMaxLength(20);
            entity.Property(e => e.CeferActualizada).HasMaxLength(10);
            entity.Property(e => e.CeferPeriodo).HasMaxLength(10);
            entity.Property(e => e.CeferRegistro).HasMaxLength(50);
            entity.Property(e => e.ClaveDg).HasMaxLength(20);
            entity.Property(e => e.ClavePes).HasMaxLength(20);
            entity.Property(e => e.ClaveVp).HasMaxLength(20);
            entity.Property(e => e.Comentarios).HasMaxLength(2000);
            entity.Property(e => e.DenominacionEntidad).HasMaxLength(250);
            entity.Property(e => e.IdEstado).HasDefaultValue(1);
            entity.Property(e => e.MotivoRechazo).HasMaxLength(500);
            entity.Property(e => e.NombreCortoEntidad).HasMaxLength(100);
            entity.Property(e => e.NombreDg).HasMaxLength(100);
            entity.Property(e => e.NombreSector).HasMaxLength(100);
            entity.Property(e => e.NombreSubsector).HasMaxLength(100);
            entity.Property(e => e.NombreVp).HasMaxLength(100);
            entity.Property(e => e.UltimaActualizacion).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.UsuarioAlta).HasMaxLength(100);
            entity.Property(e => e.UsuarioUltimaMod).HasMaxLength(100);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.AccionSupervisions)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdTipoAccionNavigation).WithMany(p => p.AccionSupervisions)
                .HasForeignKey(d => d.IdTipoAccion)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<BitacoraEstatus>(entity =>
        {
            entity.HasKey(e => e.IdBitacora);

            entity.ToTable("BitacoraEstatus", "Auditoria");

            entity.HasIndex(e => e.IdAccion, "IX_BitacoraEstatus_IdAccion").IsUnique();

            entity.HasIndex(e => e.IdEstatusAnterior, "IX_BitacoraEstatus_IdEstatusAnterior");

            entity.HasIndex(e => e.IdEstatusNuevo, "IX_BitacoraEstatus_IdEstatusNuevo");

            entity.Property(e => e.Observacion).HasMaxLength(500);
            entity.Property(e => e.UsuarioCambio).HasMaxLength(100);

            entity.HasOne(d => d.IdAccionNavigation).WithOne(p => p.BitacoraEstatus)
                .HasForeignKey<BitacoraEstatus>(d => d.IdAccion)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdEstatusAnteriorNavigation).WithMany(p => p.BitacoraEstatusIdEstatusAnteriorNavigations).HasForeignKey(d => d.IdEstatusAnterior);

            entity.HasOne(d => d.IdEstatusNuevoNavigation).WithMany(p => p.BitacoraEstatusIdEstatusNuevoNavigations).HasForeignKey(d => d.IdEstatusNuevo);
        });

        modelBuilder.Entity<ConductasInfractora>(entity =>
        {
            entity.HasKey(e => e.IdConducta);

            entity.ToTable("ConductasInfractoras", "Supervision");

            entity.HasIndex(e => e.IdAccion, "IX_ConductasInfractoras_IdAccion").IsUnique();

            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);

            entity.HasOne(d => d.IdAccionNavigation).WithOne(p => p.ConductasInfractora).HasForeignKey<ConductasInfractora>(d => d.IdAccion);
        });

        modelBuilder.Entity<EjecucionAccion>(entity =>
        {
            entity.HasKey(e => e.IdEjecucion);

            entity.ToTable("EjecucionAccion", "Supervision");

            entity.HasIndex(e => e.IdAccion, "IX_EjecucionAccion_IdAccion").IsUnique();

            entity.Property(e => e.NumOficioOrden).HasMaxLength(50);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);

            entity.HasOne(d => d.IdAccionNavigation).WithOne(p => p.EjecucionAccion).HasForeignKey<EjecucionAccion>(d => d.IdAccion);
        });

        modelBuilder.Entity<EstadoFormulario>(entity =>
        {
            entity.ToTable("EstadoFormulario", "Estado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoFormularioTransicion>(entity =>
        {
            entity.ToTable("EstadoFormularioTransicion", "Estado");

            entity.HasIndex(e => new { e.IdEstadoOrigen, e.IdEstadoDestino, e.Activo }, "UQ_EstadoFormularioTransicion_OrigenDestino").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<EstatusAccion>(entity =>
        {
            entity.HasKey(e => e.IdEstatus);

            entity.ToTable("EstatusAccion", "Catalogo");

            entity.Property(e => e.ClaveEstatus).HasMaxLength(20);
            entity.Property(e => e.ColorSemaforo).HasMaxLength(20);
            entity.Property(e => e.DescEstatus).HasMaxLength(200);
        });

        modelBuilder.Entity<EstatusSancion>(entity =>
        {
            entity.HasKey(e => e.IdEstatusSancion);

            entity.ToTable("EstatusSancion", "Catalogo");

            entity.Property(e => e.ClaveEstatus).HasMaxLength(20);
            entity.Property(e => e.DescEstatus).HasMaxLength(200);
        });

        modelBuilder.Entity<IndexScript>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("index_scripts");

            entity.Property(e => e.CreateIndexScript).HasColumnName("create_index_script");
            entity.Property(e => e.DatabaseName)
                .HasMaxLength(128)
                .HasColumnName("database_name");
            entity.Property(e => e.IndexName)
                .HasMaxLength(128)
                .HasColumnName("index_name");
            entity.Property(e => e.LastUserLookup)
                .HasColumnType("datetime")
                .HasColumnName("last_user_lookup");
            entity.Property(e => e.LastUserScan)
                .HasColumnType("datetime")
                .HasColumnName("last_user_scan");
            entity.Property(e => e.LastUserSeek)
                .HasColumnType("datetime")
                .HasColumnName("last_user_seek");
            entity.Property(e => e.TableName)
                .HasMaxLength(128)
                .HasColumnName("table_name");
            entity.Property(e => e.UserLookups).HasColumnName("user_lookups");
            entity.Property(e => e.UserScans).HasColumnName("user_scans");
            entity.Property(e => e.UserSeeks).HasColumnName("user_seeks");
            entity.Property(e => e.UserUpdates).HasColumnName("user_updates");
        });

        modelBuilder.Entity<ObservacionRecomendacion>(entity =>
        {
            entity.HasKey(e => e.IdObservacion);

            entity.ToTable("ObservacionRecomendacion", "Supervision");

            entity.HasIndex(e => e.IdAccion, "IX_ObservacionRecomendacion_IdAccion").IsUnique();

            entity.Property(e => e.JustificacionOmision).HasMaxLength(500);
            entity.Property(e => e.NumOficio).HasMaxLength(50);
            entity.Property(e => e.Omitido).HasDefaultValue(false);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);

            entity.HasOne(d => d.IdAccionNavigation).WithOne(p => p.ObservacionRecomendacion).HasForeignKey<ObservacionRecomendacion>(d => d.IdAccion);
        });

        modelBuilder.Entity<Sancion>(entity =>
        {
            entity.HasKey(e => e.IdSancion);

            entity.ToTable("Sancion", "Supervision");

            entity.HasIndex(e => e.IdAccion, "IX_Sancion_IdAccion");

            entity.HasIndex(e => e.IdEstatusSancion, "IX_Sancion_IdEstatusSancion");

            entity.Property(e => e.IdSolicitudExterno).HasMaxLength(50);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);

            entity.HasOne(d => d.IdAccionNavigation).WithMany(p => p.Sancions)
                .HasForeignKey(d => d.IdAccion)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdEstatusSancionNavigation).WithMany(p => p.Sancions)
                .HasForeignKey(d => d.IdEstatusSancion)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SancionControl>(entity =>
        {
            entity.HasKey(e => e.IdAccion);

            entity.ToTable("SancionControl", "Supervision");

            entity.Property(e => e.IdAccion).ValueGeneratedNever();
            entity.Property(e => e.JustificacionOmision).HasMaxLength(500);
            entity.Property(e => e.Omitido).HasDefaultValue(false);

            entity.HasOne(d => d.IdAccionNavigation).WithOne(p => p.SancionControl).HasForeignKey<SancionControl>(d => d.IdAccion);
        });

        modelBuilder.Entity<SolicitudCancelacion>(entity =>
        {
            entity.HasKey(e => e.IdCancelacion);

            entity.ToTable("SolicitudCancelacion", "Supervision");

            entity.HasIndex(e => e.IdAccion, "IX_SolicitudCancelacion_IdAccion").IsUnique();

            entity.Property(e => e.Motivo).HasMaxLength(500);
            entity.Property(e => e.NumMemorandum).HasMaxLength(50);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);

            entity.HasOne(d => d.IdAccionNavigation).WithOne(p => p.SolicitudCancelacion).HasForeignKey<SolicitudCancelacion>(d => d.IdAccion);
        });

        modelBuilder.Entity<TipoAccion>(entity =>
        {
            entity.HasKey(e => e.IdTipo);

            entity.ToTable("TipoAccion", "Catalogo");

            entity.Property(e => e.ClaveSubtipo).HasMaxLength(20);
            entity.Property(e => e.DescSubtipo).HasMaxLength(200);
        });

        modelBuilder.Entity<VwAccionMedidaCorrectiva>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_AccionMedidaCorrectiva");

            entity.Property(e => e.NumOficio).HasMaxLength(50);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);
        });

        modelBuilder.Entity<VwAccionSupervision>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_AccionSupervision");

            entity.Property(e => e.Casfim).HasMaxLength(20);
            entity.Property(e => e.CeferActualizada).HasMaxLength(10);
            entity.Property(e => e.CeferPeriodo).HasMaxLength(10);
            entity.Property(e => e.CeferRegistro).HasMaxLength(50);
            entity.Property(e => e.ClaveDg).HasMaxLength(20);
            entity.Property(e => e.ClavePes).HasMaxLength(20);
            entity.Property(e => e.ClaveVp).HasMaxLength(20);
            entity.Property(e => e.Comentarios).HasMaxLength(2000);
            entity.Property(e => e.DenominacionEntidad).HasMaxLength(250);
            entity.Property(e => e.IdAccion).ValueGeneratedOnAdd();
            entity.Property(e => e.NombreCortoEntidad).HasMaxLength(100);
            entity.Property(e => e.NombreDg).HasMaxLength(100);
            entity.Property(e => e.NombreSector).HasMaxLength(100);
            entity.Property(e => e.NombreSubsector).HasMaxLength(100);
            entity.Property(e => e.NombreVp).HasMaxLength(100);
            entity.Property(e => e.UsuarioAlta).HasMaxLength(100);
            entity.Property(e => e.UsuarioUltimaMod).HasMaxLength(100);
        });

        modelBuilder.Entity<VwDashboardAccionMedidaCorrectiva>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Dashboard_AccionMedidaCorrectiva");

            entity.Property(e => e.Evento).HasMaxLength(128);
            entity.Property(e => e.NumOficio).HasMaxLength(50);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);
        });

        modelBuilder.Entity<VwDashboardDgespecializadum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Dashboard_DGEspecializada");

            entity.Property(e => e.ClaveDge).HasMaxLength(20);
            entity.Property(e => e.NombreDge).HasMaxLength(150);
        });

        modelBuilder.Entity<VwDashboardFactorRiesgo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Dashboard_FactorRiesgo");
        });

        modelBuilder.Entity<VwDashboardPresentacion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Dashboard_Presentacion");

            entity.Property(e => e.ClaveDg).HasMaxLength(20);
            entity.Property(e => e.ClaveSubtipo).HasMaxLength(20);
            entity.Property(e => e.ClaveVp).HasMaxLength(20);
            entity.Property(e => e.DenominacionEntidad).HasMaxLength(250);
            entity.Property(e => e.DescEstatus).HasMaxLength(200);
            entity.Property(e => e.DescSubtipo).HasMaxLength(200);
            entity.Property(e => e.NombreSector).HasMaxLength(100);
            entity.Property(e => e.NombreSubsector).HasMaxLength(100);
            entity.Property(e => e.NombreVp).HasMaxLength(100);
            entity.Property(e => e.NumOficioOrden).HasMaxLength(50);
            entity.Property(e => e.SituacionDeConclusion)
                .HasMaxLength(21)
                .IsUnicode(false);
            entity.Property(e => e.SituacionDeInicio)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwDashboardSancion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Dashboard_Sancion");

            entity.Property(e => e.Evento).HasMaxLength(128);
        });

        modelBuilder.Entity<VwEjecucionAccion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_EjecucionAccion");

            entity.Property(e => e.DescEstatus).HasMaxLength(200);
            entity.Property(e => e.NumOficioOrden).HasMaxLength(50);
            entity.Property(e => e.SituacionDeConclusion)
                .HasMaxLength(21)
                .IsUnicode(false);
            entity.Property(e => e.SituacionDeInicio)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwEtapaAccion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_EtapaAccion");

            entity.Property(e => e.Etapa)
                .HasMaxLength(19)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwObservacionRecomendacion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ObservacionRecomendacion");

            entity.Property(e => e.NumOficio).HasMaxLength(50);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);
        });

        modelBuilder.Entity<VwSancion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Sancion");

            entity.Property(e => e.ClaveEstatus).HasMaxLength(20);
            entity.Property(e => e.DescEstatus).HasMaxLength(200);
            entity.Property(e => e.IdSolicitudExterno).HasMaxLength(50);
            entity.Property(e => e.UsuarioRegistro).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
