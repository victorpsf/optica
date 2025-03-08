import { JSX, MouseEvent } from "react";
import Loading, { LoadingColor } from "./svg/loading";

export interface IActionProps {
    text: string;
    color?: 'white' | 'primary' | 'danger' | 'warning' | 'success';
    border?: 'white' | 'primary' | 'danger' | 'warning' | 'success';
    disabled?: boolean;
    loading?: boolean;

    onPress: (event: MouseEvent<HTMLDivElement>) => void;
}

const Action = function (props: IActionProps): JSX.Element {
    const handleClick = function (event: MouseEvent<HTMLDivElement>): void {
        if (props.disabled)
            return;

        props.onPress(event);
    }

    const ColorClassName = function (): string {
        switch(props.color) {
            case "white":   return "bg-white text-black";
            case "danger":  return "bg-red-500 text-white";
            case "success": return "bg-green-500 text-white";
            case "warning": return "bg-yellow-500 text-white";
            default:        return "bg-blue-600 text-white";
        }
    }

    const BoderClassName = function (): string {
        switch(props.border) {
            case "white":   return "border-black border-2 text-black";
            case "danger":  return "border-red-500 border-2 text-red-600";
            case "success": return "border-green-500 border-2 text-green-600";
            case "warning": return "border-yellow-500 border-2 text-yellow-600";
            case "primary": return "border-blue-500 border-2 text-blue-600";
            default:        return "";
        }
    }

    const LoadingColor = function (): LoadingColor | undefined {
        switch(props.border) {
            case "white":   return "black";
            case "danger":  return "red";
            case "success": return "green";
            case "warning": return "yellow";
            case "primary": return "blue";
            default:        return undefined;
        }
    }

    return (
        <div className={`w-full flex flex-row ${ColorClassName()} ${BoderClassName()} ${(props.disabled || props.loading) ? 'cursor-not-allowed opacity-60': 'cursor-pointer hover:opacity-60'} p-2 rounded`}>
            <div className="w-[10%]"></div>
            <div className={`w-[80%] text-center`} onClick={(event) => handleClick(event)}>{props.text}</div>
            <div className="w-[10%] flex justify-center items-center">{props.loading ? <Loading color={LoadingColor()} />: undefined}</div>
        </div>
    )
}

export default Action;