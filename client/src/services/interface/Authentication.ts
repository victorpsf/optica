
export interface AuthenticationDto {
    name?: string;
    email?: string;
    password?: string;
}

export interface AuthenticationCodeDto {
    code?: string;
}

export interface AuthTokenDto
{
    token: string;
    type: string;
}