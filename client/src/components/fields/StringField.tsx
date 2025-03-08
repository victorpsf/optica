import { JSX } from "react";
import FloatLabel from "../FloatLabel";

export interface IStringFieldProps {
    label?: string;
    placeholder?: string;
    value?: string;
    max?: number;
    onChange: (value?: string) => void;

    handleMainClassName?: () => string;
}

const StringField = function (props: IStringFieldProps): JSX.Element {
    const mainClassName  = function (): string {
        if (props.handleMainClassName)
            return props.handleMainClassName();

        return "w-full p-2 h-[52px] mt-5 border-2 rounded relative";
    }

    return (
        <div className="w-full">
            <div
                className={mainClassName()}
            >
                {props.label && <FloatLabel value={props.label} has={(props.value?.length || 0) > 0} />}

                <input 
                    className="outline-[0px] w-full h-full p-2" 
                    placeholder={props.placeholder} 
                    type="text" 
                    value={props.value}
                    maxLength={props.max}
                    onChange={(ev) => props.onChange(ev.target.value)} 
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

export default StringField;