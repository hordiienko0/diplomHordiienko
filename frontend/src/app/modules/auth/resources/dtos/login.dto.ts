export interface LoginDto {
  user: LoginUserDto;
  askToChangePassword: boolean;
  accessToken: LoginTokenDto;
  refreshToken: LoginTokenDto;
}

export interface LoginUserDto {
  id: number;
  email: string;
}

export interface LoginTokenDto {
  token: string;
  expires: string;
}
