import { AuthTokenDto } from "../services/interface/Authentication"
import { UserClaim, UserDto } from "../services/interface/Claim";
import AppStorage from "./AppStorage"
import { Buffer } from 'buffer';

export const ReadByToken = function (token?: string): UserClaim {
    const parts = token?.split('.').map(a => Buffer.from(a, 'base64')) || [];

    if (parts.length === 0)
        throw new Error('Failure read token');

    return JSON.parse(parts[1]?.toString('utf-8') || "{}");
}

export const ReadToken = function (): UserClaim {
    return ReadByToken(AppStorage.get<AuthTokenDto>('authenticated')?.token);
}

export const GetUser = function (): UserDto {
    const claim = ReadToken();

    return {
        expire: new Date(claim.exp * 1000),
        userId: parseInt(claim.uI),
        generated: new Date(claim.iat * 1000),
        assinador: claim.iss,
        nbf: claim.nbf
    };
}