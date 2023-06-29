using Bogus;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ctor.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly INumberGenerateService _numberGenerateService;
    private readonly Faker _faker;

    private readonly Role _adminRole =
        new() { Name = "Admin", Type = UserRoles.Admin };

    private readonly Role _operationalManagerRole =
        new() { Name = "Operational manager", Type = UserRoles.OperationalManager };

    private readonly Role _projectManagerRole =
        new() { Name = "Project manager", Type = UserRoles.ProjectManager };

    private readonly Role _mainEngineerRole =
        new() { Name = "Main engineer", Type = UserRoles.MainEngineer };

    private readonly Role _foremanRole =
        new() { Name = "Foreman", Type = UserRoles.Foreman };

    private readonly Measurement _itemMeasurement = new() { Name = "Item" };
    private readonly Measurement _kiloMeasurement = new() { Name = "Kilo" };
    private readonly Measurement _footMeasurement = new() { Name = "Foot" };
    private readonly Measurement _tonsMeasurement = new() { Name = "Tons" };

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context,
        INumberGenerateService numberGenerateService)
    {
        _logger = logger;
        _context = context;
        _numberGenerateService = numberGenerateService;

        _faker = new Faker
        {
            Random = new Randomizer(123), //
        };
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedInternalAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
    ICollection<VendorType> VendorTypes = new List<VendorType>()
    {
            new VendorType(){Name="Architectural"},
            new VendorType(){Name="Civil Engineering"},
            new VendorType(){Name="Communications"},
            new VendorType(){Name="Electrical Engineering"},
            new VendorType(){Name="Energy Management"},
            new VendorType(){Name="Fire Safety Engineering"},
            new VendorType(){Name="Geotechnical"},
            new VendorType(){Name="Building Surveyor"},
            new VendorType(){Name="Quantity Surveyor"},
            new VendorType(){Name="Acoustics"},
            new VendorType(){Name="Lifts"},
            new VendorType(){Name="Internet"},
            new VendorType(){Name="Cleaning"},
            new VendorType(){Name="Heating"},
            new VendorType(){Name="Security "},
            new VendorType(){Name="Adapters"},
            new VendorType(){Name="Plumbing"},
            new VendorType(){Name="Wiring"},
            new VendorType(){Name="Design"},
            new VendorType(){Name="Drywall"},
            new VendorType(){Name="Deconstruction"},
            new VendorType(){Name="Smart"},
            new VendorType(){Name="CCTV"}
    };
    private async Task SeedInternalAsync()
    {
        if (_context.Roles.Any() || _context.Companies.Any())
        {
            return;
        }

        await _context.Roles.AddRangeAsync(_adminRole, _operationalManagerRole,
            _projectManagerRole, _mainEngineerRole, _foremanRole);


        await _context.Measurements.AddRangeAsync(_itemMeasurement, _kiloMeasurement,
            _footMeasurement, _tonsMeasurement);

        await _context.Users.AddRangeAsync(
            new User
            {
                FirstName = "admin",
                LastName = "admin",
                UserEmail = "admin@ctor.com",
                Password = "12345",
                AskToChangeDefaultPassword = false,
                Role = _adminRole,
                CompanyId = null,
            });

        var materialTypes = GeneraMaterialTypes();

        await _context.Companies.AddRangeAsync(GenerateCompanies(materialTypes));

        await _context.VendorTypes.AddRangeAsync(VendorTypes);

        await _context.SaveChangesAsync();
    }

    private ICollection<Company> GenerateCompanies(ICollection<MaterialType> materialTypes)
    {
        return Enumerable.Range(0, 4)
            .Select(i =>
            {
                var users = GenerateUsers(((char)('a' + i)).ToString());
                var materials = GenerateMaterials(materialTypes);

                return new Company
                {
                    CompanyId = _faker.Random.Long(1000000, 10000000000),
                    CompanyName = _faker.Company.CompanyName(),
                    Description = _faker.Company.CatchPhrase(),
                    Country = _faker.Address.County(),
                    City = _faker.Address.City(),
                    Address = _faker.Address.StreetAddress(),
                    Email = _faker.Internet.Email(),
                    JoinDate = _faker.Date.Past(yearsToGoBack: 5).ToUniversalTime(),
                    Website = _faker.Internet.DomainName(),
                    Users = users,
                    Projects = GenerateProjects(users.First(u => u.Role.Type == UserRoles.OperationalManager),
                        materials),
                    Materials = materials,
                    Vendors = GenerateVendors()
                };
            })
            .ToArray();
    }

    private record VendorSeed(string VendorName);

    private readonly ICollection<VendorSeed> _vendors = new VendorSeed[]
    {
        new VendorSeed("Asrta"),
        new VendorSeed("Hotline"),
        new VendorSeed("Invisible"),
        new VendorSeed("Heating"),
        new VendorSeed("Briliant"),
        new VendorSeed("Externum"),
        new VendorSeed("Lotos")
    };

    private ICollection<Vendor> GenerateVendors()
    {
        return Enumerable.Range(0, _faker.Random.Int(1, 3))
            .Select(_ =>
            {
                var vendor = _faker.Random.CollectionItem(_vendors);

                return new Vendor
                {
                    VendorName = vendor.VendorName,
                    Email = $"{vendor.VendorName.ToLower()}@{vendor.VendorName.Substring(0, 3).ToLower()}.ua",
                    Website = $"https://{vendor.VendorName.ToLower()}.com",
                    Phone = _faker.Phone.PhoneNumber("+380 9# ### ## ##"),
                    VendorTypes = new List<VendorType>() { _faker.Random.CollectionItem(VendorTypes) }
                };
            })
            .ToArray();
    }

    private ICollection<User> GenerateUsers(string emailNameSuffix)
    {
        return Enumerable.Range(1, 4)
            .Select(i => new User
            {
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                UserEmail = $"{i}{emailNameSuffix}@ctor.com",
                PhoneNumber = _faker.Phone.PhoneNumber("+380 9# ### ## ##"),
                Password = "12345",
                Role = i switch
                {
                    1 => _operationalManagerRole,
                    2 => _projectManagerRole,
                    3 => _mainEngineerRole,
                    4 => _foremanRole,
                    _ => throw new ArgumentOutOfRangeException(nameof(i), i, null)
                },
            })
            .ToArray();
    }

    private record ProjectSeed(string ProjectName, string ProjectType);

    private readonly ICollection<ProjectSeed> _projects = new ProjectSeed[]
    {
        new("Leessas Town", "Roads"), //
        new("Stoagem Valley", "Park"), //
        new("Rufus Street", "Housing District"), //
        new("Cliftonville", "Mansion"), //
        new("Plotezalf Center", "Shopping Centre"), //
        new("Wistful Vista", "Recreation Complex"), //
        new("Upper South Xorisk", "Housing District"), //
        new("Ropewalks", "Roads"), //
    };

    private ICollection<Project> GenerateProjects(User owner, ICollection<Material> materials)
    {
        return Enumerable.Range(0, _faker.Random.Int(1, 3))
            .Select(_ =>
            {
                var project = _faker.Random.CollectionItem(_projects);
                var phases = GeneratePhases();

                var status = phases.All(p => p.EndTime <= DateTime.UtcNow)
                    ? ProjectStatus.Finished
                    : phases.All(p => p.StartTime >= DateTime.UtcNow)
                        ? ProjectStatus.NotStarted
                        : ProjectStatus.InProcess;

                if (status == ProjectStatus.InProcess && _faker.Random.Bool(weight: 0.4f))
                {
                    status = ProjectStatus.Suspended;
                }

                return new Project
                {
                    ProjectId = _numberGenerateService.GetRandomNumberForId(),
                    ProjectName = project.ProjectName,
                    ProjectType = project.ProjectType,
                    Country = _faker.Address.Country(),
                    City = _faker.Address.City(),
                    Address = _faker.Address.StreetAddress(),
                    Budget = _faker.Random.Long(1_000, 1_000_000_000),
                    Status = status,
                    StartTime = _faker.Date.Past(yearsToGoBack: 5).ToUniversalTime(),
                    EndTime = phases.Last().EndTime,
                    User = owner,
                    Assignees = new List<Assignee> { new() { User = owner, } },
                    Building = GenerateBuildings(materials),
                    Phases = phases,
                };
            })
            .ToArray();
    }

    private ICollection<Material> GenerateMaterials(ICollection<MaterialType> materialTypes)
    {
        return Enumerable.Range(0, 3)
            .Select(_ => new Material
            {
                Amount = _faker.Random.Int(250, 550),
                Measurement = _tonsMeasurement,
                CompanyName = _faker.Company.CompanyName(),
                CompanyAddress = $"{_faker.Address.City()}, {_faker.Address.StreetAddress()}, {_faker.Address.Country()}",
                Price = _faker.Random.Int(25, 45),
                MaterialType = _faker.Random.CollectionItem(materialTypes),
            })
            .ToArray();
    }

    private readonly string[] _materialTypes = new[]
    {
        "Mud bricks", "Facing bricks", "Extruded bricks", "Engineering bricks", "Common bricks", "OPC", "PPC",
        "White cement", "Colored cement", "Hydrographic cement", "High-alumina cement", "Portland slag cement",
        "Float glass", "Shatterproof glass", "Laminated glass", "Extra clean glass", "Chromatic glass",
        "Tinted glass", "Toughened glass", "Glass blocks", "Glass wool", "Insulated glazed units", "River sand",
        "Concrete sand", "Coarse sand", "Utility sand", "Pit sand", "Fine sand", "Fill sand", "Desert sand",
        "Beach sand", "Marine sand", "Basalt", "Granite", "Sandstone", "Slate", "Limestone", "Laterite", "Marble",
        "Gneiss", "Quartzite", "Travertine", "Pinewood", "Cedarwood", "Firwood", "Hemlock timber", "Teak wood",
        "Oakwood", "Maple wood", "Cherry wood", "Walnut wood", "Beechwood", "Mahogany", "Sal wood", "Plywood",
    };

    private ICollection<MaterialType> GeneraMaterialTypes()
    {
        return _materialTypes
            .Select(t => new MaterialType { Name = t, })
            .ToArray();
    }

    private record BuildingSeed(string BuildingName);

    private readonly ICollection<BuildingSeed> _buildings = new BuildingSeed[]
    {
        new("First floor"), //
        new("Parking"), //
        new("Park"), //
        new("Second floor"), //
    };

    private ICollection<Building> GenerateBuildings(ICollection<Material> materials)
    {
        return Enumerable.Range(0, _faker.Random.Int(2, 4))
            .Select(_ =>
            {
                _faker.Random.Int();
                var building = _faker.Random.CollectionItem(_buildings);

                return new Building
                {
                    BuildingName = building.BuildingName, //
                    RequiredMaterials = GenerateRequiredMaterials(materials),
                };
            })
            .ToArray();
    }

    private ICollection<RequiredMaterial> GenerateRequiredMaterials(ICollection<Material> materials)
    {
        return Enumerable.Range(0, 1)
            .Select(_ =>
            {
                var material = _faker.Random.CollectionItem(materials);

                return new RequiredMaterial
                {
                    Material = material, //
                    Amount = _faker.Random.Long(1, 5),
                };
            })
            .ToArray();
    }

    private readonly string[] _phaseNames = new[]
    {
        "Planning", //
        "Pre-construction", //
        "Procurement", //
        "Construction", //
        "Post-construction", //
    };

    private ICollection<Phase> GeneratePhases()
    {
        var currentPhase = _faker.Random.Int(0, _phaseNames.Length);

        DateTime GenerateTime(int phaseStep) => phaseStep >= currentPhase
            ? _faker.Date.Future(yearsToGoForward: 5)
            : _faker.Date.Past(yearsToGoBack: 5);

        

        return Enumerable.Range(0, _phaseNames.Length)
            .Select(phaseStep =>
            {
                var start = GenerateTime(phaseStep).ToUniversalTime();
                var end = GenerateTime(phaseStep).ToUniversalTime();
                var steps = GeneratePhaseSteps(start, end);
                return new Phase
                {
                    PhaseName = _phaseNames[phaseStep],
                    StartTime = start,
                    EndTime = end,
                    PhaseStep = phaseStep,
                    PhaseSteps = steps
                };
            })
            .ToArray();
    }

    private readonly string[] _phaseStepNames = new[]
    {
        "Build first floor",
        "Bring Cement",
        "Place a playground"
    };

    private ICollection<PhaseStep> GeneratePhaseSteps(DateTime start, DateTime end)
    {
        return Enumerable.Range(0, _phaseStepNames.Length)
            .Select(i =>
            {
                var startDate = _faker.Date.Between(start, end);
                var endDate = _faker.Date.Between(startDate, end);
                return new PhaseStep
                {
                    PhaseStepName = _phaseStepNames[i],
                    StartDate = startDate.ToUniversalTime(),
                    EndDate = endDate.ToUniversalTime(),
                    IsDone = false
                };
            }).ToArray();

    }
}