import React, { JSX } from "react";
import FloatLabel from "../FloatLabel";

import { IoClose } from "react-icons/io5";
import { IoIosArrowDown } from "react-icons/io";
import { IoIosArrowUp } from "react-icons/io";

export interface SelectFieldProps<T> {
    label?: string;
    placeholder?: string;
    value?: any | any[];
    options: T[];
    multiple?: boolean;
    onChange: (value?: T | T[]) => void;
    handleLabel: (value: T) => any;
}

const SelectField = function <T>(props: SelectFieldProps<T>): JSX.Element {
    const [open, setOpen] = React.useState<boolean>(true);

    const mainClassName  = function (): string {
        return "w-full p-2 h-[52px] mt-5 border-2 rounded hover:shadow hover:shadow-cyan-200 relative cursor-pointer flex justify-between items-center";
    }

    const handleClickValue = function (value: T) {
        props.onChange(value);
    }

    const removeValue = function (): void {
        props.onChange(undefined);
    }

    const hasValue = function (): boolean {
        return ((Array.isArray(props.value) && props.value.length > 0) || props.value !== undefined);
    }

    return (
        <div className="w-full">
            <div className={mainClassName()}>
                {props.label && (
                    <FloatLabel 
                        value={props.label} 
                        has={hasValue()} 
                        onPress={() => setOpen(!open)}
                    />
                )}

                <div onClick={() => setOpen(!open)} className={hasValue() ? "w-[calc(100%-44px)]": "w-[calc(100%-22px)]"}>
                    {props.value ? props.handleLabel(props.value): undefined}
                </div>

                <div className="flex">
                    {hasValue() && (<div className="cursor-pointer hover:opacity-60" onClick={() => removeValue()}><IoClose size={22} /></div>)}
                    <div className="cursor-pointer hover:opacity-60" onClick={() => setOpen(!open)}>{open ? (<IoIosArrowUp size={22} />): (<IoIosArrowDown size={22} />)}</div>
                </div>
            </div>

            {open && (
                <div className="w-full p-2 mt-1 max-h-[40vh] overflow-y-auto border-2 rounded">
                    {props.options.concat(props.options).concat(props.options).map(a => (
                        <div className="w-full p-2" onClick={() => handleClickValue(a)}>
                            {props.handleLabel(a)}
                        </div>
                    ))}
                </div>
            )}

        </div>
    )
}

export default SelectField;