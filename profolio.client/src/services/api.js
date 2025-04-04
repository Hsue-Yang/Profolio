import axios from 'axios';

//vite 環境變數
const baseURL = import.meta.env.VITE_API_BASE_URL;

const apiClient = axios.create({
    baseURL: baseURL,
    headers: {
        'Content-Type': 'application/json',
    },
});

const handleError = (error, url) => {
    console.error(`${url} failed:`, error);
    throw error;
};

export const get = async (url, params = {}, signal = null) => {
    try {
        const response = await apiClient.get(`${url}`, { params, signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Request cancelled: ${url}`);
        } else {
            handleError(error, `GET ${url}`);
        }
    }
};

export const post = async (url, data = {}) => {
    try {
        const response = await apiClient.post(`${url}`, data);
        return response.data;
    } catch (error) {
        handleError(error, `POST ${url}`);
    }
};

apiClient.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

apiClient.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response) {
            const { status, data } = error.response;
            console.error(`Error ${status}:`, data.message || 'Unknown error');
            if (status === 401) {
                alert('未授權，請重新登入！');
                window.location.href = '/';
            } else if (status === 500) {
                alert('伺服器錯誤，請稍後再試！');
            }
        } else if (error.request) {
            console.error('No response received:', error.request);
        } else {
            console.error('Error in request setup:', error.message);
        }

        return Promise.reject(error);
    }
);


export default apiClient;