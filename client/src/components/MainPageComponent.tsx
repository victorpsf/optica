import React, { JSX } from "react";
import { routes } from "../pages/router";
import { useNavigate } from "react-router-dom";
import { CgLogOut } from "react-icons/cg";
import { AuthenticationContext } from "../contexts/AuthenticationContext";

export interface IMainPageComponentProps {
    children: JSX.Element;
}

const MainPageComponent = function ({ children }: IMainPageComponentProps): JSX.Element {
    const navigate = useNavigate();
    const auth = React.useContext(AuthenticationContext);

    return (
        <div className="w-full h-full flex">
            <div className="w-[200px] h-full p-2 bg-white">
                <div className="px-2 pt-2 pb-4 flex justify-between items-center border-b-2">
                    <h4 className="font-bold text-lg">Optica</h4>
                    <div className="cursor-pointer hover:opacity-60" onClick={() => auth.singOut()}><CgLogOut size={22} /></div>
                </div>

                {routes.map(a => (
                    <div 
                        className="flex justify-between items-center cursor-pointer hover:opacity-60 border-2 border-white hover:border-gray-200 p-2 rounded"
                        onClick={() => navigate(a.path)}
                    >
                        <div>{a.name}</div>
                        <div>{a.icon}</div>
                    </div>
                ))}
            </div>
            <div className="w-[calc(100%-200px)] h-full p-4">{children}</div>
        </div>
    )
}

export default MainPageComponent;