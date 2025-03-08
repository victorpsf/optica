import { JSX } from "react";

export interface ILabelProps {
    value: string;
    has: boolean;
}

const FloatLabel = function ({ value, has }: ILabelProps): JSX.Element {
    return (
        <div className={`absolute px-2 transition duration-500 ease-in-out ${has ? 'left-5 top-[-14px] bg-white text-gray-300': 'left-4 top-3'}`}>
            { value }
        </div>
    )
}

export default FloatLabel;