using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IClubInvitationService, ClubInvitationService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IClubJoinRequestService, ClubJoinRequestService>();
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IClubMemberManagementService, ClubMemberManagementService>();
        services.AddScoped<IRatingService, RatingService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>));
        services.AddScoped<IClubInvitationRepository, ClubInvitationDatabaseRepository>();
        services.AddScoped<IClubMembershipRepository, ClubMembershipDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<ClubInvitation>), typeof(CrudDatabaseRepository<ClubInvitation, StakeholdersContext>));
        services.AddScoped<IClubRepository, ClubRepository>();
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped<IClubJoinRequestRepository, ClubJoinRequestRepository>();
        services.AddScoped<IRatingRepository, RatingDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<Rating>), typeof(CrudDatabaseRepository<Rating, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped<IPersonRepository, PersonDataBaseRepository>();

        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));

    }
}