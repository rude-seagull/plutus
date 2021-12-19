import Axios, { AxiosRequestConfig } from 'axios';
import { getSession } from 'next-auth/react';

export const AXIOS_INSTANCE = Axios.create({
    baseURL: process.env.NEXT_PUBLIC_BACKEND_URL,
});

export const customInstance = async <T>(
    config: AxiosRequestConfig,
): Promise<T> => {
    const source = Axios.CancelToken.source();

    const session = await getSession();

    console.log(session.accessToken);

    const improvedConfig = {
        ...config,
        headers: {
            Authorization: `Bearer ${session.accessToken}`,
        },
    };

    const promise = AXIOS_INSTANCE({
        ...improvedConfig,
        cancelToken: source.token,
    }).then(({ data }) => data);

    // eslint-disable-next-line
    // @ts-ignore
    promise.cancel = () => {
        source.cancel('Query was cancelled by React Query');
    };

    return promise;
};
