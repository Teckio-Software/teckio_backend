
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;

using ERP_TECKIO;
using ERP_TECKIO.Modelos.Presupuesto;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Modelos.Facturaion;
using ERP_TECKIO.Modelos.Contabilidad;


public partial class Alumno01Context : DbContext
{
    public Alumno01Context()
    {
    }

    public Alumno01Context(DbContextOptions<Alumno01Context> options)
        : base(options)
    {
    }
    public virtual DbSet<Empleado> Empleados { get; set; }
    public virtual DbSet<PrecioUnitarioXEmpleado> PrecioUnitarioXEmpleados { get; set; }

    public virtual DbSet<Almacen> Almacens { get; set; }

    public virtual DbSet<AlmacenEntradaInsumo> AlmacenEntradaInsumos { get; set; }

    public virtual DbSet<AlmacenEntrada> AlmacenEntrada { get; set; }

    public virtual DbSet<AlmacenSalidaInsumo> AlmacenSalidaInsumos { get; set; }

    public virtual DbSet<AlmacenSalida> AlmacenSalida { get; set; }

    public virtual DbSet<CodigoAgrupadorSat> CodigoAgrupadorSats { get; set; }

    public virtual DbSet<Concepto> Conceptos { get; set; }

    public virtual DbSet<Contratista> Contratista { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Cotizacion> Cotizacions { get; set; }
    public virtual DbSet<Banco> Bancos { get; set; }
    public virtual DbSet<Clientes> Clientes { get; set; }
    public virtual DbSet<CuentaBancaria> CuentaBancaria { get; set; }
    public virtual DbSet<CuentaBancariaCliente> CuentaBancariaClientes { get; set; }
    public virtual DbSet<CuentaBancariaEmpresa> CuentaBancariaEmpresas { get; set; }
    public virtual DbSet<MovimientoBancario> MovimientosBancarios { get; set; }
    public virtual DbSet<MovimientoBancarioContratista> MBancariosContratistas { get; set; }
    public virtual DbSet<MovimientoBancarioCliente> MBancariosClientes { get; set; }
    public virtual DbSet<MovimientoBancarioEmpresa> MBancariosEmpresa { get; set; }
    public virtual DbSet<MovimientoBancarioSaldo> MovimientoBancarioSaldos { get; set; }

    public virtual DbSet<CuentaContable> CuentaContables { get; set; }

    public virtual DbSet<DependenciaProgramacionEstimada> DependenciaProgramacionEstimada { get; set; }

    public virtual DbSet<DetalleXContrato> DetalleXContratos { get; set; }

    public virtual DbSet<DiasConsiderados> DiasConsiderados { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Estimaciones> Estimaciones { get; set; }

    public virtual DbSet<FactorSalarioIntegrado> FactorSalarioIntegrados { get; set; }

    public virtual DbSet<FactorSalarioReal> FactorSalarioReals { get; set; }

    public virtual DbSet<FactorSalarioRealDetalle> FactorSalarioRealDetalles { get; set; }

    public virtual DbSet<FamiliaInsumo> FamiliaInsumos { get; set; }

    public virtual DbSet<Generadores> Generadores { get; set; }

    public virtual DbSet<Insumo> Insumos { get; set; }

    public virtual DbSet<InsumoExistencia> InsumoExistencia { get; set; }

    public virtual DbSet<InsumoXCotizacion> InsumoXcotizacions { get; set; }

    public virtual DbSet<InsumoXOrdenCompra> InsumoXordenCompras { get; set; }

    public virtual DbSet<InsumoXRequisicion> InsumoXrequisicions { get; set; }
    public virtual DbSet<TipoImpuesto> TipoImpuestos { get; set; }

    public virtual DbSet<ImpuestoInsumoCotizado> ImpuestoInsumoCotizados { get; set; }
    public virtual DbSet<ImpuestoInsumoOrdenCompra> ImpuestoInsumoOrdenCompras { get; set; }

    public virtual DbSet<OrdenCompra> OrdenCompras { get; set; }

    public virtual DbSet<PeriodoEstimaciones> PeriodoEstimaciones { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<PolizaDetalle> PolizaDetalles { get; set; }

    public virtual DbSet<PorcentajeAcumuladoContrato> PorcentajeAcumuladoContratos { get; set; }
    public virtual DbSet<ConjuntoIndirectos> ConjuntoIndirectos { get; set; }
    public virtual DbSet<Indirectos> Indirectos { get; set; }
    public virtual DbSet<IndirectosXConcepto> IndirectosXConceptos { get; set; }
    public virtual DbSet<PrecioUnitario> PrecioUnitarios { get; set; }

    public virtual DbSet<PrecioUnitarioDetalle> PrecioUnitarioDetalles { get; set; }

    public virtual DbSet<ProgramacionEstimada> ProgramacionEstimada { get; set; }

    public virtual DbSet<ProgramacionEstimadaGantt> ProgramacionEstimadaGantts { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<RelacionFSRInsumo> RelacionFsrinsumos { get; set; }

    public virtual DbSet<Requisicion> Requisicions { get; set; }

    public virtual DbSet<Rubro> Rubros { get; set; }

    public virtual DbSet<SaldosBalanzaComprobacion> SaldosBalanzaComprobacions { get; set; }

    public virtual DbSet<TipoInsumo> TipoInsumos { get; set; }

    public virtual DbSet<TipoPoliza> TipoPolizas { get; set; }

    public virtual DbSet<AcuseValidacion> AcuseValidacions { get; set; }
    public virtual DbSet<Archivo> Archivos { get; set; }
    public virtual DbSet<CategoriaImpuesto> CategoriaImpuestos { get; set; }
    public virtual DbSet<ClasificacionImpuesto> ClasificacionImpuestos { get; set; }
    public virtual DbSet<DetalleValidacion> DetalleValidacions { get; set; }
    public virtual DbSet<Factura> Facturas { get; set; }
    public virtual DbSet<FacturaComplementoPago> FacturaComplementoPagos { get; set; }
    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }
    public virtual DbSet<FacturaDetalleImpuesto> FacturaDetalleImpuestos { get; set; }
    public virtual DbSet<FacturaEmisor> FacturaEmisors { get; set; }
    public virtual DbSet<FacturaImpuesto> FacturaImpuestos { get; set; }
    public virtual DbSet<FacturaImpuestosLocal> FacturaImpuestosLocales { get; set; }
    public virtual DbSet<FacturaReceptor> FacturaReceptors { get; set; }
    public virtual DbSet<TipoFactor> TipoFactors { get; set; }
    public virtual DbSet<OperacionesXPrecioUnitarioDetalle> OperacionesXPrecioUnitarioDetalles { get; set; }
    public virtual DbSet<FsixinsummoMdO> FsixinsummoMdOs { get; set; }

    public virtual DbSet<FsixinsummoMdOdetalle> FsixinsummoMdOdetalles { get; set; }

    public virtual DbSet<FsrxinsummoMdO> FsrxinsummoMdOs { get; set; }

    public virtual DbSet<FsrxinsummoMdOdetalle> FsrxinsummoMdOdetalles { get; set; }
    public virtual DbSet<FormaPagoSat> FormaPagoSats { get; set; }
    public virtual DbSet<MonedaSat> MonedaSats { get; set; }
    public virtual DbSet<ProductoYservicio> ProductoYservicios { get; set; }

    public virtual DbSet<ProductoYservicioSat> ProductoYservicioSats { get; set; }
    public virtual DbSet<RegimenFiscalSat> RegimenFiscalSats { get; set; }
    public virtual DbSet<SubcategoriaProductoYServicio> SubcategoriaProductoYservicios { get; set; }
    public virtual DbSet<CategoriaProductoYServicio> CategoriaProductoYservicios { get; set; }
    public virtual DbSet<Unidad> Unidads { get; set; }
    public virtual DbSet<UnidadSat> UnidadSats { get; set; }

    public virtual DbSet<UsoCfdiSat> UsoCfdiSats { get; set; }
    public virtual DbSet<FacturaXOrdenCompra> FacturaXOrdenCompras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FsixinsummoMdO>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FSIXInsu__3214EC07D9E08BFA");

            entity.ToTable("FSIXInsummoMdO");

            entity.Property(e => e.DiasNoLaborales).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.DiasPagados).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Fsi)
                .HasColumnType("decimal(28, 6)")
                .HasColumnName("FSI");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.FsixinsummoMdOs)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FSIXInsummoMdO_IdInsumo");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.FsixinsummoMdOs)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FSIXInsummoMdO_IdProyecto");
        });
        modelBuilder.Entity<UnidadSat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UnidadSa__3214EC071F7FFC83");

            entity.ToTable("UnidadSat", "Factura");

            entity.Property(e => e.Clave).HasMaxLength(3);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Tipo).HasMaxLength(200);
        });

        modelBuilder.Entity<UsoCfdiSat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsoCfdiS__3214EC075164CB71");

            entity.ToTable("UsoCfdiSat", "Factura");

            entity.Property(e => e.Clave).HasMaxLength(5);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
        });
        modelBuilder.Entity<CategoriaProductoYServicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC070901B952");

            entity.ToTable("CategoriaProductoYServicio", "Factura");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Unidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Unidad__3214EC075C74CC95");

            entity.ToTable("Unidad", "Factura");

            entity.Property(e => e.Descripcion).HasMaxLength(5);
        });

        modelBuilder.Entity<SubcategoriaProductoYServicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subcateg__3214EC075ED39141");

            entity.ToTable("SubcategoriaProductoYServicio", "Factura");

            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductoYservicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC0738FB77AA");

            entity.ToTable("ProductoYServicio", "Factura");

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(200);

            entity.HasOne(d => d.IdCategoriaProductoYServicioNavigation).WithMany(p => p.PorductoYservicios)
                .HasForeignKey(d => d.IdCategoriaProductoYServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductoY__IdCat__5B988E2F");

            entity.HasOne(d => d.IdProductoYservicioSatNavigation).WithMany(p => p.ProductoYservicios)
                .HasForeignKey(d => d.IdProductoYservicioSat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductoYServicio_IdProductoYServicioSat");

            entity.HasOne(d => d.IdSubcategoriaProductoYServicioNavigation).WithMany(p => p.PorductoYservicios)
                .HasForeignKey(d => d.IdSubategoriaProductoYServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductoYServicio_IdSubategoriaProductoYServicio");

            entity.HasOne(d => d.IdUnidadNavigation).WithMany(p => p.ProductoYservicios)
                .HasForeignKey(d => d.IdUnidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductoYServicio_IdUnidad");

            entity.HasOne(d => d.IdUnidadSatNavigation).WithMany(p => p.ProductoYservicios)
                .HasForeignKey(d => d.IdUnidadSat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductoYServicio_IdUnidadSat");
        });
        modelBuilder.Entity<RegimenFiscalSat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RegimenF__3214EC078F758608");

            entity.ToTable("RegimenFiscalSat", "Factura");

            entity.Property(e => e.Clave).HasMaxLength(3);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
        });

        modelBuilder.Entity<ProductoYservicioSat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC07EFCEB7D3");

            entity.ToTable("ProductoYServicioSat", "Factura");

            entity.Property(e => e.Clave).HasMaxLength(8);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
        });

        modelBuilder.Entity<MonedaSat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MonedaSa__3214EC07D20CEEC0");

            entity.ToTable("MonedaSat", "Factura");

            entity.Property(e => e.Codigo).HasMaxLength(3);
            entity.Property(e => e.Moneda).HasMaxLength(200);
        });

        modelBuilder.Entity<FsixinsummoMdOdetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FSIXInsu__3214EC0714F5F2A2");

            entity.ToTable("FSIXInsummoMdODetalle");

            entity.Property(e => e.ArticulosLey).HasMaxLength(500);
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Dias).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.EsLaborableOpagado).HasColumnName("EsLaborableOPagado");
            entity.Property(e => e.IdFsixinsummoMdO).HasColumnName("IdFSIXInsummoMdO");

            entity.HasOne(d => d.IdFsixinsummoMdONavigation).WithMany(p => p.FsixinsummoMdOdetalles)
                .HasForeignKey(d => d.IdFsixinsummoMdO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FSIXInsummoMdODetalle_IdFSI");
        });

        modelBuilder.Entity<FsrxinsummoMdO>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FSRXInsu__3214EC073F7B7BD2");

            entity.ToTable("FSRXInsummoMdO");

            entity.Property(e => e.CostoDirecto).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.CostoFinal).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Fsr)
                .HasColumnType("decimal(28, 6)")
                .HasColumnName("FSR");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.FsrxinsummoMdOs)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FSRXInsummoMdO_IdInsumo");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.FsrxinsummoMdOs)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FSRXInsummoMdO_IdProyecto");
        });

        modelBuilder.Entity<FsrxinsummoMdOdetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FSRXInsu__3214EC0748E716E5");

            entity.ToTable("FSRXInsummoMdODetalle");

            entity.Property(e => e.ArticulosLey).HasMaxLength(500);
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.IdFsrxinsummoMdO).HasColumnName("IdFSRXInsummoMdO");
            entity.Property(e => e.PorcentajeFsr)
                .HasColumnType("decimal(28, 6)")
                .HasColumnName("PorcentajeFSR");

            entity.HasOne(d => d.IdFsrxinsummoMdONavigation).WithMany(p => p.FsrxinsummoMdOdetalles)
                .HasForeignKey(d => d.IdFsrxinsummoMdO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FSRXInsummoMdODetalle_IdFSR");
        });

        modelBuilder.Entity<OperacionesXPrecioUnitarioDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OperacionesXPrecioUnitarioDetalle");
            entity.ToTable("OperacionesXPrecioUnitarioDetalle");
            entity.Property(e => e.Operacion).HasMaxLength(500);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Resultado).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdPrecioUnitarioDetalleNavigation).WithMany(p => p.OperacionesXPrecioUnitarioDetalles)
                .HasForeignKey(d => d.IdPrecioUnitarioDetalle)
                .HasConstraintName("FK__Operacion__IdPre__60B24907");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AcuseValidacion_2024_02_15");

            entity.ToTable("Empleado");

            entity.Property(e => e.IdUser).HasColumnName("IdUser");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(100);
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(100);
            entity.Property(e => e.Curp).HasMaxLength(18);
            entity.Property(e => e.Rfc).HasMaxLength(13);
            entity.Property(e => e.SeguroSocial).HasMaxLength(20);
            entity.Property(e => e.FechaRelacionLaboral).HasColumnType("datetime");
            entity.Property(e => e.FechaTerminoRelacionLaboral).HasColumnType("datetime");
            entity.Property(e => e.SalarioDiario).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Estatus).HasColumnName("Estatus");
        });

        modelBuilder.Entity<PrecioUnitarioXEmpleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AcuseValidacion_2024_02_15");

            entity.ToTable("PrecioUnitarioXEmpleado");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.PrecioUnitarioXEmpleados)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK_PrecioUnitarioXEmpleado_IdEmpleado");
            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.PrecioUnitarioXEmpleados)
                .HasForeignKey(d => d.IdProyceto)
                .HasConstraintName("FK_PrecioUnitarioXEmpleado_IdPoryecto");
            entity.HasOne(d => d.IdPrecioUnitarioNavigation).WithMany(p => p.PrecioUnitarioXEmpleados)
                .HasForeignKey(d => d.IdPrecioUnitario)
                .HasConstraintName("FK_PrecioUnitarioXEmpleado_IdPrecioUnitario");
        });

        modelBuilder.Entity<AcuseValidacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AcuseValidacion_2024_02_15");

            entity.ToTable("AcuseValidacion", "Factura");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Folio).HasMaxLength(30);

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.AcuseValidacions)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AcuseValidacion_Factura_2024_02_15");
        });

        modelBuilder.Entity<Archivo>(entity =>
        {
            entity.ToTable("Archivos", "dbo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ruta)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CategoriaImpuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CategoriaImpuesto_2024_02_15");

            entity.ToTable("CategoriaImpuesto", "Factura");

            entity.Property(e => e.Tipo).HasMaxLength(15);
        });

        modelBuilder.Entity<ClasificacionImpuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ClasificacionImpuesto_2024_02_15");

            entity.ToTable("ClasificacionImpuesto", "Factura");

            entity.Property(e => e.TipoClasificacionImpuesto).HasMaxLength(150);
        });

        modelBuilder.Entity<DependenciaProgramacionEstimada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dependen__3214EC07DC96535C");

            entity.HasOne(d => d.IdProgramacionEstimadaGanttNavigation).WithMany(p => p.Dependencias)
                .HasForeignKey(d => d.IdProgramacionEstimadaGantt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProgramacionEstimadaGanttDependiente");

            entity.HasOne(d => d.IdProgramacionEstimadaGanttPredecesoraNavigation).WithMany(p => p.DependenciaPredecesora)
                .HasForeignKey(d => d.IdProgramacionEstimadaGanttPredecesora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramacionEstimadaGanttPerteneciente");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.DependenciaProgramacionEstimadas)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proyecto");
        });

        modelBuilder.Entity<DetalleValidacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DetalleValidacion_2024_02_15");

            entity.ToTable("DetalleValidacion", "Factura");

            entity.Property(e => e.CodigoValidacion)
                .HasMaxLength(5)
                .HasDefaultValueSql("('99999')");

            entity.HasOne(d => d.IdAcuseValidacionNavigation).WithMany(p => p.DetalleValidacions)
                .HasForeignKey(d => d.IdAcuseValidacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleValidacion_AcuseValidacion_2024_02_15");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Factura_2024_02_15");

            entity.ToTable("Factura", "Factura");

            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(5)
                .HasDefaultValueSql("('00000')");
            entity.Property(e => e.Descuento).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.FechaEmision).HasColumnType("datetime");
            entity.Property(e => e.FechaTimbrado).HasColumnType("datetime");
            entity.Property(e => e.FechaValidacion).HasColumnType("datetime");
            entity.Property(e => e.FolioCfdi).HasMaxLength(25);
            entity.Property(e => e.MetodoPago).HasMaxLength(5);
            entity.Property(e => e.RfcEmisor).HasMaxLength(13);
            entity.Property(e => e.SerieCfdi).HasMaxLength(25);
            entity.Property(e => e.Subtotal).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Total).HasColumnType("decimal(28, 6)");
            //entity.Ignore(e => e.FormaPago);
            //entity.Ignore(e => e.Moneda);
            //entity.Ignore(e => e.RfcReceptor);
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("UUID");
            entity.Property(e => e.VersionFactura)
                .HasMaxLength(5)
                .HasDefaultValueSql("('4.0')");

            entity.HasOne(d => d.IdArchivoNavigation).WithMany(p => p.FacturaIdArchivoNavigations)
                .HasForeignKey(d => d.IdArchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Archivo_2024_02_15");

            entity.HasOne(d => d.IdArchivoPdfNavigation).WithMany(p => p.FacturaIdArchivoPdfNavigations)
                .HasForeignKey(d => d.IdArchivoPdf)
                .HasConstraintName("FK_Factura_ArchivoPdf_2024_02_15");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_IdCliente");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_IdFormaPago");

            entity.HasOne(d => d.IdMonedaSatNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdMonedaSat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_IdMonedaSat");

            entity.HasOne(d => d.IdRegimenFiscalSatNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdRegimenFiscalSat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_IdRegimenFiscalSat");

            entity.HasOne(d => d.IdUsoCfdiNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdUsoCfdi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_IdUsoCfdi");
        });

        modelBuilder.Entity<FacturaComplementoPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FacturaComplementoPago_2024_04_10");

            entity.ToTable("FacturaComplementoPago", "Factura");

            entity.Property(e => e.ImpuestoPagado).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.ImpuestoSaldoAnterior).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.ImpuestoSaldoInsoluto).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Uuid).HasMaxLength(40);

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturaComplementoPagos)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_ComplementoPago_2024_04_10");
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FacturaDetalle_2024_02_15");

            entity.ToTable("FacturaDetalle", "Factura");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(28, 6)");
            entity.Ignore(e => e.Descripcion);
            entity.Ignore(e => e.UnidadSat);
            entity.Property(e => e.Descuento).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.IdProductoYservicio).HasColumnName("IdProductoYServicio");
            entity.Property(e => e.Importe).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaDetalle_Factura_2024_02_15");

            entity.HasOne(d => d.IdProductoYservicioNavigation).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.IdProductoYservicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaDetalle_IdProductoYServicio");
        });

        modelBuilder.Entity<FacturaDetalleImpuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FacturaDetalleImpuestos_2024_02_15");

            entity.ToTable("FacturaDetalleImpuestos", "Factura");

            entity.Property(e => e.Base).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Importe).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.TasaCuota).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdCategoriaImpuestoNavigation).WithMany(p => p.FacturaDetalleImpuestos)
                .HasForeignKey(d => d.IdCategoriaImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoriaImpuesto_FacturaDetalleImpuestos_2024_02_16");

            entity.HasOne(d => d.IdClasificacionImpuestoNavigation).WithMany(p => p.FacturaDetalleImpuestos)
                .HasForeignKey(d => d.IdClasificacionImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaDetalleImpuestos_ClasificacionImpuesto_2024_02_15");

            entity.HasOne(d => d.IdFacturaDetalleNavigation).WithMany(p => p.FacturaDetalleImpuestos)
                .HasForeignKey(d => d.IdFacturaDetalle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaDetalleImpuestos_FacturaDetalle_2024_02_15");

            entity.HasOne(d => d.IdTipoFactorNavigation).WithMany(p => p.FacturaDetalleImpuestos)
                .HasForeignKey(d => d.IdTipoFactor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaDetalleImpuestos_TipoFactor_2024_02_15");

            entity.HasOne(d => d.IdTipoImpuestoNavigation).WithMany(p => p.FacturaDetalleImpuestos)
                .HasForeignKey(d => d.IdTipoImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaDetalleImpuestos_TipoImpuesto_2024_02_15");
        });

        modelBuilder.Entity<FacturaEmisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FacturaEmisor_2024_05_23");

            entity.ToTable("FacturaEmisor", "Factura");

            entity.Property(e => e.Rfc).HasMaxLength(14);

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturaEmisors)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_FacturaEmisor_Factura_2024_05_23");

            entity.HasOne(d => d.IdRegimenFiscalSatNavigation).WithMany(p => p.FacturasEmisor)
                .HasForeignKey(d => d.IdRegimenFiscalSat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdRegimenFiscalSat_FacturaEmisor");
        });

        modelBuilder.Entity<FacturaImpuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FacturaImpuestos_2024_02_15");

            entity.ToTable("FacturaImpuestos", "Factura");

            entity.Property(e => e.TotalImpuesto).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdCategoriaImpuestoNavigation).WithMany(p => p.FacturaImpuestos)
                .HasForeignKey(d => d.IdCategoriaImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaImpuestos_CategoriaImpuesto_2024_02_15");

            entity.HasOne(d => d.IdClasificacionImpuestoNavigation).WithMany(p => p.FacturaImpuestos)
                .HasForeignKey(d => d.IdClasificacionImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaImpuestos_ClasificacionImpuesto_2024_02_15");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturaImpuestos)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaImpuestos_Factura_2024_02_15");
        });

        modelBuilder.Entity<FacturaImpuestosLocal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FacturaImpuestosLocales_2024_03_11");

            entity.ToTable("FacturaImpuestosLocales", "Factura");

            entity.Property(e => e.DescripcionImpuestoLocal).HasMaxLength(50);
            entity.Property(e => e.TotalImpuesto).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdCategoriaImpuestoNavigation).WithMany(p => p.FacturaImpuestosLocales)
                .HasForeignKey(d => d.IdCategoriaImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaImpuestosLocales_CategoriaImpuesto_2024_03_11");

            entity.HasOne(d => d.IdClasificacionImpuestoNavigation).WithMany(p => p.FacturaImpuestosLocales)
                .HasForeignKey(d => d.IdClasificacionImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaImpuestosLocales_ClasificacionImpuesto_2024_03_11");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturaImpuestosLocales)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaImpuestosLocales_Factura_2024_03_11");
        });

        modelBuilder.Entity<FacturaReceptor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FacturaReceptor_2024_05_23");

            entity.ToTable("FacturaReceptor", "Factura");

            entity.Property(e => e.RegimenFiscalReceptor).HasMaxLength(4);
            entity.Property(e => e.Rfc).HasMaxLength(14);
            entity.Property(e => e.UsoCfdi).HasMaxLength(4);

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturaReceptors)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_FacturaReceptor_Factura_2024_05_23");
        });

        modelBuilder.Entity<TipoFactor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TipoFactor_2024_02_15");

            entity.ToTable("TipoFactor", "Factura");

            entity.Property(e => e.Descripcion).HasMaxLength(10);
        });

        modelBuilder.Entity<Almacen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Almacen__3214EC070CBAE877");

            entity.ToTable("Almacen");

            entity.Property(e => e.AlmacenNombre).HasMaxLength(50);
            entity.Property(e => e.Ciudad).HasMaxLength(50);
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Colonia).HasMaxLength(100);
            entity.Property(e => e.Domicilio).HasMaxLength(100);
            entity.Property(e => e.Responsable).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(10);

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Almacens)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Almacen__IdProye__0EA330E9");
        });

        modelBuilder.Entity<AlmacenEntrada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AlmacenE__3214EC07656C112C");

            entity.Ignore(e => e.CodigoCreacion);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NoEntrada).HasMaxLength(70);
            entity.Property(e => e.Observaciones).HasMaxLength(200);
            entity.Property(e => e.PersonaRegistra).HasMaxLength(100);
            entity.HasOne(d => d.IdAlmacenNavigation).WithMany(p => p.AlmacenEntrada)
                .HasForeignKey(d => d.IdAlmacen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlmacenEn__IdAlm__6754599E");
            entity.HasOne(d => d.IdContratistaNavigation).WithMany(p => p.AlmacenEntradas)
                .HasForeignKey(d => d.IdAlmacen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdContratista_Almacenentrada_2024_06_17");
        });

        modelBuilder.Entity<AlmacenEntradaInsumo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AlmacenE__3214EC074E53A1AA");

            entity.Property(e => e.CantidadPorRecibir).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.CantidadRecibida).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdAlmacenEntradaNavigation).WithMany(p => p.AlmacenEntradaInsumos)
                .HasForeignKey(d => d.IdAlmacenEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlmacenEn__IdAlm__503BEA1C");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.AlmacenEntradaInsumos)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlmacenEn__IdIns__5224328E");

            entity.HasOne(d => d.IdOrdenCompraNavigation).WithMany(p => p.AlmacenEntradaInsumos)
                .HasForeignKey(d => d.IdOrdenCompra)
                .HasConstraintName("FK__AlmacenEn__IdOrd__531856C7");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.AlmacenEntradaInsumos)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__AlmacenEn__IdPro__540C7B00");

            entity.HasOne(d => d.IdRequisicionNavigation).WithMany(p => p.AlmacenEntradaInsumos)
                .HasForeignKey(d => d.IdRequisicion)
                .HasConstraintName("FK__AlmacenEn__IdReq__55009F39");
        });

        modelBuilder.Entity<AlmacenSalida>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AlmacenS__3214EC076B24EA82");

            entity.Property(e => e.CodigoCreacion).HasMaxLength(50);
            entity.Property(e => e.CodigoSalida).HasMaxLength(70);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Observaciones).HasMaxLength(200);
            entity.Property(e => e.PersonaRecibio).HasMaxLength(100);
            entity.Property(e => e.PersonaSurtio).HasMaxLength(100);

            entity.HasOne(d => d.IdAlmacenNavigation).WithMany(p => p.AlmacenSalida)
                .HasForeignKey(d => d.IdAlmacen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlmacenSa__IdAlm__6D0D32F4");
        });

        modelBuilder.Entity<AlmacenSalidaInsumo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AlmacenS__3214EC0757DD0BE4");

            entity.Property(e => e.CantidadPorSalir).HasColumnType("decimal(28, 6)");
            entity.Ignore(e => e.PersonaRecibio);
            entity.Ignore(e => e.PersonaSurtio);

            entity.HasOne(d => d.IdAlmacenSalidaNavigation).WithMany(p => p.AlmacenSalidaInsumos)
                .HasForeignKey(d => d.IdAlmacenSalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlmacenSa__IdAlm__59C55456");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.AlmacenSalidaInsumos)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlmacenSa__IdIns__5BAD9CC8");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.AlmacenSalidaInsumos)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AlmacenSa__IdPro__5CA1C101");
        });

        modelBuilder.Entity<CodigoAgrupadorSat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CodigoAg__3213E83F15502E78");

            entity.ToTable("CodigoAgrupadorSat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo).HasMaxLength(10);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
        });

        modelBuilder.Entity<Concepto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Concepto__3213E83F44FF419A");

            entity.ToTable("Concepto");

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Descripcion).HasMaxLength(2500);
            entity.Property(e => e.Unidad).HasMaxLength(15);
            entity.Property(e => e.PorcentajeIndirecto).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Conceptos)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK__Concepto__idEspe__46E78A0C");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Conceptos)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Concepto__IdProy__0C50D423");
        });

        modelBuilder.Entity<Contratista>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contrati__3214EC077AF13DF7");

            entity.Property(e => e.CodigoPostal).HasMaxLength(6);
            entity.Property(e => e.Colonia).HasMaxLength(100);
            entity.Property(e => e.Domicilio).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IdCuentaRetencionIsr).HasColumnName("IdCuentaRetencionISR");
            entity.Property(e => e.IdCuentaRetencionIva).HasColumnName("IdCuentaRetencionIVA");
            entity.Property(e => e.Municipio).HasMaxLength(100);
            entity.Property(e => e.NExterior)
                .HasMaxLength(25)
                .HasColumnName("NExterior");
            entity.Property(e => e.RazonSocial).HasMaxLength(200);
            entity.Property(e => e.RepresentanteLegal).HasMaxLength(200);
            entity.Property(e => e.Rfc).HasMaxLength(15);
            entity.Property(e => e.Telefono).HasMaxLength(15);

            entity.HasOne(d => d.IdCuentaAnticiposNavigation).WithMany(p => p.ContratistaIdCuentaAnticiposNavigations)
                .HasForeignKey(d => d.IdCuentaAnticipos)
                .HasConstraintName("FK__Contratis__IdCue__7FB5F314");

            entity.HasOne(d => d.IdCuentaContableNavigation).WithMany(p => p.ContratistaIdCuentaContableNavigations)
                .HasForeignKey(d => d.IdCuentaContable)
                .HasConstraintName("FK__Contratis__IdCue__7CD98669");

            entity.HasOne(d => d.IdCuentaRetencionIsrNavigation).WithMany(p => p.ContratistaIdCuentaRetencionIsrNavigations)
                .HasForeignKey(d => d.IdCuentaRetencionIsr)
                .HasConstraintName("FK__Contratis__IdCue__00AA174D");

            entity.HasOne(d => d.IdCuentaRetencionIvaNavigation).WithMany(p => p.ContratistaIdCuentaRetencionIvaNavigations)
                .HasForeignKey(d => d.IdCuentaRetencionIva)
                .HasConstraintName("FK__Contratis__IdCue__019E3B86");

            entity.HasOne(d => d.IdEgresosIvaExentoNavigation).WithMany(p => p.ContratistaIdEgresosIvaExentoNavigations)
                .HasForeignKey(d => d.IdEgresosIvaExento)
                .HasConstraintName("FK__Contratis__IdEgr__02925FBF");

            entity.HasOne(d => d.IdEgresosIvaGravableNavigation).WithMany(p => p.ContratistaIdEgresosIvaGravableNavigations)
                .HasForeignKey(d => d.IdEgresosIvaGravable)
                .HasConstraintName("FK__Contratis__IdEgr__038683F8");

            entity.HasOne(d => d.IdIvaAcreditableContableNavigation).WithMany(p => p.ContratistaIdIvaAcreditableContableNavigations)
                .HasForeignKey(d => d.IdIvaAcreditableContable)
                .HasConstraintName("FK__Contratis__IdIva__7DCDAAA2");

            entity.HasOne(d => d.IdIvaAcreditableFiscalNavigation).WithMany(p => p.ContratistaIdIvaAcreditableFiscalNavigations)
                .HasForeignKey(d => d.IdIvaAcreditableFiscal)
                .HasConstraintName("FK__Contratis__IdIva__047AA831");

            entity.HasOne(d => d.IdIvaPorAcreditarNavigation).WithMany(p => p.ContratistaIdIvaPorAcreditarNavigations)
                .HasForeignKey(d => d.IdIvaPorAcreditar)
                .HasConstraintName("FK__Contratis__IdIva__7EC1CEDB");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contrato__3214EC07324172E1");

            entity.ToTable("Contrato");

            entity.Property(e => e.CostoDestajo).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");


            entity.HasOne(d => d.IdContratistaNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdContratista)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contrato__IdCont__351DDF8C");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contrato__IdProy__3429BB53");
        });

        modelBuilder.Entity<Cotizacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cotizaci__3214EC0702FC7413");

            entity.ToTable("Cotizacion");

            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Ignore(e => e.ImporteConIva);
            entity.Ignore(e => e.ImporteSinIva);
            entity.Ignore(e => e.MontoDescuento);
            entity.Property(e => e.NoCotizacion).HasMaxLength(70);
            entity.Property(e => e.Observaciones).HasMaxLength(200);
            entity.Property(e => e.PersonaAutorizo).HasMaxLength(100);
            entity.Property(e => e.PersonaCompra).HasMaxLength(100);

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Cotizacio__IdPro__05D8E0BE");
            entity.HasOne(d => d.IdContratistaNavigation).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Cotizacio__IdCon__345EC57D");

            entity.HasOne(d => d.IdRequisicionNavigation).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.IdRequisicion)
                .HasConstraintName("FK__Cotizacio__IdReq__06CD04F7");
        });

        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Banco__3214EC07C236DC84");

            entity.ToTable("Banco");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Clave)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("RazonSocial");
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CLIENTES_2024_05_27");

            entity.Property(e => e.CodigoPostal).HasMaxLength(6);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Domicilio).HasMaxLength(500);
            entity.Property(e => e.Colonia).HasMaxLength(100);
            entity.Property(e => e.Municipio).HasMaxLength(100);
            entity.Property(e => e.NoExterior).HasMaxLength(25);
            entity.Property(e => e.RazonSocial).HasMaxLength(500);
            entity.Property(e => e.RepresentanteLegal).HasMaxLength(500);
            entity.Property(e => e.Rfc).HasMaxLength(15);
            entity.Property(e => e.Telefono).HasMaxLength(25);

            entity.HasOne(d => d.IdCuentaAnticiposNavigation).WithMany(p => p.ClienteIdCuentaAnticiposNavigations)
                .HasForeignKey(d => d.IdCuentaAnticipos)
                .HasConstraintName("FK_CLIENTES_CUENTAANTICIPOS");

            entity.HasOne(d => d.IdCuentaContableNavigation).WithMany(p => p.ClienteIdCuentaContableNavigations)
                .HasForeignKey(d => d.IdCuentaContable)
                .HasConstraintName("FK_CLIENTES_CUENTACONTABLE");

            entity.HasOne(d => d.IdIepsNavigation).WithMany(p => p.ClienteIdIepsNavigations)
                .HasForeignKey(d => d.IdIeps)
                .HasConstraintName("FK_CLIENTES_IEPS");

            entity.HasOne(d => d.IdIvaExentoNavigation).WithMany(p => p.ClienteIdIvaExentoNavigations)
                .HasForeignKey(d => d.IdIvaExento)
                .HasConstraintName("FK_CLIENTES_IVAEXENTO");

            entity.HasOne(d => d.IdIvaGravableNavigation).WithMany(p => p.ClienteIdIvaGravableNavigations)
                .HasForeignKey(d => d.IdIvaGravable)
                .HasConstraintName("FK_CLIENTES_IVAGRAVABLE");

            entity.HasOne(d => d.IdIvaPorTrasladarNavigation).WithMany(p => p.ClienteIdIvaPorTrasladarNavigations)
                .HasForeignKey(d => d.IdIvaPorTrasladar)
                .HasConstraintName("FK_CLIENTES_IVAPORTRASLADAR");

            entity.HasOne(d => d.IdIvaRetenidoNavigation).WithMany(p => p.ClienteIdIvaRetenidoNavigations)
                .HasForeignKey(d => d.IdIvaRetenido)
                .HasConstraintName("FK_CLIENTES_IVARETENIDO");

            entity.HasOne(d => d.IdIvaTrasladadoNavigation).WithMany(p => p.ClienteIdIvaTrasladadoNavigations)
                .HasForeignKey(d => d.IdIvaTrasladado)
                .HasConstraintName("FK_CLIENTES_IVATRASLADO");

            entity.HasOne(d => d.IdRetensionIsrNavigation).WithMany(p => p.ClienteIdRetensionIsrNavigations)
                .HasForeignKey(d => d.IdRetensionIsr)
                .HasConstraintName("FK_CLIENTES_RETENCIONISR");
        });

        modelBuilder.Entity<CuentaBancaria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CuentaBa__3214EC07084B3915");

            entity.ToTable("CuentaBancaria");

            entity.Property(e => e.NumeroSucursal).HasMaxLength(50);
            entity.Property(e => e.NumeroCuenta).HasMaxLength(20);
            entity.Property(e => e.Clabe).HasMaxLength(18);
            entity.Property(e => e.TipoMoneda);
            entity.Property(e => e.FechaAlta).HasColumnType("datetime");

            entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.CuentaBancarias)
                .HasForeignKey(d => d.IdBanco)
                .HasConstraintName("FK_CuentaBan_IdBanco");
            entity.HasOne(d => d.IdContratistaNavigation).WithMany(p => p.CuentaBancarias)
                .HasForeignKey(d => d.IdContratista)
                .HasConstraintName("FK__CuentaBan__IdCon__0A338187");
        });

        modelBuilder.Entity<CuentaBancariaCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CuentaBa__3214EC0799249FA1");

            entity.ToTable("CuentaBancariaCliente");

            entity.Property(e => e.NumeroSucursal).HasMaxLength(50);
            entity.Property(e => e.NumeroCuenta).HasMaxLength(20);
            entity.Property(e => e.Clabe).HasMaxLength(18);
            entity.Property(e => e.TipoMoneda);
            entity.Property(e => e.FechaAlta).HasColumnType("datetime");

            entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.CuentaBancariaClientes)
                .HasForeignKey(d => d.IdBanco)
                .HasConstraintName("FK_CuentaBanCliente_IdBanco");
            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.CuentaBancariaClientes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_CuentaBan_IdCliente");
        });

        modelBuilder.Entity<CuentaBancariaEmpresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CuentaBa__3214EC07084B3915");

            entity.ToTable("CuentaBancariaEmpresa");

            entity.Property(e => e.NumeroSucursal).HasMaxLength(50);
            entity.Property(e => e.NumeroCuenta).HasMaxLength(20);
            entity.Property(e => e.Clabe).HasMaxLength(18);
            entity.Property(e => e.TipoMoneda);
            entity.Property(e => e.FechaAlta).HasColumnType("datetime");

            entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.cuentaBancariaEmpresas)
                .HasForeignKey(d => d.IdBanco)
                .HasConstraintName("FK_CuentaBanEmpresa_IdBanco");
            entity.HasOne(d => d.IdCuentaContableNavigation).WithMany(p => p.CuentaBancariaEmpresas)
                .HasForeignKey(d => d.IdCuentaContable)
                .HasConstraintName("FK_CuentaBancariaEmpresa_IdCuentaContable");
        });

        modelBuilder.Entity<OrdenCompraXMovimientoBancario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CuentaBa__3214EC07084B3915");

            entity.ToTable("OrdenCompraXMovimientoBancario");

            entity.Property(e => e.Estatus).HasColumnName("Estatus");
            entity.Property(e => e.TotalSaldado).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdMovimientoBancarioNavigation).WithMany(p => p.OrdenCompraXMovimientoBancarios)
                .HasForeignKey(d => d.IdMovimientoBancario)
                .HasConstraintName("FK_OrdenCompraXMovimientoBancario_IdMovimientoBancario");
            entity.HasOne(d => d.IdOrdenCompraNavigation).WithMany(p => p.OrdenCompraXMovimientoBancarios)
                .HasForeignKey(d => d.IdOrdenCompra)
                .HasConstraintName("FK_OrdenCompraXMovimientoBancario_IdOrdenCompra");
        });

        modelBuilder.Entity<FacturaXOrdenCompraXMovimientoBancario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CuentaBa__3214EC07084B3915");

            entity.ToTable("FacturaXOrdenCompraXMovimientoBancario");

            entity.Property(e => e.Estatus).HasColumnName("Estatus");
            entity.Property(e => e.TotalSaldado).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdFacturaXOrdenCompraNavigation).WithMany(p => p.FacturaXOrdenCompraXMovimientoBancarios)
                .HasForeignKey(d => d.IdFacturaXOrdenCompra)
                .HasConstraintName("FK_FacturaXOrdenCompraXMovimientoBancario_IdFacturaXOrdenCompra");
            entity.HasOne(d => d.IdMovimientoBancarioNavigation).WithMany(p => p.FacturaXOrdenCompraXMovimientoBancarios)
                .HasForeignKey(d => d.IdMovimientoBancario)
                .HasConstraintName("FK_FacturaXOrdenCompraXMovimientoBancario_IdMovimientoBancario");
        });

        modelBuilder.Entity<MovimientoBancario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC07079B9A9C");

            entity.ToTable("MovimientoBancario");

            entity.Property(e => e.NoMovimientoBancario).HasMaxLength(100);
            entity.Property(e => e.Folio).HasMaxLength(100);
            entity.Property(e => e.FechaAlta).HasColumnType("datetime");
            entity.Property(e => e.FechaAplicacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCobra).HasColumnType("datetime");
            entity.Property(e => e.Modalidad);
            entity.Property(e => e.TipoDeposito);
            entity.Property(e => e.MontoTotal);
            entity.Property(e => e.Concepto).HasMaxLength(355);
            entity.Property(e => e.TipoCambio);
            entity.Property(e => e.Moneda);
            entity.Property(e => e.Estatus);
            entity.Property(e => e.TipoBeneficiario);

            entity.HasOne(d => d.IdPolizaNavigation).WithMany(p => p.MovimientosBancarios)
                .HasForeignKey(d => d.IdPoliza)
                .HasConstraintName("FK_MovimientoBancario_IdPoliza");
            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.MovimientosBancarios)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_MovimientoBancario_IdFactura");
            entity.HasOne(d => d.IdCuentaBancariaEmpresaNavigation).WithMany(p => p.MovimientosBancarios)
                .HasForeignKey(d => d.IdCuentaBancariaEmpresa)
                .HasConstraintName("FK_MovimientoBancario_IdCuentaBancariaEmpresa");

        });

        modelBuilder.Entity<MovimientoBancarioContratista>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__MB_Contr__3214EC07F7B9B8C7");

            entity.ToTable("MovimientoBancarioContratista");

            entity.Ignore(e => e.IdBeneficiario);

            entity.HasOne(d => d.IdContratistaNavigation).WithMany(p => p.MBContratistas)
                .HasForeignKey(d => d.IdContratista)
                .HasConstraintName("FK_MBContratista_IdContratista");
            entity.HasOne(d => d.IdCunetaBancariaNavigation).WithMany(p => p.MBContratistas)
                .HasForeignKey(d => d.IdCuentaBancaria)
                .HasConstraintName("FK_MB_Contratista_IdCuentaBancaria");
            entity.HasOne(d => d.IdMovimientoBancarioNavigation).WithMany(p => p.MBContratistas)
                .HasForeignKey(d => d.IdMovimientoBancario)
                .HasConstraintName("FK_MB_Contratista_IdMovimientoBancario");
        });

        modelBuilder.Entity<MovimientoBancarioCliente>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__MB_Contr__3214EC07F7B9B8C7");

            entity.ToTable("MovimientoBancarioCliente");

            entity.Ignore(e => e.IdBeneficiario);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.MBClientes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_MBCliente_IdCliente");
            entity.HasOne(d => d.IdCuentaBancariaClienteNavigation).WithMany(p => p.MBClientes)
                .HasForeignKey(d => d.IdCuentaBancaria)
                .HasConstraintName("FK_MBCliente_IdCuentaBancaria");
            entity.HasOne(d => d.IdMovimientoBancarioNavigation).WithMany(p => p.MBClientes)
                .HasForeignKey(d => d.IdMovimientoBancario)
                .HasConstraintName("FK_MBCliente_IdMovimientoBancario");
        });

        modelBuilder.Entity<MovimientoBancarioEmpresa>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__MB_Contr__3214EC07F7B9B8C7");

            entity.ToTable("MovimientoBancarioEmpresa");

            entity.Ignore(e => e.IdBeneficiario);

            entity.HasOne(d => d.IdCuentaBancariaEmpresaNavigation).WithMany(p => p.MBEmpresa)
                .HasForeignKey(d => d.IdCuentaBancaria)
                .HasConstraintName("FK_MBEmpresa_IdCuentaBancaria");
            entity.HasOne(d => d.IdMovimientoBancarioNavigation).WithMany(p => p.MBEmpresa)
                .HasForeignKey(d => d.IdMovimientoBancario)
                .HasConstraintName("FK_MBEmpresa_IdMovimientoBancario");
        });

        modelBuilder.Entity<MovimientoBancarioSaldo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC07EA03BF78");

            entity.ToTable("MovimientoBancarioSaldo");

            entity.Property(e => e.Anio);
            entity.Property(e => e.Mes);
            entity.Property(e => e.MontoFinal);

            entity.HasOne(d => d.IdCuentaBancariaNavigation).WithMany(p => p.MBESaldo)
                .HasForeignKey(d => d.IdCuentaBancaria)
                .HasConstraintName("FK_MovimientoBancarioSaldo_IdCuentaBancaria");
        });

        modelBuilder.Entity<CuentaContable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CuentaCo__3214EC0721B6055D");

            entity.ToTable("CuentaContable");

            entity.Property(e => e.Codigo).HasMaxLength(35);
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Presupuesto).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.SaldoFinal).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.SaldoInicial).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.EsCuentaContableEmpresa).HasColumnName("EsCuentaContableEmpresa");
            entity.Property(e => e.TipoCuentaContable).HasColumnName("TipoCuentaContable");

            entity.HasOne(d => d.IdRubroNavigation).WithMany(p => p.CuentaContables)
                .HasForeignKey(d => d.IdRubro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CuentaCon__IdRub__24927208");
        });

        modelBuilder.Entity<DetalleXContrato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleX__3214EC0737FA4C37");

            entity.ToTable("DetalleXContrato");

            entity.Property(e => e.ImporteDestajo).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.PorcentajeDestajo).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.FactorDestajo).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.DetalleXcontratos)
                .HasForeignKey(d => d.IdContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleXC__IdCon__3AD6B8E2");

            entity.HasOne(d => d.IdPrecioUnitarioNavigation).WithMany(p => p.DetallesXContrato)
                .HasForeignKey(d => d.IdPrecioUnitario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleXC__IdPre__39E294A9");
        });

        modelBuilder.Entity<DiasConsiderados>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DiasCons__3214EC0727F8EE98");

            entity.Property(e => e.ArticulosLey).HasMaxLength(50);
            entity.Property(e => e.Codigo).HasMaxLength(15);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.EsLaborableOpagado).HasColumnName("EsLaborableOPagado");
            entity.Property(e => e.Valor).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdFactorSalarioIntegradoNavigation).WithMany(p => p.DiasConsiderados)
                .HasForeignKey(d => d.IdFactorSalarioIntegrado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiasConsi__IdFac__29E1370A");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Especial__3213E83F276EDEB3");

            entity.ToTable("Especialidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
        });

        modelBuilder.Entity<Estimaciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estimaci__3214EC0732767D0B");

            entity.Property(e => e.CantidadAvance).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.CantidadAvanceAcumulado).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.ImporteDeAvance).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.ImporteDeAvanceAcumulado).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.PorcentajeAvance).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.PorcentajeAvanceAcumulado).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdConceptoNavigation).WithMany(p => p.Estimaciones)
                .HasForeignKey(d => d.IdConcepto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estimacio__IdCon__3552E9B6");

            entity.HasOne(d => d.IdPeriodoNavigation).WithMany(p => p.Estimaciones)
                .HasForeignKey(d => d.IdPeriodo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estimacio__IdPer__373B3228");

            entity.HasOne(d => d.IdPrecioUnitarioNavigation).WithMany(p => p.Estimaciones)
                .HasForeignKey(d => d.IdPrecioUnitario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estimacio__IdPre__345EC57D");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Estimaciones)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estimacio__IdPro__36470DEF");
        });

        modelBuilder.Entity<FactorSalarioIntegrado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FactorSa__3214EC071E6F845E");

            entity.ToTable("FactorSalarioIntegrado");

            entity.Property(e => e.Fsi)
                .HasColumnType("decimal(28, 6)")
                .HasColumnName("FSI");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.FactorSalarioIntegrados)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FactorSal__IdPro__2057CCD0");
        });

        modelBuilder.Entity<FactorSalarioReal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FactorSa__3214EC070F2D40CE");

            entity.ToTable("FactorSalarioReal");

            entity.Property(e => e.PorcentajeFsr)
                .HasColumnType("decimal(28, 6)")
                .HasColumnName("PorcentajeFSR");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.FactorSalarioReals)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FactorSal__IdPro__11158940");
        });

        modelBuilder.Entity<FactorSalarioRealDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FactorSa__3214EC0713F1F5EB");

            entity.ToTable("FactorSalarioRealDetalle");

            entity.Property(e => e.ArticulosLey).HasMaxLength(50);
            entity.Property(e => e.Codigo).HasMaxLength(15);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.PorcentajeFsrdetalle)
                .HasColumnType("decimal(28, 6)")
                .HasColumnName("PorcentajeFSRDetalle");

            entity.HasOne(d => d.IdFactorSalarioRealNavigation).WithMany(p => p.FactorSalarioRealDetalles)
                .HasForeignKey(d => d.IdFactorSalarioReal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FactorSal__IdFac__15DA3E5D");
        });

        modelBuilder.Entity<FamiliaInsumo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FamiliaI__3214EC071920BF5C");

            entity.ToTable("FamiliaInsumo");

            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<Generadores>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Generado__3214EC0776619304");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.EjeX).HasMaxLength(20);
            entity.Property(e => e.EjeY).HasMaxLength(20);
            entity.Property(e => e.EjeZ).HasMaxLength(20);
            entity.Property(e => e.X).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Y).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Z).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdPrecioUnitarioNavigation).WithMany(p => p.Generadores)
                .HasForeignKey(d => d.IdPrecioUnitario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Generador__IdPre__7849DB76");
        });
        modelBuilder.Entity<TipoImpuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TipoImpuesto_2024_02_15");

            entity.ToTable("TipoImpuesto", "Factura");

            entity.Property(e => e.ClaveImpuesto).HasMaxLength(3);
            entity.Property(e => e.DescripcionImpuesto).HasMaxLength(10);
        });

        modelBuilder.Entity<ImpuestoInsumoCotizado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Impuesto__3214EC074CF5691D");

            entity.ToTable("ImpuestoInsumoCotizado");

            entity.Property(e => e.Importe).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.ImpuestoInsumoCotizados)
                .HasForeignKey(d => d.IdImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImpuestoInsumoC_IdTipoImpuesto");

            entity.HasOne(d => d.IdInsumoCotizadoNavigation).WithMany(p => p.ImpuestoInsumoCotizados)
                .HasForeignKey(d => d.IdInsumoCotizado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImpuestoInsumoC_IdInsumoC");
        });

        modelBuilder.Entity<ImpuestoInsumoOrdenCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Impuesto__3214EC074CF5691D");

            entity.ToTable("ImpuestoInsumoOrdenCompra");

            entity.Property(e => e.Importe).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.ImpuestoInsumoOrdenCompras)
                .HasForeignKey(d => d.IdImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImpuestoInsumoOC_IdTipoImpuesto");

            entity.HasOne(d => d.IdInsumoOrdenCompraNavigation).WithMany(p => p.ImpuestoInsumoOrdenCompras)
                .HasForeignKey(d => d.IdInsumoOrdenCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImpuestoInsumoOC_IdInsumoOC");
        });

        modelBuilder.Entity<Insumo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Insumo__3214EC075FB337D6");

            entity.ToTable("Insumo");

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.CostoBase).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.EsFsrGlobal).HasColumnName("EsFsrGlobal");
            entity.Property(e => e.Descripcion).HasMaxLength(2500);
            entity.Property(e => e.Unidad).HasMaxLength(15);

            entity.HasOne(d => d.IdFamiliaInsumoNavigation).WithMany(p => p.Insumos)
                .HasForeignKey(d => d.IdFamiliaInsumo)
                .HasConstraintName("FK__Insumo__IdFamili__619B8048");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Insumos)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Insumo__IdProyec__0B5CAFEA");

            entity.HasOne(d => d.IdTipoInsumoNavigation).WithMany(p => p.Insumos)
                .HasForeignKey(d => d.IdTipoInsumo)
                .HasConstraintName("FK__Insumo__IdTipoIn__628FA481");
        });

        modelBuilder.Entity<InsumoExistencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InsumoEx__3214EC07778AC167");

            entity.Ignore(e => e.CantidadExistente);
            entity.Property(e => e.CantidadInsumosAumenta).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.CantidadInsumosRetira).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

            entity.HasOne(d => d.IdAlmacenNavigation).WithMany(p => p.InsumoExistencia)
                .HasForeignKey(d => d.IdAlmacen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InsumoExi__IdAlm__797309D9");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.InsumoExistencia)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InsumoExi__IdIns__7A672E12");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.InsumoExistencia)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__InsumoExi__IdPro__7B5B524B");
        });

        modelBuilder.Entity<InsumoXCotizacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InsumoXC__3214EC070F624AF8");

            entity.ToTable("InsumoXCotizacion");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(28, 6)");
            entity.Ignore(e => e.Ieps);
            entity.Property(e => e.ImporteTotal).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.ImporteSinIva).HasColumnType("decimal(28, 6)");
            entity.Ignore(e => e.Isan);
            entity.Ignore(e => e.Isr);
            entity.Ignore(e => e.Iva);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdCotizacionNavigation).WithMany(p => p.InsumoXcotizacions)
                .HasForeignKey(d => d.IdCotizacion)
                .HasConstraintName("FK__InsumoXCo__IdCot__114A936A");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.InsumoXcotizacions)
                .HasForeignKey(d => d.IdInsumo)
                .HasConstraintName("FK__InsumoXCo__IdIns__1332DBDC");

            entity.HasOne(d => d.IdInsumoRequisicionNavigation).WithMany(p => p.InsumoXcotizacions)
                .HasForeignKey(d => d.IdInsumoRequisicion)
                .HasConstraintName("FK__InsumoXCo__IdIns__123EB7A3");
        });

        modelBuilder.Entity<InsumoXOrdenCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InsumoXO__3214EC070F624AF8");

            entity.ToTable("InsumoXOrdenCompra");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.CantidadRecibida).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.IdInsumoXcotizacion).HasColumnName("IdInsumoXCotizacion");
            entity.Property(e => e.ImporteConIva).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.ImporteSinIva).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(28, 6)");
            entity.Ignore(e => e.Isr);
            entity.Ignore(e => e.Iva);
            entity.Ignore(e => e.Ieps);
            entity.Ignore(e => e.Isan);

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.InsumoXordenCompras)
                .HasForeignKey(d => d.IdInsumo)
                .HasConstraintName("FK__InsumoXOr__IdIns__5E8A0973");

            entity.HasOne(d => d.IdInsumoXcotizacionNavigation).WithMany(p => p.InsumoXordenCompras)
                .HasForeignKey(d => d.IdInsumoXcotizacion)
                .HasConstraintName("FK__InsumoXOr__IdIns__5D95E53A");

            entity.HasOne(d => d.IdOrdenCompraNavigation).WithMany(p => p.InsumoXordenCompras)
                .HasForeignKey(d => d.IdOrdenCompra)
                .HasConstraintName("FK__InsumoXOr__IdOrd__5F7E2DAC");
        });

        modelBuilder.Entity<InsumoXRequisicion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InsumoXR__3214EC0709A971A2");

            entity.ToTable("InsumoXRequisicion");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.CantidadComprada).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.CantidadEnAlmacen).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Folio).HasMaxLength(15);
            entity.Property(e => e.Observaciones).HasMaxLength(300);
            entity.Property(e => e.PersonaIniciales).HasMaxLength(200);
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.InsumoXrequisicions)
                .HasForeignKey(d => d.IdInsumo)
                .HasConstraintName("FK__InsumoXRe__IdIns__0B91BA14");

            entity.HasOne(d => d.IdRequisicionNavigation).WithMany(p => p.InsumoXrequisicions)
                .HasForeignKey(d => d.IdRequisicion)
                .HasConstraintName("FK__InsumoXRe__IdReq__0C85DE4D");
        });

        modelBuilder.Entity<FormaPagoSat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FormaPag__3214EC07C018D665");

            entity.ToTable("FormaPagoSat", "Factura");

            entity.Property(e => e.Clave).HasMaxLength(3);
            entity.Property(e => e.Concepto).HasMaxLength(50);
        });

        modelBuilder.Entity<OrdenCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrdenCom__3214EC07160F4887");

            entity.ToTable("OrdenCompra");

            entity.Property(e => e.Chofer).HasMaxLength(30);
            entity.Property(e => e.Elaboro).HasMaxLength(30);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NoOrdenCompra).HasMaxLength(90);
            entity.Property(e => e.Observaciones).HasMaxLength(200);
            entity.Property(e => e.EstatusSaldado).HasColumnName("EstatusSaldado");
            entity.Property(e => e.TotalSaldado).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdCotizacionNavigation).WithMany(p => p.OrdenCompras)
                .HasForeignKey(d => d.IdCotizacion)
                .HasConstraintName("FK__OrdenComp__IdCot__18EBB532");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.OrdenCompras)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__OrdenComp__IdPro__19DFD96B");

            entity.HasOne(d => d.IdRequisicionNavigation).WithMany(p => p.OrdenCompras)
                .HasForeignKey(d => d.IdRequisicion)
                .HasConstraintName("FK__OrdenComp__IdReq__1AD3FDA4");

            entity.HasOne(d => d.IdContratistaNavigation).WithMany(p => p.OrdenCompras)
                .HasForeignKey(d => d.IdContratista)
                .HasConstraintName("FK_OrdenCompra_IdCont");
        });

        modelBuilder.Entity<PeriodoEstimaciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PeriodoE__3214EC072610A626");

            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaTermino).HasColumnType("datetime");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.PeriodoEstimaciones)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PeriodoEs__IdPro__27F8EE98");
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Poliza__3214EC07534D60F1");

            entity.ToTable("Poliza");

            entity.Property(e => e.Concepto).HasMaxLength(25);
            entity.Property(e => e.FechaAlta).HasColumnType("datetime");
            entity.Property(e => e.FechaPoliza).HasColumnType("datetime");
            entity.Property(e => e.Folio).HasMaxLength(6);
            entity.Property(e => e.NumeroPoliza).HasMaxLength(25);
            entity.Property(e => e.Observaciones).HasMaxLength(100);

            entity.HasOne(d => d.IdTipoPolizaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdTipoPoliza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Poliza__IdTipoPo__5535A963");
        });

        modelBuilder.Entity<PolizaDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PolizaDe__3214EC0759FA5E80");

            entity.ToTable("PolizaDetalle");

            entity.Property(e => e.Concepto).HasMaxLength(100);
            entity.Property(e => e.Debe).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Haber).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdCuentaContableNavigation).WithMany(p => p.PolizaDetalles)
                .HasForeignKey(d => d.IdCuentaContable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PolizaDet__IdCue__5BE2A6F2");

            entity.HasOne(d => d.IdPolizaNavigation).WithMany(p => p.PolizaDetalles)
                .HasForeignKey(d => d.IdPoliza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PolizaDet__IdPol__5CD6CB2B");
        });

        modelBuilder.Entity<PorcentajeAcumuladoContrato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Porcenta__3214EC073DB3258D");

            entity.Property(e => e.PorcentajeDestajoAcumulado).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdPrecioUnitarioNavigation).WithMany(p => p.PorcentajeAcumuladoContrato)
                .HasForeignKey(d => d.IdPrecioUnitario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Porcentaj__IdPre__3F9B6DFF");
        });

        modelBuilder.Entity<ConjuntoIndirectos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conjunto__3214EC076D1A31F7");

            entity.ToTable("ConjuntoIndirectos");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.TipoCalculo).HasColumnName("TipoCalculo");
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.IdProyecto).HasColumnName("IdProyecto");


            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.ConjuntoIndirectos)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConjuntoIndirectos_IdProyecto");
        });

        modelBuilder.Entity<Indirectos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Indirect__3214EC078238E3C7");

            entity.ToTable("Indirectos");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.IdConjuntoIndirectos).HasColumnName("IdConjuntoIndirectos");
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.TipoIndirecto).HasColumnName("TipoIndirecto");
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.IdIndirectoBase).HasColumnName("IdIndirectoBase");

            entity.HasOne(d => d.IdConjuntoIndirectosNavigation).WithMany(p => p.Indirectos)
                .HasForeignKey(d => d.IdConjuntoIndirectos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Indirectos_IdConjuntoIndirectos");
        });

        modelBuilder.Entity<IndirectosXConcepto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Indirect__3214EC078238E3C7");

            entity.ToTable("IndirectosXConcepto");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.IdConcepto).HasColumnName("IdConcepto");
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.TipoIndirecto).HasColumnName("TipoIndirecto");
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.IdIndirectoBase).HasColumnName("IdIndirectoBase");

            entity.HasOne(d => d.IdConceptoNavigation).WithMany(p => p.IndirectosXConceptos)
                .HasForeignKey(d => d.IdConcepto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndirectosXConcepto_IdConcepto");
        });

        modelBuilder.Entity<PrecioUnitario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PrecioUn__3213E83F31B762FC");

            entity.ToTable("PrecioUnitario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.CantidadExcedente).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.IdConcepto).HasColumnName("idConcepto");
            entity.Property(e => e.IdPrecioUnitarioBase).HasColumnName("idPrecioUnitarioBase");
            entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

            entity.HasOne(d => d.IdConceptoNavigation).WithMany(p => p.PrecioUnitarios)
                .HasForeignKey(d => d.IdConcepto)
                .HasConstraintName("FK__PrecioUni__idCon__339FAB6E");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.PrecioUnitarios)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PrecioUni__idPro__3493CFA7");
        });

        modelBuilder.Entity<PrecioUnitarioDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PrecioUn__3214EC0770A8B9AE");

            entity.ToTable("PrecioUnitarioDetalle");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(26, 8)");
            entity.Property(e => e.CantidadExcedente).HasColumnType("decimal(26, 8)");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.PrecioUnitarioDetalles)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PrecioUni__IdIns__73852659");

            entity.HasOne(d => d.IdPrecioUnitarioNavigation).WithMany(p => p.PrecioUnitarioDetalles)
                .HasForeignKey(d => d.IdPrecioUnitario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PrecioUni__IdPre__72910220");
        });

        modelBuilder.Entity<ProgramacionEstimada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Programa__3213E83F7C4F7684");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdConcepto).HasColumnName("idConcepto");
            entity.Property(e => e.IdPadre).HasColumnName("idPadre");
            entity.Property(e => e.IdPrecioUnitario).HasColumnName("idPrecioUnitario");
            entity.Property(e => e.IdPredecesora).HasColumnName("idPredecesora");
            entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");
            entity.Property(e => e.Inicio).HasColumnType("datetime");
            entity.Property(e => e.Predecesor).HasMaxLength(50);
            entity.Property(e => e.Progreso).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Termino).HasColumnType("datetime");

            entity.HasOne(d => d.IdConceptoNavigation).WithMany(p => p.ProgramacionEstimada)
                .HasForeignKey(d => d.IdConcepto)
                .HasConstraintName("FK__Programac__idCon__4F47C5E3");

            entity.HasOne(d => d.IdPrecioUnitarioNavigation).WithMany(p => p.ProgramacionEstimada)
                .HasForeignKey(d => d.IdPrecioUnitario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Programac__idPre__503BEA1C");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.ProgramacionEstimada)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Programac__idPro__51300E55");
        });

        modelBuilder.Entity<ProgramacionEstimadaGantt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Programa__3214EC07B9AEE129");

            entity.ToTable("ProgramacionEstimadaGantt");

            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaTermino).HasColumnType("datetime");

            entity.HasOne(d => d.IdConceptoNavigation).WithMany(p => p.ProgramacionEstimadaGantts)
                .HasForeignKey(d => d.IdConcepto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Programac__IdCon__290D0E62");

            entity.HasOne(d => d.IdPrecioUnitarioNavigation).WithMany(p => p.ProgramacionEstimadaGantts)
                .HasForeignKey(d => d.IdPrecioUnitario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Programac__IdPre__2818EA29");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.ProgramacionEstimadaGantts)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Programac__IdPro__2724C5F0");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC0708EA5793");

            entity.ToTable("Proyecto");

            entity.Property(e => e.Anticipo).HasColumnType("decimal(26, 8)");
            entity.Property(e => e.CodigoProyecto).HasMaxLength(30);
            entity.Property(e => e.Domicilio).HasMaxLength(100);
            entity.Property(e => e.FechaFinal).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Moneda).HasMaxLength(10);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PorcentajeIva).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.PresupuestoConIvaMonedaNacional).HasColumnType("decimal(26, 8)");
            entity.Property(e => e.PresupuestoSinIva).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.PresupuestoSinIvaMonedaNacional).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.TipoCambio).HasColumnType("decimal(28, 6)");
        });

        modelBuilder.Entity<RelacionFSRInsumo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Relacion__3214EC0718B6AB08");

            entity.ToTable("RelacionFSRInsumo");

            entity.Property(e => e.Prestaciones).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.SueldoBase).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.RelacionFsrinsumos)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RelacionF__IdIns__1B9317B3");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.RelacionFsrinsumos)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RelacionF__IdPro__1A9EF37A");
        });

        modelBuilder.Entity<Requisicion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requisic__3214EC077E37BEF6");

            entity.ToTable("Requisicion");

            entity.Ignore(e => e.CodigoBusqueda);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.NoRequisicion).HasMaxLength(50);
            entity.Property(e => e.Observaciones).HasMaxLength(200);
            entity.Property(e => e.PersonaSolicitante).HasMaxLength(100);
            entity.Property(e => e.Residente).HasMaxLength(200);

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Requisicions)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Requisici__IdPro__00200768");
        });

        modelBuilder.Entity<Rubro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rubro__3214EC07117F9D94");

            entity.ToTable("Rubro");

            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<SaldosBalanzaComprobacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SaldosBa__3214EC0747A6A41B");

            entity.ToTable("SaldosBalanzaComprobacion");

            entity.Property(e => e.Debe).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.Haber).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.SaldoFinal).HasColumnType("decimal(28, 6)");
            entity.Property(e => e.SaldoInicial).HasColumnType("decimal(28, 6)");

            entity.HasOne(d => d.IdCuentaContableNavigation).WithMany(p => p.SaldosBalanzaComprobacions)
                .HasForeignKey(d => d.IdCuentaContable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaldosBal__IdCue__498EEC8D");
        });

        modelBuilder.Entity<TipoInsumo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoInsu__3214EC071CF15040");

            entity.ToTable("TipoInsumo");

            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoPoliza>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TipoPoliza_2024_05_14");

            entity.ToTable("TipoPoliza");

            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<FacturaXOrdenCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PrecioUn__3214EC0770A8B9AE");

            entity.ToTable("FacturaXOrdenCompra");

            entity.Property(e => e.Estatus).HasColumnName("Estatus");
            entity.Property(e => e.TotalSaldado).HasColumnType("decimal(28, 6)");


            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturaXOrdenCompras)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaXOrdenCompra_IdFactura");

            entity.HasOne(d => d.IdOrdenCompraNavigation).WithMany(p => p.FacturaXOrdenCompras)
                .HasForeignKey(d => d.IdOrdenCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaXOrdenCompra_IdOrdenCompra");
        });

        base.OnModelCreating(modelBuilder);
    }
}

