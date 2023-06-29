using System.Reflection;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ctor.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly Lazy<IUserRepository> _userRepositoryLazy;
    private readonly Lazy<ICompanyRepository> _companyRepositoryLazy;
    private readonly Lazy<IBuildingRepository> _buildingRepositoryLazy;
    private readonly Lazy<IProjectRepository> _projectRepositoryLazy;
    private readonly Lazy<IPhaseRepository> _phaseRepositoryLazy;
    private readonly Lazy<IRoleRepository> _roleRepositoryLazy;
    private readonly Lazy<IProjectPhotoRepository> _projectPhotoRepository;
    private readonly Lazy<ICompanyLogoRepository> _companyLogoRepository;
    private readonly Lazy<IBuildingBlockRepository> _buildingBlockRepository;
    private readonly Lazy<IAssigneeRepository> _assigneeRepository;
    private readonly Lazy<INotificationRepository> _notificationRepositoryLazy;
    private readonly Lazy<IProjectDocumentRepository> _projectDocumentRepository;
    private readonly Lazy<IDocumentRepository> _documentRepository;
    private readonly Lazy<IMaterialRepository> _materialRepository;
    private readonly Lazy<IMaterialTypeRepository> _materialTypeRepository;
    private readonly Lazy<IMeasurementRepository> _measurementRepository;
    private readonly Lazy<IRequiredMaterialRepository> _requiredMaterialsRepository;
    private readonly Lazy<IVendorRepository> _vendorRepositoryLazy;
    private readonly Lazy<IVendorTypeRepository> _vendorTypeRepositoryLazy;
    private readonly Lazy<IRequiredServiceRepository> _requiredService;
    private readonly Lazy<IPhaseStepRepository> _phaseStepRepository;

    public IUserRepository Users => _userRepositoryLazy.Value;
    public IRoleRepository Roles => _roleRepositoryLazy.Value;
    public ICompanyRepository Companies => _companyRepositoryLazy.Value;
    public IBuildingRepository Buildings => _buildingRepositoryLazy.Value;
    public IProjectRepository Projects => _projectRepositoryLazy.Value;
    public IPhaseRepository Phases=> _phaseRepositoryLazy.Value;
    public IProjectPhotoRepository ProjectsPhotos => _projectPhotoRepository.Value;
    public ICompanyLogoRepository CompanyLogos => _companyLogoRepository.Value;
    public IBuildingBlockRepository BuildingBlocks => _buildingBlockRepository.Value;
    public INotificationRepository Notifications => _notificationRepositoryLazy.Value;
    public IAssigneeRepository Assignees => _assigneeRepository.Value;
    public IProjectDocumentRepository ProjectDocuments => _projectDocumentRepository.Value;
    public IDocumentRepository Documents => _documentRepository.Value;
    public IMaterialRepository Materials => _materialRepository.Value;
    public IMaterialTypeRepository MaterialType => _materialTypeRepository.Value;
    public IMeasurementRepository Measurements => _measurementRepository.Value;

    public IRequiredMaterialRepository RequiredMaterials => _requiredMaterialsRepository.Value;
    public IRequiredServiceRepository RequiredServices => _requiredService.Value;
    public IPhaseStepRepository PhaseSteps => _phaseStepRepository.Value;

    public IVendorRepository Vendors => _vendorRepositoryLazy.Value;
    public IVendorTypeRepository VendorTypes => _vendorTypeRepositoryLazy.Value;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IRepositoryFactory repositoryFactory)
        : base(options)
    {
        _userRepositoryLazy = repositoryFactory.GetInstanse<Lazy<IUserRepository>>();
        _roleRepositoryLazy = repositoryFactory.GetInstanse<Lazy<IRoleRepository>>();
        _companyRepositoryLazy = repositoryFactory.GetInstanse<Lazy<ICompanyRepository>>();
        _buildingRepositoryLazy = repositoryFactory.GetInstanse<Lazy<IBuildingRepository>>();
        _projectRepositoryLazy = repositoryFactory.GetInstanse<Lazy<IProjectRepository>>();
        _phaseRepositoryLazy = repositoryFactory.GetInstanse<Lazy<IPhaseRepository>>();
        _projectPhotoRepository = repositoryFactory.GetInstanse <Lazy<IProjectPhotoRepository>>();
        _companyLogoRepository = repositoryFactory.GetInstanse<Lazy<ICompanyLogoRepository>>();
        _buildingBlockRepository = repositoryFactory.GetInstanse<Lazy<IBuildingBlockRepository>>();
        _assigneeRepository = repositoryFactory.GetInstanse<Lazy<IAssigneeRepository>>();
        _notificationRepositoryLazy = repositoryFactory.GetInstanse<Lazy<INotificationRepository>>();
        _projectDocumentRepository = repositoryFactory.GetInstanse<Lazy<IProjectDocumentRepository>>();
        _documentRepository = repositoryFactory.GetInstanse<Lazy<IDocumentRepository>>();
        _materialRepository = repositoryFactory.GetInstanse<Lazy<IMaterialRepository>>();
        _materialTypeRepository = repositoryFactory.GetInstanse<Lazy<IMaterialTypeRepository>>();
        _measurementRepository = repositoryFactory.GetInstanse<Lazy<IMeasurementRepository>>();
        _requiredMaterialsRepository = repositoryFactory.GetInstanse<Lazy<IRequiredMaterialRepository>>();
        _requiredService = repositoryFactory.GetInstanse<Lazy<IRequiredServiceRepository>>();

        _vendorRepositoryLazy = repositoryFactory.GetInstanse<Lazy<IVendorRepository>>();
        _vendorTypeRepositoryLazy = repositoryFactory.GetInstanse<Lazy<IVendorTypeRepository>>();
        _phaseStepRepository = repositoryFactory.GetInstanse<Lazy<IPhaseStepRepository>>();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {       
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
