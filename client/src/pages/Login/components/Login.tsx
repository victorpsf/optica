import React, { JSX } from "react";
import StringField from "../../../components/fields/StringField";
import PasswordField from "../../../components/fields/PasswordField";
import { AuthenticationDto, AuthenticationNameDto, EnterpriseDto } from "../../../services/interface/Authentication";
import Action from "../../../components/Action";
import AuthenticationService from "../../../services/AuthenticationService";
import { LoginStep } from "../LoginPage";
import SelectField from "../../../components/fields/SelectField";

export interface ILoginProps {
    changeStep: (type: LoginStep) => void;
}

const Login = function (props: ILoginProps): JSX.Element {
    const [state, setState] = React.useState<AuthenticationDto>({});
    const [enterprises, setEnterprises] = React.useState<EnterpriseDto[]>([]);
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

    const loadEnterprises = async function (key: keyof AuthenticationNameDto, value?: string): Promise<void> {
        setLoading(true);
        try {
            const params: AuthenticationNameDto = {};
            params[key] = value;
            params[key === 'name' ? 'email': 'name'] = state[key === 'name' ? 'email': 'name'];

            const response = await AuthenticationService.enterprises(params);
            setEnterprises(response);
        }

        catch (error) { }
        setLoading(false);
    }

    return (
        <div className="p-4 bg-white rounded md:w-[550px] w-[80%]">
            <StringField 
                label="Usuário" 
                max={500} 
                value={state.name} 
                onChange={(value) => setState({ ...state, name: value })} 
                sleepTime={0.5}
                onSleep={(v1, v2) => {
                    if (v1?.length !== v2?.length)
                        return;

                    if (v1?.length === 0)
                        setEnterprises([])

                    else loadEnterprises('name', v1);
                }}
            />
            <StringField 
                label="E-mail" 
                max={500} 
                value={state.email} 
                onChange={(value) => setState({ ...state, email: value })} 
                sleepTime={0.5}
                onSleep={(v1, v2) => {
                    if (v1?.length !== v2?.length)
                        return;

                    if (v1?.length === 0)
                        setEnterprises([])

                    else loadEnterprises('email', v1);
                }}
            />
            <PasswordField 
                label="Senha" 
                max={400} 
                value={state.password} 
                onChange={(value) => setState({ ...state, password: value })} 
            />

            {enterprises.length === 0 ? (
                <div className="p-2 text-red-500">{'Informe o e-mail ou usuário para carregar a empresa'}</div>
            ): (
                <SelectField
                    label="Empresa"
                    value={enterprises.find(a => a.enterpriseId === state.enterpriseId)}
                    onChange={(value: any) => setState({ ...state, enterpriseId: value === undefined? undefined: value.enterpriseId })}
                    options={enterprises}
                    handleLabel={(value) => value.name}
                />
            )}

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