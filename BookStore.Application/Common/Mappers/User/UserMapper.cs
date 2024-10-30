using BookStore.Application.Common.Dto.Users;
using BookStore.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace BookStore.Application.Common.Mappers.User;

[Mapper]
public static partial class UserMapper
{
    public static GetAllUsersQueryResponseDto FromListApplicationUserToGetAllUsersQueryResponseDto(
        this List<ApplicationUser> users)
    {
        return new GetAllUsersQueryResponseDto(users.Select(u => new UserDto(
            u.FirstName!,
            u.LastName!,
            u.UserName!,
            u.Email!,
            u.Roles.Any() ? string.Join(", ", u.Roles.Select(ur => ur.Role.Name)) : "N/A", 
            u.LoyaltyProgram.LoyaltyPoints,
            u.LoyaltyProgram.DiscountPercentage,
            u.LoyaltyProgram.EnrollmentDate
        )).ToList());
    }
}