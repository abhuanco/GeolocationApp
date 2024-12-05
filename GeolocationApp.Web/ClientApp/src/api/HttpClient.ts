import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';
export default class HttpClient<T, R> {
    private client: AxiosInstance;
    
    constructor() {
        const baseURL = process.env.VITE_API_URL || '';
        this.client = axios.create({
            baseURL,
            headers: {
                'Content-Type': 'application/json',
            },
        });
    }

    async get(endpoint: string, config?: AxiosRequestConfig): Promise<T> {
        const response: AxiosResponse<T> = await this.client.get(endpoint, config);
        return response.data;
    }

    async all(endpoint: string, config?: AxiosRequestConfig, filters?: Record<string, any>): Promise<R> {
        try {
            const queryParams = new URLSearchParams(filters as Record<string, string>).toString();
            const url = queryParams ? `${endpoint}?${queryParams}` : endpoint;

            const response: AxiosResponse<R> = await this.client.get(url, config);
            return response.data;
        } catch (error) {
            console.error('Error fetching data:', error);
            throw error;
        }
    }

    async post(endpoint: string, data: Partial<T>, config?: AxiosRequestConfig): Promise<T> {
        const response: AxiosResponse<T> = await this.client.post(endpoint, data, config);
        return response.data;
    }

    async put(endpoint: string, id: number | string, data: Partial<T>, config?: AxiosRequestConfig): Promise<T> {
        const response: AxiosResponse<T> = await this.client.put(`${endpoint}/${id}`, data, config);
        return response.data;
    }

    async delete(endpoint: string, id: number | string, config?: AxiosRequestConfig): Promise<void> {
        await this.client.delete(`${endpoint}/${id}`, config);
    }
}