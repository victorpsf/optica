import React, { JSX } from "react";
import StringField from "../../../components/fields/StringField";
import PasswordField from "../../../components/fields/PasswordField";
import { AuthenticationDto } from "../../../services/interface/Authentication";
import Action from "../../../components/Action";
import AuthenticationService from "../../../services/AuthenticationService";
import { LoginStep } from "../LoginPage";

export interface ILoginProps {
    changeStep: (type: LoginStep) => void;
}

const Login = function (props: ILoginProps): JSX.Element {
    const [state, setState] = React.useState<AuthenticationDto>({});
    const [loading, setLoading] = React.useState<boolean>(false);

    const onAuthenticateClick = async function (): Promise<void> {
        setLoading(true);
        try {
            await AuthenticationService.signIn(state);
            props.changeStep('CODE');
        }

        catch (error) { }
        setLoading(false);
    }

    return (
        <div className="p-4 bg-white rounded md:w-[550px] w-[80%]">
            <StringField label="Usuário" max={500} value={state.name} onChange={(value) => setState({ ...state, name: value })} />
            <StringField label="E-mail" max={500} value={state.email} onChange={(value) => setState({ ...state, email: value })} />
            <PasswordField label="Senha" max={400} value={state.password} onChange={(value) => setState({ ...state, password: value })} />

            <div className="w-full pt-4">
                <Action 
                    text="Acessar"
                    color="white"
                    border="white"
                    disabled={(
                        (((state.email?.length || 0) === 0) || ((state.name?.length || 0) === 0)) &&
                        ((state.password?.length || 0) === 0)
                    )}
                    loading={loading}
                    onPress={onAuthenticateClick}
                />
            </div>
        </div>
    )
}

export default Login;