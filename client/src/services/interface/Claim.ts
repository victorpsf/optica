
export interface UserClaim {
    exp: number;
    iat: number;
    iss: string;
    nbf: number;
    uI: string;
}

export interface UserDto {
    expire: Date;
    generated: Date;
    assinador: string;
    nbf: number;
    userId: number;
}