export interface GetCompanyUsersDto {
  users: GetCompanyUsersUserDto[];
}

export interface GetCompanyUsersUserDto {
  id: number;
  firstName: string;
  lastName: string;
  role: string;
  email: string;
  phoneNumber: string;
}
