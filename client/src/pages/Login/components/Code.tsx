import React, { JSX } from "react";
import StringField from "../../../components/fields/StringField";
import { AuthenticationCodeDto } from "../../../services/interface/Authentication";
import Action from "../../../components/Action";
import AuthenticationService from "../../../services/AuthenticationService";
import { LoginStep } from "../LoginPage";
import { AuthenticationContext } from "../../../contexts/AuthenticationContext";

export interface ICodeProps {
    changeStep: (type: LoginStep) => void;
}

const Code = function (props: ICodeProps): JSX.Element {
    const auth = React.useContext(AuthenticationContext);
    const [state, setState] = React.useState<AuthenticationCodeDto>({});
    const [loading, setLoading] = React.useState<boolean>(false);

    const onAuthenticateClick = async function (): Promise<void> {
        setLoading(true);
        try {
            var response = await AuthenticationService.code(state);
            auth.signIn(response);
        }

        catch (err) {

        }
        setLoading(false);
    }

    return (
        <div className="p-4 bg-white rounded md:w-[550px] w-[80%]">
            <StringField label="Código" max={500} value={state.code} onChange={(value) => setState({ ...state, code: value })} />

            <div className="w-full pt-4">
                <Action 
                    text="Acessar"
                    color="white"
                    border="white"
                    disabled={(((state.code?.length || 0) === 0))}
                    loading={loading}
                    onPress={onAuthenticateClick}
                />
            </div>
        </div>
    )
}

export default Code;