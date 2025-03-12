
export interface AuthenticationNameDto {
    name?: string;
    email?: string;
}

export interface AuthenticationDto extends AuthenticationNameDto {
    password?: string;
    enterpriseId?: number;
}

export interface AuthenticationCodeDto {
    code?: string;
}

export interface AuthTokenDto
{
    token: string;
    type: string;
}

export interface EnterpriseDto {
    enterpriseId: number;
    name: string;
    active: boolean;
    createdAt: string;
    deletedAt: string | null;
}