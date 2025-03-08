import { JSX } from "react";

import { FaHome } from "react-icons/fa";
import MainPage from "./Main/Main";

const iconSize = 22;
export interface IRoute {
    index?: boolean;
    path: string;
    name: string;
    element: JSX.Element;
    icon: JSX.Element;
}

export const routes: IRoute[] = [
    { 
        index: true, 
        path: '/', 
        name: 'Página Inicial', 
        element: <MainPage />, 
        icon: <FaHome size={iconSize} color="black" />
    }
]