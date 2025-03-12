import React, { JSX } from "react";
import FloatLabel from "../FloatLabel";
import { sleep } from "../../lib/Thread";

export interface IPasswordFieldProps {
    label?: string;
    placeholder?: string;
    value?: string;
    max?: number;
    onChange: (value?: string) => void;

    handleMainClassName?: () => string;
    sleepTime?: number;
    onSleep?: (v1?: string, v2?: string) => void;
}

const PasswordField = function (props: IPasswordFieldProps): JSX.Element {
    const ref = React.useRef<HTMLInputElement | null>(null);

    const mainClassName  = function (): string {
        if (props.handleMainClassName)
            return props.handleMainClassName();

        return "w-full p-2 h-[52px] my-2 border-2 rounded relative";
    }

    return (
        <div className="w-full">
            <div
                className={mainClassName()}
            >
                {props.label && (
                    <FloatLabel 
                        value={props.label} has={(props.value?.length || 0) > 0} 
                        onPress={() => {
                            ref.current?.click();
                            ref.current?.focus();
                        }}
                    />
                )}

                <input 
                    ref={ref}
                    className="outline-[0px] w-full h-full p-2" 
                    placeholder={props.placeholder} 
                    type="password"
                    value={props.value} 
                    maxLength={props.max}
                    onChange={async (ev) => {
                        const value = ev.target.value;
                        props.onChange(value);

                        if (props.onSleep) {
                            await sleep(props.sleepTime || 0.5)
                            props.onSleep(value, ev.target.value);
                        }
                    }} 
                />
            </div>

            {(props.max) && (
                <div className="w-full text-end text-[10px] text-gray-600 px-2">
                    {`${props.max - (props.value?.length || 0)} caracteres`}
                </div>
            )}
        </div>
    )
}

export default PasswordField;