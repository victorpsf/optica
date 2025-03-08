import React, { JSX } from "react";
import Login from "./components/Login";
import Code from "./components/Code";

export type LoginStep = 'LOGIN' | 'CODE';

export interface ILoginPageState {
    step: LoginStep;
}

const LoginPage = function (): JSX.Element {
    const [state, setState] = React.useState<ILoginPageState>({
        step: 'LOGIN'
    });

    return (
        <div className="w-full h-full flex justify-center items-center">
            {state.step === 'LOGIN' && (<Login changeStep={(step) => setState({ ...state, step })} />)}
            {state.step === 'CODE' && (<Code changeStep={(step) => setState({ ...state, step })} />)}
        </div>
    )
}

export default LoginPage;