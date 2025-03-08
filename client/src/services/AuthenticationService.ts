import { ENV } from "../lib/ENV";
import { Client, IClient } from "./Client";
import { AuthenticationCodeDto, AuthenticationDto, AuthTokenDto } from "./interface/Authentication";
import { EmptyReponse } from "./interface/RequestDto";

class AuthenticationService {
    client: IClient;

    constructor() {
        this.client = Client(ENV.AUTHENTICATION_BASE_PATH);
    }

    async signIn(data: AuthenticationDto): Promise<EmptyReponse> {
        return await this.client.post<EmptyReponse>('/auth/Authentication', data);
    }

    async code(data: AuthenticationCodeDto): Promise<AuthTokenDto> {
        return await this.client.post<AuthTokenDto>('/auth/Authentication/Code', data);
    }
}

export default new AuthenticationService();