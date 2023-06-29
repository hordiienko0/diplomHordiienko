using Ctor.Domain.Repositories;

namespace Ctor.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    ICompanyRepository Companies { get; }
    IBuildingRepository Buildings { get; }
    IProjectRepository Projects { get; }
    IPhaseRepository Phases { get; }
    IProjectPhotoRepository ProjectsPhotos { get; }
    ICompanyLogoRepository CompanyLogos { get; }
    IBuildingBlockRepository BuildingBlocks { get; }
    IRequiredMaterialRepository RequiredMaterials { get; }
    INotificationRepository Notifications { get; }
    IAssigneeRepository Assignees { get; }
    IProjectDocumentRepository ProjectDocuments { get; }
    IDocumentRepository Documents { get; }
    IMaterialRepository Materials { get; }
    IMaterialTypeRepository MaterialType { get; }
    IMeasurementRepository Measurements { get; }
    IVendorRepository Vendors { get; }
    IVendorTypeRepository VendorTypes { get; }
    IPhaseStepRepository PhaseSteps { get; }
    IRequiredServiceRepository RequiredServices { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
