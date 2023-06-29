export interface GetProjectTeamDto {
  users: GetProjectTeamUserDto[];
}

export interface GetProjectTeamUserDto {
  id: number;
  firstName: string;
  lastName: string;
  role: string;
  email: string;
  phoneNumber: string;
}
