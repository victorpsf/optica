import axios, { AxiosRequestConfig } from "axios"
import AppStorage from "../lib/AppStorage";

export interface IClient {
    get: <T = any, D = any>(url: string, config?: AxiosRequestConfig<D>) => Promise<T>;
    post: <T = any, D = any>(url: string, data?: D, config?: AxiosRequestConfig<D>) => Promise<T>
    put: <T = any, D = any>(url: string, data?: D, config?: AxiosRequestConfig<D>) => Promise<T>;
    delete: <T = any, D = any>(url: string, config?: AxiosRequestConfig<D>) => Promise<T>;
}

export const Client = function (baseUrl: string): IClient {
    const instance = axios.create({
        baseURL: baseUrl,
        timeout: 22000
    });

    const addHeaders = function<D = any> (config?: AxiosRequestConfig<D>): AxiosRequestConfig<D> {
        const authentication = AppStorage.get<{ token: string; type: string }>('authentication');

        if (config === undefined && authentication) {
            config = {};
        }

        if (config !== undefined && authentication)
            config.headers = { ...(config?.headers || {}), Authorization: `${authentication.type} ${authentication.token}` }

        return config || {};
    }

    return {
        get: async function<T = any, D = any>(url: string, config?: AxiosRequestConfig<D>): Promise<T> {
            const { data } = await instance.get<T>(url, addHeaders(config));
            return data;
        },
        post: async function<T = any, D = any>(url: string, data?: D, config?: AxiosRequestConfig<D>): Promise<T> {
            const response = await instance.post<T>(url, data, addHeaders(config));
            return response.data;
        },
        put: async function<T = any, D = any>(url: string, data?: D, config?: AxiosRequestConfig<D>): Promise<T> {
            const response = await instance.put<T>(url, data, addHeaders(config));
            return response.data;
        },
        delete: async function<T = any, D = any>(url: string, config?: AxiosRequestConfig<D>): Promise<T> {
            const response = await instance.delete<T>(url, addHeaders(config));
            return response.data;
        }
    }
}