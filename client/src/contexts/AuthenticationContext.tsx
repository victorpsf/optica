import React, { JSX } from "react";
import LoginPage from "../pages/Login/LoginPage";
import { AuthTokenDto } from "../services/interface/Authentication";
import { UserDto } from "../services/interface/Claim";
import { GetUser } from "../lib/UserUtils";
import AppStorage from "../lib/AppStorage";


export interface IAuthenticationContext {
    authenticated: boolean;
    user?: UserDto;

    signIn: (data: AuthTokenDto) => void;
    singOut: () => void;
}

export interface IAuthenticationProviderProps {
    children?: JSX.Element | JSX.Element[];
}

export const AuthenticationContext = React.createContext<IAuthenticationContext>({ authenticated: false, signIn: () => {}, singOut: () => {} });

export const AuthenticationProvider = function ({ children }: IAuthenticationProviderProps): JSX.Element {
    const [authenticated, setAuthenticated] = React.useState<boolean>(false);
    const [user, setUser] = React.useState<UserDto>();

    const signIn = function (value: AuthTokenDto): void {
        AppStorage.set(value, 'authenticated');
        setAuthenticated(true);
        setUser(GetUser());
    }

    const singOut = function (): void {
        AppStorage.unset('authenticated');
        setAuthenticated(false);
        setUser(undefined);
    }

    React.useEffect(() => {
        try {
            var user = GetUser();
            if (!user) return;
            
            if (user.expire > new Date()) {
                setAuthenticated(true);
                setUser(user);
            }
        }

        catch (err) { }
    }, []);

    return (
        <AuthenticationContext.Provider value={{ authenticated, user, signIn, singOut }}>
            <div className="w-full h-full bg-gray-800">
                {authenticated && (children)}
                {!authenticated && <LoginPage />}
            </div>
        </AuthenticationContext.Provider>
    )
}